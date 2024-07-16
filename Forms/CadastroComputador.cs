using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
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
    public partial class CadastroComputador : Form
    {
        public string textoServer = "Digite a tag de serviço";
        public string textoPatrimonio = "Digite o Patrimonio";
        public string textoValor = "Preencha o valor da compra";
        public string textoModelo = "Selecione ou digite o modelo";
        public string textoProcessador = "Digite o processador";
        public string textoNota = "Digite sua nota fiscal";
        public string textoSistemaOperacional = "Selecione seu sistema operacional";
        private string retornoTextEmBranco = "";


        public CadastroComputador()
        {
            InitializeComponent();
            configText();
            comboboxpreenchidaModelo(comboModelo);
            configurarTexto();
        }


        private void panel18_MouseEnter(object sender, EventArgs e)
        {
            this.panel18.Size = new System.Drawing.Size(200, 100);
        }


        private void panel2_Click(object sender, EventArgs e)
        {
            verificaNaoTaEscritoVoltaProLocal();
        }


        private void panel18_MouseLeave(object sender, EventArgs e)
        {
            this.panel18.Size = new System.Drawing.Size(200, 39);
        }


        public void verificaNaoTaEscritoVoltaProLocal()
        {
            verificaLbLabel(textServerTag,  textoServer);
            verificaLbLabel(textPatrimonio, textoPatrimonio);
            verificaLbLabel(maskedTextBox4, textoValor);
            verificaLbLabel(textProcessador,textoProcessador);
            verificaComboLabel(comboModelo, textoModelo);
            verificaComboLabel(comboSistema, textoSistemaOperacional);

        }

        private void textServerTag_MouseClick(object sender, MouseEventArgs e)
        {
            verificaText(textServerTag, textoServer);
        }


        private void textPatrimonio_Click(object sender, EventArgs e)
        {

            verificaText(textPatrimonio, textoPatrimonio);


        }

        private void maskedTextBox4_Click(object sender, EventArgs e)
        {
            verificaText(maskedTextBox4, textoValor);
        }

        private void textProcessador_Click(object sender, EventArgs e)
        {
            verificaText(textProcessador, textoProcessador);
        }
        public void verificaComboBox(ComboBox combo, string texto)
        {
           
            if (combo.Text.Equals(texto))
                combo.Text = "";
        }

        public void verificaText(MaskedTextBox textBox, string texto)
        {
           
            if (textBox.Text.Equals(texto))
            {
                textBox.Text = "";
            }
        }


        public void verificaLbLabel(MaskedTextBox textBox, string texto)
        {
            textBox.Mask = null;
            if (textBox.Text.Equals("") || textBox.Text == null )
               
                textBox.Text = texto;
               
        }

        public void verificaComboLabel(ComboBox combo, string texto)
        {
            if (combo.Text.Equals("") || combo.Text == null)
            {
                combo.Text = texto;
            }
        }



        private void comboModelo_Click(object sender, EventArgs e)
        {
            verificaComboBox(comboModelo,  textoModelo);

        }

        private void comboSistema_Click(object sender, EventArgs e)
        {
            verificaComboBox(comboSistema, textoSistemaOperacional);
        }

        private void panel17_Click(object sender, EventArgs e)
        {
            verificaNaoTaEscritoVoltaProLocal();
        }

        private void textBoxKey(MaskedTextBox textBox, string nomeText)
        {
            if (textBox != null)
            {
                textBox.KeyPress += (sender, e) => maskedTextBox1_KeyPress(sender, e, textBox, returnMask(nomeText));
            }
        }

        private string returnMask(string nomeTexto)
        {
            switch (nomeTexto)
            {
                case "patrimonio":
                    return "0000";
                case "tag":
                    return "AAAAAAAA"; // Mascara para permitir letras e números
                case "processador":
                    return "AAAAA";
                case "valor":
                    return "000000000";
                default:
                    return "";
            }
        }


        // O evento de não deixar entrar letras nos inputs
        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e, MaskedTextBox textBox, string mask)
        {
            textBox.Mask = mask;

            bool permiteLetras = mask == "AAAAAAAA" || mask == "AAAAA";

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !permiteLetras)
            {
                e.Handled = true;
            }
        }

        // Adicionando os eventos para cada input
        private void configurarTexto()
        {
            textBoxKey(textPatrimonio, "patrimonio");
            textBoxKey(textServerTag, "tag");
            textBoxKey(maskedTextBox4, "valor");
            textBoxKey(textProcessador, "processador");
        }


        private void ConfigurarTextBoxComTextoInicial(string textoInicial, MaskedTextBox textBox = null, ComboBox combo = null)
            {
           
                if  (textBox !=null)
                {
                  textBox.Enter += (sender, e) => TextBox_Enter(sender, e, textoInicial);
                  textBox.Leave += (sender, e) => TextBox_Leave(sender, e, textoInicial);
                }
               
                if (combo!=null)
                {
                    combo.Enter += (sender, e) => TextBox_Enter(sender, e, textoInicial);
                    combo.Leave += (sender, e) => TextBox_Leave(sender, e, textoInicial);
                }
            }

            private void TextBox_Enter(object sender, EventArgs e, string textoInicial)
            {
                verificaNaoTaEscritoVoltaProLocal();
            }

            private void TextBox_Leave(object sender, EventArgs e, string textoInicial)
            {
                     verificaNaoTaEscritoVoltaProLocal();
            }

            private void configText()
            {
                ConfigurarTextBoxComTextoInicial(textoServer,textServerTag, null);
                ConfigurarTextBoxComTextoInicial(textoPatrimonio,textPatrimonio, null);
                ConfigurarTextBoxComTextoInicial(textoValor,maskedTextBox4, null);
                ConfigurarTextBoxComTextoInicial(textoProcessador,textProcessador, null);
                ConfigurarTextBoxComTextoInicial(textoSistemaOperacional,null, comboSistema);
                ConfigurarTextBoxComTextoInicial(textoModelo,null, comboModelo);
            }

   

        public void comboboxpreenchidaModelo(ComboBox combo)
        {
             ComputadorDao _computadorDao = new ComputadorDao();
             combo.Items.Clear();
            foreach (String item in _computadorDao.carregarCombobox())
            {
                combo.Items.Add(item);
            }
        }

        public Boolean validaEntradaDados(string texto,MaskedTextBox textBox=null, ComboBox combo =null)
        {
            if (textBox != null)
            {
                if (textBox.Text.Equals(texto) || textBox.Text.Equals(""))
                {
                    retornoTextEmBranco = textBox.Text;

                    if (textBox.Text.Equals(""))
                    {
                        retornoTextEmBranco = "Atenção todos os campos são necessarios";
                    }
                    
                    return false;
                }
            }
            if (combo != null)
            {
                if (combo.Text.Equals("")  || combo.Text.Equals(texto))
                {
                    retornoTextEmBranco = combo.Text;

                    if (combo.Text.Equals(""))
                    {
                        retornoTextEmBranco = "Atenção todos os campos são necessarios";
                    }

                    return false;
                }

            }

            return true;
        }

        public Boolean validaSeNaoTemTextEmBranco()
        {
            if (validaEntradaDados(textoPatrimonio, textPatrimonio) != true
               || validaEntradaDados(textoServer, textServerTag) != true
               || validaEntradaDados(textoValor, maskedTextBox4) != true
               || validaEntradaDados(textoProcessador, textProcessador) != true
               || validaEntradaDados(textoSistemaOperacional, null, comboSistema) != true
               || validaEntradaDados(textoModelo, null, comboModelo) != true)
            {
                MessageBox.Show(retornoTextEmBranco);
                return false;
            }
            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
           if(validaSeNaoTemTextEmBranco())
            {
                MessageBox.Show("ok");

            }

        }

      
    }
}

