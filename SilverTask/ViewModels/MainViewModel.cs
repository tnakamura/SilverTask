
namespace SilverTask.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// <see cref="MainViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainViewModel()
        {
            this.Tasks = new TasksViewModel();
        }

        /// <summary>
        /// タスク一覧を取得します。
        /// </summary>
        public TasksViewModel Tasks { get; private set; }
    }
}
