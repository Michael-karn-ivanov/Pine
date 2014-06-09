using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pineapple
{
	public class InputReader
	{
		public static byte[] ReadInput(string input)
		{
			String[] cards = input.Split(new [] { " " }, StringSplitOptions.RemoveEmptyEntries);
			var result = new byte[cards.Length];
			for (int i = 0; i < cards.Length; i++)
			{
				result[i] = ReadSingle(cards[i]);
			}
			return result;
		}

		public static byte ReadSingle(string single)
		{
			if (single.Length != 2) return 0;
			var low = single.ToLower();
			char color = low[1];
			byte multiplicator = 0;
			if (color == 's')
				multiplicator = 0;
			else if (color == 'c')
				multiplicator = 1;
			else if (color == 'd')
				multiplicator = 2;
			else if (color == 'h')
				multiplicator = 3;
			else return 0;
			char value = low[0];
			if (value == '2')
				return (byte)(1 + multiplicator * 13);
			else if (value == '3')
				return (byte)(2 + multiplicator * 13);
			else if (value == '4')
				return (byte)(3 + multiplicator * 13);
			else if (value == '5')
				return (byte)(4 + multiplicator * 13);
			else if (value == '6')
				return (byte)(5 + multiplicator * 13);
			else if (value == '7')
				return (byte)(6 + multiplicator * 13);
			else if (value == '8')
				return (byte)(7 + multiplicator * 13);
			else if (value == '9')
				return (byte)(8 + multiplicator * 13);
			else if (value == 't')
				return (byte)(9 + multiplicator * 13);
			else if (value == 'j')
				return (byte)(10 + multiplicator * 13);
			else if (value == 'q')
				return (byte)(11 + multiplicator * 13);
			else if (value == 'k')
				return (byte)(12 + multiplicator * 13);
			else if (value == 'a')
				return (byte)(13 + multiplicator * 13);
			else return 0;
		}
	}
}
