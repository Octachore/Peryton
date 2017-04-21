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
    public class OperationsTests
    {
        [Test]
        public void Operations_Add_WithPositiveArbitraryNumbers_ReturnsNewCorrectArbitraryNumber()
        {
            var an1 = new ArbitraryNumber(new[] { 1, 5, 7, 4, 1, 2, 3, 6, 5 }, 4);
            var an2 = new ArbitraryNumber(new[] { 9, 5, 6, 3, 1, 2 }, 2);

            ArbitraryNumber result = an1 + an2;

            Assert.That(result.Digits, Is.EquivalentTo(new[] { 2, 5, 3, 0, 4, 3, 5, 6, 5 }));
            Assert.That(result.IntegerPart, Is.EquivalentTo(new[] { 2, 5, 3, 0, 4 }));
            Assert.That(result.FractionalPart, Is.EquivalentTo(new[] { 3, 5, 6, 5 }));
        }

        [Test]
        public void Operations_Sub_WithPositiveArbitraryNumbers_ReturnsNewCorrectArbitraryNumber()
        {
            var an1 = new ArbitraryNumber(new[] { 1, 5, 7, 4, 1, 2, 3, 6, 5 }, 4);
            var an2 = new ArbitraryNumber(new[] { 9, 5, 6, 3, 1, 2 }, 2);

            ArbitraryNumber result = an1 - an2;

            Assert.That(result.IsNegative, Is.False);
            Assert.That(result.Digits, Is.EquivalentTo(new[] { 6, 1, 7, 8, 1, 1, 6, 5 }));
            Assert.That(result.IntegerPart, Is.EquivalentTo(new[] { 6, 1, 7, 8 }));
            Assert.That(result.FractionalPart, Is.EquivalentTo(new[] { 1, 1, 6, 5 }));
        }
    }
}
