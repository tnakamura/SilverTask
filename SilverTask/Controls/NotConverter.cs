using System;
using System.Windows.Data;
using System.Globalization;

namespace SilverTask.Controls
{
    /// <summary>
    /// <see cref="Boolean"/> の否定の値に変換します。
    /// </summary>
    public class NotConverter : IValueConverter
    {
        /// <summary>
        /// UI 表示のためにターゲットに渡す前にソース データを変更します。
        /// </summary>
        /// <param name="value">ターゲットに渡すソース データ。</param>
        /// <param name="targetType">ターゲット依存関係プロパティで期待されるデータの <see cref="Type"/>。</param>
        /// <param name="parameter">コンバーター ロジックで使用する省略可能なパラメーター。</param>
        /// <param name="culture">変換のカルチャ。</param>
        /// <returns>ターゲット依存関係プロパティに渡す値。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        /// <summary>
        /// ソース オブジェクトに渡す前にターゲット データを変更します。このメソッドが呼び出されるのは、<see cref="BindingMode.TwoWay"/>
        /// バインディングの場合だけです。
        /// </summary>
        /// <param name="value">ソースに渡すターゲット データ。</param>
        /// <param name="targetType">ソース オブジェクトで期待されるデータの <see cref="Type"/>。</param>
        /// <param name="parameter">コンバーター ロジックで使用する省略可能なパラメーター。</param>
        /// <param name="culture">変換のカルチャ。</param>
        /// <returns>ソース オブジェクトに渡す値。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }
    }
}
