using System;

class Program
{
    static void Main(string[] args)
    {
        // Exibe o menu de opções de entrada
        Console.WriteLine("Conversor de Moeda");
        Console.Write("Digite o valor a ser convertido: ");
        double valorOrigem = Convert.ToDouble(Console.ReadLine());

        // Solicita a taxa de conversão
        Console.Write("Digite a taxa de conversão (1 unidade de origem para quantas unidades da moeda de destino): ");
        double taxaConversao = Convert.ToDouble(Console.ReadLine());

        // Calcula o valor convertido
        double valorConvertido = valorOrigem;

        // Exibe o valor convertido
        Console.WriteLine($"Valor convertido: {valorConvertido}");
    }
}