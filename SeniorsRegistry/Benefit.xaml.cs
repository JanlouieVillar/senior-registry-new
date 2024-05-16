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
using System.Windows.Shapes;

namespace SeniorsRegistry
{
  
    public partial class Benefit : Window
    {
        public List<Senior> SeniorDatabase { get; private set; }
        public Benefit()
        {
            InitializeComponent();
            showData();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Hide();
            main.Show();
        }

        private void showData()
        {
            using (DataContext context = new DataContext())
            {
                SeniorDatabase = context.Seniors.ToList();                
                seniorsList.ItemsSource = SeniorDatabase;

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            spBenefit.Visibility = Visibility.Visible;
            spBtnClaim.Visibility = Visibility.Hidden;
            spBtnUpdate.Visibility = Visibility.Hidden;
            spSearchBox.Visibility = Visibility.Hidden;
        }

        private void btnCancelClaim_Click(object sender, RoutedEventArgs e)
        {
            spBenefit.Visibility=Visibility.Hidden;
            spBtnClaim.Visibility = Visibility.Visible;
            spBtnUpdate.Visibility = Visibility.Visible;
            spSearchBox.Visibility = Visibility.Visible;
        }
    }
}
