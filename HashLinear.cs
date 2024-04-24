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

        ArrayList[] dados;

        public HashLinear()
        {
            dados = new ArrayList[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                // coloca em cada posição do vetor, um arrayList vazio
                dados[i] = new ArrayList(1);
            }
        }

        public List<Tipo> Conteudo()
        {
            List<Tipo> conteudo = new List<Tipo>();

            for (int i = 0; i < SIZE; i++)
            {
                if (dados[i].Count > 0)
                {
                    conteudo.AddRange(dados[i].Cast<Tipo>());
                }
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

            if (dados[index].Contains(item))
            {
                onde = index;
                return true;
            }

            onde = -1;
            return false;
        }

        public bool Remover(Tipo item)
        {
            int index = CalcularHash(item);

            if (dados[index].Contains(item))
            {
                dados[index].Remove(item);
                return true;
            }

            return false;
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
