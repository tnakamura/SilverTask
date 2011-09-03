using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SilverTask.Models;

namespace SilverTask.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        /// <summary>
        /// <see cref="TasksViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TasksViewModel()
            : this(RepositoryFactory.CreateTaskRepository())
        {
        }

        /// <summary>
        /// <see cref="TasksViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="repository">タスクが保存されているリポジトリ。</param>
        public TasksViewModel(ITaskRepository repository)
        {
            this._repository = repository;
            this.Tasks = new ObservableCollection<TaskViewModel>();
        }

        /// <summary>
        /// タスクが保存されているリポジトリ。
        /// </summary>
        private readonly ITaskRepository _repository;

        /// <summary>
        /// タスクの一覧を取得します。
        /// </summary>
        public ObservableCollection<TaskViewModel> Tasks { get; private set; }

        /// <summary>
        /// 完了済みのタスクを表示しているかどうか。
        /// </summary>
        private bool _isShowCompleted = false;

        /// <summary>
        /// 完了済みのタスクを表示しているかどうか示す値を取得または設定します。
        /// </summary>
        public bool IsShowCompleted
        {
            get { return _isShowCompleted; }
            set
            {
                if (_isShowCompleted != value)
                {
                    _isShowCompleted = value;
                    OnPropertyChanged("IsShowCompleted");
                }
            }
        }

        /// <summary>
        /// 新しいタスクを追加します。
        /// </summary>
        /// <returns>新しいタスク。</returns>
        public TaskViewModel NewTask()
        {
            var newTask = new TaskViewModel()
            {
                IsEditing = true,
                IsNew = true,
            };
            Tasks.Insert(0, newTask);
            return newTask;
        }

        private ICommand _loadCommand = null;

        public ICommand LoadCommand
        {
            get
            {
                return _loadCommand = _loadCommand ??
                    new DelegateCommand(_ =>
                    {
                        LoadTasks();
                    });
            }
        }

        /// <summary>
        /// タスクを取得します。
        /// </summary>
        public void LoadTasks()
        {
            this._repository.ReadTask(IsShowCompleted, result =>
            {
                if (result.Error != null)
                {
                    ViewModelMessageBox.Show(result.Error.Message);
                    return;
                }
                App.CurrentDispatcher.BeginInvoke(() =>
                {
                    this.Tasks.Clear();
                    foreach (var task in result.Tasks)
                    {
                        this.Tasks.Add(new TaskViewModel(task));
                    }
                });
            });
        }

        /// <summary>
        /// 指定したタスクを完了します。
        /// </summary>
        /// <param name="task">完了するタスク。</param>
        public void ComplateTask(TaskViewModel task)
        {
            var t = task.Unwrap();
            t.Done = true;
            _repository.UpdateTask(t, result =>
            {
                if (result.Error != null)
                {
                    ViewModelMessageBox.Show(result.Error.Message);
                    return;
                }
                LoadTasks();
            });
        }

        /// <summary>
        /// 指定したタスクを未完了します。
        /// </summary>
        /// <param name="task">未完了にするタスク。</param>
        public void UncomplateTask(TaskViewModel task)
        {
            var t = task.Unwrap();
            t.Done = false;
            _repository.UpdateTask(t, result =>
            {
                if (result.Error != null)
                {
                    ViewModelMessageBox.Show(result.Error.Message);
                    return;
                }
                LoadTasks();
            });
        }

        /// <summary>
        /// タスクを作成します。
        /// </summary>
        /// <param name="task">作成するタスク。</param>
        public void CreateTask(TaskViewModel task)
        {
            _repository.CreateTask(task.Unwrap(), result =>
            {
                if (result.Error != null)
                {
                    ViewModelMessageBox.Show(result.Error.Message);
                    return;
                }
                LoadTasks();
            });
        }

        /// <summary>
        /// 指定したタスクを更新します。
        /// </summary>
        /// <param name="task">更新するタスク。</param>
        public void UpdateTask(TaskViewModel task)
        {
            _repository.UpdateTask(task.Unwrap(), result =>
            {
                if (result.Error != null)
                {
                    ViewModelMessageBox.Show(result.Error.Message);
                    return;
                }
                LoadTasks();
            });
        }

        /// <summary>
        /// 指定したタスクを削除します。
        /// </summary>
        /// <param name="task">削除するタスク。</param>
        public void DeleteTask(TaskViewModel task)
        {
            _repository.DeleteTask(task.Unwrap(), result =>
            {
                if (result.Error != null)
                {
                    ViewModelMessageBox.Show(result.Error.Message);
                    return;
                }
                LoadTasks();
            });
        }
    }
}
