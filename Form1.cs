using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt3_Chalyi_59022
{
    public partial class FormGlówny : Form
    {
        public FormGlówny()
        {
            InitializeComponent();
        }
        private void FormGlówny_FormClosed(object sender, FormClosedEventArgs e)
        {
            //zamkniękcie aplikacji
            Application.Exit();
        }

        private void bttnLaboratorium_Click(object sender, EventArgs e)
        {
            LABORATORIUM scForma = new LABORATORIUM();
            this.Hide();
            scForma.Show();
        }

        private void bttnProjekt_Click(object sender, EventArgs e)
        {
            PROJEKT scForma = new PROJEKT();
            this.Hide();
            scForma.Show();
        }
    }
}
