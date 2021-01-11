using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Services.Dialogs;
using IC_Register_Analyzer.Models;

namespace IC_Register_Analyzer.ViewModels
{
    /// <summary>
    /// IC選択画面
    /// </summary>
    public class UserControlSelectICViewModel : BindableBase, IDialogAware
    {
        /// <summary>
        /// バインディングデータ：ICリスト
        /// </summary>
        private ObservableCollection<Model_ICList> _icList;
        public ObservableCollection<Model_ICList> ICList
        {
            get { return _icList; }
            set { SetProperty(ref _icList, value); }
        }

        /// <summary>
        /// バインディングデータ：選択中IC
        /// </summary>
        private Model_ICList _selectedIC;
        public Model_ICList SelectedIC
        {
            get { return _selectedIC; }
            set { SetProperty(ref _selectedIC, value); }
        }

        /// <summary>
        /// バインディングコマンド：IC選択
        /// </summary>
        private DelegateCommand _commandSelectIC;
        public DelegateCommand CommandSelectIC =>
            _commandSelectIC ?? (_commandSelectIC = new DelegateCommand(ExecuteCommandSelectIC));

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserControlSelectICViewModel()
        {
            // ICリスト生成
            ICList = new ObservableCollection<Model_ICList>()
            {
                new Model_ICList { Name = Model_ICList.ADF4111, Category = "PLL", Abstract = "RF PLL周波数シンセサイザ", Maker = "アナログ・デバイセズ"},
                new Model_ICList { Name = Model_ICList.R2A20178NP, Category = "DAC", Abstract = "8ビット8ch 5V系低消費乗算型D/Aコンバータ(バッファ有り)", Maker = "ルネサスエレクトロニクス"}
            };
        }

        /// <summary>
        /// IC選択コマンド実行処理
        /// </summary>
        private void ExecuteCommandSelectIC()
        {
            // 選択中ICのIC名をReturnパラメータにして閉じる
            DialogParameters param = new DialogParameters
            {
                { "Param1", SelectedIC.Name }
            };
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, param));
        }

        #region IDialogAwareの実装
        /// <summary>
        /// 画面タイトル
        /// </summary>
        public string Title => "IC Register Analyzer-IC選択画面-";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;    // 画面を閉じることができる
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
        #endregion
    }
}
