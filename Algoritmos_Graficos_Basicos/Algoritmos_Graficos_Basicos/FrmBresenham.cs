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
    public partial class FrmBresenham : Form
    {
        private CBresenham cbresenham = new CBresenham();

        public FrmBresenham()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            cbresenham.ReadData(txtXi, txtYi, txtXf, txtYf);
            cbresenham.PlotShape(picCanvas, dgvIteraciones);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbresenham.InitializeData(txtXi, txtYi, txtXf, txtYf, picCanvas, dgvIteraciones);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            cbresenham.CloseForm(this);
        }
    }
}
