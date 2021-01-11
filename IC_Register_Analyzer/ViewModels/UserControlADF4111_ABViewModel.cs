using Prism.Mvvm;
using Prism.Commands;
using IC_Register_Analyzer.Models;

namespace IC_Register_Analyzer.ViewModels
{
    /// <summary>
    /// ADF4111の解析画面(ABカウンタ・ラッチ)
    /// </summary>
    public class UserControlADF4111_ABViewModel : BindableBase
    {
        /// <summary>
        /// バインディングデータ：解析データ
        /// </summary>
        private Model_Register_ADF4111_ABCounter _registerData;
        public Model_Register_ADF4111_ABCounter RegisterData
        {
            get { return _registerData; }
            set { SetProperty(ref _registerData, value); }
        }

        /// <summary>
        /// バインディングデータ：変換結果
        /// </summary>
        private string _convResult;
        public string ConvResult
        {
            get { return _convResult; }
            set { SetProperty(ref _convResult, value); }
        }
        private static readonly string ConvResult_OK = "変換成功";
        private static readonly string ConvResult_NG = "変換失敗";
        private static readonly string ConvResult_NG_InvalidHexString = "変換失敗(文字列が16進数ではないか、32ビットを超えています。)";
        private static readonly string ConvResult_NG_InvalidDecString = "変換失敗(文字列が10進数ではないか、32ビットを超えています。)";
        private static readonly string ConvResult_NG_InvalidBinString = "変換失敗(文字列が2進数ではないか、32ビットを超えています。)";

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
        public UserControlADF4111_ABViewModel()
        {
            //解析データ生成
            RegisterData = new Model_Register_ADF4111_ABCounter();
        }

        /// <summary>
        /// 16進数文字列変化コマンド実行処理
        /// </summary>
        private void ExecuteCommandChangeHexString()
        {
            if (RegisterData.ConvertHexStringToOtherString() == true)
            {
                if (RegisterData.ConvertStringToSettings() == true)
                {
                    ConvResult = ConvResult_OK;
                }
                else
                {
                    ConvResult = ConvResult_NG;
                    RegisterData.ClearSettings();
                }
            }
            else
            {
                ConvResult = ConvResult_NG_InvalidHexString;
                RegisterData.ClearSettings();
            }
        }

        /// <summary>
        /// 10進数文字列変化コマンド実行処理
        /// </summary>
        private void ExecuteCommandChangeDecString()
        {
            if (RegisterData.ConvertDecStringToOtherString() == true)
            {
                if (RegisterData.ConvertStringToSettings() == true)
                {
                    ConvResult = ConvResult_OK;
                }
                else
                {
                    ConvResult = ConvResult_NG;
                    RegisterData.ClearSettings();
                }
            }
            else
            {
                ConvResult = ConvResult_NG_InvalidDecString;
                RegisterData.ClearSettings();
            }
        }

        /// <summary>
        /// 2進数文字列変化コマンド実行処理
        /// </summary>
        private void ExecuteCommandChangeBinString()
        {
            if (RegisterData.ConvertBinStringToOtherString() == true)
            {
                if (RegisterData.ConvertStringToSettings() == true)
                {
                    ConvResult = ConvResult_OK;
                }
                else
                {
                    ConvResult = ConvResult_NG;
                    RegisterData.ClearSettings();
                }
            }
            else
            {
                ConvResult = ConvResult_NG_InvalidBinString;
                RegisterData.ClearSettings();
            }
        }

        /// <summary>
        /// 設定データ→設定数値変換コマンド実行処理
        /// </summary>
        private void ExecuteCommandConvertSettingsToString()
        {
            if (RegisterData.ConvertSettingsToString() == true)
            {
                ConvResult = ConvResult_OK;
            }
            else
            {
                ConvResult = ConvResult_NG;
                RegisterData.ClearString();
            }
        }
    }
}
