using DemEX.Entities;
using DemEX.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemEX.Pages
{
	/// <summary>
	/// Логика взаимодействия для pgEvent.xaml
	/// </summary>
	public partial class pgEvent : Page
	{
		public pgEvent()
		{
			InitializeComponent();

			var events = DemEXEntities2.GetContext().Events.ToList();
			lvEvents.ItemsSource = events;

			DataContext = this;
			UpdateData();
		}

		public string[] SortingList { get; set; } =
		{
			"Без сортировки",
			"IT",
			"Биг Дата",
			"Дизайн",
			"Аналитика",
			"Информационная безопасность"
		};

		private void UpdateData()
		{
			var result = DemEXEntities2.GetContext().Events.ToList();

			if(cmbFilter.SelectedIndex == 0)
			{
				lvEvents.ItemsSource = result;
			}
			if(cmbFilter.SelectedIndex == 1)
			{
				result = result.OrderBy(p => p.Events1.IndexOf(cmbFilter.Text) != -1).ToList();
			}
			if(cmbFilter.SelectedIndex == 2)
			{
				result = result.OrderBy(p => p.Events1.IndexOf(cmbFilter.Text) != -1).ToList();
			}
			if (cmbFilter.SelectedIndex == 3)
			{
				result = result.OrderBy(p => p.Events1.IndexOf(cmbFilter.Text) != -1).ToList();
			}
			if (cmbFilter.SelectedIndex == 4)
			{
				result = result.OrderBy(p => p.Events1.IndexOf(cmbFilter.Text) != -1).ToList();
			}
			if (cmbFilter.SelectedIndex == 5)
			{
				result = result.OrderBy(p => p.Events1.IndexOf(cmbFilter.Text) != -1).ToList();
			}

			if (dpFilter.Text != "")
			{
				result = result.Where(p => p.DATE == dpFilter.Text).ToList();
			}
			lvEvents.ItemsSource = result;
		}

		private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateData();
		}

		private void dpFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateData();
		}

		private void btnMore_Click(object sender, RoutedEventArgs e)
		{
			//Variables.imgEvent = 
			
			NavigationService.Navigate(new pgMoreEvent());
		}

		private void lvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var siEv = lvEvents.SelectedItems;
			var pgME = new pgMoreEvent();

			var index = siEv.IndexOf(sender);
			pgME.DataContext = siEv;
			NavigationService.Navigate(pgME);
		}
	}
}
