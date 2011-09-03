using System;

namespace SilverTask.ViewModels
{
    /// <summary>
    /// コマンドを管理します。
    /// </summary>
    public static class CommandManager
    {
        public static event EventHandler RequerySuggested;

        /// <summary>
        /// <see cref="RequerySuggested"/> イベントを発生させます。
        /// </summary>
        public static void FireRequerySuggested()
        {
            var handler = RequerySuggested;
            if (handler != null)
            {
                handler(null, EventArgs.Empty);
            }
        }
    }
}
