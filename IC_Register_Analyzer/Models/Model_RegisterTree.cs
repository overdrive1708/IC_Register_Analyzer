using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IC_Register_Analyzer.Models
{
    /// <summary>
    /// レジスタツリーモデル
    /// </summary>
    public class Model_RegisterTree : ObservableCollection<Model_RegisterTree>
    {
        /// <summary>
        /// レジスタ名もしくはカテゴリ名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子要素
        /// </summary>
        public List<Model_RegisterTree> Child { get; set; }
    }
}
