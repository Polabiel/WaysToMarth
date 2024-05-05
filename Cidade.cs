using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    /// <summary>
    /// Classe Cidade implementa a interface IRegistro.
    /// Esta classe representa uma cidade em Marte.
    /// </summary>
    public class Cidade : IRegistro<Cidade>
    {
        // mapeamento da linha de dados do arquivo de cidades
        const int tamNome = 15, tamX = 7, tamY = 7, inicioNome = 0, inicioX = inicioNome + tamNome, inicioY = inicioX + tamX;

        string nomeCidade;
        double x, y;

        /// <summary>
        /// Obtém a chave do registro.
        /// </summary>
        public string Chave => nomeCidade;

        /// <summary>
        /// Obtém o nome da cidade.
        /// </summary>
        public string NomeCidade => nomeCidade;

        /// <summary>
        /// Obtém a coordenada X da cidade.
        /// </summary>
        public double X => x;

        /// <summary>
        /// Obtém a coordenada Y da cidade.
        /// </summary>
        public double Y => y;

        /// <summary>
        /// Grava os dados da cidade no StreamWriter especificado.
        /// </summary>
        /// <param name="arquivo">O StreamWriter onde gravar.</param>
        public void GravarDados(StreamWriter arquivo)
        {
            if (arquivo != null)  // arquivo foi aberto
            {
                string linha = nomeCidade.PadRight(tamNome) + x.ToString().PadRight(tamX) + y.ToString().PadRight(tamY);
                arquivo.WriteLine(linha);
            }
        }

        /// <summary>
        /// Construtor da classe Cidade.
        /// </summary>
        /// <param name="nomeCidade">O nome da cidade.</param>
        /// <param name="x">A coordenada X da cidade.</param>
        /// <param name="y">A coordenada Y da cidade.</param>
        public Cidade(string nomeCidade, double x, double y)
        {
            this.nomeCidade = nomeCidade;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Construtor padrão da classe Cidade.
        /// </summary>
        public Cidade()
        {

        }

        /// <summary>
        /// Busca uma cidade na tabela de cidades pela chave.
        /// </summary>
        /// <param name="nomeCidade">A chave da cidade a ser buscada.</param>
        /// <param name="tabelaDeCidades">A tabela de cidades onde buscar.</param>
        /// <returns>Retorna a cidade se encontrada, null caso contrário.</returns>
        public Cidade Buscar(string nomeCidade, ITabelaDeHash<Cidade> tabelaDeCidades)
        {
            Cidade cidadeEncontrada = null;

            if (tabelaDeCidades != null)
            {
                cidadeEncontrada = tabelaDeCidades.Buscar(nomeCidade);
            }

            return cidadeEncontrada;
        }

        /// <summary>
        /// Lê um registro da cidade do StreamReader especificado.
        /// </summary>
        /// <param name="arquivo">O StreamReader de onde ler.</param>
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

        /// <summary>
        /// Retorna uma representação em string da cidade.
        /// </summary>
        /// <returns>Retorna uma string que representa a cidade.</returns>
        public override string ToString()
        {
            return $"{nomeCidade} ({x},{y})";
        }

        /// <summary>
        /// Determina se o objeto especificado é igual ao objeto atual.
        /// </summary>
        /// <param name="obj">O objeto a comparar com o objeto atual.</param>
        /// <returns>Retorna verdadeiro se o objeto especificado é igual ao objeto atual, falso caso contrário.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Cidade other = (Cidade)obj;

            return nomeCidade == other.nomeCidade && x == other.x && y == other.y;
        }

        /// <summary>
        /// Retorna o código hash para este objeto.
        /// </summary>
        /// <returns>Retorna o código hash para este objeto.</returns>
        public override int GetHashCode()
        {
            return nomeCidade.GetHashCode() ^ x.GetHashCode() ^ y.GetHashCode();
        }
    }
}
