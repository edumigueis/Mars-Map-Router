using System;
using System.Collections.Generic;

namespace apCaminhosMarte.Data
{
    class ArvoreBinaria<T> where T : IComparable<T>
    {
        private int qtd = 0;
        public int Qtd { get => qtd; }

        public NoArvore<T> Raiz { get; set; }

        public ArvoreBinaria()
        { }

        public void Incluir(T info)
        {
            if (this.Raiz == null)
            {
                this.Raiz = new NoArvore<T>(info);
                qtd++;
            }
            else
                IncluirRec(Raiz, info);
        }

        private void IncluirRec(NoArvore<T> atual, T info)
        {
            int comp = info.CompareTo(atual.Info);

            if (comp == 0)
                throw new Exception("Item já existente!");

            if (comp < 0)
            {
                if (atual.Esq == null)
                {
                    atual.Esq = new NoArvore<T>(info);
                    qtd++;
                }
                else
                    IncluirRec(atual.Esq, info);
            }
            else
            {
                if (atual.Dir == null)
                {
                    atual.Dir = new NoArvore<T>(info);
                    qtd++;
                }
                else
                    IncluirRec(atual.Dir, info);
            }
        }

        public T Busca(T buscado)
        {
            return Achar(buscado, this.Raiz);
        }

        private T Achar(T buscado, NoArvore<T> atual)
        {
            if (atual == null)
                throw new Exception("Informação inexistente!");

            int comp = buscado.CompareTo(atual.Info);
            if (comp == 0)
                return atual.Info;
            if (comp < 0)
                return Achar(buscado, atual.Esq);
            else
                return Achar(buscado, atual.Dir);
        }

        public List<T> ToList()
        {
            List<T> result = new List<T>();
            Converter(this.Raiz, ref result);
            return result;
        }

        private void Converter(NoArvore<T> atual, ref List<T> result)
        {
            if (atual == null)
                return;

            result.Add(atual.Info);
            Converter(atual.Esq, ref result);
            Converter(atual.Dir, ref result);
        }   
       
    }
}
