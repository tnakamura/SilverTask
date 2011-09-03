using System;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Browser;
using Core.Models;

namespace SilverTask.Models
{
    public partial class TaskRepository : ITaskRepository
    {
        /// <summary>
        /// サービスのベースアドレス。
        /// </summary>
        private static readonly string _BaseAddress;

        static TaskRepository()
        {
            _BaseAddress = HtmlPage.Document.DocumentUri.ToString();
        }

        void ITaskRepository.ReadTask(bool completed, Action<TaskResult> callback)
        {
            string query = _BaseAddress + string.Format(
                "tasks?{0}={1}",
                TaskMeta.Done,
                completed);

            var client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                if (null != e.Error)
                {
                    callback(new TaskResult(new Task[0], e.Cancelled, e.Error));
                }
                else
                {
                    var tasks = from obj in (JsonArray)JsonArray.Parse(e.Result)
                                select new Task(obj);
                    callback(new TaskResult(tasks.ToArray()));
                }
            };
            client.DownloadStringAsync(new Uri(query));
        }

        void ITaskRepository.CreateTask(Task task, Action<TaskResult> callback)
        {
            string query = _BaseAddress + "tasks";
            string parameters = string.Format(
                "{0}={1}",
                TaskMeta.Name,
                HttpUtility.UrlEncode(task.Name));

            var client = new WebClient();
            client.UploadStringCompleted += (sender, e) =>
            {
                if (null != e.Error)
                {
                    callback(new TaskResult(new Task[0], e.Cancelled, e.Error));
                }
                else
                {
                    var result = new Task(JsonObject.Parse(e.Result));
                    callback(new TaskResult(new Task[] { result }));
                }
            };
            client.UploadStringAsync(new Uri(query), parameters);
        }

        void ITaskRepository.DeleteTask(Task task, Action<TaskResult> callback)
        {
            string uri = _BaseAddress + "tasks/" + task.Key;
            var request = WebRequest.CreateHttp(uri);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            request.BeginGetResponse(result =>
            {
                var response = (HttpWebResponse)request.EndGetResponse(result);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = new WebException(response.StatusDescription);
                    callback(new TaskResult(new Task[0], error: error));
                }
                else
                {
                    callback(new TaskResult(new Task[] { task.Clone() }));
                }
            }, null);
        }

        void ITaskRepository.UpdateTask(Task task, Action<TaskResult> callback)
        {
            string uri = _BaseAddress + "tasks/" + task.Key;
            byte[] bytes = Encoding.UTF8.GetBytes(task.ToJsonString());

            var request = WebRequest.CreateHttp(uri);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;
            request.BeginGetRequestStream(result =>
            {
                using (var stream = request.EndGetRequestStream(result))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                request.BeginGetResponse(result2 =>
                {
                    var response = (HttpWebResponse)request.EndGetResponse(result2);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var error = new WebException(response.StatusDescription);
                        callback(new TaskResult(new Task[0], error: error));
                    }
                    else
                    {
                        callback(new TaskResult(new Task[] { task.Clone() }));
                    }
                }, null);
            }, null);
        }
    }
}
