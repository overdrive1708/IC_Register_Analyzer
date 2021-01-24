using Prism.Mvvm;
using Prism.Commands;
using IC_Register_Analyzer.Models;

namespace IC_Register_Analyzer.ViewModels
{
    /// <summary>
    /// ADF4111の解析画面(初期化ラッチ)
    /// </summary>
    public class UserControlADF4111_InitializeViewModel : BindableBase
    {
        /// <summary>
        /// バインディングデータ：解析データ
        /// </summary>
        private Model_Register_ADF4111_Initialize _registerData;
        public Model_Register_ADF4111_Initialize RegisterData
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
        public UserControlADF4111_InitializeViewModel()
        {
            //解析データ生成
            RegisterData = new Model_Register_ADF4111_Initialize();
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
}
