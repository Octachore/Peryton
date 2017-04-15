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
using Learning.Base;
using NUnit.Framework;
using Probability.Markov.Chain;

namespace Learning.BaseTests
{
    public class SimpleLearnerTests
    {
        [Test]
        public void SimpleLearner_Learn_WithNullInput_ThrowsArgumentNullException()
        {
            var learner = new SimpleLearner<object>();

            Assert.That(() => learner.Learn(null), Throws.ArgumentNullException);
        }

        [Test]
        public void SimpleLearner_Learn_WithEmptyInput_LearnsNothing()
        {
            var learner = new SimpleLearner<object>();

            learner.Learn(new object[0]);

            Assert.That(learner.GetStates(), Is.Empty);
        }

        [Test]
        public void SimpleLearner_Learn_WithNotEmptyInput_Learns()
        {
            var learner = new SimpleLearner<char>();
            string input = "abcadbbbabe";

            learner.Learn(input.ToCharArray());

            IDictionary<char, MarkovState<char>> states = learner.GetStates();

            Assert.That(states, Has.Count.EqualTo(5));

            Assert.That(states['a'].Links, Has.Count.EqualTo(2));
            Assert.That(states['b'].Links, Has.Count.EqualTo(4));
            Assert.That(states['c'].Links, Has.Count.EqualTo(1));
            Assert.That(states['d'].Links, Has.Count.EqualTo(1));
            Assert.That(states['e'].Links, Is.Empty);

            Assert.That(states['a'].Links[states['b']], Is.EqualTo(2));
            Assert.That(states['a'].Links[states['d']], Is.EqualTo(1));

            Assert.That(states['b'].Links[states['c']], Is.EqualTo(1));
            Assert.That(states['b'].Links[states['a']], Is.EqualTo(1));
            Assert.That(states['b'].Links[states['b']], Is.EqualTo(2));
            Assert.That(states['b'].Links[states['e']], Is.EqualTo(1));

            Assert.That(states['c'].Links[states['a']], Is.EqualTo(1));

            Assert.That(states['d'].Links[states['b']], Is.EqualTo(1));
        }
    }
}
