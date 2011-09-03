using System;
using System.Windows.Input;

namespace SilverTask.ViewModels
{
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// コマンドの起動時に呼び出されるメソッド。
        /// </summary>
        private readonly Action<object> execute;

        /// <summary>
        /// 現在の状態でこのコマンドを実行できるかどうかを判断するメソッド。
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// <see cref="DelegateCommand"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="execute">コマンドの起動時に呼び出されるメソッド。</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, _ => true)
        {
        }

        /// <summary>
        /// <see cref="DelegateCommand"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="execute">コマンドの起動時に呼び出されるメソッド。</param>
        /// <param name="canExecute">現在の状態でこのコマンドを実行できるかどうかを判断するメソッド。</param>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// コマンドの起動時に呼び出されるメソッドを定義します。
        /// </summary>
        /// <param name="param">コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを null に設定できます。</param>
        public void Execute(object param)
        {
            this.execute(param);
        }

        /// <summary>
        /// 現在の状態でこのコマンドを実行できるかどうかを判断するメソッドを定義します。
        /// </summary>
        /// <param name="param">コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを null に設定できます。</param>
        /// <returns>このコマンドを実行できる場合は true。それ以外の場合は false。</returns>
        public bool CanExecute(object param)
        {
            return this.canExecute(param);
        }

        /// <summary>
        /// コマンドを実行するかどうかに影響するような変更があった場合に発生します。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
