using System.Globalization;
using System.Windows.Controls;

namespace IC_Register_Analyzer.Utilities
{
    /// <summary>
    /// コンボボックス入力値検証クラス群
    /// </summary>
    class ComboBoxValidationRules : ValidationRule
    {
        /// <summary>
        /// 入力値検証処理
        /// </summary>
        /// <param name="value">入力値</param>
        /// <param name="cultureInfo">カルチャー情報</param>
        /// <returns>入力値検証結果</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 入力値がNULLの場合はNGを返す
            if (value.ToString() == string.Empty)
            {
                return new ValidationResult(false, "有効値を選択してください。");
            }

            // 上記のチェックにパスしたらOKを返す
            return ValidationResult.ValidResult;
        }
    }
}
