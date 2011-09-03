using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace SilverTask.Controls
{
    /// <summary>
    /// <see cref="Boolean"/> を <see cref="Visibility"/> に変換します。
    /// </summary>
    public class BooleanVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 否定かどうか示す値を取得または設定します。
        /// </summary>
        public bool Not { get; set; }

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
                if (Not)
                {
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                }
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
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
            if (value is Visibility)
            {
                if (Not)
                {
                    return (Visibility)value == Visibility.Collapsed;
                }
                return (Visibility)value == Visibility.Visible;
            }
            return value;
        }
    }
}
