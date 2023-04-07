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
using Chat_App.Linq;
using SimpleTCP;
using Message = Chat_App.Entities.Message;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IPAddress remoteIPAddress;
        private static int remotePort;
        private static int localPort;
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
                localPort = CurrentUser.LocalPort;
                Chat.ItemsSource = messages;
                if (messages.Count > 0)
                {
                    Chat.SelectedIndex = Chat.Items.Count - 1;
                    Chat.ScrollIntoView(Chat.SelectedItem);
                    Chat.SelectedItem = null;
                }
               
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

                remoteIPAddress = IPAddress.Parse("127.0.0.1");
                Thread tRec = new Thread(Receiver);
                tRec.Start();
                ReceiverNameLabel.Content = CurrentReceiver.Name;
            }
            catch (Exception exception)
            {
               
            }
          
        }
        private static void Send(string datagram)
        {
            UdpClient sender = new UdpClient();

            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);


            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(datagram);
                sender.Send(bytes, bytes.Length, endPoint);

            }
            catch (ArgumentOutOfRangeException ex2)
            {

            }
            catch (Exception ex)
            {

            }

            finally
            {
                sender.Close();
            }
        }

        public void Receiver()
        {
            UdpClient receivingUpdClient = new UdpClient(localPort);

            IPEndPoint RemoteIpEndPoint = null;

            try
            {

                while (true)
                {
                    byte[] receiverButes = receivingUpdClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.UTF8.GetString(receiverButes);
                    try
                    {
                        this.Dispatcher.Invoke(async () =>
                        {
                            messages.Add(new Message(returnData, CurrentReceiver.Name, CurrentUser.Name));
                            Chat.ItemsSource = messages;
                            Chat.SelectedIndex = Chat.Items.Count - 1;
                            Chat.ScrollIntoView(Chat.SelectedItem);
                            Chat.SelectedItem = null;
                        });
                      
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            catch (ArgumentOutOfRangeException ex2)
            {

            }
            catch (Exception ex)
            {

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
            if (SendMEssageTextbox.Text.Length > 0)
            {
                Send(SendMEssageTextbox.Text);

                messages.Add(new Message(SendMEssageTextbox.Text, CurrentUser.Name, CurrentReceiver.Name));
                Chat.ItemsSource = messages;
                Chat.SelectedIndex = Chat.Items.Count - 1;
                Chat.ScrollIntoView(Chat.SelectedItem);
                Chat.SelectedItem = null;
                SendMEssageTextbox.Text = "";
            }
            
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

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && SendMEssageTextbox.Text.Length > 0)
            {
                Send(SendMEssageTextbox.Text);

                messages.Add(new Message(SendMEssageTextbox.Text, CurrentUser.Name, CurrentReceiver.Name));
                Chat.ItemsSource = messages;
                Chat.SelectedIndex = Chat.Items.Count - 1;
                Chat.ScrollIntoView(Chat.SelectedItem);
                Chat.SelectedItem = null;
                SendMEssageTextbox.Text = "";
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
