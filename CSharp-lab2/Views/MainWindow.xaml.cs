using System.Windows;
using CSharp_lab2.ViewModels;

namespace CSharp_lab2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new PersonViewModel();
        }
    }
}
