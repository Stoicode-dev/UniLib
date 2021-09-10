using System;

namespace Stoicode.UniLib.Utilities
{
    public static class ValueUtils
    {
        public static bool Compare(float value, float target, int units)
        {
            return Compare((double) value, target, units);
        }

        public static bool Compare(float value, float target)
        {
            return Compare((double) value, target, 1);
        }

        public static bool Compare(double value, double target)
        {
            return Compare(value, target, 1);
        }

        public static bool Compare(double value, double target, int units)
        {
            var value1 = BitConverter.DoubleToInt64Bits(value);
            var value2 = BitConverter.DoubleToInt64Bits(target);

            if ((value1 >> 63) != (value2 >> 63))
                return System.Math.Abs(value - target) < 0f;

            var diff = System.Math.Abs(value1 - value2);

            return diff <= units;
        }
    }
}