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

using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingItemList.ViewModels;

namespace ShoppingItemList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty ApplicationProperty =
            DependencyProperty.Register("Application", typeof(ApplicationViewModel), typeof(MainWindow));

        public ApplicationViewModel Application
        {
            get { return (ApplicationViewModel)GetValue(ApplicationProperty); }
            private set { SetValue(ApplicationProperty, value); }
        }

        public MainWindow()
        {
            Application = new ApplicationViewModel();
            InitializeComponent();
        }
    }
}
