using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Message> messages = new ObservableCollection<Message>();
        public static User CurrentUser = new User();
        public static User CurrentReceiver = new User();
        public MainWindow()
        {
            this.DataContext = this;
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
                messages = new ObservableCollection<Message>(new List<Message>() { new Message("Hello" +
                    "fuouifb9eqgfy9qwf9wq9fwq79fw79qf97wqf79eqf79wq79fvqew9vfwqv9fywqf" +
                    "feuoqfuebqufbequ0fb0eqwbf80\nwqbf80q\neb80fbeq08f80eqdfh08wqhd08w 0fhw08 f80qweh fg0h q08f " +
                    "g iejg heq g79gf97q 9 feq f9qf97 gd79 79", "uwu", "xx"),
                    new Message("Hello" +
                                "fuouifb9eqbf80wqbf8fefbiyeqvfyieq" +
                                "feqbpufbequofvuoeqvfuoeqvofeq" +
                                "feqfbequofueoqfuoeqvuf0vquo0fqe" +
                                "" +
                                "eqfqebfueqfuovequofvbequofbuoeqvfuoeqbfoueq" +
                                "fwqbfouwqvofuqwvyuofvyi97 gd79 79", "uwus", "xx"),
                    new Message("Hello" +
                                "fuouifb9eqgfy", "uwus", "xx")
                });
                Chat.ItemsSource = messages;

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
            messages.Add(new Message(SendMEssageTextbox.Text, CurrentUser.Name, "rec"));
            Chat.ItemsSource = messages;

            Chat.SelectedIndex = Chat.Items.Count - 1;
            Chat.ScrollIntoView(Chat.SelectedItem);
            Chat.SelectedItem = null;

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

        private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
        {
            TextBlock? tempTextBlock = sender as TextBlock;
            tempTextBlock.Height -= 10;
            tempTextBlock.Width -= 40;
            tempTextBlock.Margin = new Thickness(tempTextBlock.Margin.Left + 10, tempTextBlock.Margin.Top + 10,
                tempTextBlock.Margin.Right, tempTextBlock.Margin.Bottom);
        }

        private void Border_OnInitialized(object? sender, EventArgs e)
        {
            Border? br = sender as Border;
            br.Width = br.Width + 20;
        }
        public double heightx = 0;
        private void GetHeight(object? sender, EventArgs e)
        {
            TextBox? tt = sender as TextBox;
            heightx = tt.Height;
        }

        private void Border_OnLoaded(object sender, RoutedEventArgs e)
        {
            Border? br = sender as Border;
            br.Height = br.Height + 20;
        }
    }
}
