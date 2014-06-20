using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mona
{
    public class ParsersShould
    {
        [Fact]
        public void CreateSingleCharParser()
        {
            var parser = Parsers.SingleChar(c => char.IsLetter(c));
        }
    }
}
