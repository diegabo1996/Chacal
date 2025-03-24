using Chacal.Chat.Interfaces;
using Chacal.Chat.Services;
using Microsoft.Extensions.Logging;

namespace Chacal.MAUIClient
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
            builder.Services.AddSingleton<ILlmClient, AzureLlmClient>(serviceProvider =>
            {
                // Configure the Azure OpenAI client with your endpoint and API key
                var endpointUrl = "https://chacalhub1930363902.services.ai.azure.com/models";
                var apiKey = "AkrJpGA3dRGXX1ZYkfECuwrwEmsRUhr7YZu12ScoI4c5FKWycHo3JQQJ99BCACHYHv6XJ3w3AAAAACOG0QWU";
                var model = "DeepSeek-V3"; // Specify the model you want to use
                return new AzureLlmClient(endpointUrl, apiKey, model);
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
