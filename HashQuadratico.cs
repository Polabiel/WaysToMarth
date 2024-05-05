using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    /// <summary>
    /// Classe HashQuadratico implementa a interface ITabelaDeHash.
    /// Esta classe implementa uma tabela de hash com método de resolução de colisões por hash quadrático.
    /// </summary>
    public class HashQuadratico<Tipo> : ITabelaDeHash<Tipo>
                where Tipo : IRegistro<Tipo>
    {
        private Tipo[] tabela;
        private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

        /// <summary>
        /// Construtor da classe HashQuadratico.
        /// Inicializa a tabela de hash com o tamanho definido pela constante SIZE.
        /// </summary>
        public HashQuadratico()
        {
            this.tabela = new Tipo[SIZE];
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
            int posicao = Hash(chave);
            int tentativas = 0;
            while (tabela[posicao] != null && tabela[posicao].Chave != chave && tentativas < SIZE)
            {
                tentativas++;
                posicao = (posicao + (tentativas * tentativas)) % SIZE;
            }
            return tabela[posicao];
        }

        /// <summary>
        /// Verifica se um item existe na tabela de hash.
        /// Retorna a posição do item na tabela, se encontrado.
        /// </summary>
        public bool Existe(Tipo item, out int onde)
        {
            int posicao = Hash(item.Chave);
            int tentativas = 0;
            while (tabela[posicao] != null && !tabela[posicao].Equals(item) && tentativas < SIZE)
            {
                tentativas++;
                posicao = (posicao + (tentativas * tentativas)) % SIZE;
            }
            onde = posicao;
            return tabela[posicao] != null && tabela[posicao].Equals(item);
        }

        /// <summary>
        /// Insere um item na tabela de hash.
        /// Se houver colisão, utiliza o método de hash quadrático para encontrar uma nova posição.
        /// </summary>
        public void Inserir(Tipo item)
        {
            int posicao;
            if (Existe(item, out posicao))
            {
                int tentativas = 0;
                while (tabela[posicao] != null && tentativas < SIZE)
                {
                    tentativas++;
                    posicao = (posicao + (tentativas * tentativas)) % SIZE;
                }
                tabela[posicao] = item;
            }
        }

        /// <summary>
        /// Remove um item da tabela de hash.
        /// Se o item não for encontrado, retorna false.
        /// </summary>
        public bool Remover(Tipo item)
        {
            int posicao;
            if (Existe(item, out posicao))
            {
                tabela[posicao] = default(Tipo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calcula o valor de hash para uma chave.
        /// </summary>
        private int Hash(string chave)
        {
            long tot = 0;
            for (int i = 0; i < chave.Length; i++)
                tot += 37 * tot + (char)chave[i];

            tot = tot % SIZE;
            if (tot < 0)
                tot += SIZE;
            return (int)tot;
        }
    }
}