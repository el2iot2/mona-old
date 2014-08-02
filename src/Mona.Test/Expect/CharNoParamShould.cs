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
    public class CharNoParamShould
    {
        [Fact]
        public async Task ParseAnyCharacter()
        {
            var parser = Expect.Char();
            var parse = await parser.ParseAsync("a");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
        }

        [Fact]
        public async Task TerminateOnEmptyInput()
        {
            var parser = Expect.Char();
            var parse = await parser.ParseAsync("");
            parse.Should().BeNull();
        }

        [Fact]
        public async Task ParseAnyCharAndReturnRemainder()
        {
            var parser = Expect.Char();
            var parse = await parser.ParseAsync("abc");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

    }
}
