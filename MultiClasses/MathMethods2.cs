namespace WindowsFormsApp1
{
    // static classes are used as container to hold methods
    static class MathMethods
    {
        // public, static help, so easier to access from Form1.cs
        public static decimal Add(decimal x, decimal y)
        {
            return x + y;
        }

        public static decimal Divide (decimal x, decimal y)
        {
            decimal result;
            if (y != 0)
            {
                result = x / y;
            }
            else
            {
                result = 0.00m;
            }

            return result;
        }
    }
}
