using System;
using System.Collections.Generic;

namespace apCaminhosMarte.Data
{
    class ArvoreBinaria<T> where T : IComparable<T>
    {
        protected NoArvore<T> raiz;

        public ArvoreBinaria()
        { }

        public void Incluir(T info)
        {
            if (this.raiz == null)
                this.raiz = new NoArvore<T>(info);
            else
                IncluirRec(raiz, info);
        }

        private void IncluirRec(NoArvore<T> atual, T info)
        {
            int comp = info.CompareTo(atual.Info);

            if (comp == 0)
                throw new Exception("Item já existente!");

            if (comp < 0)
            {
                if (atual.Esq == null)
                    atual.Esq = new NoArvore<T>(info);
                else
                    IncluirRec(atual.Esq, info);
            }
            else
            {
                if (atual.Dir == null)
                    atual.Dir = new NoArvore<T>(info);
                else
                    IncluirRec(atual.Dir, info);
            }
        }

        public T Busca(T buscado)
        {
            return Achar(buscado, this.raiz);
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
            Converter(this.raiz, ref result);
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
