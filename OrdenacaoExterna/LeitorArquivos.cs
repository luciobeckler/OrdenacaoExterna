using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text;

namespace OrdenacaoExterna
{
    public class LeitorArquivos
    {
        private static LeitorArquivos? _instance;
        private LeitorArquivos() { }
        public static LeitorArquivos InstanciaGlobal
        {
            get
            {
                if (_instance == null)
                    _instance = new LeitorArquivos();

                return _instance;
            }
        }

        public void CriaBlocosIniciaisNaPastaSuporte(string path, int tamanho_memoria, string pastaDestino)
        {
            var buffer = new double[tamanho_memoria];
            int contadorArquivos = 1;

            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    int count = 0;
                    while(count < tamanho_memoria && !reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        buffer[count] = double.Parse(line, CultureInfo.InvariantCulture);

                        count++;
                    }

                    Array.Sort(buffer, 0, count);

                    string bloco = Path.Combine(pastaDestino, $"{contadorArquivos}.txt");
                    contadorArquivos++;

                    using (var writer = new StreamWriter(bloco))
                    {
                        var sb = new StringBuilder();
                        for (int i = 0; i < count; i++)
                        {
                            sb.AppendLine(buffer[i].ToString(CultureInfo.InvariantCulture));
                        }
                        writer.Write(sb.ToString());
                    }
                }
            }
            return;
        }
    }
}
