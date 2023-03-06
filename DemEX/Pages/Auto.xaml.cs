using System;
using System.Collections.Generic;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DemEX.Pages
{
	/// <summary>
	/// Логика взаимодействия для Auto.xaml
	/// </summary>
	public partial class Auto : Page
	{

		private int _countUnsuccessfull = 0;
		private Entities.Jury jury = null;
		private Entities.Members members = null;
		private Entities.Moderators moderators = null;
		private Entities.Organizer organizer = null;

		public Auto()
		{
			InitializeComponent();
		}

		private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new pgEvent());
        }

		private void btnEnter_Click(object sender, RoutedEventArgs e)
		{
			string login = tbLogin.Text;
			string password = pbPass.Password;
			var dbConn = Entities.DemEXEntities2.GetContext();


			if(login.Length > 0 && password.Length > 0)
			{
				jury = dbConn.Jury.Where(x => x.Mail == login && x.Password == password).FirstOrDefault();
				members = dbConn.Members.Where(y => y.Mail == login && y.Password == password).FirstOrDefault();
				moderators = dbConn.Moderators.Where(z => z.Mail == login && z.Password == password).FirstOrDefault();
				organizer = dbConn.Organizer.Where(q => q.Mail == login && q.Password == password).FirstOrDefault();
				if(_countUnsuccessfull < 1)
				{
					if(jury != null)
					{
						
						ClearForm();
						NavigationService.Navigate(new pgJury());
					}
					else if(members != null)
					{
						ClearForm();
						NavigationService.Navigate(new Client());
					}
					else if(moderators != null)
					{
						ClearForm();
						NavigationService.Navigate(new pgModerator());
					}
					else if(organizer !=  null)
					{
						ClearForm();
						NavigationService.Navigate(new pgOrganizer());
					}
					else
					{
						_countUnsuccessfull++;
						MessageBox.Show("Неверно введены логин или пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
						if(_countUnsuccessfull == 1)
						{
							tblCaptcha.Visibility = Visibility.Visible;
							tbCaptcha.Visibility = Visibility.Visible;
							tblCaptcha.Text = Model.Captcha.GeneratedCaptcha();
							tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
						}
					}
				}
				else
				{
					if(jury != null && jury.Mail == login && jury.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						ClearForm();
						NavigationService.Navigate(new pgJury());
					}
					else if (members != null && members.Mail == login && members.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						ClearForm();
						NavigationService.Navigate(new Client());
					}
					else if (moderators != null && moderators.Mail == login && moderators.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						ClearForm();
						NavigationService.Navigate(new pgModerator());
					}
					else if (organizer != null && organizer.Mail == login && organizer.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						ClearForm();
						NavigationService.Navigate(new pgOrganizer());
					}
					else
					{
						MessageBox.Show("Введите данные заного через 10 секунд!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
						tblCaptcha.Text = "";
						tbCaptcha.Clear();
						tblCaptcha.Visibility = Visibility.Hidden;
						tbCaptcha.Visibility = Visibility.Hidden;
						tbLogin.Clear();
						tbLogin.IsEnabled = false;
						pbPass.Clear();
						pbPass.IsEnabled = false;
						btnEnter.IsEnabled = false;
						txtBlockTimer.Visibility = Visibility.Visible;
						CountDown(10, TimeSpan.FromSeconds(1), cur => txtBlockTimer.Text = cur.ToString() + " сек. ");
					}
				}
			}
		}

		private void EnableForm()
		{
			tbLogin.IsEnabled = true;
			pbPass.IsEnabled = true;
			btnEnter.IsEnabled = true;
			txtBlockTimer.Visibility = Visibility.Hidden;
		}

		private void ClearForm()
		{
			pbPass.Clear();
			tbLogin.Clear();
			tbCaptcha.Clear();
			tblCaptcha.Visibility = Visibility.Hidden;
			tbCaptcha.Visibility = Visibility.Hidden;
			txtBlockTimer.Visibility = Visibility.Hidden;
		}

		void CountDown(int count, TimeSpan interval, Action<int> ts)
		{
			var dt = new DispatcherTimer();
			dt.Interval = interval;
			dt.Tick += (_, a) =>
			{
				if(count-- == 0)
				{
					EnableForm();
					dt.Stop();
				}
				else
				{
					ts(count);
				}
			};
			ts(count);
			dt.Start();
		}
	}
}
