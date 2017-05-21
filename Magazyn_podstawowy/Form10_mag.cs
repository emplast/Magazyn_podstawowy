using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazyn_podstawowy
{
    public partial class Form10_mag : Form
    {
        public Form10_mag()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
