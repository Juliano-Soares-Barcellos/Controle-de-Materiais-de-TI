using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class EspelhoPa : Form
    {
        private DataTable tabela = new DataTable();
     
        public EspelhoPa()
        {
            InitializeComponent();
            Preenchimento();
            dataGridView1.CellFormatting+= MudaCorComputador;
        }

        private void Preenchimento()
        {
            tabela = computadoresMapeadosEconsertado.Dao.montarTabelasDao.PasOperacao();
            dataGridView1.DataSource = tabela;
        }

        private void MudaCorComputador(object sender, DataGridViewCellFormattingEventArgs e)
        {
            

                if (e.ColumnIndex == 0 && e.Value != null && e.Value == "")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
             
              
                }
          
            }

        }
    }
