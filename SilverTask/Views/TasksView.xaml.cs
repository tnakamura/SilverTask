using System;
using System.Windows;
using System.Windows.Controls;
using SilverTask.ViewModels;
using System.Windows.Input;

namespace SilverTask.Views
{
    /// <summary>
    /// タスク一覧を表示するビュー。
    /// </summary>
    public partial class TasksView : UserControl
    {
        /// <summary>
        /// <see cref="TaskView"/> クラスの新しいインスタンスを取得します。
        /// </summary>
        public TasksView()
        {
            InitializeComponent();

            CommandManager.RequerySuggested += (sender, e) =>
            {
                // 完了済みを表示しているときはタスクを追加させない
                _addButton.IsEnabled = ViewModel != null ? !ViewModel.IsShowCompleted : false;
            };
        }

        /// <summary>
        /// ビューにバインドされているビューモデルを取得します。
        /// </summary>
        private TasksViewModel ViewModel
        {
            get
            {
                return (TasksViewModel)DataContext;
            }
        }

        private void OnRequestComplete(object sender, EventArgs e)
        {
            TaskViewModel task = (TaskViewModel)((FrameworkElement)sender).DataContext;
            ViewModel.ComplateTask(task);
        }

        private void OnRequestDelete(object sender, EventArgs e)
        {
            TaskViewModel task = (TaskViewModel)((FrameworkElement)sender).DataContext;
            ViewModel.DeleteTask(task);
        }

        private void OnRequestSave(object sender, EventArgs e)
        {
            TaskViewModel task = (TaskViewModel)((FrameworkElement)sender).DataContext;
            if (task.IsNew)
            {
                ViewModel.CreateTask(task);
            }
            else
            {
                ViewModel.UpdateTask(task);
            }
        }

        private void OnRequestCancel(object sender, EventArgs e)
        {
            TaskViewModel task = (TaskViewModel)((FrameworkElement)sender).DataContext;
            if (task.IsNew)
            {
                ViewModel.Tasks.Remove(task);
            }
        }

        private void OnRequestUncomplete(object sender, EventArgs e)
        {
            TaskViewModel task = (TaskViewModel)((FrameworkElement)sender).DataContext;
            ViewModel.UncomplateTask(task);
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var newTask = ViewModel.NewTask();
            _listBox.ScrollIntoView(newTask);
        }
    }
}
