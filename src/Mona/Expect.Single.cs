using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona
{
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

            return Parser.Create<TInput, TNode>(
                parse: input =>
                {
                    var first = input.FirstOrDefault();
                    if (!first.Equals(default(TInput)))
                    {
                        if (predicate == null || predicate(first))
                        {
                            return
                                new Parse<TInput, TNode>(
                                node: nodeSelector(first),
                                remainder: input.Skip(1),
                                error: null);  //Success
                        }
                        else
                        {
                            return
                                new Parse<TInput, TNode>(
                                node: default(TNode),
                                remainder: input, //resend the consumed symbol
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
        /// Creates a parser that parses a single symbol with the specified predicate
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting tree/node</typeparam>
        /// <param name="nodeSelector">A function to select the resulting node/tree type</param>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TNode> Single<TInput, TNode>(Func<TInput, bool> predicate, Func<TInput, TNode> nodeSelector)
        {
            return Single<TInput, TNode>(
                predicate: predicate, 
                nodeSelector: nodeSelector, 
                failureMessage: null);
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
            return Single<TInput, TInput>(
                predicate: predicate,
                nodeSelector: input => input,
                failureMessage: failureMessage);
        }
        
        /// <summary>
        /// Creates a parser that parses a single symbol with the specified predicate
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>(Func<TInput, bool> predicate)
        {
            return Single<TInput>(
                predicate: predicate, 
                failureMessage: null);
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
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TInput> Single<TInput>()
        {
            return Single<TInput>(failureMessage: null);
        }

        
    }
}
