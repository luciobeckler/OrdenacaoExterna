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

        public List<string> IntercalaBlocosDeAcordoComCaminhos(List<string> caminhosArquivosTemp, int quantCaminhos)
        {
            var novosCaminhosResultantesIntercalacao = new List<string>();

            for (int i = 0; i < caminhosArquivosTemp.Count; i += quantCaminhos)
            {
                var caminhosParaIntercalação = caminhosArquivosTemp
                    .GetRange(i, Math.Min(quantCaminhos, caminhosArquivosTemp.Count - i));

                string arquivoResultanteIntercalacao = IntercalaBlocosERetornaArquivoResultante(caminhosParaIntercalação);

                novosCaminhosResultantesIntercalacao.Add(arquivoResultanteIntercalacao);
            }

            return novosCaminhosResultantesIntercalacao;
        }

        private string IntercalaBlocosERetornaArquivoResultante(List<string> caminhosParaIntercalação)
        {
            string arquivoResultante = Path.GetTempFileName();

            // Lista de leitores para os arquivos temporários
            var leitores = new List<StreamReader>();
            var numeros = new List<double>();

            try
            {
                // Abre todos os arquivos para leitura
                foreach (var path in caminhosParaIntercalação)
                {
                    var reader = new StreamReader(path);
                    leitores.Add(reader);
                }

                // Lê a primeira linha de cada arquivo
                for (int i = 0; i < leitores.Count; i++)
                {
                    if (!leitores[i].EndOfStream)
                    {
                        string line = leitores[i].ReadLine();
                        if (double.TryParse(line, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
                        {
                            numeros.Add(numero);
                        }
                    }
                }

                // Intercala os números e escreve no arquivo resultante
                using (var writer = new StreamWriter(arquivoResultante))
                {
                    while (numeros.Count > 0)
                    {
                        // Encontra o menor número na lista
                        double menorNumero = numeros.Min();
                        int indiceMenorNumero = numeros.IndexOf(menorNumero);

                        // Escreve o menor número no arquivo resultante
                        writer.WriteLine(menorNumero.ToString(CultureInfo.InvariantCulture));

                        // Lê o próximo número do arquivo correspondente
                        if (!leitores[indiceMenorNumero].EndOfStream)
                        {
                            string line = leitores[indiceMenorNumero].ReadLine();
                            if (double.TryParse(line, NumberStyles.Any, CultureInfo.InvariantCulture, out double proximoNumero))
                            {
                                numeros[indiceMenorNumero] = proximoNumero;
                            }
                            else
                            {
                                numeros.RemoveAt(indiceMenorNumero);
                                leitores[indiceMenorNumero].Close();
                                leitores.RemoveAt(indiceMenorNumero);
                            }
                        }
                        else
                        {
                            numeros.RemoveAt(indiceMenorNumero);
                            leitores[indiceMenorNumero].Close();
                            leitores.RemoveAt(indiceMenorNumero);
                        }
                    }
                }
            }
            finally
            {
                // Fecha todos os leitores
                foreach (var reader in leitores)
                {
                    reader.Close();
                }
            }

            return arquivoResultante;
        }
    }
}
