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
using System.Collections.Generic;
using System.Linq;
using Utils;
using static System.Math;

namespace Math.Base.ArbitraryPrecisionArithmetic
{
    public class ArbitraryNumber
    {
        internal List<int> Digits { get; set; } = new List<int>();

        public int DecimalsCount { get; internal set; } = 0;

        public bool IsNegative { get; internal set; } = false;

        public List<int> IntegerPart => new List<int>(Digits.GetRange(0, Digits.Count - DecimalsCount));

        public List<int> FractionalPart => new List<int>(Digits.GetRange(Digits.Count - DecimalsCount, DecimalsCount));

        public ArbitraryNumber(IList<int> digits, int decimalsCount, bool isNegative = false)
        {
            Guard.NotNull(digits, nameof(digits));
            Guard.RequiresAll(digits, (d, i) => d >= 0 && d < 10, (d, i) => $"The digit {d} at index {i} in collection {nameof(digits)} must be >= 0 and < 10");
            Guard.Requires(decimalsCount <= digits.Count, $"The value {nameof(decimalsCount)} ({decimalsCount}) must be less than the count of {nameof(digits)} ({digits.Count}).");
            Guard.Requires(decimalsCount >= 0, $"The value {nameof(decimalsCount)} ({decimalsCount}) must be positive.");

            Digits = new List<int>(digits);
            DecimalsCount = decimalsCount;
            IsNegative = isNegative;
        }

        public ArbitraryNumber() : this(new int[0], 0)
        {
        }

        public ArbitraryNumber(ArbitraryNumber other) : this(new List<int>(other?.Digits ?? new List<int>()), other?.DecimalsCount ?? 0)
        {
        }

        public static ArbitraryNumber operator +(ArbitraryNumber a, ArbitraryNumber b) => Operations.Add(a, b);

        public static ArbitraryNumber operator *(ArbitraryNumber a, ArbitraryNumber b) => Operations.Mult(a, b);

        public static ArbitraryNumber operator -(ArbitraryNumber a) => new ArbitraryNumber(a) { IsNegative = !a.IsNegative };

        public static ArbitraryNumber operator -(ArbitraryNumber a, ArbitraryNumber b) => Operations.Sub(a, b);

        public override string ToString() => Digits.Count == 0 ? "Zero" : $"{(IsNegative ? "-" : "")}{string.Concat(IntegerPart)}.{string.Concat(FractionalPart)}";

        public static implicit operator ArbitraryNumber(int i) => new ArbitraryNumber(i.Digits(), 0, i < 0);

        public static explicit operator int(ArbitraryNumber an)
        {
            if (an == null) return 0;
            int result = 0;
            int i = 0;

            unchecked
            {
                foreach (int digit in an.IntegerPart.AsEnumerable().Reverse())
                {
                    result += digit * ((int)Pow(10, i));
                    i++;
                }
            }

            return an.IsNegative ? -result : result;
        }

        public static explicit operator ArbitraryNumber(double d)
        {
            if (d % 1 == 0) return (int)d;

            return new ArbitraryNumber(d.Digits(), d.DecimalDigits().Length, d < 0);
        }
    }
}
