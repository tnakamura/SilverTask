using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace SilverTask.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        /// <summary>
        /// タスク
        /// </summary>
        private readonly Task model;

        public TaskViewModel()
            : this(new Task())
        {
            IsNew = true;
        }

        public TaskViewModel(Task model)
        {
            this.model = model;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "タスクの名前を記入して下さい。")]
        public string Name
        {
            get { return this.model.Name; }
            set
            {
                this.Validate("Name", value);
                if (this.model.Name != value)
                {
                    this.model.Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// 完了かどうか示す値を取得または設定します。
        /// </summary>
        public bool Done
        {
            get { return this.model.Done; }
            set
            {
                if (this.model.Done != value)
                {
                    this.model.Done = value;
                    this.OnPropertyChanged("Done");
                }
            }
        }

        /// <summary>
        /// 編集中かどうか
        /// </summary>
        private bool _isEditing = false;

        /// <summary>
        /// 編集中かどうか示す値を取得または設定します。
        /// </summary>
        public bool IsEditing
        {
            get { return this._isEditing; }
            set
            {
                if (this._isEditing != value)
                {
                    this._isEditing = value;
                    this.OnPropertyChanged("IsEditing");
                }
            }
        }

        /// <summary>
        /// 新規タスクかどうか
        /// </summary>
        private bool _isNew = false;

        /// <summary>
        /// 新規タスクかどうか示す値を取得または設定します。
        /// </summary>
        public bool IsNew
        {
            get { return this._isNew; }
            set
            {
                if (this._isNew != value)
                {
                    this._isNew = value;
                    this.OnPropertyChanged("IsNew");
                }
            }
        }

        public Task Unwrap()
        {
            return this.model;
        }
    }
}
