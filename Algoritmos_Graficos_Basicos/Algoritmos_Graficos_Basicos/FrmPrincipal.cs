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
    public partial class FrmPrincipal : Form
    {
        private FrmDDA FrmDDAIns;
        private FrmBresenham FrmBresenhamIns;
        private FrmDiscCircunc FrmDiscCircunIns;
        private FrmRelleno FrmRellenoIns;
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmRellenoIns = new FrmRelleno();
        }

        private void btnDDA_Click(object sender, EventArgs e)
        {
            if (FrmDDAIns == null || FrmDDAIns.IsDisposed)
            {
                FrmDDAIns = new FrmDDA();
            }
            FrmDDAIns.Show();
            FrmDDAIns.BringToFront();

        }

        private void btnBR_Click(object sender, EventArgs e)
        {
            if (FrmBresenhamIns == null || FrmBresenhamIns.IsDisposed)
            {
                FrmBresenhamIns = new FrmBresenham();
            }
            FrmBresenhamIns.Show();
            FrmBresenhamIns.BringToFront();
        }

        private void btnCirc_Click(object sender, EventArgs e)
        {
            if (FrmDiscCircunIns == null || FrmDiscCircunIns.IsDisposed)
            {
                FrmDiscCircunIns = new FrmDiscCircunc();
            }
            FrmDiscCircunIns.Show();
            FrmDiscCircunIns.BringToFront();
        }

        private void btnRell_Click(object sender, EventArgs e)
        {
            if (FrmRellenoIns == null || FrmRellenoIns.IsDisposed)
            {
                FrmRellenoIns = new FrmRelleno();
            }
            FrmRellenoIns.Show();
            FrmRellenoIns.BringToFront();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
