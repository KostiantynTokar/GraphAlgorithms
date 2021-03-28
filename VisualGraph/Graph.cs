using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace GraphAlgorithms
{
    class RGBColor
    {
        public byte R;
        public byte G;
        public byte B;

        public RGBColor() { R = 0; G = 0; B = 0; }
        public RGBColor(byte r, byte g, byte b) { R = r; G = g; B = b; }
        public RGBColor(RGBColor other) { R = other.R; G = other.G; B = other.B; }

        public static bool operator ==(RGBColor a, RGBColor b)
        {
            return (a.R == b.R && a.G == b.G && a.B == b.B);
        }

        public static bool operator !=(RGBColor a, RGBColor b)
        {
            return !(a == b);
        }
    }

    class Vertex
    {
        protected int x;
        protected int y;
        protected RGBColor color;
        protected string name;
        protected string info;

        public Vertex(int x_, int y_)
        {
            x = x_;
            y = y_;
            color=new RGBColor();
            name = "";
            info = "";
        }
        public Vertex(int x_, int y_, RGBColor c)
        {
            x = x_;
            y = y_;
            color = c;
            name = "";
            info = "";
        }
        public Vertex(int x_, int y_, string name_)
        {
            x = x_;
            y = y_;
            color = new RGBColor();
            name = name_;
            info = "";
        }
        public Vertex(int x_, int y_, RGBColor c, string name_)
        {
            x = x_;
            y = y_;
            color = c;
            name = name_;
            info = "";
        }
        public Vertex(Vertex other)
        {
            x = other.x;
            y = other.y;
            color = new RGBColor(other.color);
            name = other.name.ToString();
            info = other.info.ToString();
        }

        public int X
        {
            get { return x; }
            //set { x = value; }
        }

        public int Y
        {
            get { return y; }
            //set { y = value; }
        }

        public RGBColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        public static bool operator ==(Vertex v1, Vertex v2)
        {
            //if (v1 == null || v2 == null) return false;
            return (v1.x == v2.x && v1.y == v2.y);
        }

        public static bool operator !=(Vertex v1, Vertex v2)
        {
            return (v1.x != v2.x || v1.y != v2.y);
        }

        public override bool Equals(object o)
        {
            return (X == ((Vertex)o).X && Y == ((Vertex)o).Y);
        }
    }

    class Line: IComparable
    {
        protected int v1;
        protected int v2;
        protected int weight;
        protected string info;
        protected static int stdWeight = 1;
        protected RGBColor color;

        public Line(int ver1, int ver2)
        {
            v1 = ver1;
            v2 = ver2;
            weight = stdWeight;
            info = "";
            color = new RGBColor();
        }
        public Line(int ver1, int ver2, int w)
        {
            v1 = ver1;
            v2 = ver2;
            weight = w;
            info = "";
            color = new RGBColor();
        }
        public Line(int ver1, int ver2, RGBColor c)
        {
            v1 = ver1;
            v2 = ver2;
            weight = stdWeight;
            info = "";
            color = new RGBColor(c);
        }
        public Line(int ver1, int ver2, int w, RGBColor c)
        {
            v1 = ver1;
            v2 = ver2;
            weight = w;
            info = "";
            color = new RGBColor(c);
        }
        public Line(Line other)
        {
            v1 = other.v1;
            v2 = other.v2;
            weight = other.weight;
            color = new RGBColor(other.color);
            info = other.info;
        }

        public int V1
        {
            get { return v1; }
        }

        public int V2
        {
            get { return v2; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        public static int StdWeight
        {
            get { return Line.stdWeight; }
            set { Line.stdWeight = value; }
        }

        public RGBColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Line otherLine = obj as Line;
            if (otherLine != null) 
                return this.Weight.CompareTo(otherLine.Weight);
            else
                throw new ArgumentException("Object is not a Line");
        }
    }

    class BFSInfo
    {
        protected List<int> distance = new List<int>();
        protected List<int> parent = new List<int>();
        protected int start;

        public int Start
        {
            get;
            set;
        }

        public List<int> Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        
        public List<int> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public override string ToString()
        {
            string res = "Из " + start.ToString() + '\n';
            res += "d\tp\n";
            for (int i = 0; i < distance.Count; ++i)
            {
                res += (distance[i] == int.MaxValue ? "inf" : distance[i].ToString()) + '\t';
                res += (parent[i] == int.MaxValue ? "inf" : parent[i].ToString());
                res += '\n';
            }
            return res;
        }
    }
    class DFSInfo
    {
        protected List<int> distance = new List<int>();
        protected List<int> finish = new List<int>();
        protected List<int> parent = new List<int>();


        public List<int> Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public List<int> Finish
        {
            get { return finish; }
            set { finish = value; }
        }

        public List<int> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public override string ToString()
        {
            string res = "p\td\tf\n";
            for (int i = 0; i < distance.Count; ++i)
            {
                res += (parent[i] == int.MaxValue ? "inf" : parent[i].ToString()) + '\t';
                res += (distance[i] == int.MaxValue ? "inf" : distance[i].ToString()) + '\t';
                res += (finish[i] == int.MaxValue ? "inf" : finish[i].ToString());
                res += '\n';
            }
            return res;
        }
    }
    class MSTKruskalInfo
    {
        //Список смежных вершин в минимальном остовном дереве
        public List<SortedSet<int>> MST = new List<SortedSet<int>>();

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < MST.Count; ++i)
            {
                res += i.ToString() + " -> ";
                foreach (int j in MST[i])
                {
                    res += j.ToString()+", ";
                }
                res += '\n';
            }
            return res;
        }
    }
    class MSTPrimInfo
    {
        private int start;
        private List<int> keys;
        private List<int> parents;

        public MSTPrimInfo()
        {
            keys = new List<int>();
            parents = new List<int>();
        }

        public List<int> Parents
        {
            get { return parents; }
            set { parents = value; }
        }

        public List<int> Keys
        {
            get { return keys; }
            set { keys = value; }
        }

        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        public override string ToString()
        {
            string res = "Из " + start.ToString() + '\n';
            res += "k\tp\n";
            for (int i = 0; i < keys.Count; ++i)
            {
                res += (keys[i] == int.MaxValue ? "inf" : keys[i].ToString()) + '\t';
                res += (parents[i] == int.MaxValue ? "inf" : parents[i].ToString());
                res += '\n';
            }
            return res;
        }
    }
    abstract class ShortcutInfo
    {
        protected int start;
        protected List<int> distance = new List<int>();
        protected List<int> parent = new List<int>();

        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        public List<int> Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public List<int> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        override public string ToString()
        {
            string res = "Из " + start.ToString() + '\n';
            res += "d\tp\n";
            for (int i = 0; i < distance.Count; ++i)
            {
                res += (distance[i] == int.MaxValue ? "inf" : distance[i].ToString()) + '\t';
                res += (parent[i] == int.MaxValue ? "inf" : parent[i].ToString());
                res += '\n';
            }
            return res;
        }
    }
    class BellmanFordInfo: ShortcutInfo
    {
        protected bool withCycle = false;

        public bool WithCycle
        {
            get { return withCycle; }
            set { withCycle = value; }
        }

        public override string ToString()
        {
            if (withCycle) return "Был найден отрицательный цикл.";
            return base.ToString();
        }
    }
    class DijkstraInfo : ShortcutInfo
    {

    }
    class FloydWarshallInfo
    {
        //Матрица, в которой на месте (i,j) сохранено расстояние между i-ой и j-ой вершинами; int.MaxValue, если пути нет
        private List<List<int>> distanceMatr = new List<List<int>>();

        public List<List<int>> DistanceMatr
        {
            get { return distanceMatr; }
            set { distanceMatr = value; }
        }

        public override string ToString()
        {
            string res = "";
            foreach (List<int> li in distanceMatr)
            {
                foreach (int i in li)
                {
                    res += (i==int.MaxValue ? "inf" : i.ToString()) + '\t';
                }
                res += '\n';
            }
            return res;
        }
    }
    class JohnsonInfo
    {
        protected bool withCycle = false;
        private List<List<int>> distanceMatr = new List<List<int>>();

        public bool WithCycle
        {
            get { return withCycle; }
            set { withCycle = value; }
        }
        public List<List<int>> DistanceMatr
        {
            get { return distanceMatr; }
            set { distanceMatr = value; }
        }

        public override string ToString()
        {
            if (withCycle) return "Был найден отрицательный цикл.";
            string res="";
            foreach (List<int> li in distanceMatr)
            {
                foreach (int i in li)
                {
                    res += (i==int.MaxValue ? "inf" : i.ToString()) + '\t';
                }
                res += '\n';
            }
            return res;
        }
    }
    abstract class MaxFlowInfo
    {
        protected int source;
        protected int sink;
        protected List<List<int>> flow = new List<List<int>>();
        protected int maxFlow = 0;

        public int MaxFlow
        {
            get { return maxFlow; }
            set { maxFlow = value; }
        }

        public int Source
        {
            get { return source; }
            set { source = value; }
        }

        public int Sink
        {
            get { return sink; }
            set { sink = value; }
        }

        public List<List<int>> Flow
        {
            get { return flow; }
            set { flow = value; }
        }

        public override string ToString()
        {
            string res = "Из " + source.ToString() + " в " + sink.ToString() + " значение максимального потока " + maxFlow.ToString();
            res += '\n';
            foreach (List<int> li in flow)
            {
                foreach (int i in li)
                {
                    res += (i == int.MaxValue ? "inf" : i.ToString()) + '\t';
                }
                res += '\n';
            }
            return res;
        }
    }
    class FordFalkersonInfo: MaxFlowInfo
    {
        
    }
    class EdmondsKarpInfo : MaxFlowInfo
    {

    }

    class Graph: ICloneable
    {
        protected List<Vertex> m;
        protected List<List<Line>> l;
        protected bool isOr;
        protected int count; //Счетчик для имен вершин
        protected RGBColor stdVertexColor;
        protected RGBColor stdLineColor;

        public RGBColor StdLineColor
        {
            get { return stdLineColor; }
            set { stdLineColor = value; }
        }

        public RGBColor StdVertexColor
        {
            get { return stdVertexColor; }
            set { stdVertexColor = value; }
        }

        public Graph(bool isOr_)
        {
            m = new List<Vertex>();
            l = new List<List<Line>>();
            count = 0;
            isOr = isOr_;
            stdVertexColor = new RGBColor(255, 0, 0);
            stdLineColor = new RGBColor(255, 0, 0);
        }
        public Graph(Graph other)
        {
            m = new List<Vertex>();
            l = new List<List<Line>>();
            foreach (Vertex v in other.m)
            {
                m.Add(new Vertex(v));
            }
            foreach (List<Line> la in other.l)
            {
                l.Add(new List<Line>());
                foreach (Line a in la)
                {
                    l[l.Count - 1].Add(a != null ? new Line(a) : null);
                }
            }
            count = other.count;
            isOr = other.isOr;
            stdVertexColor = new RGBColor(other.stdVertexColor);
            stdLineColor = new RGBColor(other.stdLineColor);
        }

        public bool IsOr
        {
            get { return isOr; }
            set { isOr = value; }
        }

        public void SetVertexColor(RGBColor c)
        {
            foreach (Vertex v in m)
            {
                v.Color = new RGBColor(c);
            }
        }

        public void SetLineColor(RGBColor c)
        {
            foreach (List<Line> la in l)
            {
                foreach (Line a in la)
                {
                    if (a != null) a.Color = new RGBColor(c);
                }
            }
        }

        public Vertex getVertex(int index)
        {
            return m[index];
        }

        public SortedSet<int> getAdjacent(int index)
        {
            SortedSet<int> res = new SortedSet<int>();
            for (int i=0; i<l[index].Count; ++i)
            {
                if (l[index][i] != null)
                    res.Add(i);
            }
            if (!IsOr)
            {
                for (int i = 0; i < l.Count; ++i)
                {
                    if (i == index) continue;
                    if (l[i][index] != null)
                        res.Add(i);
                }
            }
            return res;
        }

        public List<Line> getAdjacentLine(int index)
        {
            List<Line> res = new List<Line>();
            for (int i = 0; i < l[index].Count; ++i)
            {
                res.Add(l[index][i]);
            }
            if (!IsOr)
            {
                for (int i = 0; i < l.Count; ++i)
                {
                    if (l[i][index] != null)
                        res[i] = l[i][index];
                }
            }
            return res;
        }

        public List<Line> getIncomingLine(int index)
        {
            if (!IsOr) return getAdjacentLine(index);
            List<Line> res = new List<Line>();
            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                res.Add(l[i][index]);
            }
            return res;
        }

        public int getNumOfVertex()
        {
            return m.Count();
        }

        public int getNumOfLine()
        {
            int res = 0;
            foreach(List<Line> tmp in l)
            {
                foreach (Line a in tmp)
                {
                    if (a != null)
                        res++;
                }
            }
            //if (!IsOr) res /= 2;
            return res;
        }

        public bool addVertex(Vertex v)
        {
            if (m.Contains(v)) return false;
            m.Add(v);
            if (v.Name.Count() == 0) v.Name = (++count).ToString();
            //Сохраняем матрицу w квадратной
            for (int i = 0; i < l.Count; ++i)
            {
                l[i].Add(null);
            }
            l.Add(new List<Line>());
            for (int i = 0; i < m.Count(); ++i)
            {
                l[l.Count() - 1].Add(null);
            }
            return true;
        }

        public bool addLine(Vertex v1, Vertex v2)
        {
            if (!m.Contains(v1)) { addVertex(v1); }
            if (!m.Contains(v2)) { addVertex(v2); }
            int v1pos = m.FindIndex(delegate(Vertex v) { return v == v1; });
            int v2pos = m.FindIndex(delegate(Vertex v) { return v == v2; });
            l[v1pos][v2pos] = new Line(v1pos, v2pos, stdLineColor);
            //if (!IsOr) l[v2pos][v1pos] = new Line(v2pos, v1pos, stdLineColor);
            return true;
        }

        public bool addLine(Vertex v1, Vertex v2, int weight)
        {
            if (!m.Contains(v1)) { addVertex(v1); }
            if (!m.Contains(v2)) { addVertex(v2); }
            int v1pos = m.FindIndex(delegate(Vertex v) { return v == v1; });
            int v2pos = m.FindIndex(delegate(Vertex v) { return v == v2; });
            l[v1pos][v2pos] = new Line(v1pos, v2pos, weight, stdLineColor);
            //if (!IsOr) l[v2pos][v1pos] = new Line(v2pos, v1pos, weight, stdLineColor);
            return true;
        }

        public bool addLine(int v1, int v2)
        {
            if (v1>m.Count() || v1<0 || v2>m.Count() || v2<0) return false;
            l[v1][v2] = new Line(v1, v2, stdLineColor);
            //if (!IsOr) l[v2][v1] = new Line(v2, v1, stdLineColor);
            return true;
        }

        public bool addLine(int v1, int v2, int weight)
        {
            if (v1 > m.Count() || v1 < 0 || v2 > m.Count() || v2 < 0) return false;
            l[v1][v2] = new Line(v1, v2, weight, stdLineColor);
            //if (!IsOr) l[v2][v1] = new Line(v2, v1, weight, stdLineColor);
            return true;
        }

        //Очистка дополнительной информации с вершин и ребер, установка стандартного цвета
        public void reset()
        {
            foreach (Vertex v in m)
            {
                v.Info = "";
                v.Color = StdVertexColor;
            }
            foreach (List<Line> la in l)
            {
                foreach (Line a in la)
                {
                    if (a != null) a.Info = "";
                }
            }
        }

        public object Clone()
        {
            return new Graph(this);
            //Graph res = new Graph(IsOr);
            //foreach (Vertex v in m)
            //{
            //    res.m.Add(new Vertex(v));
            //}
            //foreach (List<Line> la in l)
            //{
            //    res.l.Add(new List<Line>());
            //    foreach (Line a in la)
            //    {
            //        res.l[res.l.Count - 1].Add(a != null ? new Line(a) : null);
            //    }
            //}
            //res.count = count;
            //res.isOr = isOr;
            //res.stdVertexColor = new RGBColor(stdVertexColor);
            //res.stdLineColor = new RGBColor(stdLineColor);
            //return res;
        }


        public BFSInfo BFS(int s, out List<Graph> StepByStepSolution)
        {
            StepByStepSolution=new List<Graph>();
            Graph curStep = Clone() as Graph;
            if(s<0 || s>getNumOfVertex()) throw new ArgumentOutOfRangeException();
            BFSInfo res = new BFSInfo();
            res.Start = s;
            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                res.Distance.Add(int.MaxValue);
                res.Parent.Add(-1);
            }
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                if (i != s) { curStep.getVertex(i).Color = new RGBColor(255, 255, 255); }
                else curStep.getVertex(i).Color = new RGBColor(133, 133, 133);
            }
            curStep.getVertex(s).Info += "/0/" + curStep.getVertex(s).Name;
            StepByStepSolution.Add(curStep.Clone() as Graph);
            res.Distance[s] = 0;
            res.Parent[s] = s;

            Queue<int> q = new Queue<int>();
            q.Enqueue(s);
            int curVertex;
            SortedSet<int> adj = new SortedSet<int>();
            while (q.Count() != 0)
            {
                curVertex = q.Dequeue();
                adj = getAdjacent(curVertex);
                foreach (int i in adj)
                {
                    if (curStep.getVertex(i).Color==new RGBColor(255,255,255))
                    {
                        curStep.getVertex(i).Color = new RGBColor(133, 133, 133);
                        res.Distance[i] = res.Distance[curVertex] + 1;
                        res.Parent[i] = curVertex;
                        q.Enqueue(i);
                        curStep.getVertex(i).Info += "/" + res.Distance[i].ToString() + "/" + curStep.getVertex(res.Parent[i]).Name;
                        //StepByStepSolution.Add(curStep.Clone() as Graph);
                    }
                }
                StepByStepSolution.Add(curStep.Clone() as Graph);
                curStep.getVertex(curVertex).Color = new RGBColor(0, 0, 0);
                StepByStepSolution.Add(curStep.Clone() as Graph);
            }
            return res;
        }
        public BFSInfo BFS(int s)
        {
            List<Graph> tmp;
            return BFS(s, out tmp);
        }

        public DFSInfo DFS(out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = Clone() as Graph;
            DFSInfo res = new DFSInfo();
            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                res.Distance.Add(int.MaxValue);
                res.Finish.Add(int.MaxValue);
                res.Parent.Add(-1);
            }
            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                curStep.getVertex(i).Color = new RGBColor(255, 255, 255);
                res.Parent[i] = i;
            }
            int time = 0;
            StepByStepSolution.Add(curStep.Clone() as Graph);

            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                if (curStep.getVertex(i).Color == new RGBColor(255, 255, 255))
                    DFSVisit(ref time, i, res, curStep, StepByStepSolution);
            }
            return res;
        }
        private static void DFSVisit(ref int time, int u, DFSInfo l, Graph curStep, List<Graph> StepByStepSolution)
        {
            ++time;
            l.Distance[u] = time;
            curStep.getVertex(u).Info += "/" + curStep.getVertex(l.Parent[u]).Name + "/" + time.ToString();
            curStep.getVertex(u).Color = new RGBColor(133, 133, 133);
            StepByStepSolution.Add(curStep.Clone() as Graph);

            foreach (int v in curStep.getAdjacent(u))
            {
                if (curStep.getVertex(v).Color == new RGBColor(255, 255, 255))
                {
                    l.Parent[v] = u;
                    DFSVisit(ref time, v, l, curStep, StepByStepSolution);
                }
            }
            curStep.getVertex(u).Color = new RGBColor(0, 0, 0);
            ++time;
            l.Finish[u] = time;
            curStep.getVertex(u).Info += "/" + l.Finish[u].ToString();
            StepByStepSolution.Add(curStep.Clone() as Graph);
         }

        //Нахождение минимального остовного дерева
        public MSTKruskalInfo MSTKruskal(out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = Clone() as Graph;
            MSTKruskalInfo res = new MSTKruskalInfo();
            for (int i = 0; i < m.Count; ++i)
            {
                res.MST.Add(new SortedSet<int>());
            }

            curStep.SetVertexColor(curStep.StdVertexColor);
            curStep.SetLineColor(curStep.StdLineColor);
            RGBColor AVertexColor = new RGBColor(0, 0, 0);
            RGBColor NotAVertexColor = new RGBColor(255, 255, 255);
            RGBColor ALineColor = new RGBColor(255, 255, 0);

            for (int i = 0; i < m.Count; ++i)
            {
                curStep.getVertex(i).Color = new RGBColor(NotAVertexColor);
            }
            StepByStepSolution.Add(curStep.Clone() as Graph);

            List<SortedSet<int>> trees = new List<SortedSet<int>>();
            for (int i = 0; i < m.Count(); ++i)
            {
                trees.Add(new SortedSet<int>());
                trees[i].Add(i);
            }

            List<Line> sortedL=new List<Line>();
            for (int i = 0; i < l.Count(); ++i)
            {
                for (int j = 0; j < l[i].Count(); ++j)
                { 
                    if(l[i][j]!=null)
                        sortedL.Add(l[i][j]);
                }
            }
            sortedL.Sort();


            int u, v;
            List<int> tmp = new List<int>();
            foreach (Line a in sortedL)
            {
                u = a.V1;
                v = a.V2;
                if (!trees[u].Contains(v))
                {
                    res.MST[u].Add(v);
                    if (!IsOr) res.MST[v].Add(u);
                    //Объединяем деревья
                    foreach (int i in trees[v])
                    {
                        trees[u].Add(i);
                    }
                    foreach (int i in trees[u])
                    {
                        tmp.Add(i);
                    }
                    foreach (int i in tmp)
                    {
                        foreach (int j in tmp)
                        {
                            trees[i].Add(j);
                        }
                    }
                    tmp.Clear();
                    curStep.getVertex(u).Color = new RGBColor(AVertexColor);
                    curStep.getVertex(v).Color = new RGBColor(AVertexColor);
                    //curStep.getAdjacentLine(u)[v].Color = new RGBColor(ALineColor);
                    //if (!IsOr) curStep.getAdjacentLine(v)[u].Color = new RGBColor(ALineColor);
                    curStep.getAdjacentLine(u)[v].Color = new RGBColor(ALineColor);
                    StepByStepSolution.Add(curStep.Clone() as Graph);
                }
            }
            return res;
        }

        public MSTPrimInfo MSTPrim(int s, out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = Clone() as Graph;
            if (s < 0 || s > getNumOfVertex()) throw new ArgumentOutOfRangeException();

            curStep.SetVertexColor(curStep.StdVertexColor);
            curStep.SetLineColor(curStep.StdLineColor);
            RGBColor AVertexColor = new RGBColor(0, 0, 0);
            RGBColor NotAVertexColor = new RGBColor(255, 255, 255);
            RGBColor ALineColor = new RGBColor(255, 255, 0);
            RGBColor NotALineColor = new RGBColor(255, 0, 0);

            MSTPrimInfo res = new MSTPrimInfo();
            res.Start = s;
            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                res.Keys.Add(int.MaxValue);
                res.Parents.Add(-1);
            }
            res.Keys[s] = 0;
            curStep.SetVertexColor(new RGBColor(NotAVertexColor));
            curStep.getVertex(s).Color = new RGBColor(AVertexColor);
            curStep.getVertex(s).Info += "/" + res.Keys[s].ToString() + "/" + curStep.getVertex(s).Name;
            StepByStepSolution.Add(new Graph(curStep));

            //Первое - ключ, второе - номер вершины
            List<KeyValuePair<int, int>> Q = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                Q.Add(new KeyValuePair<int,int>(res.Keys[i], i));
            }
            Q.Sort(delegate(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
            {
                return x.Key.CompareTo(y.Key);
            });

            int u;
            while (Q.Count != 0)
            {
                u = Q[0].Value;
                Q.RemoveAt(0);
                foreach (int v in getAdjacent(u))
                {
                    if(Q.Contains(new KeyValuePair<int,int>(res.Keys[v],v)) && curStep.getAdjacentLine(u)[v].Weight<res.Keys[v])
                    {
                        //Если ключ не бесконечность, значит путь уже был отмечен, но уже нашелся путь короче; убираем более тяжелый
                        if (res.Keys[v] != int.MaxValue)
                        {
                            //curStep.getAdjacentLine(res.Parents[v])[v].Color = new RGBColor(NotALineColor);
                            //if (!IsOr) curStep.getAdjacentLine(v)[res.Parents[v]].Color = new RGBColor(NotALineColor);
                            curStep.getAdjacentLine(res.Parents[v])[v].Color = new RGBColor(NotALineColor);
                            curStep.getVertex(v).Info = "";
                        }
                        Q.Remove(new KeyValuePair<int,int>(res.Keys[v],v));
                        res.Parents[v] = u;
                        res.Keys[v] = getAdjacentLine(u)[v].Weight;
                        Q.Add(new KeyValuePair<int, int>(res.Keys[v], v));
                        Q.Sort(delegate(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
                        {
                            return x.Key.CompareTo(y.Key);
                        });
                        curStep.getVertex(v).Color = new RGBColor(AVertexColor);
                        curStep.getVertex(v).Info += "/" + res.Keys[v].ToString() + "/" + curStep.getVertex(u).Name;
                        curStep.getAdjacentLine(u)[v].Color = new RGBColor(ALineColor);
                        //if (!IsOr) curStep.getAdjacentLine(v)[u].Color = new RGBColor(ALineColor);
                        StepByStepSolution.Add(new Graph(curStep));
                    }
                }
            }
            return res;
        }


        //Поиск расстояний из одного источника
        private static void InitSingleSourse(Graph curStep, int s, ShortcutInfo si)
        {
            si.Start = s;
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                si.Distance.Add(int.MaxValue);
                si.Parent.Add(-1);
            }
            si.Distance[s] = 0;
        }
        private static void Relax(Graph curStep, Line a,
                                  ShortcutInfo si, List<Graph> StepByStepSolution, RGBColor RelaxingLineColor)
        {
            if (si.Distance[a.V1] == int.MaxValue) return;
            if (si.Distance[a.V2] > si.Distance[a.V1] + a.Weight)
            {
                si.Distance[a.V2] = si.Distance[a.V1] + a.Weight;
                si.Parent[a.V2] = a.V1;
                curStep.getVertex(a.V2).Info = "/" + curStep.getVertex(a.V1).Name + "/" + si.Distance[a.V2].ToString();
                a.Color = RelaxingLineColor;
                StepByStepSolution.Add(new Graph(curStep));
                a.Color = new RGBColor(curStep.StdLineColor);
            }
        }
        public BellmanFordInfo BellmanFord(int s, out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = Clone() as Graph;
            curStep.IsOr = true;
            if (s < 0 || s > getNumOfVertex()) throw new ArgumentOutOfRangeException();
            BellmanFordInfo res = new BellmanFordInfo();
            InitSingleSourse(curStep, s, res);

            curStep.SetVertexColor(curStep.StdVertexColor);
            curStep.SetLineColor(curStep.StdLineColor);
            RGBColor RelaxingLineColor = new RGBColor(255, 255, 0);
            RGBColor LineFromCycleColor = new RGBColor(255, 0, 255);

            for (int i = 1; i < curStep.getNumOfVertex() - 1; ++i)
            {
                for (int j = 0; j < curStep.getNumOfVertex(); ++j)
                {
                    foreach (Line a in curStep.getAdjacentLine(j))
                    {
                        if (a == null) continue;
                        Relax(curStep, a, res, StepByStepSolution, RelaxingLineColor);
                    }
                }
            }
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                foreach (Line a in curStep.getAdjacentLine(i))
                {
                    if (a == null) continue;
                    if (res.Distance[a.V1] == int.MaxValue) continue;
                    if (res.Distance[a.V2] > res.Distance[a.V1] + a.Weight)
                    {
                        res.WithCycle = true;
                        a.Color = LineFromCycleColor;
                        StepByStepSolution.Add(new Graph(curStep));
                    }
                }
            }
            StepByStepSolution.Add(new Graph(curStep));
            return res;
        }
        public BellmanFordInfo BellmanFord(int s)
        {
            List<Graph> tmp;
            return BellmanFord(s, out tmp);
        }
        

        public DijkstraInfo Dijkstra(int s, out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = Clone() as Graph;
            curStep.IsOr = true;
            if (s < 0 || s > getNumOfVertex()) throw new ArgumentOutOfRangeException();
            DijkstraInfo res = new DijkstraInfo();
            InitSingleSourse(curStep, s, res);

            curStep.SetVertexColor(curStep.StdVertexColor);
            curStep.SetLineColor(curStep.StdLineColor);
            RGBColor RelaxingLineColor = new RGBColor(255, 255, 0);
            RGBColor SVertexColor = new RGBColor(0, 0, 0);

            List<KeyValuePair<int, int>> Q = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                Q.Add(new KeyValuePair<int, int>(res.Distance[i], i));
            }
            Q.Sort(delegate(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
            {
                return x.Key.CompareTo(y.Key);
            });

            int u;
            while (Q.Count != 0)
            {
                u = Q[0].Value;
                Q.RemoveAt(0);

                curStep.getVertex(u).Color = SVertexColor;
                StepByStepSolution.Add(new Graph(curStep));

                foreach (Line a in curStep.getAdjacentLine(u))
                {
                    if (a == null) continue;
                    Relax(curStep, a, res, StepByStepSolution, RelaxingLineColor);
                }
                Q.Sort(delegate(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
                {
                    return x.Key.CompareTo(y.Key);
                });
            }
            return res;
        }
        public DijkstraInfo Dijkstra(int s)
        {
            List<Graph> tmp;
            return Dijkstra(s, out tmp);
        }

        //Поиск расстояний между каждой парой вершин
        
        public FloydWarshallInfo FloydWarshall(out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = new Graph(this);
            curStep.IsOr = true;
            FloydWarshallInfo res = new FloydWarshallInfo();

            curStep.SetVertexColor(curStep.StdVertexColor);
            curStep.SetLineColor(curStep.StdLineColor);
            RGBColor RelaxingVertexColor = new RGBColor(0, 0, 0);

            int n = m.Count;
            List<Line> ll;
            Line a;
            for (int i = 0; i < n; ++i)
            {
                res.DistanceMatr.Add(new List<int>());
                ll = curStep.getAdjacentLine(i);
                for (int j = 0; j < n; ++j)
                {
                    if (i == j) { res.DistanceMatr[i].Add(0); continue; }
                    a = ll[j];
                    if (a == null) { res.DistanceMatr[i].Add(int.MaxValue); continue; }
                    res.DistanceMatr[i].Add(a.Weight);
                }
            }
            List<List<int>> prevMatr = new List<List<int>>(res.DistanceMatr);
            for (int k = 0; k < n; ++k)
            {
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        if (prevMatr[i][k] == int.MaxValue || prevMatr[k][j] == int.MaxValue) continue;
                        if (prevMatr[i][k] + prevMatr[k][j] < res.DistanceMatr[i][j])
                        {
                            res.DistanceMatr[i][j] = prevMatr[i][k] + prevMatr[k][j];
                            curStep.getVertex(i).Info = "->" + curStep.getVertex(j).Name + "/" + res.DistanceMatr[i][j];
                            curStep.getVertex(i).Color = RelaxingVertexColor;
                            curStep.getVertex(j).Info = "<-" + curStep.getVertex(i).Name + "/" + res.DistanceMatr[i][j];
                            curStep.getVertex(j).Color = RelaxingVertexColor;
                            StepByStepSolution.Add(new Graph(curStep));
                            curStep.getVertex(i).Info = "";
                            curStep.getVertex(i).Color = curStep.stdVertexColor;
                            curStep.getVertex(j).Info = "";
                            curStep.getVertex(j).Color = curStep.stdVertexColor;
                        }
                    }
                }
                prevMatr = new List<List<int>>(res.DistanceMatr);
            }
            StepByStepSolution.Add(new Graph(curStep));
            return res;
        }

        public JohnsonInfo Johnson(out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = new Graph(this);
            curStep.IsOr = true;
            JohnsonInfo res = new JohnsonInfo();

            curStep.SetVertexColor(curStep.StdVertexColor);
            curStep.SetLineColor(curStep.StdLineColor);
            RGBColor RelaxingVertexColor = new RGBColor(0, 0, 0);

            Graph gs = new Graph(this);
            gs.addVertex(new Vertex(300, 300));
            int s=gs.getNumOfVertex()-1;
            for (int i = 0; i < s; ++i)
            {
                gs.addLine(s, i, 0);
            }

            BellmanFordInfo bfi = gs.BellmanFord(s);
            if (res.WithCycle = bfi.WithCycle) return res;

            List<int> h = new List<int>();
            for (int i = 0; i < gs.getNumOfVertex(); ++i)
            {
                h.Add(bfi.Distance[i]);
            }

            Graph reweighting = new Graph(gs);
            for (int i = 0; i < gs.getNumOfVertex(); ++i)
            {
                for (int j = 0; j < gs.getNumOfVertex(); ++j)
                {
                    if (gs.getAdjacentLine(i)[j] != null)
                    {
                        reweighting.getAdjacentLine(i)[j] = new Line(i, j, gs.getAdjacentLine(i)[j].Weight + h[i] - h[j]);
                    }
                }
            }

            DijkstraInfo di;
            for (int u = 0; u < s; ++u)
            {
                di = reweighting.Dijkstra(u);
                res.DistanceMatr.Add(new List<int>());
                for (int v = 0; v < s; ++v)
                {
                    res.DistanceMatr[u].Add(di.Distance[v]==int.MaxValue ? int.MaxValue : di.Distance[v] + h[v] - h[u]);
                    if (res.DistanceMatr[u][v] != int.MaxValue)
                    {
                        curStep.getVertex(u).Info = "->" + curStep.getVertex(v).Name + "/" + res.DistanceMatr[u][v];
                        curStep.getVertex(u).Color = RelaxingVertexColor;
                        curStep.getVertex(v).Info = "<-" + curStep.getVertex(u).Name + "/" + res.DistanceMatr[u][v];
                        curStep.getVertex(v).Color = RelaxingVertexColor;
                        StepByStepSolution.Add(new Graph(curStep));
                        curStep.getVertex(u).Info = "";
                        curStep.getVertex(u).Color = curStep.stdVertexColor;
                        curStep.getVertex(v).Info = "";
                        curStep.getVertex(v).Color = curStep.stdVertexColor;
                    }
                }
            }
            StepByStepSolution.Add(new Graph(curStep));
            return res;
        }

        public FordFalkersonInfo FordFalkerson(int source, int sink, out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = new Graph(this);
            curStep.IsOr = true;
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                foreach (Line a in curStep.getAdjacentLine(i))
                {
                    if (a != null)
                    {
                        a.Weight = Math.Abs(a.Weight);
                    }
                }
            }
            FordFalkersonInfo res = new FordFalkersonInfo();
            res.Source = source;
            res.Sink = sink;
            for(int i=0; i<getNumOfVertex(); ++i)
            {
                res.Flow.Add(new List<int>());
                for(int j=0; j<getNumOfVertex(); ++j)
                {
                    res.Flow[i].Add(0);
                }
            }

            RGBColor MarkedVertex = new RGBColor(0, 0, 0);
            RGBColor PathVertex = new RGBColor(255, 255, 0);
            RGBColor PathLine = new RGBColor(255, 255, 0);
            RGBColor SLine = new RGBColor(255, 0, 255);

            curStep.reset();

            //Список предшественников для первой компоненты обозначения, в начале каждой итерации заполняется -1
            List<int> parent = new List<int>();
            //Список для второй компоненты обозначения, в начале каждой итерации заполняется 0
            List<int> curFlowInVertex=new List<int>();
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                parent.Add(-1);
                curFlowInVertex.Add(0);
            }
            parent[source] = source;
            curFlowInVertex[source]=int.MaxValue;

            //Вершины, находящиеся в очереди, обозначены и не рассмотрены
            Queue<int> q = new Queue<int>();
            List<Line> adj;
            int curVertex;
            Line tmp;
            int theta;

            do
            {
                for (int i = 0; i < parent.Count; ++i)
                {
                    parent[i] = -1;
                    curFlowInVertex[i] = 0;
                }
                parent[source] = source;
                curFlowInVertex[source]=int.MaxValue;
                curStep.getVertex(source).Info = "/(+0,inf)";
                curStep.getVertex(source).Color = MarkedVertex;
                StepByStepSolution.Add(new Graph(curStep));

                q.Clear();
                q.Enqueue(source);
                while (q.Count != 0 && parent[sink] == -1)
                {
                    curVertex = q.Dequeue();
                    adj = curStep.getAdjacentLine(curVertex);
                    foreach (Line a in adj)
                    {
                        if (a == null) continue;
                        if (curStep.getVertex(a.V2).Color != MarkedVertex && a.Weight > res.Flow[a.V1][a.V2])
                        {
                            parent[a.V2] = curVertex;
                            curFlowInVertex[a.V2] = Math.Min(curFlowInVertex[a.V1], a.Weight - res.Flow[a.V1][a.V2]);
                            curStep.getVertex(a.V2).Info = "/(+" + curStep.getVertex(a.V1).Name + "," + curFlowInVertex[a.V2]+")";
                            curStep.getVertex(a.V2).Color = MarkedVertex;
                            StepByStepSolution.Add(new Graph(curStep));
                            q.Enqueue(a.V2);
                        }
                    }
                    adj = curStep.getIncomingLine(curVertex);
                    foreach (Line a in adj)
                    {
                        if (a == null) continue;
                        if (curStep.getVertex(a.V1).Color != MarkedVertex && res.Flow[a.V1][a.V2] > 0)
                        {
                            parent[a.V1] = a.V2;
                            curFlowInVertex[a.V1] = Math.Min(curFlowInVertex[a.V2], res.Flow[a.V1][a.V2]);
                            curStep.getVertex(a.V1).Info = "/(-" + curStep.getVertex(a.V2).Name + "," + curFlowInVertex[a.V1]+")";
                            curStep.getVertex(a.V2).Color = MarkedVertex;
                            StepByStepSolution.Add(new Graph(curStep));
                            q.Enqueue(a.V1);
                        }
                    }
                }
                if (parent[sink] == -1) break;

                theta = curFlowInVertex[sink];
                //Строим путь
                for (int i = sink; i!=source; i = parent[i])
                {
                    tmp = curStep.getAdjacentLine(parent[i])[i];
                    if (tmp != null) //Пришли по направлению ребра
                    {
                        res.Flow[parent[i]][i] += theta;
                        tmp.Info = res.Flow[parent[i]][i].ToString();
                    }
                    else
                    {
                        tmp = getAdjacentLine(i)[parent[i]];
                        res.Flow[i][parent[i]] -= theta;
                        tmp.Info = res.Flow[i][parent[i]].ToString();
                    }
                    tmp.Color = PathLine;
                    curStep.getVertex(tmp.V1).Color = PathVertex;
                    curStep.getVertex(tmp.V2).Color = PathVertex;
                    StepByStepSolution.Add(new Graph(curStep));
                }

                for (int i = 0; i < curStep.getNumOfVertex(); ++i)
                {
                    curStep.getVertex(i).Color = curStep.StdVertexColor;
                    curStep.getVertex(i).Info = "";
                }
                for (int i = 0; i < curStep.getNumOfVertex(); ++i)
                {
                    foreach (Line a in curStep.getAdjacentLine(i))
                    {
                        if (a == null) continue;
                        a.Color = curStep.StdLineColor;
                    }
                }
                StepByStepSolution.Add(new Graph(curStep));

            } while (parent[sink] != -1);

            //Считаем максимальный поток
            q.Clear();
            q.Enqueue(source);
            while (q.Count != 0)
            {
                curVertex = q.Dequeue();
                adj = curStep.getAdjacentLine(curVertex);
                foreach (Line a in adj)
                {
                    if (a == null) continue;
                    if (res.Flow[a.V1][a.V2] == 0) continue;
                    res.MaxFlow += res.Flow[a.V1][a.V2];
                    if (res.Flow[a.V1][a.V2] < a.Weight)
                    {
                        q.Enqueue(a.V2);
                    }
                    a.Color = SLine;
                    StepByStepSolution.Add(new Graph(curStep));
                }
            }

            return res;
        }

        public EdmondsKarpInfo EdmondsKarp(int source, int sink, out List<Graph> StepByStepSolution)
        {
            StepByStepSolution = new List<Graph>();
            Graph curStep = new Graph(this);
            curStep.IsOr = true;
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                foreach (Line a in curStep.getAdjacentLine(i))
                {
                    if (a != null)
                    {
                        a.Weight = Math.Abs(a.Weight);
                    }
                }
            }
            EdmondsKarpInfo res = new EdmondsKarpInfo();
            res.Source = source;
            res.Sink = sink;
            for (int i = 0; i < getNumOfVertex(); ++i)
            {
                res.Flow.Add(new List<int>());
                for (int j = 0; j < getNumOfVertex(); ++j)
                {
                    res.Flow[i].Add(0);
                }
            }

            RGBColor MarkedVertex = new RGBColor(0, 0, 0);
            RGBColor PathVertex = new RGBColor(255, 255, 0);
            RGBColor PathLine = new RGBColor(255, 255, 0);
            RGBColor SLine = new RGBColor(255, 0, 255);

            curStep.reset();

            BFSInfo bfsi;
            //Список для второй компоненты обозначения, в начале каждой итерации заполняется 0
            List<int> curFlowInVertex = new List<int>();
            for (int i = 0; i < curStep.getNumOfVertex(); ++i)
            {
                curFlowInVertex.Add(0);
            }
            curFlowInVertex[source] = int.MaxValue;

            List<Line> adj;
            Line tmp;
            int curVertex;
            int theta;
            Graph gf;

            do
            {
                for (int i = 0; i < curFlowInVertex.Count; ++i)
                {
                    curFlowInVertex[i] = 0;
                }
                curFlowInVertex[source] = int.MaxValue;
                curStep.getVertex(source).Info = "/(+0,inf)";
                curStep.getVertex(source).Color = MarkedVertex;
                StepByStepSolution.Add(new Graph(curStep));


                //Строим остаточную сеть
                gf = new Graph(curStep);
                for (int i = 0; i < gf.getNumOfVertex(); ++i)
                {
                    adj = gf.getAdjacentLine(i);
                    for (int j = 0; j < gf.getNumOfVertex(); ++j)
                    {
                        tmp = adj[j];
                        if (tmp == null) continue;
                        //Поток в ребре полностью заполнен - удаляем из сети
                        if (tmp.Weight == res.Flow[tmp.V1][tmp.V2])
                        {
                            gf.l[i][j] = null;
                        }
                    }
                    adj = gf.getIncomingLine(i);
                    for (int j = 0; j < gf.getNumOfVertex(); ++j)
                    {
                        tmp = adj[j];
                        if (tmp == null) continue;
                        //Поток не нулевой - можно провести поток в отрицательную сторону
                        if (res.Flow[tmp.V1][tmp.V2] > 0)
                        {
                            gf.addLine(tmp.V2, tmp.V1);
                        }
                    }
                }
                bfsi = gf.BFS(source);

                if (bfsi.Parent[sink] == -1) break;

                //Строим путь
                List<Line> Path = new List<Line>();

                for (int i = sink; i != source; i = bfsi.Parent[i])
                {
                    tmp = curStep.getAdjacentLine(bfsi.Parent[i])[i];
                    Path.Add(tmp);
                    tmp.Color = PathLine;
                    curStep.getVertex(tmp.V1).Color = PathVertex;
                    curStep.getVertex(tmp.V2).Color = PathVertex;
                    StepByStepSolution.Add(new Graph(curStep));
                }
                curStep.getVertex(source).Color = MarkedVertex;
                for (int i = Path.Count - 1; i >= 0; --i)
                {
                    tmp = Path[i];
                    if (curStep.getVertex(tmp.V2).Color != MarkedVertex)
                    {
                        curFlowInVertex[tmp.V2] = Math.Min(curFlowInVertex[bfsi.Parent[tmp.V2]], tmp.Weight - res.Flow[tmp.V1][tmp.V2]);
                        curStep.getVertex(tmp.V2).Info = "/(+" + curStep.getVertex(tmp.V1).Name + "," + curFlowInVertex[tmp.V2].ToString() + ")";
                        StepByStepSolution.Add(new Graph(curStep));
                    }
                    else
                    {
                        curFlowInVertex[tmp.V1] = Math.Min(curFlowInVertex[tmp.V2], res.Flow[tmp.V1][tmp.V2]);
                        curStep.getVertex(tmp.V1).Info = "/(-" + curStep.getVertex(tmp.V2).Name + "," + curFlowInVertex[tmp.V1].ToString() + ")";
                        StepByStepSolution.Add(new Graph(curStep));
                    }
                }

                theta = curFlowInVertex[sink];

                for (int i = sink; i != source; i = bfsi.Parent[i])
                {
                    tmp = curStep.getAdjacentLine(bfsi.Parent[i])[i];
                    if (tmp != null) //Пришли по направлению ребра
                    {
                        res.Flow[bfsi.Parent[i]][i] += theta;
                        tmp.Info = res.Flow[bfsi.Parent[i]][i].ToString();
                    }
                    else
                    {
                        tmp = getAdjacentLine(i)[bfsi.Parent[i]];
                        res.Flow[i][bfsi.Parent[i]] -= theta;
                        tmp.Info = res.Flow[i][bfsi.Parent[i]].ToString();
                    }
                    tmp.Color = PathLine;
                    curStep.getVertex(tmp.V1).Color = PathVertex;
                    curStep.getVertex(tmp.V2).Color = PathVertex;
                    StepByStepSolution.Add(new Graph(curStep));
                }

                for (int i = 0; i < curStep.getNumOfVertex(); ++i)
                {
                    curStep.getVertex(i).Color = curStep.StdVertexColor;
                    curStep.getVertex(i).Info = "";
                }
                for (int i = 0; i < curStep.getNumOfVertex(); ++i)
                {
                    foreach (Line a in curStep.getAdjacentLine(i))
                    {
                        if (a == null) continue;
                        a.Color = curStep.StdLineColor;
                    }
                }
                StepByStepSolution.Add(new Graph(curStep));

            } while (bfsi.Parent[sink] != -1);

            //Считаем максимальный поток
            Queue<int> q = new Queue<int>();
            q.Enqueue(source);
            while (q.Count != 0)
            {
                curVertex = q.Dequeue();
                adj = curStep.getAdjacentLine(curVertex);
                foreach (Line a in adj)
                {
                    if (a == null) continue;
                    if (res.Flow[a.V1][a.V2] == 0) continue;
                    res.MaxFlow += res.Flow[a.V1][a.V2];
                    if (res.Flow[a.V1][a.V2] < a.Weight)
                    {
                        q.Enqueue(a.V2);
                    }
                    a.Color = SLine;
                    StepByStepSolution.Add(new Graph(curStep));
                }
            }

            return res;
        }
    }
}
