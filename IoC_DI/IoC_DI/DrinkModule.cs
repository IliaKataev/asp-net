using IoC_DI.models;
using Ninject.Modules;

namespace IoC_DI
{
    public class DrinkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDrink>().To<Tea>();
            Bind<IOutputWriter>().To<FileOutputWriter>();
        }
    }
}
