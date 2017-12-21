using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.GenericPropertyList.Utilities
{
    public static class MathHelper
    {
        public static double ToSign(double value)
        {
            return value >= 0 ? 1 : -1;
        }

        public static double ToDegree(double angle)
        {
            return angle * 180.0 / Math.PI;
        }

        public static double ToRadian(double angle)
        {
            return angle * Math.PI / 180.0;
        }

        public static int RoundInt(double number)
        {
            return (int)Math.Round(number, 0, MidpointRounding.AwayFromZero);
        }

        public static int GetCos(double number, double angleInDegree)
        {
            return RoundInt(Math.Cos(ToRadian(angleInDegree)) * number);
        }

        public static int GetSin(double number, double angleInDegree)
        {
            return RoundInt(Math.Sin(ToRadian(angleInDegree)) * number);
        }

        public static bool AreNumbersClose(int numberA, int numberB, int threshold)
        {
            int difference = numberA - numberB;
            return (Math.Abs(difference) < threshold);
        }
    }
}
