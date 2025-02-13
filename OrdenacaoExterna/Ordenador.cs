using System.Globalization;

namespace OrdenacaoExterna
{
    public class Ordenador
    {
        private static Ordenador _instance;

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

        public string GeraArquivoOrdenado(string pastaSuporte, int quantCaminhos)
        {
            int numIntercalacao = 1; 

            while (true)
            {
                string[] arquivos = Directory.GetFiles(pastaSuporte);
                int numArquivos = arquivos.Length;

                if (numArquivos == 1)
                {
                    return arquivos[0]; 
                }

                string novoSubdiretorio = Path.Combine(pastaSuporte, $"Intercalacao_{numIntercalacao}");
                Directory.CreateDirectory(novoSubdiretorio);

                for (int i = 0; i < numArquivos; i += quantCaminhos)
                {
                    int quantArquivosNoGrupo = Math.Min(quantCaminhos, numArquivos - i);

                    List<StreamReader> leitores = new List<StreamReader>();
                    List<double> valoresAtuais = new List<double>();

                    for (int j = 0; j < quantArquivosNoGrupo; j++)
                    {
                        var leitor = new StreamReader(arquivos[i + j]);
                        leitores.Add(leitor);
                        valoresAtuais.Add(double.Parse(leitor.ReadLine(), CultureInfo.InvariantCulture));
                    }

                    string arquivoSaida = Path.Combine(novoSubdiretorio, $"intercalado_{i / quantCaminhos + 1}.txt");
                    using (var escritor = new StreamWriter(arquivoSaida))
                    {
                        while (valoresAtuais.Any(v => v != double.MaxValue))
                        {
                            double menorValor = valoresAtuais.Min();
                            int indiceMenor = valoresAtuais.IndexOf(menorValor);

                            escritor.WriteLine(menorValor.ToString(CultureInfo.InvariantCulture));

                            if (!leitores[indiceMenor].EndOfStream)
                            {
                                valoresAtuais[indiceMenor] = double.Parse(leitores[indiceMenor].ReadLine(), CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                valoresAtuais[indiceMenor] = double.MaxValue;
                            }
                        }
                    }

                    foreach (var leitor in leitores)
                    {
                        leitor.Close();
                    }
                }

                pastaSuporte = novoSubdiretorio;
                numIntercalacao++;
            }
        }
    }
}
