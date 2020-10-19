//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

namespace apCaminhosMarte.Data
{
    class CaminhoEntreCidades
    {
        public int Distancia { get; set; }
        public int Tempo { get; set; }
        public int Custo { get; set; }

        public CaminhoEntreCidades(int distancia, int tempo, int custo)
        {
            this.Distancia = distancia;
            this.Tempo = tempo;
            this.Custo = custo;
        }

        public override bool Equals(object obj)
        {
            return obj is CaminhoEntreCidades cidades &&
                   Distancia == cidades.Distancia &&
                   Tempo == cidades.Tempo &&
                   Custo == cidades.Custo;
        }

        public override int GetHashCode()
        {
            int hashCode = 522326332;
            hashCode = hashCode * -1521134295 + Distancia.GetHashCode();
            hashCode = hashCode * -1521134295 + Tempo.GetHashCode();
            hashCode = hashCode * -1521134295 + Custo.GetHashCode();
            return hashCode;
        }
    }
}
