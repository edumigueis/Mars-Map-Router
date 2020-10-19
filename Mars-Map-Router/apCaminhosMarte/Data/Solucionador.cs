//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

using System.Collections.Generic;
using System.Linq;

namespace apCaminhosMarte.Data
{
    static class Solucionador
    {
        static public bool BuscarCaminhos(ref Stack<AvancoCaminho> caminhoEncontrado, ref List<AvancoCaminho[]> resultados, ArvoreBinaria<Cidade> arvore, Cidade origem, Cidade destino, ref AvancoCaminho[,] matrizCaminhos)
        {
            caminhoEncontrado = new Stack<AvancoCaminho>();
            resultados = new List<AvancoCaminho[]>();
            var passou = new bool[arvore.Qtd];

            BuscarCaminhosRec(origem, ref destino, ref matrizCaminhos, ref caminhoEncontrado, ref resultados, ref passou);

            if (resultados.Count <= 0)
                return false;

            return true;
        }

        static private void BuscarCaminhosRec(Cidade atual, ref Cidade destino, ref AvancoCaminho[,] matrizCaminhos, ref Stack<AvancoCaminho> caminhoEncontrado, ref List<AvancoCaminho[]> resultados, ref bool[] passou)
        {
            for (int j = 0; j < matrizCaminhos.GetLength(1); j++)
            {
                AvancoCaminho ac = matrizCaminhos[atual.Id, j];

                if (ac != null && !passou[j])
                {
                    passou[atual.Id] = true;
                    caminhoEncontrado.Push(ac);

                    if (j == destino.Id)
                    {
                        var caminho = new AvancoCaminho[caminhoEncontrado.Count];
                        caminhoEncontrado.CopyTo(caminho, 0);

                        resultados.Add(caminho);
                        caminhoEncontrado.Pop();
                        passou[atual.Id] = false;
                    }
                    else
                    {
                        BuscarCaminhosRec(ac.Destino, ref destino, ref matrizCaminhos, ref caminhoEncontrado, ref resultados, ref passou);
                    }
                }
            }

            if (caminhoEncontrado.Count != 0)
            {
                caminhoEncontrado.Pop();
                passou[atual.Id] = false;
            }
        }

        static public AvancoCaminho[] BuscarMelhorCaminho(List<AvancoCaminho[]> caminhos)
        {
            var distancias = new List<int>();

            for (int i = 0; i < caminhos.Count; i++)
            {
                int distancia = 0;

                for (int j = 0; j < caminhos[i].Length; j++)
                {
                    distancia += caminhos[i][j].Caminho.Distancia;                    
                }

                distancias.Add(distancia);
            }

            return caminhos[distancias.IndexOf(distancias.Min())];
        }
    }
}
