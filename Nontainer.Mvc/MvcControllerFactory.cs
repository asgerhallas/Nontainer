using System;
using System.Collections.Concurrent;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nontainer.Mvc
{
    public class MvcControllerFactory : DefaultControllerFactory
    {
        readonly ConcurrentDictionary<IController, Scope> controllers;

        Func<Scope, RequestContext, string, IController> factory;

        public MvcControllerFactory()
        {
            controllers = new ConcurrentDictionary<IController, Scope>();
        }

        public void Use(Func<Scope, RequestContext, string, IController> factory)
        {
            this.factory = factory;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var requestScope = new Scope();
            var controller = factory(requestScope, requestContext, controllerName);
            controllers.TryAdd(controller, requestScope);
            return controller;
        }

        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
            controllers[controller].Dispose();
        }
    }
}