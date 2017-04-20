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
using static Math.Base.PopulationOperations;

namespace Statistics.Base
{
    public static class LinearCorrelation
    {
        public static double PearsonCorrelationCoefficient(IList<double> seqX, IList<double> seqY)
            => Covariance(seqX, seqY) / (StandardDeviation(seqX) * StandardDeviation(seqY));
    }
}
