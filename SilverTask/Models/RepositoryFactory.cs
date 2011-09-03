
namespace SilverTask.Models
{
    /// <summary>
    /// リポジトリを生成します。
    /// </summary>
    public static class RepositoryFactory
    {
        /// <summary>
        /// タスクが保存されているリポジトリを取得します。
        /// </summary>
        /// <returns>タスクが保存されているリポジトリ。</returns>
        public static ITaskRepository CreateTaskRepository()
        {
#if DEBUG
            return new TestRepository();
#else
            return new TaskRepository();
#endif
        }
    }
}
