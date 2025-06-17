using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Algoritmos_Graficos_Basicos
{
    internal class CDiscCircun
    {
        private float xc;
        private float yc;
        private float radio;
        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        public CDiscCircun()
        {
            xc = 0.0f; yc = 0.0f; radio = 0.0f;
        }

        // Lee los datos de los TextBox y los asigna a los atributos de la clase
        public void ReadData(TextBox txtXc, TextBox txtYc, TextBox txtRadio)
        {
            try
            {
                xc = float.Parse(txtXc.Text);
                yc = float.Parse(txtYc.Text);
                radio = float.Parse(txtRadio.Text);
            }
            catch
            {
                MessageBox.Show("Ingreso no válido...", "Mensaje de error");
            }
        }

        // Inicializa los datos y limpia los TextBox y el PictureBox
        public void InitializeData(TextBox txtXc, TextBox txtYc, TextBox txtRadio, PictureBox picCanvas, DataGridView dgv)
        {
            xc = 0.0f; yc = 0.0f; radio = 0.0f;
            txtXc.Text = ""; txtYc.Text = ""; txtRadio.Text = "";
            txtXc.Focus();
            picCanvas.Refresh();
            if (dgv != null)
            {
                dgv.Rows.Clear();
            }
        }

        // Algoritmo de Bresenham para circunferencia con tabla de iteraciones
        private void BresenhamCircle(float xc, float yc, float r, PictureBox picCanvas, DataGridView dgv)
        {
            int x_c = (int)Math.Round(xc);
            int y_c = (int)Math.Round(yc);
            int rad = (int)Math.Round(r);

            if (dgv != null)
            {
                dgv.Rows.Clear();
                dgv.Columns.Clear();
                dgv.Columns.Add("Iteracion", "Iteración");
                dgv.Columns.Add("X", "X");
                dgv.Columns.Add("Y", "Y");
            }

            float centerX = picCanvas.Width / 2f;
            float centerY = picCanvas.Height / 2f;
            mGraph = picCanvas.CreateGraphics();
            mPen = new Pen(Color.ForestGreen, 1);

            int x = 0;
            int y = rad;
            int d = 3 - 2 * rad;
            int i = 0;

            // Guardar los puntos previos para unirlos en cada octante
            PointF?[] prevPoints = new PointF?[8];

            void PlotCirclePoints(int cx, int cy, int px, int py)
            {
                // Calcula los 8 puntos simétricos
                PointF[] points = new PointF[8];
                points[0] = new PointF(centerX + (cx + px) * SF, centerY - (cy + py) * SF);
                points[1] = new PointF(centerX + (cx - px) * SF, centerY - (cy + py) * SF);
                points[2] = new PointF(centerX + (cx + px) * SF, centerY - (cy - py) * SF);
                points[3] = new PointF(centerX + (cx - px) * SF, centerY - (cy - py) * SF);
                points[4] = new PointF(centerX + (cx + py) * SF, centerY - (cy + px) * SF);
                points[5] = new PointF(centerX + (cx - py) * SF, centerY - (cy + px) * SF);
                points[6] = new PointF(centerX + (cx + py) * SF, centerY - (cy - px) * SF);
                points[7] = new PointF(centerX + (cx - py) * SF, centerY - (cy - px) * SF);

                for (int k = 0; k < 8; k++)
                {
                    mGraph.DrawRectangle(mPen, (int)Math.Round(points[k].X), (int)Math.Round(points[k].Y), 1, 1);

                    // Une el punto anterior con el actual si existe
                    if (prevPoints[k].HasValue)
                    {
                        mGraph.DrawLine(mPen, prevPoints[k].Value, points[k]);
                    }
                    prevPoints[k] = points[k];
                }
            }

            while (y >= x)
            {
                PlotCirclePoints(x_c, y_c, x, y);

                if (dgv != null)
                {
                    dgv.Invoke((MethodInvoker)delegate
                    {
                        dgv.Rows.Add(i, x, y);
                        dgv.FirstDisplayedScrollingRowIndex = dgv.RowCount - 1;
                    });
                }

                Application.DoEvents();
                Thread.Sleep(200);

                x++;
                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                {
                    d = d + 4 * x + 6;
                }
                i++;
            }
        }

        // Traza la circunferencia y muestra la tabla de iteraciones lógicas
        public void PlotShape(PictureBox picCanvas, DataGridView dgv)
        {
            BresenhamCircle(xc, yc, radio, picCanvas, dgv);
        }

        public void CloseForm(Form ObjForm)
        {
            ObjForm.Close();
        }
    }
}
