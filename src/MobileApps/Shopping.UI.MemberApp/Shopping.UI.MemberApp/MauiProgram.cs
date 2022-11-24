using Shopping.UI.MemberApp.Services;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.Services.ProductServices;
using Shopping.UI.MemberApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Shopping.UI.MemberApp.Services.OrderServices;

namespace Shopping.UI.MemberApp;

public static class MauiProgram
{
    public static IServiceProvider Services;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        var services = builder.Services;

        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<HttpClientService>();
        


        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<MyIndexViewModel>();
        

        services.AddSingleton<AppShell>();
        services.AddSingleton<HomeView>();
        services.AddSingleton<LoginView>();
        services.AddSingleton<MyIndexView>();
        

        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        Routing.RegisterRoute(nameof(SubmmitOrderView), typeof(SubmmitOrderView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));

        var app = builder.Build();
        Services = app.Services;
        return app;

    }
}
