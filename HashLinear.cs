using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    /// <summary>
    /// Classe HashLinear implementa a interface ITabelaDeHash.
    /// Esta classe implementa uma tabela de hash com método de resolução de colisões por hash linear.
    /// </summary>
    public class HashLinear<Tipo> : ITabelaDeHash<Tipo>
            where Tipo : IRegistro<Tipo>
    {
        private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

        private List<Tipo>[] dados;
        private int[] hashCodes;

        /// <summary>
        /// Construtor da classe HashLinear.
        /// Inicializa a tabela de hash com o tamanho definido pela constante SIZE.
        /// </summary>
        public HashLinear()
        {
            dados = new List<Tipo>[SIZE];
            hashCodes = new int[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dados[i] = new List<Tipo>();
                hashCodes[i] = -1;
            }
        }

        /// <summary>
        /// Retorna o conteúdo da tabela de hash.
        /// </summary>
        public List<Tipo> Conteudo()
        {
            List<Tipo> conteudo = new List<Tipo>();

            foreach (var lista in dados)
            {
                conteudo.AddRange(lista);
            }

            return conteudo;
        }

        /// <summary>
        /// Insere um item na tabela de hash.
        /// Se houver colisão, utiliza o método de hash linear para encontrar uma nova posição.
        /// </summary>
        public void Inserir(Tipo item)
        {
            int index = Hash(item);
            if (hashCodes[index] == -1)
            {
                hashCodes[index] = item.GetHashCode();
            }
            dados[index].Add(item);
        }

        /// <summary>
        /// Verifica se um item existe na tabela de hash.
        /// Retorna a posição do item na tabela, se encontrado.
        /// </summary>
        public bool Existe(Tipo item, out int onde)
        {
            int index = Hash(item);

            if (hashCodes[index] == item.GetHashCode())
            {
                onde = dados[index].IndexOf(item);
                return onde != -1;
            }

            onde = -1;
            return false;
        }

        /// <summary>
        /// Busca um item na tabela de hash pela chave.
        /// Se o item não for encontrado, retorna o valor padrão do tipo.
        /// </summary>
        public Tipo Buscar(string chave)
        {
            int posicao = Hash(chave);
            foreach (Tipo item in dados[posicao])
            {
                if (item.Chave == chave)
                {
                    return item;
                }
            }
            return default(Tipo);
        }

        /// <summary>
        /// Remove um item da tabela de hash.
        /// Se o item não for encontrado, retorna false.
        /// </summary>
        public bool Remover(Tipo item)
        {
            int index = Hash(item);

            return dados[index].Remove(item);
        }

        /// <summary>
        /// Calcula o valor de hash para um item.
        /// </summary>
        public int Hash(Tipo item)
        {
            return Math.Abs(item.GetHashCode()) % SIZE;
        }

        /// <summary>
        /// Calcula o valor de hash para uma chave.
        /// </summary>
        public int Hash(string chave)
        {
            return Math.Abs(chave.GetHashCode()) % SIZE;
        }
    }
}
