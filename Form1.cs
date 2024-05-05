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

        /// <summary>
        /// Evento disparado quando o usuário clica no botão para ler o arquivo de cidades.
        /// Lê o arquivo de cidades e insere as cidades na tabela de hash.
        /// </summary>
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

        /// <summary>
        /// Evento disparado quando o usuário clica no botão para inserir uma cidade.
        /// Insere uma cidade na tabela de hash.
        /// </summary>
        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (tabela == null)
            {
                MessageBox.Show("Tabela de hash não foi inicializada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nomeCidade = txtCidade.Text;
            double x = double.TryParse(udX.Text, out x) ? x : 0;
            double y = double.TryParse(udY.Text, out y) ? y : 0;

            Cidade novaCidade = new Cidade(nomeCidade,x,y);
            tabela.Inserir(novaCidade);
            lsbCidades.Items.Add(new ListViewItem(new string[] { novaCidade.NomeCidade, novaCidade.X.ToString(), novaCidade.Y.ToString() }));

            MessageBox.Show("Cidade inserida com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Evento disparado quando o usuário clica no botão para remover uma cidade.
        /// Remove uma cidade da tabela de hash.
        /// </summary>
        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (tabela == null)
            {
                MessageBox.Show("Tabela de hash não foi inicializada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lsbCidades.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nenhuma cidade selecionada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem selectedCityItem = (ListViewItem)lsbCidades.SelectedItems[0];
            string nomeCidade = selectedCityItem.SubItems[0].Text;
            double x = double.Parse(selectedCityItem.SubItems[1].Text);
            double y = double.Parse(selectedCityItem.SubItems[2].Text);

            Cidade cidadeRemovida = new Cidade(nomeCidade, x, y);
            tabela.Remover(cidadeRemovida);
            lsbCidades.Items.Remove(selectedCityItem);

            MessageBox.Show("Cidade removida com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Evento disparado quando o usuário clica no botão para buscar uma cidade.
        /// Busca uma cidade na tabela de hash.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (tabela == null)
            {
                MessageBox.Show("Tabela de hash não foi inicializada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nomeCidade = txtCidade.Text;
            double x = double.TryParse(udX.Text, out x) ? x : 0;
            double y = double.TryParse(udY.Text, out y) ? y : 0;

            Cidade cidadeBuscada = new Cidade(nomeCidade, x, y);
            Cidade cidadeEncontrada = cidadeBuscada.Buscar(nomeCidade, tabela);

            if (cidadeEncontrada != null && cidadeEncontrada.NomeCidade == cidadeBuscada.NomeCidade)
            {
                MessageBox.Show($"Nome: {cidadeEncontrada.NomeCidade}\nX: {cidadeEncontrada.X}\nY: {cidadeEncontrada.Y}", "Informações da Cidade", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Cidade buscada: {cidadeBuscada.NomeCidade}\nCidade encontrada: {(cidadeEncontrada != null ? cidadeEncontrada.NomeCidade : "Não encontrada")}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento disparado quando o usuário clica no botão para listar as informações de uma cidade.
        /// Lista as informações de uma cidade.
        /// </summary>
        private void btnListar_Click(object sender, EventArgs e)
        {
            if (lsbCidades.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nenhuma cidade selecionada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem selectedCityItem = (ListViewItem)lsbCidades.SelectedItems[0];
            string nomeCidade = selectedCityItem.SubItems[0].Text;
            double x = double.Parse(selectedCityItem.SubItems[1].Text);
            double y = double.Parse(selectedCityItem.SubItems[2].Text);

            Cidade cidadeSelecionada = new Cidade(nomeCidade, x, y);

            MessageBox.Show($"Nome: {cidadeSelecionada.NomeCidade}\nX: {cidadeSelecionada.X}\nY: {cidadeSelecionada.Y}", "Informações da Cidade", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
