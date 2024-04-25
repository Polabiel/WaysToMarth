using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    public class HashDuplo<Tipo> : ITabelaDeHash<Tipo>
            where Tipo : IRegistro<Tipo>
    {
        private Tipo[] tabela;
        private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

        public HashDuplo()
        {
            this.tabela = new Tipo[SIZE];
        }

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

        public bool Existe(Tipo item, out int onde)
        {
            int posicao = Hash1(item);
            int incremento = Hash2(item);

            for (int tentativas = 0; tentativas < SIZE; tentativas++)
            {
                int index = (posicao + (tentativas * incremento)) % SIZE;
                if (tabela[index] != null && tabela[index].Equals(item))
                {
                    onde = index;
                    return true;
                }
            }

            onde = -1;
            return false;
        }

        public void Inserir(Tipo item)
        {
            int posicao = Hash1(item);
            int incremento = Hash2(item);

            for (int tentativas = 0; tentativas < SIZE; tentativas++)
            {
                int index = (posicao + (tentativas * incremento)) % SIZE;
                if (tabela[index] == null)
                {
                    tabela[index] = item;
                    return;
                }
            }

            throw new Exception("Tabela de hash cheia.");
        }

        public bool Remover(Tipo item)
        {
            int posicao = Hash1(item);
            int incremento = Hash1(item);

            for (int tentativas = 0; tentativas < SIZE; tentativas++)
            {
                int index = (posicao + (tentativas * incremento)) % SIZE;
                if (tabela[index] != null && tabela[index].Equals(item))
                {
                    tabela[index] = default(Tipo);
                    return true;
                }
            }

            return false;
        }

        public Tipo Buscar(string chave)
        {
            for (int i = 0; i < SIZE; i++)
            {
                if (tabela[i] != null && tabela[i].Chave == chave)
                {
                    return tabela[i];
                }
            }
            return default(Tipo);
        }

        private int Hash1(Tipo item)
        {
            return item.GetHashCode() % SIZE;
        }

        private int Hash2(Tipo item)
        {
            return (item.GetHashCode() % (SIZE - 1)) + 1;
        }
    }
}