using apCaminhosEmMarte;
using System;
using System.Collections;
using System.Collections.Generic;

class BucketHash<Tipo> : ITabelaDeHash<Tipo>
    where Tipo : IRegistro<Tipo>
{
    private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

    List<Tipo>[] dados;

    public BucketHash()
    {
        dados = new List<Tipo>[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            dados[i] = new List<Tipo>(1);
        }
    }

    public int Hash(string chave)
    {
        long tot = 0;
        for (int i = 0; i < chave.Length; i++)
        {
            tot += 37 * tot + (char)chave[i];
        }

        return (int)(tot % SIZE);
    }

    public void Inserir(Tipo item)
    {
        int valorDeHash = Hash(item.Chave);
        if (!dados[valorDeHash].Contains(item))
        {
            dados[valorDeHash].Add(item);
        }
    }

    public bool Remover(Tipo item)
    {
        int onde = 0;
        if (!Existe(item, out onde))
        {
            return false;
        }

        dados[onde].Remove(item);
        return true;
    }

    public Tipo Buscar(string chave)
    {
        int posicao = Hash(chave);
        List<Tipo> lista = dados[posicao];
        for (int i = 0; i < lista.Count; i++)
        {
            Tipo item = lista[i];
            if (item.Chave == chave)
            {
                return item;
            }
        }
        return default(Tipo);
    }

    public bool Existe(Tipo item, out int posicao)
    {
        posicao = Hash(item.Chave);
        return dados[posicao].Contains(item);
    }

    public List<Tipo> Conteudo()
    {
        List<Tipo> saida = new List<Tipo>();
        foreach (var lista in dados)
        {
            if (lista.Count > 0)
            {
                saida.AddRange(lista);
            }
        }
        return saida;
    }
}
