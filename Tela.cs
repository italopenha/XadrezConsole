using XadrezConsole.Tabuleiro;

namespace XadrezConsole
{
    internal class Tela
    {
        public static void ImprimirQuadro(QuadroDeJogo quadro)
        {
            for (int i = 0; i < quadro.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < quadro.Colunas; j++)
                {
                    if (quadro.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(quadro.Peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
