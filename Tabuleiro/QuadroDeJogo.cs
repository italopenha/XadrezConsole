namespace XadrezConsole.Tabuleiro
{
    internal class QuadroDeJogo
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public QuadroDeJogo() { }

        public QuadroDeJogo(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[Linhas, Colunas];
        }
    }
}
