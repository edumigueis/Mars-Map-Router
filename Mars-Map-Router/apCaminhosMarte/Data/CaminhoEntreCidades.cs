//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

namespace apCaminhosMarte.Data
{
    /// <summary>
    /// Representa um caminho entre cidades sem considerar os extremos, ou seja, 
    /// só considera a existência do caminho própriamente dito, com suas propriedades:
    /// distância, tempo e custo.
    /// </summary>
    class CaminhoEntreCidades
    {
        public int Distancia { get; set; }
        public int Tempo { get; set; }
        public int Custo { get; set; }

        /// <summary>
        ///     Constrói uma instância da classe.
        /// </summary>
        /// <param name="distancia">Distância entre duas cidades.</param>
        /// <param name="tempo">Tempo de deslocamento entre os extremos do caminho.</param>
        /// <param name="custo">Custo de percorrer o cainho.</param>
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
