using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace BasicSocial.Web
{
	public class StructureMapDependencyResolver : IDependencyResolver
	{
		private readonly IContainer _container;

		public StructureMapDependencyResolver(IContainer container)
		{
			_container = container;
		}

		public object GetService(Type serviceType) => _container.TryGetInstance(serviceType);

	    public IEnumerable<object> GetServices(Type serviceType) => _container.GetAllInstances(serviceType).Cast<object>();
	}
}