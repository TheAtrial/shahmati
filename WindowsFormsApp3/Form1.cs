using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp3
{

    public partial class Form1 : Form
    {

        public void lfnei(List<Edge> E, List<Vertex> V, List<Neighbor> N, int cur)
        {
            N.Clear();
            int a, b, c;
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == cur || E[i].v2 == cur)
                {
                    if (E[i].v1 == cur)
                    {
                        a = E[i].v1;
                        b = E[i].v2;
                        c = E[i].r;
                        N.Add(new Neighbor(a, b, c));
                    }
                    else if (E[i].v2 == cur)
                    {
                        a = E[i].v2;
                        b = E[i].v1;
                        c = E[i].r;
                        N.Add(new Neighbor(a, b, c));
                    }
                }
            }
        }


        public int[] deija(List<Edge> E, List<Vertex> V, List<Neighbor> N, int start)
        {
            int curv = start;
            for (int i = 0; i < V.Count; i++)
            {
                V[i].zn = int.MaxValue;
                V[i].metka = false;
            }
            V[curv].zn = 0;
            int u;
            int d = 0;
            int[] P = new int[V.Count];
            for (int i = 0; i < V.Count; i++)
            {
                P[i] = int.MaxValue;
            }
            P[curv] = start;
            while (curv != -1)
            {
                lfnei(E, V, N, curv);
                for (int k = 0; k < N.Count; k++) // заполнение значений к вершинам
                {
                    u = N[k].v2;
                    if (!V[u].metka && N[k].r + V[curv].zn < V[u].zn)
                    {
                        V[u].zn = N[k].r + V[curv].zn;
                        P[u] = curv;
                    }
                }
                V[curv].metka = true;
                curv = min_vertex(V);
            }

            return P;

        }

        private int min_vertex(List<Vertex> V)
        {
            int res = -1,
                min = int.MaxValue;
            for (int i = 0; i != V.Count; ++i)
                if (!V[i].metka && V[i].zn < min)
                {
                    min = V[i].zn;
                    res = i;
                }
            return res;
        }


        DrawGraph G;
        List<Vertex> V;
        List<Edge> E;
        List<Neighbor> N;
        Random x = new Random();


        public Form1()
        {
            InitializeComponent();
            V = new List<Vertex>();
            G = new DrawGraph(sheet.Width, sheet.Height);
            E = new List<Edge>();
            N = new List<Neighbor>();
            sheet.Image = G.GetBitmap();
        }


        //кнопка - рассчет пути
        private void Shw_Click(object sender, EventArgs e)
        {
            try
            {
                
            int start = int.Parse(textBox2.Text),
            end = int.Parse(textBox1.Text);




            int fig = int.Parse(textBox3.Text);
            Figure figure = null;
                if (start < 0 || end < 0 || start > 64 || end > 64)
                {
                    MessageBox.Show("Ошибка: Некорректно введенное значение!");
                }
                else
                {
                    switch (fig)
                    {
                        case 1:
                            figure = new Slon(true);
                            break;
                        case 2:
                            figure = new Ladja();
                            break;
                        case 3:
                            figure = new Kvina();
                            break;
                        case 4:
                            figure = new Korolb();
                            break;
                        case 5:
                            figure = new Konb();
                            break;
                        case 6:
                            figure = new Peshka();
                            break;
                        default:
                            MessageBox.Show("Ошибка: Неверно указано значение фигуры!");
                            break;
                    }

                    //---------------------------------------------------------------------
                    //Figure figure = new Slon(true); // <<<<-------------------------здесь менять
                    //---------------------------------------------------------------------

                    if (fig > 0 && fig < 7)
                    {
                        figure.CalcGraph();
                        List<Neighbor> _N = new List<Neighbor>();
                        start = figure.kletkiToVershini[start - 1];
                        end = figure.kletkiToVershini[end - 1];
                        if (start == -1 || end == -1)
                        {
                            MessageBox.Show("не могу добраться");
                            return;
                        }
                        int[] res = deija(figure.E, figure.V, _N, start);

                        List<int> path = new List<int>();
                        int curver = end;
                        bool nedostizhimo = false;

                        while (!nedostizhimo && curver != start)
                        {
                            path.Insert(0, curver);
                            curver = res[curver];
                            if (curver == int.MaxValue)
                                nedostizhimo = true;
                        }

                        if (!nedostizhimo)
                        {
                            path.Insert(0, start);
                            textBox4.Clear();
                            foreach (int ver in path)
                            {
                                Vertex vertex = figure.V[ver];
                                int kletka = Array.IndexOf(figure.kletkiToVershini, ver) + 1;
                                G.drawVertex2(vertex.x, vertex.y, kletka.ToString(), true);
                                textBox4.Text += kletka.ToString() + " ";
                            }
                        }
                        else
                            MessageBox.Show("не могу добраться");

                        sheet.Image = G.GetBitmap();
                    }
                }
        }
            catch (FormatException)
            {
                const string caption1 = "Ошибка: Неверное имя клетки";
                MessageBox.Show(caption1);
            }
        }



        private void VertexB_Click(object sender, EventArgs e)
        {
            int rand = 1;
            bool metka = false;
            G.clearSheet();
            G.drawALLGraph(V, E);
            sheet.Image = G.GetBitmap();
            // добавление вершины
            for (float i = 0; i < 8; i++)
                    for (float j = 0; j < 8; j++)
                    {
                        {
                            if (j % 2 != 0)
                            {
                                if ((i % 2) == 0)
                                {
                                    int a = (int)i;
                                    int b = (int)j;
                                    V.Add(new Vertex(a, b, metka, rand));
                                    G.drawVertex(a, b, V.Count.ToString(), metka);
                                }
                                if ((i % 2) != 0)
                                {
                                    int a = (int)i;
                                    int b = (int)j;
                                    V.Add(new Vertex(a, b, metka, rand));
                                    G.drawVertex1(a, b, V.Count.ToString(), metka);
                                }
                            }
                            if (j % 2 == 0)
                            {
                                if ((i % 2) != 0)
                                {
                                    int a = (int)i;
                                    int b = (int)j;
                                    V.Add(new Vertex(a, b, metka, rand));
                                    G.drawVertex(a, b, V.Count.ToString(), metka);
                                }
                                if ((i % 2) == 0)
                                {
                                    int a = (int)i;
                                    int b = (int)j;
                                    V.Add(new Vertex(a, b, metka, rand));
                                    G.drawVertex1(a, b, V.Count.ToString(), metka);
                                }
                            }
                        }
                    }
                
                sheet.Image = G.GetBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
