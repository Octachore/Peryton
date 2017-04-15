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

namespace Utils
{
    public class BlockEnumerator<T>
    {
        private readonly int _blockSize;
        private readonly IEnumerable<T> _input;
        private readonly IEnumerator<T> _enumerator;

        public T[] CurrentBlock { get; private set; }

        private int _headPosition;

        public BlockEnumerator(IEnumerable<T> input, int blockSize)
        {
            Guard.NotNull(input, nameof(input));
            Guard.Requires(blockSize > 0, $"{blockSize} must be > 0");

            _input = input;
            _enumerator = input.GetEnumerator();
            _blockSize = blockSize;
            _headPosition = blockSize - 1;
        }

        public bool Next()
        {
            if (CurrentBlock == null)
            {
                return FirstMove();
            }

            if (!_enumerator.MoveNext())
            {
                CurrentBlock = null;
                return false;
            }

            Array.Copy(CurrentBlock, 1, CurrentBlock, 0, CurrentBlock.Length - 1); // "shift" left [a, b, c, d] -> [b, c, d, d]

            CurrentBlock[CurrentBlock.Length - 1] = _enumerator.Current;

            return true;
        }

        private bool FirstMove()
        {
            T[] firstBlock = new T[_blockSize];
            for (int i = 0; i < _blockSize; i++)
            {
                if (!_enumerator.MoveNext()) return false;
                firstBlock[i] = _enumerator.Current;
            }
            CurrentBlock = firstBlock;
            return true;
        }
    }
}
