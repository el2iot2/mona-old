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
    public class CharWhile
    {
        [Fact]
        public async Task ParseSingleExpectedLetter()
        {
            var parser = Expect.While<char>(char.IsLetter);
            IParse<char, IEnumerable<char>> parse = await parser.ParseAsync("a");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Equal('a');
        }

        [Fact]
        public async Task ParseNothingAndReturnRemainder()
        {
            var parser = Expect.While<char>(char.IsLetter);
            IParse<char, IEnumerable<char>> parse = await parser.ParseAsync("1");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equals('1');
        }

        [Fact]
        public async Task TerminateOnEmptyInput()
        {
            var parser = Expect.While<char>(char.IsLetter);
            IParse<char, IEnumerable<char>> parse = await parser.ParseAsync("");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().BeEmpty();
        }

        [Fact]
        public async Task ParseEntireInput()
        {
            var parser = Expect.While<char>(char.IsLetter);
            IParse<char, IEnumerable<char>> parse = await parser.ParseAsync("abc");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Equal('a', 'b', 'c');
            var remainder = await parse.Remainder.ToList();
            remainder.Should().BeEmpty();
        }

        [Fact]
        public async Task ParseNothingAndReturnEntireInput()
        {
            var parser = Expect.While<char>(char.IsLetter);
            IParse<char, IEnumerable<char>> parse = await parser.ParseAsync("123");
            parse.Succeeded().Should().BeTrue();
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }
    }
}
