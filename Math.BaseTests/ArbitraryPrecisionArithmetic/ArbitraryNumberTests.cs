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
        public void ArbitraryNumber_HasIntegerAndFractionalParts()
        {
            var an = new ArbitraryNumber(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4);

            Assert.That(an.IntegerPart, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5 }));
            Assert.That(an.FractionalPart, Is.EquivalentTo(new[] { 6, 7, 8, 9 }));
        }

        [Test]
        public void ArbitraryNumber_NewInstance_WithoutArguments_IsZero()
        {
            var an = new ArbitraryNumber();

            Assert.That(an.Digits, Is.EquivalentTo(new int[0]));
            Assert.That(an.DecimalsCount, Is.Zero);
            Assert.That(an.IsNegative, Is.False);
        }

        [Test]
        public void ArbitraryNumber_NewInstance_WithNullDigitsArray_ThrowsArgumentNullException()
        {
            Assert.That(() => new ArbitraryNumber(null, 1), Throws.ArgumentNullException);
        }

        [Test]
        public void ArbitraryNumber_NewInstance_WithNullArbitraryNumber_IsZero()
        {
            var an = new ArbitraryNumber(null);

            Assert.That(an.Digits, Is.EquivalentTo(new int[0]));
            Assert.That(an.DecimalsCount, Is.Zero);
            Assert.That(an.IsNegative, Is.False);
        }

        [Test]
        public void ArbitraryNumber_NewInstance_WithNotDigitsIntegersArgument_ThrowsInvalidOperationException()
        {
            Assert.That(() => new ArbitraryNumber(new[] { 1, 2, 3, 42, 5, 6 }, 0), Throws.InvalidOperationException);
            Assert.That(() => new ArbitraryNumber(new[] { 1, 2, 3, -1, 5, 6 }, 0), Throws.InvalidOperationException);
        }

        [Test]
        public void ArbitraryNumber_NewInstance_WithNegativeDecimalCount_ThrowsInvalidOperationException()
        {
            Assert.That(() => new ArbitraryNumber(new[] { 1, 2, 3 }, -1), Throws.InvalidOperationException);
        }

        [TestCase(0, new int[0], 0, false)]
        [TestCase(9, new[] { 9 }, 0, false)]
        [TestCase(42, new[] { 4, 2 }, 0, false)]
        [TestCase(1452344929, new[] { 1, 4, 5, 2, 3, 4, 4, 9, 2, 9 }, 0, false)]
        [TestCase(-42, new[] { 4, 2 }, 0, true)]
        public void ArbitraryNumber_ImplicitCast_FromInt_ReturnsRightArbitraryNumber(int origin, int[] digits, int decimalCount, bool isNegative)
        {
            ArbitraryNumber an = origin;

            Assert.That(an.Digits, Is.EquivalentTo(digits));
            Assert.That(an.DecimalsCount, Is.EqualTo(decimalCount));
            Assert.That(an.IsNegative, Is.EqualTo(isNegative));
        }

        [TestCase(new int[0], 0, false, 0)]
        [TestCase(new[] { 9 }, 0, false, 9)]
        [TestCase(new[] { 4, 2 }, 0, false, 42)]
        [TestCase(new[] { 1, 8, 6, 4, 1, 0, 9, 4, 2 }, 3, false, 186410)]
        [TestCase(new[] { 1, 8, 6, 4, 1, 0, 9, 4, 2 }, 3, true, -186410)]
        public void ArbitraryNumber_ExplicitCast_ToInt_ReturnsRightArbitraryNumber(int[] digits, int decimalCount, bool isNegative, int expectedOutput)
        {
            var an = new ArbitraryNumber(digits, decimalCount, isNegative);

            int i = (int)an;

            Assert.That(i, Is.EqualTo(expectedOutput));
        }

        [TestCase(new[] { 1, 8, 6, 4, 1, 0, 9, 4, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 3, false, -2147483648)]
        public void ArbitraryNumber_ExplicitCast_ToInt_HandlesOverflow(int[] digits, int decimalCount, bool isNegative, int expectedOutput)
        {
            var an = new ArbitraryNumber(digits, decimalCount, isNegative);

            int i = (int)an;

            Assert.That(i, Is.EqualTo(expectedOutput));
        }

        [TestCase(0, new int[0], 0, false)]
        [TestCase(9, new[] { 9 }, 0, false)]
        [TestCase(42, new[] { 4, 2 }, 0, false)]
        [TestCase(1452344929, new[] { 1, 4, 5, 2, 3, 4, 4, 9, 2, 9 }, 0, false)]
        [TestCase(-42, new[] { 4, 2 }, 0, true)]
        [TestCase(9.1, new[] { 9, 1 }, 1, false)]
        [TestCase(911672.6423, new[] { 9, 1, 1, 6, 7, 2, 6, 4, 2, 3 }, 4, false)]
        [TestCase(-911672.6423, new[] { 9, 1, 1, 6, 7, 2, 6, 4, 2, 3 }, 4, true)]
        public void ArbitraryNumber_ExplicitCast_FromDouble_ReturnsRightArbitraryNumber(double origin, int[] digits, int decimalCount, bool isNegative)
        {
            var an = (ArbitraryNumber)origin;

            Assert.That(an.Digits, Is.EquivalentTo(digits));
            Assert.That(an.DecimalsCount, Is.EqualTo(decimalCount));
            Assert.That(an.IsNegative, Is.EqualTo(isNegative));
        }
    }
}
