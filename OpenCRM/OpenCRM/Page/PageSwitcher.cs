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

        public static void Switch(Uri newPage)
        {
            mainwindow.frmSource.Navigate(newPage);
        }
    }
    
}
