using System;
using ServiceHost;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                new Host().Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.ReadLine();
        }
    }
}