using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using PSO_FCM.Utility;

namespace PSO_FCM.Logic.PSO
{
    public class Pso
    {
        public List<Particle> Particles { get; set; }
        public Dim[] GloablBestPosition { get; set; }
        public double GloablBestError { get; set; }
        public int C { get; set; } //number of clusters
        public int N { get; set; } //sample size
        public double M { get; set; } //Fuzzy exponent
        public int Num { get; set; }//Max Iteration
        public int T { get; set; }//Time
        public int D { get; set; }//Dimensions
        public double W { get; set; }//Weight
        public double C1 { get; set; }//Learning Factor
        public double C2 { get; set; }//Learning Factor
        public double[,] U { get; set; }//Matrix
        public double Variance { get; set; } //Variance
        public List<Data> Datas { get; set; }
        public Pso(int c, int n, double m, double w, double c1, double c2, int num, int dim, List<Data> datas)
        {
            C = c;
            N = n;
            M = m;
            Num = num;
            T = 0;
            D = dim;
            Datas = datas;
            W = w;
            C1 = c1;
            C2 = c2;
            GloablBestError = double.MinValue;
            GloablBestPosition=new Dim[c];
            Particles = new List<Particle>();
            for (int a = 0; a < Num; a++)
            {
                
                Particle p = new Particle
                {
                    U = new double[N, c],
                    C = c,
                    N = n,
                    Position = new Dim[c],
                    BestPosition = new Dim[c],
                    Velocity = new Dim[c],
                    Error = Double.MinValue,
                    BestError = Double.MinValue,

                };
                for (int i = 0; i < c; i++)
                {
                    p.Position[i] = new Dim(D);
                    p.Velocity[i] = new Dim(D);
                    if (a == 0)
                    {
                        GloablBestPosition[i]= new Dim(D);
                    }
                    for (int j = 0; j < D; j++)
                    {
                        p.Position[i].Val[j] = GeneralCom.GetRandom();
                        //p.Velocity[i].Val[j] = GeneralCom.GetRandom();
                        if (a == 0)
                        {
                            GloablBestPosition[i].Val[j] = GeneralCom.GetRandom();
                        }
                    }
                }
                p.BestPosition = p.Position;
                
                p.CalcU(datas, M);
                p.CalcV(W, C1, C2, GloablBestPosition);
                Particles.Add(p);
            }

        }

        public void Calc()
        {
            foreach (Particle particle in Particles)
            {
                particle.CalcError(Datas,M);
                if (particle.BestError < particle.Error)
                {
                    particle.BestError = particle.Error;
                    particle.BestPosition = particle.Position;
                }
            }
            var bestp = Particles.OrderBy(p => p.BestError).FirstOrDefault();
            if (bestp != null)
            {
                if (GloablBestError < bestp.BestError)
                {
                    GloablBestError = bestp.BestError;
                    GloablBestPosition = bestp.BestPosition;
                    U = bestp.U;
                }    
            }
            double xbar = 0;
            int cnt = 0;
            foreach (Particle particle in Particles)
            {
                particle.CalcV(W,C1,C2,GloablBestPosition);
                particle.UpdatePost();
                xbar += particle.Error;
                cnt++;
            }
            xbar = xbar/cnt;
            Variance = Particles.Sum(particle => Math.Pow((particle.Error - xbar), 2))/cnt;
        }
    }
}