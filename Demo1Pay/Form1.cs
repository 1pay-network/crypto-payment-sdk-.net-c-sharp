using System.Diagnostics;
using System.Text;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Demo1Pay;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void ButtonOnClick(object? sender, EventArgs e)
    {
        var paymentDialog = new PaymentDialog();
        paymentDialog.OnePaySuccess = response =>
        {
            MessageBox.Show("Success with hash " + response.hash, "Success");
        };
        paymentDialog.OnePayFail = response =>
        {
            MessageBox.Show("Failed pay " + response.amount + response.token.ToUpper(), "Failed");
        };
        paymentDialog.ShowDialog();
    }
}