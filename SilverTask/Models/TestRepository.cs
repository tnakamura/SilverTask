using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Models;

namespace SilverTask.Models
{
    public partial class TestRepository : ITaskRepository
    {
        private static readonly List<Task> Tasks = new List<Task>();
        private const int TASK_COUNT = 100;

        static TestRepository()
        {
            var r = new Random();
            for (int i = 0; i < TASK_COUNT; i++)
            {
                var task = new Task()
                {
                    Key = i.ToString(),
                    Name = string.Format("Task{0}", i),
                    Created = DateTime.Now,
                };
                Tasks.Add(task);
            }
        }

        public void ReadTask(bool completed, Action<TaskResult> callback)
        {
            ThreadStart(method: () =>
            {
                var tasks = from task in TestRepository.Tasks
                            where task.Done == completed
                            orderby task.Created descending
                            select task;
                if (callback != null)
                {
                    callback(new TaskResult(tasks.Select(t => t.Clone()).ToArray()));
                }
            });
        }

        public void CreateTask(Task task, Action<TaskResult> callback)
        {
            ThreadStart(method: () =>
            {
                var clone = task.Clone();
                Tasks.Add(clone);
                callback(new TaskResult(new Task[] { clone }));
            });
        }

        public void DeleteTask(Task task, Action<TaskResult> callback)
        {
            ThreadStart(method: () =>
            {
                var target = TestRepository.Tasks.FirstOrDefault(t => t.Key == task.Key);
                if (target != null)
                {
                    TestRepository.Tasks.Remove(target);
                }
                callback(new TaskResult(new Task[] { target.Clone() }));
            });
        }

        public void UpdateTask(Task task, Action<TaskResult> callback)
        {
            ThreadStart(method: () =>
            {
                var target = TestRepository.Tasks.FirstOrDefault(t => t.Key == task.Key);
                if (target != null)
                {
                    target.Name = task.Name;
                    target.Done = task.Done;
                    target.Created = task.Created;
                }
                callback(new TaskResult(new Task[] { task }));
            });
        }

        private static Thread ThreadStart(string name = null, bool isBackground = false, Action method = null)
        {
            Thread thread = new Thread((ThreadStart)delegate
            {
                if (method != null)
                {
                    method();
                }
            });
            thread.Name = name ?? thread.Name;
            thread.IsBackground = isBackground;
            thread.Start();
            return thread;
        }

    }
}
