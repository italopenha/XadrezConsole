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
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadrez()
        {
            Quadro = new QuadroDeJogo(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Quadro.RetirarPeca(origem);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Quadro.RetirarPeca(destino);
            Quadro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Quadro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Quadro));
            ColocarNovaPeca('c', 2, new Torre(Cor.Branca, Quadro));
            ColocarNovaPeca('d', 2, new Torre(Cor.Branca, Quadro));
            ColocarNovaPeca('e', 2, new Torre(Cor.Branca, Quadro));
            ColocarNovaPeca('e', 1, new Torre(Cor.Branca, Quadro));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Quadro));

            ColocarNovaPeca('c', 7, new Torre(Cor.Preta, Quadro));
            ColocarNovaPeca('c', 8, new Torre(Cor.Preta, Quadro));
            ColocarNovaPeca('d', 7, new Torre(Cor.Preta, Quadro));
            ColocarNovaPeca('e', 7, new Torre(Cor.Preta, Quadro));
            ColocarNovaPeca('e', 8, new Torre(Cor.Preta, Quadro));
            ColocarNovaPeca('d', 8, new Rei(Cor.Preta, Quadro));
        }
    }
}