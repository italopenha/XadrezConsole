using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, QuadroDeJogo quadro) : base(cor, quadro)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
