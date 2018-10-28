using System;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.CustomControl
{
    public class ExtendedEntry : Entry
    {
		public static readonly BindableProperty TextAlignProperty = BindableProperty.Create( nameof(TextAlign), typeof(TextAlignment), typeof(ExtendedEntry), TextAlignment.Start);
		public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ExtendedEntry), int.MaxValue);
		public static readonly BindableProperty HasBorderProperty = BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(ExtendedEntry), true);
		public static readonly BindableProperty PlaceholderTextColorProperty = BindableProperty.Create(nameof(PlaceholderTextColor), typeof(Color), typeof(ExtendedEntry), Color.Default);
		public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(nameof(TextChangedCommand), typeof(Command), typeof(ExtendedEntry), null);
		public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(ExtendedEntry), ReturnType.Next);
		public static readonly BindableProperty CompletedCommandProperty = BindableProperty.Create(nameof(CompletedCommand), typeof(Command), typeof(ExtendedEntry), null);

		public new event EventHandler<EventArgs> Completed;

		public Command CompletedCommand
		{
			get { return (Command)GetValue(CompletedCommandProperty); }
			set { SetValue(CompletedCommandProperty, value); }
		}
		public TextAlignment TextAlign
        {
            get { return (TextAlignment)GetValue(TextAlignProperty); }
            set { SetValue(TextAlignProperty, value); }
        }

        public int MaxLength
        {
            get { return (int)this.GetValue(MaxLengthProperty); }
            set { this.SetValue(MaxLengthProperty, value); }
        }

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

		public Command TextChangedCommand
		{
			get { return (Command)GetValue(TextChangedCommandProperty); }
			set { SetValue(TextChangedCommandProperty, value); }
		}

		public ReturnType ReturnType
		{
			get { return (ReturnType)GetValue(ReturnTypeProperty); }
			set { SetValue(ReturnTypeProperty, value); }
		}

		public void InvokeCompleted()
		{
			Completed?.Invoke(this, null);
			CompletedCommand?.Execute(null);
		}

		public ExtendedEntry() 
		{
			
			TextChanged += (sender, e) => 
			{
				TextChangedCommand?.Execute(e);
			};
		}
    }

	public enum ReturnType
	{
		Go,
		Next,
		Done,
		Send,
		Search
	}
}
