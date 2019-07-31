using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpBasicsDemo
{
    public static class TPLExecution
    {
        public static void Invoke()
        {
            Console.WriteLine("START\n");

            var t1 = Task.Factory.StartNew(() => MakeBreakfast("Slava", 2000)).ContinueWith((t) => MakeDinner("Slava", 3000)); ;
            var t2 = Task.Factory.StartNew(() => MakeBreakfast("Eugene", 1000)).ContinueWith((t) => MakeDinner("Eugene", 2000));

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(">> Doing some housecleaning\n");
                Thread.Sleep(500);
            }

            Task.WaitAll(t1, t2);

            Console.WriteLine("FINISH");
            Console.ReadKey();
        }

        static void MakeBreakfast(string name, int time)
        {
            Console.WriteLine("{0} started making breakfast\n", name);
            Thread.Sleep(time);
            Console.WriteLine("{0} finished making breakfast\n", name);
        }

        static void MakeDinner(string name, int time)
        {
            Console.WriteLine("{0} started making dinner\n", name);
            Thread.Sleep(2000);
            Console.WriteLine("{0} finished making diner\n", name);
        }
    }
}
