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

namespace WinFormsApp
{
    public partial class BokaKonferensLokal : Form
    {
        private Controller controller;
        private IList<Konferenslokal> tillgänglig;
        private BindingList<Konferenslokal> valdaKonferenslokaler = new BindingList<Konferenslokal>();
        public BokaKonferensLokal(Controller controller)
        {
            InitializeComponent();
            //this.controller = controller;
            //dataGridView_1.DataSource = new BindingList<Konferenslokal>(controller.HämtaTillgängligaLokaler());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }



        private void btn_Sök1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
