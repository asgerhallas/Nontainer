using System;
using Microsoft.AspNet.SignalR;

namespace Nontainer.SignalR
{
    public class LifetimeHub : Hub, ILifetimeHub
    {
        public event Action OnDisposed = () => { };

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            OnDisposed();
        }
    }
}