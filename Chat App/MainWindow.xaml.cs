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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Data.Linq;
using Chat_App.Entities;
using System.IO;
using System.Windows.Media.Animation;

namespace Chat_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static User CurrentUser = new User();
        public MainWindow()
        {
            InitializeComponent();
            Search_Settings_View.Visibility = Visibility.Hidden;
            Search_Settings_View.Margin = new Thickness(-360, 40, 0, 0);
            TransparentBorder.Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
        }

        private void CloseWindowButtonClick(object sender, MouseButtonEventArgs e) //Closing window
        {
            this.Close();
            
        }

        private void DragWindowOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) //Dragging window
        {
            this.DragMove();
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var color = (Color)ColorConverter.ConvertFromString("#cdd9e5");
            var colorlast = new SolidColorBrush(color);
            SettingsBars.Foreground = colorlast;
            
        }

        private void SettingsBars_OnMouseLeave(object sender, MouseEventArgs e)
        {
            SettingsBars.Foreground = Brushes.Aquamarine;
        }
        DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dir = dir.Parent?.Parent?.Parent;
                userName.Content = CurrentUser.Name;
                if (CurrentUser.Avatar == null)
                {
                    MessageBox.Show("Avatar Error!");
                }
                else
                {
                    BitmapImage bp = new BitmapImage();
                    bp.BeginInit();
                    bp.StreamSource = new MemoryStream(CurrentUser.Avatar);
                    bp.EndInit();
                    AvatarBorder.Background = new ImageBrush(bp);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
          
        }

        private async void TransparentBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            await Task.Delay(500);
            Search_Settings_View.Visibility = Visibility.Hidden;
        }

        private void SettingsBars_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Search_Settings_View.Visibility = Visibility.Visible;
            BeginStoryboard sb = this.FindResource("ShowSettingsStoryBoard") as BeginStoryboard;
            sb.Storyboard.Begin();
            TransparentBorder.Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
        }

        private void SendMessage_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var color = (Color)ColorConverter.ConvertFromString("#cdd9e5");
            var colorlast = new SolidColorBrush(color);
            SendMessage.Foreground = colorlast;
        }

        private void SendMessage_OnMouseLeave(object sender, MouseEventArgs e)
        {
            SendMessage.Foreground = Brushes.Aquamarine;
        }

        private void SendMessage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMEssageTextbox.Text = "";
        }

        private int linecount1 = 0;
        private int y = 0;
        private void SendMEssageTextbox_OnTextChanged(object sender, TextChangedEventArgs e)//635 642
        {
            
          
            if (SendMEssageTextbox.LineCount < 4)
            {
                SendMEssageTextbox.Height = SendMEssageTextbox.LineCount * 50;
                TXTBorderx.Height = (SendMEssageTextbox.LineCount * 50)+ 25;
                if (linecount1 < SendMEssageTextbox.LineCount && SendMEssageTextbox.LineCount < 4)
                {
                    SendMEssageTextbox.Margin = new Thickness(SendMEssageTextbox.Margin.Left, SendMEssageTextbox.Margin.Top - (SendMEssageTextbox.Height - 50), SendMEssageTextbox.Margin.Right,
                        SendMEssageTextbox.Margin.Bottom);
                    TXTBorderx.Margin = new Thickness(TXTBorderx.Margin.Left, TXTBorderx.Margin.Top - (SendMEssageTextbox.Height - 50), TXTBorderx.Margin.Right,
                        TXTBorderx.Margin.Bottom);

                }

                if (TXTBorderx.Margin.Top == 485)
                {
                    TXTBorderx.Margin = new Thickness(TXTBorderx.Margin.Left, TXTBorderx.Margin.Top + 50, TXTBorderx.Margin.Right,
                        TXTBorderx.Margin.Bottom);
                    SendMEssageTextbox.Margin = new Thickness(SendMEssageTextbox.Margin.Left, SendMEssageTextbox.Margin.Top + 50, SendMEssageTextbox.Margin.Right,
                        SendMEssageTextbox.Margin.Bottom);
                }
                if (SendMEssageTextbox.LineCount == 1 && SendMEssageTextbox.Text.Length == 0)
                {
                    TXTBorderx.Margin = new Thickness(TXTBorderx.Margin.Left, 635, TXTBorderx.Margin.Right,
                        TXTBorderx.Margin.Bottom);
                    SendMEssageTextbox.Margin = new Thickness(SendMEssageTextbox.Margin.Left, 642, SendMEssageTextbox.Margin.Right,
                        SendMEssageTextbox.Margin.Bottom);
                }
            }

            
            
            linecount1 = SendMEssageTextbox.LineCount;
        }
    }
}
