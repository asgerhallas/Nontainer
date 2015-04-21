using System;

namespace Nontainer
{
    public abstract class CompositionRoot : IDisposable
    {
        protected readonly Scope appScope = new Scope();

        protected Switch<T, R> Switch<T, R>(T subject)
        {
            return new Switch<T, R>(subject);
        }

        public void Dispose()
        {
            appScope.Dispose();
        }
    }
}