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

        ArvoreBinaria<Cidade> Arvore { get; set; }

        public LeitorDeArquivoMarsMap()
        {
            Arvore = new ArvoreBinaria<Cidade>();
        }

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
