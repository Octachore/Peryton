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

namespace Utils
{
    public static class Guard
    {
        public static void NotNull<T>(T argument, string argumentName)
        {
            if (argument == null) throw new ArgumentNullException(argumentName);
        }

        public static void Requires(bool condition, string message)
        {
            NotNull(condition, nameof(condition));
            if (!condition) throw new InvalidOperationException(message);
        }
    }
}
