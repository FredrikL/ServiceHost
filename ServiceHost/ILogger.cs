using System;

namespace ServiceHost
{
    public interface ILogger
    {
        void Info(string msg);
        void Debug(string msg);
        void Error(string msg);
    }

    public class ConsoleLogger : ILogger
    {
        public void Info(string msg)
        {
            Console.WriteLine("Info: " + msg);
        }

        public void Debug(string msg)
        {
            Console.WriteLine("Debug: " + msg);
        }

        public void Error(string msg)
        {
            Console.WriteLine("Error: " + msg);
        }
    }
}