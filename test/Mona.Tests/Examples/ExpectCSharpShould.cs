using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [InlineData("@class")]
        public void MatchIdentifier(string literal)
        {
            var parse = ExpectCSharp.Identifier().Parse(literal);
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be(literal);
        }

        [Theory]
        [InlineData("9")]
        [InlineData("^")]
        [InlineData("class")]
        public void RejectIdentifier(string literal)
        {
            var parse = ExpectCSharp.Identifier().Parse(literal);
            parse.Succeeded().Should().BeFalse();
        }
    }
}
