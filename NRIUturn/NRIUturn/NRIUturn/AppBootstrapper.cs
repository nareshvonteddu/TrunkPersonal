using Caliburn.Micro;
using Autofac;
using NRIUturn.DataAccess;
using NRIUturn.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using NRIUturn.DataModels;
using System.Collections.ObjectModel;

namespace NRIUturn
{
    public class AppBootstrapper : Bootstrapper<IShell>
    {
        private IContainer _container;
        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            IEventAggregator eventAggregator = new EventAggregator();
            builder.RegisterInstance(eventAggregator).As<IEventAggregator>().SingleInstance();
            DataLayer dataLayer = new DataLayer(eventAggregator);
            builder.RegisterInstance(dataLayer).As<IDataLayer>();
            User _user = new User();
            USCities UScities = new USCities();
            USStates USStates = new USStates();
            IndianStates IndianStates = new IndianStates();
            IndianCities IndianCities = new IndianCities();
            builder.RegisterInstance(IndianStates).As<IndianStates>().SingleInstance();
            builder.RegisterInstance(USStates).As<USStates>().SingleInstance();
            builder.RegisterInstance(UScities).As<USCities>().SingleInstance();
            builder.RegisterInstance(IndianCities).As<IndianCities>().SingleInstance();
            builder.RegisterInstance(_user).As<IUser>().SingleInstance();
            builder.RegisterType<ShellViewModel>().As<IShell>();
            builder.RegisterType<LoginPageViewModel>().As<ILoginPageViewModel>();
            builder.RegisterType<HomeViewModel>().Keyed<IScreen>("Home");
            builder.RegisterType<ImigrationViewModel>().Keyed<IScreen>("Imigration");
            builder.RegisterType<IndiaEducationViewModel>().Keyed<IScreen>("IndiaEducation");
            builder.RegisterType<IndiaHouseViewModel>().Keyed<IScreen>("IndiaHouse");
            builder.RegisterType<IndiaJobViewModel>().Keyed<IScreen>("IndiaJob");
            builder.RegisterType<USEducationViewModel>().Keyed<IScreen>("USEducation");
            builder.RegisterType<USHouseViewModel>().Keyed<IScreen>("USHouse");
            builder.RegisterType<USJobViewModel>().Keyed<IScreen>("USJob");
            builder.RegisterType<WaitControlViewModel>().Keyed<IScreen>("WaitControlViewModel");

            _container = builder.Build();
        }

        protected override object GetInstance(System.Type service, string key)
        {
            object instance;
            if (string.IsNullOrEmpty(key))
            {
                if (_container.TryResolve(service, out instance))
                    return instance;
            }
            else
            {
                if (_container.TryResolveNamed(key, service, out instance))
                    return instance;
            }
            return instance;
        }

        protected override IEnumerable<object> GetAllInstances(System.Type service)
        {
            IEnumerable<object> result = _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
            return result;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }
    }
}
