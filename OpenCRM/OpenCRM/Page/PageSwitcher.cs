using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM
{
    public static class PageSwitcher
    {
        public static MainWindow mainwindow;
        /// <summary>
        /// Switch the current page of the mainFrame to any that you specify with the Uri
        /// </summary>
        /// <param name="newPage">Uri of the New Page</param>
        public static void Switch(Uri newPage)
        {
            mainwindow.frmSource.Navigate(newPage);
            
        }
        /// <summary>
        /// Hides or display the buttons of the main navigation in the window.
        /// </summary>
        /// <param name="show">True to show or False to hide</param>
        public static void MainButtons(Boolean show)
        {
            if (show)
            {
                PageSwitcher.mainwindow.btnHome.Visibility = System.Windows.Visibility.Visible;
                PageSwitcher.mainwindow.btnSettings.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                PageSwitcher.mainwindow.btnHome.Visibility = System.Windows.Visibility.Hidden;
                PageSwitcher.mainwindow.btnSettings.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        /// <summary>
        /// Displays the Page for login if the param show is true, otherwise hides it.
        /// </summary>
        /// <param name="show">True to show or False to hide</param>
        public static void ShowFrame(Boolean show)
        {
            if (show)
            {
                PageSwitcher.mainwindow.frmSource.Visibility = System.Windows.Visibility.Visible;
                PageSwitcher.mainwindow.gridSplash.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                PageSwitcher.mainwindow.frmSource.Visibility = System.Windows.Visibility.Hidden;
                PageSwitcher.mainwindow.gridSplash.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
    
}
