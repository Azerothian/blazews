using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Service s = new Service("1","1", "http://blazews.localhost.com/"))
            {
                var all = s.Application.GetAll();
                foreach (var a in all)
                {
                    System.Console.WriteLine(a.Name);
                }
            }
            System.Console.WriteLine("Waiting...");
            System.Console.ReadLine();
        }
    }
}
