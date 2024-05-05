using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
    /// <summary>
    /// Interface ITabelaDeHash para tipo genérico.
    /// Esta interface define métodos para inserção, remoção, verificação de existência, obtenção de conteúdo e busca de um item em uma tabela de hash.
    /// </summary>
    public interface ITabelaDeHash<Tipo>
        where Tipo : IRegistro<Tipo>
    {
        /// <summary>
        /// Insere um item na tabela de hash.
        /// </summary>
        /// <param name="item">O item a ser inserido.</param>
        void Inserir(Tipo item);

        /// <summary>
        /// Remove um item da tabela de hash.
        /// </summary>
        /// <param name="item">O item a ser removido.</param>
        /// <returns>Retorna verdadeiro se o item foi removido com sucesso, falso caso contrário.</returns>
        bool Remover(Tipo item);

        /// <summary>
        /// Verifica se um item existe na tabela de hash.
        /// </summary>
        /// <param name="item">O item a ser verificado.</param>
        /// <param name="onde">A posição do item na tabela, se encontrado.</param>
        /// <returns>Retorna verdadeiro se o item existe na tabela, falso caso contrário.</returns>
        bool Existe(Tipo item, out int onde);

        /// <summary>
        /// Obtém o conteúdo da tabela de hash.
        /// </summary>
        /// <returns>Retorna uma lista com o conteúdo da tabela de hash.</returns>
        List<Tipo> Conteudo();

        /// <summary>
        /// Busca um item na tabela de hash pela chave.
        /// </summary>
        /// <param name="chave">A chave do item a ser buscado.</param>
        /// <returns>Retorna o item se encontrado, o valor padrão do tipo caso contrário.</returns>
        Tipo Buscar(string chave);
    }
}
