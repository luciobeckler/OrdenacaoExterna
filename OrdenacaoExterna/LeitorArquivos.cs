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

        public List<string> RetornaCaminhoBlocosIniciais(string path, int tamanho_memoria)
        {
            var caminhosArquivosTemp = new List<string>();
            var buffer = new List<double>(tamanho_memoria);

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    buffer.Clear();

                    for (int i = 0; i < tamanho_memoria && !reader.EndOfStream; i++) 
                    {
                        string line = reader.ReadLine();
                        double doubleNumber = double.Parse(line, CultureInfo.InvariantCulture);
                        buffer.Add(doubleNumber);
                    }

                    buffer.Sort();

                    string caminhoArquivoTemp = Path.GetTempFileName();
                    caminhosArquivosTemp.Add(caminhoArquivoTemp);

                    using (var writer = new StreamWriter(caminhoArquivoTemp))
                    {
                        foreach (var num in buffer)
                        {
                            writer.WriteLine(num);
                        }
                    }
                }
            }
            
            return caminhosArquivosTemp;
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
