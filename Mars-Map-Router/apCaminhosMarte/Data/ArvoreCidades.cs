namespace apCaminhosMarte.Data
{
    class ArvoreCidades : ArvoreBinaria<Cidade>
    {
        public ArvoreCidades()
        {}

        public Cidade BuscarPorId(int id)
        {
            return base.Busca(new Cidade(id, default, default, default));
        }
    }
}
