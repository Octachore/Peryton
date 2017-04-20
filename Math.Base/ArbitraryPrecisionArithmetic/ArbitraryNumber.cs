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
        private List<int> _digits = new List<int> { 0 };

        private int _decimalsCount = 0;

        public List<int> IntegerPart => new List<int>(_digits.GetRange(0, _digits.Count - _decimalsCount));

        public List<int> FractionalPart => new List<int>(_digits.GetRange(_digits.Count - _decimalsCount, _decimalsCount));

        internal List<int> Digits => new List<int>(_digits);

        public ArbitraryNumber(IList<int> digits, int decimalsCount)
        {
            Guard.Requires(decimalsCount <= digits.Count, $"The value {nameof(decimalsCount)} must be less than the count of {nameof(digits)} ({digits.Count}).");
            Guard.Requires(decimalsCount >= 0, $"The value {nameof(decimalsCount)} must be positive.");

            _digits = new List<int>(digits);
            _decimalsCount = decimalsCount;
        }

        public ArbitraryNumber() : this(new[] { 0 }, 0)
        {
        }

        public ArbitraryNumber Add(ArbitraryNumber other) => Operations.Add(this, other);
    }
}
