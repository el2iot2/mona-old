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
    public class CharShould
    {
        [Fact]
        public async Task ParseSingleExpectedLetter()
        {
            var parser = Expect.Char('a');
            IParse<char, char> parse = await parser.Parse("a").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
        }

        [Fact]
        public async Task RejectSingleUnexpectedLetter()
        {
            var parser = Expect.Char('a');
            IParse<char, char> parse = await parser.Parse("1").SingleAsync();
            parse.Failed().Should().BeTrue();
        }

        [Fact]
        public async Task TerminateOnEmptyInput()
        {
            var parser = Expect.Char('a');
            IParse<char, char> parse = await parser.Parse("").SingleOrDefaultAsync();
            parse.Should().BeNull();
        }

        [Fact]
        public async Task ParseFirstExpectedLetterAndReturnRemainder()
        {
            var parser = Expect.Char('a');
            IParse<char, char> parse = await parser.Parse("abc").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

        [Fact]
        public async Task RejectFirstUnexpectedLetterAndReturnEntireInput()
        {
            var parser = Expect.Char('a');
            IParse<char, char> parse = await parser.Parse("123").SingleAsync();
            parse.Failed().Should().BeTrue();
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }

    }
}
