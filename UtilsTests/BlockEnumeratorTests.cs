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
    public class BlockEnumeratorTests
    {
        [Test]
        public void BlockEnumerator_NewInstance_WithNullInput_ThrowsArgumentNullException()
        {
            Assert.That(() => new BlockEnumerator<object>(null, 1), Throws.ArgumentNullException);
        }

        [Test]
        public void BlockEnumerator_NewInstance_WithNegativeBlockSize_ThrowsInvalidOperationException()
        {
            Assert.That(() => new BlockEnumerator<int>(new[] { 1, 2, 3 }, 0), Throws.InvalidOperationException);
            Assert.That(() => new BlockEnumerator<int>(new[] { 1, 2, 3 }, -1), Throws.InvalidOperationException);
        }

        [Test]
        public void BlockEnumerator_Next_WithEmptyInput_ReturnsFalse()
        {
            var enumerator = new BlockEnumerator<int>(new int[0], 1);

            bool result = enumerator.Next();

            Assert.That(result, Is.False);
        }

        [Test]
        public void BlockEnumerator_Next_WithInputShorterThatBlockSize_ReturnsFalse()
        {
            var enumerator = new BlockEnumerator<int>(new int[] { 1, 2, 3 }, 4);

            bool result = enumerator.Next();

            Assert.That(result, Is.False);
        }

        [Test]
        public void BlockEnumerator_Next_WithInputLongerThatBlockSize_EnumeratesByBlock()
        {
            var enumerator = new BlockEnumerator<int>(new[] { 1, 2, 3, 4, 5 }, 2);


            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.CurrentBlock, Is.EquivalentTo(new[] { 1, 2 }));

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.CurrentBlock, Is.EquivalentTo(new[] { 2, 3 }));

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.CurrentBlock, Is.EquivalentTo(new[] { 3, 4 }));

            Assert.That(enumerator.Next(), Is.True);
            Assert.That(enumerator.CurrentBlock, Is.EquivalentTo(new[] { 4, 5 }));

            Assert.That(enumerator.Next(), Is.False);
            Assert.That(enumerator.CurrentBlock, Is.Null);
        }
    }
}
