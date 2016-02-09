using System;
using System.IO;
using System.Linq;
using PSO_FCM.Logic;

namespace PSO_FCM.Utility
{
    public static class GeneralCom
    {
        private static readonly Random Rand = new Random((int) DateTime.Now.Ticks%Int32.MaxValue);
        public static double Euclideandistance(Dim d1, Dim d2)
        {
            if (d1.Val == null || d2.Val == null)
                return 0;
            if (d1.Val.Length != d2.Val.Length)
                return 0;
            double sumsq= d1.Val.Select((t, i) => Math.Pow(t - d2.Val[i], 2)).Sum();
            return Math.Sqrt(sumsq);
        }

        public static double GetRandom(double min = 0, double max=1000)
        {
            var d = (double) Rand.Next((int) (min*100), (int) (max*100))/100;
            if (Math.Abs(max - 1000) < 0.5)
            {
                File.AppendAllText("logD", d + "\t\n");
            }
            return d;
        }
    }
}