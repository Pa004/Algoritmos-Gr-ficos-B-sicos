using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algoritmos_Graficos_Basicos
{
    public partial class FrmDDA : Form
    {
        private CDDA cdda = new CDDA();
        public FrmDDA()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            cdda.ReadData(txtXi, txtYi, txtXf, txtYf);
            cdda.PlotShape(picCanvas, dgvIteraciones);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cdda.InitializeData(txtXi, txtYi, txtXf, txtYf, picCanvas, dgvIteraciones);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            cdda.CloseForm(this);
        }
    }
}
