using System;
using System.Collections.Generic;
using Xadrez.Tab;
using Xadrez.Xadrez;

namespace Xadrez
{
    public class Tela
    {
        public static void ImprimirPartida(Partida partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                Console.WriteLine("Jogador atual: " + partida.CorAtual);

                if (partida.Xeque)
                {
                    Console.WriteLine("Xeque!");
                }
            }
            else
            {
                Console.WriteLine("Xequemate!");
                Console.WriteLine("Vencedor: " + partida.CorAtual);
            }
        }

        public static void ImprimirPecasCapturadas(Partida partida)
        {
            Console.WriteLine("Pecas Capturadas ");
            Console.WriteLine();
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branco));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                if (conjunto.Count > 1)
                {
                    Console.Write("" + peca + ", ");
                }
                else
                {
                    Console.Write(peca);
                }
            }

            Console.Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int l = 0; l < tabuleiro.Linha; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tabuleiro.Coluna; c++)
                {
                    ImprimirPeca(tabuleiro.Peca(l, c));
                }

                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int l = 0; l < tabuleiro.Linha; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tabuleiro.Coluna; c++)
                {
                    if (posicoesPossiveis[l, c])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    ImprimirPeca(tabuleiro.Peca(l, c));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;

        }

        public static PosicaoXadrez LerPosicao()
        {
            string posicaoDigitada = Console.ReadLine();

            if (string.IsNullOrEmpty((posicaoDigitada)))
            {
                throw new TabuleiroExeption("Digite algum valor");
            }

            posicaoDigitada = posicaoDigitada.Trim();

            if (posicaoDigitada.Length != 2)
            {
                throw new TabuleiroExeption("Posição incorreta! Digite no formato letra + número (ex: A1).");
            }

            char coluna = char.ToUpper(posicaoDigitada[0]);

            if (coluna < 'A' || coluna > 'H' || !int.TryParse(posicaoDigitada[1].ToString(), out int linha) || linha < 1 || linha > 8)
            {
                throw new TabuleiroExeption("Posição incorreta! Digite no formato letra + número (ex: A1).");
            }

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }

        public static string ImprimirPromocao()
        {
            Console.WriteLine();
            Console.WriteLine("Peça promovida!");
            Console.WriteLine();
            Console.WriteLine("Digite uma das opções");
            Console.WriteLine();
            Console.WriteLine("D - Dama");
            Console.WriteLine("T - Torre");
            Console.WriteLine("B - Bispo");
            Console.WriteLine("C - Cavalo");

            string posicaoDigitada = Console.ReadLine();

            if (string.IsNullOrEmpty((posicaoDigitada)))
            {
                throw new TabuleiroExeption("Digite algum valor");
            }

            string digitado = posicaoDigitada.ToUpper();

            return digitado;
        }
    }
}
