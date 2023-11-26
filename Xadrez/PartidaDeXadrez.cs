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
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Quadro = new QuadroDeJogo(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Quadro.RetirarPeca(origem);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Quadro.RetirarPeca(destino);
            Quadro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Quadro.RetirarPeca(origemT);
                T.IncrementarMovimentos();
                Quadro.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Quadro.RetirarPeca(origemT);
                T.IncrementarMovimentos();
                Quadro.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    else
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);

                    pecaCapturada = Quadro.RetirarPeca(posP);
                    Capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Quadro.RetirarPeca(destino);
            p.DecrementarMovimentos();
            if (pecaCapturada != null)
            {
                Quadro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Quadro.ColocarPeca(p, origem);

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Quadro.RetirarPeca(destinoT);
                T.DecrementarMovimentos();
                Quadro.ColocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Quadro.RetirarPeca(destinoT);
                T.DecrementarMovimentos();
                Quadro.ColocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Quadro.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                        posP = new Posicao(3, destino.Coluna);
                    else
                        posP = new Posicao(4, destino.Coluna);

                    Quadro.ColocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = Quadro.Peca(destino);

            // #jogadaespecial promoção
            if (p is Peao)
            {
                if (p.Cor == Cor.Branca && destino.Linha == 0 || p.Cor == Cor.Preta && destino.Linha == 7)
                {
                    p = Quadro.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(p.Cor, Quadro);
                    Quadro.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (TesteXequeMate(Adversaria(JogadorAtual)))
                Terminada = true;
            else
            {
                Turno++;
                MudaJogador();
            }

            // #jogadaespecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                VulneravelEnPassant = p;
            else
                VulneravelEnPassant = null;
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
            if (!Quadro.Peca(origem).MovimentoPossivel(destino))
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
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
                throw new TabuleiroException($"Não há rei da cor {cor} no tabuleiro!");

            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
                return false;

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Quadro.Linhas; i++)
                {
                    for (int j = 0; j < Quadro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Quadro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Quadro));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Quadro));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Quadro));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Quadro));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Quadro, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Quadro));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Quadro));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Quadro));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Quadro, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Quadro, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Quadro, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Quadro, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Quadro, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Quadro, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Quadro, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Quadro, this));

            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Quadro));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Quadro));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Quadro));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Quadro));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Quadro, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Quadro));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Quadro));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Quadro));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Quadro, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Quadro, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Quadro, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Quadro, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Quadro, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Quadro, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Quadro, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, Quadro, this));
        }
    }
}