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
    public class AutoDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private bool _hasDefaultValue = false;
        private readonly TValue _defaultValue;
        private readonly bool _hasDefaultLambda;
        private readonly Func<TKey, TValue> _defaultLambda;

        public AutoDictionary(Func<TKey, TValue> defaultLambda)
        {
            Guard.NotNull(defaultLambda, nameof(defaultLambda));

            _defaultLambda = defaultLambda;
            _hasDefaultLambda = true;
        }

        public AutoDictionary(TValue defaultValue)
        {
            _defaultValue = defaultValue;
            _hasDefaultValue = true;
        }

        public AutoDictionary()
        {
        }

        public new TValue this[TKey key]
        {
            get
            {
                CreateIfNotExist(key);
                return base[key];
            }
            set
            {
                CreateIfNotExist(key);
                base[key] = value;
            }
        }

        private void CreateIfNotExist(TKey key)
        {
            if (ContainsKey(key)) return;

            TValue value = _hasDefaultValue 
                            ? _defaultValue 
                            : _hasDefaultLambda
                                ? _defaultLambda(key)
                                : default(TValue);
            Add(key, value);
        }
    }
}