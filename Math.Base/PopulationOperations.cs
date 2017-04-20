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
using System.Linq;
using Utils;
using static System.Math;

namespace Math.Base
{
    public static class PopulationOperations
    {
        public static double Covariance(IList<double> seqX, IList<double> seqY) // "Pearson" covariance
        {
            Guard.NotNull(seqX, nameof(seqX));
            Guard.NotNull(seqY, nameof(seqY));
            Guard.Requires(seqX.Any(), $"The sequence {nameof(seqX)} must not be empty.");
            Guard.Requires(seqY.Any(), $"The sequence {nameof(seqY)} must not be empty.");
            Guard.Requires(seqX.Count == seqY.Count, $"The count of {nameof(seqX)} ({seqX.Count}) must be equal to the count of {nameof(seqY)} ({seqY.Count}).");

            double meanX = ArithmeticMean(seqX);
            double meanY = ArithmeticMean(seqY);

            return seqX.Zip(seqY, (x, y) => (x - meanX) * (y - meanY)).Sum() / seqX.Count;
        }

        public static double ArithmeticMean(IList<double> seq)
        {
            Guard.NotNull(seq, nameof(seq));
            Guard.Requires(seq.Any(), $"Sequence {nameof(seq)} must not be empty.");

            return seq.Sum() / seq.Count;
        }

        public static double StandardDeviation(IList<double> seq)
        {
            Guard.NotNull(seq, nameof(seq));
            Guard.Requires(seq.Any(), $"The sequence {nameof(seq)} must not be empty.");

            return Sqrt(Variance(seq));
        }

        public static double Variance(IList<double> seq)
        {
            Guard.NotNull(seq, nameof(seq));
            Guard.Requires(seq.Any(), $"The sequence {nameof(seq)} must not be empty.");

            double mean = ArithmeticMean(seq);

            return seq.Sum(x => Pow(x - mean, 2)) / seq.Count;
        }
    }
}
