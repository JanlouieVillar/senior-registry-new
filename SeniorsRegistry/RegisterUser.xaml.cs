﻿using System;
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
    
    public partial class RegisterUser : Window
    {
        public List<User> UserDatabase { get; private set; }
        public RegisterUser()
        {
            InitializeComponent();
            showData();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
        private void showData()
        {
            using (DataContext context = new DataContext())
            {
                UserDatabase = context.Users.ToList();
                usersList.ItemsSource = UserDatabase;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            User SelectedUser = usersList.SelectedItem as User;
            using (DataContext context = new DataContext())
            {
                // get data from form
                var fullname = txtFullName.Text;
                var username = CeasarCipher(txtUserName.Text);
                var userpass = EncodePass(txtUserPass.Password.ToString());
                var administrator = txtAdministrator.Text;

                // check for blank entries
                if (fullname != "" && username != "" && userpass.ToString() != ""
                    && administrator != "")
                {

                    context.Users.Add(new User()
                    {
                        FullName = fullname,
                        UserName = username,
                        UserPass = userpass.ToString(),
                        Administrator = 0
                    });
                        
                    
                    context.SaveChanges();
                    errorMessage.Visibility = Visibility.Hidden;                    
                    // save data to database
                }
                else
                {                    
                    errorMessage.Visibility = Visibility.Visible;
                    btnSave.Visibility = Visibility.Visible;
                }

            }
        }

        private int EncodePass(string pass2Encode)
        {
            int encoded = 0;
            char temp;
            int i = pass2Encode.Length;
            for (int j = 0; j < i; j++)
            {
                temp = pass2Encode[j];
                encoded = encoded + Asc(temp);
            }
            return encoded;
        }

        private int Asc(char String)
        {
            int int32 = Convert.ToInt32(String);
            if (int32 < 128)
                return int32;
            try
            {
                Encoding fileIoEncoding = Encoding.Default;
                char[] chars = new char[1] { String };
                if (fileIoEncoding.IsSingleByte)
                {
                    byte[] bytes = new byte[1];
                    fileIoEncoding.GetBytes(chars, 0, 1, bytes, 0);
                    return (int)bytes[0];
                }
                byte[] bytes1 = new byte[2];
                if (fileIoEncoding.GetBytes(chars, 0, 1, bytes1, 0) == 1)
                    return (int)bytes1[0];
                if (BitConverter.IsLittleEndian)
                {
                    byte num = bytes1[0];
                    bytes1[0] = bytes1[1];
                    bytes1[1] = num;
                }
                return (int)BitConverter.ToInt16(bytes1, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CeasarCipher(string test)
        {
            string encoded = "";

            char[] highlet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };
            char[] lowlet = { 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N' };

            test = test.ToUpper();
            int el = test.Length;
            for (int i = 0; i < el; i++)
            {
                for (int k = 0; k < 13; k++)
                {
                    if (Asc(test[i]) > 64 && Asc(test[i]) < 78)
                    {
                        if (test[i] == highlet[k])
                        {
                            encoded = encoded + lowlet[k];
                            break;
                        }
                    }
                    else
                    {
                        if (Asc(test[i]) == 32)
                        {
                            encoded = encoded + " ";
                            break;
                        }
                        else
                        {
                            if (test[i] == lowlet[k])
                            {
                                encoded = encoded + highlet[k];
                                break;
                            }
                        } // == 32
                    }  // > 64 and < 78
                }  // for k
            } // for i

            return encoded;
        }

    }
}
