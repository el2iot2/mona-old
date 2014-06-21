using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// All basic parser generators and extension methods for combining them
    /// </summary>
    public static partial class Parsers
    {
        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate and failure message
        /// </summary>
        /// <typeparam name="TInput">The type of the input</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>(Func<TInput, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ?? 
                Strings.ErrorSinglePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, TInput>(
                parseFunc: input => input
                    .Take(1)
                    .Select(
                        symbol => predicate(symbol) ?
                            new Parse<TInput, TInput>(
                                node: symbol, 
                                remainder: input.Skip(1), //advance the input
                                error: null  //Success
                            ) :
                            new Parse<TInput, TInput>(
                                node: symbol,
                                remainder: input,
                                error: new Exception(failureMessage) //Error
                            )
                    )     
            );
        }

        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TSource, TSource> Single<TSource>(Func<TSource, bool> predicate)
        {
            return Single<TSource>(predicate: predicate, failureMessage: null);
        }
        

        /// <summary>
        /// Creates a parser that parses a single symbol
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="failureMessage"></param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>(string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorSinglePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, TInput>(
                parseFunc: input => input
                    .Take(1)
                    .Select(
                        symbol => new Parse<TInput, TInput>(
                                node: symbol,
                                remainder: input.Skip(1), //advance the input
                                error: null  //Success
                            )
                    )
            );
        }

        /// <summary>
        /// Creates a parser that parses a single symbol
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>()
        {
            return Single<TInput>(failureMessage: null);
        }
    }
}
