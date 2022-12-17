using Shopping.UI.MemberApp.Commons;
using Shopping.UI.MemberApp.Configs;
using Shopping.UI.MemberApp.Services.AccountServices;
using Shopping.UI.MemberApp.ViewModels;
using ZXing.Net.Maui;
using ZXing.QrCode.Internal;

namespace Shopping.UI.MemberApp;

public partial class CameraBarcodeReaderView : ContentPage
{
    public CameraBarcodeReaderView()
	{
        InitializeComponent();
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = true
        };
        
    }
    protected async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        //多个结果取第一个
        var result= e.Results.FirstOrDefault();
        if (result != null)
        {
            Console.WriteLine($"Barcodes: {result.Format} -> {result.Value}");
            await GotoResult(result.Value);
        }
           
    }
    protected void DengClicked(object sender, EventArgs args)
    {
        cameraBarcodeReaderView.IsTorchOn = !cameraBarcodeReaderView.IsTorchOn;
    }
    protected void JingTouClicked(object sender, EventArgs args)
    {
        cameraBarcodeReaderView.CameraLocation = cameraBarcodeReaderView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }
    protected async void BarcodesSuccessClicked(object sender, EventArgs args)
    {
        await GotoResult(Appsettings.IdentityQRCodeLogin+ "?qrcode=50f8f0be-05ac-41c9-b0b2-a86e7e844010");
    }
    
    public async Task GotoResult(string value)
    {
        if (value.StartsWith(Appsettings.IdentityQRCodeLogin))
        {
            var unescapedUrl = System.Net.WebUtility.UrlEncode(value);
            await Shell.Current.GoToAsync(nameof(LoginView) + "?action=qrcodelogin&qrcode=" + unescapedUrl);

        }
        else 
        { 
        
        }
    }
}