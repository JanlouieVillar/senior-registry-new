using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SeniorsRegistry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer clockTimer = new DispatcherTimer();
        public MainWindow()
        {  
            InitializeComponent();
            
            // format date and time
            txtDateDisplay.Text =DateTime.Now.ToString("dddd MMMM dd, yyyy");            
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimerEngine;
            clockTimer.Start();

            // check for admin rights
            if (AdminLogin.Text=="No")
            {
                spBenefit.Visibility = Visibility.Hidden;
                spUpdateStat.Visibility = Visibility.Hidden;
                spUserManagement.Visibility = Visibility.Hidden;
            }
            else
            {
                spBenefit.Visibility = Visibility.Visible;
                spUpdateStat.Visibility = Visibility.Visible;
                spUserManagement.Visibility = Visibility.Visible;
            }

        }

        private void ClockTimerEngine(object? sender, EventArgs e)
        {
            txtTimeDisplay.Text = DateTime.Now.ToString("hh:mm:ss");            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
                

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            this.Cursor = Cursors.Hand;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            
            RegisterWindow register = new RegisterWindow();
            this.Hide();
            register.Show();
            
        }

        private void btnUserMgt_Click(object sender, RoutedEventArgs e)
        {
            if (AdminLogin.Text=="Yes")
            {
                RegisterUser userWindow = new RegisterUser();
                this.Hide();
                userWindow.Show();
            }
            else
            {
                txtUserManagement.Text = "Action not Authorized!";
            }

        }

        private void btnBenefit_Click(object sender, RoutedEventArgs e)
        {
            if (AdminLogin.Text == "Yes")
            {
                Benefit benifit = new Benefit();
                benifit.Show();
                this.Hide();
            }
            else
            {
                txtBenefitClaim.Text = "Action not Authorized!";
            }

        }
    }
}