using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Interface IRegistro para tipo genérico.
/// Esta interface define métodos para leitura e gravação de dados de/para um fluxo, e uma propriedade para a chave do registro.
/// </summary>
public interface IRegistro<Tipo>
{
    /// <summary>
    /// Lê um registro do StreamReader especificado.
    /// </summary>
    /// <param name="arquivo">O StreamReader de onde ler.</param>
    void LerRegistro(StreamReader arquivo);

    /// <summary>
    /// Grava o registro no StreamWriter especificado.
    /// </summary>
    /// <param name="arquivo">O StreamWriter onde gravar.</param>
    void GravarDados(StreamWriter arquivo);

    /// <summary>
    /// Obtém a chave do registro.
    /// </summary>
    string Chave { get; }
}

