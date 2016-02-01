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
using PSO_FCM.Logic.PSO;
using PSO_FCM.Utility;

namespace PSO_FCM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var data = Loader.LoadData();
            MessageBox.Show(data[0].CluseterName);
            double w = GeneralCom.GetRandom(0.1, 0.9);
            double c1 = GeneralCom.GetRandom(0.1, 0.9);
            double c2 = GeneralCom.GetRandom(0.1, 0.9);
            double rate = Math.Pow(10, -100);
            Pso ps = new Pso(3, data.Count, 2, w, c1, c2, 30, data[0].DataDim.Val.Length, data);
            File.WriteAllText("log", "");
            for (int i = 0; i < 200; i++)
            {
                ps.Calc();
                File.AppendAllText("log",ps.GloablBestError+"\t;\t"+ps.Variance+"\t\n");
                if(ps.Variance<rate)
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
            MessageBox.Show("d");
        }
    }
}
