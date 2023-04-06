using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Chat_App.Entities;

namespace Chat_App
{
    public class TemplateSelector : DataTemplateSelector //Template Selector for messages in chat
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is Chat_App.Entities.Message)
            {
                Chat_App.Entities.Message? taskitem = item as Chat_App.Entities.Message;

                if (taskitem.Sender == MainWindow.CurrentUser.Name)
                {
                    return
                        element.FindResource("right_side_message") as DataTemplate;
                }
                else
                {
                    return
                        element.FindResource("left_side_message") as DataTemplate;
                }
            }
            else
            {

                return element.FindResource("right_side_message") as DataTemplate;
            }


        }
    }

    public class ContainerSelector : StyleSelector //Container Selector for messages in chat
    {
        public Style Style1 { get; set; }
        public Style Style2 { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            Message s = (Message)item;
            if (s.Sender == MainWindow.CurrentUser.Name)
                return Style1;
            else
                return Style2;
        }
    }
}
