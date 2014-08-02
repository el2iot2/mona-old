using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
        /// Creates a parser that parses a single symbol with the specified predicate and failure message
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting tree/node</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <param name="nodeSelector">A function to select the resulting node/tree type</param>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TNode> Single<TInput, TNode>(Func<TInput, bool> predicate, Func<TInput, TNode> nodeSelector, string failureMessage)
        {
            failureMessage = failureMessage ?? 
                Strings.ErrorSinglePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, TNode>(
                parse: async input =>
                {
                    var symbols = await input.Take(1).ToList();
                    if (symbols.Any())
                    {
                        if (predicate(symbols[0]))
                        {
                            return
                                new Parse<TInput, TNode>(
                                node: nodeSelector(symbols[0]),
                                remainder: input,
                                error: null);  //Success
                        }
                        else
                        {
                            return
                                new Parse<TInput, TNode>(
                                node: default(TNode),
                                remainder: input.StartWith(symbols).Publish(), //resend the consumed symbol
                                error: new Exception(failureMessage));
                        }
                    }
                    else
                    {
                        return 
                            new Parse<TInput, TNode>(
                            node: default(TNode),
                            remainder: input,
                            error: new Exception(failureMessage) //Error
                        );
                    }
                }
            );
        }

        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate and failure message
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
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
                parse: async input =>
                    {
                        using(input.Connect())
                        {
                            var symbols = await input.Take(1).ToList();
                            if (symbols.Any())
                            {
                                if (predicate == null || predicate(symbols[0]))
                                {
                                    return
                                        new Parse<TInput, TInput>(
                                        node: symbols[0],
                                        remainder: input,
                                        error: null);  //Success
                                }
                                else
                                {
                                    return
                                        new Parse<TInput, TInput>(
                                        node: symbols[0],
                                        remainder: input.StartWith(symbols).Publish(), //resend the consumed symbol
                                        error: new Exception(failureMessage));
                                }   
                            }
                            else
                            {
                                return new Parse<TInput, TInput>(
                                    node: default(TInput),
                                    remainder: input,
                                    error: new Exception(failureMessage) //Error
                                );
                            }
                        }
                    }
            );
        }

        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>(Func<TInput, bool> predicate)
        {
            return Single<TInput>(predicate: predicate, failureMessage: null);
        }

        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting tree/node</typeparam>
        /// <param name="nodeSelector"></param>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TNode> Single<TInput, TNode>(Func<TInput, bool> predicate, Func<TInput, TNode> nodeSelector)
        {
            return Single<TInput, TNode>(predicate: predicate, nodeSelector: nodeSelector, failureMessage: null);
        }
        

        /// <summary>
        /// Creates a parser that parses a single symbol
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>(string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorSinglePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Single<TInput>(predicate: null, failureMessage: failureMessage);
        }

        /// <summary>
        /// Creates a parser that parses a single symbol
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting tree/node</typeparam>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <param name="nodeSelector"></param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TNode> Single<TInput, TNode>(Func<TInput, TNode> nodeSelector, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorSinglePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Single<TInput, TNode>(predicate: null, nodeSelector: nodeSelector, failureMessage: failureMessage);
        }

        /// <summary>
        /// Creates a parser that parses a single symbol
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>()
        {
            return Single<TInput>(failureMessage: null);
        }

        /// <summary>
        /// Creates a parser that parses a single symbol
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting tree/node</typeparam>
        /// <param name="nodeSelector"></param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TNode> Single<TInput, TNode>(Func<TInput, TNode> nodeSelector)
        {
            return Single<TInput, TNode>(nodeSelector: nodeSelector, failureMessage: null);
        }
    }
}
