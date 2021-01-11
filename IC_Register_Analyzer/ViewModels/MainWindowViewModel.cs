using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using Prism.Services.Dialogs;
using IC_Register_Analyzer.Views;
using IC_Register_Analyzer.Models;

namespace IC_Register_Analyzer.ViewModels
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 画面管理情報
        /// </summary>
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        /// <summary>
        /// バインディングデータ：画面タイトル
        /// </summary>
        private string _title = "IC Register Analyzer-メイン画面-";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// バインディングデータ：IC名
        /// </summary>
        private string _selectICName = "選択されていません。";
        public string SelectICName
        {
            get { return _selectICName; }
            set { SetProperty(ref _selectICName, value); }
        }

        /// <summary>
        /// バインディングコマンド：IC選択
        /// </summary>
        private DelegateCommand _commandShowSelectIC;
        public DelegateCommand CommandShowSelectIC =>
            _commandShowSelectIC ?? (_commandShowSelectIC = new DelegateCommand(ExecuteCommandShowSelectIC));

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="dialogService"></param>
        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            // 画面管理情報設定
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        /// <summary>
        /// IC選択コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowSelectIC()
        {
            // 終了コールバックを設定してIC選択画面を表示する
            _dialogService.Show(nameof(Views.UserControlSelectIC), null, CallbackCloseSelectICDialog);
        }

        /// <summary>
        /// IC選択画面終了時のコールバック処理
        /// </summary>
        /// <param name="dr"></param>
        private void CallbackCloseSelectICDialog(IDialogResult dr)
        {
            if (dr.Result == ButtonResult.OK)
            {
                SelectICName = dr.Parameters.GetValue<string>("Param1");

                // 選択されたICに合わせて解析画面を表示するコマンドを実行
                if (SelectICName == Model_ICList.ADF4111)
                {
                    ExecuteCommandShowADF4111();
                }
                else if(SelectICName == Model_ICList.R2A20178NP)
                {
                    ExecuteCommandShowR2A20178NP();
                }
                else
                {
                    ExecuteCommandShowNone();
                }
            }
        }

        /// <summary>
        /// 解析画面(IC未選択)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowNone()
        {
            _regionManager.RequestNavigate("MainWindowContentRegion", nameof(UserControlNone));
        }

        /// <summary>
        /// 解析画面(ADF4111)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowADF4111()
        {
            _regionManager.RequestNavigate("MainWindowContentRegion", nameof(UserControlADF4111));
        }

        /// <summary>
        /// 解析画面(R2A20178NP)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowR2A20178NP()
        {
            _regionManager.RequestNavigate("MainWindowContentRegion", nameof(UserControlR2A20178NP));
        }
    }
}
