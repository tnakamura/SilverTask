using System;
using System.Windows;
using System.Windows.Controls;
using SilverTask.ViewModels;

namespace SilverTask.Views
{
    /// <summary>
    /// タスクを表示するためのビュー。
    /// </summary>
    public partial class TaskView : UserControl
    {
        /// <summary>
        /// <see cref="TaskView"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TaskView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ビューにバインドされているビューモデルを取得します。
        /// </summary>
        private TaskViewModel ViewModel
        {
            get { return (TaskViewModel)DataContext; }
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.IsEditing = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.IsEditing = false;
            RequestCancel(this, new EventArgs());

            // TextBox の内容をクリア
            if (ViewModel.IsNew == false)
            {
                _nameTextBox.Text = ViewModel.Name;
                var exp = _nameTextBox.GetBindingExpression(TextBox.TextProperty);
                exp.UpdateSource();
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var exp = _nameTextBox.GetBindingExpression(TextBox.TextProperty);
            exp.UpdateSource();
            if (ViewModel.HasErrors == false)
            {
                RequestSave(this, new EventArgs());
                ViewModel.IsEditing = false;
            }
        }

        /// <summary>
        /// タスクの削除するときに発生します。
        /// </summary>
        public event EventHandler RequestDelete = delegate { };

        /// <summary>
        /// タスクの完了するときに発生します。
        /// </summary>
        public event EventHandler RequestComplete = delegate { };

        /// <summary>
        /// タスクを保存するときに発生します。
        /// </summary>
        public event EventHandler RequestSave = delegate { };

        /// <summary>
        /// タスクを未完了にするときに発生します。
        /// </summary>
        public event EventHandler RequestUncomplete = delegate { };

        /// <summary>
        /// タスクの編集をキャンセルするときに発生します。
        /// </summary>
        public event EventHandler RequestCancel = delegate { };

        private void CompleteButtonClick(object sender, RoutedEventArgs e)
        {
            RequestComplete(this, new EventArgs());
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            RequestDelete(this, new EventArgs());
        }

        private void UnCompletedButtonClick(object sender, RoutedEventArgs e)
        {
            RequestUncomplete(this, new EventArgs());
        }
    }
}
