using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace apCaminhosMarte.Data
{
    class LeitorDeArquivoMarsMap
    {
        ArvoreBinaria<Cidade> Arvore { get; set; }

        public LeitorDeArquivoMarsMap()
        {
            Arvore = new ArvoreBinaria<Cidade>();
        }

        public ArvoreBinaria<Cidade> LerCidades()
        {
            StreamReader sr = new StreamReader("../../txt/Cidades.txt");

            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                Arvore.Incluir(new Cidade(int.Parse(linha.Substring(0, 3)), 
                                                    linha.Substring(3, 15).Trim(), 
                                          int.Parse(linha.Substring(18, 5)), 
                                          int.Parse(linha.Substring(23, 5))));
            }

            return Arvore;
        }

        public List<AvancoCaminho> LerCaminhos()
        {
            StreamReader sr = new StreamReader("../../txt/Caminhos.txt");

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
    }
}
