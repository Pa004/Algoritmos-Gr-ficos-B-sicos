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
    public partial class FrmRelleno : Form
    {
        CRelleno objRelleno = new CRelleno();
        public FrmRelleno()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            objRelleno.ReadData(txtNumSides, txtSide);
            objRelleno.PlotShape(txtNumSides, picCanvas);
            objRelleno.AttachFloodFillHandler(picCanvas, Color.Blue, dgvPuntos);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            objRelleno.InitializeData(txtNumSides, txtSide, picCanvas);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            objRelleno.CloseForm(this);
        }
    }
}
