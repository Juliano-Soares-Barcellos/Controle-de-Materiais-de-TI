
using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class ArrastarArquivoCsv : Form
    {
        private DataTable dataTable = new DataTable();

        public ArrastarArquivoCsv()
        {
            InitializeComponent();
            MessageBox.Show("Atencão !!! \n para importar arquivo o mesmo deve ser do tipo CSV, o arquivo nao deve conter nome de coluna \n " +
                "o arquivo deve conter uma ordem de nome,numero,quantidade_conserto e a data");
            DragEnter += label2_DragEnter;
            DragDrop += label2_DragDrop;
        }

        private void label2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
             ((string[])e.Data.GetData(DataFormats.FileDrop))[0].ToLower().EndsWith(".csv"))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void label2_DragDrop(object sender, DragEventArgs e)
        {
            Teste teste = new Teste();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string caminhoArquivo = files[0];
                // label2.Text = Path.GetFileName(caminhoArquivo);

                List<Object[]> arquivo = ExibirDadosDoCSV(caminhoArquivo);
                Teste t = new Teste();
                NumeroDao numeroDao = new NumeroDao();

                for (int i = 0; i < arquivo.Count; i++)
                {
                    string numero = arquivo[i][1].ToString();
                    Object[] encontrar = numeroDao.EncontrarNumero(numero);

                    if (encontrar.Length > 0)
                    {
                        numeroDao.Update(numero);
                        int idProduto = Convert.ToInt32(encontrar[0]);
                        numeroDao.NovaData(idProduto);
                    }
                    else
                    {
                        t.InsereBancoArquivo(new List<Object[]> { arquivo[i] });
                    }
                }
                EnviarDadosDoArquivo();

                MessageBox.Show("Arquivo Gravado com sucesso");
            }
        }

        private List<Object[]> ExibirDadosDoCSV(string caminhoArquivo)
        {
            List<Object[]> listaDados = new List<Object[]>();

            try
            {
                dataTable.Clear();
                using (StreamReader reader = new StreamReader(caminhoArquivo))
                {
                    string linha = reader.ReadLine();
                    string[] colunas = linha.Split(';');

                    foreach (string coluna in colunas)
                    {
                        dataTable.Columns.Add(coluna.Trim());
                    }

                    while ((linha = reader.ReadLine()) != null)
                    {
                        string[] valores = linha.Split(';');
                        dataTable.Rows.Add(valores);

                        listaDados.Add((Object[])valores.Clone());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler o arquivo CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return listaDados;
        }
        private void ArrastarArquivoCsv_FormClosed(object sender, FormClosedEventArgs e)
        {
            TelaInicial telaInicial = new TelaInicial();
            EnviarDadosDoArquivo();
            telaInicial.Show();
        }


        public event EventHandler DadosEnviados;


        private void EnviarDadosDoArquivo()
        {

            OnDadosEnviados(EventArgs.Empty);
        }


        protected virtual void OnDadosEnviados(EventArgs e)
        {
            DadosEnviados?.Invoke(this, e);
        }

    }
}
