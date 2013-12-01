using OpenCRM.Controllers.Session;
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
using System.Windows.Shapes;

namespace OpenCRM.Views.Chat
{
    /// <summary>
    /// Lógica de interacción para ChatView.xaml
    /// </summary>
    public partial class ChatView
    {
        List<String> Messages;
        public ChatView()
        {
            InitializeComponent();
            Messages = new List<String>();
            tbxUserName.Text = Session.UserName;
        }

        private void btnAddUsers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVolume_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStartConference_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbxSendMessage.Text != "")
            {
                Messages.Add(tbxSendMessage.Text);
                tbxMessages.Text += "                                   " + DateTime.Now.ToString("G") + "\r\n" + tbxSendMessage.Text + "\r\n-------------------------------------------------------------------------------\r\n\r\n";
                tbxSendMessage.Text = "";
            }
        }
    }
}
