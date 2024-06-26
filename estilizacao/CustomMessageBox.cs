using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomMessageBox : Form
{
    private Label messageLabel;
    private Button yesButton;
    private Button noButton;
    private DialogResult result = DialogResult.No;

    public CustomMessageBox(string message, string caption)
    {
        this.Text = caption;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Size = new Size(300, 200); // Novo tamanho do formulário
        this.Font = new Font("Arial", 8); // Define a fonte aqui

        // Define a posição e tamanho do Label
        messageLabel = new Label();
        messageLabel.Text = message;
        messageLabel.AutoSize = true;
        messageLabel.Location = new Point(20, 20); // Posição do Label
        messageLabel.MaximumSize = new Size(260, 0); // Limita a largura do texto para quebrar linha
        messageLabel.Font = this.Font;

        // Define a posição e tamanho dos botões
        yesButton = new Button();
        yesButton.Text = "Sim";
        yesButton.DialogResult = DialogResult.Yes;
        yesButton.Location = new Point(100, 130); // Posição do botão "Sim"
        yesButton.Size = new Size(75, 23); // Tamanho do botão "Sim"

        noButton = new Button();
        noButton.Text = "Não";
        noButton.DialogResult = DialogResult.No;
        noButton.Location = new Point(180, 130); // Posição do botão "Não"
        noButton.Size = new Size(75, 23); // Tamanho do botão "Não"

        // Adiciona os controles ao formulário
        this.Controls.Add(messageLabel);
        this.Controls.Add(yesButton);
        this.Controls.Add(noButton);

        this.AcceptButton = yesButton;
        this.CancelButton = noButton;
    }

    public static DialogResult Show(string message, string caption)
    {
        using (CustomMessageBox msgBox = new CustomMessageBox(message, caption))
        {
            return msgBox.ShowDialog();
        }
    }
}
