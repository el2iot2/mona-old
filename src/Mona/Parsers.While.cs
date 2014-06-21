using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// All basic parser generateors and extension methods for combining them
    /// </summary>
    public static partial class Parsers
    {
        /// <summary>
        /// Creates a parser that parses input symbols while the supplied predicate matches them
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IParser<TInput, TInput[]> While<TInput>(Func<TInput, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorWhilePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, TInput[]>(
                parseFunc: input => input
                    .TakeWhile(predicate)
                    .ToArray()
                    .Select(
                        symbols => 
                            new Parse<TInput, TInput[]>(
                                node: symbols,
                                remainder: input.Skip(symbols.Length), //advance the input
                                error: null  //Success
                            )
                    )
            );
        }

        /// <summary>
        /// Creates a parser that parses input symbols while the supplied predicate matches them
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns></returns>
        public static IParser<TInput, TInput[]> While<TInput>(Func<TInput, bool> predicate)
        {
            return While<TInput>(predicate: predicate, failureMessage: null);
        }

        /// <summary>
        /// Creates a parser that parses input symbols while the supplied predicate matches them
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IParser<TInput, TInput[]> While<TInput>(Func<TInput, int, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorWhilePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, TInput[]>(
                parseFunc: input => input
                    .TakeWhile(predicate)
                    .ToArray()
                    .Select(
                        symbols =>
                            new Parse<TInput, TInput[]>(
                                node: symbols,
                                remainder: input.Skip(symbols.Length), //advance the input
                                error: null  //Success
                            )
                    )
            );
        }
    }
}
