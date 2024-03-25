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

    ITabelaDeHash<Cidade> tabela;

    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {

    }

    private void btnLerArquivo_Click(object sender, EventArgs e)
    {
      if (dlgAbrir.ShowDialog() == DialogResult.OK)
      {
        if (rbBucketHash.Checked)
           tabela = new BucketHash<Cidade>();
        else
          if (rbHashLinear.Checked)
             tabela = new HashLinear<Cidade>();
          else 
            if (rbHashQuadratico.Checked)
               tabela = new HashQuadratico<Cidade>();
            else
              if (rbHashDuplo.Checked)
                tabela = new HashDuplo<Cidade>();


        var arquivo = new StreamReader(dlgAbrir.FileName);
        while (! arquivo.EndOfStream) 
        {
          Cidade umaCidade = new Cidade();
          umaCidade.LerRegistro(arquivo);
          tabela.Inserir(umaCidade);
        }

        arquivo.Close();
      }
    }
  }
}
