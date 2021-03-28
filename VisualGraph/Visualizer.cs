using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace VisualGraph
{
    class Visualizer
    {
        static bool isInit = false;
        protected Graph g;
        protected int chosenVertex=-1;
        Random r = new Random();
        protected bool showWeight;
        protected List<Graph> algorithm;
        protected int algStep;

        public Graph G
        {
            get { return g; }
            set { g = value; }
        }

        public Visualizer(Graph g_)
        {
            //if (g_ == null) throw new ArgumentNullException();
            g = g_;
            showWeight = true;
            algorithm = null;
            algStep = 0;
        }

        public void visualize()
        {
            if (!isInit) init();
            Glut.glutDisplayFunc(display);
            Glut.glutKeyboardFunc(keyb);
            Glut.glutMouseFunc(mouse);
            //Glut.glutIdleFunc(display);
            Glut.glutMainLoop();
        }

        public static void init()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Glut.glutInitWindowSize(500, 500);
            Glut.glutInitWindowPosition(700, 100);
            Glut.glutCreateWindow("Graph");

            Gl.glViewport(0, 0, 500, 500);
            Gl.glClearColor(0.0f, 0.0f, 0.388f, 0.0f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-250.0, 250.0, -250.0, 250.0, -1.0, 1.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();


            isInit = true;
        }

        virtual protected void display()
        {
            if (algorithm == null || algorithm.Count==0) visGraph(g, showWeight);
            else if (algStep == algorithm.Count - 1)
            {
                visGraph(algorithm[algStep], showWeight);
            }
            else
            {
                visGraph(algorithm[algStep], showWeight);
                algStep++;
                Gl.glFlush();
                Glut.glutPostRedisplay();
                System.Threading.Thread.Sleep(3000);
            }
            
            //Gl.glFlush();
            //Glut.glutPostRedisplay();
            //Glut.glutSwapBuffers();
        }

        virtual protected void keyb(byte key, int x, int y)
        {
            if (key == (byte)'c')
            {
                algorithm = null;
                algStep = 0;
            }
            else if (key == (byte)'1')
            {
                int s = Menu.chooseStart();
                try
                {
                    Console.WriteLine(g.BFS(s, out algorithm));
                    algStep = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (key == (byte)'2')
            {
                Console.WriteLine(g.DFS(out algorithm));
                algStep = 0;
            }
            else if (key == (byte)'3')
            {
                Console.WriteLine(g.MSTKruskal(out algorithm));
                algStep = 0;
            }
            else if (key == (byte)'4')
            {
                int s = Menu.chooseStart();
                try
                {
                    Console.WriteLine(g.MSTPrim(s, out algorithm));
                    algStep = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (key == (byte)'5')
            {
                int s = Menu.chooseStart();
                try
                {
                    Console.WriteLine(g.BellmanFord(s, out algorithm));
                    algStep = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (key == (byte)'6')
            {
                int s = Menu.chooseStart();
                try
                {
                    Console.WriteLine(g.Dijkstra(s, out algorithm));
                    algStep = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (key == (byte)'7')
            {
                Console.WriteLine(g.FloydWarshall(out algorithm));
                algStep = 0;
            }
            else if (key == (byte)'8')
            {
                Console.WriteLine(g.Johnson(out algorithm));
                algStep = 0;
            }
            else if (key == (byte)'9')
            {
                int s = Menu.chooseStart();
                int f = Menu.chooseFinish();
                try
                {
                    Console.WriteLine(g.FordFalkerson(s, f, out algorithm));
                    algStep = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (key == (byte)'0')
            {
                int s = Menu.chooseStart();
                int f = Menu.chooseFinish();
                try
                {
                    Console.WriteLine(g.EdmondsKarp(s, f, out algorithm));
                    algStep = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (key == (byte)'n')
            {
                g = new Graph(true);
                algorithm = null;
                algStep = 0;
            }
            else if (key == (byte)'o')
            {
                g.IsOr = !g.IsOr;
                algorithm = null;
                algStep = 0;
            }
            else if (key == (byte)'w')
            {
                showWeight = !showWeight;
            }
            Glut.glutPostRedisplay();
        }

        virtual protected void mouse(int button, int state, int x, int y)
        {
            int n=-1;
            switch (button)
            {
                case Glut.GLUT_LEFT_BUTTON:
                    if (state == Glut.GLUT_DOWN)
                    {
                        if (!isin(x - 250, 250 - y, ref n))
                        {
                            g.addVertex(new Vertex(x - 250, 250 - y, g.StdVertexColor));
                            algorithm = null;
                            algStep = 0;
                            Glut.glutPostRedisplay();
                        }
                    }
                    break;
                case Glut.GLUT_RIGHT_BUTTON:
                    if (state == Glut.GLUT_DOWN)
                    {
                        if (isin(x - 250, 250 - y, ref n))
                        {
                            if (chosenVertex == -1)
                            {
                                chosenVertex = n;
                                g.getVertex(chosenVertex).Color = new RGBColor(0, 0, 0);
                            }
                            else
                            {
                                if (g.getAdjacentLine(chosenVertex)[n] == null && g.getAdjacentLine(n)[chosenVertex] == null)
                                {
                                    g.addLine(chosenVertex, n, r.Next(-100, 101));
                                    g.getVertex(chosenVertex).Color = new RGBColor(g.StdVertexColor);
                                    chosenVertex = -1;
                                }
                            }
                            algorithm = null;
                            algStep = 0;
                            Glut.glutPostRedisplay();
                        }
                    }
                    break;
            }
        }

        virtual protected bool isin(int x, int y, ref int n)
        {
            double eps = 15f;
            bool res = false;
            for (int i = 0; i < g.getNumOfVertex() && !res; ++i)
            {
                res = (Math.Sqrt((x - g.getVertex(i).X) * (x - g.getVertex(i).X) +
                                 (y - g.getVertex(i).Y) * (y - g.getVertex(i).Y)) < eps);
                if (res) n = i;
            }
            return res;
        }

        static protected void visGraph(Graph gr, bool showWeight)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glPointSize(10);

            SortedSet<int> tmp;
            for (int i = 0; i < gr.getNumOfVertex(); ++i)
            {
                tmp = gr.getAdjacent(i);
                foreach (int j in tmp)
                {
                    if (!gr.IsOr && j < i) continue;
                    visLine(gr, i, j, showWeight);
                }
            }

            for (int i = 0; i < gr.getNumOfVertex(); ++i)
            {
                visVertex(gr.getVertex(i));
            }
        }

        static protected void visText(int x, int y, string text)
        {
            if (text == null || text == "") return;
            Gl.glColor3ub(255, 170, 255);
            Gl.glRasterPos2i(x, y);
            foreach (char c in text)
            {
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_9_BY_15, c);
            }
        }

        static protected void visVertex(Vertex v)
        {
            double R=5;
            Gl.glColor3ub(v.Color.R, v.Color.G, v.Color.B);
            //Gl.glBegin(Gl.GL_POINTS);
            //Gl.glVertex2i(v.X, v.Y);
            //Gl.glEnd();
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < 360; ++i)
            {
                Gl.glVertex2d((double)v.X + Math.Cos((double)i * Math.PI / (double)180) * R, (double)v.Y + Math.Sin((double)i * Math.PI / (double)180) * R);
            }
            Gl.glEnd();
            visText(v.X + 5, v.Y + 5, v.Name+v.Info);
        }

        static protected void visLine(Graph gr, int v1, int v2, bool showWeight)
        {
            Line a = gr.getAdjacentLine(v1)[v2];
            Gl.glColor3ub(a.Color.R, a.Color.G, a.Color.B);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2i(gr.getVertex(v1).X, gr.getVertex(v1).Y);
            Gl.glVertex2i(gr.getVertex(v2).X, gr.getVertex(v2).Y);
            Gl.glEnd();
            if (gr.IsOr)
            {
                double alpha;

                if (gr.getVertex(v2).X == gr.getVertex(v1).X)
                {
                    alpha = Math.Sign(gr.getVertex(v2).X - gr.getVertex(v1).X) * Math.PI / 2;
                }
                else
                {
                    alpha = Math.Atan2((double)(gr.getVertex(v2).X - gr.getVertex(v1).X),
                                       (double)(gr.getVertex(v2).Y - gr.getVertex(v1).Y));
                }
                Gl.glPushMatrix();
                Gl.glTranslated(gr.getVertex(v2).X, gr.getVertex(v2).Y, 0);
                alpha = alpha * 180.0 / Math.PI;
                Gl.glRotated(alpha, 0, 0, -1);
                Gl.glBegin(Gl.GL_TRIANGLES);
                Gl.glVertex2i(0, 0);
                Gl.glVertex2i(-5, -15);
                Gl.glVertex2i(5, -15);
                Gl.glEnd();
                Gl.glPopMatrix();
            }
            if (showWeight)
            {
                visText(gr.getVertex(v2).X - (gr.getVertex(v2).X - gr.getVertex(v1).X) / 2,
                        gr.getVertex(v2).Y - (gr.getVertex(v2).Y - gr.getVertex(v1).Y) / 2,
                        "(" + gr.getAdjacentLine(v1)[v2].Weight.ToString() + ")"+gr.getAdjacentLine(v1)[v2].Info);
            }

        }
    }
}
