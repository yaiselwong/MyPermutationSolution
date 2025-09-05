using Microsoft.Extensions.Logging;
using MyPermutationSolution.Client.Shared.Interfaces;
using MyPermutationSolution.Client.Shared.Services;

namespace MyPermutationSolution.App
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://yaiselwong.bsite.net") });
            builder.Services.AddScoped<ICustomHttpClient, CustomHttpClient>();
            builder.Services.AddScoped<IPermutationService, PermutationService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
