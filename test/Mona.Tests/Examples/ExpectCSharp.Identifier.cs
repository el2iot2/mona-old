using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
                    (at, identifierOrKeyWord) => string.Concat(at,identifierOrKeyWord))
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
            return IdentifierOrKeyWord()
                .Select(parse => {
                    if (Keywords.Contains(parse.Node))
                    {
                        return parse.WithError(
                            new Exception("Identifier '{0}' is a keyword. use '@{0}'".Interpolate(parse.Node)));
                    }
                    return parse;
                });
        }

        public static readonly HashSet<string> Keywords = new HashSet<string>(EnumerateKeywords());

        public static IEnumerable<string> EnumerateKeywords()
        {
            yield return "abstract";
            yield return "as";
            yield return "base"; 
            yield return "bool"; 
            yield return "break";
            yield return "byte"; 
            yield return "case"; 
            yield return "catch"; 
            yield return "char"; 
            yield return "checked"; 
            yield return "class"; 
            yield return "const"; 
            yield return "continue"; 
            yield return "decimal"; 
            yield return "default"; 
            yield return "delegate"; 
            yield return "do"; 
            yield return "double"; 
            yield return "else"; 
            yield return "enum"; 
            yield return "event"; 
            yield return "explicit"; 
            yield return "extern"; 
            yield return "false"; 
            yield return "finally"; 
            yield return "fixed"; 
            yield return "float"; 
            yield return "for"; 
            yield return "foreach"; 
            yield return "goto"; 
            yield return "if"; 
            yield return "implicit"; 
            yield return "in"; 
            yield return "int"; 
            yield return "interface"; 
            yield return "internal"; 
            yield return "is"; 
            yield return "lock"; 
            yield return "long"; 
            yield return "namespace"; 
            yield return "new"; 
            yield return "null"; 
            yield return "object"; 
            yield return "operator"; 
            yield return "out"; 
            yield return "override"; 
            yield return "params"; 
            yield return "private"; 
            yield return "protected"; 
            yield return "public"; 
            yield return "readonly"; 
            yield return "ref"; 
            yield return "return"; 
            yield return "sbyte"; 
            yield return "sealed"; 
            yield return "short"; 
            yield return "sizeof"; 
            yield return "stackalloc"; 
            yield return "static"; 
            yield return "string"; 
            yield return "struct"; 
            yield return "switch"; 
            yield return "this"; 
            yield return "throw"; 
            yield return "true"; 
            yield return "try"; 
            yield return "typeof"; 
            yield return "uint";
            yield return "ulong"; 
            yield return "unchecked"; 
            yield return "unsafe"; 
            yield return "ushort"; 
            yield return "using"; 
            yield return "virtual"; 
            yield return "void";
            yield return "volatile"; 
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
