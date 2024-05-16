using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
   
    public partial class RegisterWindow : Window
    {
        public List<Senior> SeniorDatabase { get; private set; }
        int sw;
        public RegisterWindow()
        {
            InitializeComponent();
            seniorsList.ColumnWidth = DataGridLength.Auto;
            showData();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // ---- hide control buttons -----
            spBtnAdd.Visibility = Visibility.Hidden;
            spBtnEdit.Visibility = Visibility.Hidden;
            spBtnDelete.Visibility = Visibility.Hidden;
            // ---- show save button ---------
            spBtnSave.Visibility = Visibility.Visible;
            // ---- enable typing -------------
            dataDisplayBorder.IsEnabled = true;
            sw = 0;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // ---- hide control buttons -----
            spBtnAdd.Visibility = Visibility.Hidden;
            spBtnEdit.Visibility = Visibility.Hidden;
            spBtnDelete.Visibility = Visibility.Hidden;
            // ---- show save button ---------
            spBtnSave.Visibility = Visibility.Visible;
            // ---- enable typing -------------
            dataDisplayBorder.IsEnabled = true;
            sw = 1; // edit
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Senior SelectedUser = seniorsList.SelectedItem as Senior;
            using (DataContext context = new DataContext())
            {
                if (SelectedUser!= null)
                {
                    Senior senior = context.Seniors.Single(x=>x.Id == SelectedUser.Id);
                    context.Remove(senior);
                    context.SaveChanges();
                }
            }
            showData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // ---- show control buttons -----
            spBtnAdd.Visibility = Visibility.Visible;
            spBtnEdit.Visibility = Visibility.Visible;
            spBtnDelete.Visibility = Visibility.Visible;
            // ---- hide save button ---------
            spBtnSave.Visibility = Visibility.Hidden;
            // ---- disable typing -------------
            dataDisplayBorder.IsEnabled = false;
            // and finally, save data
            saveData();
            
            showData();
            // show saved data
        }

        private void saveData()
        {
            if (sw==1) 
            {
                using (DataContext context = new DataContext())
                {
                    Senior SelectedUser = seniorsList.SelectedItem as Senior;
                    Senior senior = context.Seniors.Find(SelectedUser.Id);
                    // get data from form
                    var lastname = txtLastName.Text;
                    var firstname = txtFirstName.Text;
                    var middlename = txtMiddleName.Text;
                    var birthday = txtBirthday.Text;
                    var contact = txtContact.Text;
                    var guardian = txtGuardian.Text;
                    var zone = cmbZone.Text;
                    var cred = "";

                    if (chkPSA.IsChecked == true)
                    {
                        cred = cred + "PSA";
                    }
                    if (chkSID.IsChecked == true)
                    {
                        cred = cred + "SID";
                    }

                    // check for blank entries
                    if (lastname != "" && firstname != "" && middlename != ""
                        && birthday != "" && contact != "" &&
                        guardian != "" && zone != "" && cred != "")
                    {
                        senior.FirstName = firstname;
                        senior.LastName = lastname;
                        senior.MiddleName = middlename;
                        senior.Birthday = birthday;
                        senior.Contact = contact;
                        senior.Guardian = guardian;
                        senior.Zone = zone;
                        senior.Credential = cred;
                        senior.Benefit1 = "n/a";
                        senior.Benefit2 = "n/a";
                        senior.Benefit3 = "n/a";
                        senior.Benefit4 = "n/a";
                        context.SaveChanges();
                        errorMessage.Visibility = Visibility.Hidden;
                        loloPic.Source = new BitmapImage(new Uri(@"images/senior.png", UriKind.Relative));
                        // save data to database
                    }
                }
                // do the edit and exit                
                return;
            }
            
            using (DataContext context = new DataContext())
            {
                // get data from form
                var lastname = txtLastName.Text;
                var firstname = txtFirstName.Text;
                var middlename = txtMiddleName.Text;
                var birthday = txtBirthday.Text;
                var contact = txtContact.Text;
                var guardian = txtGuardian.Text;
                var zone = cmbZone.Text;
                var cred = "";

                if (chkPSA.IsChecked == true) 
                {
                    cred = cred + "PSA";
                }
                if (chkSID.IsChecked == true)
                {
                    cred = cred + "SID";
                }

                // check for blank entries
                if (lastname != "" && firstname != "" && middlename!="" 
                    && birthday!="" && contact!= "" && 
                    guardian!="" && zone!="" && cred!="") 
                {                  
                
                    context.Seniors.Add(new Senior()
                    {
                        FirstName = firstname,
                        LastName = lastname,
                        MiddleName = middlename,
                        Birthday = birthday,
                        Contact = contact,
                        Guardian = guardian,
                        Zone = zone,
                        Credential = cred,
                        Benefit1 = "n/a",
                        Benefit2 = "n/a",
                        Benefit3 = "n/a",
                        Benefit4 = "n/a"
                    });           
                    context.SaveChanges();
                    errorMessage.Visibility = Visibility.Hidden;
                    loloPic.Source = new BitmapImage(new Uri(@"images/senior.png",UriKind.Relative));
                    // save data to database
                } else 
                {
                    loloPic.Source = new BitmapImage(new Uri(@"images/warning.png", UriKind.Relative));
                    errorMessage.Visibility = Visibility.Visible;
                    btnSave.Visibility = Visibility.Visible;
                }
            }
        } // saveData

        private void showData() 
        {
            using (DataContext context = new DataContext())
            {
                SeniorDatabase = context.Seniors.ToList();
                seniorsList.ItemsSource = SeniorDatabase;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            
            using (DataContext context = new DataContext())
            {
                seniorsList.ItemsSource =
                    context.Seniors.Where(x => x.LastName.Contains(searchText) ||
                    x.FirstName.Contains(searchText) 
                    || x.MiddleName.Contains(searchText)
                    || x.Credential.Contains(searchText) 
                    || x.Zone.Contains(searchText)).ToList();
            }
        }
    }
}
