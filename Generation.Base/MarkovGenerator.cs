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
using Probability.Base;
using Probability.Markov.Chain;
using Utils;

namespace Generation.Base
{
    public class MarkovGenerator<T>
    {
        private readonly IDictionary<T, MarkovState<T>> _possibleInitialStates;

        public MarkovGenerator(IDictionary<T, MarkovState<T>> possibleInitialStates)
        {
            Guard.NotNull(possibleInitialStates, nameof(possibleInitialStates));
            Guard.Requires(possibleInitialStates.Any(), $"The sequence {nameof(possibleInitialStates)} must not be empty.");

            _possibleInitialStates = possibleInitialStates;
        }

        public IEnumerable<T> Generate(int max) => Generate(max, Picker.EquiPick(_possibleInitialStates).Key);

        public IEnumerable<T> Generate(int max, T initialValue)
        {
            var visitor = new MarkovChainVisitor<T>(_possibleInitialStates[initialValue]);
            int count = 0;

            while (visitor.Next() && count < max)
            {
                yield return visitor.CurrentState.Value;
                count++;
            }
        }
    }
}
