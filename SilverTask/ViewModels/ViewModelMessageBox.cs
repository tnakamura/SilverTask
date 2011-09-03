using System;
using System.Windows;

namespace SilverTask.ViewModels
{
    public static class ViewModelMessageBox
    {
        internal static Func<string, bool?> _Show = PrivateShow;

        public static bool? Show(string message)
        {
            return _Show(message);
        }

        private static bool? PrivateShow(string message)
        {
            bool? result = null;
            if (MessageBox.Show(message) == MessageBoxResult.OK)
            {
                result = true;
            }
            return result;
        }
    }
}
