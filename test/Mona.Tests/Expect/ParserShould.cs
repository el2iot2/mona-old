using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mona
{
    public partial class ParserShould
    {
        [Fact]
        public void SelectNodeWhenSucceeded()
        {
            var parser = Expect
                .Char()
                .SelectNode(input => input.ToString());

            var parse = parser.Parse("a");
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be("a");
            parse.Remainder.Should().BeEmpty("the entire input should be consumed");
        }

        [Fact]
        public void SelectNodeWhenFailed()
        {
            var parser = Expect
                .Char(Char.IsDigit)
                .SelectNode(input => input.ToString());

            var parse = parser.Parse("a");
            parse.Failed().Should().BeTrue();
            parse.Node.Should().Be(default(char).ToString());
            parse.Remainder.Should().Equal('a');
        }
    }
}
