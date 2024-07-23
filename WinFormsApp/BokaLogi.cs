using AffärsLager;
using Entiteterna;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WinFormsApp
{
    public partial class BokaLogi : Form
    {
        private Controller controller;
        private IList<Logial> tillgänglig;
        private BindingList<Logial> valdaLogialer = new BindingList<Logial>();
        public BokaLogi(Controller controller)
        {

            InitializeComponent();
            this.controller = controller;
            dataGridViewVisaLogi.DataSource = new BindingList<Logial>(controller.HämtaTillgängligaLogialer());
        }
       
        private void dateTimePickerFrån_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerTill(object sender, EventArgs e)
        {

        }

        private void btnSök(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (dataGridViewVisaLogi.SelectedRows.Count > 0)
            {
                // Get the index of the first selected row
                int selectedIndex = dataGridViewVisaLogi.SelectedRows[0].Index;

                // Ensure tillgänglig is not null and selectedIndex is within the bounds
                if (tillgänglig != null && selectedIndex < tillgänglig.Count)
                {
                    Logial valdLogi = tillgänglig[selectedIndex];
                    controller.Otillgänglig(valdLogi);
                    valdaLogialer.Add(valdLogi);

                    // Add a new row to dataGridView1 and set its value
                    int newRowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[newRowIndex].Cells[0].Value= valdLogi; // Adjust the cell index and value as needed

                    // Remove the selected row from dataGridViewVisaLogi
                    dataGridViewVisaLogi.Rows.RemoveAt(selectedIndex);
                    dataGridView1.DataSource= new BindingList<Logial>(controller.HämtaTillgängligaLogialer());
                    dataGridView1.Refresh();
                  
                }
            }
        }

        private void dataGridViewVisaLogi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
