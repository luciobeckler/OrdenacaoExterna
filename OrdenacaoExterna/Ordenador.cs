using OrdenacaoExterna;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            List<double> numerosArquivos = new List<double>();

            foreach (var path in caminhosParaIntercalação)
            {
                var numerosDoArquivo = leitorEGeradorDeArquivos.GeraListaNumeros(path);
                numerosArquivos.AddRange(numerosDoArquivo);
            }
            numerosArquivos.Sort();

            arquivoResultante = escritor.EscreveNumerosNoArquivo(numerosArquivos, arquivoResultante);
            return arquivoResultante;
        }
    }
}
