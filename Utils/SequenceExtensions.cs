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

using System;
using System.Collections.Generic;
using static System.Math;

namespace Utils
{
    public static class SequenceExtensions
    {
        public static void Swap<T>(this IList<T> seq, int i, int j)
        {
            Guard.Requires(i < seq.Count, $"{nameof(i)} must be < sequence count ({seq.Count}) but was {i}.");
            Guard.Requires(j < seq.Count, $"{nameof(j)} must be < sequence count ({seq.Count}) but was {j}.");
            Guard.Requires(i >= 0, $"{nameof(i)} must be positive but was {i}.");
            Guard.Requires(j >= 0, $"{nameof(j)} must be positive but was {j}.");

            if (i == j) return;

            T oldI = seq[i];
            seq[i] = seq[j];
            seq[j] = oldI;
        }

        public static void Trim<T>(this IList<T> seq, T identifier)
        {
            TrimLeft(seq, identifier);
            TrimRight(seq, identifier);
        }

        public static void TrimRight<T>(this IList<T> seq, T identifier, int? limit = null)
        {
            Guard.NotNull(seq, nameof(seq));

            if (seq.Count == 0) return;
            int max = Max(0, seq.Count - limit ?? seq.Count);
            for (int i = seq.Count - 1; i >= max; i--)
            {
                if (seq[i]?.Equals(identifier) == true) seq.RemoveAt(i);
                else break;
            }
        }

        public static void TrimLeft<T>(this IList<T> seq, T identifier)
        {
            Guard.NotNull(seq, nameof(seq));

            if (seq.Count == 0) return;

            for (int i = 0; i < seq.Count; i++)
            {
                if (seq[i]?.Equals(identifier) == true) seq.RemoveAt(i);
                else break;
            }
        }

        public static IEnumerable<T> After<T>(this IEnumerable<T> seq, T value) => seq.After(t => t?.Equals(value) ?? false);

        public static IEnumerable<T> Before<T>(this IEnumerable<T> seq, T value) => seq.Before(t => t?.Equals(value) ?? false);

        public static IEnumerable<T> After<T>(this IEnumerable<T> seq, Func<T, bool> f)
        {
            bool started = false;
            foreach (T item in seq)
            {
                if (f(item)) started = true;
                if (started) yield return item;
            }
        }

        public static IEnumerable<T> Before<T>(this IEnumerable<T> seq, Func<T, bool> f)
        {
            foreach (T item in seq)
            {
                if (f(item)) yield break;
                yield return item;
            }
        }
    }
}
