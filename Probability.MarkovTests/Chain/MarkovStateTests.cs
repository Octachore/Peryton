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
using NUnit.Framework;
using Probability.Markov.Chain;
using Utils;

namespace Probability.MarkovTests.Chain
{
    public class MarkovStateTests
    {
        [Test]
        public void MarkovState_NewInstance_HasCorrectValue()
        {
            int value = Randomizer.GetInt();
            var state = new MarkovState<int>(value);

            Assert.That(state.Value, Is.EqualTo(value));
        }

        [Test]
        public void MarkovState_NewInstance_HasNoLink()
        {
            var state = new MarkovState<int>(42);

            Assert.That(state.Links, Is.Empty);
        }
    }
}
