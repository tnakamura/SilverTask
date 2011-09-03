namespace Core.Models
{
    [System.Runtime.Serialization.DataContract]
    public partial class Task
    {
        public Task() { OnCreated(); }
        [System.Runtime.Serialization.DataMember(Name="key")]
        public string Key { get;set; }
        [System.Runtime.Serialization.DataMember(Name="created")]
        public System.DateTime Created { get;set; }
        [System.Runtime.Serialization.DataMember(Name="done")]
        public bool Done { get;set; }
        [System.Runtime.Serialization.DataMember(Name="name")]
        public string Name { get;set; }
        [System.Runtime.Serialization.DataMember(Name="user")]
        public string User { get;set; }
        partial void OnCreated();
    }

    public static partial class TaskMeta
    {
        public static string Key { get { return "key"; } }
        public static string Created { get { return "created"; } }
        public static string Done { get { return "done"; } }
        public static string Name { get { return "name"; } }
        public static string User { get { return "user"; } }
    }
}

