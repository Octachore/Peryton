﻿#region License
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
        public static ArbitraryNumber Add(ArbitraryNumber a, ArbitraryNumber b)
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

            if(carryOver > 0) result.Digits.Insert(0, carryOver);

            result.DecimalsCount = num1.DecimalsCount;

            return result;
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
