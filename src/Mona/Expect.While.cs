using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// All basic parser generators and extension methods for combining them
    /// </summary>
    public static partial class Expect
    {
        /// <summary>
        /// Creates a parser that parses input symbols while the supplied predicate matches them
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <returns>The parser.</returns>
        public static IParser<TInput, IEnumerable<TInput>> While<TInput>(Func<TInput, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorWhilePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, IEnumerable<TInput>>(
                parse: input => {
                    var symbols = input
                        .TakeWhile(predicate)
                        .ToList();
                    return new Parse<TInput, IEnumerable<TInput>>(
                            node: symbols,
                            remainder: input.Skip(symbols.Count), //advance the input
                            error: null  //Success
                        );
                }
            );
        }

        /// <summary>
        /// Creates a parser that parses input symbols while the supplied predicate matches them
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns>The parser.</returns>
        public static IParser<TInput, IEnumerable<TInput>> While<TInput>(Func<TInput, bool> predicate)
        {
            return While<TInput>(predicate: predicate, failureMessage: null);
        }

        /// <summary>
        /// Creates a parser that parses input symbols while the supplied predicate matches them
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <returns>The parser.</returns>
        public static IParser<TInput, IEnumerable<TInput>> While<TInput>(Func<TInput, int, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorWhilePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, IEnumerable<TInput>>(
                parse: input =>
                {
                    var symbols = input
                        .TakeWhile(predicate)
                        .ToList();
                    return new Parse<TInput, IEnumerable<TInput>>(
                            node: symbols,
                            remainder: input, //advance the input
                            error: null  //Success
                        );
                }
            );
        }

        /// <summary>
        /// Creates a parser that parses input symbols while the supplied predicate matches them
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <returns>The parser.</returns>
        public static IParser<TInput, IEnumerable<TInput>> While<TInput>(Func<TInput, int, bool> predicate)
        {
            return While<TInput>(predicate: predicate, failureMessage: null);
        }
    }
}
