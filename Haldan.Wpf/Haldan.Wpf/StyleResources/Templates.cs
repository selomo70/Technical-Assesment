using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Haldan.Wpf.StyleResources
{
    public partial class Templates : ResourceDictionary
    {
        public Templates()
        {
           
        }

        private void PART_TITLEBAR_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            Window window = border.Tag as Window;
            if (window != null)
            {
                window.DragMove();
            }
        }

        private void PART_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            var btnClose = sender as Button;
            Window window = btnClose.Tag as Window;
            if (window != null)
            {
                window.Close();
            }
        }

        private void PART_MAXIMIZE_RESTORE_Click(object sender, RoutedEventArgs e)
        {
            var btnMaximize = sender as Button;
            Window window = btnMaximize.Tag as Window;
            if (window != null)
            {
                if (window.WindowState == System.Windows.WindowState.Normal)
                {
                    window.WindowState = System.Windows.WindowState.Maximized;
                }
                else
                {
                    window.WindowState = System.Windows.WindowState.Normal;
                }
            }
        }

        private void PART_MINIMIZE_Click(object sender, RoutedEventArgs e)
        {
            var btnClose = sender as Button;
            Window window = btnClose.Tag as Window;
            if (window != null)
            {
                window.WindowState = System.Windows.WindowState.Minimized;
            }
        }
    }
}
