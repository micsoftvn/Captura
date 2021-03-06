﻿using Captura.Models;
using Captura.ViewModels;
using Ninject.Modules;

namespace Captura
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            // Singleton View Models
            Bind<MainViewModel>().ToSelf().InSingletonScope();
            Bind<VideoViewModel>().ToSelf().InSingletonScope();

            Bind<CustomOverlaysViewModel>().ToSelf().InSingletonScope();

            // Settings
            Bind<Settings>().ToSelf().InSingletonScope();

            // Localization
            Bind<LanguageManager>().ToMethod(M => LanguageManager.Instance).InSingletonScope();

            // Hotkeys
            Bind<HotKeyManager>().ToSelf().InSingletonScope();

            // Image Writers
            Bind<IImageWriterItem>().To<DiskWriter>().InSingletonScope();
            Bind<IImageWriterItem>().To<ClipboardWriter>().InSingletonScope();
            Bind<IImageWriterItem>().To<ImgurWriter>().InSingletonScope();

            // Video Writer Providers
            Bind<IVideoWriterProvider>().To<FFMpegWriterProvider>().InSingletonScope();
            Bind<IVideoWriterProvider>().To<GifWriterProvider>().InSingletonScope();
            Bind<IVideoWriterProvider>().To<StreamingWriterProvider>().InSingletonScope();

            // Check if SharpAvi is available
            if (ServiceProvider.FileExists("SharpAvi.dll"))
            {
                Bind<IVideoWriterProvider>().To<SharpAviWriterProvider>().InSingletonScope();
            }

            // Video Source Providers
            Bind<IVideoSourceProvider>().To<ScreenSourceProvider>().InSingletonScope();
            Bind<IVideoSourceProvider>().To<RegionSourceProvider>().InSingletonScope();
            Bind<IVideoSourceProvider>().To<WindowSourceProvider>().InSingletonScope();
            Bind<IVideoSourceProvider>().To<DeskDuplSourceProvider>().InSingletonScope();
            Bind<IVideoSourceProvider>().To<NoVideoSourceProvider>().InSingletonScope();

            // Check if Bass is available
            if (BassAudioSource.Available)
            {
                Bind<AudioSource>().To<BassAudioSource>().InSingletonScope();
            }
            else Bind<AudioSource>().To<NoAudioSource>().InSingletonScope();
        }
    }
}