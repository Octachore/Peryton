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
using Probability.Markov.Chain;

namespace Probability.MarkovTests.Chain
{
    public class MarkovChainVisitorTests
    {
        [Test]
        public void Dummy()
        {
            var state1 = new MarkovState<string>("State 1");
            var state2 = new MarkovState<string>("State 2");
            var state3 = new MarkovState<string>("State 3");

            state1.Links.Add(state2, 43);
            state1.Links.Add(state3, 100);

            var visitor = new MarkovChainVisitor<string>(state1);

            while (visitor.Next())
            {
                Console.WriteLine(visitor.CurrentState.Value);
            }
        }
    }
}
