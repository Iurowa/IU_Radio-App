using MvvmCross.ViewModels;
using Android.Support.V4.View;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters;
using Android.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using Iubh.RadioApp.Droid.Bindings;
using MvvmCross;
using MvvmCross.Plugin.File;
using Android.OS;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Content.Res;
using Android.Content;
using Android.Support.V4.Content.Res;
using System.Runtime.Remoting.Contexts;

using Android.Support.V4.Graphics;
using System.IO;
using System.Reflection;
using System.Linq;
using MvvmCross.IoC;
using Iubh.RadioApp.Core.Services;
using Iubh.RadioApp.Droid.Services;
using Iubh.RadioApp.Common.Services;

namespace Iubh.RadioApp.Droid
{
    public class Setup : MvxAppCompatSetup//<App>
    {
        protected override IMvxApplication CreateApp()
        {
            var messageService = Mvx.IoCProvider.ConstructAndRegisterSingleton<IMessageService, MessageService>();
            messageService.Start();

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDeviceService, DeviceService>();
            return new Iubh.RadioApp.Core.App();
        }

        protected override void FillBindingNames(IMvxBindingNameRegistry registry)
        {
            base.FillBindingNames(registry);
            registry.AddOrOverwrite(typeof(ImageButton), nameof(ImageButton.Click));
            registry.AddOrOverwrite(typeof(DatePicker), nameof(DatePicker.DateTime));
            registry.AddOrOverwrite(typeof(ViewPager), nameof(ViewPager.CurrentItem));
        }


        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<ImageView>(BindingNames.Image, imageView => new ImageViewImageTargetBinding(imageView));
        }


        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new AppViewPresenter(this.AndroidViewAssemblies);
        }
    }
}