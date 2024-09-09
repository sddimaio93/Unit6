﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT481_Unit6_ShawneeDimaio
{
    internal class DressingRooms
    {
        int rooms;
        Semaphore semaphore;
        long waitTimer;
        long runTimer;

        public DressingRooms()
        {
            rooms = 3;
            semaphore = new Semaphore(rooms, rooms);
        }

        public DressingRooms(int r)
        {
            rooms = r;
            semaphore = new Semaphore(rooms, rooms);
        }

        public async Task RequestRoom(Customer c)
        {
            Stopwatch stopWatch = new Stopwatch();
            Console.WriteLine("Customer is waiting");

            stopWatch.Start();
            semaphore.WaitOne();
            stopWatch.Stop();
            waitTimer += stopWatch.ElapsedTicks;

            int roomWaitTime = GetRandomNumber(1, 3);
            stopWatch.Start();
            Thread.Sleep((roomWaitTime * c.getNumberOfItems()));

            stopWatch.Stop();
            runTimer += stopWatch.ElapsedTicks;

            Console.WriteLine("Customer finished trying on items in room");
            semaphore.Release();
        }
        public long getWaitTime()
        {
            return waitTimer;
        }

        public long getRunTime()
        {
            return runTimer;
        }

        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
    }
}
