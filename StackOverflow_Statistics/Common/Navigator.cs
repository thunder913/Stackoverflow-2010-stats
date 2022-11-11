using StackOverflow_Statistics.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace StackOverflow_Statistics.Common
{
    public static class Navigator
    {
        private static NavigationService NavigationService { get; } = (Application.Current.MainWindow as MainWindow).NavigationService;
        public static object Parameter { get; private set; }
        public static void NavigateTo(object root)
        {
            NavigationService.Navigate(root);
        }

        public static void Navigate(string path, object param = null)
        {
            Parameter = param;
            NavigationService.Navigate(new Uri(path, UriKind.Relative), param);
        }

        public static void GoBack()
        {
            NavigationService.GoBack();
        }

        public static void GoForward()
        {
            NavigationService.GoForward();
        }
    }
}
