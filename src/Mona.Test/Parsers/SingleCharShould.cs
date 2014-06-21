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
    public class SingleCharShould
    {
        [Fact]
        public async Task ParseSingleValidLetter()
        {
            var parser = Parsers.SingleChar(c => char.IsLetter(c));
            IParse<char, char> parse = await parser.Parse("a").SingleOrDefaultAsync();
            parse.Success().Should().BeTrue();
            parse.Node.Should().Be('a');
        }

        [Fact]
        public async Task RejectSingleInvalidLetter()
        {
            var parser = Parsers.SingleChar(c => char.IsLetter(c));
            IParse<char, char> parse = await parser.Parse("1").SingleOrDefaultAsync();
            parse.Failed().Should().BeTrue();
        }

        [Fact]
        public async Task TerminateOnEmptyInput()
        {
            var parser = Parsers.SingleChar(c => char.IsLetter(c));
            IParse<char, char> parse = await parser.Parse("").SingleOrDefaultAsync();
            parse.Should().BeNull();
        }
    }
}
