using OrdenacaoExterna;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenacaoExterna
{
    public class Ordenador
    {
        private static Ordenador _instance;
        LeitorArquivos leitorEGeradorDeArquivos = LeitorArquivos.InstanciaGlobal;
        Escritor escritor = Escritor.InstanciaGlobal;


        private Ordenador() { }
        public static Ordenador InstanciaGlobal
        {
            get
            {
                if (_instance == null)
                    _instance = new Ordenador();

                return _instance;
            }
        }

        public string IntercalaBlocos(string pastaSuporte, int quantCaminhos)
        {
            int numIntercalacao = 1; // Contador para nomear os subdiretórios

            while (true)
            {
                // Obtém a lista de arquivos no diretório atual
                string[] arquivos = Directory.GetFiles(pastaSuporte);
                int numArquivos = arquivos.Length;

                // Condição de parada: apenas um arquivo restante
                if (numArquivos == 1)
                {
                    return arquivos[0]; // Retorna o caminho do arquivo final
                }

                // Cria um novo subdiretório para armazenar os arquivos intercalados
                string novoSubdiretorio = Path.Combine(pastaSuporte, $"Intercalacao_{numIntercalacao}");
                Directory.CreateDirectory(novoSubdiretorio);

                // Processa os arquivos em grupos de 'quantCaminhos'
                for (int i = 0; i < numArquivos; i += quantCaminhos)
                {
                    // Determina quantos arquivos serão intercalados neste grupo
                    int quantArquivosNoGrupo = Math.Min(quantCaminhos, numArquivos - i);

                    // Lista para armazenar os leitores e valores atuais de cada arquivo
                    List<StreamReader> leitores = new List<StreamReader>();
                    List<double> valoresAtuais = new List<double>();

                    // Inicializa os leitores e valores atuais
                    for (int j = 0; j < quantArquivosNoGrupo; j++)
                    {
                        var leitor = new StreamReader(arquivos[i + j]);
                        leitores.Add(leitor);
                        valoresAtuais.Add(double.Parse(leitor.ReadLine(), CultureInfo.InvariantCulture));
                    }

                    // Cria o arquivo de saída para este grupo
                    string arquivoSaida = Path.Combine(novoSubdiretorio, $"intercalado_{i / quantCaminhos + 1}.txt");
                    using (var escritor = new StreamWriter(arquivoSaida))
                    {
                        // Intercala os valores até que todos os arquivos sejam totalmente lidos
                        while (valoresAtuais.Any(v => v != double.MaxValue))
                        {
                            // Encontra o menor valor atual
                            double menorValor = valoresAtuais.Min();
                            int indiceMenor = valoresAtuais.IndexOf(menorValor);

                            // Escreve o menor valor no arquivo de saída
                            escritor.WriteLine(menorValor.ToString(CultureInfo.InvariantCulture));

                            // Lê o próximo valor do arquivo correspondente
                            if (!leitores[indiceMenor].EndOfStream)
                            {
                                valoresAtuais[indiceMenor] = double.Parse(leitores[indiceMenor].ReadLine(), CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                // Marca o arquivo como concluído
                                valoresAtuais[indiceMenor] = double.MaxValue;
                            }
                        }
                    }

                    // Fecha os leitores
                    foreach (var leitor in leitores)
                    {
                        leitor.Close();
                    }
                }

                // Atualiza a pasta de suporte para o novo subdiretório
                pastaSuporte = novoSubdiretorio;
                numIntercalacao++;
            }
        }

        private int CalculaIntercalacoesNecessarias(int quantidadeBlocos, int quantCaminhos)
        {
            int intercalacoes = quantidadeBlocos / quantCaminhos;
            if (quantidadeBlocos % quantCaminhos != 0)
                intercalacoes += 1;

            return intercalacoes;
        }
    }
}
