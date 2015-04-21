using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Nontainer.WebApi
{
    public class WebApiControllerFactory : IHttpControllerActivator
    {
        Func<Scope, HttpRequestMessage, HttpControllerDescriptor, Type, IHttpController> factory;

        public void Use(Func<Scope, HttpRequestMessage, HttpControllerDescriptor, Type, IHttpController> factory)
        {
            this.factory = factory;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var requestScope = new Scope();
            var controller = factory(requestScope, request, controllerDescriptor, controllerType);
            request.RegisterForDispose(requestScope);
            return controller;
        }
    }
}