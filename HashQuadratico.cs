using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    public class HashQuadratico<Tipo> : ITabelaDeHash<Tipo>
            where Tipo : IRegistro<Tipo>
    {
        private Tipo[] tabela;
        private int tamanho;
        private int quantidade;

        public HashQuadratico(int tamanho)
        {
            this.tamanho = tamanho;
            tabela = new Tipo[tamanho];
            quantidade = 0;
        }

        public List<Tipo> Conteudo()
        {
            List<Tipo> conteudo = new List<Tipo>(quantidade);
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
            int posicao = Hash(item);
            int tentativas = 0;
            while (tabela[posicao] != null && !tabela[posicao].Equals(item) && tentativas < tamanho)
            {
                tentativas++;
                posicao = (posicao + (tentativas * tentativas)) % tamanho;
            }
            onde = posicao;
            return tabela[posicao] != null && tabela[posicao].Equals(item);
        }

        public void Inserir(Tipo item)
        {
            int posicao;
            if (!Existe(item, out posicao))
            {
                if (quantidade == tamanho)
                {
                    throw new InvalidOperationException("Tabela de hash cheia.");
                }
                tabela[posicao] = item;
                quantidade++;
            }
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
            return false;
        }

        private int Hash(Tipo item)
        {
            return item.GetHashCode() % tamanho;
        }
    }
}