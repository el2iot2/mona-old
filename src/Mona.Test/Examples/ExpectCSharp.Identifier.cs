using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.Test.Examples
{
    /// <summary>
    /// Interpretation of the C# grammar listed at
    /// http://msdn.microsoft.com/en-us/library/aa664812(v=vs.71).aspx
    /// </summary>
    public static partial class ExpectCSharp
    {
        /// <summary>
        /// Creates a parser for an identifier
        /// </summary>
        /// <remarks>
        /// identifier:
        ///   available-identifier
        ///   @ identifier-or-keyword
        /// </remarks>
        /// <returns>the parser</returns>
        public static IParser<char, string> Identifier()
        {
            return Expect.OneOf(
                AvailableIdentifier(), 
                Expect.Concatenation(
                    Expect.Char('@'),
                    IdentifierOrKeyWord(),
                    (c, identifierOrKeyWord) => identifierOrKeyWord)
                );
        }

        /// <summary>
        /// Creates a parser for an identifier that isn't a keyword
        /// </summary>
        /// <remarks>
        ///  available-identifier:
        ///    An identifier-or-keyword that is not a keyword
        /// </remarks>
        /// <returns></returns>
        public static IParser<char, string> AvailableIdentifier()
        {
            return IdentifierOrKeyWord();
        }

        /// <summary>
        /// Creates a parser for any valid identifier and/or keyword
        /// </summary>
        /// identifier-or-keyword:
        ///   identifier-start-character   identifier-part-charactersopt
        /// </remarks>
        /// <returns></returns>
        public static IParser<char, string> IdentifierOrKeyWord()
        {
            return Expect.Concatenation(
                IdentifierStartCharacter(),
                IdentifierPartCharacter()
                    .ZeroOrMore(chars => new String(chars.ToArray())),
                (start, parts) => String.Concat(start, parts)
            );
        }

        /// <summary>
        /// Creates a parser for identifier-start-character
        /// </summary>
        /// identifier-start-character:
        ///   letter-character
        ///   _ (the underscore character U+005F)
        /// letter-character:
        ///   A Unicode character of classes Lu, Ll, Lt, Lm, Lo, or Nl 
        ///   A unicode-escape-sequence representing a character of classes Lu, Ll, Lt, Lm, Lo, or Nl
        /// </remarks>
        /// <returns></returns>
        public static IParser<char, char> IdentifierStartCharacter()
        {
            return Expect.Char(symbol =>
            {
                if (symbol == '_')
                {
                    return true;
                }
                switch (CharUnicodeInfo.GetUnicodeCategory(symbol))
                {
                    case UnicodeCategory.UppercaseLetter: //Lu
                    case UnicodeCategory.LowercaseLetter: //Ll
                    case UnicodeCategory.TitlecaseLetter: //Lt
                    case UnicodeCategory.ModifierLetter: //Lm
                    case UnicodeCategory.OtherLetter: //Lo
                    case UnicodeCategory.LetterNumber: //Nl
                        return true;
                    default:
                        return false;
                }
            });
        }

        /// <summary>
        /// Creates a Parser for identifier-part-character
        /// </summary>
        /// identifier-part-character:
        ///   letter-character
        ///   decimal-digit-character
        ///   connecting-character
        ///   combining-character
        ///   formatting-character
        /// letter-character:
        ///   A Unicode character of classes Lu, Ll, Lt, Lm, Lo, or Nl 
        ///   A unicode-escape-sequence representing a character of classes Lu, Ll, Lt, Lm, Lo, or Nl
        /// combining-character:
        ///   A Unicode character of classes Mn or Mc 
        ///   A unicode-escape-sequence representing a character of classes Mn or Mc
        /// decimal-digit-character:
        ///   A Unicode character of the class Nd 
        ///   A unicode-escape-sequence representing a character of the class Nd
        /// connecting-character:
        ///   A Unicode character of the class Pc 
        ///   A unicode-escape-sequence representing a character of the class Pc
        /// formatting-character:
        ///   A Unicode character of the class Cf 
        ///   A unicode-escape-sequence representing a character of the class Cf
        /// </remarks>
        /// <returns></returns>
        public static IParser<char, char> IdentifierPartCharacter()
        {
            return Expect.Char(symbol =>
            {
                switch (CharUnicodeInfo.GetUnicodeCategory(symbol))
                {
                    case UnicodeCategory.UppercaseLetter: //Lu
                    case UnicodeCategory.LowercaseLetter: //Ll
                    case UnicodeCategory.TitlecaseLetter: //Lt
                    case UnicodeCategory.ModifierLetter: //Lm
                    case UnicodeCategory.OtherLetter: //Lo
                    case UnicodeCategory.LetterNumber: //Nl
                    case UnicodeCategory.NonSpacingMark: //Mn
                    case UnicodeCategory.SpacingCombiningMark: //Mc
                    case UnicodeCategory.DecimalDigitNumber: //Nd
                    case UnicodeCategory.ConnectorPunctuation: //Pc
                    case UnicodeCategory.Format: //Cf
                        return true;
                    default:
                        return false;
                }
            });
        }
        

        

    }
}
