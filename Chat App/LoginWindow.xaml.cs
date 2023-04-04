using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FontAwesome.WPF;

namespace Chat_App
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            
            InitializeComponent();
            TextBox_PasswordShow_Login.Visibility = Visibility.Hidden;
        }

        private void Close_Sign_in_window(object sender, MouseButtonEventArgs e) //Closing sign in window
        {
            this.Close();
        }

        private void Draging_Signin_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        MainWindow mn = new MainWindow();
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            
            Thread t = new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { mn.Show(); });

            });
            t.Start();
            this.Close();
        }

        private void Show_Password_Button(object sender, MouseButtonEventArgs e)
        {
            if (ShowHidePAsswordButton.Icon == FontAwesomeIcon.Eye)
            {
                TextBox_PasswordShow_Login.Text = Password_Login.Password;
                TextBox_PasswordShow_Login.Visibility = Visibility.Visible;
                Password_Login.Visibility = Visibility.Hidden;
                ShowHidePAsswordButton.Icon = FontAwesomeIcon.EyeSlash;
            }
            else
            {
                Password_Login.Password = TextBox_PasswordShow_Login.Text;
                TextBox_PasswordShow_Login.Visibility = Visibility.Hidden;
                Password_Login.Visibility = Visibility.Visible;
                ShowHidePAsswordButton.Icon = FontAwesomeIcon.Eye;
            }
        }
    }
}
