using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class ComputadorConsertado : Form
    {
        private Computador _computador;
        private ComputadorDao _computadorDao;
        private ComputadorSaida _computadorSaida;

        public ComputadorConsertado()
        {
        
            this._computadorDao = new ComputadorDao();
            this._computadorSaida = new ComputadorSaida();

            InitializeComponent();
        }

        private void Benter_Click(object sender, EventArgs e)
        {
            if (Tnome.Text.Equals("") || TDescricao.Text.Equals(""))
            {
                MessageBox.Show("Por favor preencha todos os campos ");
            }
            else
            {
                string message = "Deseja prosseguir? Digite 's' para sim ou 'n' para não:";
                string caption = "Confirmação";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, caption, buttons);

                if (result == DialogResult.Yes)
                {
                    _computadorSaida.DescricaoSaida = TDescricao.Text;
                    _computadorSaida.DataSaida = DateTime.Now;
                    string selectedValues = "";

                    foreach (object item in CheckBox.CheckedItems)
                    {
                        selectedValues += item.ToString() + ",";
                    }

                    if (!string.IsNullOrEmpty(selectedValues))
                    {
                        selectedValues = selectedValues.TrimEnd(',');
                    }
                    
                    _computador =new Computador(_computadorDao.AcharPcs(Tnome.Text),Tnome.Text,"","", selectedValues);
                    _computadorSaida.Computador_id = _computador;

                    if (_computador.Id >= 1)
                    {
                       List<int>Datas= _computadorDao.CompararTamanhoData(_computadorSaida);
                        if (Datas.Count>1)
                        {
                            if (Datas[1] < Datas[0])
                            {
                                _computadorDao.InserirComputadorSaida(_computadorSaida);
                                _computadorDao.updaterComputador(_computador);
                            }
                            else
                            {
                                MessageBox.Show("Não é possivel dar baixa nesse computador mais que uma vez");
                            }
                        }
                        else
                        {
                            MessageBox.Show("");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este computador nao existe");

                    }


                }
                else
                {
                    MessageBox.Show("Problemas ao inserir no banco, favor contatar os profissionais de TI");
                }
            }
        }
    }
}
