using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Dao
{
    class GravadorCsv
    {
        public void GravarCSV(DataTable tabela)
        {
            // Abre o SaveFileDialog para que o usuário escolha o nome e local do arquivo CSV
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivo CSV|*.csv";
            saveFileDialog.Title = "Salvar Arquivo CSV";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoArquivo = saveFileDialog.FileName;

                using (StreamWriter writer = new StreamWriter(caminhoArquivo, false, Encoding.UTF8))
                {
                    // Escreve o cabeçalho do arquivo CSV com o nome das colunas
                    foreach (DataColumn coluna in tabela.Columns)
                    {
                        writer.Write(coluna.ColumnName + ";");
                    }
                    writer.WriteLine(); // Nova linha após o cabeçalho

                    // Escreve os dados da tabela no arquivo CSV
                    foreach (DataRow linha in tabela.Rows)
                    {
                        foreach (var item in linha.ItemArray)
                        {
                            writer.Write(item + ";");
                        }
                        writer.WriteLine(); // Nova linha após cada linha da tabela
                    }
                }

                MessageBox.Show("Arquivo gravado com sucesso !!!");
            }
        }
    }
}
