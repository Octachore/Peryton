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
using System.Collections.Generic;
using Probability.Markov.Chain;
using Utils;

namespace Learning.Base
{
    public class SimpleLearner<T>
    {
        private AutoDictionary<T, MarkovState<T>> _states = new AutoDictionary<T, MarkovState<T>>(key => new MarkovState<T>(key));

        public void Learn(IEnumerable<IEnumerable<T>> sequences)
        {
            Guard.NotNull(sequences, nameof(sequences));

            foreach (IEnumerable<T> seq in sequences)
            {
                Learn(seq);
            }
        }

        public void Learn(IEnumerable<T> seq)
        {
            Guard.NotNull(seq, nameof(seq));

            var enumerator = new BlockEnumerator<T>(seq, 2);

            while (enumerator.Next())
            {
                T first = enumerator.CurrentBlock[0];
                T second = enumerator.CurrentBlock[1];

                _states[first].Links[_states[second]] += 1;
            }
        }

        public IDictionary<T, MarkovState<T>> GetStates() => new Dictionary<T, MarkovState<T>>(_states);
    }
}
