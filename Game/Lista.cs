using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Elemento
    {
        public int Num;
        public Elemento Prox;
        public string Cor;
        public Elemento()
        {
            Num = 0;
            Prox = null;
            Cor = "";
        }
    }

    public class Lista
    {
        private Elemento Início;
        private Elemento Fim;
        private int Tamanho;
        public int SCORE;

        public Lista()
        {
            this.InserirFinal();
            for (int i = 0; i < 8; i++)
                this.InserirFinal();
        } // 8 Elementos randômicos

        public void RetirarSeq()
        {
            if (ListaVazia() == false)
            {
                Elemento Aux = this.Início.Prox;
                do
                {
                    if (Aux.Prox.Cor == Aux.Cor && Aux.Prox.Prox.Cor == Aux.Cor && Aux.Prox.Prox.Prox.Cor == Aux.Cor)
                    {
                        SCORE += 3;

                        for (int i = 0; i < 4; i++)
                        {
                            this.RetirarItem(Aux.Num);
                            Aux = Aux.Prox;
                            Tamanho--;
                        }
                    }
                    else if (Aux.Prox.Cor == Aux.Cor && Aux.Prox.Prox.Cor == Aux.Cor)
                    {
                        SCORE += 2;
                        for (int i = 0; i < 3; i++)
                        {
                            this.RetirarItem(Aux.Num);
                            Aux = Aux.Prox;
                            Tamanho--;
                        }
                    }
                    Aux = Aux.Prox;
                } while (Aux.Cor != "White");
            }

        } // Retira sequências de 3 e 4

        public void RetirarItem(int o)
        {
            Elemento aux, anterior;
            anterior = this.Início;
            aux = Início;
            aux = aux.Prox;
            do
            {
                if (aux.Num == o)
                {
                    anterior.Prox = aux.Prox;

                    if (aux == this.Fim)
                    {
                        Fim = anterior;
                    }
                    else if (aux == Início.Prox)
                    {
                        aux = aux.Prox;
                        Início.Prox = aux;
                    }
                    return;
                }
                else
                {
                    anterior = aux;
                    aux = aux.Prox;
                }
            } while (aux.Cor != "White");
        } // Retirar elemento da lista

        public void InserirFinal() //Lista  circular encadeada com cabeça
        {
        
            if (Início == null)
            {
                Elemento Head = new Elemento();
                Head.Cor = "White";
                Head.Num = 0;   
                Início = Head;
                Fim = Head;
                Head.Prox = Fim;
                return;
            }
            else
            {
                Elemento Novo = this.CriaElemento();
                Fim.Prox = Novo;
                Novo.Prox = Início;
                Fim = Novo;
            }
          
        }

        private bool Primo(int o)
        {
            int cont = 0;
            for (int i = 1; i <= o; i++)
            {
                if (o % i == 0 && o != 1)
                {
                    cont++;
                }
                else if (o == 1)
                    return false;
            }
            return cont > 2 ? false : true;
        } //Bolas verdes

        private bool Palindromo(int o)
        {
            string original = o.ToString(), nova = "";
            for (int i = original.Length - 1; i >= 0; i--)
                nova += original[i];
            return nova == original ? true : false;
        } // Bolas roxas

        public void InserirPos(int pos, Elemento x)
        {
            if (ListaVazia() == false)
            {
                Elemento aux = this.Início.Prox, ant = null, novo = x;
                int o = 1;
                if (Tamanho > 20)
                {
                    Console.WriteLine("FIM DO JOGO");
                    this.Tamanho = 0;
                    this.EsvaziarLista();
                    return;

                }
                if (pos != 1) // Inserir em posições do meio e do fim, porém como se trata de uma lista circular tivemos que tratar alguns casos especifícos
                {
                    while (o < pos && aux != null)
                    {
                        ant = aux;
                        aux = aux.Prox;
                        o++;
                    }
                    if (o == pos)
                    {
                        ant.Prox = novo;
                        novo.Prox = aux;
                    }
                }
                else if (pos==1) // Se for inserir na primeira posição
                {
                    Elemento oldtrack = this.Início.Prox;
                    x.Prox = oldtrack;
                    this.Início.Prox = x;
                }
                
            }



        } // Insere o elemento na posição indicada

        public Elemento CriaElemento()
        {
            Elemento Novo = new Elemento();
            Random o = new Random();
            Novo.Num = Tamanho++;

            int x = o.Next(0, 100);
            if (this.Primo(x) == true) // Números primos serão bolas verdes
            {
                Novo.Cor = "GREEN";
            }
            else if (this.Palindromo(x) == true) // Números palindromos serão bolas rochas
            {
                Novo.Cor = "PURPLE";
            }
            else if (x % 2 != 0) // Números impáres serão bolas vermelhas
            {
                Novo.Cor = "RED";
            }
            else if (x % 2 == 0) // Números pares serão bolas azuis
            {
                Novo.Cor = "BLUE";
            }
            return Novo;
        } // Cria o elemento com uma cor
      
        public bool VerificarCorExiste(string cor)
        { 
            for (Elemento Aux = this.Início.Prox; Aux.Cor!="White";)
            {
                if (cor == Aux.Cor)
                    return true;
                Aux = Aux.Prox;
            }
            return false;
        } // Verificar se a cor existe

        public void MostraLista()
        {


            if (Início.Cor == Fim.Cor)
            {
                Console.WriteLine("Você venceu !!! \n\n");
                return;
            }
            else
            {
                Console.WriteLine("Elementos da Lista: {0}\n", Tamanho);
                Elemento Aux = this.Início.Prox;
                int pos = 1;
                while (Aux.Cor!= "White")
                {
                    Console.WriteLine("[{0}] - {1}",pos++,Aux.Cor);
                    Aux = Aux.Prox;
                }

                Console.WriteLine("SCORE: {0}", SCORE);
            }
        } // Mostra elementos

        public void EsvaziarLista()
        {
            if (Início == null)
            {
                Console.WriteLine("A lista não possui nenhum elemento!!! \n\n");
            }
            else
            {
                Início = null;
                Fim = null;
                Tamanho = 0;
            }
        } // Esvaziar a lista

        public bool ListaVazia()
        {
            if (this.Início == null && this.Fim == null)
                return true;
            else
                return false;
           
        } // Verifica se a lista está 
    }
}
