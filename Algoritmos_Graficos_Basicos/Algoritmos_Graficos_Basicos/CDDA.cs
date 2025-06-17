using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Algoritmos_Graficos_Basicos
{
    internal class CDDA
    {
        private float x_i;
        private float y_i;
        private float x_f;
        private float y_f;
        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        public CDDA()
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

        // DDA con retardo y llenado de tabla usando valores lógicos
        private void DDA_Line_Logical(float xi, float yi, float xf, float yf, PictureBox picCanvas, DataGridView dgv)
        {
            float dx = xf - xi;
            float dy = yf - yi;
            float steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            float xInc = dx / steps;
            float yInc = dy / steps;
            float x = xi;
            float y = yi;

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
            mPen = new Pen(Color.Crimson, 1);

            // Variables para el punto anterior
            float prevDrawX = centerX + x * SF;
            float prevDrawY = centerY - y * SF;

            for (int i = 0; i <= steps; i++)
            {
                float drawX = centerX + x * SF;
                float drawY = centerY - y * SF;

                // Dibuja línea desde el punto anterior al actual (excepto en la primera iteración)
                if (i > 0)
                {
                    mGraph.DrawLine(mPen, prevDrawX, prevDrawY, drawX, drawY);
                }
                // Opcional: también puedes marcar el punto
                mGraph.DrawRectangle(mPen, (int)Math.Round(drawX), (int)Math.Round(drawY), 1, 1);

                // Actualiza la tabla
                if (dgv != null)
                {
                    dgv.Invoke((MethodInvoker)delegate
                    {
                        dgv.Rows.Add(i, Math.Round(x, 2), Math.Round(y, 2));
                        dgv.FirstDisplayedScrollingRowIndex = dgv.RowCount - 1;
                    });
                }

                Application.DoEvents();
                Thread.Sleep(200);

                // Actualiza el punto anterior
                prevDrawX = drawX;
                prevDrawY = drawY;

                x += xInc;
                y += yInc;
            }
        }

        // Traza la línea y muestra la tabla de iteraciones lógicas
        public void PlotShape(PictureBox picCanvas, DataGridView dgv)
        {
            DDA_Line_Logical(x_i, y_i, x_f, y_f, picCanvas, dgv);
        }

        public void CloseForm(Form ObjForm)
        {
            ObjForm.Close();
        }
    }
}
