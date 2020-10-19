//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

using System;
using System.Collections.Generic;

namespace apCaminhosMarte.Data
{
    class Cidade : IComparable<Cidade>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int X { get; set; } //Posição X da cidade dentre os 4096px da imagem original.
        public int Y { get; set; } //Posição Y da cidade dentre os 2048px da imagem original.

        /// <summary>
        /// Constrói uma instância de Cidade.
        /// </summary>
        /// <param name="id">O id da cidade (int)</param>
        /// <param name="nome">O nome da cidade</param>
        /// <param name="x">A coordenada X da cidade (int)</param>
        /// <param name="y">A coordenada Y da cidade (int)</param>
        public Cidade(int id, string nome, int x, int y)
        {
            this.Id = id;
            this.Nome = nome;
            this.X = x;
            this.Y = y;
        }

        public int CompareTo(Cidade other) //Cidades podem ser comparadas a partir do id
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
