using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    public interface INode<TSource>
    {
        IObservable<TSource> Remainder { get; }

        Exception Error { get; }
    }

    public static class NodeExtensions
    {
        public static bool Failed<TSource>(this INode<TSource> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            return node.Error != null;
        }
    }
}
