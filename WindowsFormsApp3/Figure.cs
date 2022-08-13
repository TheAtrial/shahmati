using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class Figure
    {
        public int[] kletkiToVershini;
        public const int side = 8;
        public List<Vertex> V;
        public List<Edge> E;
        public List<Neighbor> N;

        public Figure() {
            this.V = new List<Vertex>();
            this.E = new List<Edge>();
            this.N = new List<Neighbor>();
            kletkiToVershini = Enumerable.Repeat(-1, 64).ToArray();
        }

        public virtual void CalcGraph(){}
        public virtual bool CanReach(Vertex v1, Vertex v2){return true;}
        public bool estbInE(Edge x) {
            foreach (Edge edge in E)
                if (edge == x)
                    return true;
            return false;
        }
    }

    public class Slon: Figure
    {
        bool red;

        public Slon(bool red) {
            this.red = red;
        }

        public override bool CanReach(Vertex v1, Vertex v2) {
            return Math.Abs(v1.x - v2.x) == Math.Abs(v1.y - v2.y);
        }

        public override void CalcGraph(){
            
            for (int i = 0, count = 0; i != side * side; i+=2, ++count) {
                bool offset = Convert.ToBoolean(i / 8 % 2);
                int cell = i + Convert.ToInt32(red ? !offset : offset);
                kletkiToVershini[cell] = count;
                V.Add(new Vertex(cell / side, cell % side, false, Int32.MaxValue));
            }

            for (int i = 0; i != V.Count; ++i) 
                for (int j = 0; j != V.Count; ++j)
                    if (i != j)
                    {
                        Edge edge = new Edge(j, i, 1);
                        if (CanReach(V[i], V[j]) && !estbInE(edge))
                            E.Add(new Edge(i, j, 1));       
                    }
        }
    
    }

    public class Ladja : Figure //perfect inglish naming
    {
        public override bool CanReach(Vertex v1, Vertex v2)
        {
            return v1.x == v2.x || v1.y == v2.y;
        }

        public override void CalcGraph()
        {

            for (int i = 0; i != side * side; ++i)
            {
                kletkiToVershini[i] = i;
                V.Add(new Vertex(i / side, i % side, false, Int32.MaxValue));
            }

            for (int i = 0; i != V.Count; ++i)
                for (int j = 0; j != V.Count; ++j)
                    if (i != j)
                    {
                        Edge edge = new Edge(j, i, 1);
                        if (CanReach(V[i], V[j]) && !estbInE(edge))
                            E.Add(new Edge(i, j, 1));
                    }
        }
    }

    public class Kvina : Figure 
    {
        public override bool CanReach(Vertex v1, Vertex v2)
        {
            return v1.x == v2.x || 
                v1.y == v2.y || 
                Math.Abs(v1.x - v2.x) == Math.Abs(v1.y - v2.y);
        }

        public override void CalcGraph()
        {

            for (int i = 0; i != side * side; ++i)
            {
                kletkiToVershini[i] = i;
                V.Add(new Vertex(i / side, i % side, false, Int32.MaxValue));
            }

            for (int i = 0; i != V.Count; ++i)
                for (int j = 0; j != V.Count; ++j)
                    if (i != j)
                    {
                        Edge edge = new Edge(j, i, 1);
                        if (CanReach(V[i], V[j]) && !estbInE(edge))
                            E.Add(new Edge(i, j, 1));
                    }
        }
    }

    public class Korolb : Figure 
    {
        public override bool CanReach(Vertex v1, Vertex v2)
        {
            return Math.Abs(v1.x - v2.x) <= 1 && Math.Abs(v1.y - v2.y) <= 1;
        }

        public override void CalcGraph()
        {

            for (int i = 0; i != side * side; ++i)
            {
                kletkiToVershini[i] = i;
                V.Add(new Vertex(i / side, i % side, false, Int32.MaxValue));
            }

            for (int i = 0; i != V.Count; ++i)
                for (int j = 0; j != V.Count; ++j)
                    if (i != j)
                    {
                        Edge edge = new Edge(j, i, 1);
                        if (CanReach(V[i], V[j]) && !estbInE(edge))
                            E.Add(new Edge(i, j, 1));
                    }
        }
    }


    public class Konb : Figure 
    {
        public override bool CanReach(Vertex v1, Vertex v2)
        {
            int dx = Math.Abs(v1.x - v2.x),
                dy = Math.Abs(v1.y - v2.y);
            return dx == 1 && dy == 2 || dx == 2 && dy == 1;
        }

        public override void CalcGraph()
        {

            for (int i = 0; i != side * side; ++i)
            {
                kletkiToVershini[i] = i;
                V.Add(new Vertex(i / side, i % side, false, Int32.MaxValue));
            }

            for (int i = 0; i != V.Count; ++i)
                for (int j = 0; j != V.Count; ++j)
                    if (i != j)
                    {
                        Edge edge = new Edge(j, i, 1);
                        if (CanReach(V[i], V[j]) && !estbInE(edge))
                            E.Add(new Edge(i, j, 1));
                    }
        }
    }

    public class Peshka : Figure 
    {
        public override bool CanReach(Vertex v1, Vertex v2)
        {
            return v1.x == v2.x && Math.Abs(v1.y - v2.y) == 1;
        }

        public override void CalcGraph()
        {

            for (int i = 0; i != side * side; ++i)
            {
                kletkiToVershini[i] = i;
                V.Add(new Vertex(i / side, i % side, false, Int32.MaxValue));
            }

            for (int i = 0; i != V.Count; ++i)
                for (int j = 0; j != V.Count; ++j)
                    if (i != j)
                    {
                        Edge edge = new Edge(j, i, 1);
                        if (CanReach(V[i], V[j]) && !estbInE(edge))
                            E.Add(new Edge(i, j, 1));
                    }
        }
    }
}
