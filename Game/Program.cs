using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Lista Game = new Lista();
            Elemento jogada;
            do
            {
                Console.Clear();
                Game.MostraLista();
                jogada = Game.CriaElemento();
                Console.WriteLine("\nEscolha o indice onde ira jogar a cor {0}",jogada.Cor);
                Game.InserirPos(int.Parse(Console.ReadLine()), jogada);
                Console.WriteLine();
                if (Game.ListaVazia()==false)
                {
                    Game.RetirarSeq();
                }
            
                Game.MostraLista();
      
            } while (Game.ListaVazia()!=true);
        }
    }
}
