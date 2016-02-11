using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using PSO_FCM.Utility;

namespace PSO_FCM.Logic.PSO
{
    public class Particle
    {
        public Dim[] Position { get; set; }
        public double Error { get; set; }
        public double Fitness { get; set; }
        public double BestFitness { get; set; }
        public Dim[] Velocity { get; set; }
        public Dim[] BestPosition { get; set; }
        public double BestError{ get; set; }
        public double[,] U { get; set; }//Matrix
        public int C { get; set; } //number of clusters
        public int N { get; set; } //sample size

        public void CalcU(List<Data> datas, double m)
        {
            //File.WriteAllText("data.res", "");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < C; k++)
                    {
                        var a = GeneralCom.Euclideandistance(datas[i].DataDim, Position[j]);
                        var b = GeneralCom.Euclideandistance(datas[i].DataDim, Position[k]);
                      //  File.AppendAllText("logU", "a;" + a + "\t\n");
                     //   File.AppendAllText("logU", "b;" + b + "\t\n");
                        sum += Math.Pow((a / b), 2 / (m - 1));
                    }
                    U[i, j] = 1 / sum;
                   // File.AppendAllText("dataa", U[i, j].ToString(CultureInfo.InvariantCulture) + ";");
                }
               // File.AppendAllText("dataa", "\t\n");
            }
        }

        public void CalcV(double w, double c1, double c2, Dim[] globalPosition)
        {
            for (int c = 0; c< C; c++)
            {
                for (int d = 0; d < Velocity[c].Val.Length; d++)
                {
                    double y1 = GeneralCom.GetRandom(0, 1);
                    double y2 = GeneralCom.GetRandom(0, 1);
                    Velocity[c].Val[d] = w * Velocity[c].Val[d] + c1 * y1*(BestPosition[c].Val[d]-Position[c].Val[d])
                        + c2 * y2 * (globalPosition[c].Val[d] - Position[c].Val[d]);
                }
            }
        }

        public void UpdatePost()
        {
            for (int c = 0; c < C; c++)
            {
                for (int d = 0; d < Velocity[c].Val.Length; d++)
                {
                    Position[c].Val[d] = Position[c].Val[d]+ Velocity[c].Val[d];
                }
            }
        }

        public void CalcError(List<Data> datas,double m)
        {
            double sigma = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    sigma += Math.Pow(U[i, j], m)*
                             Math.Pow(GeneralCom.Euclideandistance(datas[i].DataDim, Position[j]), 2);
                }
            }
            Fitness = sigma;
            File.AppendAllText("FitnessParticle", Fitness + "\t\n");
            Error = 1/(sigma+1);
            //File.AppendAllText("dataa", "\t\n" + Error + "\t\n\t\n");

        }

    }
}