using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cem3
{
    class Program
    {
        static double Syst(int swit, double x, double y0, double y1, double h)
        {
            switch (swit)
            {
                case 1: return h * y1;
                case 2: return h * (-4.0 * y1 - 4.0 * y0);
            }
            return 0;
        }

        static void Main(string[] args)
        {
            double x = 0.0, y0 = 1.0, y1 = -1.0, h = 0.01, yreal = 1.0;
            double y0new, y1new;
            Console.WriteLine("Эйлер");
            Console.WriteLine("  x          y0     yreal    Погрешность");
            for (x = 0; x <= 1; x += h)
            {
                Console.WriteLine("{0:f4}    {1:f4}    {2:f4}   {3:f4}", x, y0, yreal, yreal - y0);
                y1new = y1 + Syst(2, x, y0, y1, h);
                y0new = y0 + Syst(1, x, y0, y1, h);
                y0 = y0new; y1 = y1new;
                yreal = (1 + x) * Math.Exp(-2 * x);
            }
            Console.ReadKey();
            Console.Clear();
            x = 0.0; y0 = 1.0; y1 = -1.0; yreal = 1.0;
            Console.WriteLine("\nРК 2");
            Console.WriteLine("  x          y0     yreal    Погрешность");
            for (x = 0; x <= 1; x += h)
            {
                Console.WriteLine("{0:f4}    {1:f4}    {2:f4}   {3:f4}", x, y0, yreal, yreal - y0);
                y0new = y0 + h * Syst(1, x + h / 2, y0 + h / 2 * Syst(1, x, y0, y1, h), y1 + h / 2 * Syst(2, x, y0, y1, h), h);
                y1new = y1 + h * Syst(2, x + h / 2, y0 + h / 2 * Syst(1, x, y0, y1, h), y1 + h / 2 * Syst(2, x, y0, y1, h), h);
                y0 = y0new; y1 = y1new;
                yreal = (1 + x) * Math.Exp(-2 * x);
            }
            Console.ReadKey();
            Console.Clear();
            x = 0.0; y0 = 1.0; y1 = -1.0; yreal = 1.0;
            double[] r1 = new double[4];
            double[] r0 = new double[4];
            Console.WriteLine("\nРК 4");
            Console.WriteLine("  x          y0     yreal    Погрешность");
            for (x = 0; x <= 1; x += h)
            {
                Console.WriteLine("{0:f4}    {1:f4}    {2:f4}   {3:f4}", x, y0, yreal, yreal - y0);
                r0[0] = h * Syst(1, x, y0, y1, h);
                r1[0] = h * Syst(2, x, y0, y1, h);
                r0[1] = h * Syst(1, x + h / 2, y0 + 1 / 2 * r0[0], y1 + 1 / 2 * r1[0], h);
                r1[1] = h * Syst(2, x + h / 2, y0 + 1 / 2 * r0[0], y1 + 1 / 2 * r1[0], h);
                r0[2] = h * Syst(1, x + h / 2, y0 + 1 / 2 * r0[1], y1 + 1 / 2 * r1[1], h);
                r1[2] = h * Syst(2, x + h / 2, y0 + 1 / 2 * r0[1], y1 + 1 / 2 * r1[1], h);
                r0[3] = h * Syst(1, x + h / 2, y0 + r0[2], y1 + r1[2], h);
                r1[3] = h * Syst(2, x + h / 2, y0 + r0[2], y1 + r1[2], h);
                y0 += (r0[0] + 2 * r0[1] + 2 * r0[2] + r0[3]) / 6;
                y1 += (r1[0] + 2 * r1[1] + 2 * r1[2] + r1[3]) / 6;
                yreal = (1 + x) * Math.Exp(-2 * x);
            }
            Console.ReadKey();
            Console.Clear();
            x = 0.0; y0 = 1.0; y1 = -1.0; yreal = 1.0;
            Console.WriteLine("\nПредикт-корректор");
            Console.WriteLine("  x          y0     yreal    Погрешность");
            for (x = 0; x <= 1; x += h)
            {
                Console.WriteLine("{0:f4}    {1:f4}    {2:f4}   {3:f4}", x, y0, yreal, yreal - y0);
                y0 += h * (Syst(1, x, y0, y1, h) + Syst(1, x + h, y0 + h * Syst(1, x, y0, y1, h), y1 + h * Syst(2, x, y0, y1, h), h)) / 2;
                y1 += h * (Syst(2, x, y0, y1, h) + Syst(2, x + h, y0 + h * Syst(1, x, y0, y1, h), y1 + h * Syst(2, x, y0, y1, h), h)) / 2;
                yreal = (1 + x) * Math.Exp(-2 * x);
            }
            Console.ReadKey();
        }
    }
}
