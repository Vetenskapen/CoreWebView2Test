using Microsoft.Web.WebView2.Core;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            //Lyssnar på att göra fönstret större / mindre
            this.Resize += new EventHandler(this.Form_Resize);
            //starting page
            this.webView.Source = new Uri("https://google.se");
            this.webView.Visible = true;
            UthoppAsync();
        }


        //Koden under låter webbläsaren följa storleken på fönstret
        private void Form_Resize(object sender, EventArgs e)
        {
            webView.Size = this.ClientSize - new Size(webView.Location);

        }

        private void webView_Click(object sender, EventArgs e)
        {
            
        }

        //koden under är för navigation 
        private void button1_Click(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        private async Task UthoppAsync()
        {
            
            string url = "https://www.w3schools.com/action_page.php";
            var postData = "fname=test&lname=test";
            //måste göra om postdata till system io stream för att köra metoden webresource request
            System.IO.MemoryStream postData2 = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(postData));
            
            //Väntar in navigation
            await NavigateTo(url, webView);
            // efter faställt att navigation finns, skickar parametrar
            var request = webView.CoreWebView2.Environment.CreateWebResourceRequest(url,
              "POST", postData2, "Content-Type: application/x-www-form-urlencoded");
            webView.CoreWebView2.NavigateWithWebResourceRequest(request);
        }
        //Metod för att navigera till url. Async används för att faställa att vi har laddat in allt.
        public async Task NavigateTo(string url, Microsoft.Web.WebView2.WinForms.WebView2 webb) { await webb.EnsureCoreWebView2Async(); webb.Source = new Uri(url); }
    }
}
