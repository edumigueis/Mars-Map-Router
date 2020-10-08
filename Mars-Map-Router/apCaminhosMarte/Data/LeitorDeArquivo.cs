using System;
using System.IO;

namespace apCaminhosMarte.Data
{
    class LeitorDeArquivo
    {
        private ArvoreBinaria<string> LerArquivoComoArvoreBinaria(string arq)
        {
            if (arq.Equals(""))
                throw new FileNotFoundException("O nome do arquivo não foi fornecido.");
            if (!Path.GetExtension(arq).Equals(".txt"))
                throw new ArgumentOutOfRangeException("O arquivo fornecido não é .txt!");

            StreamReader sr = new StreamReader(arq);

            ArvoreBinaria<string> arvore = new ArvoreBinaria<string>();
            return arvore;
        }


    }
}
