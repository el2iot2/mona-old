﻿using System;
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
    }
}