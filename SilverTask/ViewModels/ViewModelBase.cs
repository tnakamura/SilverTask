using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace SilverTask.ViewModels
{
    /// <summary>
    /// ビューモデルの基底クラス。
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// <see cref="ViewModelBase"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected ViewModelBase()
        {
        }

        /// <summary>
        /// プロパティが変更されたときに発生します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <see cref="PropertyChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

            // HACK: Button の状態を変更する
            CommandManager.FireRequerySuggested();
        }

        /// <summary>
        /// 検証エラーを格納します。
        /// </summary>
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        /// <summary>
        /// オブジェクトに検証エラーがあるかどうかを示す値を取得します。
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return 0 < _errors.Count;
            }
        }

        /// <summary>
        /// プロパティまたはオブジェクト全体の検証エラーが変更されたときに発生します。
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// 指定されたプロパティまたはオブジェクト全体の検証エラーを取得します。
        /// </summary>
        /// <param name="propertyName">
        /// 検証エラーを取得する対象のプロパティの名前。オブジェクト全体のエラーを取得する場合は null
        /// または System.String.Empty。
        /// </param>
        /// <returns>プロパティまたはオブジェクトの検証エラー</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.Values;
        }

        /// <summary>
        /// プロパティに設定する値を検証します。
        /// </summary>
        /// <typeparam name="T">プロパティの型。</typeparam>
        /// <param name="propertyName">プロパティ名。</param>
        /// <param name="value">設定する値。</param>
        protected void Validate<T>(string propertyName, T value)
        {
            try
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                {
                    MemberName = propertyName,
                });

                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                    OnErrorsChanged(propertyName);
                }
            }
            catch (ValidationException ex)
            {
                _errors[propertyName] = ex.Message;
                OnErrorsChanged(propertyName);
                throw;
            }
        }

        /// <summary>
        /// <see cref="ErrorsChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        private void OnErrorsChanged(string propertyName)
        {
            var h = ErrorsChanged;
            if (h != null)
            {
                h(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
