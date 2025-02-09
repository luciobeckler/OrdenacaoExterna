


using OrdenacaoExterna;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

class IntercalamentoBalanceadoMultiplosCaminhos()
{
    const int TAMANHO_MEMORIA = 300;
    const int NUM_CAMINHOS = 10;

    static void Main(string[] args)
    {
        

        string input = "";
        string output = "";
        
        const string INPUT_ARQUIVO_TESTE = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseTeste\\ordExt_InputTeste.txt";
        const string OUTUPUT_ARQUIVO_TESTE = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseTeste\\ordExt_OutPutTeste.txt";
        const string INPUT_ARQUIVO_REAL = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseReal\\ordExt_Input.txt";
        const string OUTPUT_ARQUIVO_REAL = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseReal\\ordExt_OutPutTeste.txt"; ;
        
        const string pastaSuporte = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\ArquivosSuporte";

        Console.WriteLine("1 - Arquivo teste");
        Console.WriteLine("2 - Arquivo real");
        string resposta = Console.ReadLine();

        if (resposta == "1")
        {
            input = INPUT_ARQUIVO_TESTE;
            output = OUTUPUT_ARQUIVO_TESTE;
        }
        else if (resposta == "2")
        {
            input = INPUT_ARQUIVO_REAL;
            output = OUTPUT_ARQUIVO_REAL;
        }

        Stopwatch stopWatchCriacaoBlocos = new Stopwatch();
        stopWatchCriacaoBlocos.Start();


        LeitorArquivos leitorEGeradorDeArquivos = LeitorArquivos.InstanciaGlobal;
        Ordenador ordenador = Ordenador.InstanciaGlobal;

        leitorEGeradorDeArquivos
                .RetornaCaminhoBlocosIniciais(input, TAMANHO_MEMORIA, pastaSuporte);
        
        stopWatchCriacaoBlocos.Stop();
        Console.WriteLine("Tempo criação blocos iniciais: " + stopWatchCriacaoBlocos.ElapsedMilliseconds);

        Stopwatch stopWatchIntercalacao = new Stopwatch();                 
        stopWatchIntercalacao.Start();

        ordenador.IntercalaBlocos(pastaSuporte, NUM_CAMINHOS);

        stopWatchIntercalacao.Stop();
        Console.WriteLine("Tempo intercalação: " + stopWatchIntercalacao.ElapsedMilliseconds);

        
        //RenomeiaArquivo(CaminhosArquivosTemporarios[0], output);
    }
    private static void RenomeiaArquivo(string alvo, string novoNome)
    {
        File.Move(alvo, novoNome);
        Console.WriteLine("Intercalação concluída. Arquivo final: " + novoNome);   
    }

}