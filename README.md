# 1PAY.network - Crypto Payment SDK

Website: [1pay.network](https://1pay.network)

Documents: [1pay.network/documents](https://1pay.network/documents)

Full example of 1PAY.network integration for WinForm, .NET app written in C#

> Focus on file /Demo1Pay/PaymentDialog.cs

```csharp
using System.Diagnostics;
using System.Text;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Demo1Pay;

public class PaymentDialog: Form
{

    private WebView2 _webView;
    public Action<PaymentResponse>? OnePaySuccess;
    public Action<PaymentResponse>? OnePayFail;
    
    
    public PaymentDialog()
    { 
        _webView = new WebView2
        {
            Size = new Size(480, 640),
        };
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Width = 480;
        this.Height = 640;
        this.Text = "Payment";

       
        _webView.Source = new Uri(BuildPaymentUrl(0.1f, "usdt", "demo note"));
        _webView.SourceChanged += WebViewOnSourceChanged;
        this.Resize += PromptOnResize;
        
        this.Controls.Add(_webView);
    }
    
    private void PromptOnResize(object? sender, EventArgs e)
    {
        this._webView.Size = this.ClientSize - new System.Drawing.Size(_webView.Location);
    }

    private void WebViewOnSourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
    {
        if (sender is WebView2 wv)
        {
            var wvUri = wv.Source;
                var url = wv.Source.ToString();
                if (url.Contains("1pay.network/success"))
                {
                    OnePaySuccess?.Invoke(new PaymentResponse(PaymentResponse.QueryToDict(wvUri.Query)));

                }
                else if (url.Contains("1pay.network/fail"))
                {
                    OnePayFail?.Invoke(new PaymentResponse(PaymentResponse.QueryToDict(wvUri.Query)));
                }
        }
    }

    private string BuildPaymentUrl(float amount, string token, string note)
    {
        return new StringBuilder()
            .Append("https://1pay.network/app")
            .Append("?recipient=0x8d70EC40AAd376aa6fD08e4CFD363EaC0AB2c174")
            .Append("&token=usdt,usdc,dai")
            .Append("&network=ethereum,arbitrum,optimism,bsc")
            .Append("&paymentAmount=").Append(amount)
            .Append("&paymentToken=").Append(token)
            .Append("&paymentNote=").Append(note)
            .ToString();
    }
}
```
