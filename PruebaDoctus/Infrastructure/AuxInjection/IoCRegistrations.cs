using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaDoctus.AuxInjection
{
    public static class IoCRegistrations
    {
        public static void RegisterDependencies(Container container)
        {
            container.RegisterSingleton<IDataStore<Item>, MockDataStore>();
            container.RegisterSingleton<ICountryDataStore, CountryDataStoreMock>();
    }
    }
}
