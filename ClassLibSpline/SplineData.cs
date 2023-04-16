using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibSpline
{
    public class SplineData
    {
        public RawData Data { get; set; }
        public int NodeCnt { get; set; }
        public double StartDer { get; set; }
        public double EndDer { get; set; }
        public List<SplineDataItem> DataItems { get; set; }
        public double Integral { get; set; }
        public SplineData(RawData rawData, double leftSecDer, double rightSecDer, int nodeCnt)
        {
            Data = rawData;
            NodeCnt = nodeCnt;
            StartDer = leftSecDer;
            EndDer = rightSecDer;
            DataItems = new List<SplineDataItem>();
        }

        public SplineData(RawData rawData, double[] ders, int nodeCnt)
        {
            Data = rawData;
            NodeCnt = nodeCnt;
            StartDer = ders[0];
            EndDer = ders[1];
            DataItems = new List<SplineDataItem>();
        }

        public void Interpolate()
        {
            double[] field = new double[NodeCnt];
            double[] firstDer = new double[NodeCnt];
            double[] secDer = new double[NodeCnt];

            int nx = Data.NodeCnt;
            double[] x = new double[nx];
            Array.Copy(Data.Grid, x, nx);

            int ny = 1;
            double[] y = new double[nx * ny];
            Array.Copy(Data.Field, y, nx * ny);

            int nsite = NodeCnt;
            int ret = 0;
            double[] site = new double[nsite];
            site[0] = Data.Start;
            site[nsite - 1] = Data.End;
            for (int i = 1; i < NodeCnt - 1; ++i)
            {
                site[i] = site[i - 1] + (Data.End - Data.Start) / NodeCnt;
            }

            double[] scoeff = new double[4 * (nx - 1) * ny];
            int[] dorder = new int[3] { 1, 1, 1 };
            double[] results = new double[3 * ny * nsite];

            double[] llim = new double[1] { Data.Start };
            double[] rlim = new double[1] { Data.End };

            double[] ders = new double[2] { StartDer, EndDer };

            double[] integrals = new double[1];
            try
            {
                CubeInterpolate(nx, Data.IsUnifgorm ? new double[] { Data.Start, Data.End } : x, ny, y, ders, scoeff, nsite, Data.IsUnifgorm ? new double[] { site[0], site[nsite - 1] } : site, 3, dorder, results, 1, llim, rlim, integrals, ref ret, Data.IsUnifgorm);
                Integral = integrals[0];
                for (int i = 0; i < results.Length; ++i)
                {
                    switch (i % 3)
                    {
                        case 0:
                            field[i / 3] = results[i];
                            break;

                        case 1:
                            firstDer[i / 3] = results[i];
                            break;
                        
                        case 2:
                            secDer[i / 3] = results[i];
                            break;
                    }
                }
                for (int i = 0; i < NodeCnt; ++i)
                {
                    DataItems.Add(new SplineDataItem(site[i], field[i], firstDer[i], secDer[i]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //[DllImport("..\\..\\..\\..\\x64\\Debug\\DLL.MKL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CubeInterpolate(int nx, double[] x, int ny, double[] y, double[] ders, double[] scoeff, int nsite,
            double[] site, int ndorder, int[] dorder, double[] results, int nlim, 
            double[] l_lims, double[] r_lims, double[] int_results, ref int ret, bool isUniform);
    }
}
