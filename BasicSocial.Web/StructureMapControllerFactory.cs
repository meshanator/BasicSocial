using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace BasicSocial.Web
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		private readonly IContainer _container;

		public StructureMapControllerFactory(IContainer container)
		{
			_container = container;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return base.GetControllerInstance(requestContext, controllerType);
			}

			return _container.GetInstance(controllerType) as IController;

			//return (IController)StructureMap.ObjectFactory.GetInstance(controllerType);
		}
	}
}