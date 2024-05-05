using apCaminhosEmMarte;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Classe BucketHash implementa a interface ITabelaDeHash.
/// Esta classe implementa uma tabela de hash com método de resolução de colisões por bucket.
/// </summary>
class BucketHash<Tipo> : ITabelaDeHash<Tipo>
  where Tipo : IRegistro<Tipo>
{
    private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

    ArrayList[] dados;

    /// <summary>
    /// Construtor da classe BucketHash.
    /// Inicializa a tabela de hash com o tamanho definido pela constante SIZE.
    /// </summary>
    public BucketHash()
    {
        dados = new ArrayList[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            // coloca em cada posição do vetor, um arrayList vazio
            dados[i] = new ArrayList(1);
        }
    }

    /// <summary>
    /// Calcula o valor de hash para uma chave.
    /// </summary>
    public int Hash(string chave)
    {
        long tot = 0;
        for (int i = 0; i < chave.Length; i++)
            tot += 37 * tot + (char)chave[i];

        tot = tot % dados.Length;
        if (tot < 0)
            tot += dados.Length;
        return (int)tot;
    }

    /// <summary>
    /// Insere um item na tabela de hash.
    /// </summary>
    public void Inserir(Tipo item)
    {
        int valorDeHash = Hash(item.Chave);
        if (!dados[valorDeHash].Contains(item))
            dados[valorDeHash].Add(item);
    }

    /// <summary>
    /// Remove um item da tabela de hash.
    /// </summary>
    public bool Remover(Tipo item)
    {
        int onde = 0;
        if (!Existe(item, out onde))
            return false;

        dados[onde].Remove(item);
        return true;
    }

    /// <summary>
    /// Verifica se um item existe na tabela de hash.
    /// Retorna a posição do item na tabela, se encontrado.
    /// </summary>
    public bool Existe(Tipo item, out int posicao)
    {
        posicao = Hash(item.Chave);
        return dados[posicao].Contains(item);
    }

    /// <summary>
    /// Retorna o conteúdo da tabela de hash.
    /// </summary>
    public List<Tipo> Conteudo()
    {
        List<Tipo> saida = new List<Tipo>();
        for (int i = 0; i < dados.Length; i++)
            if (dados[i].Count > 0)
            {
                string linha = $"{i,5} : ";
                foreach (Tipo item in dados[i])
                    saida.Add(item);
            }
        return saida;
    }

    /// <summary>
    /// Busca um item na tabela de hash pela chave.
    /// Se o item não for encontrado, retorna o valor padrão do tipo.
    /// </summary>
    public Tipo Buscar(string chave)
    {
        int posicao = Hash(chave);
        foreach (Tipo item in dados[posicao])
            if (item.Chave == chave)
                return item;
        return default(Tipo);
    }
}
