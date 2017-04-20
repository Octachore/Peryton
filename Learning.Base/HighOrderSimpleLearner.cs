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
using Probability.Markov.Chain;
using Utils;
using Utils.EqualityComparers;

namespace Learning.Base
{
    public class HighOrderSimpleLearner<T>
    {
        private readonly int _order;

        private AutoDictionary<T[], MarkovState<T>> _states = new AutoDictionary<T[], MarkovState<T>>(key => new MarkovState<T>(key.Last()), new CollectionEqualityComparer<T>());

        public HighOrderSimpleLearner(int order)
        {
            Guard.Requires(order > 0, $"{nameof(order)} must be > 0 but was {order}");

            _order = order;
        }

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

            var enumerator = new BlockEnumerator<T>(seq, _order + 1);

            while (enumerator.Next())
            {
                T[] first = enumerator.CurrentBlock.Take(_order).ToArray();
                T[] second = enumerator.CurrentBlock.Skip(1).Take(_order).ToArray();

#warning Storing the last item of the key in the MArkov state may not be appropriate for order > 1
                _states[first].Links[_states[second]] += 1;
            }
        }

        public IDictionary<T[], MarkovState<T>> GetStates() => new Dictionary<T[], MarkovState<T>>(_states);
    }
}
