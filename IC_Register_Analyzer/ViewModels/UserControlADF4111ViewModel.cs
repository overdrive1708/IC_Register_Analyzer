using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using IC_Register_Analyzer.Views;
using IC_Register_Analyzer.Models;

namespace IC_Register_Analyzer.ViewModels
{
    /// <summary>
    /// ADF4111の解析画面
    /// </summary>
    public class UserControlADF4111ViewModel : BindableBase
    {
        /// <summary>
        /// 画面管理情報
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// レジスタ名定数(判定のため定数化)
        /// </summary>
        private static readonly string registerReference = "リファレンス・カウンタ・ラッチ";
        private static readonly string registerAB = "ABカウンタ・ラッチ";
        private static readonly string registerFunction = "ファンクション・ラッチ";
        private static readonly string registerInitialize = "初期化ラッチ";

        /// <summary>
        /// バインディングデータ：レジスタツリー
        /// </summary>
        private ObservableCollection<Model_RegisterTree> _registerTree;
        public ObservableCollection<Model_RegisterTree> RegisterTree
        {
            get { return _registerTree; }
            set { SetProperty(ref _registerTree, value); }
        }

        /// <summary>
        /// バインディングコマンド：レジスタツリー選択項目変化
        /// </summary>
        private DelegateCommand<object> _commandSelectedRegisterChanged;
        public DelegateCommand<object> CommandSelectedRegisterChanged =>
            _commandSelectedRegisterChanged ?? (_commandSelectedRegisterChanged = new DelegateCommand<object>(ExecuteCommandSelectedRegisterChanged, CanExecuteCommandSelectedRegisterChanged));

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserControlADF4111ViewModel(IRegionManager regionManager)
        {
            // 画面管理情報設定
            _regionManager = regionManager;

            // レジスタツリー生成
            RegisterTree = new ObservableCollection<Model_RegisterTree>()
            {
                new Model_RegisterTree()
                {
                    Name = registerReference
                },
                new Model_RegisterTree()
                {
                    Name = registerAB
                },
                new Model_RegisterTree()
                {
                    Name = registerFunction
                },
                new Model_RegisterTree()
                {
                    Name = registerInitialize
                }
            };
        }

        /// <summary>
        /// レジスタツリー選択項目変化コマンド実行処理
        /// </summary>
        /// <param name="newValue">選択された情報を表すRegisterTreeModel</param>
        private void ExecuteCommandSelectedRegisterChanged(object newValue)
        {
            // 選択されたレジスタに合わせて解析画面を表示するコマンドを実行
            Model_RegisterTree selectdata = (Model_RegisterTree)newValue;
            if(selectdata.Name == registerReference)
            {
                ExecuteCommandShowADF4111Reference();
            }
            else if(selectdata.Name == registerAB)
            {
                ExecuteCommandShowADF4111AB();
            }
            else if(selectdata.Name == registerFunction)
            {
                ExecuteCommandShowADF4111Function();
            }
            else if(selectdata.Name == registerInitialize)
            {
                ExecuteCommandShowADF4111Initialize();
            }
            else
            {
                ExecuteCommandShowADF4111None();
            }
        }

        /// <summary>
        /// TreeViewのSelectedItemChangedイベントの実行可否の取得
        /// </summary>
        /// <param name="newValue">選択された情報を表すRegisterTreeModel</param>
        /// <returns>SelectedItemChangedの実行可否を表すbool</returns>
        private bool CanExecuteCommandSelectedRegisterChanged(object newValue)
        {
            return true;
        }

        /// <summary>
        /// 解析画面(レジスタ未選択)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowADF4111None()
        {
            _regionManager.RequestNavigate("ADF4111ContentRegion", nameof(UserControlADF4111_None));
        }

        /// <summary>
        /// 解析画面(リファレンス・カウンタ・ラッチ)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowADF4111Reference()
        {
            _regionManager.RequestNavigate("ADF4111ContentRegion", nameof(UserControlADF4111_Reference));
        }

        /// <summary>
        /// 解析画面(ABカウンタ・ラッチ)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowADF4111AB()
        {
            _regionManager.RequestNavigate("ADF4111ContentRegion", nameof(UserControlADF4111_AB));
        }

        /// <summary>
        /// 解析画面(ファンクション・ラッチ)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowADF4111Function()
        {
            _regionManager.RequestNavigate("ADF4111ContentRegion", nameof(UserControlADF4111_Function));
        }

        /// <summary>
        /// 解析画面(初期化ラッチ)表示コマンド実行処理
        /// </summary>
        private void ExecuteCommandShowADF4111Initialize()
        {
            _regionManager.RequestNavigate("ADF4111ContentRegion", nameof(UserControlADF4111_Initialize));
        }
    }
}
