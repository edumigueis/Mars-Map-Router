//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

using System.Collections.Generic;
using System.Linq;

namespace apCaminhosMarte.Data
{
    /// <summary>
    /// Classe estática para encontrar caminhos entre cidades.
    /// </summary>
    static class Solucionador
    {
        /// <summary>
        /// Busca todos os caminhos entre duas cidades.
        /// </summary>
        /// <param name="caminhoEncontrado">Stack vazia para armazenamento de caminho</param>
        /// <param name="resultados">List<AvancoCaminho[]> vazia para receber os resultados</param>
        /// <param name="arvore">ArvoreBinaria de cidades.</param>
        /// <param name="origem">Objeto de Cidade que representa a origem</param>
        /// <param name="destino">Objeto de Cidade que representa o destino final</param>
        /// <param name="matrizCaminhos">Matriz esparssa de caminhos.</param>
        /// <returns>Retorna true se existe caminho e false se não existe</returns>
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

        /// <summary>
        /// Método recursivo para busca de caminhos. Esse método efetua a real busca de caminhos na classe.
        /// </summary>
        /// <param name="atual">Cidade sendo percorrida naquela iteração do método.</param>
        /// <param name="destino">Cidade objetivo como destino.</param>
        /// <param name="matrizCaminhos">Matriz esparssa que relaciona caminhos e cidades.</param>
        /// <param name="caminhoEncontrado">Stack que contém a sucessão de AvancosCaminhos para chegar ao destino final.</param>
        /// <param name="resultados">Conjunto de stacks que possuem todas as relções de caminhos do origem inicial a destino final.</param>
        /// <param name="passou">Vetor boolean que especifica se a iteração do método passou por uma cidade específica (relativo ao id).</param>
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

        /// <summary>
        /// Busca o melhor caminhos dentre os vetores de AvancoCaminho guardados numa List.
        /// </summary>
        /// <param name="caminhos">List<AvancoCaminho[]> com todos os caminhos entre duas cidades.</param>
        /// <returns>Retorna um vetor de AvancoCaminho contendo  melhor caminho(menor distância) entre duas cidades.</returns>
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
