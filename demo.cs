using System;
using System.Threading;

namespace FishTime
{
    class Program
    {
        static void Main(string[] args)
        {
            FishDataSource fishDataSource = new FishDataSource();

            for (; ; )
            {
                FishData fishData = fishDataSource.RetrieveFishData();
                if (fishData != null)
                    FishTimeDataStorage.Instance.AppendRecord(fishData);
                Thread.Sleep(1000);
            }
        }
    }
}
