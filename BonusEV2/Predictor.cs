using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonusEV2
{
    public enum LineValue
    {
        Highcard = 0,
        Pair = 1,
        TwoPairs = 2,
        Set = 3,
        Straight = 4,
        Flash = 5,
        FullHouse = 6,
        Care = 7,
        StraightFlash = 8,
        Royal = 9,
        Highcard1E = 10,
        Pair1E = 11,
        TwoPairs1E = 12,
        Set1E = 13,
        Straight1E = 14,
        Flash1E = 15,
        Care1E = 16,
        StraightFlash1E = 17,
        Royal1E = 18,
        Highcard2E = 19,
        Pair2E = 20,
        Set2E = 21,
        Straight2E = 22,
        Flash2E = 23,
        StraightFlash2E = 24,
        Royal2E = 25,
        Highcard3E = 26,
        Pair3E = 27,
        Straight3E = 28,
        Flash3E = 29,
        StraightFlash3E = 30,
        Royal3E = 31,
        E4 = 32,
        E5 = 33,
        E3 = 34
    }

    public class Predictor
    {
        public const decimal DeadHandEV = -6.0m;
        public const int SampleSize = 500;
        public const int DeckSize = 52;
        public static Random random = new Random(DateTime.Now.Millisecond);
        public const decimal FantazyEV = 11.5m;

        public decimal Evaluate(byte[] heroHand, byte[] deck)
        {
            LineValue shortValue;
            LineValue middleValue;
            LineValue topValue;
            bool isFantasyGained = false;

            byte[] shortCount;
            byte[] middleCount;
            byte[] topCount;
            lookAtHand(heroHand, out shortValue, out middleValue, out topValue, out isFantasyGained, out shortCount, out middleCount, out topCount);

            decimal score;
            if (canGetScore(shortValue, middleValue, topValue, isFantasyGained, out score, shortCount, middleCount, topCount))
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

        public bool canGetScore(LineValue shortValue, LineValue middleValue, LineValue topValue, bool isFantasyGained, out decimal score, byte[] shortCount, byte[] middleCount, byte[] topCount)
        {
            #region мертвые по комбинациям
            if ((valueIn(topValue, LineValue.StraightFlash) && valueIn(middleValue, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Straight3E) && valueIn(middleValue, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Flash3E) && valueIn(middleValue, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Care, LineValue.Care1E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.FullHouse) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E)) ||
                (valueIn(topValue, LineValue.Flash, LineValue.Flash1E, LineValue.Flash2E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse)) ||
                (valueIn(topValue, LineValue.Straight, LineValue.Straight1E, LineValue.Straight2E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash)) ||
                (valueIn(topValue, LineValue.Set2E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Set1E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Set) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash, LineValue.Straight)) ||
                (valueIn(topValue, LineValue.TwoPairs1E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E)) ||
                (valueIn(topValue, LineValue.TwoPairs) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash, LineValue.Straight, LineValue.Set, LineValue.Set1E, LineValue.Set2E)) ||
                (valueIn(topValue, LineValue.Pair3E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Pair2E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Pair1E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash, LineValue.Straight)) ||
                (valueIn(topValue, LineValue.Pair) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash, LineValue.Straight, LineValue.Set, LineValue.Set1E, LineValue.Set2E, LineValue.TwoPairs1E, LineValue.TwoPairs)) ||
                (valueIn(topValue, LineValue.Highcard3E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal)) ||
                (valueIn(topValue, LineValue.Highcard2E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash, LineValue.Straight)) ||
                (valueIn(topValue, LineValue.Highcard1E) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash, LineValue.Straight, LineValue.Set, LineValue.Set1E, LineValue.Set2E, LineValue.TwoPairs1E, LineValue.TwoPairs)) ||
                (valueIn(topValue, LineValue.Highcard) && valueIn(middleValue, LineValue.StraightFlash, LineValue.Royal, LineValue.Care, LineValue.Care1E, LineValue.FullHouse, LineValue.Flash, LineValue.Straight, LineValue.Set, LineValue.Set1E, LineValue.Set2E, LineValue.TwoPairs1E, LineValue.TwoPairs, LineValue.Pair3E, LineValue.Pair2E, LineValue.Pair1E, LineValue.Pair))
            )
            {
                score = DeadHandEV;
                return true;
            }
            if((valueIn(middleValue, LineValue.Pair, LineValue.Highcard1E) && valueIn(shortValue, LineValue.Set)) ||
                (valueIn(middleValue, LineValue.Highcard) && valueIn(shortValue, LineValue.Pair)))
            {
                score = DeadHandEV;
                return true;
            }
            #endregion
            #region str flash
            if (topValue == LineValue.StraightFlash && middleValue == LineValue.StraightFlash)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] > 0 && middleCount[i] == 0) break;
                    else if (topCount[i] == 0 && middleCount[i] > 0)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region care
            if (valueIn(topValue, LineValue.Care, LineValue.Care1E) && valueIn(middleValue, LineValue.Care, LineValue.Care1E))
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 4 && middleCount[i] != 4) break;
                    else if (topCount[i] != 4 && middleCount[i] == 4)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region full house
            if (topValue == LineValue.FullHouse && middleValue == LineValue.FullHouse)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 3 && middleCount[i] != 3) break;
                    else if (topCount[i] != 3 && middleCount[i] == 3)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region Flash
            if (topValue == LineValue.Flash && middleValue == LineValue.Flash)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] > 0 && middleCount[i] == 0) break;
                    else if (topCount[i] == 0 && middleCount[i] > 0)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region Straight
            if (topValue == LineValue.Straight && middleValue == LineValue.Straight)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] > 0 && middleCount[i] == 0) break;
                    else if (topCount[i] == 0 && middleCount[i] > 0)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region Set
            if (topValue == LineValue.Set && valueIn(middleValue, LineValue.Set, LineValue.Set1E, LineValue.Set2E))
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 3 && middleCount[i] != 3) break;
                    else if (topCount[i] != 3 && middleCount[i] == 3)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region Two pairs
            if (topValue == LineValue.TwoPairs && valueIn(middleValue, LineValue.TwoPairs, LineValue.TwoPairs1E))
            {
                bool topPairUp = false;
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 2 && middleCount[i] != 2) { topPairUp = true; break; }
                    else if (topCount[i] != 2 && middleCount[i] == 2)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
                if (!topPairUp) 
                {
                    for (int i = 13; i > 0; i--)
                    {
                        if (topCount[i] == 1 && middleCount[i] == 0) { break; }
                        else if (topCount[i] == 0 && middleCount[i] == 1)
                        {
                            score = DeadHandEV;
                            return true;
                        }
                    }
                }
            }
            #endregion
            #region Pair
            if (topValue == LineValue.Pair && valueIn(middleValue, LineValue.Pair, LineValue.Pair1E, LineValue.Pair2E, LineValue.Pair3E))
            {
                bool topPairUp = false;
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 2 && middleCount[i] != 2) { topPairUp = true; break; }
                    else if (topCount[i] != 2 && middleCount[i] == 2)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
                if (!topPairUp)
                {
                    for (int i = 13; i > 0; i--)
                    {
                        if (topCount[i] == 1 && middleCount[i] == 0) { break; }
                        else if (topCount[i] == 0 && middleCount[i] == 1)
                        {
                            score = DeadHandEV;
                            return true;
                        }
                    }
                }
            }
            #endregion
            #region Highcard
            if (topValue == LineValue.Highcard && valueIn(middleValue, LineValue.Highcard, LineValue.Highcard1E, LineValue.Highcard2E, LineValue.Highcard3E, LineValue.E4))
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 1 && middleCount[i] == 0) { break; }
                    else if (topCount[i] == 0 && middleCount[i] == 1)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region set short
            if (shortValue == LineValue.Set && middleValue == LineValue.Set)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (middleCount[i] == 3 && shortCount[i] != 3) break;
                    else if (middleCount[i] != 3 && shortCount[i] == 3)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region pair short
            if (middleValue == LineValue.Pair && valueIn(shortValue, LineValue.Pair, LineValue.Pair1E))
            {
                bool topPairUp = false;
                for (int i = 13; i > 0; i--)
                {
                    if (middleCount[i] == 2 && shortCount[i] != 2) { topPairUp = true; break; }
                    else if (middleCount[i] != 2 && shortCount[i] == 2)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
                if (!topPairUp)
                {
                    for (int i = 13; i > 0; i--)
                    {
                        if (middleCount[i] == 1 && shortCount[i] == 0) { break; }
                        else if (middleCount[i] == 0 && middleCount[i] == 1)
                        {
                            score = DeadHandEV;
                            return true;
                        }
                    }
                }
            }
            #endregion
            #region highcard short
            if (middleValue == LineValue.Highcard && valueIn(shortValue, LineValue.Highcard, LineValue.Highcard1E, LineValue.Highcard2E))
            {
                for (int i = 13; i > 0; i--)
                {
                    if (middleCount[i] == 1 && shortCount[i] == 0) { break; }
                    else if (shortCount[i] == 0 && middleCount[i] == 1)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            score = 0;

            if (!valueIn(shortValue, LineValue.Highcard, LineValue.Pair, LineValue.Set) ||
                !valueIn(middleValue, LineValue.Highcard, LineValue.Pair, LineValue.TwoPairs, LineValue.Set, LineValue.Straight, LineValue.Flash, LineValue.FullHouse, LineValue.Care, LineValue.StraightFlash, LineValue.Royal) ||
                !valueIn(topValue, LineValue.Highcard, LineValue.Pair, LineValue.TwoPairs, LineValue.Set, LineValue.Straight, LineValue.Flash, LineValue.FullHouse, LineValue.Care, LineValue.StraightFlash, LineValue.Royal))
                return false;

            score += getTopBonus(topValue);
            if (middleValue == LineValue.Set)
                score += 2;
            else
                score += (2 * getTopBonus(middleValue));

            if (shortValue == LineValue.Set)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (shortCount[i] == 3)
                    {
                        score += (9 + i);
                        break;
                    }
                }
            }
            if (shortValue == LineValue.Pair)
            {
                for (int i = 13; i > 4; i--)
                {
                    if (shortCount[i] == 2)
                    {
                        score += (i - 4);
                        break;
                    }
                }
            }
            if (isFantasyGained) score += FantazyEV;
            return true;
        }

        public decimal getTopBonus(LineValue value)
        {
            switch (value)
            {
                case LineValue.Royal:
                    return 25;
                case LineValue.StraightFlash:
                    return 15;
                case LineValue.Care:
                    return 10;
                case LineValue.FullHouse:
                    return 6;
                case LineValue.Flash:
                    return 4;
                case LineValue.Straight:
                    return 2;
                default:
                    return 0;
            }
        }

        bool valueIn(LineValue value, params LineValue[] ins)
        {
            foreach (LineValue i in ins)
                if (value == i) return true;
            return false;
        }

        public void lookAtHand(byte[] heroHand, out LineValue shortValue, out LineValue middleValue, out LineValue topValue, out bool isFantasyGained,
            out byte[] shortCount, out byte[] middleCount, out byte[] topCount)
        {
            byte[] countValue;
            byte[] colorValue;
            countRange(heroHand, 0, 3, out countValue, out colorValue);
            shortCount = countValue;
            shortValue = getShortValue(countValue, colorValue, out isFantasyGained);
            countRange(heroHand, 3, 8, out countValue, out colorValue);
            middleCount = countValue;
            middleValue = getMiddleValue(countValue, colorValue);
            countRange(heroHand, 8, 13, out countValue, out colorValue);
            topCount = countValue;
            topValue = getMiddleValue(countValue, colorValue);
        }

        public LineValue getMiddleValue(byte[] countValue, byte[] colorValue)
        {
            if (countValue[0] == 5) return LineValue.E5;
            if (countValue[0] == 4) return LineValue.E4;
            #region 3 empty
            if (countValue[0] == 3)
            {
                int firstValue = -1;
                int secondValue = -1;
                bool sameColor = false;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 2) return LineValue.Pair3E;
                    else if (countValue[i] == 1)
                    {
                        if (firstValue > 0)
                        {
                            secondValue = i;
                            break;
                        }
                        else firstValue = i;
                    }
                }
                for (int i = 1; i < colorValue.Length; i++)
                {
                    if (colorValue[i] == 2)
                    {
                        sameColor = true;
                        break;
                    }
                    else if (colorValue[i] == 1) break;
                }
                if (firstValue >= 9 && secondValue >= 9)
                {
                    if (sameColor) return LineValue.Royal3E;
                    else return LineValue.Straight3E;
                }
                if ((firstValue <= 4 && secondValue == 13) || (secondValue - firstValue <= 4))
                {
                    if (sameColor) return LineValue.StraightFlash3E;
                    else return LineValue.Straight3E;
                }
                if (sameColor) return LineValue.Flash3E;
                return LineValue.Highcard3E;
            }
            #endregion
            #region 2 empty
            if (countValue[0] == 2)
            {
                int firstValue = -1;
                int secondValue = -1;
                int thirdValue = -1;
                bool sameColor = false;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 3) return LineValue.Set2E;
                    else if (countValue[i] == 2) return LineValue.Pair2E;
                    else if (countValue[i] == 1)
                    {
                        if (firstValue < 0) firstValue = i;
                        else if (secondValue < 0) secondValue = i;
                        else if (thirdValue < 0)
                        {
                            thirdValue = i;
                            break;
                        }
                    }
                }
                for (int i = 1; i < colorValue.Length; i++)
                {
                    if (colorValue[i] == 3) { sameColor = true; break; }
                    else if (colorValue[i] > 0) break;
                }
                if (firstValue >= 9 && thirdValue >= 9)
                {
                    if (sameColor) return LineValue.Royal2E;
                    else return LineValue.Straight2E;
                }
                if ((secondValue <= 4 && thirdValue == 13) || (thirdValue - firstValue <= 4))
                {
                    if (sameColor) return LineValue.StraightFlash2E;
                    else return LineValue.Straight2E;
                }
                if (sameColor) return LineValue.Flash2E;
                return LineValue.Highcard2E;
            }
            #endregion
            #region 1 empty
            if (countValue[0] == 1)
            {
                int firstValue = -1;
                int secondValue = -1;
                int thirdValue = -1;
                int fourthValue = -1;
                int firstPairIndex = -1;
                bool sameColor = false;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 4) return LineValue.Care1E;
                    else if (countValue[i] == 3) return LineValue.Set1E;
                    else if (countValue[i] == 2)
                    {
                        if (firstPairIndex >= 0) return LineValue.TwoPairs1E;
                        else
                        {
                            if (firstValue < 0) firstPairIndex = i;
                            else return LineValue.Pair1E;
                        }
                    }
                    else
                    {
                        if (firstPairIndex >= 0) return LineValue.Pair1E;
                        else
                        {
                            if (firstValue < 0) firstValue = i;
                            else if (secondValue < 0) secondValue = i;
                            else if (thirdValue < 0) thirdValue = i;
                            else fourthValue = i;
                        }
                    }
                }
                for (int i = 1; i < colorValue.Length; i++)
                {
                    if (colorValue[i] == 4) { sameColor = true; break; }
                    else if (colorValue[i] > 0) break;
                }

                if (firstValue >= 9 && fourthValue >= 9)
                {
                    if (sameColor) return LineValue.Royal1E;
                    else return LineValue.Straight1E;
                }
                if ((thirdValue <= 4 && fourthValue == 13) || (fourthValue - firstValue <= 4))
                {
                    if (sameColor) return LineValue.StraightFlash1E;
                    else return LineValue.Straight1E;
                }
                if (sameColor) return LineValue.Flash1E;
                return LineValue.Highcard1E;
            }
            #endregion
            #region 0 empty
            if (countValue[0] == 0)
            {
                int firstValue = -1;
                int secondValue = -1;
                int thirdValue = -1;
                int fourthValue = -1;
                int fifthValue = -1;
                bool hasPair = false;
                bool hasTriple = false;
                bool sameColor = false;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 4) return LineValue.Care;
                    else if (countValue[i] == 3)
                    {
                        if (hasPair) return LineValue.FullHouse;
                        hasTriple = true;
                    }
                    else if (countValue[i] == 2)
                    {
                        if (hasTriple) return LineValue.FullHouse;
                        else if (hasPair) return LineValue.TwoPairs;
                        else hasPair = true;
                    }
                    else
                    {
                        if (firstValue < 0) firstValue = i;
                        else if (secondValue < 0) secondValue = i;
                        else if (thirdValue < 0) thirdValue = i;
                        else if (fourthValue < 0) fourthValue = i;
                        else fifthValue = i;
                    }
                }
                if (hasTriple) return LineValue.Set;
                if (hasPair) return LineValue.Pair;

                for (int i = 1; i < colorValue.Length; i++)
                {
                    if (colorValue[i] == 5) { sameColor = true; break; }
                    else if (colorValue[i] > 0) break;
                }

                if (firstValue == 9 && fifthValue == 13)
                {
                    if (sameColor) return LineValue.Royal;
                    else return LineValue.Straight;
                }
                if (fourthValue == 4 && fifthValue == 5 || (fifthValue - firstValue) == 4)
                {
                    if (sameColor) return LineValue.StraightFlash;
                    else return LineValue.Straight;
                }
                if (sameColor) return LineValue.Flash;
                return LineValue.Highcard;
            }
            #endregion

            throw new Exception();
        }

        public LineValue getShortValue(byte[] countValue, byte[] colorValue, out bool isFantasyGained)
        {
            isFantasyGained = false;
            if (countValue[0] == 3) return LineValue.E3;
            if (countValue[0] == 2) return LineValue.Highcard2E;
            if (countValue[0] == 1)
            {
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 2)
                    {
                        isFantasyGained = i >= 11;
                        return LineValue.Pair1E;
                    }
                    else if (countValue[i] == 1) return LineValue.Highcard1E;
                }
                return LineValue.Highcard1E;
            }
            for (int i = 1; i < countValue.Length; i++)
            {
                if (countValue[i] == 3)
                {
                    isFantasyGained = true;
                    return LineValue.Set;
                }
                else if (countValue[i] == 2)
                {
                    isFantasyGained = i >= 11;
                    return LineValue.Pair;
                }
            }
            return LineValue.Highcard;
        }

        public void countRange(byte[] heroHand, int startIndexInclusive, int endIndexExclusive, out byte[] countValue, out byte[] colorValue)
        {
            countValue = new byte[14];
            colorValue = new byte[5];
            for (int i = startIndexInclusive; i < endIndexExclusive; i++)
            {
                if (heroHand[i] == 0)
                {
                    countValue[0]++;
                    colorValue[0]++;
                }
                else
                {
                    var b = (heroHand[i] - 1);
                    countValue[b % 13 + 1]++;
                    colorValue[(int)((b - b % 13) / 13) + 1]++;
                }
            }
        }

        public List<byte[]> determineEmptyIndexes(byte[] heroHand)
        {
            var result = new List<byte[]>();
            var topLineEmpty = new List<byte>();
            var middleLineEmpty = new List<byte>();
            var bottomLineEmpty = new List<byte>();

            for (byte i = 0; i < heroHand.Length; i++)
            {
                if (heroHand[i] == 0)
                {
                    if (i < 3)
                    {
                        topLineEmpty.Add(i);
                        continue;
                    }
                    if (i < 8)
                    {
                        middleLineEmpty.Add(i);
                        continue;
                    }
                    bottomLineEmpty.Add(i);
                }
            }

            if (topLineEmpty.Count > 1) result.Add(new byte[] { topLineEmpty[0], topLineEmpty[1] });
            if (middleLineEmpty.Count > 1) result.Add(new byte[] { middleLineEmpty[0], middleLineEmpty[1] });
            if (bottomLineEmpty.Count > 1) result.Add(new byte[] { bottomLineEmpty[0], bottomLineEmpty[1] });

            if (topLineEmpty.Count > 0)
            {
                if (middleLineEmpty.Count > 0)
                {
                    result.Add(new byte[] { topLineEmpty[0], middleLineEmpty[0] });
                    result.Add(new byte[] { middleLineEmpty[0], topLineEmpty[0] });
                }
                if (bottomLineEmpty.Count > 0)
                {
                    result.Add(new byte[] { topLineEmpty[0], bottomLineEmpty[0] });
                    result.Add(new byte[] { bottomLineEmpty[0], topLineEmpty[0] });
                }
            }

            if (middleLineEmpty.Count > 0)
                if (bottomLineEmpty.Count > 0)
                {
                    result.Add(new byte[] { middleLineEmpty[0], bottomLineEmpty[0] });
                    result.Add(new byte[] { bottomLineEmpty[0], middleLineEmpty[0] });
                }

            return result;
        }

        public void rePickTriple(byte[] deck, byte[] triple)
        {
            deck[triple[0]] = 0;
            deck[triple[1]] = 0;
            deck[triple[2]] = 0;
        }

        public byte[] pickRandomThree(byte[] deck)
        {
            byte[] result = new byte[3];

            var first = random.Next(DeckSize);
            while (deck[first] == 1) first = random.Next(DeckSize);
            deck[first] = 1;
            result[0] = (byte)(first + 1);

            var second = random.Next(DeckSize);
            while (deck[second] == 1) second = random.Next(DeckSize);
            deck[second] = 1;
            result[1] = (byte)(second + 1);

            var third = random.Next(DeckSize);
            while (deck[third] == 1) third = random.Next(DeckSize);
            deck[third] = 1;
            result[2] = (byte)(third + 1);

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
