using System;
using System.Globalization;
using System.Windows.Controls;

namespace IC_Register_Analyzer.Utilities
{
    /// <summary>
    /// 文字列入力値検証クラス群
    /// </summary>
    class StringValidationRules : ValidationRule
    {
        /// <summary>
        /// ビット幅
        /// </summary>
        public int BitWidth { get; set; }

        /// <summary>
        /// 文字列基数
        /// </summary>
        public StringBases StringBase { get; set; }
        public enum StringBases
        {
            Binary,
            Decimal,
            Hexadecimal
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StringValidationRules()
        {
            BitWidth = 0;
            StringBase = StringBases.Binary;
        }

        /// <summary>
        /// 入力値検証処理
        /// </summary>
        /// <param name="value">入力値</param>
        /// <param name="cultureInfo">カルチャー情報</param>
        /// <returns>入力値検証結果</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int max = 0;
            int convertBase;

            // ビット幅から最大値を算出
            for (int cnt = 0; cnt < BitWidth; cnt++)
            {
                max <<= 1;
                max |= 1;
            }

            // 32bit整数変換用基数判定
            switch (StringBase)
            {
                case StringBases.Binary:
                    convertBase = 2;
                    break;
                case StringBases.Decimal:
                    convertBase = 10;
                    break;
                case StringBases.Hexadecimal:
                    convertBase = 16;
                    break;
                default:
                    convertBase = 10;
                    break;
            }

            // 入力値がNULLの場合はNGを返す
            if (null == value)
            {
                return new ValidationResult(false, "値を入力してください。");
            }

            // 入力値の文字列が空の場合はNGを返す
            string str = value.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return new ValidationResult(false, "値を入力してください。");
            }

            try
            {
                // 入力値が指定されたビット幅に収まらない場合はNGを返す
                if (Convert.ToUInt32(value.ToString(), convertBase) > max)
                {
                    return new ValidationResult(false, "値が" + BitWidth.ToString() + "bitの範囲を超えています。");
                }
            }
            catch
            {
                // 32bit整数変換に失敗する場合はNGを返す
                return new ValidationResult(false, "値が" + BitWidth.ToString() + "bitの範囲を超えているか、" + convertBase.ToString() + "進法ではありません。");
            }

            // 上記のチェックにパスしたらOKを返す
            return ValidationResult.ValidResult;
        }
    }
}
