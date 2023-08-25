using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Dao
{
    class ArquivoTxtDao
    {
        public void GeararArquivoTxt(String headset)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Arquivo TXT|*.txt";
                saveFileDialog.Title = "Salvar Arquivo txt";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoArquivo = saveFileDialog.FileName;

                    // Cria e escreve o conteúdo no arquivo
                    using (StreamWriter writer = new StreamWriter(caminhoArquivo, false, Encoding.UTF8))
                    {
                        string texto = $"Quantidade de headset: {headset}, headset na garantia: {"teste"}";
                        writer.WriteLine(texto);
                    }

                    MessageBox.Show("Arquivo criado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }
    }
}


