using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibSpline
{
    public class RawData
    {
        public double Start { get; set; }
        public double End { get; set; }
        public int NodeCnt { get; set; }
        public bool IsUnifgorm { get; set; }
        public FRaw Func { get; set; }
        public double[] Grid { get; set; }
        public double[] Field { get; set; }
        public RawData(double left, double right, int nodeCnt, bool isUnifgorm, FRaw func)
        {
            Start = left;
            End = right;
            NodeCnt = nodeCnt;
            IsUnifgorm = isUnifgorm;
            Func = func;
            Grid = new double[nodeCnt];
            Field = new double[nodeCnt];

            Grid[0] = left;
            Grid[NodeCnt - 1] = right;

            if (!isUnifgorm)
            {
                Random rnd = new Random();
                for (int i = 1; i < nodeCnt - 1; ++i)
                {
                    Grid[i] = Start + rnd.NextDouble() * (End - Start);
                }
                Array.Sort(Grid);
            }
            else
            {
                for (int i = 1; i < nodeCnt - 1; ++i)
                {
                    Grid[i] = Start + i * (End - Start) / NodeCnt;
                }
            }

            for (int i = 0; i < nodeCnt; ++i)
            {
                Field[i] = func(Grid[i]);
            }
        }
        public RawData(string filename)
        {
            try
            {
                Load(filename, out RawData rawData);
                Start = rawData.Start;
                End = rawData.End;
                NodeCnt = rawData.NodeCnt;
                IsUnifgorm = rawData.IsUnifgorm;
                Func = rawData.Func;
                Grid = rawData.Grid;
                Field = rawData.Field;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public void Save(string filename)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                fs = File.Create(filename);
                writer = new StreamWriter(fs);
                writer.WriteLine(Start.ToString());
                writer.WriteLine(End.ToString());
                writer.WriteLine(NodeCnt.ToString());
                writer.WriteLine(IsUnifgorm.ToString());
               
                for (int i = 0; i < NodeCnt; ++i)
                {
                    writer.WriteLine(Grid[i].ToString());
                }
                
                for (int i = 0; i < NodeCnt; ++i)
                {
                    writer.WriteLine(Field[i].ToString());
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving: {ex}");
                throw;
            }
            finally
            {
                writer.Dispose();
                fs.Close();
            }
        }
        public static void Load(string filename, out RawData rawData)
        {
            FileStream fs = null;
            StreamReader reader = null;
            try
            {
                double left, right;
                int node_cnt;
                bool is_uniform;
                fs = File.OpenRead(filename);
                reader = new StreamReader(fs);
                left = double.Parse(reader.ReadLine());
                right = double.Parse(reader.ReadLine());
                node_cnt = Convert.ToInt32(reader.ReadLine());
                is_uniform = Convert.ToBoolean(reader.ReadLine());

                rawData = new RawData(left, right, node_cnt, is_uniform, FRawLinear);
                
                for (int i = 0; i < node_cnt; ++i)
                {
                    rawData.Grid[i] = double.Parse(reader.ReadLine());
                }
                
                for (int i = 0; i < node_cnt; ++i)
                {
                    rawData.Field[i] = double.Parse(reader.ReadLine());
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading: {ex}");
                throw;
            }
            finally
            {
                reader.Dispose();
                fs.Close();
            }
        }
        public static double FRawLinear(double x)
        {
            return x;
        }
        public static double FRawCubic(double x)
        {
            return x * x * x ;
        }
        public static double FRawRandom(double x)
        {
            Random rand = new Random();
            return rand.NextDouble();
        }
    }
}
