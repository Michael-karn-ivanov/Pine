using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple;

namespace TimeMeasureConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var start = DateTime.Now;
            for (int i = 0; i < 5000; i++)
            {
                var objectUnderTest = new LastTurnInPosition();
                methodAgainstAlmostEmpty(objectUnderTest);
                methodAgainstEmpty(objectUnderTest);
            }
            var stop = DateTime.Now;
            Console.WriteLine((stop.Ticks - start.Ticks)/10000);
   //         Console.WriteLine(stop.ToLongTimeString());
            Console.ReadKey();
        }

        static void methodAgainstAlmostEmpty(LastTurnInPosition objectUnderTest)
        {
            var heroHand = InputReader.ReadInput("9h 9s ? Th Ts Td 6s ? Jh Js Jd Qh Qs");
            var villainHand = InputReader.ReadInput("2h 3s 4d 2s 3h 4c 5c 7d 2d 3d 4s 5d 8s");
            var triple = InputReader.ReadInput("9d 6d Ad");
            int outFirstIdx;
            int outSecondIdx;
            objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);
        }

        static void methodAgainstEmpty(LastTurnInPosition objectUnderTest)
        {
            var heroHand = InputReader.ReadInput("Qh 9s 6h Ts Qs As 6s ? Jh Jc Jd Kh ?");
            var villainHand = InputReader.ReadInput("2h 3s 4d 2s 3h 4c 5c 7d 2d 3d 4s 5d 8s");
            var triple = InputReader.ReadInput("Js Tc Td");
            int outFirstIdx;
            int outSecondIdx;
            objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);
        }
    }
}
