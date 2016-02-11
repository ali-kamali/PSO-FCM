using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PSO_FCM.Logic;
using PSO_FCM.Logic.FCM;
using PSO_FCM.Logic.PSO;
using PSO_FCM.Utility;

namespace PSO_FCM
{
    public partial class Form1 : Form
    {
        private const double W = 1; //GeneralCom.GetRandom(0.1, 0.9);
        private const double C1 = 1.49; //GeneralCom.GetRandom(0.1, 0.9);
        private const double C2 = 1.49; //GeneralCom.GetRandom(0.1, 0.9);
        readonly double _rate = Math.Pow(10, -30);
        private const int C = 3;
        private const int M = 2;
        private int _n ;
        private double[] _maxD;
        private List<Data> _data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _data = Loader.LoadData();
            _n = _data.Count;
            _maxD = new double[_data[0].DataDim.Val.Length];
            for (int i = 0; i < _data[0].DataDim.Val.Length; i++)
            {
                for (int j = 0; j < _data.Count; j++)
                {
                    _maxD[i] = Math.Max(_data[j].DataDim.Val[i], _maxD[i]);
                }
            }
            PsoFcm();
            //Fcm();

        }

        public void PsoFcm()
        {
            
            Pso ps = new Pso(C, _n, M, W, C1, C2, 30, _data[0].DataDim.Val.Length, _data, _maxD);
            File.WriteAllText("log", "");
            for (int i = 0; i < 200; i++)
            {
                ps.Calc();
                File.AppendAllText("FitnessPSO", ps.Fitness + "\t\n");
                File.AppendAllText("log", ps.GloablBestError + "\t;\t" + ps.Variance + "\t\n");
                if (ps.Variance < _rate)
                    break;
            }

            File.WriteAllText("datares", "");
            for (int i = 0; i < ps.N; i++)
            {
                for (int j = 0; j < ps.C; j++)
                {
                    File.AppendAllText("datares", ps.U[i, j].ToString(CultureInfo.InvariantCulture) + ";");
                }
                File.AppendAllText("datares", "\t\n");
            }
            Fcm fc = new Fcm(C, _n, M, 30, _data[0].DataDim.Val.Length, _data, ps.U);

            File.WriteAllText("datafcm", "");
            for (int a = 0; a < 100; a++)
            {
                fc.CalcCenter();
                fc.CalcU();
                fc.CalcFitness(_data,M);
                File.AppendAllText("FitnessFCMPSO", fc.Fitness+ "\t\n");
                for (int i = 0; i < ps.N; i++)
                {
                    for (int j = 0; j < ps.C; j++)
                    {
                        File.AppendAllText("datafcm", fc.U[i, j].ToString(CultureInfo.InvariantCulture) + ";");
                    }
                    File.AppendAllText("datafcm", "\t\n");
                }
                File.AppendAllText("datafcm", "\t\n--\t\n");
            }
            
        }

        public void Fcm()
        {
            double[,] U=new double[_n,C];
            for (int i = 0; i < _n; i++)
            {
                double[] tem=new double[C];
                double sum = 0;
                for (int j = 0; j < C; j++)
                {
                    tem[j] = GeneralCom.GetRandom()+0.001;
                    sum += tem[j];
                }
                for (int j = 0; j < C; j++)
                {
                    U[i, j] = tem[j] / sum;
                }
            }
            Fcm fc = new Fcm(C, _n, M, 30, _data[0].DataDim.Val.Length, _data, U);
            File.WriteAllText("datafcm", "");
            for (int a = 0; a < 100; a++)
            {
                fc.CalcCenter();
                fc.CalcU();
                fc.CalcFitness(_data,M);
                File.AppendAllText("FitnessFCM", fc.Fitness + "\t\n");
                for (int i = 0; i < _n; i++)
                {
                    for (int j = 0; j < C; j++)
                    {
                        File.AppendAllText("datafcm", fc.U[i, j].ToString(CultureInfo.InvariantCulture) + ";");
                    }
                    File.AppendAllText("datafcm", "\t\n");
                }
                File.AppendAllText("datafcm", "\t\n--\t\n");
            }
        }
    }
}
