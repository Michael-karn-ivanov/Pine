using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple
{
	public class LastTurnInPosition
	{
		public const decimal FantazyEV = 2.5M;

		public LastTurnInPosition() { }

		public decimal Work(byte[] villainHand, byte[] heroHand, byte[] triple, out int firstIndexOfTriple, out int secondIndexOfTriple)
		{
			var villainShortLine = new byte[3];
			var villainMiddleLine = new byte[5];
			var villainTopLine = new byte[5];

			var shortVillainValue = SortLine(villainHand, villainShortLine, 0, 3);
			var middleVillainValue = SortLine(villainHand, villainMiddleLine, 3, 5);
			var topVillainValue = SortLine(villainHand, villainTopLine, 8, 5);

			bool isVillainHandDead = IsHandDead(villainShortLine, villainMiddleLine, villainTopLine,
				shortVillainValue, middleVillainValue, topVillainValue);

			decimal villainTotalBonus = 0;
			decimal heroTotalBonus = 0;
			decimal villainLineScore = 0;
			decimal heroLineScore = 0;

			if (!isVillainHandDead)
			{
				villainTotalBonus += CalculateBonusShort(villainShortLine, shortVillainValue);
				villainTotalBonus += CalculateBonusMiddle(villainMiddleLine, middleVillainValue);
				villainTotalBonus += CalculateBonusTop(villainTopLine, topVillainValue);
			}

			var heroShortLine = new byte[3];
			var heroMiddleLine = new byte[5];
			var heroTopLine = new byte[5];

			var shortHeroValue = SortLine(heroHand, heroShortLine, 0, 3);
			var middleHeroValue = SortLine(heroHand, heroMiddleLine, 3, 5);
			var topHeroValue = SortLine(heroHand, heroTopLine, 8, 5);

			if (shortHeroValue != LineValue.Undefined)
			{
				heroTotalBonus += CalculateBonusShort(heroShortLine, shortHeroValue);
			}
			if (middleHeroValue != LineValue.Undefined)
			{
				heroTotalBonus += CalculateBonusMiddle(heroMiddleLine, middleHeroValue);
			}
			if (topHeroValue != LineValue.Undefined)
			{
				heroTotalBonus += CalculateBonusTop(heroTopLine, topHeroValue);
			}

            int firstIndex = -1;
            int secondIndex = -1;
            #region располагаем пару по индексам

            for (int i = 0; i < heroHand.Length; i++)
            {
                if (heroHand[i] == 0)
                {
                    if (firstIndex < 0)
                        firstIndex = i;
                    else if (secondIndex < 0)
                        secondIndex = i;
                }
            }
            #endregion

			decimal maxScore = -1000;
			firstIndexOfTriple = -1;
			secondIndexOfTriple = -1;

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (i == j) continue;
					var placeScore = TryPlace(firstIndex, secondIndex, triple[i], triple[j],
					   heroHand, heroShortLine, heroMiddleLine, heroTopLine, shortHeroValue, middleHeroValue, topHeroValue,
					   villainShortLine, villainMiddleLine, villainTopLine, shortVillainValue, middleVillainValue, topVillainValue, isVillainHandDead);
					if (placeScore > maxScore)
					{
						maxScore = placeScore;
						firstIndexOfTriple = i;
						secondIndexOfTriple = j;
					}
				}
			}

			return maxScore + heroTotalBonus - villainTotalBonus;
		}

		public decimal ScoreCompare(bool isHeroHandDead, bool isVillainHandDead, byte[] heroShortLine, byte[] heroMiddleLine, byte[] heroTopLine,
			LineValue shortHeroValue, LineValue middleHeroValue, LineValue topHeroValue,
			byte[] villainShortLine, byte[] villainMiddleLine, byte[] villainTopLine,
			LineValue shortVillainValue, LineValue middleVillainValue, LineValue topVillainValue)
		{
			if (isHeroHandDead && isVillainHandDead) return 0;
			else if (isHeroHandDead && !isVillainHandDead) return -6;
			else if (!isHeroHandDead && isVillainHandDead) return 6;

			var compareShort = CompareOneOther(heroShortLine, villainShortLine, shortHeroValue, shortVillainValue);
			var compareMiddle = CompareOneOther(heroMiddleLine, villainMiddleLine, middleHeroValue, middleVillainValue);
			var compareTop = CompareOneOther(heroTopLine, villainTopLine, topHeroValue, topVillainValue);
			if (compareShort > 0 && compareMiddle > 0 && compareTop > 0) return 6;
			if (compareShort < 0 && compareMiddle < 0 && compareTop < 0) return -6;
			return (compareShort > 0 ? 1 : -1) + (compareMiddle > 0 ? 1 : -1) + (compareTop > 0 ? 1 : -1);
		}

		public decimal TryPlace(int firstIndex, int secondIndex, byte card1, byte card2, byte[] heroHand,
			byte[] heroShortLine, byte[] heroMiddleLine, byte[] heroTopLine,
			LineValue shortHeroValue, LineValue middleHeroValue, LineValue topHeroValue,
			byte[] villainShortLine, byte[] villainMiddleLine, byte[] villainTopLine,
			LineValue shortVillainValue, LineValue middleVillainValue, LineValue topVillainValue, bool isVillainHandDead)
		{
            heroHand[firstIndex] = card1;
            heroHand[secondIndex] = card2;

			if (firstIndex < 3)
			{
				byte[] firstLine = new byte[3];
				var firstValue = SortLine(heroHand, firstLine, 0, 3);
				var bonusShort = CalculateBonusShort(firstLine, firstValue);

				if (secondIndex > 2 && secondIndex < 8)
				{
					byte[] secondLine = new byte[5];
					var secondValue = SortLine(heroHand, secondLine, 3, 5);
					var isHeroHandDead = IsHandDead(firstLine, secondLine, heroTopLine, firstValue, secondValue, topHeroValue);
					var score = ScoreCompare(isHeroHandDead, isVillainHandDead,
						firstLine, secondLine, heroTopLine, firstValue, secondValue, topHeroValue,
						villainShortLine, villainMiddleLine, villainTopLine, shortVillainValue, middleVillainValue, topVillainValue);
					if (!isHeroHandDead)
						return bonusShort + CalculateBonusMiddle(secondLine, secondValue) + score;
					else return score;
				}
				else if (secondIndex >= 8)
				{
					byte[] secondLine = new byte[5];
					var secondValue = SortLine(heroHand, secondLine, 3, 5);
					var isHeroHandDead = IsHandDead(firstLine, heroMiddleLine, secondLine, firstValue, middleHeroValue, secondValue);
					var score = ScoreCompare(isHeroHandDead, isVillainHandDead,
						firstLine, heroMiddleLine, secondLine, firstValue, middleHeroValue, secondValue,
						villainShortLine, villainMiddleLine, villainTopLine, shortVillainValue, middleVillainValue, topVillainValue);
					if (!isHeroHandDead)
						return bonusShort + CalculateBonusTop(secondLine, secondValue) + score;
					else return score;
				}
				else // both slots in first line
				{
					var isHeroHandDead = IsHandDead(firstLine, heroMiddleLine, heroTopLine, firstValue, middleHeroValue, topHeroValue);
					var score = ScoreCompare(isHeroHandDead, isVillainHandDead,
						firstLine, heroMiddleLine, heroTopLine, firstValue, middleHeroValue, topHeroValue,
						villainShortLine, villainMiddleLine, villainTopLine, shortVillainValue, middleVillainValue, topVillainValue);
					if (!isHeroHandDead)
						return bonusShort + score;
					else return score;
				}
			}
			else if (firstIndex < 8)
			{
				byte[] firstLine = new byte[5];
				var firstValue = SortLine(heroHand, firstLine, 3, 5);
				var bonusMiddle = CalculateBonusMiddle(firstLine, firstValue);

				if (secondIndex >= 8)
				{
					byte[] secondLine = new byte[5];
					var secondValue = SortLine(heroHand, secondLine, 8, 5);
					var isHeroHandDead = IsHandDead(heroShortLine, firstLine, secondLine, shortHeroValue, firstValue, secondValue);
					var score = ScoreCompare(isHeroHandDead, isVillainHandDead,
						heroShortLine, firstLine, secondLine, shortHeroValue, firstValue, secondValue,
						villainShortLine, villainMiddleLine, villainTopLine, shortVillainValue, middleVillainValue, topVillainValue);
					if (!isHeroHandDead)
						return bonusMiddle + CalculateBonusTop(secondLine, secondValue) + score;
					else return score;
				}
				else // both slots in second line
				{
					var isHeroHandDead = IsHandDead(heroShortLine, firstLine, heroTopLine, shortHeroValue, firstValue, topHeroValue);
					var score = ScoreCompare(isHeroHandDead, isVillainHandDead,
						heroShortLine, firstLine, heroTopLine, shortHeroValue, firstValue, topHeroValue,
						villainShortLine, villainMiddleLine, villainTopLine, shortVillainValue, middleVillainValue, topVillainValue);
					if (!isHeroHandDead)
						return bonusMiddle + score;
					else return score;
				}
			}
			else // both slots in third line
			{
				byte[] firstLine = new byte[5];
				var firstValue = SortLine(heroHand, firstLine, 8, 5);
				var isHeroHandDead = IsHandDead(heroShortLine, heroMiddleLine, firstLine, shortHeroValue, middleHeroValue, firstValue);
				var score = ScoreCompare(isHeroHandDead, isVillainHandDead,
						heroShortLine, heroMiddleLine, firstLine, shortHeroValue, middleHeroValue, firstValue,
						villainShortLine, villainMiddleLine, villainTopLine, shortVillainValue, middleVillainValue, topVillainValue);
				if (!isHeroHandDead)
					return CalculateBonusTop(firstLine, firstValue) + score;
				else return score;
			}
		}

		public decimal CalculateBonusShort(byte[] sortedLine, LineValue value)
		{
			if (value == LineValue.Set)
			{
				// за 222 - 10 оч, 333 - 111 оч итп ААА - 22 оч
				return 10 + (sortedLine[0] - 1) % 13 + FantazyEV;
			}
			if (value == LineValue.Pair)
			{
				var bas = sortedLine[0] % 13 - 4;
				decimal result = 0;
				if (bas >= 7) result += FantazyEV;
				return (result + (bas > 0 ? bas : 0));
			}
			return 0;
		}

		public decimal CalculateBonusMiddle(byte[] sortedLine, LineValue value)
		{
			if (value == LineValue.Set) return 2;
			if (value == LineValue.Straight) return 4;
			if (value == LineValue.Flash) return 8;
			if (value == LineValue.FullHouse) return 12;
			if (value == LineValue.Care) return 20;
			if (value == LineValue.StraightFlash) return 30;
			if (value == LineValue.Royal) return 50;
			return 0;
		}

		public decimal CalculateBonusTop(byte[] sortedLine, LineValue value)
		{
			if (value == LineValue.Straight) return 2;
			if (value == LineValue.Flash) return 4;
			if (value == LineValue.FullHouse) return 6;
			if (value == LineValue.Care) return 10;
			if (value == LineValue.StraightFlash) return 15;
			if (value == LineValue.Royal) return 25;
			return 0;
		}

		public LineValue SortLine(byte[] hand, byte[] line, int startIndexInc, int length)
		{
			LineValue value = LineValue.Undefined;
			byte[] valueCountArray = new byte[13];
			var color = (int)((hand[startIndexInc] - 1) / 13);
			var isFlash = true;
			for (int i = startIndexInc; i < startIndexInc + length; i++)
			{
				if (hand[i] == 0) return LineValue.Undefined;
				var nextColor = ((int)((hand[i] - 1) / 13));
				if (nextColor != color) isFlash = false;
				valueCountArray[(hand[i] - 1) % 13]++;
			}
			int minIndex = 100;
			int maxIndex = -1;
			
			for (byte i = 0; i < valueCountArray.Length; i++)
			{
				if (valueCountArray[i] > 0)
				{
					if (i < minIndex) minIndex = i;
					if (i > maxIndex) maxIndex = i;
				}
				if (!isFlash)
				{
					#region каре
					if (valueCountArray[i] == 4)
					{
						line[0] = (byte)(i + 1);
						line[1] = (byte)(i + 1);
						line[2] = (byte)(i + 1);
						line[3] = (byte)(i + 1);
						for (int k = 0; k < valueCountArray.Length; k++)
						{
							if (valueCountArray[k] == 1)
								line[4] = (byte)(k + 1);
						}
						return LineValue.Care;
					}
					#endregion
					#region сет и фулхауз
					else if (valueCountArray[i] == 3)
					{
						int lineCounter = 0;
						line[0] = (byte)(i + 1);
						line[1] = (byte)(i + 1);
						line[2] = (byte)(i + 1);
						for (int k = 0; k < valueCountArray.Length; k++)
						{
							if (valueCountArray[k] == 2)
							{
								line[3] = (byte)(k + 1);
								line[4] = (byte)(k + 1);
								return LineValue.FullHouse;
							}
							else if (valueCountArray[k] == 1)
							{
								if (line[4] == 0)
									line[4] = (byte)(k + 1);
								else
									line[3] = (byte)(k + 1);
							}
						}
						return LineValue.Set;
					}
					#endregion
					#region пара фуллхауз или две пары
					else if (valueCountArray[i] == 2)
					{
						for (int k = i + 1; k < valueCountArray.Length; k++)
						{
							if (valueCountArray[k] == 3)
							{
								line[0] = (byte)(k + 1);
								line[1] = (byte)(k + 1);
								line[2] = (byte)(k + 1);
								line[3] = (byte)(i + 1);
								line[4] = (byte)(i + 1);
								return LineValue.FullHouse;
							}
							else if (valueCountArray[k] == 2)
							{
								line[0] = (byte)(k + 1);
								line[1] = (byte)(k + 1);
								line[2] = (byte)(i + 1);
								line[3] = (byte)(i + 1);
								for (int j = 0; j < valueCountArray.Length; j++)
								{
									if (valueCountArray[j] == 1)
										line[4] = (byte)(j + 1);
								}
								return LineValue.TwoPair;
							}
						}
						line[0] = (byte)(i + 1);
						line[1] = (byte)(i + 1);
						for (int k = 0; k < valueCountArray.Length; k++)
						{
							if (valueCountArray[k] == 1)
							{
								if (line.Length == 5)
								{
									if (line[4] == 0) line[4] = (byte)(k + 1);
									else if (line[3] == 0) line[3] = (byte)(k + 1);
									else line[2] = (byte)(k + 1);
								}
								else
									line[2] = (byte)(k + 1);
							}
						}
						return LineValue.Pair;
					}
					#endregion
				}
			}

			#region флеш стритфлеш
			if (line.Length == 5 && isFlash)
			{
				if (maxIndex - minIndex == 4)
				{
					line[0] = (byte)(minIndex + 5);
					line[1] = (byte)(minIndex + 4);
					line[2] = (byte)(minIndex + 3);
					line[3] = (byte)(minIndex + 2);
					line[4] = (byte)(minIndex + 1);
					// if Ace - royal
					if ((byte)(minIndex + 5) == 13)
						return LineValue.Royal;
					return LineValue.StraightFlash;
				}
				if (valueCountArray[0] == 1 && valueCountArray[1] == 1 &&
					valueCountArray[2] == 1 && valueCountArray[3] == 1 && valueCountArray[12] == 1)
				{
					line[0] = 4;
					line[1] = 3;
					line[2] = 2;
					line[3] = 1;
					line[4] = 13;
					return LineValue.StraightFlash;
				}

				int LineCounter = 4;
				for (int i = 0; i < valueCountArray.Length; i++)
				{
					if (valueCountArray[i] == 1)
					{
						line[LineCounter] = (byte)(i + 1);
						LineCounter--;
					}
				}
				return LineValue.Flash;
			}
			#endregion

			# region нет пар сетов фуллов и каре и расстояние = 5 === стрит
			if (line.Length == 5 && maxIndex - minIndex == 4)
			{
				line[0] = (byte)(minIndex + 5);
				line[1] = (byte)(minIndex + 4);
				line[2] = (byte)(minIndex + 3);
				line[3] = (byte)(minIndex + 2);
				line[4] = (byte)(minIndex + 1);
				return LineValue.Straight;
			}
			if (valueCountArray[0] == 1 && valueCountArray[1] == 1 &&
				valueCountArray[2] == 1 && valueCountArray[3] == 1 && valueCountArray[12] == 1)
			{
				line[0] = 4;
				line[1] = 3;
				line[2] = 2;
				line[3] = 1;
				line[4] = 13;
				return LineValue.Straight;
			}
			#endregion


			int lcounter = line.Length - 1;
			for (int i = 0; i < valueCountArray.Length; i++)
			{
				if (valueCountArray[i] == 1)
				{
					line[lcounter] = (byte)(i + 1);
					lcounter--;
				}
			}
			return LineValue.HighCard;
		}

		public bool IsHandDead(byte[] shortLineSorted, byte[] middleLineSorted, byte[] topLineSorted,
			LineValue shortValue, LineValue middleValue, LineValue topValue)
		{
			return !(CompareOneOther(shortLineSorted, middleLineSorted, shortValue, middleValue) <= 0 &&
				CompareOneOther(middleLineSorted, topLineSorted, middleValue, topValue) <= 0);
		}

		public int CompareOneOther(byte[] oneSorted, byte[] otherSorted, LineValue oneValue, LineValue otherValue)
		{
			int compare = oneValue.CompareTo(otherValue);
			if (compare < 0) return -1;
			if (compare > 0) return 1;

			for (int i = 0; i < 5; i++)
			{
				if (oneSorted.Length == i && otherSorted.Length > i) return -1;
				if (oneSorted.Length > i && otherSorted.Length == i) return 1;
				if (oneSorted.Length == i && otherSorted.Length == i) return 0;
				if (oneSorted[i] < otherSorted[i]) return -1;
				if (oneSorted[i] > otherSorted[i]) return 1;
			}
			return 0;
		}
	}

	public enum LineValue
	{
		Undefined,
		HighCard,
		Pair,
		TwoPair,
		Set,
		Straight,
		Flash,
		FullHouse,
		Care,
		StraightFlash,
		Royal
	}
}
