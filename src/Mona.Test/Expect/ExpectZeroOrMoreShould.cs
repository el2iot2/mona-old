using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        
    }
}
