using System.Collections.Generic;
using System.IO;
using PSO_FCM.Logic;

namespace PSO_FCM.Utility
{
    public static class Loader
    {
        public static List<Data> LoadData(string fileName = "wine.data")
        {
            var lines= File.ReadAllLines(fileName);
            var data = new List<Data>(lines.Length);
            foreach (string line in lines)
            {
                var lined = line.Split(',');
                var dim = new double[lined.Length - 1];
                
                for (int i = 1; i < lined.Length; i++)
                {
                    double.TryParse(lined[i], out dim[i - 1]);
                }
                data.Add(new Data
                {
                    CluseterName = lined[0],
                    DataDim = new Dim(dim)
                });
            }
            return data;
        }
    }
}