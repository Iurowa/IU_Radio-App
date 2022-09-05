using System;
using System.Threading.Tasks;
using Iubh.RadioApp.Core.Services;
using Iubh.RadioApp.Core.ViewModels;
using Iubh.RadioApp.Data;
using Iubh.RadioApp.Data.Database;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin.File;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core
{
    public class App : MvxApplication
    {
        private static IDbLocal dbLocal;
        public static IDbLocal DbLocal => dbLocal ?? (dbLocal = Mvx.IoCProvider.Resolve<IDbLocal>());

        private static IDb db;
        public static IDb Db => db ?? (db = Mvx.IoCProvider.Resolve<IDb>());

        private static IMvxFileStore fileStore;
        public static IMvxFileStore FileStore => fileStore ?? (fileStore = Mvx.IoCProvider.Resolve<IMvxFileStore>());


        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();


            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDbLocal, DbLocal>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDb, Db>();

            if (App.FileStore.Exists(Config.Static.DatabaseFileName) == false)
            {
                App.FileStore.WriteFile(Config.Static.DatabaseFileName, string.Empty);

                try
                {
                    App.DbLocal.Init();
                }
                catch
                {
                    App.FileStore.DeleteFile(Config.Static.DatabaseFileName);
                    throw;
                }
            }
            else
            {
                App.DbLocal.Update();
            }

            if (App.DbLocal.GetConfigValue(Config.Static.OnboardingId) == "true")
            {
                if (App.DbLocal.ExistConfigValue(Config.Static.IsLoginId) == true)
                {
                    RegisterAppStart<TabBarModeratorViewModel>();
                }
                else
                {
                    RegisterAppStart<TabBarViewModel>();
                }
            }
            else
            {
                App.DbLocal.AddConfigValue(Config.Static.OnboardingId, "true");
                RegisterAppStart<OnboardingViewModel>();
            }
        }

        public override Task Startup()
        {
            return Task.CompletedTask;
        }
    }
}
