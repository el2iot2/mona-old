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
        /// Explicitly create a parser via a parse func
        /// </summary>
        /// <typeparam name="TInput">The type of the input</typeparam>
        /// <typeparam name="TNode">The type of the resulting node</typeparam>
        /// <returns></returns>
        public static IParser<TInput, TNode> Create<TInput, TNode>(Func<IObservable<TInput>, IObservable<IParse<TInput,TNode>>> parseFunc)
        {
            return new Parser<TInput, TNode>(parseFunc: parseFunc);
        }

        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate and failure message
        /// </summary>
        /// <typeparam name="TInput">The type of the input</typeparam>
        /// <param name="predicate"></param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IParser<TInput, TInput> SingleSymbol<TInput>(Func<TInput, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ?? 
                Strings.ErrorSingleSymbolPredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, TInput>(
                parseFunc: input => input
                    .Take(1)
                    .Select(
                        symbol => 
                            new Parse<TInput, TInput>(
                                node: symbol, 
                                remainder: input,
                                error: predicate(symbol) ?
                                    null : //Success
                                    new Exception(failureMessage) //Error
                            )
                    )     
            );
        }

        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IParser<TSource, TSource> SingleSymbol<TSource>(Func<TSource, bool> predicate)
        {
            return SingleSymbol<TSource>(predicate: predicate, failureMessage: null);
        }

        
    }
}
