namespace CodeWars.Kyu6.SupermarketQueue
{
    /// <summary>
    /// https://www.codewars.com/kata/the-supermarket-queue/
    /// </summary>
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Kata
    {
        public static long QueueTime(int[] customers, int n)
        {
            var workerPool = new Dictionary<int, int>();
            Stack<int> freeWorkers = new Stack<int>(n);
            // init
            foreach (var workerNumber in Enumerable.Range(0, n))
            {
                freeWorkers.Push(workerNumber);
                workerPool.Add(workerNumber, 0);
            }
            int queueTime = 0;
            int customerPointer = 0;
            bool workToBeDone = customerPointer < customers.Length;
            while (workToBeDone)
            {
                //customer load
                while (freeWorkers.Count > 0 && customerPointer < customers.Length)
                {
                    //worker assignment
                    workerPool[freeWorkers.Pop()] = customers[customerPointer++];
                }
                int workload = workerPool
                    .Where(kvp => kvp.Value != 0).Min(kvp => kvp.Value);
                queueTime += workload;
                //tick
                foreach (var workerNumber in new List<int>(workerPool.Keys))
                {
                    int currentWorkerLoad = workerPool[workerNumber] - workload;
                    //process
                    if (currentWorkerLoad <= 0)
                    {
                        workerPool[workerNumber] = 0;
                        freeWorkers.Push(workerNumber);
                    }else{
                        workerPool[workerNumber] = currentWorkerLoad;
                    }
                }
                workToBeDone = workerPool.Values.Any(el => el > 0) || customerPointer < customers.Length;
            }
            return queueTime;
        }
    }
}