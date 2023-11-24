using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class PartidaDeXadrez
    {
        public QuadroDeJogo Quadro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Quadro = new QuadroDeJogo(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Quadro.RetirarPeca(origem);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Quadro.RetirarPeca(destino);
            Quadro.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            Quadro.ColocarPeca(new Torre(Cor.Branca, Quadro), new PosicaoXadrez('c', 1).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Branca, Quadro), new PosicaoXadrez('c', 2).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Branca, Quadro), new PosicaoXadrez('d', 2).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Branca, Quadro), new PosicaoXadrez('e', 2).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Branca, Quadro), new PosicaoXadrez('e', 1).ToPosicao());
            Quadro.ColocarPeca(new Rei(Cor.Branca, Quadro), new PosicaoXadrez('d', 1).ToPosicao());

            Quadro.ColocarPeca(new Torre(Cor.Preta, Quadro), new PosicaoXadrez('c', 7).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Preta, Quadro), new PosicaoXadrez('c', 8).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Preta, Quadro), new PosicaoXadrez('d', 7).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Preta, Quadro), new PosicaoXadrez('e', 7).ToPosicao());
            Quadro.ColocarPeca(new Torre(Cor.Preta, Quadro), new PosicaoXadrez('e', 8).ToPosicao());
            Quadro.ColocarPeca(new Rei(Cor.Preta, Quadro), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }
}
