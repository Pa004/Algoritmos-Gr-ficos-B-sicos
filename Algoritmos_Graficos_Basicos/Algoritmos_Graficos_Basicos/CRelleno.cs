using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Algoritmos_Graficos_Basicos
{
    internal class CRelleno
    {
        private float Side;
        private float numSides;
        private Graphics mGraph;
        private const float SF = 10;
        private Pen mPen;

        public CRelleno()
        {
            Side = 0.0f; numSides = 0.0f;
        }
        public void ReadData(TextBox txtNumSides, TextBox txtSide)
        {
            try
            {
                numSides = float.Parse(txtNumSides.Text);
                Side = float.Parse(txtSide.Text);
            }
            catch
            {
                MessageBox.Show("Ingreso no valido....", "Mensaje de error");
            }
        }

        public void InitializeData(TextBox txtNumSides, TextBox txtSide, PictureBox picCanvas, DataGridView dgv = null)
        {
            Side = 0.0f; numSides = 0.0f;
            txtNumSides.Text = ""; txtSide.Text = "";
            txtNumSides.Focus();
            picCanvas.Image = null;
            picCanvas.Refresh();
            if (dgv != null)
            {
                dgv.Rows.Clear();
                dgv.Columns.Clear();
            }
        }

        public void PlotShape(TextBox txtNumSides, PictureBox picCanvas)
        {
            float centerX = picCanvas.Width / 2f;
            float centerY = picCanvas.Height / 2f;
            numSides = float.Parse(txtNumSides.Text);
            int n = (int)numSides;
            PointF[] hexPoints = new PointF[n];
            float startAngle = 0f;

            for (int i = 0; i < n; i++)
            {
                float angleDeg = startAngle + i * (360f / n);
                float angleRad = (float)(Math.PI / 180f * angleDeg);
                float x = centerX + Side * (float)Math.Cos(angleRad) * SF;
                float y = centerY + Side * (float)Math.Sin(angleRad) * SF;
                hexPoints[i] = new PointF(x, y);
            }

            // Dibuja sobre un Bitmap para que FloodFill funcione correctamente
            Bitmap bmp = new Bitmap(picCanvas.Width, picCanvas.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(picCanvas.BackColor);
                using (Pen pen = new Pen(Color.Crimson, 3))
                {
                    g.DrawPolygon(pen, hexPoints);
                }
            }
            picCanvas.Image = bmp;
        }

        // Flood Fill progresivo sobre el Bitmap del PictureBox y muestra los puntos en una tabla
        public void FloodFill(PictureBox picCanvas, int x, int y, Color fillColor, DataGridView dgv = null)
        {
            if (picCanvas.Image == null)
                return;

            Bitmap bmp = (Bitmap)picCanvas.Image;
            Color targetColor = bmp.GetPixel(x, y);
            if (targetColor.ToArgb() == fillColor.ToArgb() || targetColor.ToArgb() == Color.Crimson.ToArgb())
                return;

            // Inicializa la tabla si se proporciona
            if (dgv != null)
            {
                dgv.Invoke((MethodInvoker)delegate
                {
                    dgv.Rows.Clear();
                    dgv.Columns.Clear();
                    dgv.Columns.Add("Iteracion", "Iteración");
                    dgv.Columns.Add("X", "X");
                    dgv.Columns.Add("Y", "Y");
                });
            }

            Queue<Point> pixels = new Queue<Point>();
            pixels.Enqueue(new Point(x, y));
            int iter = 0;

            while (pixels.Count > 0)
            {
                Point pt = pixels.Dequeue();
                if (pt.X < 0 || pt.X >= bmp.Width || pt.Y < 0 || pt.Y >= bmp.Height)
                    continue;

                Color currentColor = bmp.GetPixel(pt.X, pt.Y);
                if (currentColor.ToArgb() != targetColor.ToArgb())
                    continue;

                bmp.SetPixel(pt.X, pt.Y, fillColor);

                // Visualiza el proceso pixel a pixel
                picCanvas.Image = bmp;
                picCanvas.Refresh();
                Application.DoEvents();
                Thread.Sleep(1);

                // Añade el punto a la tabla
                if (dgv != null)
                {
                    int iteracion = iter;
                    int px = pt.X;
                    int py = pt.Y;
                    dgv.Invoke((MethodInvoker)delegate
                    {
                        dgv.Rows.Add(iteracion, px, py);
                        dgv.FirstDisplayedScrollingRowIndex = dgv.RowCount - 1;
                    });
                }
                iter++;

                pixels.Enqueue(new Point(pt.X + 1, pt.Y));
                pixels.Enqueue(new Point(pt.X - 1, pt.Y));
                pixels.Enqueue(new Point(pt.X, pt.Y + 1));
                pixels.Enqueue(new Point(pt.X, pt.Y - 1));
            }
        }

        // Método para asociar el evento de click al PictureBox
        public void AttachFloodFillHandler(PictureBox picCanvas, Color fillColor, DataGridView dgv = null)
        {
            // Evita múltiples suscripciones
            picCanvas.MouseClick -= PicCanvas_MouseClick;
            picCanvas.MouseClick += PicCanvas_MouseClick;

            void PicCanvas_MouseClick(object sender, MouseEventArgs e)
            {
                FloodFill(picCanvas, e.X, e.Y, fillColor, dgv);
            }
        }

        public void CloseForm(Form ObjForm)
        {
            ObjForm.Close();
        }
    }
}
