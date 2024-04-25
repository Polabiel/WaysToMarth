using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    public class HashLinear<Tipo> : ITabelaDeHash<Tipo>
            where Tipo : IRegistro<Tipo>
    {
        private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

        List<Tipo>[] dados;

        public HashLinear()
        {
            dados = new List<Tipo>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                // coloca em cada posição do vetor, uma lista vazia
                dados[i] = new List<Tipo>();
            }
        }

        public List<Tipo> Conteudo()
        {
            List<Tipo> conteudo = new List<Tipo>();

            for (int i = 0; i < SIZE; i++)
            {
                conteudo.AddRange(dados[i]);
            }

            return conteudo;
        }

        public void Inserir(Tipo item)
        {
            int index = CalcularHash(item);
            dados[index].Add(item);
        }

        public bool Existe(Tipo item, out int onde)
        {
            int index = CalcularHash(item);

            onde = dados[index].IndexOf(item);
            return onde != -1;
        }

        public bool Remover(Tipo item)
        {
            int index = CalcularHash(item);

            return dados[index].Remove(item);
        }

        private int CalcularHash(Tipo item)
        {
            int hash = item.GetHashCode() % SIZE;
            int index = hash;

            while (dados[index].Count > 0)
            {
                index = (index + 1) % SIZE;

                if (index == hash)
                {
                    throw new Exception("Tabela de hash cheia");
                }
            }

            return index;
        }
    }
}
