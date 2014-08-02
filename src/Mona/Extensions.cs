using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// Helpers/Extensions for common types
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Completes a string.Format to interpolate the arguments into the format in the current culture
        /// </summary>
        /// <param name="format">The format specifier</param>
        /// <param name="args">The format arguments</param>
        /// <returns></returns>
        public static string Interpolate(this string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args: args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<T> DelegateSubscribers<T>(IObservable<T> source)
        {
            var sourceSubscription = source.Subscribe(
                onNext: item => { },
                onCompleted: () => { },
                onError: exception => { }
                );

            var sourceObservable = Observable.Defer(
                observableFactory: () => Observable.Create<T>(
                    subscribe: observer => {

                        return Disposable.Create(() => { });
                    }));

            var sourceObserver = Observer.Create<T>(
                onNext: item => {},
                onCompleted: () => {},
                onError: exception => {}
                );

            return source.Multicast(
                subjectSelector: () => Subject.Create(
                    observer: sourceObserver,
                    observable: sourceObservable),
                    selector: intermediate => intermediate);
        }
    }
}
