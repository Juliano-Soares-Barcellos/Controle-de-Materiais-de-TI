namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    partial class FiltroMes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LabelConsertado = new System.Windows.Forms.Label();
            this.LabelGarantia = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Tabela = new System.Windows.Forms.DataGridView();
            this.BotaoProcurar = new System.Windows.Forms.Button();
            this.ComboBoxAno = new System.Windows.Forms.ComboBox();
            this.ComboBoxMes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tabela)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.LabelConsertado);
            this.panel1.Controls.Add(this.LabelGarantia);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Tabela);
            this.panel1.Controls.Add(this.BotaoProcurar);
            this.panel1.Controls.Add(this.ComboBoxAno);
            this.panel1.Controls.Add(this.ComboBoxMes);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 660);
            this.panel1.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(199, 614);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(92, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Gravar Dados";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(386, 495);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 545);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(266, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Total de vezes que estes produtos foram consertados :";
            this.label6.Visible = false;
            // 
            // LabelConsertado
            // 
            this.LabelConsertado.AutoSize = true;
            this.LabelConsertado.Location = new System.Drawing.Point(386, 545);
            this.LabelConsertado.Name = "LabelConsertado";
            this.LabelConsertado.Size = new System.Drawing.Size(35, 13);
            this.LabelConsertado.TabIndex = 10;
            this.LabelConsertado.Text = "label7";
            this.LabelConsertado.Visible = false;
            // 
            // LabelGarantia
            // 
            this.LabelGarantia.AutoSize = true;
            this.LabelGarantia.Location = new System.Drawing.Point(386, 448);
            this.LabelGarantia.Name = "LabelGarantia";
            this.LabelGarantia.Size = new System.Drawing.Size(35, 13);
            this.LabelGarantia.TabIndex = 9;
            this.LabelGarantia.Text = "label7";
            this.LabelGarantia.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 495);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "produtos Consertado neste mês :";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 448);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Total de produtos na garantia";
            this.label4.Visible = false;
            // 
            // Tabela
            // 
            this.Tabela.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Tabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tabela.Location = new System.Drawing.Point(3, 257);
            this.Tabela.Name = "Tabela";
            this.Tabela.Size = new System.Drawing.Size(485, 174);
            this.Tabela.TabIndex = 6;
            // 
            // BotaoProcurar
            // 
            this.BotaoProcurar.Location = new System.Drawing.Point(260, 209);
            this.BotaoProcurar.Name = "BotaoProcurar";
            this.BotaoProcurar.Size = new System.Drawing.Size(75, 23);
            this.BotaoProcurar.TabIndex = 5;
            this.BotaoProcurar.Text = "Procurar";
            this.BotaoProcurar.UseVisualStyleBackColor = true;
            this.BotaoProcurar.Click += new System.EventHandler(this.BotaoProcurar_Click);
            // 
            // ComboBoxAno
            // 
            this.ComboBoxAno.FormattingEnabled = true;
            this.ComboBoxAno.Items.AddRange(new object[] {
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.ComboBoxAno.Location = new System.Drawing.Point(218, 163);
            this.ComboBoxAno.Name = "ComboBoxAno";
            this.ComboBoxAno.Size = new System.Drawing.Size(173, 21);
            this.ComboBoxAno.TabIndex = 4;
            // 
            // ComboBoxMes
            // 
            this.ComboBoxMes.FormattingEnabled = true;
            this.ComboBoxMes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ComboBoxMes.Items.AddRange(new object[] {
            "Janeiro",
            "Fevereiro",
            "Março",
            "Abril",
            "Maio",
            "Junho",
            "Julho",
            "Agosto",
            "Setembro",
            "Outubro",
            "Novembro",
            "Dezembro"});
            this.ComboBoxMes.Location = new System.Drawing.Point(218, 105);
            this.ComboBoxMes.Name = "ComboBoxMes";
            this.ComboBoxMes.Size = new System.Drawing.Size(173, 21);
            this.ComboBoxMes.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Escolha o Ano :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Escolha o mes :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Verificacao de Gasto de Conserto Por Mes :";
            // 
            // FiltroMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 660);
            this.Controls.Add(this.panel1);
            this.Name = "FiltroMes";
            this.Text = "FiltroMes";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tabela)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LabelConsertado;
        private System.Windows.Forms.Label LabelGarantia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView Tabela;
        private System.Windows.Forms.Button BotaoProcurar;
        private System.Windows.Forms.ComboBox ComboBoxAno;
        private System.Windows.Forms.ComboBox ComboBoxMes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}