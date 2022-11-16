using Joinrpg.Schedule.App.Services.Interfaces;
using Joinrpg.Schedule.Main.PresentationMode;
using Joinrpg.Schedule.Main.ViewModels;
using Joinrpg.Schedule.Main.Views;
using Joinrpg.Schedule.Services.Impl;
using Microsoft.Extensions.Logging;

namespace Joinrpg.Schedule.Main
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews()
                ;

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IScheduleService, ScheduleServiceImpl>();
            mauiAppBuilder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            mauiAppBuilder.Services.AddSingleton<CurrentScheduleHolder>();
            mauiAppBuilder.Services.AddSingleton<PresentationSelecter>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<ScheduleViewModel>();
            mauiAppBuilder.Services.AddTransient<ItemDetailViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<SchedulePage>();
            mauiAppBuilder.Services.AddTransient<ItemDetailPage>();

            return mauiAppBuilder;
        }
    }
}