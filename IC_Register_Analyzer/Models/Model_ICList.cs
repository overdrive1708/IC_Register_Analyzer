namespace IC_Register_Analyzer.Models
{
    /// <summary>
    /// ICリストモデル
    /// </summary>
    public class Model_ICList
    {
        /// <summary>
        /// IC名定数(判定のため定数化)
        /// </summary>
        public static readonly string ADF4111 = "ADF4111";
        public static readonly string R2A20178NP = "R2A20178NP";

        /// <summary>
        /// IC名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// カテゴリ
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// メーカー
        /// </summary>
        public string Maker { get; set; }
    }
}
