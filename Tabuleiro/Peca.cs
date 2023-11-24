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

        public abstract bool[,] MovimentosPossiveis();
    }
}
