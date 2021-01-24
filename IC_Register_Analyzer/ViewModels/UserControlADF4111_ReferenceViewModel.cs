using System.Globalization;
using System.Windows.Controls;
using Prism.Mvvm;
using Prism.Commands;
using IC_Register_Analyzer.Models;

namespace IC_Register_Analyzer.ViewModels
{
    /// <summary>
    /// ADF4111の解析画面(リファレンス・カウンタ・ラッチ)
    /// </summary>
    public class UserControlADF4111_ReferenceViewModel : BindableBase
    {
        /// <summary>
        /// バインディングデータ：解析データ
        /// </summary>
        private Model_Register_ADF4111_ReferenceCounter _registerData;
        public Model_Register_ADF4111_ReferenceCounter RegisterData
        {
            get { return _registerData; }
            set { SetProperty(ref _registerData, value); }
        }

        /// <summary>
        /// バインディングコマンド：16進数文字列変化
        /// </summary>
        private DelegateCommand _commandChangeHexString;
        public DelegateCommand CommandChangeHexString =>
            _commandChangeHexString ?? (_commandChangeHexString = new DelegateCommand(ExecuteCommandChangeHexString));

        /// <summary>
        /// バインディングコマンド：10進数文字列変化
        /// </summary>
        private DelegateCommand _commandChangeDecString;
        public DelegateCommand CommandChangeDecString =>
            _commandChangeDecString ?? (_commandChangeDecString = new DelegateCommand(ExecuteCommandChangeDecString));

        /// <summary>
        /// バインディングコマンド：2進数文字列変化
        /// </summary>
        private DelegateCommand _commandChangeBinString;
        public DelegateCommand CommandChangeBinString =>
            _commandChangeBinString ?? (_commandChangeBinString = new DelegateCommand(ExecuteCommandChangeBinString));

        /// <summary>
        /// バインディングコマンド：設定データ→設定数値変換
        /// </summary>
        private DelegateCommand _commandConvertSettingsToString;
        public DelegateCommand CommandConvertSettingsToString =>
            _commandConvertSettingsToString ?? (_commandConvertSettingsToString = new DelegateCommand(ExecuteCommandConvertSettingsToString));

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserControlADF4111_ReferenceViewModel()
        {
            //解析データ生成
            RegisterData = new Model_Register_ADF4111_ReferenceCounter();
        }

        /// <summary>
        /// 16進数文字列変化コマンド実行処理
        /// </summary>
        private void ExecuteCommandChangeHexString()
        {
            RegisterData.ConvertHexStringToOtherString();
            RegisterData.ConvertStringToSettings();
        }

        /// <summary>
        /// 10進数文字列変化コマンド実行処理
        /// </summary>
        private void ExecuteCommandChangeDecString()
        {
            RegisterData.ConvertDecStringToOtherString();
            RegisterData.ConvertStringToSettings();
        }

        /// <summary>
        /// 2進数文字列変化コマンド実行処理
        /// </summary>
        private void ExecuteCommandChangeBinString()
        {
            RegisterData.ConvertBinStringToOtherString();
            RegisterData.ConvertStringToSettings();
        }

        /// <summary>
        /// 設定データ→設定数値変換コマンド実行処理
        /// </summary>
        private void ExecuteCommandConvertSettingsToString()
        {
            RegisterData.ConvertSettingsToString();
        }
    }

    /// <summary>
    /// 14ビットリファレンス・カウンタ文字列入力値検証クラス
    /// </summary>
    class ADF4111_Reference_RefCounterValidationRules : ValidationRule
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

            if (!uint.TryParse(str, out uint ret))
            {
                return new ValidationResult(false, "値を入力してください。");
            }
            if (Model_Register_ADF4111_ReferenceCounter.R_Max_Threshold < ret)
            {
                return new ValidationResult(false, "範囲内の値を入力してください。(0～" + Model_Register_ADF4111_ReferenceCounter.R_Max_Threshold + ")");
            }

            // 上記のチェックにパスしたらOKを返す
            return ValidationResult.ValidResult;
        }
    }
}
