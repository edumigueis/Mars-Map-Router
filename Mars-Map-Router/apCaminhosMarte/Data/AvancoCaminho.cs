//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

using System;
using System.Collections.Generic;

namespace apCaminhosMarte.Data
{
    /// <summary>
    /// Representa uma união entre um caminho e seus extremos, portanto, dois objetos de Cidade e um objeto de Caminho.
    /// </summary>
    class AvancoCaminho : ICloneable
    {
        public Cidade Origem { get; set; }
        public Cidade Destino { get; set; }
        public CaminhoEntreCidades Caminho { get; set; }

        public AvancoCaminho(Cidade origem, Cidade destino, CaminhoEntreCidades caminho)
        {
            this.Origem = origem;
            this.Destino = destino;
            this.Caminho = caminho;
        }

        public object Clone()
        {
            AvancoCaminho av = new AvancoCaminho(this.Origem, this.Destino, this.Caminho);
            return av;
        }

        public override bool Equals(object obj)
        {
            return obj is AvancoCaminho caminho &&
                   EqualityComparer<Cidade>.Default.Equals(Origem, caminho.Origem) &&
                   EqualityComparer<Cidade>.Default.Equals(Destino, caminho.Destino) &&
                   EqualityComparer<CaminhoEntreCidades>.Default.Equals(Caminho, caminho.Caminho);
        }

        public override int GetHashCode()
        {
            int hashCode = 2106482187;
            hashCode = hashCode * -1521134295 + EqualityComparer<Cidade>.Default.GetHashCode(Origem);
            hashCode = hashCode * -1521134295 + EqualityComparer<Cidade>.Default.GetHashCode(Destino);
            hashCode = hashCode * -1521134295 + EqualityComparer<CaminhoEntreCidades>.Default.GetHashCode(Caminho);
            return hashCode;
        }
    }
}
