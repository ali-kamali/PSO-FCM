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
        private static double W = 0.16; //GeneralCom.GetRandom(0.1, 0.9);
        private static double C1 = 1; //GeneralCom.GetRandom(0.1, 0.9);
        private static double C2 = 1.23; //GeneralCom.GetRandom(0.1, 0.9);
        readonly double _rate = Math.Pow(10, -10);
        private const int C = 3;
        private const int M = 2;
        private int _n ;
        private double[] _maxD;
        private List<Data> _data;
        private string DateSet = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var DataFiles = Directory.GetFiles(Application.StartupPath,"*.dat");
            foreach (string dataFile in DataFiles)
            {
                cb_data.Items.Add(dataFile.Substring(dataFile.LastIndexOf('\\')+1));
            }
            if (DataFiles.Any())
            {
                cb_data.SelectedIndex = 0;
            }
        }

        private void btn_Create1000X_Click(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines(cb_data.SelectedItem.ToString());
            string[] Finall=new string[1000*lines.Length];
            for (int i = 0; i < lines.Length*1000; i++)
            {
                int index =(int)GeneralCom.GetRandom(0, lines.Length);
                Finall[i] = lines[index];
            }
            File.WriteAllLines(cb_data.SelectedItem.ToString().Replace(".dat","X1000.dat"),Finall);
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            _data = Loader.LoadData(cb_data.SelectedItem.ToString());
            DateSet = cb_data.SelectedItem.ToString().Replace(".dat", "");
            if (!Directory.Exists(DateSet))
                Directory.CreateDirectory(DateSet);
            _n = _data.Count;
            _maxD = new double[_data[0].DataDim.Val.Length];
            for (int i = 0; i < _data[0].DataDim.Val.Length; i++)
            {
                for (int j = 0; j < _data.Count; j++)
                {
                    _maxD[i] = Math.Max(_data[j].DataDim.Val[i], _maxD[i]);
                }
            }
            cb_data.Enabled = false;
            btn_Create1000X.Enabled = false;
            btn_Load.Enabled = false;
            btn_Pso10000.Enabled = true;
            btn_PSOFCMRR.Enabled = true;
            btn_PSOFCM.Enabled = true;
            btn_FCM.Enabled = true;
        }

        private void btn_FCM_Click(object sender, EventArgs e)
        {
            File.WriteAllText(DateSet + "//FitnessFCM", "");
            //File.WriteAllText(DateSet + "datafcm", "");
            var now = DateTime.Now;
            double[,] U = new double[_n, C];
            for (int i = 0; i < _n; i++)
            {
                double[] tem = new double[C];
                double sum = 0;
                for (int j = 0; j < C; j++)
                {
                    tem[j] = GeneralCom.GetRandom() + 0.001;
                    sum += tem[j];
                }
                for (int j = 0; j < C; j++)
                {
                    U[i, j] = tem[j] / sum;
                }
            }
            Fcm fc = new Fcm(C, _n, M, 30, _data[0].DataDim.Val.Length, _data, U);
            
            for (int a = 0; a < 100; a++)
            {
                fc.CalcCenter();
                fc.CalcU();
                fc.CalcFitness(_data, M);
                File.AppendAllText(DateSet+"//FitnessFCM", fc.Fitness+"\t;\t"+DateTime.Now.Subtract(now).TotalSeconds + "\t\n");
                
            }
            //for (int i = 0; i < _n; i++)
            //{
            //    for (int j = 0; j < C; j++)
            //    {
            //        File.AppendAllText(DateSet + "//datafcm", fc.U[i, j].ToString(CultureInfo.InvariantCulture) + ";");
            //    }
            //    File.AppendAllText(DateSet + "//datafcm", "\t\n");
            //}
        }

        private void btn_PSOFCM_Click(object sender, EventArgs e)
        {
            Pso ps = new Pso(C, _n, M, W, C1, C2, 30, _data[0].DataDim.Val.Length, _data, _maxD);
            //File.WriteAllText("log", "");
            //File.WriteAllText("FitnessParticle", "");
            File.WriteAllText(DateSet + "//FitnessPSO", "");
            File.WriteAllText(DateSet + "//FitnessFCMPSO", "");
            //File.WriteAllText("datares", "");
            //File.WriteAllText("datafcm", "");
            var now = DateTime.Now;
            for (int i = 0; i < 200; i++)
            {
                ps.Calc();
                File.AppendAllText(DateSet + "//FitnessPSO", ps.GloablBestFitness + "\t\n");
                //File.AppendAllText("log", ps.GloablBestError + "\t;\t" + ps.Variance + "\t\n");
                if (ps.Variance < _rate)
                    break;
            }

            //for (int i = 0; i < ps.N; i++)
            //{
            //    for (int j = 0; j < ps.C; j++)
            //    {
            //        File.AppendAllText("datares", ps.U[i, j].ToString(CultureInfo.InvariantCulture) + ";");
            //    }
            //    File.AppendAllText("datares", "\t\n");
            //}
            Fcm fc = new Fcm(C, _n, M, 30, _data[0].DataDim.Val.Length, _data, ps.U);

            for (int a = 0; a < 100; a++)
            {
                fc.CalcCenter();
                fc.CalcU();
                fc.CalcFitness(_data, M);
                File.AppendAllText(DateSet+"//FitnessFCMPSO", fc.Fitness + "\t;\t"+DateTime.Now.Subtract(now).TotalSeconds + "\t\n");
                
            }
            //for (int i = 0; i < ps.N; i++)
            //{
            //    for (int j = 0; j < ps.C; j++)
            //    {
            //        File.AppendAllText(DateSet + "//datafcm", fc.U[i, j].ToString(CultureInfo.InvariantCulture) + ";");
            //    }
            //    File.AppendAllText(DateSet + "//datafcm", "\t\n");
            //}

        }
        private void btn_Pso10000_Click(object sender, EventArgs e)
        {
            for (int it = 0; it < 10000; it++)
            {
                W = GeneralCom.GetRandom(0.1, 0.9);
                C1 = GeneralCom.GetRandom(0.1, 2);
                C2 = GeneralCom.GetRandom(0.1, 2);
                Pso ps = new Pso(C, _n, M, W, C1, C2, 30, _data[0].DataDim.Val.Length, _data, _maxD);
                int i = 0;
                for (i = 0; i < 200; i++)
                {
                    ps.Calc();
                    if (ps.Variance < _rate)
                        break;
                }
                File.AppendAllText("PSOIterate", it + ";" + W + ";" + C1 + ";" + C2 + ";" + i + ";" + ps.GloablBestFitness + ";" + "\t\n");
            }
        }
        private void btn_PSOFCMRR_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_AutoAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cb_data.Items.Count; i++)
            {
                cb_data.SelectedIndex = i;
                btn_Load_Click(sender, null);
                btn_FCM_Click(sender, null);
                btn_PSOFCM_Click(sender, null);
                btn_PSOFCMRR_Click(sender, null);
            }
        }
    }
}
