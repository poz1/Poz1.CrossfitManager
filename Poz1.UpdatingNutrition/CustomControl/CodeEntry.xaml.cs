//using System.Collections.Generic;
//using System.Linq;
//using Pagita.Model;
//using Xamarin.Forms;

//namespace Pagita.CustomControl
//{
//	public partial class CodeEntry : ContentView
//	{
//		private readonly List<ExtendedEntry> codeEntries;

//		public static readonly BindableProperty CodeProperty = BindableProperty.Create(nameof(Code), typeof(MachineCode), typeof(CodeEntry), new MachineCode(), BindingMode.OneWayToSource);

//		public MachineCode Code
//		{
//			get { return (MachineCode)GetValue(CodeProperty); }
//			set { SetValue(CodeProperty, value); }
//		}

//		public CodeEntry()
//		{
//			InitializeComponent();

//			codeEntries = new List<ExtendedEntry>();
//			var stacks = Container.Children.ToList();

//			foreach (StackLayout item in stacks)
//			{
//				var entry = item.Children[0] as ExtendedEntry;
//				entry.TextChanged += CodeEntryTextChanged;
//				codeEntries.Add(entry);
//			}
//		}

//		void CodeEntryTextChanged(object sender, TextChangedEventArgs e)
//		{
//			if (e.OldTextValue != e.NewTextValue)
//			{
//				var selectedEntry = sender as ExtendedEntry;
//				selectedEntry.TextChanged -= CodeEntryTextChanged;

//				if (e.NewTextValue != string.Empty && e.NewTextValue!= null)
//				{
//					if (selectedEntry == codeEntries[0] || selectedEntry == codeEntries[1])
//					{
//						if (char.IsLetter(e.NewTextValue[0]))
//						{
//							selectedEntry.Text = e.NewTextValue.ToUpper();
//							codeEntries[codeEntries.IndexOf(selectedEntry) + 1].Focus();
//						}
//						else
//						{
//							selectedEntry.Text = string.Empty;
//						}
//					}
//					else
//					{
//						if (char.IsNumber(e.NewTextValue[0]))
//						{
//							if (codeEntries.IndexOf(selectedEntry) < (codeEntries.Count - 1))
//								codeEntries[codeEntries.IndexOf(selectedEntry) + 1].Focus();
//						}
//						else
//							selectedEntry.Text = string.Empty;
//					}
//				}

//				selectedEntry.TextChanged += CodeEntryTextChanged;
//			}
//		}
//	}
//}
