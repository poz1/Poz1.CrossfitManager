using Xamarin.Forms;

namespace Pagita.CustomControl
{
    class BindableWebView : WebView
    {
        public static readonly BindableProperty NavigatingCommandProperty = BindableProperty.Create("NavigatingCommand", typeof(Command), typeof(BindableWebView), null);
        public static readonly BindableProperty NavigatedCommandProperty = BindableProperty.Create("NavigatedCommand", typeof(Command), typeof(BindableWebView), null);

        public Command NavigatingCommand
        {
            get { return (Command)GetValue(NavigatingCommandProperty); }
            set { SetValue(NavigatingCommandProperty, value); }
        }
        public Command NavigatedCommand
        {
            get { return (Command)GetValue(NavigatedCommandProperty); }
            set { SetValue(NavigatedCommandProperty, value); }
        }

        public BindableWebView()
        {
            Navigated += BindableWebView_Navigated;
            Navigating += BindableWebView_Navigating;
        }

        private void BindableWebView_Navigating(System.Object sender, WebNavigatingEventArgs e)
        {
            NavigatingCommand?.Execute(null);
        }

        private void BindableWebView_Navigated(System.Object sender, WebNavigatedEventArgs e)
        {
            NavigatedCommand?.Execute(null);
        }

    }
}
