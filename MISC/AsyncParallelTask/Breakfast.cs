using System.Diagnostics;

namespace AsyncParallelTask
{
    public static class Breakfast
    {

        public static void Coffee()
             => Thread.Sleep(1000);

        public static void HeatPan()
           => Thread.Sleep(1000);

        public static void Eggs()
           => Thread.Sleep(1000);

        public static void Bacon()
           => Thread.Sleep(1000);

        public static void Bread()
           => Thread.Sleep(1000);

        public static void Juice()
           => Thread.Sleep(1000);



        public static async Task CoffeeAsync()
     => await Task.Delay(1000);

        public static async Task HeatPanAsync()
           => await Task.Delay(1000);

        public static async Task EggsAsync()
           => await Task.Delay(1000);

        public static async Task BaconAsync()
           => await Task.Delay(1000);

        public static async Task BreadAsync()
           => await Task.Delay(1000);

        public static async Task JuiceAsync()
           => await Task.Delay(1000);



        public static void DoBreakfast()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciamos el desayuno");

            Coffee();
            Console.WriteLine("Cafe listo");

            HeatPan();
            Console.WriteLine("Sarten listo");

            Eggs();
            Console.WriteLine("Huevito listo");

            Bacon();
            Console.WriteLine("Tocino listo");

            Bread();
            Console.WriteLine("Pan listo");

            Juice();
            Console.WriteLine("Jugo listo");

            stopwatch.Stop();

            Console.WriteLine($"pasaron {stopwatch.Elapsed}");


        }



        public static async Task DoBreakfastAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciamos el desayuno");

            var panTask = HeatPanAsync();
            Console.WriteLine("Sarten listo");

            var coffeeTask = CoffeeAsync();
            Console.WriteLine("Cafe listo");

            await panTask;
            var eggsTask = EggsAsync();
            Console.WriteLine("Huevito listo");

            var baconTask = BaconAsync();
            Console.WriteLine("Tocino listo");

            var breadTask = BreadAsync();
            Console.WriteLine("Pan listo");

            var juiceTask = JuiceAsync();
            Console.WriteLine("Jugo listo");

            await coffeeTask;
            await eggsTask;
            await baconTask;
            await breadTask;
            await juiceTask;

            stopwatch.Stop();

            Console.WriteLine($"pasaron {stopwatch.Elapsed}");
        }

    }
}
