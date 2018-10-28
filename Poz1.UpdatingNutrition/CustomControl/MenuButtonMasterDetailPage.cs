using Xamarin.Forms;

namespace Pagita.CustomControl
{
	public class MenuButtonMasterDetailPage : MasterDetailPage
	{
		public static readonly BindableProperty MenuButtonProperty = BindableProperty.Create("MenuButton", typeof(ToolbarItem), typeof(MenuButtonMasterDetailPage), null);
		public static readonly BindableProperty OnAppearingCommandProperty = BindableProperty.Create("OnAppearingCommand", typeof(Command), typeof(MenuButtonMasterDetailPage), null);
		public static readonly BindableProperty OnBackPressedCommandProperty = BindableProperty.Create(nameof(OnBackPressedCommand), typeof(Command), typeof(BindableContentPage), null);
		public static readonly BindableProperty OnIsPresentedChangedCommandProperty = BindableProperty.Create(nameof(OnIsPresentedChangedCommand), typeof(Command), typeof(MenuButtonMasterDetailPage), null);

		public ToolbarItem MenuButton
		{
			get { return (ToolbarItem)GetValue(MenuButtonProperty); }
			set { SetValue(MenuButtonProperty, value); }
		}

		public Command OnAppearingCommand
		{
			get { return (Command)GetValue(OnAppearingCommandProperty); }
			set { SetValue(OnAppearingCommandProperty, value); }
		}

		public Command OnIsPresentedChangedCommand
		{
			get { return (Command)GetValue(OnIsPresentedChangedCommandProperty); }
			set { SetValue(OnIsPresentedChangedCommandProperty, value); }
		}

		public Command OnBackPressedCommand
		{
			get { return (Command)GetValue(OnBackPressedCommandProperty); }
			set { SetValue(OnBackPressedCommandProperty, value); }
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			OnAppearingCommand?.Execute(null);
		}

		protected override bool OnBackButtonPressed()
		{
			if (OnBackPressedCommand != null)
			{
				OnBackPressedCommand.Execute(null);
				return true;
			}
			else
				return base.OnBackButtonPressed();
		}

		public MenuButtonMasterDetailPage()
		{
			this.PropertyChanged += (sender, e) => 
			{
				if (e.PropertyName == nameof(IsPresented))
					OnIsPresentedChangedCommand?.Execute(IsPresented);
			};
		}
	}
}
