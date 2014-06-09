using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonusEV2
{
    public enum LineValue
    { }

    public class Class1
    {
        public const decimal DeadHandEV = -6.0m;
        public const int SampleSize = 500;
        public const int DeckSize = 52;
        public static Random random = new Random(DateTime.Now.Millisecond);

        public decimal Evaluate(byte[] heroHand, byte[] deck)
        {
            LineValue shortValue;
            LineValue middleValue;
            LineValue topValue;

            lookAtHand(heroHand, out shortValue, out middleValue, out topValue);

            decimal score;
            if (canGetScore(shortValue, middleValue, topValue, out score))
                return score;

            decimal sum = 0m;
            var heroHandCopy = new byte[13];
            var emptyIndexPack = determineEmptyIndexes(heroHand);
            for (int i = 0; i < SampleSize; i++)
            {
                heroHand.CopyTo(heroHandCopy, 0);
                byte[] triple = pickRandomThree(deck);
                sum += EvaluateTriple(heroHandCopy, triple, emptyIndexPack, deck);
                rePickTriple(deck, triple);

            }
            return sum / SampleSize;
        }

        private void rePickTriple(byte[] deck, byte[] triple)
        {
            deck[triple[0]] = 0;
            deck[triple[1]] = 0;
            deck[triple[2]] = 0;
        }

        private byte[] pickRandomThree(byte[] deck)
        {
            byte[] result = new byte[3];

            var first = random.Next(DeckSize);
            while (deck[first] == 1) first = random.Next(DeckSize);
            deck[first] = 1;
            result[0] = (byte)first;

            var second = random.Next(DeckSize);
            while (deck[second] == 1) second = random.Next(DeckSize);
            deck[second] = 1;
            result[1] = (byte)second;

            var third = random.Next(DeckSize);
            while (deck[third] == 1) third = random.Next(DeckSize);
            deck[third] = 1;
            result[2] = (byte)third;

            return result;
        }

        public decimal EvaluateTriple(byte[] heroHand, byte[] triple, List<byte[]> setOfIndexPairs, byte[] deck)
        {
            decimal maxScore = -1000m;
            foreach (var pair in setOfIndexPairs)
            {
                var score = placeCardsAndEval(heroHand, pair[0], pair[1], triple[0], triple[1], deck);
                if (score > maxScore) maxScore = score;
            }
            return maxScore;
        }

        public decimal placeCardsAndEval(byte[] heroHand, byte firstIndex, byte secondIndex, byte firstCard, byte secondCard, byte[] deck)
        {
            heroHand[firstIndex] = firstCard;
            heroHand[secondIndex] = secondCard;

            var result = Evaluate(heroHand, deck);

            heroHand[firstIndex] = 0;
            heroHand[secondIndex] = 0;
            return result;
        }
    }
}
