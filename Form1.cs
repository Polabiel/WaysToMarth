using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosEmMarte
{
  public partial class FrmCaminhos : Form
  {
    public FrmCaminhos()
    {
      InitializeComponent();
    }

    private ITabelaDeHash<Cidade> tabela;

    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {

    }

    private void btnLerArquivo_Click(object sender, EventArgs e)
    {
        if (dlgAbrir.ShowDialog() != DialogResult.OK)
            return;

        if (rbBucketHash.Checked)
            tabela = new BucketHash<Cidade>();
        else if (rbHashLinear.Checked)
            tabela = new HashLinear<Cidade>();
        else if (rbHashQuadratico.Checked)
            tabela = new HashQuadratico<Cidade>();
        else if (rbHashDuplo.Checked)
            tabela = new HashDuplo<Cidade>();

        try
        {
            using (var arquivo = new StreamReader(dlgAbrir.FileName))
            {
                while (!arquivo.EndOfStream)
                    {
                        Cidade umaCidade = new Cidade();
                        umaCidade.LerRegistro(arquivo);
                        tabela.Inserir(umaCidade);
                        lsbCidades.Items.Add(new ListViewItem(new string[] { umaCidade.NomeCidade, umaCidade.X.ToString(), umaCidade.Y.ToString() }));
                    }
                }
            }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao ler o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }

        private void txtCidade_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender; // Converte o objeto sender para o tipo TextBox
            string nomeCidade = textBox.Text; // Obtém o texto digitado no campo de texto

            // Procurar pelo nome da cidade na tabela de hash cidade
            if (tabela != null)
            {
                Cidade cidadeEncontrada = tabela.Buscar(nomeCidade);
                if (cidadeEncontrada != null)
                {
                    // Cidade encontrada, faça algo com ela
                    MessageBox.Show($"Cidade encontrada: {cidadeEncontrada.NomeCidade}", "Cidade Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Cidade não encontrada
                    MessageBox.Show("Cidade não encontrada", "Cidade Não Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
