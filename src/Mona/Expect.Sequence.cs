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
        /// Creates a parser that expects the specified sequence of symbols
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting tree/node</typeparam>
        /// <param name="sequence">the expected sequence</param>
        /// <param name="nodeSelector">A function to select the resulting node/tree type</param>
        /// <param name="equalityComparer">The equality comparer to use in the sequence check</param>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TNode> Sequence<TInput, TNode>(
            IEnumerable<TInput> sequence, 
            Func<IEnumerable<TInput>, TNode> nodeSelector, 
            string failureMessage, 
            IEqualityComparer<TInput> equalityComparer)
        {
            equalityComparer = equalityComparer ?? EqualityComparer<TInput>.Default;
            
            var expected = (sequence ?? Enumerable.Empty<TInput>()).ToList();

            failureMessage = failureMessage ?? 
                Strings.ErrorSequencePredicateFormat.Interpolate(
                    Strings.SymbolTypeSymbol,
                    Strings.PredicateUnspecified);

            return Create<TInput, TNode>(
                parse: async input =>
                {
                    var symbols = await input.Take(expected.Count).ToList();
                    if (symbols.SequenceEqual(expected, equalityComparer))
                    {
                        return 
                                new Parse<TInput, TNode>(
                                node: nodeSelector(symbols),
                            remainder: input,
                            error: null);  //Success
                    }
                    else
                    {
                        return new Parse<TInput, TNode>(
                            node: default(TNode),
                            remainder: input.StartWith(symbols).Publish(), //rewind the sequence for the next parser
                            error: new Exception(failureMessage) //Error
                        );
                    }
                }
            );
        }

        /// <summary>
        /// Creates a parser that expects the specified sequence of symbols
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting tree/node</typeparam>
        /// <param name="nodeSelector"></param>
        /// <param name="sequence">the expected sequence</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, TNode> Sequence<TInput, TNode>(IEnumerable<TInput> sequence, Func<IEnumerable<TInput>, TNode> nodeSelector)
        {
            return Sequence<TInput, TNode>(
                sequence: sequence,
                nodeSelector: nodeSelector,
                failureMessage: null,
                equalityComparer: null);
        }


        /// <summary>
        /// Creates a parser that expects the specified sequence of symbols
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <param name="sequence">the expected sequence</param>
        /// <param name="failureMessage">An error message returned on failure</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, IEnumerable<TInput>> Sequence<TInput>(IEnumerable<TInput> sequence, string failureMessage)
        {
            return Sequence<TInput, IEnumerable<TInput>>(
                sequence: sequence, 
                nodeSelector: symbols => symbols, 
                equalityComparer: null, 
                failureMessage: failureMessage);
        }

        /// <summary>
        /// Creates a parser that expects the specified sequence of symbols
        /// </summary>
        /// <typeparam name="TInput">The type of an input symbol</typeparam>
        /// <param name="sequence">the expected sequence</param>
        /// <returns>The specified parser.</returns>
        public static IParser<TInput, IEnumerable<TInput>> Sequence<TInput>(IEnumerable<TInput> sequence)
        {
            return Sequence<TInput>(
                sequence: sequence, 
                failureMessage: null);
        }

        
    }
}
