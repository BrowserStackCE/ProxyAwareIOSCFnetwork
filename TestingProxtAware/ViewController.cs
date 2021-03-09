using CoreGraphics;
using Foundation;
using System;
using System.Reflection.Emit;
using System.Threading;
using UIKit;

namespace TestingProxtAware
{
    public partial class ViewController : UIViewController
    {
        UITextField usernameField;
        UILabel label1 = new UILabel();
        String URL;

        public static string httpClientObject;
        public ViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            nfloat h = 31.0f;
            nfloat w = View.Bounds.Width;

            var textView = new UITextView(new CoreGraphics.CGRect(10, 82, w - 20, View.Frame.Height));

            var frame = new CGRect(10, 40, 300, 40);
            usernameField = new UITextField(frame);
            usernameField.Placeholder = "Enter URL";
            View.Add(usernameField);


            UIButton toggleBtn = new UIButton(new CGRect(50, 300, 250, 75));
            toggleBtn.BackgroundColor = UIColor.Gray;
            toggleBtn.Tag = 0;
            toggleBtn.SetTitle("Make Proxy Aware", UIControlState.Normal);
            toggleBtn.TouchUpInside += delegate
            {
                if (0 == toggleBtn.Tag)
                {

                    toggleBtn.Tag = 1;
                    textView = new UITextView(new CoreGraphics.CGRect(10, 100, w - 20, View.Frame.Height / 2));


                    textView.Text = "Proxy Off";
                    textView.BackgroundColor = UIColor.White;

                    this.View.Add(textView);
                    this.View.AddSubview(toggleBtn);



                }
                else if (1 == toggleBtn.Tag)
                {
                    textView = new UITextView(new CoreGraphics.CGRect(10, 100, w - 20, View.Frame.Height / 2));
                    Getproxy(usernameField.Text);

                    textView.Text = httpClientObject;
                    textView.BackgroundColor = UIColor.Blue;
                    this.View.Add(textView);
                    toggleBtn.Tag = 0;

                    this.View.AddSubview(toggleBtn);
                    URL = usernameField.Text;


                }
            };
            this.View.AddSubview(toggleBtn);



        }





        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

        }
        public static async void Getproxy(String URL)
        {

            ProxyInfoProvider proxyObj = new ProxyInfoProvider();

            httpClientObject = await new HttpService(proxyObj, URL).GetStringAsync(URL, CancellationToken.None);

            Console.WriteLine(httpClientObject);

        }



    }
}