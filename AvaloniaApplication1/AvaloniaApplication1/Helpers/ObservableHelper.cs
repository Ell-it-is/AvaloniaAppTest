using System;
using System.Reactive.Linq;

namespace AvaloniaApplication1.Helpers;

public static class ObservableHelper {
    public static IObservable<TSource> Trace<TSource>(this IObservable<TSource> source, string name) {
        int id = 0;
        return Observable.Create<TSource>(observer => {
            int id1 = ++id;
            Action<string, object> trace = (m, v) => Console.WriteLine("{0}{1}: {2}({3})", name, id1, m, v);
            trace("Subscribe", "");
            IDisposable disposable = source.Subscribe(
                v => { trace("OnNext", v); observer.OnNext(v); },
                e => { trace("OnError", ""); observer.OnError(e); },
                () => { trace("OnCompleted", ""); observer.OnCompleted(); });
            return () => { trace("Dispose", ""); disposable.Dispose(); };
        });
    }
}