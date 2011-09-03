using System;
using System.Json;

namespace Core.Models
{
    /// <summary>
    /// タスクを表します。
    /// </summary>
    public partial class Task
    {
        partial void OnCreated()
        {
            Created = DateTime.Now;
        }

        /// <summary>
        /// <see cref="Task"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="json">初期化に使う JSON オブジェクト。</param>
        public Task(JsonValue json)
            : this()
        {
            this.Key = json[TaskMeta.Key];
            this.User = json[TaskMeta.User];
            this.Name = json[TaskMeta.Name];
            this.Done = json[TaskMeta.Done];
            DateTime created;
            if (DateTime.TryParse(json[TaskMeta.Done].ToString(), out created))
            {
                this.Created = created;
            }
        }

        /// <summary>
        /// 現在のインスタンスのクローンを作成します。
        /// </summary>
        /// <returns>現在のインスタンスのクローン。</returns>
        public Task Clone()
        {
            return new Task()
            {
                Key = this.Key,
                Name = this.Name,
                Done = this.Done,
                Created = this.Created,
                User = this.User,
            };
        }

        /// <summary>
        /// JSON 文字列に変換します。
        /// </summary>
        /// <returns>JSON 文字列。</returns>
        public string ToJsonString()
        {
            var obj = new JsonObject(DateTimeOffset.Now);
            obj.Add(TaskMeta.Name, (JsonValue)Name);
            obj.Add(TaskMeta.Done, (JsonValue)Done);
            return obj.ToString();
        }
    }
}
