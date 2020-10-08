
namespace apCaminhosMarte.Data
{
    class NoArvore<T>
    {
        public T Info { get; set; }
        public NoArvore<T> Esq { get; set; }
        public NoArvore<T> Dir { get; set; }

        public NoArvore(T info)
        {
            this.Info = info;
            this.Esq = this.Dir = null;
        }
    }
}
