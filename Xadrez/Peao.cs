using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Peao : Peca
    {
        private PartidaDeXadrez Partida;
        public Peao(Cor cor, QuadroDeJogo quadro, PartidaDeXadrez partida) : base(cor, quadro)
        {
            Partida = partida;
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Quadro.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Quadro.Peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Quadro.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Quadro.Linhas, Quadro.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Quadro.PosicaoValida(pos) && Livre(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Quadro.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Quadro.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Quadro.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                // #jogadaespecial en passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Quadro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Quadro.Peca(esquerda) == Partida.VulneravelEnPassant)
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Quadro.PosicaoValida(direita) && ExisteInimigo(direita) && Quadro.Peca(direita) == Partida.VulneravelEnPassant)
                        mat[direita.Linha - 1, direita.Coluna] = true;
                }
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Quadro.PosicaoValida(pos) && Livre(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Quadro.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Quadro.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Quadro.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                // #jogadaespecial en passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Quadro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Quadro.Peca(esquerda) == Partida.VulneravelEnPassant)
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Quadro.PosicaoValida(direita) && ExisteInimigo(direita) && Quadro.Peca(direita) == Partida.VulneravelEnPassant)
                        mat[direita.Linha + 1, direita.Coluna] = true;
                }
            }
            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
