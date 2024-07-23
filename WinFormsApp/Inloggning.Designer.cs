namespace WinFormsApp
{
    partial class Inloggning
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
            LoggaIn = new Label();
            Användare = new Label();
            Lösenord = new Label();
            btnLoggain = new Button();
            btnAvbryt = new Button();
            textBoxAnvändarNamn = new TextBox();
            textBoxLösenord = new TextBox();
            SuspendLayout();
            // 
            // LoggaIn
            // 
            LoggaIn.AutoSize = true;
            LoggaIn.Location = new Point(210, 9);
            LoggaIn.Name = "LoggaIn";
            LoggaIn.Size = new Size(67, 20);
            LoggaIn.TabIndex = 0;
            LoggaIn.Text = "Logga in";
            // 
            // Användare
            // 
            Användare.AutoSize = true;
            Användare.Location = new Point(197, 77);
            Användare.Name = "Användare";
            Användare.Size = new Size(80, 20);
            Användare.TabIndex = 1;
            Användare.Text = "Användare";
            // 
            // Lösenord
            // 
            Lösenord.AutoSize = true;
            Lösenord.Location = new Point(197, 156);
            Lösenord.Name = "Lösenord";
            Lösenord.Size = new Size(70, 20);
            Lösenord.TabIndex = 2;
            Lösenord.Text = "Lösenord";
            // 
            // btnLoggain
            // 
            btnLoggain.Location = new Point(337, 253);
            btnLoggain.Margin = new Padding(3, 4, 3, 4);
            btnLoggain.Name = "btnLoggain";
            btnLoggain.Size = new Size(86, 31);
            btnLoggain.TabIndex = 3;
            btnLoggain.Text = "Logga in";
            btnLoggain.UseVisualStyleBackColor = true;
            btnLoggain.Click += btnLoggaIn_Click;
            // 
            // btnAvbryt
            // 
            btnAvbryt.Location = new Point(14, 253);
            btnAvbryt.Margin = new Padding(3, 4, 3, 4);
            btnAvbryt.Name = "btnAvbryt";
            btnAvbryt.Size = new Size(86, 31);
            btnAvbryt.TabIndex = 4;
            btnAvbryt.Text = "Avbryt";
            btnAvbryt.UseVisualStyleBackColor = true;
            btnAvbryt.Click += btnAvbryt_Click_1;
            // 
            // textBoxAnvändarNamn
            // 
            textBoxAnvändarNamn.Location = new Point(122, 101);
            textBoxAnvändarNamn.Margin = new Padding(3, 4, 3, 4);
            textBoxAnvändarNamn.Name = "textBoxAnvändarNamn";
            textBoxAnvändarNamn.Size = new Size(233, 27);
            textBoxAnvändarNamn.TabIndex = 5;
            // 
            // textBoxLösenord
            // 
            textBoxLösenord.Location = new Point(122, 180);
            textBoxLösenord.Margin = new Padding(3, 4, 3, 4);
            textBoxLösenord.Name = "textBoxLösenord";
            textBoxLösenord.Size = new Size(233, 27);
            textBoxLösenord.TabIndex = 6;
            // 
            // Inloggning
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 300);
            Controls.Add(textBoxLösenord);
            Controls.Add(textBoxAnvändarNamn);
            Controls.Add(btnAvbryt);
            Controls.Add(btnLoggain);
            Controls.Add(Lösenord);
            Controls.Add(Användare);
            Controls.Add(LoggaIn);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Inloggning";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LoggaIn;
        private Label Användare;
        private Label Lösenord;
        private Button btnLoggain;
        private Button btnAvbryt;
        private TextBox textBoxAnvändarNamn;
        private TextBox textBoxLösenord;
    }
}