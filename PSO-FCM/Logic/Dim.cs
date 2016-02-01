using System.Windows.Forms;

namespace PSO_FCM.Logic
{
    public class Dim
    {
        public double[] Val { get; set; }
        public Dim(int length)
        {
            Val=new double[length];
        }
        public Dim(double[] data)
        {
            Val = data;
        }
    }
}