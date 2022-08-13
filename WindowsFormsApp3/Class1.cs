using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{

    public class Vertex
    {
        public int x, y;
        public bool metka;
        public int zn;

        public Vertex(int x, int y, bool metka, int zn)
        {
            this.x = x;
            this.y = y;
            this.metka = metka;
            this.zn = zn;
        }

        public static bool operator ==(Vertex v1, Vertex v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.metka == v2.metka && v1.zn == v2.zn;
        }

        public static bool operator !=(Vertex v1, Vertex v2)
        {
            return !(v1 == v2);
        }
    }

    public class Edge
    {
        public int v1, v2, r;

        public Edge(int v1, int v2, int r)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.r = r;
        }

        public static bool operator ==(Edge e1, Edge e2)
        {
            return (e1.v1 == e2.v1 && e1.v2 == e2.v2 ||
                   e1.v1 == e2.v2 && e1.v2 == e2.v1) &&
                   e1.r == e2.r;
        }

        public static bool operator !=(Edge e1, Edge e2)
        {
            return !(e1 == e2);
        }

    }

    public class Neighbor
    {
        public int v1, v2, r;
        public Neighbor(int v1, int v2, int r)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.r = r;
        }
    }


    class DrawGraph
    {
        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 12; //радиус окружности вершины

        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.DarkOliveGreen);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.Blue);
            darkGoldPen.EndCap = LineCap.ArrowAnchor;
            darkGoldPen.Width = 8;

            fo = new Font("Arial", 12);
            br = Brushes.White;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public void drawVertex(int x, int y, string number, bool metka)
        {
            gr.FillRectangle(new SolidBrush(Color.Tomato), x *25 , y *25 , 25, 25);
            point = new PointF(x*25, y*25);
            gr.DrawString(number, fo, br, point);
        }

        public void drawVertex1(int x, int y, string number, bool metka)
        {
            gr.FillRectangle(new SolidBrush(Color.Black), x* 25, y*25, 25, 25);
            point = new PointF(x*25, y*25);
            gr.DrawString(number, fo, br, point);
        }
        public void drawVertex2(int x, int y, string number, bool metka)
        {
            gr.FillRectangle(new SolidBrush(Color.Aqua), x * 25, y * 25, 25, 25);
            point = new PointF(x * 25, y * 25);
            gr.DrawString(number, fo, Brushes.Black, point);
        }

        internal void drawVertex(object x, object y, string v, bool metka)
        {
            throw new NotImplementedException();
        }
        internal void drawVertex1(object x, object y, string v, bool metka)
        {
            throw new NotImplementedException();
        }
        internal void drawVertex2(object x, object y, string v, bool metka)
        {
            throw new NotImplementedException();
        }

        public void drawSelectedVertex(int x, int y)
        {
            gr.FillRectangle(new SolidBrush(Color.Tomato), x * 25, y * 25, 25, 25);
        }

        public void drawEdge(Vertex V1, Vertex V2, Edge E, int numberE)
        {
            bool m = false;
            if (E.v1 == E.v2)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString(numberE.ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString(), m);
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x-5, V1.y-5, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                gr.DrawString(numberE.ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString(), m);
                drawVertex(V2.x, V2.y, (E.v2 + 1).ToString(), m);
            }
        }

        public void drawALLGraph(List<Vertex> V, List<Edge> E)
        {
            bool m = false;
            
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                drawVertex(V[i].x, V[i].y, (i + 1).ToString(), m);
            }
        }

     
    }
}
