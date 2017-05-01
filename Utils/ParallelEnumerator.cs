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

namespace Utils
{
    public class ParallelEnumerator<T>
    {
        private readonly IEnumerator<T>[] _enumerators;

        public T[] Current { get; private set; }

        public ParallelEnumerator(params IEnumerable<T>[] enumerables)
        {
            Guard.NotNull(enumerables, nameof(enumerables));
            Guard.RequiresAll(enumerables, (e, i) => e != null, (e, i) => $"The enumerable at index {i} must not be null");

            _enumerators = enumerables.Select(e => e.GetEnumerator()).ToArray();
        }

        public bool Next()
        {
            Current = new T[_enumerators.Length];
            bool allEnded = true;
            for (int i = 0; i < _enumerators.Length; i++)
            {
                if (_enumerators[i].MoveNext())
                {
                    allEnded = false;
                    Current[i] = _enumerators[i].Current;
                }
                else Current[i] = default(T);
            }
            return !allEnded;
        }
    }
}
