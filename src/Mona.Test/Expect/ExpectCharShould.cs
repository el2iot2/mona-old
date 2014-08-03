using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mona
{
    public partial class ExpectCharShould
    {
        [Theory]
        [InlineData("a")]
        [InlineData("z")]
        [InlineData("_")]
        public void AnonymouslyMatch(string character)
        {
            var parser = Expect.Char();
            var parse = parser.Parse(character);
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be(character[0]);
            parse.Remainder.Should().BeEmpty("the entire input should be consumed");
        }

        [Fact]
        public void AnonymouslyMatchAndAdvanceInput()
        {
            var parser = Expect.Char();
            var parse = parser.Parse("abc");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

        [Fact]
        public void AnonymouslyRejectEndOfInput()
        {
            var parser = Expect.Char();
            var parse = parser.Parse("");
            parse.Failed().Should().BeTrue();
            parse.Remainder.Should().BeEmpty("the input is empty");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("z")]
        [InlineData("_")]
        public void LiterallyMatch(string literal)
        {
            var parser = Expect.Char(literal[0]);
            var parse = parser.Parse(literal);
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be(literal[0]);
            parse.Remainder.Should().BeEmpty();
        }

        [Fact]
        public void LiterallyMatchAndAdvanceInput()
        {
            var parser = Expect.Char('a');
            var parse = parser.Parse("abc");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

        [Fact]
        public void LiterallyRejectAndUnwindInput()
        {
            var parser = Expect.Char('a');
            var parse = parser.Parse("123");
            parse.Failed().Should().BeTrue();
            var remainder = parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }

        [Theory]
        [InlineData("a")]
        [InlineData("z")]
        [InlineData("_")]
        public void LiterallyReject(string literal)
        {
            var parser = Expect.Char(literal[0]);
            var parse = parser.Parse("1");
            parse.Failed().Should().BeTrue();
            parse.Remainder.Should().Equal("1");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("z")]
        [InlineData("_")]
        public void LiterallyRejectEndOfInput(string literal)
        {
            var parser = Expect.Char(literal[0]);
            var parse = parser.Parse("");
            parse.Failed().Should().BeTrue();
            parse.Remainder.Should().BeEmpty("the input is empty");
        }

        [Fact]
        public void FunctionallyMatch()
        {
            var parser = Expect.Char(char.IsLetter);
            var parse = parser.Parse("a");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
        }

        [Fact]
        public void FunctionallyReject()
        {
            var parser = Expect.Char(char.IsLetter);
            var parse = parser.Parse("1");
            parse.Failed().Should().BeTrue();
        }

        [Fact]
        public void FunctionallyRejectEndOfInput()
        {
            var parser = Expect.Char(char.IsLetter);
            var parse = parser.Parse("");
            parse.Failed().Should().BeTrue();
            parse.Remainder.Should().BeEmpty("the input is empty");
        }

        [Fact]
        public void FunctionallyRejectAndAdvanceInput()
        {
            var parser = Expect.Char(char.IsLetter);
            var parse = parser.Parse("abc");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

        [Fact]
        public void FunctionallyRejectAndRewindInput()
        {
            var parser = Expect.Char(char.IsLetter);
            var parse = parser.Parse("123");
            parse.Failed().Should().BeTrue();
            var remainder = parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }
    }
}
