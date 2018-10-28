//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
//using System.Globalization;
//using Pagita.Model;
//using Xamarin.Forms;
//using Xamarin.Forms.Maps;

//namespace Pagita.CustomControl
//{
//	public partial class MachineMapView : ContentView
//	{
//		double tabSize;
//		BoxView tab;

//		public static readonly BindableProperty MachinesSourceProperty = BindableProperty.Create(nameof(MachinesSource), typeof(ObservableCollection<Machine>), typeof(MachineMapView), new ObservableCollection<Machine>(), propertyChanged:HandleBindingPropertyChangedDelegate);
//		public static readonly BindableProperty CenterOnFirstMachineProperty = BindableProperty.Create(nameof(CenterOnFirstMachine), typeof(bool), typeof(MachineMapView), false);
//		public static readonly BindableProperty VisibleRegionChangedCommandProperty = BindableProperty.Create(nameof(VisibleRegionChangedCommand), typeof(Command), typeof(MachineMapView), null);

//		public Command VisibleRegionChangedCommand
//		{
//			get { return (Command)GetValue(VisibleRegionChangedCommandProperty); }
//			set { SetValue(VisibleRegionChangedCommandProperty, value); }
//		}

//		public ObservableCollection<Machine> MachinesSource
//		{
//			get { return (ObservableCollection<Machine>)GetValue(MachinesSourceProperty); }
//			set { SetValue(MachinesSourceProperty, value); }
//		}

//		public bool CenterOnFirstMachine
//		{
//			get { return (bool)GetValue(CenterOnFirstMachineProperty); }
//			set { SetValue(CenterOnFirstMachineProperty, value); }
//		}

//		private ObservableCollection<Pin> pinsList;
//		public ObservableCollection<Pin> PinsList
//		{
//			get { return pinsList; }
//			set
//			{
//				if (pinsList != value)
//				{
//					pinsList = value;
//					OnPropertyChanged();
//				}
//			}
//		}

//		private static event EventHandler OnMachineSourceChangedEvent;

//		public MachineMapView()
//		{
//			InitializeComponent();

//			map.BindingContext = this;

//			PinsList = new ObservableCollection<Pin>();

//			map.VisibleRegionChanged += (sender, e) => 
//			{
//				VisibleRegionChangedCommand?.Execute(map.VisibleRegion);
//			};

//			OnMachineSourceChangedEvent+= MachineMapView_OnMachineSourceChangedEvent;

//			PropertyChanged += (sender, e) => 
//			{
//				if (e.PropertyName == nameof(CenterOnFirstMachine) && PinsList != null && PinsList.Count > 0 && PinsList[0] != null)
//					MoveToMachine(PinsList[0].Location);
//			};
//		}

//		protected override void OnBindingContextChanged()
//		{
//			base.OnBindingContextChanged();
//		}

//		static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
//		{
//			if (oldValue != null)
//			{
//				var coll = (INotifyCollectionChanged)oldValue;
//				// Unsubscribe from CollectionChanged on the old collection
//				coll.CollectionChanged -= Coll_CollectionChanged;
//			}
//			if (newValue != null)
//			{
//				var colln = (INotifyCollectionChanged)newValue;
//				colln.CollectionChanged += Coll_CollectionChanged;
//			}
//		}

//		static void Coll_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
//		{
//			OnMachineSourceChangedEvent?.Invoke(sender, e);
//		}

//		void MachineMapView_OnMachineSourceChangedEvent(object sender, EventArgs e)
//		{
//			var eventArg = (NotifyCollectionChangedEventArgs)e;

//			if (eventArg.Action == NotifyCollectionChangedAction.Add) 
//			{
//				foreach (Machine machine in eventArg.NewItems)
//				{
//					var pin = new Pin(typeof(Machine))
//					{
//						Address = machine.MachineAddress,
//						Label = machine.MachineDescription,
//						Location = new Position(double.Parse(machine.MachineLatitudine, CultureInfo.InvariantCulture), double.Parse(machine.MachineLongitudine, CultureInfo.InvariantCulture)),
//						ShowCallout = true,
//						ClusterShowCallout = false,
//						Marker = "Pagita.Image.MapPin.OrangePin.png",
//						ClusterMarker = "Pagita.Image.MapPin.GreenPin.png"
//					};

//					pin.ClusterClicked += Pin_ClusterClicked;
//					PinsList.Add(pin);
//				}
//			}
//			else if (eventArg.Action == NotifyCollectionChangedAction.Reset)
//				PinsList.Clear();
//		}

//		protected override void LayoutChildren(double x, double y, double width, double height)
//		{
//			base.LayoutChildren(x, y, width, height);

//			var offset = ((mainGrid.Height / 100) * panelRow.Height.Value);

//			panel.TranslateTo(0, offset, 200, Easing.CubicIn);

//			map.Clicked += (sender, e) =>
//			{
//				panel.TranslateTo(0, offset, 200, Easing.CubicIn);
//				cv.Position = 0;
//			};

//			cv.PositionSelected += async (sender, e) => 
//			{
//				await tab.TranslateTo(tabSize * double.Parse(e.SelectedPosition.ToString()), 0 , 200, Easing.CubicOut);
//			};
//		}

//		async void Pin_ClusterClicked(object sender, List<Pin> e)
//		{
//			var source = e;
//			tabContainer.Children.Clear();
//			cv.Position = 0;

//			cv.ItemsSource = source;
//			var scaleFactor = (100f / source.Count) / 100f;
//			tabSize = tabContainer.Width * scaleFactor;

//			tab = new BoxView() { BackgroundColor = (Color)Application.Current.Resources["PagitaOrange"] };
//			Grid.SetColumn(tab, 0);
//			tabContainer.Children.Add(tab);

//			tabContainer.ColumnDefinitions[0].Width = new GridLength(tabSize, GridUnitType.Absolute);

//			await panel.TranslateTo(0, 0, 500, Easing.CubicOut);
//		}

//		public void MoveToMachine(Position machinePosition) 
//		{
//			var machineRegion = MapSpan.FromCenterAndRadius(machinePosition, Distance.FromMeters(500));
//			map.VisibleRegion = machineRegion;
//		}
//	}
//}
