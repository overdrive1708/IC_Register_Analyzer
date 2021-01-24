using System.Globalization;
using System.Windows.Controls;
using Prism.Mvvm;
using Prism.Commands;
using IC_Register_Analyzer.Models;

namespace IC_Register_Analyzer.ViewModels
{
    /// <summary>
    /// R2A20178NPの解析画面
    /// </summary>
    public class UserControlR2A20178NPViewModel : BindableBase
    {
        /// <summary>
        /// バインディングデータ：解析データ
        /// </summary>
        private Model_Register_R2A20178NP _registerData;
        public Model_Register_R2A20178NP RegisterData
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
        public UserControlR2A20178NPViewModel()
        {
            // 解析データ生成
            RegisterData = new Model_Register_R2A20178NP();
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
    /// DACデータ文字列入力値検証クラス
    /// </summary>
    class DacDataValidationRules : ValidationRule
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
            if ((0 > ret) || (255 < ret))
            {
                return new ValidationResult(false, "範囲内の値を入力してください。(0～255)");
            }

            // 上記のチェックにパスしたらOKを返す
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// DACセレクトデータ入力値検証クラス
    /// </summary>
    class DacSelectDataValidationRules : ValidationRule
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
            if ((value.ToString().Equals(Model_Register_R2A20178NP.DACSelectData_Null)) == true)
            {
                return new ValidationResult(false, "有効値を選択してください。(VOUT1選択～VOUT8選択)");
            }

            // 入力値が「Don't care」の場合はNGを返す
            if ((value.ToString().Equals(Model_Register_R2A20178NP.DACSelectData_DoNotCare)) == true)
            {
                return new ValidationResult(false, "選択してください。");
            }

            // 上記のチェックにパスしたらOKを返す
            return ValidationResult.ValidResult;
        }
    }
}
