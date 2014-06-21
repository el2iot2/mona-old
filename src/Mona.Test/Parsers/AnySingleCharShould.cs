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
    public class AnySingleCharShould
    {
        [Fact]
        public async Task ParseAnySingleCharacter()
        {
            var parser = Parsers.SingleChar();
            IParse<char, char> parse = await parser.Parse("a").SingleAsync();
            parse.Success().Should().BeTrue();
            parse.Node.Should().Be('a');
        }

        [Fact]
        public async Task TerminateOnEmptyInput()
        {
            var parser = Parsers.SingleChar();
            IParse<char, char> parse = await parser.Parse("").SingleOrDefaultAsync();
            parse.Should().BeNull();
        }

        [Fact]
        public async Task ParseAnySingleCharAndReturnRemainder()
        {
            var parser = Parsers.SingleChar();
            IParse<char, char> parse = await parser.Parse("abc").SingleAsync();
            parse.Success().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

    }
}
