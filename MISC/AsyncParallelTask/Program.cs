namespace AsyncParallelTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Payment.RunAsyncSemaphore();
        }
    }
}