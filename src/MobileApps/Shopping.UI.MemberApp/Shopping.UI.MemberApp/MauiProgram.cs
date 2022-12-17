using Shopping.UI.MemberApp.Services;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.Services.ProductServices;
using Shopping.UI.MemberApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Shopping.UI.MemberApp.Services.OrderServices;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace Shopping.UI.MemberApp;

public static class MauiProgram
{
    public static IServiceProvider Services;
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureLifecycleEvents(events => {
#if ANDROID
                events.AddAndroid(android =>
                {
                   var result = android.OnApplicationCreate(AppActionEventArgs => {
                        
                    });
                });
#endif

            })
            ;


        var services = builder.Services;

        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<HttpClientService>();
        


        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddTransient<MyIndexViewModel>();
        services.AddSingleton<ProductCategoryViewModel>();
        services.AddSingleton<OrderListViewModel>();
        
        services.AddSingleton<AppShell>();
        services.AddSingleton<HomeView>();
        services.AddTransient<LoginView>();
        services.AddSingleton<MyIndexView>();
        services.AddSingleton<OrderListView>();
        services.AddSingleton<OrderDetailView>();
        services.AddSingleton<ProductCategoryView>();
        services.AddTransient<CameraBarcodeReaderView>();
        

        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        Routing.RegisterRoute(nameof(MyIndexView), typeof(MyIndexView));
        Routing.RegisterRoute(nameof(SubmmitOrderView), typeof(SubmmitOrderView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(ProductDetailView), typeof(ProductDetailView));
        Routing.RegisterRoute(nameof(ProductListView), typeof(ProductListView));
        Routing.RegisterRoute(nameof(OrderListView), typeof(OrderListView));
        Routing.RegisterRoute(nameof(OrderDetailView), typeof(OrderDetailView));
        Routing.RegisterRoute(nameof(CameraBarcodeReaderView), typeof(CameraBarcodeReaderView));

        var app = builder.Build();
        Services = app.Services;
       
        return app;

    }
}
