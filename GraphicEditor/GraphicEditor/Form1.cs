using System;
using System.Windows.Forms;
using System.Drawing;

namespace GraphicEditor
{
    public partial class Form1 : Form
    {
        int cnt = 0;
        Point[][] p;
        bool isMouseDown;
        int lineCatchIndex;
        bool isPointFocused;
        int pointCatchIndex;
        string mode;
        bool clearAll;

        public Form1()
        {
            //this.DoubleBuffered = true;
            mode = "Line";
            pointCatchIndex = -1;
            lineCatchIndex = -1;
            InitializeComponent();
            p = new Point[100][];
            for(int i = 0; i<100; i++)
            {
                p[i] = new Point[2];  
            }
            addBtn.Enabled = false;

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            addBtn.Enabled = false;
            editBtn.Enabled = true;
            deleteBtn.Enabled = true;
            mode = "Line";
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            addBtn.Enabled = true;
            editBtn.Enabled = false;
            deleteBtn.Enabled = true;
            mode = "Edit";
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            addBtn.Enabled = true;
            editBtn.Enabled = true;
            deleteBtn.Enabled = false;
            mode = "Delete";
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if (mode == "Line")
            {
                p[cnt][1].X = e.X;
                p[cnt][1].Y = e.Y;
                cnt++;
            }
            if (mode == "Edit")
            {
                p[lineCatchIndex][pointCatchIndex].X = e.X;
                p[lineCatchIndex][pointCatchIndex].Y = e.Y;
            }
            if (mode == "Delete")
            {
                p[lineCatchIndex][pointCatchIndex].X = 0;
                p[lineCatchIndex][pointCatchIndex].Y = 0;
            }
            
            mode = "Line";
            panel1.Invalidate();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            if(mode == "Line")
            {
                p[cnt][0].X = e.X;
                p[cnt][0].Y = e.Y;
            }
            if (mode == "Edit")
            {
                p[lineCatchIndex][pointCatchIndex].X = e.X;
                p[lineCatchIndex][pointCatchIndex].Y = e.Y;
            }

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                p[cnt][1].X = e.X;
                p[cnt][1].Y = e.Y;
                if (mode == "Line")
                {
                    p[cnt][1].X = e.X;
                    p[cnt][1].Y = e.Y;
                }
                if (mode == "Edit" && editBtn.Enabled == false)
                {
                    p[lineCatchIndex][pointCatchIndex].X = e.X;
                    p[lineCatchIndex][pointCatchIndex].Y = e.Y;
                }
            }
            else
            {
                if (editBtn.Enabled == false)
                {
                    mode = "Line";
                    isPointFocused = false;
                    pointCatchIndex = -1;
                    lineCatchIndex = -1;
                    for (int i = 0; i < cnt; i++)
                    {
                        if (Math.Abs(p[i][0].X - e.X) < 5 && Math.Abs(p[i][0].Y - e.Y) < 5)
                        {
                            isPointFocused = true;
                            pointCatchIndex = 0;
                            lineCatchIndex = i;
                            mode = "Edit";
                        }
                        if (Math.Abs(p[i][1].X - e.X) < 5 && Math.Abs(p[i][1].Y - e.Y) < 5)
                        {
                            isPointFocused = true;
                            pointCatchIndex = 1;
                            lineCatchIndex = i;
                            mode = "Edit";
                        }
                    }
                }
            }
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if(isPointFocused && editBtn.Enabled == false)
            {
                g.DrawEllipse(new Pen(Color.Red), p[lineCatchIndex][pointCatchIndex].X - 5, p[lineCatchIndex][pointCatchIndex].Y - 5, 10, 10);
            }

            for (int i = 0; i < cnt; i++)
            {
                g.DrawLine(new Pen(Color.Black), p[i][0].X, p[i][0].Y, p[i][1].X, p[i][1].Y);
            }
            if(isMouseDown)
            {
                g.DrawLine(new Pen(Color.Black), p[cnt][0].X, p[cnt][0].Y, p[cnt][1].X, p[cnt][1].Y);
            }
            /*if(clearAll)
            {
                Rectangle rect = this.ClientRectangle;
                rect.X = 0;
                rect.Y = 0;
                rect.Width = panel1.Width;
                rect.Height = panel1.Height;
                e.Graphics.DrawRectangle(new Pen(Color.White), rect);
                g.DrawRectangle(new Pen(Color.White), 0, 0, panel1.Width, panel1.Height);
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
                clearAll = false;
            }*/
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            /*clearAll = true;
            panel1.Invalidate();*/
        }
    }
}
