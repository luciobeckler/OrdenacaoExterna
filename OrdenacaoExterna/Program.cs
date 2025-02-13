using OrdenacaoExterna;
using System.Diagnostics;

class IntercalamentoBalanceadoMultiplosCaminhos()
{
    const int TAMANHO_MEMORIA = 300;
    const int NUM_CAMINHOS = 10;

    static void Main(string[] args)
    {
        string diretorioAtual = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\")); 
        string pastaSuporte = Path.Combine(diretorioAtual, "PastaSuporte");
        string input = VerificaQualArquivoSeraUtilizado(diretorioAtual);
        
        Stopwatch stopWatchCriacaoBlocos = new Stopwatch();
        stopWatchCriacaoBlocos.Start();

        LeitorArquivos leitorEGeradorDeArquivos = LeitorArquivos.InstanciaGlobal;
        leitorEGeradorDeArquivos
                .CriaBlocosIniciaisNaPastaSuporte(input, TAMANHO_MEMORIA, pastaSuporte);
        
        stopWatchCriacaoBlocos.Stop();
        double tempoCriacaoBlocosMilisegundos = stopWatchCriacaoBlocos.ElapsedMilliseconds;
        double tempoCriacaoBlocosSegundos = tempoCriacaoBlocosMilisegundos / 1000;
        double tempoCriacaoBlocosMinutos = tempoCriacaoBlocosSegundos / 60;

        Console.WriteLine($"Tempo criação blocos iniciais: {tempoCriacaoBlocosMilisegundos}ms -  {tempoCriacaoBlocosSegundos}s -  {tempoCriacaoBlocosMinutos}min");

        Stopwatch stopWatchIntercalacao = new Stopwatch();                 
        stopWatchIntercalacao.Start();

        Ordenador ordenador = Ordenador.InstanciaGlobal;
        string arquivoOrdenadoPath = ordenador.GeraArquivoOrdenadoERetornaPath(pastaSuporte, NUM_CAMINHOS);

        stopWatchIntercalacao.Stop();
        double tempoIntercalacaoMilisegundos = stopWatchCriacaoBlocos.ElapsedMilliseconds;
        double tempoIntercalacaoSegundos = tempoCriacaoBlocosMilisegundos / 1000;
        double tempoIntercalacaoMinutos = tempoCriacaoBlocosSegundos / 60;

        Console.WriteLine($"Tempo intercalação: {tempoIntercalacaoMilisegundos}ms - {tempoIntercalacaoSegundos}s - {tempoIntercalacaoMinutos}min");
        Console.WriteLine($"O resultado da ordenação do arquivo solicitado pode ser encontrada na pasta: {arquivoOrdenadoPath}");
        Console.WriteLine($"O tempo de execução total foi de {tempoCriacaoBlocosSegundos + tempoIntercalacaoSegundos}s - {tempoCriacaoBlocosMinutos + tempoIntercalacaoMinutos}min");
    }

    private static string VerificaQualArquivoSeraUtilizado(string diretorioAtual)
    {
        Console.WriteLine("1 - Arquivo teste");
        Console.WriteLine("2 - Arquivo real");
        string resposta = Console.ReadLine()!;

        string INPUT_ARQUIVO_TESTE = Path.Combine(diretorioAtual, "BaseTeste", "ordExt_InputTeste.txt");
        string INPUT_ARQUIVO_REAL = Path.Combine(diretorioAtual, "BaseReal", "ordExt_Input.txt");

        if (resposta == "1")
            return INPUT_ARQUIVO_TESTE;
        else if (resposta == "2")
            return INPUT_ARQUIVO_REAL;
        else
            throw new Exception("Número selecionado inválido");
    }
}