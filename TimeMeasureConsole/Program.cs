using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple;
using BonusEV2;

namespace TimeMeasureConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Go!");
            var start = DateTime.Now;
            for (int i = 0; i < 5000; i++)
            {
                var objectUnderTest = new Predictor();
                methodAgainstAlmostEmpty(objectUnderTest);
                methodAgainstEmpty(objectUnderTest);
            }
            var stop = DateTime.Now;
            Console.WriteLine((stop.Ticks - start.Ticks)/10000);
   //         Console.WriteLine(stop.ToLongTimeString());
            Console.ReadKey();
        }


        public const string Deck1 = "Ac Js Th Td Tc Jh 2d 2h 3h 3s "
                + "4h 4c 4s 4d 5c 5s 5h 5d 6s 6h "
                + "9c 9s 9d 9h Ts Js Jd Jc Qd Qh Kd Kc";

        public const string Deck2 = "Qd 3d 5d 3s 4s 7s 2h 2s 2c 3h "
            + "4s 4c 4d 5c 5s 5h 6h 6s 8s 8c " +
            "9c 9d Td Th Tc Ts Jh Jc Js Kc";

        static void methodAgainstAlmostEmpty(Predictor objectUnderTest)
        {
            byte[] heroHand = InputReader.ReadInput("Qs Qc 6c 2c 3c 6d Ad ? 7d 7c 7s 8c ?");
            byte[] deck = InputReader.ReadDeck(Deck1);
            objectUnderTest.Evaluate(heroHand, deck);
        }

        static void methodAgainstEmpty(Predictor objectUnderTest)
        {
            byte[] heroHand = InputReader.ReadInput("6d Qs ? 7h Kh Ks 4d 3h Ad Jd 8d 2d ?");
            byte[] deck = InputReader.ReadDeck(Deck2);
            objectUnderTest.Evaluate(heroHand, deck);
        }
    }
}
