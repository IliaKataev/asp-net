using Ninject.Modules;
using IoC_DI.SongDirectory.models;
using System.Runtime.Serialization;
using IoC_DI.SongDirectory.output;

namespace IoC_DI.SongDirectory
{
    public class SongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISong>().To<Song>();
            Bind<output.IFormatter>().To<MorseFormatter>();
            Bind<IOutput>().To<FileOutput>();
        }
    }
}
