using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibSpline
{
    public enum FRawEnum
    {
        Linear,
        Cubic,
        Random
    }
    public delegate double FRaw(double x);
}
