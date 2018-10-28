//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using Pagita.Model;
//using Xamarin.Forms;

//namespace Pagita.CustomControl
//{
//	public partial class CreditCardEntry : ContentView
//	{
//		public static readonly BindableProperty CardProperty = BindableProperty.Create(nameof(Card), typeof(CreditCard), typeof(CreditCardEntry), new CreditCard(), BindingMode.OneWayToSource);

//		public CreditCard Card
//		{
//			get { return (CreditCard)GetValue(CardProperty); }
//			set { SetValue(CardProperty, value); }
//		}

//		public CreditCardEntry()
//		{
//			InitializeComponent();

//			MonthsPicker.ItemsSource = DateTimeFormatInfo.CurrentInfo.MonthNames.Where((arg) => arg.Length >0).ToList();
//			YearsPicker.ItemsSource  = Enumerable.Range(DateTime.Today.Year, 15).ToList();

//			//var year = DateTime.ParseExact(YearsPicker.ItemsSource[YearsPicker.SelectedIndex].ToString(), "yyyy", CultureInfo.CurrentCulture);
//			//var month = DateTime.ParseExact((string)MonthsPicker.ItemsSource[MonthsPicker.SelectedIndex], "MMMM", CultureInfo.CurrentCulture).Month;

//			//Card.ExpirationDate = new DateTime(year.Year,month,1);

//			MonthsPicker.SelectedIndexChanged += (sender, e) => 
//			{
//				Card.ExpirationDate = new DateTime(Card.ExpirationDate.Year, DateTime.ParseExact((string)MonthsPicker.ItemsSource[MonthsPicker.SelectedIndex], "MMMM", CultureInfo.CurrentCulture).Month,1);
// 			};

//			YearsPicker.SelectedIndexChanged += (sender, e) =>
//			{
//				Card.ExpirationDate = new DateTime(DateTime.ParseExact(YearsPicker.ItemsSource[YearsPicker.SelectedIndex].ToString(),  "yyyy", CultureInfo.CurrentCulture).Year, Card.ExpirationDate.Month, 1);
//			};

//			MonthsPicker.SelectedIndex = 0;
//			YearsPicker.SelectedIndex = 0;

//			CardNumberEntry.TextChanged += CardNumberEntryTextChanged;
//		}

//		void CardNumberEntryTextChanged(object sender, TextChangedEventArgs e)
//		{
//			if (e.OldTextValue == null || e.OldTextValue.Length < e.NewTextValue.Length)
//			{
//				CardNumberEntry.TextChanged -= CardNumberEntryTextChanged;

//				if (e.NewTextValue != string.Empty && char.IsDigit(e.NewTextValue.ToCharArray()[e.NewTextValue.Length - 1]))
//				{
//					if (CardNumberEntry.Text.ToCharArray().Length == 4)
//					{
//						CardNumberEntry.Text = e.NewTextValue.ToUpper() + " - ";
//						SetCardIcon();
//					}
//					else
//					{
//						var groups = CardNumberEntry.Text.Split('-');

//						if (groups.Length >= 2 && groups.Length < 4)
//							if (groups[groups.Length - 1].ToCharArray().Length == 5)
//								CardNumberEntry.Text = e.NewTextValue.ToUpper() + " - ";
//					}
//				}

//				else 
//				{
//					if (!(e.NewTextValue.ToCharArray()[e.NewTextValue.Length - 2] == '-'))
//					{
//						if (e.OldTextValue != null)
//							CardNumberEntry.Text = e.OldTextValue;
//						else
//							CardNumberEntry.Text = string.Empty;
//					}
//				}

//				CardNumberEntry.TextChanged += CardNumberEntryTextChanged;
//			}
//		}

//		private void SetCardIcon() 
//		{
//			CircuitIcon.Source = ImageSource.FromResource("Pagita.Image.CreditCard." + Card.Circuit + ".png");
//		}
//	}
//}
