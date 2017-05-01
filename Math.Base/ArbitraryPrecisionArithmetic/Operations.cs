#region License
// Copyright (c) 2017 Nicolas Maurice (Octachore)
// Licensed under the terms of the GNU General Public License


// This file is part of Peryton.

// Peryton is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3 of the License, or
// any later version.

// Peryton is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Peryton.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System.Linq;
using Utils;

namespace Math.Base.ArbitraryPrecisionArithmetic
{
    public static class Operations
    {
        public static ArbitraryNumber Max(ArbitraryNumber a, ArbitraryNumber b)
        {
            if (a.IntegerPart.Count > b.IntegerPart.Count) return a;
            if (a.IntegerPart.Count < b.IntegerPart.Count) return b;
            if (a.Digits[0] >= b.Digits[0]) return a;
            else return b;
        }

        public static ArbitraryNumber Min(ArbitraryNumber a, ArbitraryNumber b)
        {
            if (a.IntegerPart.Count < b.IntegerPart.Count) return a;
            if (a.IntegerPart.Count > b.IntegerPart.Count) return b;
            if (a.Digits[0] <= b.Digits[0]) return a;
            else return b;
        }

        public static ArbitraryNumber Add(ArbitraryNumber a, ArbitraryNumber b)
        {
            if (a.IsNegative)
            {
                if (b.IsNegative) return -AddPositives(-a, -b);
                else return SubstractPositives(b, -a);
            }
            else
            {
                if (b.IsNegative) return SubstractPositives(a, -b);
                else return AddPositives(a, b);
            }
        }

        public static ArbitraryNumber Sub(ArbitraryNumber a, ArbitraryNumber b)
        {
            if (a.IsNegative)
            {
                if (b.IsNegative) return -SubstractPositives(-a, -b);
                else return -AddPositives(-a, b);
            }
            else
            {
                if (b.IsNegative) return AddPositives(a, -b);
                else return SubstractPositives(a, b);
            }
        }

        public static ArbitraryNumber Mult(ArbitraryNumber a, ArbitraryNumber b)
        {
            if (a.IsNegative == b.IsNegative) return MultPositives(Abs(a), Abs(b));
            else return -MultPositives(Abs(a), Abs(b));
        }

        public static ArbitraryNumber Pow(ArbitraryNumber a, int pow)
        {
            if (pow == 0) return 1;
            throw new System.NotImplementedException();
        }

        public static ArbitraryNumber Pow(ArbitraryNumber a, ArbitraryNumber pow)
        {
            throw new System.NotImplementedException();
        }

        private static ArbitraryNumber MultPositives(ArbitraryNumber arbitraryNumber1, ArbitraryNumber arbitraryNumber2)
        {
            var result = new ArbitraryNumber();
            var an = new ArbitraryNumber(arbitraryNumber1);

            foreach (int digit in arbitraryNumber2.Digits.AsEnumerable().Reverse())
            {
                result += MultBySingleDigit(an, digit);
                an.Digits.Add(0);
            }


            result.DecimalsCount += arbitraryNumber2.DecimalsCount;

            Trim(result);

            return result;
        }

        private static ArbitraryNumber MultBySingleDigit(ArbitraryNumber an, int digit)
        {
            Guard.Requires(0 <= digit && digit < 10, $"Requires 0 <= {nameof(digit)} < 10, but actual value was {digit}");

            var result = new ArbitraryNumber();

            int carryOver = 0;
            foreach (int d in an.Digits.AsEnumerable().Reverse())
            {
                int mul = d * digit + carryOver;
                int val = mul % 10;
                carryOver = (mul - val) / 10;

                result.Digits.Insert(0, val);
            }

            while (carryOver > 0)
            {
                int val = carryOver % 10;
                result.Digits.Insert(0, val);
                carryOver = (carryOver - val) / 10;
            }

            result.DecimalsCount = an.DecimalsCount;

            Trim(result);

            return result;
        }

        private static void InsertdecimalShift(ArbitraryNumber intermediateResult, int decimalShift)
        {
            for (int i = 0; i < decimalShift; i++)
            {
                intermediateResult.Digits.Insert(0, 0);
            }
        }

        private static ArbitraryNumber Abs(ArbitraryNumber num)
        {
            var abs = new ArbitraryNumber(num);
            abs.IsNegative = false;
            return abs;
        }

        private static ArbitraryNumber SubstractPositives(ArbitraryNumber a, ArbitraryNumber b)
        {
            var num1 = new ArbitraryNumber(a);
            var num2 = new ArbitraryNumber(b);

            AlignFractionalDigits(num1, num2);

            var result = new ArbitraryNumber();

            var enumerator = new ParallelEnumerator<int>(num1.Digits.AsEnumerable().Reverse(), num2.Digits.AsEnumerable().Reverse());

            int carryOver = 0;
            while (enumerator.Next())
            {
                int val1 = enumerator.Current[0];
                int val2 = enumerator.Current[1];
                int sub = val1 - val2 - carryOver;

                int val = 0;
                if (sub < 0)
                {
                    val = 10 + sub;
                    carryOver = 1;
                }
                else
                {
                    val = sub;
                    carryOver = 0;
                }

                result.Digits.Insert(0, val);
            }

            if (carryOver > 0)
            {
                result.Digits.Insert(0, carryOver);
                result.IsNegative = true;
            }

            result.DecimalsCount = num1.DecimalsCount;

            Trim(result);

            return result;
        }

        private static ArbitraryNumber AddPositives(ArbitraryNumber a, ArbitraryNumber b)
        {
            var num1 = new ArbitraryNumber(a);
            var num2 = new ArbitraryNumber(b);

            AlignFractionalDigits(num1, num2);

            var result = new ArbitraryNumber();

            var enumerator = new ParallelEnumerator<int>(num1.Digits.AsEnumerable().Reverse(), num2.Digits.AsEnumerable().Reverse());

            int carryOver = 0;
            while (enumerator.Next())
            {
                int val1 = enumerator.Current[0];
                int val2 = enumerator.Current[1];
                int added = val1 + val2 + carryOver;

                int val = added % 10;
                carryOver = (added - val) / 10;

                result.Digits.Insert(0, val);
            }

            if (carryOver > 0) result.Digits.Insert(0, carryOver);

            result.DecimalsCount = num1.DecimalsCount;

            Trim(result);

            return result;
        }

        private static void Trim(ArbitraryNumber num)
        {

            if (num.DecimalsCount > 0)
            {
                int initialDigitsCount = num.Digits.Count;
                num.Digits.TrimRight(0, num.DecimalsCount);
                num.DecimalsCount -= initialDigitsCount - num.Digits.Count;
            }
            num.Digits.TrimLeft(0);
            if (!num.Digits.Any()) num.DecimalsCount = 0;
        }

        private static void AlignFractionalDigits(ArbitraryNumber num1, ArbitraryNumber num2)
        {
            if (num1.FractionalPart.Count == num2.FractionalPart.Count) return;

            ArbitraryNumber lessFractional = Selector.Min(an => an.FractionalPart.Count, num1, num2);
            ArbitraryNumber mostFractional = Selector.Max(an => an.FractionalPart.Count, num1, num2);

            lessFractional.Digits.AddRange(Enumerable.Repeat(0, mostFractional.FractionalPart.Count - lessFractional.FractionalPart.Count));
            lessFractional.DecimalsCount += mostFractional.FractionalPart.Count - lessFractional.FractionalPart.Count;
        }
    }
}
