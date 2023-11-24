using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, QuadroDeJogo quadro) : base(cor, quadro)
        {

        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Quadro.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Quadro.Linhas, Quadro.Colunas];

            Posicao pos = new Posicao(0, 0);

            // norte
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Quadro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Quadro.Peca(pos) != null && Quadro.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }

            // sul
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Quadro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Quadro.Peca(pos) != null && Quadro.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }

            // leste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Quadro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Quadro.Peca(pos) != null && Quadro.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            // oeste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Quadro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Quadro.Peca(pos) != null && Quadro.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }
            return mat;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
