using System.Collections.Generic;
using System.IO;

namespace apCaminhosMarte.Data
{
    class LeitorDeArquivoMarsMap
    {
        private ArvoreBinaria<Cidade> LerCidades()
        {
            StreamReader sr = new StreamReader("/txt/Cidades.txt");

            ArvoreBinaria<Cidade> arvore = new ArvoreBinaria<Cidade>();

            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                arvore.Incluir(new Cidade(int.Parse(linha.Substring(0, 3)), 
                                                    linha.Substring(3, 15).Trim(), 
                                          int.Parse(linha.Substring(18, 5)), 
                                          int.Parse(linha.Substring(23, 5))));
            }

            return arvore;
        }

        private List<AvancoCaminho> LerCaminhos()
        {
            StreamReader sr = new StreamReader("/txt/Caminhos.txt");

            List<AvancoCaminho> lista = new List<AvancoCaminho>();

            ArvoreCidades arvore = new ArvoreCidades();

            while (!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                lista.Add(new AvancoCaminho(arvore.BuscarPorId(int.Parse(linha.Substring(0,3))), 
                                            arvore.BuscarPorId(int.Parse(linha.Substring(3,3))), 
                                            new CaminhoEntreCidades(int.Parse(linha.Substring(6, 5)), 
                                                                    int.Parse(linha.Substring(11, 4)), 
                                                                    int.Parse(linha.Substring(15, 5)))));
            }

            return lista;
        }
    }
}
