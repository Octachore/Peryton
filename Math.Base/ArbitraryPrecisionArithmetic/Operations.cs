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
        public static ArbitraryNumber Add(ArbitraryNumber a, ArbitraryNumber b)
        {
            var result = new ArbitraryNumber();

            var enumerator = new ParallelEnumerator<int>(a.Digits.AsEnumerable().Reverse(), b.Digits.AsEnumerable().Reverse());

            while (enumerator.Next())
            {

            }

            throw new System.NotImplementedException();
        }
    }
}
