using System;
// to convert [] to a list of `things`
using System.Linq;

namespace WindowsFormsApp1
{
    static class MathMethods
    {
        public static void Summarize(decimal[] values, out decimal max, out decimal min, out decimal avg, out decimal sum)
        {
            var list = values.ToList();

            max = list.Max();
            min = list.Min();
            avg = Math.Round(list.Average(), 2);
            sum = Math.Round(list.Sum(), 2);
        }
    }
}
