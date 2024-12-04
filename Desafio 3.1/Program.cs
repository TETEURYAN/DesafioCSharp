using System;

namespace ConsultorioOdontologico
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================");
                Console.WriteLine("   Bem-vindo ao Consultório Odontológico");
                Console.WriteLine("=======================================");
                Console.WriteLine("1 - Cadastro de Pacientes");
                Console.WriteLine("2 - Agenda");
                Console.WriteLine("3 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        PacienteMenuView.Display();
                        break;

                    case "2":
                        AgendaMenuView.Display();
                        break;

                    case "3":
                        Console.WriteLine("Encerrando o sistema... Até logo!");
                        return;

                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
