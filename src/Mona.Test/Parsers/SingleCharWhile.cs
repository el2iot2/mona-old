using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mona
{
    public class SingleCharWhile
    {
        [Fact]
        public async Task ParseSingleExpectedLetter()
        {
            var parser = Parsers.WhileChar(c => char.IsLetter(c));
            IParse<char, char[]> parse = await parser.Parse("a").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Equal('a');
        }

        [Fact]
        public async Task ParseNothingAndReturnRemainder()
        {
            var parser = Parsers.WhileChar(c => char.IsLetter(c));
            IParse<char, char[]> parse = await parser.Parse("1").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equals('1');
        }

        [Fact]
        public async Task TerminateOnEmptyInput()
        {
            var parser = Parsers.WhileChar(c => char.IsLetter(c));
            IParse<char, char[]> parse = await parser.Parse("").SingleOrDefaultAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
        }

        [Fact]
        public async Task ParseEntireInput()
        {
            var parser = Parsers.WhileChar(c => char.IsLetter(c));
            IParse<char, char[]> parse = await parser.Parse("abc").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Equal('a', 'b', 'c');
            var remainder = await parse.Remainder.ToList();
            remainder.Should().BeEmpty();
        }

        [Fact]
        public async Task ParseNothingAndReturnEntireInput()
        {
            var parser = Parsers.WhileChar(c => char.IsLetter(c));
            IParse<char, char[]> parse = await parser.Parse("123").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }
    }
}
