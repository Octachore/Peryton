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
using NUnit.Framework;
using Probability.Base;

namespace Probability.BaseTests
{
    public class CumulativeDistributionTests
    {
        [Test]
        public void CumulativeDistribution_GetCumulativeDistribution_WithNullArgument_ThrowsArgumentNullException()
        {
            Assert.That(() => CumulativeDistribution.GetCumulativeDistribution<int>(null), Throws.ArgumentNullException);
        }

        [Test]
        public void CumulativeDistribution_GetCumulativeDistribution_WithEmptyArgument_ReturnsEmptyResult()
        {
            IDictionary<double, int> result = CumulativeDistribution.GetCumulativeDistribution<int>(new Dictionary<int, double>());

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void CumulativeDistribution_GetCumulativeDistribution_WithWeightedValues_ReturnsDistributedValues()
        {
            var weightedValues = new Dictionary<int, double>
            {
                { 1, 2.5 },
                { 45, 0.17 },
                {100, 43.12 }
            };
            IDictionary <double, int> result = CumulativeDistribution.GetCumulativeDistribution<int>(weightedValues);

            Assert.That(result, Is.EquivalentTo(new Dictionary<double, int>
            {
                { 2.5, 1 },
                { 2.67, 45 },
                {45.79, 100 }
            }));
        }
    }
}
