namespace WinFormsApp
{
    partial class BokaKonferensLokal
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
            btn_Sök1 = new Button();
            dataGridView_1 = new DataGridView();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dataGridView_1).BeginInit();
            SuspendLayout();
            // 
            // btn_Sök1
            // 
            btn_Sök1.Location = new Point(616, 56);
            btn_Sök1.Name = "btn_Sök1";
            btn_Sök1.Size = new Size(94, 29);
            btn_Sök1.TabIndex = 0;
            btn_Sök1.Text = "Sök";
            btn_Sök1.UseVisualStyleBackColor = true;
            btn_Sök1.Click += btn_Sök1_Click;
            // 
            // dataGridView_1
            // 
            dataGridView_1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_1.Location = new Point(190, 158);
            dataGridView_1.Name = "dataGridView_1";
            dataGridView_1.RowHeadersWidth = 51;
            dataGridView_1.RowTemplate.Height = 29;
            dataGridView_1.Size = new Size(396, 231);
            dataGridView_1.TabIndex = 1;
            dataGridView_1.CellContentClick += dataGridView_1_CellContentClick;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(66, 55);
            dateTimePicker1.MaxDate = new DateTime(2030, 12, 25, 0, 0, 0, 0);
            dateTimePicker1.MinDate = new DateTime(2023, 9, 28, 10, 17, 52, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(221, 27);
            dateTimePicker1.TabIndex = 2;
            dateTimePicker1.Value = new DateTime(2023, 12, 25, 23, 59, 59, 0);
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(344, 55);
            dateTimePicker2.MaxDate = new DateTime(2030, 12, 31, 0, 0, 0, 0);
            dateTimePicker2.MinDate = new DateTime(2023, 8, 29, 23, 59, 0, 0);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(226, 27);
            dateTimePicker2.TabIndex = 3;
            dateTimePicker2.Value = new DateTime(2023, 12, 25, 23, 59, 59, 0);
            // 
            // BokaKonferensLokal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(775, 450);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(dataGridView_1);
            Controls.Add(btn_Sök1);
            Name = "BokaKonferensLokal";
            Text = "BokaKonferensLokal";
            ((System.ComponentModel.ISupportInitialize)dataGridView_1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Sök1;
        private DataGridView dataGridView_1;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
    }
}