using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ClassLibSpline;

namespace SplineWPFLab
{
    public class ViewData : IDataErrorInfo
    {
        public double Start { get; set; }
        public double End { get; set; }
        public int NodeCnt { get; set; }
        public bool IsUniform { get; set; }
        public FRawEnum Func { get; set; }
        public double LeftDer { get; set; }
        public double RightDer { get; set; }
        public double[] Ders { get; set; }
        public int NodeCntSpline { get; set; }

        public string SplineIntegral { get; set; }

        public string Error
        {
            get { return "Non correct data"; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "NodeCnt":
                        if (NodeCnt < 2)
                            result = "Интерполируемая функция должна быть измерена \n хотя бы в двух точках!";
                        break;

                    case "NodeCntSpline":
                        if (NodeCntSpline < 2)
                            result = "Интерполяция должна проводиться \n хотя бы в двух точках!";
                        break;

                    case "End":
                        if (End <= Start)
                            result = "Правый конец отрезка должен \n быть больше левого!";
                        break;
                }
                return result;
            }
        }
        public RawData data;
        public SplineData spline;
        public ViewData() { }

        public void Save(string filename)
        {
            if (data is null)
            {
                MessageBox.Show("Nothing to save!");
                return;
            }
            try
            {
                data.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex}");
            }
        }

        public void Load(string filename)
        {
            try
            {
                RawData.Load(filename, out data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex}");
            }
        }

        public class RegexConverter : IValueConverter
        {
            public string Format;
            public int Num;
            public RegexConverter(string format, int num) => (Format, Num) = (format, num);
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return "";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var reg = Regex.Matches((string)value ?? "-1", Format);
                return reg?.Count != Num ? null : Num == 1 ? Double.Parse(reg[0].Value) : new double[] { Double.Parse(reg[0].Value), Double.Parse(reg[1].Value) };
            }
        }
    }
}
