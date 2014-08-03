using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Mona.Test.Examples.Beard
{
    public class ExpectCSharpShould
    {
        [Theory]
        [InlineData("a")]
        [InlineData("_")]
        [InlineData("_97")]
        [InlineData("MonaLisa")]
        public void MatchIdentifiers(string literal)
        {
            var parse = ExpectCSharp.Identifier().Parse(literal);
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be(literal);
        }

        [Theory]
        [InlineData("9")]
        [InlineData("^")]
        [InlineData("class")]
        public void RejectIdentifiers(string literal)
        {
            var parse = ExpectCSharp.Identifier().Parse(literal);
            parse.Succeeded().Should().BeFalse();
        }
    }
}
