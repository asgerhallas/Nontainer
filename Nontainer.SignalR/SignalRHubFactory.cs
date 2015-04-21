using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace Nontainer.SignalR
{
    public class SignalRHubFactory : IHubActivator
    {
        Func<Scope, HubDescriptor, ILifetimeHub> factory;

        public void Use(Func<Scope, HubDescriptor, ILifetimeHub> factory)
        {
            this.factory = factory;
        }
        
        public IHub Create(HubDescriptor descriptor)
        {
            var requestScope = new Scope();
            var hub = factory(requestScope, descriptor);
            hub.OnDisposed += requestScope.Dispose;
            return hub;
        }
    }
}
