namespace WinFormsApp
{
    partial class BokaLogi
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
            button1 = new Button();
            dataGridViewVisaLogi = new DataGridView();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewVisaLogi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(491, 92);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Sök";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSök;
            // 
            // dataGridViewVisaLogi
            // 
            dataGridViewVisaLogi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewVisaLogi.Location = new Point(12, 25);
            dataGridViewVisaLogi.MultiSelect = false;
            dataGridViewVisaLogi.Name = "dataGridViewVisaLogi";
            dataGridViewVisaLogi.ReadOnly = true;
            dataGridViewVisaLogi.RowTemplate.Height = 25;
            dataGridViewVisaLogi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewVisaLogi.Size = new Size(474, 227);
            dataGridViewVisaLogi.TabIndex = 3;
            dataGridViewVisaLogi.CellContentClick += dataGridViewVisaLogi_CellContentClick;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(572, 25);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(474, 227);
            dataGridView1.TabIndex = 4;
            // 
            // BokaLogi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 571);
            Controls.Add(dataGridView1);
            Controls.Add(dataGridViewVisaLogi);
            Controls.Add(button1);
            Name = "BokaLogi";
            Text = "BokaLogi";
            ((System.ComponentModel.ISupportInitialize)dataGridViewVisaLogi).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private DataGridView dataGridViewVisaLogi;
        private DataGridView dataGridView1;
    }
}