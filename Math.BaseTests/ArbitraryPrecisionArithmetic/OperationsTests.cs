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

        [TestCase(new[] { 1, 5, 7, 4, 1, 2, 3, 6, 5 }, 4, false, new[] { 9, 5, 6, 3, 1, 2 }, 2, false, new[] { 1, 5, 0, 5, 3, 5, 3, 3, 3, 5, 9, 7, 8, 8 }, 5, false)]
        public void Operations_Mult_WithArbitraryNumbers_ReturnsNewCorrectArbitraryNumber(int[] digits1, int decimalCount1, bool isNegative1, int[] digits2, int decimalCount2, bool isNegative2, int[] expectedDigits, int excpectedDecimalCount, bool expectedIsNegative)
        {
            var an1 = new ArbitraryNumber(digits1, decimalCount1);
            var an2 = new ArbitraryNumber(digits2, decimalCount2);

            ArbitraryNumber result = an1 * an2;

            Assert.That(result.Digits, Is.EquivalentTo(expectedDigits));
            Assert.That(result.DecimalsCount, Is.EqualTo(excpectedDecimalCount));
            Assert.That(result.IsNegative, Is.EqualTo(expectedIsNegative));
        }

        [TestCase(9, new[] { 1, 4, 1, 6, 7, 1, 1, 2, 8, 5 }, 4, false)]
        [TestCase(42, new[] { 6, 6, 1, 1, 3, 1, 9, 3, 3 }, 3, false)]
        [TestCase(0, new int[0], 0, false)]
        [TestCase(-1, new[] { 1, 5, 7, 4, 1, 2, 3, 6, 5 }, 4, true)]
        [TestCase(100, new[] { 1, 5, 7, 4, 1, 2, 3, 6, 5 }, 2, false)]
        [TestCase(-37, new[] { 5, 8, 2, 4, 2, 5, 7, 5, 0, 5 }, 4, true)]
        [TestCase(1598416841, new[] { 2, 5, 1, 6, 1, 0, 5, 7, 5, 1, 9, 7, 6, 3, 8, 9, 6, 5 }, 4, false)]
        public void Operations_Mult_WithInteger_ReturnsNewCorrectArbitraryNumber(int factor, int[] digits, int decimalCount, bool isNegative)
        {
            var an = new ArbitraryNumber(new[] { 1, 5, 7, 4, 1, 2, 3, 6, 5 }, 4);

            ArbitraryNumber result = an * factor;

            Assert.That(result.Digits, Is.EquivalentTo(digits));
            Assert.That(result.DecimalsCount, Is.EqualTo(decimalCount));
            Assert.That(result.IsNegative, Is.EqualTo(isNegative));
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
