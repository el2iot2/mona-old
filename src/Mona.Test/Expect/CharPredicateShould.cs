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
    public class CharPredicateShould
    {
        [Fact]
        public async Task ParseSingleValidLetter()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.ParseAsync("a");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
        }

        [Fact]
        public async Task RejectSingleInvalidLetter()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.ParseAsync("1");
            parse.Failed().Should().BeTrue();
        }

        [Fact]
        public async Task TerminateOnEmptyInput()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.ParseAsync("");
            parse.Should().BeNull();
        }

        [Fact]
        public async Task ParseFirstValidLetterAndReturnRemainder()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.ParseAsync("abc");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

        [Fact]
        public async Task RejectFirstInvalidLetterAndReturnEntireInput()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.ParseAsync("123");
            parse.Failed().Should().BeTrue();
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }
    }
}
