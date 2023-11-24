using XadrezConsole.Tabuleiro;
using XadrezConsole.Tabuleiro.Exceptions;

namespace XadrezConsole.Xadrez
{
    internal class PartidaDeXadrez
    {
        public QuadroDeJogo Quadro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
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

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Quadro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Quadro.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Quadro.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Quadro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
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
