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
using Learning.Base;
using NUnit.Framework;

namespace Learning.BaseTests
{
    public class HighOrderSimpleLearnerTests
    {
        [Test]
        public void HighOrderSimpleLearner_NewInstance_WithNegativeOrder_ThrowsInvalidOperationException()
        {
            Assert.That(() => new HighOrderSimpleLearner<object>(0), Throws.InvalidOperationException);
            Assert.That(() => new HighOrderSimpleLearner<object>(-1), Throws.InvalidOperationException);
        }

        [Test]
        public void HighOrderSimpleLearner_Learn_WithNullSequence_ThrowsArgumentNullException()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void HighOrderSimpleLearner_Learn_WithEmptySequence_LearnsNothing()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void HighOrderSimpleLearner_Learn_WithSequenceContainingNoMoreItemsThatOrderValue_LearnsNothing()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void HighOrderSimpleLearner_Learn_WithAppropriateSequence_Learns()
        {
            throw new System.NotImplementedException();
        }
    }
}