namespace WinFormsApp
{
    partial class HuvudMeny
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
            btnBokaLogi = new Button();
            button2 = new Button();
            BokaSkidskola = new Button();
            BokaUtrustning = new Button();
            LoggaUt = new Button();
            SuspendLayout();
            // 
            // btnBokaLogi
            // 
            btnBokaLogi.Location = new Point(293, 172);
            btnBokaLogi.Margin = new Padding(3, 4, 3, 4);
            btnBokaLogi.Name = "btnBokaLogi";
            btnBokaLogi.Size = new Size(138, 31);
            btnBokaLogi.TabIndex = 0;
            btnBokaLogi.Text = "Boka Logi";
            btnBokaLogi.UseVisualStyleBackColor = true;
            btnBokaLogi.Click += btnBokaLogi1;
            // 
            // button2
            // 
            button2.Location = new Point(523, 172);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(170, 31);
            button2.TabIndex = 1;
            button2.Text = "Boka konferenslokal";
            button2.UseVisualStyleBackColor = true;
            // 
            // BokaSkidskola
            // 
            BokaSkidskola.Location = new Point(523, 275);
            BokaSkidskola.Margin = new Padding(3, 4, 3, 4);
            BokaSkidskola.Name = "BokaSkidskola";
            BokaSkidskola.Size = new Size(170, 31);
            BokaSkidskola.TabIndex = 2;
            BokaSkidskola.Text = "Boka skidskola";
            BokaSkidskola.UseVisualStyleBackColor = true;
            // 
            // BokaUtrustning
            // 
            BokaUtrustning.Location = new Point(293, 275);
            BokaUtrustning.Margin = new Padding(3, 4, 3, 4);
            BokaUtrustning.Name = "BokaUtrustning";
            BokaUtrustning.Size = new Size(138, 31);
            BokaUtrustning.TabIndex = 3;
            BokaUtrustning.Text = "Boka utrustning";
            BokaUtrustning.UseVisualStyleBackColor = true;
            // 
            // LoggaUt
            // 
            LoggaUt.Location = new Point(54, 447);
            LoggaUt.Margin = new Padding(3, 4, 3, 4);
            LoggaUt.Name = "LoggaUt";
            LoggaUt.Size = new Size(86, 31);
            LoggaUt.TabIndex = 4;
            LoggaUt.Text = "Logga ut";
            LoggaUt.UseVisualStyleBackColor = true;
            LoggaUt.Click += LoggaUt_Click;
            // 
            // HuvudMeny
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 489);
            Controls.Add(LoggaUt);
            Controls.Add(BokaUtrustning);
            Controls.Add(BokaSkidskola);
            Controls.Add(button2);
            Controls.Add(btnBokaLogi);
            Margin = new Padding(3, 4, 3, 4);
            Name = "HuvudMeny";
            Text = "HuvudMeny";
            ResumeLayout(false);
        }

        #endregion

        private Button btnBokaLogi;
        private Button button2;
        private Button BokaSkidskola;
        private Button BokaUtrustning;
        private Button LoggaUt;
    }
}