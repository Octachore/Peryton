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
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Probability.Base
{
    public static class Picker
    {
        public static T Pick<T>(IDictionary<T, double> weightedValues)
        {
            IDictionary<double, T> distributedValues = CumulativeDistribution.GetCumulativeDistribution(weightedValues);

            double max = distributedValues.Keys.Last();
            double rnd = Randomizer.GetDouble(max);

            foreach (KeyValuePair<double, T> item in distributedValues)
            {
                if (item.Key > rnd) return item.Value;
                rnd -= item.Key;
            }

            throw new InvalidOperationException("Invalid distribution");
        }
    }
}
