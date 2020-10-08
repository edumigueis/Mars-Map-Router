using System;
using System.Collections.Generic;

namespace apCaminhosMarte.Data
{
    class Cidade : IComparable<Cidade>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Cidade(int id, string nome, int x, int y)
        {
            this.Id = id;
            this.Nome = nome;
            this.X = x;
            this.Y = y;
        }

        public int CompareTo(Cidade other)
        {
            return this.Id.CompareTo(other.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is Cidade cidade &&
                   Id == cidade.Id &&
                   Nome == cidade.Nome &&
                   X == cidade.X &&
                   Y == cidade.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = -84828061;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
