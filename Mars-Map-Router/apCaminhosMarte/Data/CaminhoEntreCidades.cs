using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte.Data
{
    class CaminhoEntreCidades
    {
        int dist, tmp, custo;

        public CaminhoEntreCidades(int distancia, int tempo, int custo)
        {
            this.dist = distancia;
            this.tmp = tempo;
            this.custo = custo;
        }

        public int Distancia { get => dist; set => dist = value; }
        public int Tempo { get => tmp; set => tmp = value; }
        public int Custo { get => custo; set => custo = value; }
    }
}
