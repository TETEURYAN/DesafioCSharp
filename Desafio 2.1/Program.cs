using Desafio_2._1.Services;
using Microsoft.VisualBasic;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Conversor de Moeda");

        Console.Write("Digite a moeda de origem: ");
        string moedaOrigem = Console.ReadLine().ToUpper();  

       
        Console.Write("Digite a moeda de destino: ");
        string moedaDestino = Console.ReadLine().ToUpper();  

       
        Console.Write("Digite o valor: ");
        if (!double.TryParse(Console.ReadLine(), out double valorEntrada))
        {
            Console.WriteLine("Valor inválido. Tente novamente.");
            return;
        }

        Request APIConversor = new Request();

        
        Conversion conversao = await APIConversor.IntegracaoAsync(moedaOrigem, moedaDestino);

        
        if (conversao.Concrete)
        {
            Console.WriteLine("Erro ao obter a taxa de conversão.");
            return;
        }

        // Calcula o valor convertido
        double valorConvertido = Math.Round(valorEntrada * conversao.ConversionRate, 2, MidpointRounding.ToEven);

        // Exibe as informações de conversão
        Console.WriteLine($"Taxa de conversão: {conversao.ConversionRate:F6}");
        Console.WriteLine($"Valor convertido: {moedaDestino} {valorConvertido:F2}");
    }
}
