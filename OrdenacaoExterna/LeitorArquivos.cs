using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenacaoExterna
{
    public class LeitorArquivos
    {
        private static LeitorArquivos _instance;

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

        public void RetornaCaminhoBlocosIniciais(string path, int tamanho_memoria, string pastaDestino)
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

                    for (; count < tamanho_memoria && !reader.EndOfStream; count++)
                    {
                        string line = reader.ReadLine();
                        buffer[count] = double.Parse(line, CultureInfo.InvariantCulture);
                    }

                    Array.Sort(buffer, 0, count);

                    string caminhoArquivoTemp = Path.Combine(pastaDestino, $"{contadorArquivos}.txt");
                    contadorArquivos++;

                    using (var writer = new StreamWriter(caminhoArquivoTemp))
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


        //!Se o arquivo for realmente MUITO grande pode vir a dar problema de falta de RAM neste método.
        public List<double> GeraListaNumeros(string path)
        {
            var listaNumeros = new List<double>();

            var linhas = File.ReadAllLines(path);

            foreach (var linha in linhas)
            {
                string linhaCorrigida = linha.Replace(',', '.');

                listaNumeros.Add(double.Parse(linhaCorrigida, CultureInfo.InvariantCulture));
            }

            return listaNumeros;
        }

    }
}
