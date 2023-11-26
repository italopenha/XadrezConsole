using System.Collections.Generic;
using XadrezConsole.Tabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirQuadro(partida.Quadro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine($"Turno: {partida.Turno}");
            if(!partida.Terminada)
            {
                Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
                if (partida.Xeque)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Xeque!");
                    Console.ForegroundColor = aux;
                }
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("XEQUE MATE!");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Vencedor: {partida.JogadorAtual}");
                Console.ForegroundColor = aux;
            }                         
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write($"{x} ");
            }
            Console.Write("]");
        }

        public static void ImprimirQuadro(QuadroDeJogo quadro)
        {
            for (int i = 0; i < quadro.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < quadro.Colunas; j++)
                {
                    ImprimirPeca(quadro.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirQuadro(QuadroDeJogo quadro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < quadro.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < quadro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(quadro.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse($"{s[1]} ");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
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
                Console.Write(" ");
            }
        }
    }
}
