using AffärsLager;
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
    public partial class Inloggning : Form
    {
        private Controller controller;
        public Inloggning(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void btnLoggaIn_Click(object sender, EventArgs e)
        {
            if (controller.LoggaIn(int.Parse(textBoxAnvändarNamn.Text), textBoxLösenord.Text))
            {
                new HuvudMeny(controller).Show();
                this.Close();
            }
            else if (controller.LoggaInMottagare(int.Parse(textBoxAnvändarNamn.Text), textBoxLösenord.Text))
            {
                new HuvudMeny(controller).Show();
                this.Close();
            }
            else if (controller.LoggaIn2(int.Parse(textBoxAnvändarNamn.Text), textBoxLösenord.Text))
            {
                new HuvudMeny(controller).Show();
                this.Close();
            }
        }

        private void btnAvbryt_Click(object sender, EventArgs e)
        {

        }



        private void btnAvbryt_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
