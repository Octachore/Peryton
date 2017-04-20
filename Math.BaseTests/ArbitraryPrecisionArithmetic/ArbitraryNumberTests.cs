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
using Math.Base.ArbitraryPrecisionArithmetic;
using NUnit.Framework;

namespace Math.BaseTests.ArbitraryPrecisionArithmetic
{
    public class ArbitraryNumberTests
    {
        [Test]
        public void Dummy()
        {
            var an = new ArbitraryNumber(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4);

            Assert.That(an.IntegerPart, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5 }));
            Assert.That(an.FractionalPart, Is.EquivalentTo(new[] { 6, 7, 8, 9 }));
        }
    }
}
