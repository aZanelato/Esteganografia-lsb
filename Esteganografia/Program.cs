using System;
using System.IO;
using System.Threading;

namespace Esteganografia
{
    class Program
    {
        static void Main(string[] args)
        {
            var op = "9";
            while (op != "3")
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("   Esteganografia");
                Console.WriteLine("----------------------\n");
                Console.WriteLine("1 - Codificar imagem \n" +
                    "2 - Decodificar imagem \n" +
                    "3 - Sair\n");
                Console.Write("Digite a opção escolhida: ");
                op = Console.ReadLine();

                try
                {
                    switch (op)
                    {
                        case "1":
                            Console.Clear();
                            Console.Write("Digite o caminho da imagem bmp: ");
                            var caminho = Console.ReadLine();

                            if (File.Exists(caminho))
                            {
                                Console.Write("Digite a mensagem que deseja codificar: ");
                                var texto = Console.ReadLine();
                                Esteganografia.Criptografar(caminho, texto);
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Arquivo não encontrado !");
                                Thread.Sleep(1500);
                            }

                            Console.Clear();
                            break;

                        case "2":
                            Console.Clear();
                            Console.Write("Digite o caminho da imagem que deseja decodificar: ");
                            caminho = Console.ReadLine();

                            if (File.Exists(caminho))
                            {
                                var msg = Esteganografia.Descriptografar(caminho);
                                Console.WriteLine(msg);
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Arquivo não encontrado !");
                                Thread.Sleep(1500);
                            }

                            Console.Clear();
                            break;

                        case "3":
                            Console.WriteLine("Fechando..");
                            Thread.Sleep(1500);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
