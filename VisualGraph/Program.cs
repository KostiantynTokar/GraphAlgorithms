using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;



namespace GraphAlgorithms
{
    class Menu
    {
        public static void menu()
        {
            Console.WriteLine("Введите номер алгоритма:");
            Console.WriteLine("1: поиск в ширину");
            Console.WriteLine("2: поиск в глубину");
            Console.WriteLine("3: Крускал");
            Console.WriteLine("4: Прим");
            Console.WriteLine("5: Беллман-Форд");
            Console.WriteLine("6: Дейкстра");
            Console.WriteLine("7: Флойд-Уоршелл");
            Console.WriteLine("8: Джонсон");
            Console.WriteLine("9: Форд-Фалкерсон");
            Console.WriteLine("0: Эдмондс-Карп");
            Console.WriteLine("с: Очистить");
            Console.WriteLine("n: Нарисовать новый граф");
            Console.WriteLine("o: Ориентированный/неориентированный");
            Console.WriteLine("w: Показывать/не показывать вес ребер");
            Console.WriteLine();

        }

        public static int chooseStart()
        {
            Console.WriteLine("Выберите нормер вершины-начала");
            return getNum();
        }

        public static int chooseFinish()
        {
            Console.WriteLine("Выберите нормер вершины-конца");
            return getNum();
        }

        private static int getNum()
        {
            int s;
            bool res = int.TryParse(Console.ReadLine(), out s);
            if (!res)
            {
                error();
                return (-1);
            }
            return s - 1;
        }

        private static void error()
        {
            Console.WriteLine("Некорректный выбор вершины");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Menu.menu();
            Graph g = new Graph(!false);
#if (DEBUG)
            g.addVertex(new Vertex(150, 100, new RGBColor(255,0,0)));
            g.addLine(new Vertex(-50, 50, new RGBColor(255, 0, 0)), new Vertex(150, 200, new RGBColor(255, 0, 0)), 7);
            g.addVertex(new Vertex(-100, -150, new RGBColor(255, 0, 0)));
            g.addLine(0, 2, 5);
            g.addLine(0, 3, 3);
            g.addVertex(new Vertex(50, -100, new RGBColor(255, 0, 0)));
            g.addVertex(new Vertex(100, -150, new RGBColor(255, 0, 0)));
            g.addVertex(new Vertex(50, -200, new RGBColor(255,0,0)));
            g.addLine(3, 4, 2);
            g.addLine(4, 5, 9);
            g.addLine(5, 6, 2);
            g.addLine(6, 3, 5);
            g.addVertex(new Vertex(-200, -100, new RGBColor(255, 0, 0)));
            g.addLine(3, 7, 4);
#endif
            Visualizer v = new Visualizer(g);
            v.visualize();
        }
    }
}
