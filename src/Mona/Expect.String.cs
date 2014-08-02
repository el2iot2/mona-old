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
    public static partial class Expect
    {
        /// <summary>
        /// Creates a parser that expects a string of characters
        /// </summary>
        /// <param name="sequence">the expected sequence</param>
        /// <param name="failureMessage"></param>
        /// <param name="nodeSelector"></param>
        /// <returns>The parser.</returns>
        public static IParser<char, TNode> String<TNode>(string sequence, Func<IEnumerable<char>, TNode> nodeSelector, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorStringFormat.Interpolate(
                    sequence);
            return Sequence<char, TNode>(
                sequence: sequence, 
                failureMessage: failureMessage, 
                nodeSelector: nodeSelector, 
                equalityComparer: null);
        }

        /// <summary>
        /// Creates a parser that expects a string of characters
        /// </summary>
        /// <param name="sequence">the expected sequence</param>
        /// <param name="nodeSelector"></param>
        /// <returns>The parser.</returns>
        public static IParser<char, TNode> String<TNode>(string sequence, Func<IEnumerable<char>, TNode> nodeSelector)
        {
            return Sequence<char, TNode>(
                sequence: sequence,
                failureMessage: null,
                nodeSelector: nodeSelector,
                equalityComparer: null);
        }

        /// <summary>
        /// Creates a parser that expects a string of characters
        /// </summary>
        /// <param name="sequence">the expected sequence</param>
        /// <param name="failureMessage"></param>
        /// <returns>The parser.</returns>
        public static IParser<char, string> String(string sequence, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorStringFormat.Interpolate(
                    sequence);
            return String<string>(
                sequence: sequence, 
                failureMessage: failureMessage, 
                nodeSelector: chars => new string(chars.ToArray()));
        }


        /// <summary>
        /// Creates a parser that expects a string of characters
        /// </summary>
        /// <param name="sequence">the expected sequence</param>
        /// <returns>The parser.</returns>
        public static IParser<char, string> String(string sequence)
        {
            return String(
                sequence: sequence, 
                failureMessage: null);
        }

        

    }
}
