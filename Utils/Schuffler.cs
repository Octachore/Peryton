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

namespace Utils
{
    public static class Schuffler
    {
        public static void FisherYates<T>(IList<T> seq)
        {
            Guard.NotNull(seq, nameof(seq));

            if (seq.Count == 1) return;

            for (int i = 0; i < seq.Count - 1; i++)
            {
                int j = Randomizer.GetInt(i, seq.Count);
                seq.Swap(i, j);
            }
        }
    }
}
