using XadrezConsole.Tabuleiro;

namespace XadrezConsole
{
    internal class Tela
    {
        public static void ImprimirQuadro(QuadroDeJogo quadro)
        {
            for (int i = 0; i < quadro.Linhas; i++)
            {
                for (int j = 0; j < quadro.Colunas; j++)
                {
                    if (quadro.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{quadro.Peca(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
