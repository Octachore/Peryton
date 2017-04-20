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
using static Math.Base.PopulationOperations;

namespace Math.BaseTests
{
    public class PopulationOperationsTests
    {
        [Test]
        public void PopulationOperations_ArithmeticMean_WithNullSequence_ThrowsArgumentNullException()
        {
            Assert.That(() => ArithmeticMean(null), Throws.ArgumentNullException);
        }

        [Test]
        public void PopulationOperations_ArithmeticMean_WithEmptySequence_ThrowsInvalidOperationException()
        {
            Assert.That(() => ArithmeticMean(new double[0]), Throws.InvalidOperationException);
        }

        [TestCase(new[] { 1.0 }, 1.0)]
        [TestCase(new[] { -1.0 }, -1.0)]
        [TestCase(new[] { 0.0 }, 0.0)]
        [TestCase(new[] { 0.0, 1.23, 453.19739, 42 }, 124.1068475)]
        public void PopulationOperations_ArithmeticMean_WithNotEmptySequence_ReturnsArithmeticMean(double[] seq, double expected)
        {
            Assert.That(ArithmeticMean(seq), Is.EqualTo(expected).Within(0.01));
        }

        [Test]
        public void PopulationOperations_Variance_WithNullSequence_ThrowsArgumentNullException()
        {
            Assert.That(() => Variance(null), Throws.ArgumentNullException);
        }

        [Test]
        public void PopulationOperations_Variance_WithEmptySequence_ThrowsInvalidOperationException()
        {
            Assert.That(() => Variance(new double[0]), Throws.InvalidOperationException);
        }

        [TestCase(new[] { 1.0 }, 0.0)]
        [TestCase(new[] { -1.0 }, 0.0)]
        [TestCase(new[] { 0.0 }, 0.0)]
        [TestCase(new[] { 0.0, 1.23, 453.19739, 42 }, 36385.8372)]
        [TestCase(new[] { -3.25, 17, 0, 287381.17991, -34.6390, 12.23 }, 11470686098)]
        public void PopulationOperations_Variance_WithNotEmptySequence_ReturnsStandardDeviation(double[] seq, double expected)
        {
            Assert.That(Variance(seq), Is.EqualTo(expected).Within(0.01));
        }

        [Test]
        public void PopulationOperations_StandardDeviation_WithNullSequence_ThrowsArgumentNullException()
        {
            Assert.That(() => StandardDeviation(null), Throws.ArgumentNullException);
        }

        [Test]
        public void PopulationOperations_StandardDeviation_WithEmptySequence_ThrowsInvalidOperationException()
        {
            Assert.That(() => StandardDeviation(new double[0]), Throws.InvalidOperationException);
        }

        [TestCase(new[] { 1.0 }, 0.0)]
        [TestCase(new[] { -1.0 }, 0.0)]
        [TestCase(new[] { 0.0 }, 0.0)]
        [TestCase(new[] { 0.0, 1.23, 453.19739, 42 }, 220.2599592)]
        [TestCase(new[] { -3.25, 17, 0, 287381.17991, -34.6390, 12.23 }, 117323.5838)]
        public void PopulationOperations_StandardDeviation_WithNotEmptySequence_ReturnsStandardDeviation(double[] seq, double expected)
        {
            Assert.That(StandardDeviation(seq), Is.EqualTo(expected).Within(0.01));
        }

        [Test]
        public void PopulationOperations_Covariance_WithNullSequence_ThrowsArgumentNullException()
        {
            Assert.That(() => Covariance(new[] { 0d }, null), Throws.ArgumentNullException);
            Assert.That(() => Covariance(null, new[] { 0d }), Throws.ArgumentNullException);
        }

        [Test]
        public void PopulationOperations_Covariance_WithEmptySequence_ThrowsArgumentNullException()
        {
            Assert.That(() => Covariance(new[] { 0d }, new double[0]), Throws.InvalidOperationException);
            Assert.That(() => Covariance(new double[0], new[] { 0d }), Throws.InvalidOperationException);
        }

        [TestCase(new[] { 1.25, 12.6461, -432.12, 47825 }, new[] { 12, 4512, 3.17, -13 }, -13516645.56)]
        [TestCase(new[] { 1.25, 12.6461, -432.12, 47825 }, new[] { 1.25, 12.6461, -432.12, 47825 }, 431391727.3)]
        public void PopulationOperations_Covariance_WithNotEmptySequence_ReturnsCovariance(double[] seqX, double[] seqY, double expected)
        {
            Assert.That(Covariance(seqX, seqY), Is.EqualTo(expected).Within(0.01));
        }

        [TestCase(new[] { 1.25, 12.6461, -432.12, 47825 }, new[] { 12, 4512, 3.17, -13 })]
        [TestCase(new[] { 1.25, 12.6461, -432.12, 47825 }, new[] { 1.25, 12.6461, -432.12, 47825 })]
        public void PopulationOperations_Covariance_WithNotEmptySequence_IsCommutative(double[] seqX, double[] seqY)
        {
            Assert.That(Covariance(seqX, seqY), Is.EqualTo(Covariance(seqY, seqX)));
        }
    }
}
