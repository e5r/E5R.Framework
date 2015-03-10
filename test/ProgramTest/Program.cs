using System;
using System.Text;
using E5R.Framework.Security.Auth;

namespace E5R.Framework.ProgramTest
{
    public class Program
    {
        public void Main()
        {
            Console.WriteLine("E5R.Framework Manual Tests Program");

            Console.WriteLine(new AuthId());
            Console.WriteLine(new AuthToken());
        }
    }
}
