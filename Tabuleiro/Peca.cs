﻿namespace XadrezConsole.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public QuadroDeJogo Quadro { get; protected set; }

        public Peca() { }

        public Peca(Posicao posicao, Cor cor, QuadroDeJogo quadro)
        {
            Posicao = posicao;
            Cor = cor;
            QteMovimentos = 0;
            Quadro = quadro;
        }
    }
}