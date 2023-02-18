using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelTask
{
    public static class DataGenerator
    {
        public static IEnumerable<int> GetPendingPayments(int count) 
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Random().Next(1000);
            }
        
        }

    }
}
