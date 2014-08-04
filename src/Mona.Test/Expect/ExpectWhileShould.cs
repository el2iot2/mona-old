using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mona
{
    public class ExpectWhileShould
    {
        [Fact]
        public void MatchSingleLetter()
        {
            var parser = Expect.While<char>(char.IsLetter);
            var parse = parser.Parse("a");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Equal('a');
        }

        [Fact]
        public void MatchNothingAndMaintainSingleInput()
        {
            var parser = Expect.While<char>(char.IsLetter);
            var parse = parser.Parse("1");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
            var remainder = parse.Remainder.ToList();
            remainder.Should().Equals('1');
        }

        [Fact]
        public void MatchNothingAndAcceptEndOfInput()
        {
            var parser = Expect.While<char>(char.IsLetter);
            var parse = parser.Parse("");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
        }

        [Fact]
        public void MatchEntireInput()
        {
            var parser = Expect.While<char>(char.IsLetter);
            var parse = parser.Parse("abc");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Equal('a', 'b', 'c');
            var remainder = parse.Remainder.ToList();
            remainder.Should().BeEmpty();
        }

        [Fact]
        public void MatchNothingAndMaintainInputs()
        {
            var parser = Expect.While<char>(char.IsLetter);
            var parse = parser.Parse("123");
            parse.Succeeded().Should().BeTrue();
            var remainder = parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }
    }
}
