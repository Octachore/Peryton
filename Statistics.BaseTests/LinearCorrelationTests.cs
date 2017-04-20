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
using NUnit.Framework;
using static Statistics.Base.LinearCorrelation;

namespace Statistics.BaseTests
{
    public class LinearCorrelationTests
    {
        [Test]
        public void LinearCorrelation_PearsonCorrelationCoefficient_WithNullSequence_ThrowsArgumentNullException()
        {
            Assert.That(() => PearsonCorrelationCoefficient(null, new[] { 0d, 1d }), Throws.ArgumentNullException);
            Assert.That(() => PearsonCorrelationCoefficient(new[] { 0d, 1d }, null), Throws.ArgumentNullException);
        }

        [Test]
        public void LinearCorrelation_PearsonCorrelationCoefficient_WithEmptySequence_ThrowsInvalidOperationException()
        {
            Assert.That(() => PearsonCorrelationCoefficient(new double[0], new[] { 0d, 1d }), Throws.InvalidOperationException);
            Assert.That(() => PearsonCorrelationCoefficient(new[] { 0d, 1d }, new double[0]), Throws.InvalidOperationException);
        }

        [Test]
        public void LinearCorrelation_PearsonCorrelationCoefficient_WithSingleElementSequence_ThrowsInvalidOperationException()
        {
            Assert.That(() => PearsonCorrelationCoefficient(new[] { 0d }, new[] { 0d, 1d }), Throws.InvalidOperationException);
            Assert.That(() => PearsonCorrelationCoefficient(new[] { 0d, 1d }, new[] { 0d }), Throws.InvalidOperationException);
        }

        [Test]
        public void LinearCorrelation_PearsonCorrelationCoefficient_WithMultipleElementSequence_ReturnsPearsonCorrelationCoefficient(double[] seqX, double[] seqY, double expected)
        {
            throw new System.NotImplementedException("Add test cases");

            Assert.That(PearsonCorrelationCoefficient(seqX, seqY), Is.EqualTo(expected).Within(0.01));
        }
    }
}
