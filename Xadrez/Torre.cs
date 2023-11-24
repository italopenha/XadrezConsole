using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, QuadroDeJogo quadro) : base(cor, quadro)
        {

        }

        public override string ToString()
        {
            return "T";
        }
    }
}
