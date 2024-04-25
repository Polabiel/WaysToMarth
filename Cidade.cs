using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    public class Cidade : IRegistro<Cidade>
    {
        // mapeamento da linha de dados do arquivo de cidades
        const int tamNome = 15, tamX = 7, tamY = 7, inicioNome = 0, inicioX = inicioNome + tamNome, inicioY = inicioX + tamX;

        string nomeCidade;

        double x, y;

        public string Chave => nomeCidade;
        public string NomeCidade => nomeCidade;

        public double X => x;

        public double Y => y;

        public void GravarDados(StreamWriter arquivo)
        {
            if (arquivo != null)  // arquivo foi aberto
            {
                string linha = nomeCidade.PadRight(tamNome) + x.ToString().PadRight(tamX) + y.ToString().PadRight(tamY);
                arquivo.WriteLine(linha);
            }
        }


        public Cidade Buscar(string nomeCidade, ITabelaDeHash<Cidade> tabelaDeCidades)
        {
            Cidade cidadeEncontrada = null;

            if (tabelaDeCidades != null)
            {
                cidadeEncontrada = tabelaDeCidades.Buscar(nomeCidade);
            }

            return cidadeEncontrada;
        }


        public void LerRegistro(StreamReader arquivo)
        {
            if (arquivo != null)  // arquivo foi aberto
                if (!arquivo.EndOfStream) // se não acabou de ler
                {
                    string linhaLida = arquivo.ReadLine();

                    // separamos cada campo a partir da linha lida
                    nomeCidade = linhaLida.Substring(inicioNome, tamNome);
                    string strX = linhaLida.Substring(inicioX, tamX);
                    x = double.Parse(strX);
                    y = double.Parse(linhaLida.Substring(inicioY, tamY));
                }
        }
    }
}
