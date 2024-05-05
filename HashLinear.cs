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

        private List<Tipo>[] dados;
        private int[] hashCodes;

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

        public List<Tipo> Conteudo()
        {
            List<Tipo> conteudo = new List<Tipo>();

            foreach (var lista in dados)
            {
                conteudo.AddRange(lista);
            }

            return conteudo;
        }

        public void Inserir(Tipo item)
        {
            int index = Hash(item);
            if (hashCodes[index] == -1)
            {
                hashCodes[index] = item.GetHashCode();
            }
            dados[index].Add(item);
        }

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

        public bool Remover(Tipo item)
        {
            int index = Hash(item);

            return dados[index].Remove(item);
        }
        public int Hash(Tipo item)
        {
            return Math.Abs(item.GetHashCode()) % SIZE;
        }

        public int Hash(string chave)
        {
            return Math.Abs(chave.GetHashCode()) % SIZE;
        }

        
    }
}
