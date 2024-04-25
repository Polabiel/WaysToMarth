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
        private int tamanho;
        private int quantidade;

        public HashDuplo(int tamanho)
        {
            this.tamanho = tamanho;
            tabela = new Tipo[tamanho];
            quantidade = 0;
        }

        public List<Tipo> Conteudo()
        {
            List<Tipo> conteudo = new List<Tipo>();
            for (int i = 0; i < tamanho; i++)
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
            int tentativas = 0;

            while (tabela[posicao] != null && !tabela[posicao].Equals(item) && tentativas < tamanho)
            {
                posicao = (posicao + incremento) % tamanho;
                tentativas++;
            }

            if (tabela[posicao] != null && tabela[posicao].Equals(item))
            {
                onde = posicao;
                return true;
            }
            else
            {
                onde = -1;
                return false;
            }
        }

        public void Inserir(Tipo item)
        {
            if (quantidade == tamanho)
            {
                throw new InvalidOperationException("Tabela de hash está cheia.");
            }

            int posicao = Hash1(item);
            int incremento = Hash2(item);

            while (tabela[posicao] != null)
            {
                posicao = (posicao + incremento) % tamanho;
            }

            tabela[posicao] = item;
            quantidade++;
        }

        public bool Remover(Tipo item)
        {
            int posicao;
            if (Existe(item, out posicao))
            {
                tabela[posicao] = default(Tipo);
                quantidade--;
                return true;
            }
            else
            {
                return false;
            }
        }

        private int Hash1(Tipo item)
        {
            return item.GetHashCode() % tamanho;
        }

        private int Hash2(Tipo item)
        {
            return 1 + (item.GetHashCode() % (tamanho - 1));
        }
    }
}