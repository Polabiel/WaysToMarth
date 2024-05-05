using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    /// <summary>
    /// Classe HashDuplo implementa a interface ITabelaDeHash.
    /// Esta classe implementa uma tabela de hash com método de resolução de colisões por hash duplo.
    /// </summary>
    public class HashDuplo<Tipo> : ITabelaDeHash<Tipo>
                where Tipo : IRegistro<Tipo>
    {
        private Tipo[] tabela;
        private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

        /// <summary>
        /// Construtor da classe HashDuplo.
        /// Inicializa a tabela de hash com o tamanho definido pela constante SIZE.
        /// </summary>
        public HashDuplo()
        {
            this.tabela = new Tipo[SIZE];
        }

        /// <summary>
        /// Insere um item na tabela de hash.
        /// Se houver colisão, utiliza o método de hash duplo para encontrar uma nova posição.
        /// </summary>
        public void Inserir(Tipo item)
        {
            int hash = CalcularHash(item.Chave);
            int index = hash % SIZE;

            if (tabela[index] == null)
            {
                tabela[index] = item;
            }
            else
            {
                int step = CalcularStep(item.Chave);
                int newIndex = (index + step) % SIZE;

                while (tabela[newIndex] != null)
                {
                    newIndex = (newIndex + step) % SIZE;
                }

                tabela[newIndex] = item;
            }
        }

        /// <summary>
        /// Remove um item da tabela de hash.
        /// Se o item não for encontrado, retorna false.
        /// </summary>
        public bool Remover(Tipo item)
        {
            int hash = CalcularHash(item.Chave);
            int index = hash % SIZE;

            if (tabela[index] != null && tabela[index].Equals(item))
            {
                tabela[index] = default(Tipo);
                return true;
            }
            else
            {
                int step = CalcularStep(item.Chave);
                int newIndex = (index + step) % SIZE;

                while (tabela[newIndex] != null && !tabela[newIndex].Equals(item))
                {
                    newIndex = (newIndex + step) % SIZE;
                }

                if (tabela[newIndex] != null && tabela[newIndex].Equals(item))
                {
                    tabela[newIndex] = default(Tipo);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica se um item existe na tabela de hash.
        /// Retorna a posição do item na tabela, se encontrado.
        /// </summary>
        public bool Existe(Tipo item, out int onde)
        {
            int hash = CalcularHash(item.Chave);
            int index = hash % SIZE;

            if (tabela[index] != null && tabela[index].Equals(item))
            {
                onde = index;
                return true;
            }
            else
            {
                int step = CalcularStep(item.Chave);
                int newIndex = (index + step) % SIZE;

                while (tabela[newIndex] != null && !tabela[newIndex].Equals(item))
                {
                    newIndex = (newIndex + step) % SIZE;
                }

                if (tabela[newIndex] != null && tabela[newIndex].Equals(item))
                {
                    onde = newIndex;
                    return true;
                }
            }

            onde = -1;
            return false;
        }

        /// <summary>
        /// Retorna o conteúdo da tabela de hash.
        /// </summary>
        public List<Tipo> Conteudo()
        {
            List<Tipo> conteudo = new List<Tipo>();
            for (int i = 0; i < SIZE; i++)
            {
                if (tabela[i] != null)
                {
                    conteudo.Add(tabela[i]);
                }
            }
            return conteudo;
        }

        /// <summary>
        /// Busca um item na tabela de hash pela chave.
        /// Se o item não for encontrado, retorna o valor padrão do tipo.
        /// </summary>
        public Tipo Buscar(string chave)
        {
            int hash = CalcularHash(chave);
            int index = hash % SIZE;

            if (tabela[index] != null && tabela[index].Chave == chave)
            {
                return tabela[index];
            }
            else
            {
                int step = CalcularStep(chave);
                int newIndex = (index + step) % SIZE;

                while (tabela[newIndex] != null && tabela[newIndex].Chave != chave)
                {
                    newIndex = (newIndex + step) % SIZE;
                }

                if (tabela[newIndex] != null && tabela[newIndex].Chave == chave)
                {
                    return tabela[newIndex];
                }
            }

            return default(Tipo);
        }

        /// <summary>
        /// Calcula o valor de hash para uma chave.
        /// </summary>
        private int CalcularHash(string chave)
        {
            int hash = 0;
            for (int i = 0; i < chave.Length; i++)
            {
                hash = (hash * 31 + chave[i]) % SIZE;
            }
            return hash;
        }

        /// <summary>
        /// Calcula o valor do passo para o método de hash duplo.
        /// </summary>
        private int CalcularStep(string chave)
        {
            int step = 1;
            for (int i = 0; i < chave.Length; i++)
            {
                step = (step * 37 + chave[i]) % SIZE;
            }
            return step;
        }
    }
}