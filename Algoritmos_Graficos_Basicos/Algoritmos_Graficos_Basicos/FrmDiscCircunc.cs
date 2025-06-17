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
    public partial class FrmDiscCircunc : Form
    {
        private CDiscCircun cdiscCircun = new CDiscCircun();

        public FrmDiscCircunc()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            cdiscCircun.ReadData(txtXc, txtYc, txtRadio);
            cdiscCircun.PlotShape(picCanvas, dgvIteraciones);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cdiscCircun.InitializeData(txtXc, txtYc, txtRadio, picCanvas, dgvIteraciones);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            cdiscCircun.CloseForm(this);
        }
    }
}
