using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace Nontainer.SignalR
{
    public interface ILifetimeHub : IHub
    {
        event Action OnDisposed;
    }
}