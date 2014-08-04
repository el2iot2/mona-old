/*This file is generated to generate the generics involved...Do not modify directly*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona
{
    /// <summary>
    /// Fluent type for creating parser generators via a set of primitives
    /// </summary>
    public static partial class Expect
    {
		/// <summary>
        /// Creates a parser that expects a concatenation of 2 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <param name="parseSelector">A function that builds the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			Func<
				IEnumerable<TInput>,
				Exception,
				IParse<TInput, TNode1>,
				IParse<TInput, TNode2>,
				IParse<TInput, TConcatNode>
			> parseSelector
			)
        {
            return Parser.Create<TInput, TConcatNode>(
                parse: input =>
                {
                    var parse1 = parser1
                        .Parse(input);
                    
					var input2 = parse1 != null ? parse1.Remainder : input;

					var parse2 = parser2
						.Parse(input2);

					

                    var remainder = parse2 != null ? parse2.Remainder : input;
                    Exception error = null;
                    var errors = new List<Exception>();

                    if (parse1.Failed()) { errors.Add(parse1.Error); }
					if (parse2.Failed()) { errors.Add(parse2.Error); }
					
                    if (errors.Any())
                    {
                        error = new AggregateException(
                            errors.First().Message, 
                            errors.ToArray());
                    }

					var parse = parseSelector(
						remainder,
						error,
                        parse1,
						parse2
						);

					return parse;
                });
        }
		
		/// <summary>
		/// Creates a parser that expects a concatenation of 2 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <param name="nodeSelector">A function that builds the node for the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			Func<
				TNode1,
				TNode2,
				TConcatNode
			> nodeSelector
			)
        {
            return Concatenation(
			parser1,
			parser2,
			parseSelector: (
				remainder, 
				error, 
				parse1,
			parse2
			) => new Parse<TInput, TConcatNode>(
				remainder: remainder,
				error: error,
				node: nodeSelector(
				parse1.Node,
				parse2.Node
				))
                );
        }

        /// <summary>
        /// Creates a parser that expects a concatenation of 3 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <param name="parseSelector">A function that builds the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			Func<
				IEnumerable<TInput>,
				Exception,
				IParse<TInput, TNode1>,
				IParse<TInput, TNode2>,
				IParse<TInput, TNode3>,
				IParse<TInput, TConcatNode>
			> parseSelector
			)
        {
            return Parser.Create<TInput, TConcatNode>(
                parse: input =>
                {
                    var parse1 = parser1
                        .Parse(input);
                    
					var input2 = parse1 != null ? parse1.Remainder : input;

					var parse2 = parser2
						.Parse(input2);

					var input3 = parse2 != null ? parse2.Remainder : input;

					var parse3 = parser3
						.Parse(input3);

					

                    var remainder = parse3 != null ? parse3.Remainder : input;
                    Exception error = null;
                    var errors = new List<Exception>();

                    if (parse1.Failed()) { errors.Add(parse1.Error); }
					if (parse2.Failed()) { errors.Add(parse2.Error); }
					if (parse3.Failed()) { errors.Add(parse3.Error); }
					
                    if (errors.Any())
                    {
                        error = new AggregateException(
                            errors.First().Message, 
                            errors.ToArray());
                    }

					var parse = parseSelector(
						remainder,
						error,
                        parse1,
						parse2,
						parse3
						);

					return parse;
                });
        }
		
		/// <summary>
		/// Creates a parser that expects a concatenation of 3 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <param name="nodeSelector">A function that builds the node for the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			Func<
				TNode1,
				TNode2,
				TNode3,
				TConcatNode
			> nodeSelector
			)
        {
            return Concatenation(
			parser1,
			parser2,
			parser3,
			parseSelector: (
				remainder, 
				error, 
				parse1,
			parse2,
			parse3
			) => new Parse<TInput, TConcatNode>(
				remainder: remainder,
				error: error,
				node: nodeSelector(
				parse1.Node,
				parse2.Node,
				parse3.Node
				))
                );
        }

        /// <summary>
        /// Creates a parser that expects a concatenation of 4 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <param name="parseSelector">A function that builds the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			Func<
				IEnumerable<TInput>,
				Exception,
				IParse<TInput, TNode1>,
				IParse<TInput, TNode2>,
				IParse<TInput, TNode3>,
				IParse<TInput, TNode4>,
				IParse<TInput, TConcatNode>
			> parseSelector
			)
        {
            return Parser.Create<TInput, TConcatNode>(
                parse: input =>
                {
                    var parse1 = parser1
                        .Parse(input);
                    
					var input2 = parse1 != null ? parse1.Remainder : input;

					var parse2 = parser2
						.Parse(input2);

					var input3 = parse2 != null ? parse2.Remainder : input;

					var parse3 = parser3
						.Parse(input3);

					var input4 = parse3 != null ? parse3.Remainder : input;

					var parse4 = parser4
						.Parse(input4);

					

                    var remainder = parse4 != null ? parse4.Remainder : input;
                    Exception error = null;
                    var errors = new List<Exception>();

                    if (parse1.Failed()) { errors.Add(parse1.Error); }
					if (parse2.Failed()) { errors.Add(parse2.Error); }
					if (parse3.Failed()) { errors.Add(parse3.Error); }
					if (parse4.Failed()) { errors.Add(parse4.Error); }
					
                    if (errors.Any())
                    {
                        error = new AggregateException(
                            errors.First().Message, 
                            errors.ToArray());
                    }

					var parse = parseSelector(
						remainder,
						error,
                        parse1,
						parse2,
						parse3,
						parse4
						);

					return parse;
                });
        }
		
		/// <summary>
		/// Creates a parser that expects a concatenation of 4 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <param name="nodeSelector">A function that builds the node for the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			Func<
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TConcatNode
			> nodeSelector
			)
        {
            return Concatenation(
			parser1,
			parser2,
			parser3,
			parser4,
			parseSelector: (
				remainder, 
				error, 
				parse1,
			parse2,
			parse3,
			parse4
			) => new Parse<TInput, TConcatNode>(
				remainder: remainder,
				error: error,
				node: nodeSelector(
				parse1.Node,
				parse2.Node,
				parse3.Node,
				parse4.Node
				))
                );
        }

        /// <summary>
        /// Creates a parser that expects a concatenation of 5 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <typeparam name="TNode5">The type of the tree node for parser5</typeparam>
		/// <param name="parseSelector">A function that builds the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <param name="parser5">parser5</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			IParser<TInput, TNode5> parser5,
			Func<
				IEnumerable<TInput>,
				Exception,
				IParse<TInput, TNode1>,
				IParse<TInput, TNode2>,
				IParse<TInput, TNode3>,
				IParse<TInput, TNode4>,
				IParse<TInput, TNode5>,
				IParse<TInput, TConcatNode>
			> parseSelector
			)
        {
            return Parser.Create<TInput, TConcatNode>(
                parse: input =>
                {
                    var parse1 = parser1
                        .Parse(input);
                    
					var input2 = parse1 != null ? parse1.Remainder : input;

					var parse2 = parser2
						.Parse(input2);

					var input3 = parse2 != null ? parse2.Remainder : input;

					var parse3 = parser3
						.Parse(input3);

					var input4 = parse3 != null ? parse3.Remainder : input;

					var parse4 = parser4
						.Parse(input4);

					var input5 = parse4 != null ? parse4.Remainder : input;

					var parse5 = parser5
						.Parse(input5);

					

                    var remainder = parse5 != null ? parse5.Remainder : input;
                    Exception error = null;
                    var errors = new List<Exception>();

                    if (parse1.Failed()) { errors.Add(parse1.Error); }
					if (parse2.Failed()) { errors.Add(parse2.Error); }
					if (parse3.Failed()) { errors.Add(parse3.Error); }
					if (parse4.Failed()) { errors.Add(parse4.Error); }
					if (parse5.Failed()) { errors.Add(parse5.Error); }
					
                    if (errors.Any())
                    {
                        error = new AggregateException(
                            errors.First().Message, 
                            errors.ToArray());
                    }

					var parse = parseSelector(
						remainder,
						error,
                        parse1,
						parse2,
						parse3,
						parse4,
						parse5
						);

					return parse;
                });
        }
		
		/// <summary>
		/// Creates a parser that expects a concatenation of 5 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <typeparam name="TNode5">The type of the tree node for parser5</typeparam>
		/// <param name="nodeSelector">A function that builds the node for the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <param name="parser5">parser5</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			IParser<TInput, TNode5> parser5,
			Func<
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5,
				TConcatNode
			> nodeSelector
			)
        {
            return Concatenation(
			parser1,
			parser2,
			parser3,
			parser4,
			parser5,
			parseSelector: (
				remainder, 
				error, 
				parse1,
			parse2,
			parse3,
			parse4,
			parse5
			) => new Parse<TInput, TConcatNode>(
				remainder: remainder,
				error: error,
				node: nodeSelector(
				parse1.Node,
				parse2.Node,
				parse3.Node,
				parse4.Node,
				parse5.Node
				))
                );
        }

        /// <summary>
        /// Creates a parser that expects a concatenation of 6 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <typeparam name="TNode5">The type of the tree node for parser5</typeparam>
		/// <typeparam name="TNode6">The type of the tree node for parser6</typeparam>
		/// <param name="parseSelector">A function that builds the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <param name="parser5">parser5</param>
		/// <param name="parser6">parser6</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5,
				TNode6
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			IParser<TInput, TNode5> parser5,
			IParser<TInput, TNode6> parser6,
			Func<
				IEnumerable<TInput>,
				Exception,
				IParse<TInput, TNode1>,
				IParse<TInput, TNode2>,
				IParse<TInput, TNode3>,
				IParse<TInput, TNode4>,
				IParse<TInput, TNode5>,
				IParse<TInput, TNode6>,
				IParse<TInput, TConcatNode>
			> parseSelector
			)
        {
            return Parser.Create<TInput, TConcatNode>(
                parse: input =>
                {
                    var parse1 = parser1
                        .Parse(input);
                    
					var input2 = parse1 != null ? parse1.Remainder : input;

					var parse2 = parser2
						.Parse(input2);

					var input3 = parse2 != null ? parse2.Remainder : input;

					var parse3 = parser3
						.Parse(input3);

					var input4 = parse3 != null ? parse3.Remainder : input;

					var parse4 = parser4
						.Parse(input4);

					var input5 = parse4 != null ? parse4.Remainder : input;

					var parse5 = parser5
						.Parse(input5);

					var input6 = parse5 != null ? parse5.Remainder : input;

					var parse6 = parser6
						.Parse(input6);

					

                    var remainder = parse6 != null ? parse6.Remainder : input;
                    Exception error = null;
                    var errors = new List<Exception>();

                    if (parse1.Failed()) { errors.Add(parse1.Error); }
					if (parse2.Failed()) { errors.Add(parse2.Error); }
					if (parse3.Failed()) { errors.Add(parse3.Error); }
					if (parse4.Failed()) { errors.Add(parse4.Error); }
					if (parse5.Failed()) { errors.Add(parse5.Error); }
					if (parse6.Failed()) { errors.Add(parse6.Error); }
					
                    if (errors.Any())
                    {
                        error = new AggregateException(
                            errors.First().Message, 
                            errors.ToArray());
                    }

					var parse = parseSelector(
						remainder,
						error,
                        parse1,
						parse2,
						parse3,
						parse4,
						parse5,
						parse6
						);

					return parse;
                });
        }
		
		/// <summary>
		/// Creates a parser that expects a concatenation of 6 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <typeparam name="TNode5">The type of the tree node for parser5</typeparam>
		/// <typeparam name="TNode6">The type of the tree node for parser6</typeparam>
		/// <param name="nodeSelector">A function that builds the node for the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <param name="parser5">parser5</param>
		/// <param name="parser6">parser6</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5,
				TNode6
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			IParser<TInput, TNode5> parser5,
			IParser<TInput, TNode6> parser6,
			Func<
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5,
				TNode6,
				TConcatNode
			> nodeSelector
			)
        {
            return Concatenation(
			parser1,
			parser2,
			parser3,
			parser4,
			parser5,
			parser6,
			parseSelector: (
				remainder, 
				error, 
				parse1,
			parse2,
			parse3,
			parse4,
			parse5,
			parse6
			) => new Parse<TInput, TConcatNode>(
				remainder: remainder,
				error: error,
				node: nodeSelector(
				parse1.Node,
				parse2.Node,
				parse3.Node,
				parse4.Node,
				parse5.Node,
				parse6.Node
				))
                );
        }

        /// <summary>
        /// Creates a parser that expects a concatenation of 7 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <typeparam name="TNode5">The type of the tree node for parser5</typeparam>
		/// <typeparam name="TNode6">The type of the tree node for parser6</typeparam>
		/// <typeparam name="TNode7">The type of the tree node for parser7</typeparam>
		/// <param name="parseSelector">A function that builds the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <param name="parser5">parser5</param>
		/// <param name="parser6">parser6</param>
		/// <param name="parser7">parser7</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5,
				TNode6,
				TNode7
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			IParser<TInput, TNode5> parser5,
			IParser<TInput, TNode6> parser6,
			IParser<TInput, TNode7> parser7,
			Func<
				IEnumerable<TInput>,
				Exception,
				IParse<TInput, TNode1>,
				IParse<TInput, TNode2>,
				IParse<TInput, TNode3>,
				IParse<TInput, TNode4>,
				IParse<TInput, TNode5>,
				IParse<TInput, TNode6>,
				IParse<TInput, TNode7>,
				IParse<TInput, TConcatNode>
			> parseSelector
			)
        {
            return Parser.Create<TInput, TConcatNode>(
                parse: input =>
                {
                    var parse1 = parser1
                        .Parse(input);
                    
					var input2 = parse1 != null ? parse1.Remainder : input;

					var parse2 = parser2
						.Parse(input2);

					var input3 = parse2 != null ? parse2.Remainder : input;

					var parse3 = parser3
						.Parse(input3);

					var input4 = parse3 != null ? parse3.Remainder : input;

					var parse4 = parser4
						.Parse(input4);

					var input5 = parse4 != null ? parse4.Remainder : input;

					var parse5 = parser5
						.Parse(input5);

					var input6 = parse5 != null ? parse5.Remainder : input;

					var parse6 = parser6
						.Parse(input6);

					var input7 = parse6 != null ? parse6.Remainder : input;

					var parse7 = parser7
						.Parse(input7);

					

                    var remainder = parse7 != null ? parse7.Remainder : input;
                    Exception error = null;
                    var errors = new List<Exception>();

                    if (parse1.Failed()) { errors.Add(parse1.Error); }
					if (parse2.Failed()) { errors.Add(parse2.Error); }
					if (parse3.Failed()) { errors.Add(parse3.Error); }
					if (parse4.Failed()) { errors.Add(parse4.Error); }
					if (parse5.Failed()) { errors.Add(parse5.Error); }
					if (parse6.Failed()) { errors.Add(parse6.Error); }
					if (parse7.Failed()) { errors.Add(parse7.Error); }
					
                    if (errors.Any())
                    {
                        error = new AggregateException(
                            errors.First().Message, 
                            errors.ToArray());
                    }

					var parse = parseSelector(
						remainder,
						error,
                        parse1,
						parse2,
						parse3,
						parse4,
						parse5,
						parse6,
						parse7
						);

					return parse;
                });
        }
		
		/// <summary>
		/// Creates a parser that expects a concatenation of 7 parsers
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
		/// <typeparam name="TConcatNode">The resulting tree node type</typeparam>
		/// <typeparam name="TNode1">The type of the tree node for parser1</typeparam>
		/// <typeparam name="TNode2">The type of the tree node for parser2</typeparam>
		/// <typeparam name="TNode3">The type of the tree node for parser3</typeparam>
		/// <typeparam name="TNode4">The type of the tree node for parser4</typeparam>
		/// <typeparam name="TNode5">The type of the tree node for parser5</typeparam>
		/// <typeparam name="TNode6">The type of the tree node for parser6</typeparam>
		/// <typeparam name="TNode7">The type of the tree node for parser7</typeparam>
		/// <param name="nodeSelector">A function that builds the node for the resulting parse object</param>
		/// <param name="parser1">parser1</param>
		/// <param name="parser2">parser2</param>
		/// <param name="parser3">parser3</param>
		/// <param name="parser4">parser4</param>
		/// <param name="parser5">parser5</param>
		/// <param name="parser6">parser6</param>
		/// <param name="parser7">parser7</param>
		/// <returns>A parser that represents the concatenation</returns>
        public static IParser<TInput, TConcatNode>
            Concatenation<
				TInput, 
				TConcatNode,
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5,
				TNode6,
				TNode7
				>(
            IParser<TInput, TNode1> parser1,
			IParser<TInput, TNode2> parser2,
			IParser<TInput, TNode3> parser3,
			IParser<TInput, TNode4> parser4,
			IParser<TInput, TNode5> parser5,
			IParser<TInput, TNode6> parser6,
			IParser<TInput, TNode7> parser7,
			Func<
				TNode1,
				TNode2,
				TNode3,
				TNode4,
				TNode5,
				TNode6,
				TNode7,
				TConcatNode
			> nodeSelector
			)
        {
            return Concatenation(
			parser1,
			parser2,
			parser3,
			parser4,
			parser5,
			parser6,
			parser7,
			parseSelector: (
				remainder, 
				error, 
				parse1,
			parse2,
			parse3,
			parse4,
			parse5,
			parse6,
			parse7
			) => new Parse<TInput, TConcatNode>(
				remainder: remainder,
				error: error,
				node: nodeSelector(
				parse1.Node,
				parse2.Node,
				parse3.Node,
				parse4.Node,
				parse5.Node,
				parse6.Node,
				parse7.Node
				))
                );
        }

        
    }
}
