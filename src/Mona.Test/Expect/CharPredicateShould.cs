﻿using FluentAssertions;
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
            IParse<char, char> parse = await parser.Parse("a").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
        }

        [Fact]
        public async Task RejectSingleInvalidLetter()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.Parse("1").SingleAsync();
            parse.Failed().Should().BeTrue();
        }

        [Fact]
        public async Task RejectEndOfInput()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.Parse("").SingleOrDefaultAsync();
            parse.Failed().Should().BeTrue();
        }

        [Fact]
        public async Task ParseFirstValidLetterAndReturnRemainder()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.Parse("abc").SingleAsync();
            parse.Succeeded().Should().BeTrue();
            parse.Node.Should().Be('a');
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('b', 'c');
        }

        [Fact]
        public async Task RejectFirstInvalidLetterAndReturnEntireInput()
        {
            var parser = Expect.Char(char.IsLetter);
            IParse<char, char> parse = await parser.Parse("123").SingleAsync();
            parse.Failed().Should().BeTrue();
            var remainder = await parse.Remainder.ToList();
            remainder.Should().Equal('1', '2', '3');
        }
    }
}
