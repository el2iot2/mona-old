using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mona.Test.Examples
{
    /// <summary>
    /// Mini language for http://github.com/automatonic/beard
    /// </summary>
    public class BeardTemplateShould
    {
        [Fact]
        void ComposeBeardTemplateParser()
        {
            var FrontMatterDefinitions = Expect.Create<char, char>(null);
            var FrontMatter = Expect.Concat(
                Expect.String("<!--"),
                FrontMatterDefinitions,
                Expect.String("-->"),
                nodeSelector: (prefix, definitions, suffix) => definitions
                );
        }
    }
}
