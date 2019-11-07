using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;

namespace Esteganografia
{
    public static class Esteganografia
    {
        public static void Criptografar(string caminho, string mensagem)
        {
            try
            {
                //Cria uma instância da imagem bmp
                using var bmp = new Bitmap(Image.FromFile(caminho));

                //Verifica se a mensagem é maior do que o limite que um pixel armazena
                if (mensagem.Length > 255)
                    throw new Exception("Mensagem muito grande, deve conter no máximo 255 caracteres");

                Console.WriteLine("Codificando imagem..");

                //Percorre a altura e largura da imagem, percorrendo pixel por pixel
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        //Recebe a informação do pixel atual
                        var pixel = bmp.GetPixel(i, j);

                        //Insere a mensagem nos pixels de altura da imagem
                        if (i < 1 && j < mensagem.Length)
                        {
                            //Converte a letra para um char e depois converte para ascii
                            var letra = Convert.ToChar(mensagem.Substring(j, 1));
                            var valor = Convert.ToInt32(letra);

                            //Insere o valor da letra em ascii para o pixel B
                            bmp.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, valor));

                        }
                        //Armazena no ultimo pixel da imagem a quantidade de caractéres que contém a mensagem
                        if (i == bmp.Width - 1 && j == bmp.Height - 1)
                        {
                            bmp.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, mensagem.Length));
                        }
                    }
                }

                var local = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "imgCodificada.bmp");
                bmp.Save(local);

                Console.WriteLine("Imagem codificada salva na área de trabalho !");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao codificar a imagem !\n" + ex.Message);
            }
        }

        public static string Descriptografar(string caminho)
        {
            try
            {
                //Cria uma instância da imagem bmp
                using var bmp = new Bitmap(Image.FromFile(caminho));

                Console.WriteLine("Decodificando imagem..");

                //Variavel que irá receber a mensagem
                var msg = "";

                //Recebe o ultimo pixel que contem as informações da quantidade de caractéres que contem a mensagem
                var ultimoPixel = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
                int tamanhoMensagem = ultimoPixel.B;

                //Percorre a largura e altura da imagem, percorrendo pixel por pixel
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        //Recebe as informações do pixel atual
                        var pixel = bmp.GetPixel(i, j);

                        //Verifica os pixels pelo tamanho da mensagem, percorrendo na altura da imagem
                        if (i < 1 && j < tamanhoMensagem)
                        {
                            //Recebe o valor do pixel b e converte para letra, não está convertendo caractéres especiais
                            int valor = pixel.B;
                            var letra = Encoding.ASCII.GetString(new byte[] { Convert.ToByte(valor) });
                            msg += letra;
                        }
                    }
                }
            

                if (msg == string.Empty)
                    return "Nenhuma mensagem encontrada nessa imagem !";

                return msg;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao decodificar a imagem !\n" + ex.Message);
            }
        }
    }
}
