using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Algoritmos_Graficos_Basicos
{
    internal class CBresenham
    {
        private float x_i;
        private float y_i;
        private float x_f;
        private float y_f;
        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        public CBresenham()
        {
            x_i = 0.0f; y_i = 0.0f; x_f = 0.0f; y_f = 0.0f;
        }

        // Lee los datos de los TextBox y los asigna a los atributos de la clase
        public void ReadData(TextBox txtXi, TextBox txtYi, TextBox txtXf, TextBox txtYf)
        {
            try
            {
                x_i = float.Parse(txtXi.Text);
                y_i = float.Parse(txtYi.Text);
                x_f = float.Parse(txtXf.Text);
                y_f = float.Parse(txtYf.Text);
            }
            catch
            {
                MessageBox.Show("Ingreso no válido...", "Mensaje de error");
            }
        }

        // Inicializa los datos y limpia los TextBox y el PictureBox
        public void InitializeData(TextBox txtXi, TextBox txtYi, TextBox txtXf, TextBox txtYf, PictureBox picCanvas, DataGridView dgv)
        {
            x_i = 0.0f; y_i = 0.0f; x_f = 0.0f; y_f = 0.0f;
            txtXi.Text = ""; txtYi.Text = ""; txtXf.Text = ""; txtYf.Text = "";
            txtXi.Focus();
            picCanvas.Refresh();
            if (dgv != null)
            {
                dgv.Rows.Clear();
            }
        }

        // Bresenham con retardo y llenado de tabla usando valores lógicos
        private void Bresenham_Line_Logical(float xi, float yi, float xf, float yf, PictureBox picCanvas, DataGridView dgv)
        {
            int x0 = (int)Math.Round(xi);
            int y0 = (int)Math.Round(yi);
            int x1 = (int)Math.Round(xf);
            int y1 = (int)Math.Round(yf);

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

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
            mPen = new Pen(Color.MediumBlue, 1);

            int i = 0;
            // Variables para el punto anterior
            float prevDrawX = centerX + x0 * SF;
            float prevDrawY = centerY - y0 * SF;
            int prevX = x0;
            int prevY = y0;

            while (true)
            {
                float drawX = centerX + x0 * SF;
                float drawY = centerY - y0 * SF;

                // Dibuja línea desde el punto anterior al actual (excepto en la primera iteración)
                if (i > 0)
                {
                    mGraph.DrawLine(mPen, prevDrawX, prevDrawY, drawX, drawY);
                }
                // Opcional: también puedes marcar el punto
                mGraph.DrawRectangle(mPen, (int)Math.Round(drawX), (int)Math.Round(drawY), 1, 1);

                // Muestra valores lógicos en la tabla
                if (dgv != null)
                {
                    dgv.Invoke((MethodInvoker)delegate
                    {
                        dgv.Rows.Add(i, x0, y0);
                        dgv.FirstDisplayedScrollingRowIndex = dgv.RowCount - 1;
                    });
                }

                Application.DoEvents();
                Thread.Sleep(200);

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
                i++;

                // Actualiza el punto anterior
                prevDrawX = drawX;
                prevDrawY = drawY;
                prevX = x0;
                prevY = y0;
            }
        }

        // Traza la línea y muestra la tabla de iteraciones lógicas
        public void PlotShape(PictureBox picCanvas, DataGridView dgv)
        {
            Bresenham_Line_Logical(x_i, y_i, x_f, y_f, picCanvas, dgv);
        }

        public void CloseForm(Form ObjForm)
        {
            ObjForm.Close();
        }
    }
}
