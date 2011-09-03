using System;
using Core.Models;

namespace SilverTask.Models
{
    public interface ITaskRepository
    {
        /// <summary>
        /// タスクを取得します。
        /// </summary>
        /// <param name="completed">完了済みを取得するとき true。未完了を取得するとき false。</param>
        /// <param name="callback">コールバック。</param>
        void ReadTask(bool completed, Action<TaskResult> callback);

        /// <summary>
        /// タスクを作成します。
        /// </summary>
        /// <param name="task">作成するタスク。</param>
        /// <param name="callback">コールバック。</param>
        void CreateTask(Task task, Action<TaskResult> callback);

        /// <summary>
        /// タスクを削除します。
        /// </summary>
        /// <param name="tasks">削除するタスク。</param>
        /// <param name="callback">コールバック。</param>
        void DeleteTask(Task tasks, Action<TaskResult> callback);

        /// <summary>
        /// タスクを更新します。
        /// </summary>
        /// <param name="tasks">更新するタスク。</param>
        /// <param name="callback">コールバック。</param>
        void UpdateTask(Task tasks, Action<TaskResult> callback);
    }

    /// <summary>
    /// リポジトリ操作の結果を格納します。
    /// </summary>
    public class TaskResult
    {
        /// <summary>
        /// タスクの一覧を取得します。
        /// </summary>
        public Task[] Tasks { get; private set; }

        /// <summary>
        /// キャンセルされたかどうか示す値を取得します。
        /// </summary>
        public bool Cancelled { get; private set; }

        /// <summary>
        /// 発生したエラーを取得します。
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// <see cref="TaskResult"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="tasks">タスク。</param>
        /// <param name="cancelled">キャンセルされたかどうか。</param>
        /// <param name="error">発生したエラー。</param>
        public TaskResult(Task[] tasks, bool cancelled = false, Exception error = null)
        {
            this.Tasks = tasks;
            this.Cancelled = cancelled;
            this.Error = error;
        }
    }
}
