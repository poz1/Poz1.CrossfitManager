using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.CustomControl
{
	public class GridView : Grid
	{
		//public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<GridView, object>(p => p.CommandParameter, null);
		//public static readonly BindableProperty CommandProperty = BindableProperty.Create<GridView, Command>(p => p.Command, null);
		public static readonly BindableProperty MaxColumnsProperty = BindableProperty.Create(nameof(MaxColumns), typeof(int), typeof(GridView), 3);
		public static readonly BindableProperty TileHeightProperty = BindableProperty.Create(nameof(TileHeight), typeof(float), typeof(GridView), 30f);
		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(GridView), new DataTemplate(typeof(ViewCell)));
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(GridView), null);

		public IList ItemsSource
		{
			get { return (IList)GetValue(GridView.ItemsSourceProperty); }
			set { SetValue(GridView.ItemsSourceProperty, value); }
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);

            if (RowDefinitions.Count == 0 && ColumnDefinitions.Count == 0 && width != -1 && height != -1)
			{
                for (var i = 0; i < MaxColumns; i++)
                {
                    try
                    {
                        if(Math.Abs(TileHeight) < 0.01)
                            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(width/ MaxColumns) });
                        else
                            RowDefinitions.Add(new RowDefinition() { Height = TileHeight });

                        ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
                }
			}
		}

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		public int MaxColumns
		{
			get { return (int)GetValue(MaxColumnsProperty); }
			set { SetValue(MaxColumnsProperty, value); }
		}

		public float TileHeight
		{
			get { return (float)GetValue(TileHeightProperty); }
			set { SetValue(TileHeightProperty, value); }
		}

		//public object CommandParameter
		//{
		//	get { return GetValue(CommandParameterProperty); }
		//	set { SetValue(CommandParameterProperty, value); }
		//}

		//public Command Command
		//{
		//	get { return (Command)GetValue(CommandProperty); }
		//	set { SetValue(CommandProperty, value); }
		//}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(ItemsSource))
			{
				((INotifyCollectionChanged)ItemsSource).CollectionChanged += async (sender, e) =>
				{
					if(e.Action == NotifyCollectionChangedAction.Add)
						await AddItem(ItemsSource.Count - 1);
				};
			}
		}

		public async Task AddItem(int index)
		{
			if (ItemsSource != null)
			{
				var tile = await BuildTile(ItemsSource[index]);

				//if (RowDefinitions.Count < (index / MaxColumns))
				//	RowDefinitions.Add(new RowDefinition() { Height = new GridLength(TileHeight) });
					                   
				Children.Add(tile, index%MaxColumns, index/MaxColumns);
			}
		}

		private async Task<Xamarin.Forms.View> BuildTile(object item)
		{
			return await Task.Run(() =>
			{
				var buildTile = (ViewCell)ItemTemplate.CreateContent();
				buildTile.View.BindingContext = item;

				var tapGestureRecognizer = new TapGestureRecognizer
				{
					//Command = Command,
					//CommandParameter = item1
				};

				buildTile.View.GestureRecognizers.Add(tapGestureRecognizer);
				return buildTile.View;
			});
		}
	}
}

