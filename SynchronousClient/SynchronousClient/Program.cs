using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SynchronousSocketClient.StartClient();
            Console.ReadLine();
        }
    }
}
