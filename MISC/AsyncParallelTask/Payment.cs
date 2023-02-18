using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelTask
{
    public class Payment
    {
        public static async Task Run()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();


            //obtenermos una lista de pagos pendientes
            var PayCards = DataGenerator.GetPendingPayments(1_000_000);
            var client = new RestService();

            var result = new List<(int, bool)>();


            foreach (var item in PayCards)
            {
                result.Add(await client.Pay(item));
            }

            stopWatch.Stop();
            Console.WriteLine($"Transcurrieron {stopWatch.Elapsed}");


        }



        public static async Task RunAsync()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();


            //obtenermos una lista de pagos pendientes
            var PayCards = DataGenerator.GetPendingPayments(1_000_000).ToList();
            var client = new RestService();

            var result = new List<Task<(int, bool)>>();


            foreach (var item in PayCards)
            {
                result.Add(client.Pay(item));
            }

            var taskResult = await Task.WhenAll(result);
            stopWatch.Stop();
            Console.WriteLine($"Transcurrieron {stopWatch.Elapsed}");

        }


        public static async Task RunAsyncSemaphore()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();


            //obtenermos una lista de pagos pendientes
            var PayCards = DataGenerator.GetPendingPayments(1_000).ToList();
            var client = new RestService();

            var result = new List<Task<(int, bool)>>();

            var semaphore = new SemaphoreSlim(1000);

            result = PayCards.Select(async payment =>
            {
                await semaphore.WaitAsync();

                try
                {
                    return await client.Pay(payment);
                }
                finally
                {

                    semaphore.Release();
                }
            }).ToList();



            var taskResult = await Task.WhenAll(result);
            stopWatch.Stop();
            Console.WriteLine($"Transcurrieron {stopWatch.Elapsed}");

        }


    }
}
