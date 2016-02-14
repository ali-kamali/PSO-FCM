using System.Linq;
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

        public override string ToString()
        {
            return Val.Aggregate("", (current, d) => current + (d + ","));
        }
    }
}