using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mona
{
    public class ExpectZeroOrMoreShould
    {
        [Fact]
        public void MatchEndOfInput()
        {
            var parser = Expect
                .Char('a')
                .ZeroOrMore();
            var parse = parser.Parse("");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
        }

        [Fact]
        public void MatchNothingAndRetainInput()
        {
            var parser = Expect
                .Char('a')
                .ZeroOrMore();
            var parse = parser.Parse("123");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
            parse.Remainder.Should().Equal('1', '2', '3');
        }

        [Fact]
        public void MatchPrefixAndRetainInput()
        {
            var parser = Expect
                .Char('a')
                .ZeroOrMore();
            var parse = parser.Parse("aa123");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Equal('a', 'a');
            parse.Remainder.Should().Equal('1', '2', '3');
        }
    }
}
