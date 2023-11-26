namespace XadrezConsole.Tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public QuadroDeJogo Quadro { get; protected set; }

        public Peca() { }

        public Peca(Cor cor, QuadroDeJogo quadro)
        {
            Posicao = null;
            Cor = cor;
            QteMovimentos = 0;
            Quadro = quadro;
        }

        public void IncrementarMovimentos()
        {
            QteMovimentos++;
        }

        public void DecrementarMovimentos()
        {
            QteMovimentos--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < Quadro.Linhas; i++)
            {
                for (int j = 0; j < Quadro.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
