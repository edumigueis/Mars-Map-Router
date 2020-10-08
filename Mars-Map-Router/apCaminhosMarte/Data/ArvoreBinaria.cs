using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte.Data
{
    class ArvoreBinaria<T> where T: IComparable<T>
    {
        protected NoArvore<T> raiz;
        public ArvoreBinaria() { 
        
        }
        public void Incluir(T info)
        {
            if (this.raiz == null)
                this.raiz = new NoArvore<T>(info);
            else
                incluirRec(raiz, info);
        }

        private void incluirRec(NoArvore<T> atual, T info)
        {
            int comp = info.CompareTo(atual.Info);

            if (comp == 0) throw new Exception("Item já existente!");

            if (comp < 0)
            {
                if (atual.Esq == null)
                    atual.Esq = new NoArvore<T>(info);
                else
                    incluirRec(atual.Esq, info);
            }
            else // comp > 0
            {
                if (atual.Dir == null)
                    atual.Dir = new NoArvore<T>(info);
                else
                    incluirRec(atual.Dir, info);
            }
        }

        public T BuscaT(T buscado)
        {
            return achar(buscado, this.raiz);
        }

        private T achar(T buscado, NoArvore<T> atual)
        {
            if (atual == null)
                throw new Exception("T inexistente!");

            int comp = buscado.CompareTo(atual.Info);
            if (comp == 0)
                return atual.Info;
            if (comp < 0)
                return achar(buscado, atual.Esq);
            else
                return achar(buscado, atual.Dir);
        }
    }
}
