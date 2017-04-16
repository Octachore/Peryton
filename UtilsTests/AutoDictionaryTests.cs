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
using NUnit.Framework;
using Utils;

namespace UtilsTests
{
    public class AutoDictionaryTests
    {
        [Test]
        public void AutoDictionary_NewInstance_WithNullDefaultLambda_ThrowsArgumentNullException()
        {
            Func<int, double> f = null;
            Assert.That(() => new AutoDictionary<int, double>(f), Throws.ArgumentNullException);
        }

        [Test]
        public void AutoDictionary_Get_WithDefaultLambdaAndNoExistingKey_GetsExpectedValue()
        {
            Func<int, int[]> f = i => new[] { i, 42 };
            var dic = new AutoDictionary<int, int[]>(f);

            Assert.That(dic[0], Is.EquivalentTo(f(0)));
            Assert.That(dic[1], Is.EquivalentTo(f(1)));
            Assert.That(dic[2], Is.EquivalentTo(f(2)));
        }

        [Test]
        public void AutoDictionary_Set_WithDefaultLambda_SetsExpectedValue()
        {
            Func<int, int[]> f = i => new[] { i, 42 };
            var dic = new AutoDictionary<int, int[]>(f)
            {
                [0] = new int[0],
                [1] = new[] { 1, 2, 3, 4, 5 },
                [2] = new[] { 100 }
            };

            Assert.That(dic[0], Is.EquivalentTo(new int[0]));
            Assert.That(dic[1], Is.EquivalentTo(new[] { 1, 2, 3, 4, 5 }));
            Assert.That(dic[2], Is.EquivalentTo(new[] { 100 }));
        }

        [Test]
        public void AutoDictionary_Get_WithDefaultValueAndNoExistingKey_GetsExpectedValue()
        {
            int def = 42;
            var dic = new AutoDictionary<int, int>(def);

            Assert.That(dic[0], Is.EqualTo(42));
            Assert.That(dic[1], Is.EqualTo(42));
            Assert.That(dic[2], Is.EqualTo(42));
        }

        [Test]
        public void AutoDictionary_Set_WithDefaultValue_SetsExpectedValue()
        {
            int def = 42;
            var dic = new AutoDictionary<int, int>(def)
            {
                [0] = 100,
                [1] = 17,
                [2] = 12
            };

            Assert.That(dic[0], Is.EqualTo(100));
            Assert.That(dic[1], Is.EqualTo(17));
            Assert.That(dic[2], Is.EqualTo(12));
        }

        [Test]
        public void AutoDictionary_Get_WithNoDefaultValueOrLambdaAndNoExistingKey_GetsExpectedValue()
        {
            var dic = new AutoDictionary<int, int>();

            Assert.That(dic[0], Is.EqualTo(default(int)));
            Assert.That(dic[1], Is.EqualTo(default(int)));
            Assert.That(dic[2], Is.EqualTo(default(int)));
        }

        [Test]
        public void AutoDictionary_Set_WithNoDefaultValueOrLambda_SetsExpectedValue()
        {
            var dic = new AutoDictionary<int, int>()
            {
                [0] = 100,
                [1] = 17,
                [2] = 12
            };

            Assert.That(dic[0], Is.EqualTo(100));
            Assert.That(dic[1], Is.EqualTo(17));
            Assert.That(dic[2], Is.EqualTo(12));
        }
    }
}
