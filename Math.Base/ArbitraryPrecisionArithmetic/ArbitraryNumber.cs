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
using Utils;

namespace Math.Base.ArbitraryPrecisionArithmetic
{
    public class ArbitraryNumber
    {
        internal List<int> Digits { get; set; } = new List<int>();

        internal int DecimalsCount = 0;

        public List<int> IntegerPart => new List<int>(Digits.GetRange(0, Digits.Count - DecimalsCount));

        public List<int> FractionalPart => new List<int>(Digits.GetRange(Digits.Count - DecimalsCount, DecimalsCount));

        public ArbitraryNumber(IList<int> digits, int decimalsCount)
        {
            Guard.Requires(decimalsCount <= digits.Count, $"The value {nameof(decimalsCount)} must be less than the count of {nameof(digits)} ({digits.Count}).");
            Guard.Requires(decimalsCount >= 0, $"The value {nameof(decimalsCount)} must be positive.");

            Digits = new List<int>(digits);
            DecimalsCount = decimalsCount;
        }

        public ArbitraryNumber()
        {
        }

        public ArbitraryNumber(ArbitraryNumber other) : this(new List<int>(other.Digits), other.DecimalsCount)
        {
        }

        public ArbitraryNumber Add(ArbitraryNumber other) => Operations.Add(this, other);

        public static ArbitraryNumber operator +(ArbitraryNumber a, ArbitraryNumber b) => a?.Add(b);

        public override string ToString() => $"{string.Concat(IntegerPart)}.{string.Concat(FractionalPart)}";
    }
}
