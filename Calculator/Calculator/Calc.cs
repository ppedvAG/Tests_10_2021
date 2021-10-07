using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Calculator.Tests")]

namespace Calculator
{
    public class Calc
    {
        public int Sum(int a, int b)
        {
   
            return checked(a + b);
        }

        internal int Minus(int a, int b)
        {
            return a - b;
        }
    }
}