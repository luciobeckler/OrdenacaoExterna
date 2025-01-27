using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenacaoExterna
{
    public class Escritor
    {
        private static Escritor _instance;

        private Escritor() { }

        public static Escritor InstanciaGlobal
        {
            get
            {
                if (_instance == null)
                    _instance = new Escritor();

                return _instance;
            }
        }

        public string EscreveNumerosNoArquivo(List<double> numerosArquivos, string arquivoResultante)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(arquivoResultante))
                {
                    foreach (var numero in numerosArquivos)
                    {
                        writer.WriteLine(numero);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao escrever no arquivo: {ex.Message}");
                throw;
            }

            return arquivoResultante;
        }
    }
}
