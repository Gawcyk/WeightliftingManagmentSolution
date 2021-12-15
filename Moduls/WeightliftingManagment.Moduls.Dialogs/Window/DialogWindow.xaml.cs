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

using MahApps.Metro.Controls;

using Prism.Services.Dialogs;

namespace WeightliftingManagment.Moduls.Dialogs.Window
{
    /// <summary>
    /// Logika interakcji dla klasy DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : MetroWindow, IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
