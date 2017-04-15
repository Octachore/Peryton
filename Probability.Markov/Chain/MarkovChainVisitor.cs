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
using System.Linq;
using Probability.Base;
using Utils;

namespace Probability.Markov.Chain
{
    public class MarkovChainVisitor<T>
    {
        private readonly MarkovState<T> _initialState;

        public MarkovState<T> CurrentState { get; private set; }

        public MarkovChainVisitor(MarkovState<T> initialState)
        {
            Guard.NotNull(initialState, nameof(initialState));

            _initialState = initialState;
        }

        public bool Next()
        {
            if(CurrentState == null)
            {
                CurrentState = _initialState;
                return true;
            }

            if (!CurrentState.Links.Any())
            {
                CurrentState = null;
                return false;
            }

            CurrentState = Picker.Pick(CurrentState.Links);
            return true;
        }
    }
}
