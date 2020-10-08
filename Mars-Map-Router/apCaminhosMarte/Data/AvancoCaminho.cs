using System;

namespace apCaminhosMarte.Data
{
    class AvancoCaminho : ICloneable
    {
        Cidade origem, destino;
        CaminhoEntreCidades caminho;

        public AvancoCaminho(Cidade origem, Cidade destino, CaminhoEntreCidades cm)
        {
            this.origem = origem;
            this.destino = destino;
            this.caminho = cm;
        }

        public Cidade Origem { get => origem; set => origem = value; }
        public Cidade Destino { get => destino; set => destino = value; }
        public CaminhoEntreCidades Caminho { get => caminho; set => caminho = value; }

        public object Clone()
        {
            AvancoCaminho av = new AvancoCaminho(this.origem, this.destino, this.caminho);
            return av;
        }
    }
}
