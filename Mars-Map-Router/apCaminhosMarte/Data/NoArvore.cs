//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

namespace apCaminhosMarte.Data
{
    class NoArvore<T>
    {
        //Propriedades
        public T Info { get; set; }
        public NoArvore<T> Esq { get; set; }
        public NoArvore<T> Dir { get; set; }

        /// <summary>
        /// Constrói uma instância de NoArvore com valor armazenado. Porém, sem descendentes.
        /// </summary>
        /// <param name="info">Valor a ser armazenado no nó</param>
        public NoArvore(T info)
        {
            this.Info = info;
            this.Esq = this.Dir = null;
        }
    }
}
