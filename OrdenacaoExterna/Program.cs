


using OrdenacaoExterna;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

class IntercalamentoBalanceadoMultiplosCaminhos()
{
    const int TAMANHO_MEMORIA = 100;
    const int NUM_CAMINHOS = 50;

    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string input = "";
        string output = "";
        
        const string INPUT_ARQUIVO_TESTE = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseTeste\\ordExt_InputTeste.txt";
        const string OUTUPUT_ARQUIVO_TESTE = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseTeste\\ordExt_OutPutTeste.txt";
        const string INPUT_ARQUIVO_REAL = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseReal\\ordExt_Input.txt";
        const string OUTPUT_ARQUIVO_REAL = "C:\\Users\\lucio\\OneDrive\\Documentos\\Estudo\\IFMG\\ProgIV\\TP2-Ordenacao-Externa\\OrdenacaoExterna\\OrdenacaoExterna\\BaseReal\\ordExt_OutPutTeste.txt"; ;

        Console.WriteLine("Digite 1 para arquivo de teste e 2 para arquivo real:");
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
            
        

        LeitorArquivos leitorEGeradorDeArquivos = LeitorArquivos.InstanciaGlobal;
        Ordenador ordenador = Ordenador.InstanciaGlobal;

        List<string> CaminhosArquivosTemporarios = leitorEGeradorDeArquivos
                .RetornaCaminhoBlocosIniciais(input, TAMANHO_MEMORIA);

        while (CaminhosArquivosTemporarios.Count > 1)
        {
            CaminhosArquivosTemporarios = ordenador
                .IntercalaBlocosDeAcordoComCaminhos(CaminhosArquivosTemporarios, NUM_CAMINHOS);
        }

        RenomeiaArquivo(CaminhosArquivosTemporarios[0], output);

        stopwatch.Stop();
        Console.WriteLine($"O tempo de execução do programa foi: {stopwatch.ElapsedMilliseconds} ms.");
    }

    private static void RenomeiaArquivo(string alvo, string novoNome)
    {
        File.Move(alvo, novoNome);
        Console.WriteLine("Intercalação concluída. Arquivo final: " + novoNome);   
    }

}