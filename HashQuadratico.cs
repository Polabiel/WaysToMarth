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
        private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

        public HashQuadratico()
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