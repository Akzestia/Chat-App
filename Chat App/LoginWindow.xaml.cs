using System;
using System.Collections.Generic;
using System.Data.Linq;
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
using Chat_App.Entities;
using Chat_App.Linq;
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
            TextBox_PasswordShow_Signup_Repeat.Visibility = Visibility.Hidden;
            TextBox_PasswordShow_Signup.Visibility = Visibility.Hidden;
            SignupCanvas.Visibility = Visibility.Hidden;
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
            try
            {
                
                DataContext db = new DataContext(SqlMethods.connectionstring);
                var temp = db.GetTable<User>();
                int k = 0;
                foreach (var VARIABLE in temp)
                {
                    if (VARIABLE.Name == UserNameLogin.Text && VARIABLE.Password == Password_Login.Password)
                    {
                        k = 1;
                        MainWindow.CurrentUser = VARIABLE;
                        MainWindow.CurrentUser.InitUSerPort();
                        Thread t = new Thread(() =>
                        {
                            this.Dispatcher.Invoke(() => { mn.Show(); });

                        });
                        t.Start();
                        this.Close();
                    }

                }

                if (k == 0)
                {
                    MessageBox.Show("Incorrect User Name or Password");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
           
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

        private void CreateAccount_OnMouseEnter(object sender, MouseEventArgs e)
        {
            CreateAccount.Foreground = Brushes.Aquamarine;
            CreateAccount.TextDecorations = TextDecorations.Underline;
        }

        private void CreateAccount_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var color = (Color)ColorConverter.ConvertFromString("#cdd9e5");
            var colorlast = new SolidColorBrush(color);
            CreateAccount.Foreground = colorlast;
            CreateAccount.TextDecorations = null;
        }

        private void CreateAccount_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoginCanvas.Visibility = Visibility.Hidden;
            Password_Login.Password = "";
            TextBox_PasswordShow_Login.Text = "";
            UserNameLogin.Text = "";
            SignupCanvas.Visibility = Visibility.Visible;
        }


        public async Task signupnewuser()
        {
            DataContext db = new DataContext(SqlMethods.connectionstring);

            var t = db.GetTable<User>();
            int k = 0;
            foreach (var VARIABLE in t)
            {
                if (VARIABLE.Name == UserNameSignUp.Text)
                {
                    k = 1;
                    break;
                }
            }

            if (Password_SignUp.Password == Password_SignUp_Repeat.Password && k == 0)
            {
                MainWindow.CurrentUser = new User(UserNameSignUp.Text, Password_SignUp.Password);
                db.GetTable<User>().InsertOnSubmit(MainWindow.CurrentUser);
                db.SubmitChanges();
                MainWindow.CurrentUser.InitUSerPort();

            }
            else
            {
                if (k == 1) //User Name is already taken
                {

                }
                else //Incorrect password
                {

                }
            }
            await Task.Delay(1);
        }

        private async void SignUpClick(object sender, RoutedEventArgs e)
        {
            await signupnewuser();
            Thread startchatwindowthread = new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { mn.Show(); });

            });
            startchatwindowthread.Start();
            this.Close();
        }

        private void BackToLogin_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BackToLogin.Foreground = Brushes.Aquamarine;
            BackToLogin.TextDecorations = TextDecorations.Underline;
        }

        private void BackToLogin_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var color = (Color)ColorConverter.ConvertFromString("#cdd9e5");
            var colorlast = new SolidColorBrush(color);
            BackToLogin.Foreground = colorlast;
            BackToLogin.TextDecorations = null;
        }

        private void BackToLogin_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SignupCanvas.Visibility = Visibility.Hidden;
            LoginCanvas.Visibility = Visibility.Visible;
            TextBox_PasswordShow_Signup_Repeat.Text = "";
            TextBox_PasswordShow_Signup.Text = "";
            Password_SignUp.Password = "";
            Password_SignUp_Repeat.Password = "";
            UserNameSignUp.Text ="";
        }

        private void ShowHidePAsswordButton_SignUp1_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ShowHidePAsswordButton_SignUp1.Icon == FontAwesomeIcon.Eye)
            {
                TextBox_PasswordShow_Signup.Text = Password_SignUp.Password;
                TextBox_PasswordShow_Signup.Visibility = Visibility.Visible;
                Password_SignUp.Visibility = Visibility.Hidden;
                ShowHidePAsswordButton_SignUp1.Icon = FontAwesomeIcon.EyeSlash;
            }
            else
            {
                Password_SignUp.Password = TextBox_PasswordShow_Signup.Text;
                TextBox_PasswordShow_Signup.Visibility = Visibility.Hidden;
                Password_SignUp.Visibility = Visibility.Visible;
                ShowHidePAsswordButton_SignUp1.Icon = FontAwesomeIcon.Eye;
            }
        }

        private void ShowHidePAsswordButton_SignUp2_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ShowHidePAsswordButton_SignUp2.Icon == FontAwesomeIcon.Eye)
            {
                TextBox_PasswordShow_Signup_Repeat.Text = Password_SignUp_Repeat.Password;
                TextBox_PasswordShow_Signup_Repeat.Visibility = Visibility.Visible;
                Password_SignUp_Repeat.Visibility = Visibility.Hidden;
                ShowHidePAsswordButton_SignUp2.Icon = FontAwesomeIcon.EyeSlash;
            }
            else
            {
                Password_SignUp_Repeat.Password = TextBox_PasswordShow_Signup_Repeat.Text;
                TextBox_PasswordShow_Signup_Repeat.Visibility = Visibility.Hidden;
                Password_SignUp_Repeat.Visibility = Visibility.Visible;
                ShowHidePAsswordButton_SignUp2.Icon = FontAwesomeIcon.Eye;
            }
        }
    }
}
