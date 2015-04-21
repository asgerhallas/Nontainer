using System;
using System.Collections.Generic;

namespace Nontainer
{
    public class Scope : IDisposable
    {
        readonly List<IDisposer> disposers = new List<IDisposer>();

        public T Using<T>(T disposable, Action<T> onDispose = null) where T : IDisposable
        {
            onDispose = onDispose ?? (x => { });

            disposers.Add(new Disposer<T>
            {
                Disposable = disposable,
                Dispose = x =>
                {
                    onDispose(x);
                    x.Dispose();
                }
            });

            return disposable;
        }

        public void Dispose()
        {
            foreach (var disposer in disposers)
            {
                disposer.Execute();
            }
        }

        public interface IDisposer
        {
            void Execute();
        }

        public class Disposer<T> : IDisposer
        {
            public Action<T> Dispose { get; set; }
            public T Disposable { get; set; }

            public void Execute()
            {
                Dispose(Disposable);
            }
        }
    }
}