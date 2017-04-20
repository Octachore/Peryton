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
using NUnit.Framework;
using Utils;

namespace UtilsTests
{
    public class ParallelEnumeratorTests
    {
        [Test]
        public void ParallelEnumerator_NewInstance_WithNullArgument_ThrowsArgumentNullException()
        {
            Assert.That(() => new ParallelEnumerator<object>(null), Throws.ArgumentNullException);
        }

        [Test]
        public void ParallelEnumerator_NewInstance_WithNullEnumerable_ThrowsInvalidOperationException()
        {
            Assert.That(() => new ParallelEnumerator<int>(new[] { 0 }, null, new[] { 0 }), Throws.InvalidOperationException);
        }

        [Test]
        public void ParallelEnumerator_Next_WithValidData_Enumerates()
        {
            int[] data1 = new[] { 1, 2, 3, 4, 5 };
            int[] data2 = new[] { 6, 7, 8 };
            int[] data3 = new[] { 9, 10, 11, 12 };

            var enumerator = new ParallelEnumerator<int>(data1, data2, data3);

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.Current, Is.EquivalentTo(new[] { 1, 6, 9 }));

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.Current, Is.EquivalentTo(new[] { 2, 7, 10 }));

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.Current, Is.EquivalentTo(new[] { 3, 8, 11 }));

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.Current, Is.EquivalentTo(new[] { 4, 0, 12 }));

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.Current, Is.EquivalentTo(new[] { 5, 0, 0 }));

            Assert.That(enumerator.Next(), Is.False);
        }
    }
}
