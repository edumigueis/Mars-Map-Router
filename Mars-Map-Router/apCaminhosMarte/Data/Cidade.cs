using System;

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
    }
}
