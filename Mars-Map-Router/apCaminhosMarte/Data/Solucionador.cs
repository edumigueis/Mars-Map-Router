using System.Collections.Generic;
using System.Linq;

namespace apCaminhosMarte.Data
{
    static class Solucionador
    {
        static public bool BuscarCaminhos(ref Stack<AvancoCaminho> caminhoEncontrado, ref List<AvancoCaminho[]> resultados, ref ArvoreBinaria<Cidade> arvore, ref Cidade origem, ref Cidade destino, ref bool[] passou, ref AvancoCaminho[,] matrizCaminhos)
        {
            caminhoEncontrado = new Stack<AvancoCaminho>();
            resultados = new List<AvancoCaminho[]>();
            passou = new bool[arvore.Qtd];

            BuscarCaminhosRec(origem, ref destino, ref matrizCaminhos, ref caminhoEncontrado, ref resultados, ref arvore, ref passou);

            if (resultados.Count <= 0)
                return false;

            return true;
        }

        static private void BuscarCaminhosRec(Cidade atual, ref Cidade destino, ref AvancoCaminho[,] matrizCaminhos, ref Stack<AvancoCaminho> caminhoEncontrado, ref List<AvancoCaminho[]> resultados, ref ArvoreBinaria<Cidade> arvore, ref bool[] passou)
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
                        BuscarCaminhosRec(ac.Destino, ref destino, ref matrizCaminhos, ref caminhoEncontrado, ref resultados, ref arvore, ref passou);
                    }
                }
            }
        }
    }
}
