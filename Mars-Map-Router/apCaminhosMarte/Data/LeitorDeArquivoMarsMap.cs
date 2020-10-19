//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace apCaminhosMarte.Data
{
    class LeitorDeArquivoMarsMap
    {
        private int qtdLinhas = 0;

        ArvoreBinaria<Cidade> Arvore { get; set; } //árvore para armazenar os dados lidos

        public LeitorDeArquivoMarsMap()
        {
            Arvore = new ArvoreBinaria<Cidade>();
        }

        /// <summary>
        /// Guarda os dados do arquivo de cidades em uma árvore binária
        /// </summary>
        /// <returns>Uma ArvoreBinaria<Cidade> contendo todas as cidades ordenadas.</returns>
        public ArvoreBinaria<Cidade> LerCidades()
        {
            StreamReader sr = new StreamReader("../../txt/CidadesDes.txt", Encoding.UTF7);

            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                Arvore.Incluir(new Cidade(int.Parse(linha.Substring(0, 3)), 
                                                    linha.Substring(3, 15).Trim(), 
                                          int.Parse(linha.Substring(18, 5)), 
                                          int.Parse(linha.Substring(23, 5))));

                qtdLinhas++;
            }

            return Arvore;
        }

        /// <summary>
        /// Cria uma lista de caminhos disponíveis, com origem, destino e dados de percurso.
        /// </summary>
        /// <returns>Retorna uma lista de AvancoCaminhos contendo dados de caminhos entre cidades.</returns>
        public List<AvancoCaminho> LerCaminhos()
        {
            StreamReader sr = new StreamReader("../../txt/Caminhos.txt", Encoding.UTF7);

            List<AvancoCaminho> lista = new List<AvancoCaminho>();

            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                lista.Add(new AvancoCaminho(Arvore.Busca(new Cidade(int.Parse(linha.Substring(0,3)), default, default, default)), 
                                            Arvore.Busca(new Cidade(int.Parse(linha.Substring(3,3)), default, default, default)), 
                                            new CaminhoEntreCidades(int.Parse(linha.Substring(6, 5)), 
                                                                    int.Parse(linha.Substring(11, 4)), 
                                                                    int.Parse(linha.Substring(15, 5)))));
            }

            return lista;
        }

        /// <summary>
        /// Le o arquivo em uma matriz exparsa que relaciona as origens com o destino.
        /// </summary>
        /// <returns>Retorna uma matriz exparsa com os caminhos indexados de suas origens e destinos.</returns>
        public AvancoCaminho[,] LerCaminhosComoMatriz()
        {
            StreamReader sr = new StreamReader("../../txt/Caminhos.txt", Encoding.UTF7);

            AvancoCaminho[,] lista = new AvancoCaminho[qtdLinhas, qtdLinhas];

            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                lista[int.Parse(linha.Substring(0, 3)), int.Parse(linha.Substring(3, 3))] = new AvancoCaminho(Arvore.Busca(new Cidade(int.Parse(linha.Substring(0, 3)), default, default, default)),
                                                                                                                    Arvore.Busca(new Cidade(int.Parse(linha.Substring(3, 3)), default, default, default)),
                                                                                                                    new CaminhoEntreCidades(int.Parse(linha.Substring(6, 5)),
                                                                                                                                            int.Parse(linha.Substring(11, 4)),
                                                                                                                                            int.Parse(linha.Substring(15, 5))));
            }

            return lista;
        }
    }
}
