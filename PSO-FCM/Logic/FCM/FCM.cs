using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PSO_FCM.Utility;

namespace PSO_FCM.Logic.FCM
{
    public class Fcm
    {
        public int C { get; set; } //number of clusters
        public int N { get; set; } //sample size
        public double M { get; set; } //Fuzzy exponent
        public int Num { get; set; }//Max Iteration
        public int T { get; set; }//Time
        public int D { get; set; }//Dimensions
        public double[,] U { get; set; }//Matrix
        public Dim[] Centers { get; set; } //Centers
        public List<Data> Datas { get; set; }


        public Fcm(int c, int n, double m, int num, int dim, List<Data> datas, double[,] u)
        {
            C = c;
            N = n;
            M = m;
            Num = num;
            T = 0;
            D = dim;
            Datas = datas;
            U = u;
            Centers=new Dim[c];
            
            

        }

        public void CalcCenter()
        {
            for (int j = 0; j < C; j++)
            {
                Centers[j] = new Dim(D);
                for (int dd = 0; dd < D; dd++)
                {
                    double sigma1 = 0;
                    for (int i = 0; i < N; i++)
                    {
                        sigma1 += Math.Pow(U[i, j], M) * Datas[i].DataDim.Val[dd];
                    }
                    double sigma2 = 0;
                    for (int i = 0; i < N; i++)
                    {
                        sigma2 += Math.Pow(U[i, j], M);
                    }
                    Centers[j].Val[dd] = sigma1 / sigma2;
                }
            }
        }

        public void CalcU()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    double sigma = 0;
                    for (int k = 0; k < C; k++)
                    {
                        var a = GeneralCom.Euclideandistance(Datas[i].DataDim, Centers[j]);
                        var b = GeneralCom.Euclideandistance(Datas[i].DataDim, Centers[k]);
                        sigma += Math.Pow(a / b, (2 / (M - 1)));
                    }
                    U[i, j] = 1 / sigma;
                }
            }
        }
    }
}