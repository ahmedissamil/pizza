using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pizz
{
    public partial class Form1 : Form
    {
        List<Circle> L1 = new List<Circle>();
        Bitmap off;
        float x, y;
        float xm, ym;
        float R;
        float R2;
        int u, d, key = 0;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Gold);
            for (int i = 0; i < L1.Count; i++)
            {
                for (float th = L1[i].StartTh; th < L1[i].EndTh; th += 1)
                {
                    R = (float)(th * Math.PI / 180);
                    x = (float)(L1[i].Rad * Math.Cos(R) + L1[i].XC);
                    y = (float)(L1[i].Rad * Math.Sin(R) + L1[i].YC);
                    g.FillEllipse(Brushes.Black, x, y, 10, 10);
                }
                R = (float)(L1[i].StartTh * Math.PI / 180);
                L1[i].x = (float)(L1[i].Rad * Math.Cos(R) + L1[i].XC);
                L1[i].y = (float)(L1[i].Rad * Math.Sin(R) + L1[i].YC);
                g.FillEllipse(Brushes.Yellow, L1[i].x, L1[i].y, 10, 10);
                g.DrawLine(Pens.Green, L1[i].x, L1[i].y, L1[i].XC, L1[i].YC);
                R = (float)(L1[i].EndTh * Math.PI / 180);
                L1[i].x2 = (float)(L1[i].Rad * Math.Cos(R) + L1[i].XC);
                L1[i].y2 = (float)(L1[i].Rad * Math.Sin(R) + L1[i].YC);
                g.FillEllipse(Brushes.Black, L1[i].x2, L1[i].y2, 10, 10);
                g.DrawLine(Pens.Green, L1[i].x2, L1[i].y2, L1[i].XC, L1[i].YC);
            }
            if (key != 0)
            {
                R2 = (float)(22.5 * Math.PI / 180);
                R = (float)(L1[u].StartTh * Math.PI / 180);
                x = (float)(300 * Math.Cos(R + R2) + 600);
                y = (float)(300 * Math.Sin(R + R2) + 400);
                g.DrawLine(Pens.Black, x, y, 600, 400);
            }
        }
        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            int sa = 0;
            for (int i = 0; i < 8; i++)
            {
                Circle pnn = new Circle();
                pnn.XC = 600;
                pnn.YC = 400;
                pnn.Rad = 150;
                pnn.StartTh = sa;
                pnn.EndTh = sa + 45;
                L1.Add(pnn);
                sa += 45;
            }
        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                xm = (float)(5 * Math.Cos(R + R2) + L1[u].XC);
                ym = (float)(5 * Math.Sin(R + R2) + L1[u].YC);
                L1[u].XC = xm;
                L1[u].YC = ym;
            }
            if (e.KeyCode == Keys.Up)
            {
                key = 1;
                u++;
                if (u > 7)
                {
                    u = 0;
                }
                if (u < 0)
                {
                    u = 7;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                key = 1;
                u--;
                if (u > 7)
                {
                    u = 0;
                }
                if (u < 0)
                {
                    u = 7;
                }
            }
            DrawDubb(this.CreateGraphics());
        }
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}