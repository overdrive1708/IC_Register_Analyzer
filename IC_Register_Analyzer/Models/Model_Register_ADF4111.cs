using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace IC_Register_Analyzer.Models
{
    /// <summary>
    /// レジスタモデル(ADF4111)全レジスタ共通
    /// </summary>
    public class Model_Register_ADF4111 : BindableBase
    {
        /// <summary>
        /// 有効ビットマスク(24bit)
        /// </summary>
        private static readonly uint ValidBitMask = 0b111111111111111111111111;

        /// <summary>
        /// 16進数文字列
        /// </summary>
        private string _hexString;
        public string HexString
        {
            get { return _hexString; }
            set { SetProperty(ref _hexString, value); }
        }
        private static readonly string HexString_Init = "000000";
        protected static readonly int HexString_MaxDigit = 6;

        /// <summary>
        /// 10進数文字列
        /// </summary>
        private string _decString;
        public string DecString
        {
            get { return _decString; }
            set { SetProperty(ref _decString, value); }
        }
        private static readonly string DecString_Init = "0";

        /// <summary>
        /// 2進数文字列
        /// </summary>
        private string _binString;
        public string BinString
        {
            get { return _binString; }
            set { SetProperty(ref _binString, value); }
        }
        private static readonly string BinString_Init = "000000000000000000000000";
        protected static readonly int BinString_MaxDigit = 24;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model_Register_ADF4111()
        {
            HexString = HexString_Init;
            DecString = DecString_Init;
            BinString = BinString_Init;
        }

        /// <summary>
        /// 設定数値クリア処理
        /// </summary>
        public void ClearString()
        {
            HexString = string.Empty;
            DecString = string.Empty;
            BinString = string.Empty;
        }

        /// <summary>
        /// 16進数文字列→その他文字列変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertHexStringToOtherString()
        {
            bool ret;
            uint workdata;  // 24bitレジスタのため、32bit用意する。

            try
            {
                // 入力値が24bitを超えた場合は24bitMAXでクランプ
                if (Convert.ToUInt32(HexString, 16) <= ValidBitMask)
                {
                    workdata = Convert.ToUInt32(HexString, 16);
                }
                else
                {
                    workdata = ValidBitMask;
                    HexString = Convert.ToString(ValidBitMask, 16);
                    HexString = HexString.PadLeft(HexString_MaxDigit, '0');
                }

                // 基数変換
                DecString = Convert.ToString(workdata, 10);
                BinString = Convert.ToString(workdata, 2);
                BinString = BinString.PadLeft(BinString_MaxDigit, '0');
                
                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }

        /// <summary>
        /// 10進数文字列→その他文字列変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertDecStringToOtherString()
        {
            bool ret;
            uint workdata;  // 24bitレジスタのため、32bit用意する。

            try
            {
                // 入力値が24bitを超えた場合は24bitMAXでクランプ
                if (Convert.ToUInt32(DecString, 10) <= ValidBitMask)
                {
                    workdata = Convert.ToUInt32(DecString, 10);
                }
                else
                {
                    workdata = ValidBitMask;
                    DecString = Convert.ToString(workdata, 10);
                }

                // 基数変換
                HexString = Convert.ToString(workdata, 16);
                HexString = HexString.PadLeft(HexString_MaxDigit, '0');
                HexString = HexString.ToUpper();
                BinString = Convert.ToString(workdata, 2);
                BinString = BinString.PadLeft(BinString_MaxDigit, '0');

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }

        /// <summary>
        /// 2進数文字列→その他文字列変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertBinStringToOtherString()
        {
            bool ret;
            uint workdata;  // 24bitレジスタのため、32bit用意する。

            try
            {
                // 入力値が24bitを超えた場合は24bitMAXでクランプ
                if (Convert.ToUInt32(BinString, 2) <= ValidBitMask)
                {
                    workdata = Convert.ToUInt32(BinString, 2);
                }
                else
                {
                    workdata = ValidBitMask;
                    BinString = Convert.ToString(workdata, 2);
                    BinString = BinString.PadLeft(BinString_MaxDigit, '0');
                }

                // 基数変換
                HexString = Convert.ToString(workdata, 16);
                HexString = HexString.PadLeft(HexString_MaxDigit, '0');
                HexString = HexString.ToUpper();
                DecString = Convert.ToString(workdata, 10);

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }
    }

    /// <summary>
    /// レジスタモデル(ADF4111)リファレンス・カウンタ・ラッチ
    /// </summary>
    public class Model_Register_ADF4111_ReferenceCounter : Model_Register_ADF4111
    {
        /// <summary>
        /// ビットマスク
        /// </summary>
        private static readonly uint BitMask_DLYSYNC = 0b011000000000000000000000;
        private static readonly uint BitMask_LDP = 0b000100000000000000000000;
        private static readonly uint BitMask_T = 0b000011000000000000000000;
        private static readonly uint BitMask_ABP = 0b000000110000000000000000;
        private static readonly uint BitMask_R = 0b000000001111111111111100;
        private static readonly uint BitMask_C = 0b000000000000000000000011;

        /// <summary>
        /// DLY/SYNC
        /// </summary>
        private string _dataDLYSYNC;
        public string DLYSYNC
        {
            get { return _dataDLYSYNC; }
            set { SetProperty(ref _dataDLYSYNC, value); }
        }
        private static readonly string DLYSYNC_Null = string.Empty;
        private static readonly string DLYSYNC_Normal1 = "通常動作(00)";
        private static readonly string DLYSYNC_NonDlySync = "プリスケーラの出力はRF入力の遅延をともわずに再同期化";
        private static readonly string DLYSYNC_Normal2 = "通常動作(10)";
        private static readonly string DLYSYNC_DlySync = "プリスケーラの出力はRF入力の遅延をともなって再同期化";
        private static readonly uint DLYSYNC_Bit_Normal1 = 0b000000000000000000000000;
        private static readonly uint DLYSYNC_Bit_NonDlySync = 0b001000000000000000000000;
        private static readonly uint DLYSYNC_Bit_Normal2 = 0b010000000000000000000000;
        private static readonly uint DLYSYNC_Bit_DlySync = 0b011000000000000000000000;

        /// <summary>
        /// LDP
        /// </summary>
        private string _dataLDP;
        public string LDP
        {
            get { return _dataLDP; }
            set { SetProperty(ref _dataLDP, value); }
        }
        private static readonly string LDP_Null = string.Empty;
        private static readonly string LDP_3Cycle = "ロック検出がセットされる前に3つの連続したサイクルにわたり位相遅延が15ns未満であることが必要";
        private static readonly string LDP_5Cycle = "ロック検出がセットされる前に5つの連続したサイクルにわたり位相遅延が15ns未満であることが必要";
        private static readonly uint LDP_Bit_3Cycle = 0b000000000000000000000000;
        private static readonly uint LDP_Bit_5Cycle = 0b000100000000000000000000;

        /// <summary>
        /// T
        /// </summary>
        private string _dataT;
        public string T
        {
            get { return _dataT; }
            set { SetProperty(ref _dataT, value); }
        }
        private static readonly string T_Null = string.Empty;
        private static readonly string T_Normal = "通常動作";
        private static readonly uint T_Bit_Normal = 0b000000000000000000000000;

        /// <summary>
        /// ABP
        /// </summary>
        private string _dataABP;
        public string ABP
        {
            get { return _dataABP; }
            set { SetProperty(ref _dataABP, value); }
        }
        private static readonly string ABP_Null = string.Empty;
        private static readonly string ABP_3_0_NS1 = "3.0ns(00)";
        private static readonly string ABP_1_5_NS = "1.5ns";
        private static readonly string ABP_6_0_NS = "6.0ns";
        private static readonly string ABP_3_0_NS2 = "3.0ns(11)";
        private static readonly uint ABP_Bit_3_0_NS1 = 0b000000000000000000000000;
        private static readonly uint ABP_Bit_1_5_NS = 0b000000010000000000000000;
        private static readonly uint ABP_Bit_6_0_NS = 0b000000100000000000000000;
        private static readonly uint ABP_Bit_3_0_NS2 = 0b000000110000000000000000;

        /// <summary>
        /// R
        /// </summary>
        private string _dataR;
        public string R
        {
            get { return _dataR; }
            set { SetProperty(ref _dataR, value); }
        }
        private static readonly string R_Null = string.Empty;
        private static readonly int R_Bit_Shift = 2;
        private static readonly uint R_Max_Threshold = 16383;

        /// <summary>
        /// C
        /// </summary>
        private string _dataC;
        public string C
        {
            get { return _dataC; }
            set { SetProperty(ref _dataC, value); }
        }
        private static readonly string C_Null = string.Empty;
        private static readonly string C_R_Counter = "Rカウンタ";
        private static readonly uint C_Bit_R_Counter = 0b000000000000000000000000;

        /// <summary>
        /// バインディングデータ：解析データ：DLY/SYNCのリスト
        /// </summary>
        private ObservableCollection<string> _listDLYSYNC;
        public ObservableCollection<string> ListDLYSYNC
        {
            get { return _listDLYSYNC; }
            set { SetProperty(ref _listDLYSYNC, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：LDPのリスト
        /// </summary>
        private ObservableCollection<string> _listLDP;
        public ObservableCollection<string> ListLDP
        {
            get { return _listLDP; }
            set { SetProperty(ref _listLDP, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：Tのリスト
        /// </summary>
        private ObservableCollection<string> _listT;
        public ObservableCollection<string> ListT
        {
            get { return _listT; }
            set { SetProperty(ref _listT, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：ABPのリスト
        /// </summary>
        private ObservableCollection<string> _listABP;
        public ObservableCollection<string> ListABP
        {
            get { return _listABP; }
            set { SetProperty(ref _listABP, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：Cのリスト
        /// </summary>
        private ObservableCollection<string> _listC;
        public ObservableCollection<string> ListC
        {
            get { return _listC; }
            set { SetProperty(ref _listC, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model_Register_ADF4111_ReferenceCounter()
        {
            ListDLYSYNC = new ObservableCollection<string>()
            {
                DLYSYNC_Null,
                DLYSYNC_Normal1,
                DLYSYNC_NonDlySync,
                DLYSYNC_Normal2,
                DLYSYNC_DlySync
            };

            ListLDP = new ObservableCollection<string>()
            {
                LDP_Null,
                LDP_3Cycle,
                LDP_5Cycle
            };

            ListT = new ObservableCollection<string>()
            {
                T_Null,
                T_Normal
            };

            ListABP = new ObservableCollection<string>()
            {
                ABP_Null,
                ABP_3_0_NS1,
                ABP_1_5_NS,
                ABP_6_0_NS,
                ABP_3_0_NS2
            };

            ListC = new ObservableCollection<string>()
            {
                C_Null,
                C_R_Counter
            };

            ConvertStringToSettings();
        }

        /// <summary>
        /// 設定データクリア処理
        /// </summary>
        public void ClearSettings()
        {
            DLYSYNC = DLYSYNC_Null;
            LDP = LDP_Null;
            T = T_Null;
            ABP = ABP_Null;
            R = R_Null;
            C = C_Null;
        }

        /// <summary>
        /// 設定数値→設定データ変換処理
        /// </summary>
        /// <returns変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertStringToSettings()
        {
            bool ret;
            uint workdata;  // 24bitレジスタのため、32bit用意する。

            try
            {
                // 2進数データをもとに各設定データの変換をする
                workdata = Convert.ToUInt32(BinString, 2);

                ConvertStringToSettingsDLYSYNC(workdata);
                ConvertStringToSettingsLDP(workdata);
                ConvertStringToSettingsT(workdata);
                ConvertStringToSettingsABP(workdata);
                ConvertStringToSettingsR(workdata);
                ConvertStringToSettingsC(workdata);

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(DLY/SYNC)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsDLYSYNC(uint inputData)
        {
            uint workDLYSYNC = inputData & BitMask_DLYSYNC;

            if (workDLYSYNC == DLYSYNC_Bit_Normal1)
            {
                DLYSYNC = DLYSYNC_Normal1;
            }
            else if (workDLYSYNC == DLYSYNC_Bit_NonDlySync)
            {
                DLYSYNC = DLYSYNC_NonDlySync;
            }
            else if (workDLYSYNC == DLYSYNC_Bit_Normal2)
            {
                DLYSYNC = DLYSYNC_Normal2;
            }
            else if (workDLYSYNC == DLYSYNC_Bit_DlySync)
            {
                DLYSYNC = DLYSYNC_DlySync;
            }
            else
            {
                DLYSYNC = DLYSYNC_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(LDP)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsLDP(uint inputData)
        {
            uint workLDP = inputData & BitMask_LDP;

            if (workLDP == LDP_Bit_3Cycle)
            {
                LDP = LDP_3Cycle;
            }
            else if (workLDP == LDP_Bit_5Cycle)
            {
                LDP = LDP_5Cycle;
            }
            else
            {
                LDP = LDP_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(T)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsT(uint inputData)
        {
            uint workT = inputData & BitMask_T;

            if (workT == T_Bit_Normal)
            {
                T = T_Normal;
            }
            else
            {
                T = T_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(ABP)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsABP(uint inputData)
        {
            uint workABP = inputData & BitMask_ABP;

            if (workABP == ABP_Bit_3_0_NS1)
            {
                ABP = ABP_3_0_NS1;
            }
            else if (workABP == ABP_Bit_1_5_NS)
            {
                ABP = ABP_1_5_NS;
            }
            else if (workABP == ABP_Bit_6_0_NS)
            {
                ABP = ABP_6_0_NS;
            }
            else if (workABP == ABP_Bit_3_0_NS2)
            {
                ABP = ABP_3_0_NS2;
            }
            else
            {
                ABP = ABP_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(R)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsR(uint inputData)
        {
            uint workR = inputData & BitMask_R;
            workR >>= R_Bit_Shift;
            R = Convert.ToString(workR, 10);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(C)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsC(uint inputData)
        {
            uint workC = inputData & BitMask_C;

            if (workC == C_Bit_R_Counter)
            {
                C = C_R_Counter;
            }
            else
            {
                C = C_Null;
            }
        }

        /// <summary>
        /// 設定データ→設定数値変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertSettingsToString()
        {
            uint workdata = 0;  // 24bitレジスタのため、32bit用意する。

            // 各設定値を設定して、数値変換する
            if (DLYSYNC == DLYSYNC_Normal1)
            {
                workdata |= DLYSYNC_Bit_Normal1;
            }
            else if (DLYSYNC == DLYSYNC_NonDlySync)
            {
                workdata |= DLYSYNC_Bit_NonDlySync;
            }
            else if (DLYSYNC == DLYSYNC_Normal2)
            {
                workdata |= DLYSYNC_Bit_Normal2;
            }
            else if(DLYSYNC == DLYSYNC_DlySync)
            {
                workdata |= DLYSYNC_Bit_DlySync;
            }
            else
            {
                return (false);
            }

            if(LDP == LDP_3Cycle)
            {
                workdata |= LDP_Bit_3Cycle;
            }
            else if (LDP == LDP_5Cycle)
            {
                workdata |= LDP_Bit_5Cycle;
            }
            else
            {
                return (false);
            }

            if (T == T_Normal)
            {
                workdata |= T_Bit_Normal;
            }
            else
            {
                return (false);
            }

            if (ABP == ABP_3_0_NS1)
            {
                workdata |= ABP_Bit_3_0_NS1;
            }
            else if (ABP == ABP_1_5_NS)
            {
                workdata |= ABP_Bit_1_5_NS;
            }
            else if (ABP == ABP_6_0_NS)
            {
                workdata |= ABP_Bit_6_0_NS;
            }
            else if (ABP == ABP_3_0_NS2)
            {
                workdata |= ABP_Bit_3_0_NS2;
            }
            else
            {
                return (false);
            }

            try
            {
                if (Convert.ToUInt32(R, 10) <= R_Max_Threshold)
                {
                    workdata |= (Convert.ToUInt32(R, 10) << R_Bit_Shift);
                }
                else
                {
                    return (false);
                }
            }
            catch
            {
                return (false);
            }

            if (C == C_R_Counter)
            {
                workdata |= C_Bit_R_Counter;
            }
            else
            {
                return (false);
            }

            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            DecString = Convert.ToString(workdata, 10);
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');

            return (true);
        }
    }

    /// <summary>
    /// レジスタモデル(ADF4111)ABカウンタ・ラッチ
    /// </summary>
    public class Model_Register_ADF4111_ABCounter : Model_Register_ADF4111
    {
        /// <summary>
        /// ビットマスク
        /// </summary>
        private static readonly uint BitMask_G = 0b001000000000000000000000;
        private static readonly uint BitMask_B = 0b000111111111111100000000;
        private static readonly uint BitMask_A = 0b000000000000000011111100;
        private static readonly uint BitMask_C = 0b000000000000000000000011;

        /// <summary>
        /// G
        /// </summary>
        private string _dataG;
        public string G
        {
            get { return _dataG; }
            set { SetProperty(ref _dataG, value); }
        }
        private static readonly string G_Null = string.Empty;
        private static readonly string G_CP_Current1 = "チャージ・ポンプ電流設定1を使用";
        private static readonly string G_CP_Current2 = "チャージ・ポンプ電流設定2を使用";
        private static readonly uint G_Bit_CP_Current1 = 0b000000000000000000000000;
        private static readonly uint G_Bit_CP_Current2 = 0b001000000000000000000000;

        /// <summary>
        /// B
        /// </summary>
        private string _dataB;
        public string B
        {
            get { return _dataB; }
            set { SetProperty(ref _dataB, value); }
        }
        private static readonly string B_Null = string.Empty;
        private static readonly string B_Disallowed = "不許可";
        private static readonly int B_Bit_Shift = 8;
        private static readonly uint B_Disallowed_Threshold = 2;
        private static readonly uint B_Min_Threshold = 3;
        private static readonly uint B_Max_Threshold = 8191;

        /// <summary>
        /// A
        /// </summary>
        private string _dataA;
        public string A
        {
            get { return _dataA; }
            set { SetProperty(ref _dataA, value); }
        }
        private static readonly string A_Null = string.Empty;
        private static readonly int A_Bit_Shift = 2;
        private static readonly uint A_Max_Threshold = 63;

        /// <summary>
        /// C
        /// </summary>
        private string _dataC;
        public string C
        {
            get { return _dataC; }
            set { SetProperty(ref _dataC, value); }
        }
        private static readonly string C_Null = string.Empty;
        private static readonly string C_N_Counter = "Nカウンタ";
        private static readonly uint C_Bit_N_Counter = 0b000000000000000000000001;

        /// <summary>
        /// バインディングデータ：解析データ：Gのリスト
        /// </summary>
        private ObservableCollection<string> _listG;
        public ObservableCollection<string> ListG
        {
            get { return _listG; }
            set { SetProperty(ref _listG, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：Cのリスト
        /// </summary>
        private ObservableCollection<string> _listC;
        public ObservableCollection<string> ListC
        {
            get { return _listC; }
            set { SetProperty(ref _listC, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model_Register_ADF4111_ABCounter()
        {
            ListG = new ObservableCollection<string>()
            {
                G_Null,
                G_CP_Current1,
                G_CP_Current2
            };

            ListC = new ObservableCollection<string>()
            {
                C_Null,
                C_N_Counter
            };

            ConvertStringToSettings();
        }

        /// <summary>
        /// 設定データクリア処理
        /// </summary>
        public void ClearSettings()
        {
            G = G_Null;
            B = B_Null;
            A = A_Null;
            C = C_Null;
        }

        /// <summary>
        /// 設定数値→設定データ変換処理
        /// </summary>
        /// <returns変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertStringToSettings()
        {
            bool ret;
            uint workdata;  // 24bitレジスタのため、32bit用意する。

            try
            {
                // 2進数データをもとに各設定データの変換をする
                workdata = Convert.ToUInt32(BinString, 2);

                ConvertStringToSettingsG(workdata);
                ConvertStringToSettingsB(workdata);
                ConvertStringToSettingsA(workdata);
                ConvertStringToSettingsC(workdata);

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(G)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsG(uint inputData)
        {
            uint workG = inputData & BitMask_G;

            if (workG == G_Bit_CP_Current1)
            {
                G = G_CP_Current1;
            }
            else if (workG == G_Bit_CP_Current2)
            {
                G = G_CP_Current2;
            }
            else
            {
                G = G_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(B)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsB(uint inputData)
        {
            uint workB = inputData & BitMask_B;
            workB >>= B_Bit_Shift;
            if(B_Disallowed_Threshold < workB)
            {
                B = Convert.ToString(workB, 10);
            }
            else
            {
                B = B_Disallowed;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(A)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsA(uint inputData)
        {
            uint workA = inputData & BitMask_A;
            workA >>= A_Bit_Shift;
            A = Convert.ToString(workA, 10);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(C)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsC(uint inputData)
        {
            uint workC = inputData & BitMask_C;

            if (workC == C_Bit_N_Counter)
            {
                C = C_N_Counter;
            }
            else
            {
                C = C_Null;
            }
        }

        /// <summary>
        /// 設定データ→設定数値変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertSettingsToString()
        {
            uint workdata = 0;  // 24bitレジスタのため、32bit用意する。

            // 各設定値を設定して、数値変換する
            if(G == G_CP_Current1)
            {
                workdata |= G_Bit_CP_Current1;
            }
            else if(G == G_CP_Current2)
            {
                workdata |= G_Bit_CP_Current2;
            }
            else
            {
                return (false);
            }

            try
            {
                if((B_Min_Threshold <= Convert.ToUInt32(B, 10)) && (Convert.ToUInt32(B, 10) <= B_Max_Threshold))
                {
                    workdata |= (Convert.ToUInt32(B, 10) << B_Bit_Shift);
                }
                else
                {
                    return (false);
                }
            }
            catch
            {
                return (false);
            }

            try
            {
                if (Convert.ToUInt32(A, 10) <= A_Max_Threshold)
                {
                    workdata |= (Convert.ToUInt32(A, 10) << A_Bit_Shift);
                }
                else
                {
                    return (false);
                }
            }
            catch
            {
                return (false);
            }

            if (C == C_N_Counter)
            {
                workdata |= C_Bit_N_Counter;
            }
            else
            {
                return (false);
            }

            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            DecString = Convert.ToString(workdata, 10);
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');

            return (true);
        }
    }

    /// <summary>
    /// レジスタモデル(ADF4111)ファンクション・ラッチ
    /// </summary>
    public class Model_Register_ADF4111_Function : Model_Register_ADF4111
    {
        /// <summary>
        /// ビットマスク
        /// </summary>
        private static readonly uint BitMask_P =    0b110000000000000000000000;
        private static readonly uint BitMask_PD2 =  0b001000000000000000000000;
        private static readonly uint BitMask_CPI2 = 0b000111000000000000000000;
        private static readonly uint BitMask_CPI1 = 0b000000111000000000000000;
        private static readonly uint BitMask_TC =   0b000000000111100000000000;
        private static readonly uint BitMask_F5F4 = 0b000000000000011000000000;
        private static readonly uint BitMask_F3 =   0b000000000000000100000000;
        private static readonly uint BitMask_F2 =   0b000000000000000010000000;
        private static readonly uint BitMask_M =    0b000000000000000001110000;
        private static readonly uint BitMask_PD1 =  0b000000000000000000001000;
        private static readonly uint BitMask_F1 =   0b000000000000000000000100;
        private static readonly uint BitMask_C =    0b000000000000000000000011;

        /// <summary>
        /// P
        /// </summary>
        private string _dataP;
        public string P
        {
            get { return _dataP; }
            set { SetProperty(ref _dataP, value); }
        }
        private static readonly string P_Null = string.Empty;
        private static readonly string P_Prescaler_8_9 = "8/9";
        private static readonly string P_Prescaler_16_17 = "16/17";
        private static readonly string P_Prescaler_32_33 = "32/33";
        private static readonly string P_Prescaler_64_65 = "64/65";
        private static readonly uint P_Bit_Prescaler_8_9 = 0b000000000000000000000000;
        private static readonly uint P_Bit_Prescaler_16_17 = 0b010000000000000000000000;
        private static readonly uint P_Bit_Prescaler_32_33 = 0b100000000000000000000000;
        private static readonly uint P_Bit_Prescaler_64_65 = 0b110000000000000000000000;

        /// <summary>
        /// PD2
        /// </summary>
        private string _dataPD2;
        public string PD2
        {
            get { return _dataPD2; }
            set { SetProperty(ref _dataPD2, value); }
        }
        private static readonly string PD2_Null = string.Empty;
        private static readonly string PD2_Async = "非同期";
        private static readonly string PD2_Sync = "同期";
        private static readonly uint PD2_Bit_Async = 0b000000000000000000000000;
        private static readonly uint PD2_Bit_Sync = 0b001000000000000000000000;

        /// <summary>
        /// CPI2
        /// </summary>
        private string _dataCPI2;
        public string CPI2
        {
            get { return _dataCPI2; }
            set { SetProperty(ref _dataCPI2, value); }
        }
        private static readonly string CPI2_Null = string.Empty;
        private static readonly string CPI2_000 = "2.7kΩ：1.09mA / 4.7kΩ：0.63mA / 10kΩ：0.29mA";
        private static readonly string CPI2_001 = "2.7kΩ：2.18mA / 4.7kΩ：1.25mA / 10kΩ：0.59mA";
        private static readonly string CPI2_010 = "2.7kΩ：3.26mA / 4.7kΩ：1.88mA / 10kΩ：0.88mA";
        private static readonly string CPI2_011 = "2.7kΩ：4.35mA / 4.7kΩ：2.50mA / 10kΩ：1.76mA";
        private static readonly string CPI2_100 = "2.7kΩ：5.44mA / 4.7kΩ：3.13mA / 10kΩ：1.47mA";
        private static readonly string CPI2_101 = "2.7kΩ：6.53mA / 4.7kΩ：3.75mA / 10kΩ：1.76mA";
        private static readonly string CPI2_110 = "2.7kΩ：7.62mA / 4.7kΩ：4.38mA / 10kΩ：2.06mA";
        private static readonly string CPI2_111 = "2.7kΩ：8.70mA / 4.7kΩ：5.00mA / 10kΩ：2.35mA";
        private static readonly uint CPI2_Bit_000 = 0b000000000000000000000000;
        private static readonly uint CPI2_Bit_001 = 0b000001000000000000000000;
        private static readonly uint CPI2_Bit_010 = 0b000010000000000000000000;
        private static readonly uint CPI2_Bit_011 = 0b000011000000000000000000;
        private static readonly uint CPI2_Bit_100 = 0b000100000000000000000000;
        private static readonly uint CPI2_Bit_101 = 0b000101000000000000000000;
        private static readonly uint CPI2_Bit_110 = 0b000110000000000000000000;
        private static readonly uint CPI2_Bit_111 = 0b000111000000000000000000;

        /// <summary>
        /// CPI1
        /// </summary>
        private string _dataCPI1;
        public string CPI1
        {
            get { return _dataCPI1; }
            set { SetProperty(ref _dataCPI1, value); }
        }
        private static readonly string CPI1_Null = string.Empty;
        private static readonly string CPI1_000 = "2.7kΩ：1.09mA / 4.7kΩ：0.63mA / 10kΩ：0.29mA";
        private static readonly string CPI1_001 = "2.7kΩ：2.18mA / 4.7kΩ：1.25mA / 10kΩ：0.59mA";
        private static readonly string CPI1_010 = "2.7kΩ：3.26mA / 4.7kΩ：1.88mA / 10kΩ：0.88mA";
        private static readonly string CPI1_011 = "2.7kΩ：4.35mA / 4.7kΩ：2.50mA / 10kΩ：1.76mA";
        private static readonly string CPI1_100 = "2.7kΩ：5.44mA / 4.7kΩ：3.13mA / 10kΩ：1.47mA";
        private static readonly string CPI1_101 = "2.7kΩ：6.53mA / 4.7kΩ：3.75mA / 10kΩ：1.76mA";
        private static readonly string CPI1_110 = "2.7kΩ：7.62mA / 4.7kΩ：4.38mA / 10kΩ：2.06mA";
        private static readonly string CPI1_111 = "2.7kΩ：8.70mA / 4.7kΩ：5.00mA / 10kΩ：2.35mA";
        private static readonly uint CPI1_Bit_000 = 0b000000000000000000000000;
        private static readonly uint CPI1_Bit_001 = 0b000000001000000000000000;
        private static readonly uint CPI1_Bit_010 = 0b000000010000000000000000;
        private static readonly uint CPI1_Bit_011 = 0b000000011000000000000000;
        private static readonly uint CPI1_Bit_100 = 0b000000100000000000000000;
        private static readonly uint CPI1_Bit_101 = 0b000000101000000000000000;
        private static readonly uint CPI1_Bit_110 = 0b000000110000000000000000;
        private static readonly uint CPI1_Bit_111 = 0b000000111000000000000000;

        /// <summary>
        /// TC
        /// </summary>
        private string _dataTC;
        public string TC
        {
            get { return _dataTC; }
            set { SetProperty(ref _dataTC, value); }
        }
        private static readonly string TC_Null = string.Empty;
        private static readonly string TC_0000 = "3";
        private static readonly string TC_0001 = "7";
        private static readonly string TC_0010 = "11";
        private static readonly string TC_0011 = "15";
        private static readonly string TC_0100 = "19";
        private static readonly string TC_0101 = "23";
        private static readonly string TC_0110 = "27";
        private static readonly string TC_0111 = "31";
        private static readonly string TC_1000 = "35";
        private static readonly string TC_1001 = "39";
        private static readonly string TC_1010 = "43";
        private static readonly string TC_1011 = "58";
        private static readonly string TC_1100 = "51";
        private static readonly string TC_1101 = "55";
        private static readonly string TC_1110 = "59";
        private static readonly string TC_1111 = "63";
        private static readonly uint TC_Bit_0000 = 0b000000000000000000000000;
        private static readonly uint TC_Bit_0001 = 0b000000000000100000000000;
        private static readonly uint TC_Bit_0010 = 0b000000000001000000000000;
        private static readonly uint TC_Bit_0011 = 0b000000000001100000000000;
        private static readonly uint TC_Bit_0100 = 0b000000000010000000000000;
        private static readonly uint TC_Bit_0101 = 0b000000000010100000000000;
        private static readonly uint TC_Bit_0110 = 0b000000000011000000000000;
        private static readonly uint TC_Bit_0111 = 0b000000000011100000000000;
        private static readonly uint TC_Bit_1000 = 0b000000000100000000000000;
        private static readonly uint TC_Bit_1001 = 0b000000000100100000000000;
        private static readonly uint TC_Bit_1010 = 0b000000000101000000000000;
        private static readonly uint TC_Bit_1011 = 0b000000000101100000000000;
        private static readonly uint TC_Bit_1100 = 0b000000000110000000000000;
        private static readonly uint TC_Bit_1101 = 0b000000000110100000000000;
        private static readonly uint TC_Bit_1110 = 0b000000000111000000000000;
        private static readonly uint TC_Bit_1111 = 0b000000000111100000000000;

        /// <summary>
        /// F5F4
        /// </summary>
        private string _dataF5F4;
        public string F5F4
        {
            get { return _dataF5F4; }
            set { SetProperty(ref _dataF5F4, value); }
        }
        private static readonly string F5F4_Null = string.Empty;
        private static readonly string F5F4_Disable1 = "高速ロック・ディスエーブル(00)";
        private static readonly string F5F4_Mode1 = "高速ロック・モード1";
        private static readonly string F5F4_Disable2 = "高速ロック・ディスエーブル(10)";
        private static readonly string F5F4_Mode2 = "高速ロック・モード2";
        private static readonly uint F5F4_Bit_Disable1 = 0b000000000000000000000000;
        private static readonly uint F5F4_Bit_Mode1 = 0b000000000000001000000000;
        private static readonly uint F5F4_Bit_Disable2 = 0b000000000000010000000000;
        private static readonly uint F5F4_Bit_Mode2 = 0b000000000000011000000000;

        /// <summary>
        /// F3
        /// </summary>
        private string _dataF3;
        public string F3
        {
            get { return _dataF3; }
            set { SetProperty(ref _dataF3, value); }
        }
        private static readonly string F3_Null = string.Empty;
        private static readonly string F3_Normal = "通常";
        private static readonly string F3_ThreeState = "スリーステート";
        private static readonly uint F3_Bit_Normal = 0b000000000000000000000000;
        private static readonly uint F3_Bit_ThreeState = 0b000000000000000100000000;

        /// <summary>
        /// F2
        /// </summary>
        private string _dataF2;
        public string F2
        {
            get { return _dataF2; }
            set { SetProperty(ref _dataF2, value); }
        }
        private static readonly string F2_Null = string.Empty;
        private static readonly string F2_Negative = "負極性";
        private static readonly string F2_Positive = "正極性";
        private static readonly uint F2_Bit_Negative = 0b000000000000000000000000;
        private static readonly uint F2_Bit_Positive = 0b000000000000000010000000;

        /// <summary>
        /// M
        /// </summary>
        private string _dataM;
        public string M
        {
            get { return _dataM; }
            set { SetProperty(ref _dataM, value); }
        }
        private static readonly string M_Null = string.Empty;
        private static readonly string M_ThreeStateOutput = "スリーステート出力";
        private static readonly string M_DigitalLockDetect = "デジタル・ロック検出";
        private static readonly string M_NDividerOutput = "Nデバイダ出力";
        private static readonly string M_DVDD = "DVDD";
        private static readonly string M_RDividerOutput = "Rデバイダ出力";
        private static readonly string M_AnalogLockDetect = "アナログ・ロック検出";
        private static readonly string M_SerialDataOutput = "シリアル・データ出力";
        private static readonly string M_DGND = "DGND";
        private static readonly uint M_Bit_ThreeStateOutput = 0b000000000000000000000000;
        private static readonly uint M_Bit_DigitalLockDetect = 0b000000000000000000010000;
        private static readonly uint M_Bit_NDividerOutput = 0b000000000000000000100000;
        private static readonly uint M_Bit_DVDD = 0b000000000000000000110000;
        private static readonly uint M_Bit_RDividerOutput = 0b000000000000000001000000;
        private static readonly uint M_Bit_AnalogLockDetect = 0b000000000000000001010000;
        private static readonly uint M_Bit_SerialDataOutput = 0b000000000000000001100000;
        private static readonly uint M_Bit_DGND = 0b000000000000000001110000;

        /// <summary>
        /// PD1
        /// </summary>
        private string _dataPD1;
        public string PD1
        {
            get { return _dataPD1; }
            set { SetProperty(ref _dataPD1, value); }
        }
        private static readonly string PD1_Null = string.Empty;
        private static readonly string PD1_Normal = "通常動作";
        private static readonly string PD1_PowerDown = "パワーダウン";
        private static readonly uint PD1_Bit_Normal = 0b000000000000000000000000;
        private static readonly uint PD1_Bit_PowerDown = 0b000000000000000000001000;

        /// <summary>
        /// F1
        /// </summary>
        private string _dataF1;
        public string F1
        {
            get { return _dataF1; }
            set { SetProperty(ref _dataF1, value); }
        }
        private static readonly string F1_Null = string.Empty;
        private static readonly string F1_Normal = "通常";
        private static readonly string F1_Reset = "R、A、Bカウンタ・リセット状態";
        private static readonly uint F1_Bit_Normal = 0b000000000000000000000000;
        private static readonly uint F1_Bit_Reset = 0b000000000000000000000100;

        /// <summary>
        /// C
        /// </summary>
        private string _dataC;
        public string C
        {
            get { return _dataC; }
            set { SetProperty(ref _dataC, value); }
        }
        private static readonly string C_Null = string.Empty;
        private static readonly string C_Function = "ファンクション・ラッチ";
        private static readonly uint C_Bit_Function = 0b000000000000000000000010;

        /// <summary>
        /// バインディングデータ：解析データ：Pのリスト
        /// </summary>
        private ObservableCollection<string> _listP;
        public ObservableCollection<string> ListP
        {
            get { return _listP; }
            set { SetProperty(ref _listP, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：PD2のリスト
        /// </summary>
        private ObservableCollection<string> _listPD2;
        public ObservableCollection<string> ListPD2
        {
            get { return _listPD2; }
            set { SetProperty(ref _listPD2, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：CPI2のリスト
        /// </summary>
        private ObservableCollection<string> _listCPI2;
        public ObservableCollection<string> ListCPI2
        {
            get { return _listCPI2; }
            set { SetProperty(ref _listCPI2, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：CPI1のリスト
        /// </summary>
        private ObservableCollection<string> _listCPI1;
        public ObservableCollection<string> ListCPI1
        {
            get { return _listCPI1; }
            set { SetProperty(ref _listCPI1, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：TCのリスト
        /// </summary>
        private ObservableCollection<string> _listTC;
        public ObservableCollection<string> ListTC
        {
            get { return _listTC; }
            set { SetProperty(ref _listTC, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F5F4のリスト
        /// </summary>
        private ObservableCollection<string> _listF5F4;
        public ObservableCollection<string> ListF5F4
        {
            get { return _listF5F4; }
            set { SetProperty(ref _listF5F4, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F3のリスト
        /// </summary>
        private ObservableCollection<string> _listF3;
        public ObservableCollection<string> ListF3
        {
            get { return _listF3; }
            set { SetProperty(ref _listF3, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F2のリスト
        /// </summary>
        private ObservableCollection<string> _listF2;
        public ObservableCollection<string> ListF2
        {
            get { return _listF2; }
            set { SetProperty(ref _listF2, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：Mのリスト
        /// </summary>
        private ObservableCollection<string> _listM;
        public ObservableCollection<string> ListM
        {
            get { return _listM; }
            set { SetProperty(ref _listM, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：PD1のリスト
        /// </summary>
        private ObservableCollection<string> _listPD1;
        public ObservableCollection<string> ListPD1
        {
            get { return _listPD1; }
            set { SetProperty(ref _listPD1, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F1のリスト
        /// </summary>
        private ObservableCollection<string> _listF1;
        public ObservableCollection<string> ListF1
        {
            get { return _listF1; }
            set { SetProperty(ref _listF1, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：Cのリスト
        /// </summary>
        private ObservableCollection<string> _listC;
        public ObservableCollection<string> ListC
        {
            get { return _listC; }
            set { SetProperty(ref _listC, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model_Register_ADF4111_Function()
        {
            ListP = new ObservableCollection<string>()
            {
                P_Null,
                P_Prescaler_8_9,
                P_Prescaler_16_17,
                P_Prescaler_32_33,
                P_Prescaler_64_65
            };

            ListPD2 = new ObservableCollection<string>()
            {
                PD2_Null,
                PD2_Async,
                PD2_Sync
            };

            ListCPI2 = new ObservableCollection<string>()
            {
                CPI2_Null,
                CPI2_000,
                CPI2_001,
                CPI2_010,
                CPI2_011,
                CPI2_100,
                CPI2_101,
                CPI2_110,
                CPI2_111
            };

            ListCPI1 = new ObservableCollection<string>()
            {
                CPI1_Null,
                CPI1_000,
                CPI1_001,
                CPI1_010,
                CPI1_011,
                CPI1_100,
                CPI1_101,
                CPI1_110,
                CPI1_111
            };

            ListTC = new ObservableCollection<string>()
            {
                TC_Null,
                TC_0000,
                TC_0001,
                TC_0010,
                TC_0011,
                TC_0100,
                TC_0101,
                TC_0110,
                TC_0111,
                TC_1000,
                TC_1001,
                TC_1010,
                TC_1011,
                TC_1100,
                TC_1101,
                TC_1110,
                TC_1111
            };

            ListF5F4 = new ObservableCollection<string>()
            {
                F5F4_Null,
                F5F4_Disable1,
                F5F4_Mode1,
                F5F4_Disable2,
                F5F4_Mode2
            };

            ListF3 = new ObservableCollection<string>()
            {
                F3_Null,
                F3_Normal,
                F3_ThreeState
            };

            ListF2 = new ObservableCollection<string>()
            {
                F2_Null,
                F2_Negative,
                F2_Positive
            };

            ListM = new ObservableCollection<string>()
            {
                M_Null,
                M_ThreeStateOutput,
                M_DigitalLockDetect,
                M_NDividerOutput,
                M_DVDD,
                M_RDividerOutput,
                M_AnalogLockDetect,
                M_SerialDataOutput,
                M_DGND
            };

            ListPD1 = new ObservableCollection<string>()
            {
                PD1_Null,
                PD1_Normal,
                PD1_PowerDown
            };

            ListF1 = new ObservableCollection<string>()
            {
                F1_Null,
                F1_Normal,
                F1_Reset
            };

            ListC = new ObservableCollection<string>()
            {
                C_Null,
                C_Function
            };

            ConvertStringToSettings();
        }

        /// <summary>
        /// 設定データクリア処理
        /// </summary>
        public void ClearSettings()
        {
            P = P_Null;
            PD2 = PD2_Null;
            CPI2 = CPI2_Null;
            CPI1 = CPI1_Null;
            TC = TC_Null;
            F5F4 = F5F4_Null;
            F3 = F3_Null;
            F2 = F2_Null;
            M = M_Null;
            PD1 = PD1_Null;
            F1 = F1_Null;
            C = C_Null;
        }

        /// <summary>
        /// 設定数値→設定データ変換処理
        /// </summary>
        /// <returns変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertStringToSettings()
        {
            bool ret;
            uint workdata;  // 24bitレジスタのため、32bit用意する。

            try
            {
                // 2進数データをもとに各設定データの変換をする
                workdata = Convert.ToUInt32(BinString, 2);

                ConvertStringToSettingsP(workdata);
                ConvertStringToSettingsPD2(workdata);
                ConvertStringToSettingsCPI2(workdata);
                ConvertStringToSettingsCPI1(workdata);
                ConvertStringToSettingsTC(workdata);
                ConvertStringToSettingsF5F4(workdata);
                ConvertStringToSettingsF3(workdata);
                ConvertStringToSettingsF2(workdata);
                ConvertStringToSettingsM(workdata);
                ConvertStringToSettingsPD1(workdata);
                ConvertStringToSettingsF1(workdata);
                ConvertStringToSettingsC(workdata);

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(P)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsP(uint inputData)
        {
            uint workP = inputData & BitMask_P;

            if (workP == P_Bit_Prescaler_8_9)
            {
                P = P_Prescaler_8_9;
            }
            else if (workP == P_Bit_Prescaler_16_17)
            {
                P = P_Prescaler_16_17;
            }
            else if(workP == P_Bit_Prescaler_32_33)
            {
                P = P_Prescaler_32_33;
            }
            else if(workP == P_Bit_Prescaler_64_65)
            {
                P = P_Prescaler_64_65;
            }
            else
            {
                P = P_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(PD2)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsPD2(uint inputData)
        {
            uint workPD2 = inputData & BitMask_PD2;

            if (workPD2 == PD2_Bit_Async)
            {
                PD2 = PD2_Async;
            }
            else if (workPD2 == PD2_Bit_Sync)
            {
                PD2 = PD2_Sync;
            }
            else
            {
                PD2 = PD2_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(CPI2)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsCPI2(uint inputData)
        {
            uint workCPI2 = inputData & BitMask_CPI2;

            if (workCPI2 == CPI2_Bit_000)
            {
                CPI2 = CPI2_000;
            }
            else if (workCPI2 == CPI2_Bit_001)
            {
                CPI2 = CPI2_001;
            }
            else if (workCPI2 == CPI2_Bit_010)
            {
                CPI2 = CPI2_010;
            }
            else if (workCPI2 == CPI2_Bit_011)
            {
                CPI2 = CPI2_011;
            }
            else if (workCPI2 == CPI2_Bit_100)
            {
                CPI2 = CPI2_100;
            }
            else if (workCPI2 == CPI2_Bit_101)
            {
                CPI2 = CPI2_101;
            }
            else if (workCPI2 == CPI2_Bit_110)
            {
                CPI2 = CPI2_110;
            }
            else if (workCPI2 == CPI2_Bit_111)
            {
                CPI2 = CPI2_111;
            }
            else
            {
                CPI2 = CPI2_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(CPI1)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsCPI1(uint inputData)
        {
            uint workCPI1 = inputData & BitMask_CPI1;

            if (workCPI1 == CPI1_Bit_000)
            {
                CPI1 = CPI1_000;
            }
            else if (workCPI1 == CPI1_Bit_001)
            {
                CPI1 = CPI1_001;
            }
            else if (workCPI1 == CPI1_Bit_010)
            {
                CPI1 = CPI1_010;
            }
            else if (workCPI1 == CPI1_Bit_011)
            {
                CPI1 = CPI1_011;
            }
            else if (workCPI1 == CPI1_Bit_100)
            {
                CPI1 = CPI1_100;
            }
            else if (workCPI1 == CPI1_Bit_101)
            {
                CPI1 = CPI1_101;
            }
            else if (workCPI1 == CPI1_Bit_110)
            {
                CPI1 = CPI1_110;
            }
            else if (workCPI1 == CPI1_Bit_111)
            {
                CPI1 = CPI1_111;
            }
            else
            {
                CPI1 = CPI1_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(TC)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsTC(uint inputData)
        {
            uint workTC = inputData & BitMask_TC;

            if (workTC == TC_Bit_0000)
            {
                TC = TC_0000;
            }
            else if (workTC == TC_Bit_0001)
            {
                TC = TC_0001;
            }
            else if (workTC == TC_Bit_0010)
            {
                TC = TC_0010;
            }
            else if (workTC == TC_Bit_0011)
            {
                TC = TC_0011;
            }
            else if (workTC == TC_Bit_0100)
            {
                TC = TC_0100;
            }
            else if (workTC == TC_Bit_0101)
            {
                TC = TC_0101;
            }
            else if (workTC == TC_Bit_0110)
            {
                TC = TC_0110;
            }
            else if (workTC == TC_Bit_0111)
            {
                TC = TC_0111;
            }
            else if (workTC == TC_Bit_1000)
            {
                TC = TC_1000;
            }
            else if (workTC == TC_Bit_1001)
            {
                TC = TC_1001;
            }
            else if (workTC == TC_Bit_1010)
            {
                TC = TC_1010;
            }
            else if (workTC == TC_Bit_1011)
            {
                TC = TC_1011;
            }
            else if (workTC == TC_Bit_1100)
            {
                TC = TC_1100;
            }
            else if (workTC == TC_Bit_1101)
            {
                TC = TC_1101;
            }
            else if (workTC == TC_Bit_1110)
            {
                TC = TC_1110;
            }
            else if (workTC == TC_Bit_1111)
            {
                TC = TC_1111;
            }
            else
            {
                TC = TC_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F5F4)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF5F4(uint inputData)
        {
            uint workF5F4 = inputData & BitMask_F5F4;

            if (workF5F4 == F5F4_Bit_Disable1)
            {
                F5F4 = F5F4_Disable1;
            }
            else if (workF5F4 == F5F4_Bit_Mode1)
            {
                F5F4 = F5F4_Mode1;
            }
            else if (workF5F4 == F5F4_Bit_Disable2)
            {
                F5F4 = F5F4_Disable2;
            }
            else if (workF5F4 == F5F4_Bit_Mode2)
            {
                F5F4 = F5F4_Mode2;
            }
            else
            {
                F5F4 = F5F4_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F3)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF3(uint inputData)
        {
            uint workF3 = inputData & BitMask_F3;

            if (workF3 == F3_Bit_Normal)
            {
                F3 = F3_Normal;
            }
            else if (workF3 == F3_Bit_ThreeState)
            {
                F3 = F3_ThreeState;
            }
            else
            {
                F3 = F3_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F2)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF2(uint inputData)
        {
            uint workF2 = inputData & BitMask_F2;

            if (workF2 == F2_Bit_Negative)
            {
                F2 = F2_Negative;
            }
            else if (workF2 == F2_Bit_Positive)
            {
                F2 = F2_Positive;
            }
            else
            {
                F2 = F2_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(M)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsM(uint inputData)
        {
            uint workM = inputData & BitMask_M;

            if (workM == M_Bit_ThreeStateOutput)
            {
                M = M_ThreeStateOutput;
            }
            else if (workM == M_Bit_DigitalLockDetect)
            {
                M = M_DigitalLockDetect;
            }
            else if (workM == M_Bit_NDividerOutput)
            {
                M = M_NDividerOutput;
            }
            else if (workM == M_Bit_DVDD)
            {
                M = M_DVDD;
            }
            else if (workM == M_Bit_RDividerOutput)
            {
                M = M_RDividerOutput;
            }
            else if (workM == M_Bit_AnalogLockDetect)
            {
                M = M_AnalogLockDetect;
            }
            else if (workM == M_Bit_SerialDataOutput)
            {
                M = M_SerialDataOutput;
            }
            else if (workM == M_Bit_DGND)
            {
                M = M_DGND;
            }
            else
            {
                M = M_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(PD1)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsPD1(uint inputData)
        {
            uint workPD1 = inputData & BitMask_PD1;

            if (workPD1 == PD1_Bit_Normal)
            {
                PD1 = PD1_Normal;
            }
            else if (workPD1 == PD1_Bit_PowerDown)
            {
                PD1 = PD1_PowerDown;
            }
            else
            {
                PD1 = PD1_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F1)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF1(uint inputData)
        {
            uint workF1 = inputData & BitMask_F1;

            if (workF1 == F1_Bit_Normal)
            {
                F1 = F1_Normal;
            }
            else if (workF1 == F1_Bit_Reset)
            {
                F1 = F1_Reset;
            }
            else
            {
                F1 = F1_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(C)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsC(uint inputData)
        {
            uint workC = inputData & BitMask_C;

            if (workC == C_Bit_Function)
            {
                C = C_Function;
            }
            else
            {
                C = C_Null;
            }
        }

        /// <summary>
        /// 設定データ→設定数値変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertSettingsToString()
        {
            uint workdata = 0;  // 24bitレジスタのため、32bit用意する。

            // 各設定値を設定して、数値変換する
            if(P == P_Prescaler_8_9)
            {
                workdata |= P_Bit_Prescaler_8_9;
            }
            else if(P == P_Prescaler_16_17)
            {
                workdata |= P_Bit_Prescaler_16_17;
            }
            else if(P == P_Prescaler_32_33)
            {
                workdata |= P_Bit_Prescaler_32_33;
            }
            else if(P == P_Prescaler_64_65)
            {
                workdata |= P_Bit_Prescaler_64_65;
            }
            else
            {
                return (false);
            }

            if(PD2 == PD2_Async)
            {
                workdata |= PD2_Bit_Async;
            }
            else if(PD2 == PD2_Sync)
            {
                workdata |= PD2_Bit_Sync;
            }
            else
            {
                return (false);
            }

            if(CPI2 == CPI2_000)
            {
                workdata |= CPI2_Bit_000;
            }
            else if(CPI2 == CPI2_001)
            {
                workdata |= CPI2_Bit_001;
            }
            else if (CPI2 == CPI2_010)
            {
                workdata |= CPI2_Bit_010;
            }
            else if (CPI2 == CPI2_011)
            {
                workdata |= CPI2_Bit_011;
            }
            else if (CPI2 == CPI2_100)
            {
                workdata |= CPI2_Bit_100;
            }
            else if (CPI2 == CPI2_101)
            {
                workdata |= CPI2_Bit_101;
            }
            else if (CPI2 == CPI2_110)
            {
                workdata |= CPI2_Bit_110;
            }
            else if (CPI2 == CPI2_111)
            {
                workdata |= CPI2_Bit_111;
            }
            else
            {
                return (false);
            }

            if (CPI1 == CPI1_000)
            {
                workdata |= CPI1_Bit_000;
            }
            else if (CPI1 == CPI1_001)
            {
                workdata |= CPI1_Bit_001;
            }
            else if (CPI1 == CPI1_010)
            {
                workdata |= CPI1_Bit_010;
            }
            else if (CPI1 == CPI1_011)
            {
                workdata |= CPI1_Bit_011;
            }
            else if (CPI1 == CPI1_100)
            {
                workdata |= CPI1_Bit_100;
            }
            else if (CPI1 == CPI1_101)
            {
                workdata |= CPI1_Bit_101;
            }
            else if (CPI1 == CPI1_110)
            {
                workdata |= CPI1_Bit_110;
            }
            else if (CPI1 == CPI1_111)
            {
                workdata |= CPI1_Bit_111;
            }
            else
            {
                return (false);
            }
            
            if (TC == TC_0000)
            {
                workdata |= TC_Bit_0000;
            }
            else if (TC == TC_0001)
            {
                workdata |= TC_Bit_0001;
            }
            else if (TC == TC_0010)
            {
                workdata |= TC_Bit_0010;
            }
            else if (TC == TC_0011)
            {
                workdata |= TC_Bit_0011;
            }
            else if (TC == TC_0100)
            {
                workdata |= TC_Bit_0100;
            }
            else if (TC == TC_0101)
            {
                workdata |= TC_Bit_0101;
            }
            else if (TC == TC_0110)
            {
                workdata |= TC_Bit_0110;
            }
            else if (TC == TC_0111)
            {
                workdata |= TC_Bit_0111;
            }
            else if (TC == TC_1000)
            {
                workdata |= TC_Bit_1000;
            }
            else if (TC == TC_1001)
            {
                workdata |= TC_Bit_1001;
            }
            else if (TC == TC_1010)
            {
                workdata |= TC_Bit_1010;
            }
            else if (TC == TC_1011)
            {
                workdata |= TC_Bit_1011;
            }
            else if (TC == TC_1100)
            {
                workdata |= TC_Bit_1100;
            }
            else if (TC == TC_1101)
            {
                workdata |= TC_Bit_1101;
            }
            else if (TC == TC_1110)
            {
                workdata |= TC_Bit_1110;
            }
            else if (TC == TC_1111)
            {
                workdata |= TC_Bit_1111;
            }
            else
            {
                return (false);
            }

            if (F5F4 == F5F4_Disable1)
            {
                workdata |= F5F4_Bit_Disable1;
            }
            else if(F5F4 == F5F4_Mode1)
            {
                workdata |= F5F4_Bit_Mode1;
            }
            else if(F5F4 == F5F4_Disable2)
            {
                workdata |= F5F4_Bit_Disable2;
            }
            else if(F5F4 == F5F4_Mode2)
            {
                workdata |= F5F4_Bit_Mode2;
            }
            else
            {
                return (false);
            }

            if(F3 == F3_Normal)
            {
                workdata |= F3_Bit_Normal;
            }
            else if(F3 == F3_ThreeState)
            {
                workdata |= F3_Bit_ThreeState;
            }
            else
            {
                return (false);
            }

            if(F2 == F2_Negative)
            {
                workdata |= F2_Bit_Negative;
            }
            else if(F2 == F2_Positive)
            {
                workdata |= F2_Bit_Positive;
            }
            else
            {
                return (false);
            }

            if(M == M_ThreeStateOutput)
            {
                workdata |= M_Bit_ThreeStateOutput;
            }
            else if(M == M_DigitalLockDetect)
            {
                workdata |= M_Bit_DigitalLockDetect;
            }
            else if (M == M_NDividerOutput)
            {
                workdata |= M_Bit_NDividerOutput;
            }
            else if (M == M_DVDD)
            {
                workdata |= M_Bit_DVDD;
            }
            else if (M == M_RDividerOutput)
            {
                workdata |= M_Bit_RDividerOutput;
            }
            else if (M == M_AnalogLockDetect)
            {
                workdata |= M_Bit_AnalogLockDetect;
            }
            else if (M == M_SerialDataOutput)
            {
                workdata |= M_Bit_SerialDataOutput;
            }
            else if (M == M_DGND)
            {
                workdata |= M_Bit_DGND;
            }
            else
            {
                return (false);
            }

            if(PD1 == PD1_Normal)
            {
                workdata |= PD1_Bit_Normal;
            }
            else if(PD1 == PD1_PowerDown)
            {
                workdata |= PD1_Bit_PowerDown;
            }
            else
            {
                return (false);
            }
            
            if(F1 == F1_Normal)
            {
                workdata |= F1_Bit_Normal;
            }
            else if(F1 == F1_Reset)
            {
                workdata |= F1_Bit_Reset;
            }
            else
            {
                return (false);
            }

            if (C == C_Function)
            {
                workdata |= C_Bit_Function;
            }
            else
            {
                return (false);
            }

            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            DecString = Convert.ToString(workdata, 10);
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');

            return (true);
        }
    }

    /// <summary>
    /// レジスタも出る(ADF4111)初期化ラッチ
    /// </summary>
    public class Model_Register_ADF4111_Initialize : Model_Register_ADF4111
    {
        /// <summary>
        /// ビットマスク
        /// </summary>
        private static readonly uint BitMask_P = 0b110000000000000000000000;
        private static readonly uint BitMask_PD2 = 0b001000000000000000000000;
        private static readonly uint BitMask_CPI2 = 0b000111000000000000000000;
        private static readonly uint BitMask_CPI1 = 0b000000111000000000000000;
        private static readonly uint BitMask_TC = 0b000000000111100000000000;
        private static readonly uint BitMask_F5F4 = 0b000000000000011000000000;
        private static readonly uint BitMask_F3 = 0b000000000000000100000000;
        private static readonly uint BitMask_F2 = 0b000000000000000010000000;
        private static readonly uint BitMask_M = 0b000000000000000001110000;
        private static readonly uint BitMask_PD1 = 0b000000000000000000001000;
        private static readonly uint BitMask_F1 = 0b000000000000000000000100;
        private static readonly uint BitMask_C = 0b000000000000000000000011;

        /// <summary>
        /// P
        /// </summary>
        private string _dataP;
        public string P
        {
            get { return _dataP; }
            set { SetProperty(ref _dataP, value); }
        }
        private static readonly string P_Null = string.Empty;
        private static readonly string P_Prescaler_8_9 = "8/9";
        private static readonly string P_Prescaler_16_17 = "16/17";
        private static readonly string P_Prescaler_32_33 = "32/33";
        private static readonly string P_Prescaler_64_65 = "64/65";
        private static readonly uint P_Bit_Prescaler_8_9 = 0b000000000000000000000000;
        private static readonly uint P_Bit_Prescaler_16_17 = 0b010000000000000000000000;
        private static readonly uint P_Bit_Prescaler_32_33 = 0b100000000000000000000000;
        private static readonly uint P_Bit_Prescaler_64_65 = 0b110000000000000000000000;

        /// <summary>
        /// PD2
        /// </summary>
        private string _dataPD2;
        public string PD2
        {
            get { return _dataPD2; }
            set { SetProperty(ref _dataPD2, value); }
        }
        private static readonly string PD2_Null = string.Empty;
        private static readonly string PD2_Async = "非同期";
        private static readonly string PD2_Sync = "同期";
        private static readonly uint PD2_Bit_Async = 0b000000000000000000000000;
        private static readonly uint PD2_Bit_Sync = 0b001000000000000000000000;

        /// <summary>
        /// CPI2
        /// </summary>
        private string _dataCPI2;
        public string CPI2
        {
            get { return _dataCPI2; }
            set { SetProperty(ref _dataCPI2, value); }
        }
        private static readonly string CPI2_Null = string.Empty;
        private static readonly string CPI2_000 = "2.7kΩ：1.09mA / 4.7kΩ：0.63mA / 10kΩ：0.29mA";
        private static readonly string CPI2_001 = "2.7kΩ：2.18mA / 4.7kΩ：1.25mA / 10kΩ：0.59mA";
        private static readonly string CPI2_010 = "2.7kΩ：3.26mA / 4.7kΩ：1.88mA / 10kΩ：0.88mA";
        private static readonly string CPI2_011 = "2.7kΩ：4.35mA / 4.7kΩ：2.50mA / 10kΩ：1.76mA";
        private static readonly string CPI2_100 = "2.7kΩ：5.44mA / 4.7kΩ：3.13mA / 10kΩ：1.47mA";
        private static readonly string CPI2_101 = "2.7kΩ：6.53mA / 4.7kΩ：3.75mA / 10kΩ：1.76mA";
        private static readonly string CPI2_110 = "2.7kΩ：7.62mA / 4.7kΩ：4.38mA / 10kΩ：2.06mA";
        private static readonly string CPI2_111 = "2.7kΩ：8.70mA / 4.7kΩ：5.00mA / 10kΩ：2.35mA";
        private static readonly uint CPI2_Bit_000 = 0b000000000000000000000000;
        private static readonly uint CPI2_Bit_001 = 0b000001000000000000000000;
        private static readonly uint CPI2_Bit_010 = 0b000010000000000000000000;
        private static readonly uint CPI2_Bit_011 = 0b000011000000000000000000;
        private static readonly uint CPI2_Bit_100 = 0b000100000000000000000000;
        private static readonly uint CPI2_Bit_101 = 0b000101000000000000000000;
        private static readonly uint CPI2_Bit_110 = 0b000110000000000000000000;
        private static readonly uint CPI2_Bit_111 = 0b000111000000000000000000;

        /// <summary>
        /// CPI1
        /// </summary>
        private string _dataCPI1;
        public string CPI1
        {
            get { return _dataCPI1; }
            set { SetProperty(ref _dataCPI1, value); }
        }
        private static readonly string CPI1_Null = string.Empty;
        private static readonly string CPI1_000 = "2.7kΩ：1.09mA / 4.7kΩ：0.63mA / 10kΩ：0.29mA";
        private static readonly string CPI1_001 = "2.7kΩ：2.18mA / 4.7kΩ：1.25mA / 10kΩ：0.59mA";
        private static readonly string CPI1_010 = "2.7kΩ：3.26mA / 4.7kΩ：1.88mA / 10kΩ：0.88mA";
        private static readonly string CPI1_011 = "2.7kΩ：4.35mA / 4.7kΩ：2.50mA / 10kΩ：1.76mA";
        private static readonly string CPI1_100 = "2.7kΩ：5.44mA / 4.7kΩ：3.13mA / 10kΩ：1.47mA";
        private static readonly string CPI1_101 = "2.7kΩ：6.53mA / 4.7kΩ：3.75mA / 10kΩ：1.76mA";
        private static readonly string CPI1_110 = "2.7kΩ：7.62mA / 4.7kΩ：4.38mA / 10kΩ：2.06mA";
        private static readonly string CPI1_111 = "2.7kΩ：8.70mA / 4.7kΩ：5.00mA / 10kΩ：2.35mA";
        private static readonly uint CPI1_Bit_000 = 0b000000000000000000000000;
        private static readonly uint CPI1_Bit_001 = 0b000000001000000000000000;
        private static readonly uint CPI1_Bit_010 = 0b000000010000000000000000;
        private static readonly uint CPI1_Bit_011 = 0b000000011000000000000000;
        private static readonly uint CPI1_Bit_100 = 0b000000100000000000000000;
        private static readonly uint CPI1_Bit_101 = 0b000000101000000000000000;
        private static readonly uint CPI1_Bit_110 = 0b000000110000000000000000;
        private static readonly uint CPI1_Bit_111 = 0b000000111000000000000000;

        /// <summary>
        /// TC
        /// </summary>
        private string _dataTC;
        public string TC
        {
            get { return _dataTC; }
            set { SetProperty(ref _dataTC, value); }
        }
        private static readonly string TC_Null = string.Empty;
        private static readonly string TC_0000 = "3";
        private static readonly string TC_0001 = "7";
        private static readonly string TC_0010 = "11";
        private static readonly string TC_0011 = "15";
        private static readonly string TC_0100 = "19";
        private static readonly string TC_0101 = "23";
        private static readonly string TC_0110 = "27";
        private static readonly string TC_0111 = "31";
        private static readonly string TC_1000 = "35";
        private static readonly string TC_1001 = "39";
        private static readonly string TC_1010 = "43";
        private static readonly string TC_1011 = "58";
        private static readonly string TC_1100 = "51";
        private static readonly string TC_1101 = "55";
        private static readonly string TC_1110 = "59";
        private static readonly string TC_1111 = "63";
        private static readonly uint TC_Bit_0000 = 0b000000000000000000000000;
        private static readonly uint TC_Bit_0001 = 0b000000000000100000000000;
        private static readonly uint TC_Bit_0010 = 0b000000000001000000000000;
        private static readonly uint TC_Bit_0011 = 0b000000000001100000000000;
        private static readonly uint TC_Bit_0100 = 0b000000000010000000000000;
        private static readonly uint TC_Bit_0101 = 0b000000000010100000000000;
        private static readonly uint TC_Bit_0110 = 0b000000000011000000000000;
        private static readonly uint TC_Bit_0111 = 0b000000000011100000000000;
        private static readonly uint TC_Bit_1000 = 0b000000000100000000000000;
        private static readonly uint TC_Bit_1001 = 0b000000000100100000000000;
        private static readonly uint TC_Bit_1010 = 0b000000000101000000000000;
        private static readonly uint TC_Bit_1011 = 0b000000000101100000000000;
        private static readonly uint TC_Bit_1100 = 0b000000000110000000000000;
        private static readonly uint TC_Bit_1101 = 0b000000000110100000000000;
        private static readonly uint TC_Bit_1110 = 0b000000000111000000000000;
        private static readonly uint TC_Bit_1111 = 0b000000000111100000000000;

        /// <summary>
        /// F5F4
        /// </summary>
        private string _dataF5F4;
        public string F5F4
        {
            get { return _dataF5F4; }
            set { SetProperty(ref _dataF5F4, value); }
        }
        private static readonly string F5F4_Null = string.Empty;
        private static readonly string F5F4_Disable1 = "高速ロック・ディスエーブル(00)";
        private static readonly string F5F4_Mode1 = "高速ロック・モード1";
        private static readonly string F5F4_Disable2 = "高速ロック・ディスエーブル(10)";
        private static readonly string F5F4_Mode2 = "高速ロック・モード2";
        private static readonly uint F5F4_Bit_Disable1 = 0b000000000000000000000000;
        private static readonly uint F5F4_Bit_Mode1 = 0b000000000000001000000000;
        private static readonly uint F5F4_Bit_Disable2 = 0b000000000000010000000000;
        private static readonly uint F5F4_Bit_Mode2 = 0b000000000000011000000000;

        /// <summary>
        /// F3
        /// </summary>
        private string _dataF3;
        public string F3
        {
            get { return _dataF3; }
            set { SetProperty(ref _dataF3, value); }
        }
        private static readonly string F3_Null = string.Empty;
        private static readonly string F3_Normal = "通常";
        private static readonly string F3_ThreeState = "スリーステート";
        private static readonly uint F3_Bit_Normal = 0b000000000000000000000000;
        private static readonly uint F3_Bit_ThreeState = 0b000000000000000100000000;

        /// <summary>
        /// F2
        /// </summary>
        private string _dataF2;
        public string F2
        {
            get { return _dataF2; }
            set { SetProperty(ref _dataF2, value); }
        }
        private static readonly string F2_Null = string.Empty;
        private static readonly string F2_Negative = "負極性";
        private static readonly string F2_Positive = "正極性";
        private static readonly uint F2_Bit_Negative = 0b000000000000000000000000;
        private static readonly uint F2_Bit_Positive = 0b000000000000000010000000;

        /// <summary>
        /// M
        /// </summary>
        private string _dataM;
        public string M
        {
            get { return _dataM; }
            set { SetProperty(ref _dataM, value); }
        }
        private static readonly string M_Null = string.Empty;
        private static readonly string M_ThreeStateOutput = "スリーステート出力";
        private static readonly string M_DigitalLockDetect = "デジタル・ロック検出";
        private static readonly string M_NDividerOutput = "Nデバイダ出力";
        private static readonly string M_DVDD = "DVDD";
        private static readonly string M_RDividerOutput = "Rデバイダ出力";
        private static readonly string M_AnalogLockDetect = "アナログ・ロック検出";
        private static readonly string M_SerialDataOutput = "シリアル・データ出力";
        private static readonly string M_DGND = "DGND";
        private static readonly uint M_Bit_ThreeStateOutput = 0b000000000000000000000000;
        private static readonly uint M_Bit_DigitalLockDetect = 0b000000000000000000010000;
        private static readonly uint M_Bit_NDividerOutput = 0b000000000000000000100000;
        private static readonly uint M_Bit_DVDD = 0b000000000000000000110000;
        private static readonly uint M_Bit_RDividerOutput = 0b000000000000000001000000;
        private static readonly uint M_Bit_AnalogLockDetect = 0b000000000000000001010000;
        private static readonly uint M_Bit_SerialDataOutput = 0b000000000000000001100000;
        private static readonly uint M_Bit_DGND = 0b000000000000000001110000;

        /// <summary>
        /// PD1
        /// </summary>
        private string _dataPD1;
        public string PD1
        {
            get { return _dataPD1; }
            set { SetProperty(ref _dataPD1, value); }
        }
        private static readonly string PD1_Null = string.Empty;
        private static readonly string PD1_Normal = "通常動作";
        private static readonly string PD1_PowerDown = "パワーダウン";
        private static readonly uint PD1_Bit_Normal = 0b000000000000000000000000;
        private static readonly uint PD1_Bit_PowerDown = 0b000000000000000000001000;

        /// <summary>
        /// F1
        /// </summary>
        private string _dataF1;
        public string F1
        {
            get { return _dataF1; }
            set { SetProperty(ref _dataF1, value); }
        }
        private static readonly string F1_Null = string.Empty;
        private static readonly string F1_Normal = "通常";
        private static readonly string F1_Reset = "R、A、Bカウンタ・リセット状態";
        private static readonly uint F1_Bit_Normal = 0b000000000000000000000000;
        private static readonly uint F1_Bit_Reset = 0b000000000000000000000100;

        /// <summary>
        /// C
        /// </summary>
        private string _dataC;
        public string C
        {
            get { return _dataC; }
            set { SetProperty(ref _dataC, value); }
        }
        private static readonly string C_Null = string.Empty;
        private static readonly string C_Initialize = "初期化ラッチ";
        private static readonly uint C_Bit_Initialize = 0b000000000000000000000011;

        /// <summary>
        /// バインディングデータ：解析データ：Pのリスト
        /// </summary>
        private ObservableCollection<string> _listP;
        public ObservableCollection<string> ListP
        {
            get { return _listP; }
            set { SetProperty(ref _listP, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：PD2のリスト
        /// </summary>
        private ObservableCollection<string> _listPD2;
        public ObservableCollection<string> ListPD2
        {
            get { return _listPD2; }
            set { SetProperty(ref _listPD2, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：CPI2のリスト
        /// </summary>
        private ObservableCollection<string> _listCPI2;
        public ObservableCollection<string> ListCPI2
        {
            get { return _listCPI2; }
            set { SetProperty(ref _listCPI2, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：CPI1のリスト
        /// </summary>
        private ObservableCollection<string> _listCPI1;
        public ObservableCollection<string> ListCPI1
        {
            get { return _listCPI1; }
            set { SetProperty(ref _listCPI1, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：TCのリスト
        /// </summary>
        private ObservableCollection<string> _listTC;
        public ObservableCollection<string> ListTC
        {
            get { return _listTC; }
            set { SetProperty(ref _listTC, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F5F4のリスト
        /// </summary>
        private ObservableCollection<string> _listF5F4;
        public ObservableCollection<string> ListF5F4
        {
            get { return _listF5F4; }
            set { SetProperty(ref _listF5F4, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F3のリスト
        /// </summary>
        private ObservableCollection<string> _listF3;
        public ObservableCollection<string> ListF3
        {
            get { return _listF3; }
            set { SetProperty(ref _listF3, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F2のリスト
        /// </summary>
        private ObservableCollection<string> _listF2;
        public ObservableCollection<string> ListF2
        {
            get { return _listF2; }
            set { SetProperty(ref _listF2, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：Mのリスト
        /// </summary>
        private ObservableCollection<string> _listM;
        public ObservableCollection<string> ListM
        {
            get { return _listM; }
            set { SetProperty(ref _listM, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：PD1のリスト
        /// </summary>
        private ObservableCollection<string> _listPD1;
        public ObservableCollection<string> ListPD1
        {
            get { return _listPD1; }
            set { SetProperty(ref _listPD1, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：F1のリスト
        /// </summary>
        private ObservableCollection<string> _listF1;
        public ObservableCollection<string> ListF1
        {
            get { return _listF1; }
            set { SetProperty(ref _listF1, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：Cのリスト
        /// </summary>
        private ObservableCollection<string> _listC;
        public ObservableCollection<string> ListC
        {
            get { return _listC; }
            set { SetProperty(ref _listC, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model_Register_ADF4111_Initialize()
        {
            ListP = new ObservableCollection<string>()
            {
                P_Null,
                P_Prescaler_8_9,
                P_Prescaler_16_17,
                P_Prescaler_32_33,
                P_Prescaler_64_65
            };

            ListPD2 = new ObservableCollection<string>()
            {
                PD2_Null,
                PD2_Async,
                PD2_Sync
            };

            ListCPI2 = new ObservableCollection<string>()
            {
                CPI2_Null,
                CPI2_000,
                CPI2_001,
                CPI2_010,
                CPI2_011,
                CPI2_100,
                CPI2_101,
                CPI2_110,
                CPI2_111
            };

            ListCPI1 = new ObservableCollection<string>()
            {
                CPI1_Null,
                CPI1_000,
                CPI1_001,
                CPI1_010,
                CPI1_011,
                CPI1_100,
                CPI1_101,
                CPI1_110,
                CPI1_111
            };

            ListTC = new ObservableCollection<string>()
            {
                TC_Null,
                TC_0000,
                TC_0001,
                TC_0010,
                TC_0011,
                TC_0100,
                TC_0101,
                TC_0110,
                TC_0111,
                TC_1000,
                TC_1001,
                TC_1010,
                TC_1011,
                TC_1100,
                TC_1101,
                TC_1110,
                TC_1111
            };

            ListF5F4 = new ObservableCollection<string>()
            {
                F5F4_Null,
                F5F4_Disable1,
                F5F4_Mode1,
                F5F4_Disable2,
                F5F4_Mode2
            };

            ListF3 = new ObservableCollection<string>()
            {
                F3_Null,
                F3_Normal,
                F3_ThreeState
            };

            ListF2 = new ObservableCollection<string>()
            {
                F2_Null,
                F2_Negative,
                F2_Positive
            };

            ListM = new ObservableCollection<string>()
            {
                M_Null,
                M_ThreeStateOutput,
                M_DigitalLockDetect,
                M_NDividerOutput,
                M_DVDD,
                M_RDividerOutput,
                M_AnalogLockDetect,
                M_SerialDataOutput,
                M_DGND
            };

            ListF1 = new ObservableCollection<string>()
            {
                F1_Null,
                F1_Normal,
                F1_Reset
            };

            ListPD1 = new ObservableCollection<string>()
            {
                PD1_Null,
                PD1_Normal,
                PD1_PowerDown
            };

            ListC = new ObservableCollection<string>()
            {
                C_Null,
                C_Initialize
            };

            ConvertStringToSettings();
        }

        /// <summary>
        /// 設定データクリア処理
        /// </summary>
        public void ClearSettings()
        {
            P = P_Null;
            PD2 = PD2_Null;
            CPI2 = CPI2_Null;
            CPI1 = CPI1_Null;
            TC = TC_Null;
            F5F4 = F5F4_Null;
            F3 = F3_Null;
            F2 = F2_Null;
            M = M_Null;
            PD1 = PD1_Null;
            F1 = F1_Null;
            C = C_Null;
        }

        /// <summary>
        /// 設定数値→設定データ変換処理
        /// </summary>
        /// <returns変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertStringToSettings()
        {
            bool ret;
            uint workdata;  // 24bitレジスタのため、32bit用意する。

            try
            {
                // 2進数データをもとに各設定データの変換をする
                workdata = Convert.ToUInt32(BinString, 2);

                ConvertStringToSettingsP(workdata);
                ConvertStringToSettingsPD2(workdata);
                ConvertStringToSettingsCPI2(workdata);
                ConvertStringToSettingsCPI1(workdata);
                ConvertStringToSettingsTC(workdata);
                ConvertStringToSettingsF5F4(workdata);
                ConvertStringToSettingsF3(workdata);
                ConvertStringToSettingsF2(workdata);
                ConvertStringToSettingsM(workdata);
                ConvertStringToSettingsPD1(workdata);
                ConvertStringToSettingsF1(workdata);
                ConvertStringToSettingsC(workdata);

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(P)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsP(uint inputData)
        {
            uint workP = inputData & BitMask_P;

            if (workP == P_Bit_Prescaler_8_9)
            {
                P = P_Prescaler_8_9;
            }
            else if (workP == P_Bit_Prescaler_16_17)
            {
                P = P_Prescaler_16_17;
            }
            else if (workP == P_Bit_Prescaler_32_33)
            {
                P = P_Prescaler_32_33;
            }
            else if (workP == P_Bit_Prescaler_64_65)
            {
                P = P_Prescaler_64_65;
            }
            else
            {
                P = P_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(PD2)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsPD2(uint inputData)
        {
            uint workPD2 = inputData & BitMask_PD2;

            if (workPD2 == PD2_Bit_Async)
            {
                PD2 = PD2_Async;
            }
            else if (workPD2 == PD2_Bit_Sync)
            {
                PD2 = PD2_Sync;
            }
            else
            {
                PD2 = PD2_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(CPI2)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsCPI2(uint inputData)
        {
            uint workCPI2 = inputData & BitMask_CPI2;

            if (workCPI2 == CPI2_Bit_000)
            {
                CPI2 = CPI2_000;
            }
            else if (workCPI2 == CPI2_Bit_001)
            {
                CPI2 = CPI2_001;
            }
            else if (workCPI2 == CPI2_Bit_010)
            {
                CPI2 = CPI2_010;
            }
            else if (workCPI2 == CPI2_Bit_011)
            {
                CPI2 = CPI2_011;
            }
            else if (workCPI2 == CPI2_Bit_100)
            {
                CPI2 = CPI2_100;
            }
            else if (workCPI2 == CPI2_Bit_101)
            {
                CPI2 = CPI2_101;
            }
            else if (workCPI2 == CPI2_Bit_110)
            {
                CPI2 = CPI2_110;
            }
            else if (workCPI2 == CPI2_Bit_111)
            {
                CPI2 = CPI2_111;
            }
            else
            {
                CPI2 = CPI2_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(CPI1)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsCPI1(uint inputData)
        {
            uint workCPI1 = inputData & BitMask_CPI1;

            if (workCPI1 == CPI1_Bit_000)
            {
                CPI1 = CPI1_000;
            }
            else if (workCPI1 == CPI1_Bit_001)
            {
                CPI1 = CPI1_001;
            }
            else if (workCPI1 == CPI1_Bit_010)
            {
                CPI1 = CPI1_010;
            }
            else if (workCPI1 == CPI1_Bit_011)
            {
                CPI1 = CPI1_011;
            }
            else if (workCPI1 == CPI1_Bit_100)
            {
                CPI1 = CPI1_100;
            }
            else if (workCPI1 == CPI1_Bit_101)
            {
                CPI1 = CPI1_101;
            }
            else if (workCPI1 == CPI1_Bit_110)
            {
                CPI1 = CPI1_110;
            }
            else if (workCPI1 == CPI1_Bit_111)
            {
                CPI1 = CPI1_111;
            }
            else
            {
                CPI1 = CPI1_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(TC)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsTC(uint inputData)
        {
            uint workTC = inputData & BitMask_TC;

            if (workTC == TC_Bit_0000)
            {
                TC = TC_0000;
            }
            else if (workTC == TC_Bit_0001)
            {
                TC = TC_0001;
            }
            else if (workTC == TC_Bit_0010)
            {
                TC = TC_0010;
            }
            else if (workTC == TC_Bit_0011)
            {
                TC = TC_0011;
            }
            else if (workTC == TC_Bit_0100)
            {
                TC = TC_0100;
            }
            else if (workTC == TC_Bit_0101)
            {
                TC = TC_0101;
            }
            else if (workTC == TC_Bit_0110)
            {
                TC = TC_0110;
            }
            else if (workTC == TC_Bit_0111)
            {
                TC = TC_0111;
            }
            else if (workTC == TC_Bit_1000)
            {
                TC = TC_1000;
            }
            else if (workTC == TC_Bit_1001)
            {
                TC = TC_1001;
            }
            else if (workTC == TC_Bit_1010)
            {
                TC = TC_1010;
            }
            else if (workTC == TC_Bit_1011)
            {
                TC = TC_1011;
            }
            else if (workTC == TC_Bit_1100)
            {
                TC = TC_1100;
            }
            else if (workTC == TC_Bit_1101)
            {
                TC = TC_1101;
            }
            else if (workTC == TC_Bit_1110)
            {
                TC = TC_1110;
            }
            else if (workTC == TC_Bit_1111)
            {
                TC = TC_1111;
            }
            else
            {
                TC = TC_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F5F4)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF5F4(uint inputData)
        {
            uint workF5F4 = inputData & BitMask_F5F4;

            if (workF5F4 == F5F4_Bit_Disable1)
            {
                F5F4 = F5F4_Disable1;
            }
            else if (workF5F4 == F5F4_Bit_Mode1)
            {
                F5F4 = F5F4_Mode1;
            }
            else if (workF5F4 == F5F4_Bit_Disable2)
            {
                F5F4 = F5F4_Disable2;
            }
            else if (workF5F4 == F5F4_Bit_Mode2)
            {
                F5F4 = F5F4_Mode2;
            }
            else
            {
                F5F4 = F5F4_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F3)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF3(uint inputData)
        {
            uint workF3 = inputData & BitMask_F3;

            if (workF3 == F3_Bit_Normal)
            {
                F3 = F3_Normal;
            }
            else if (workF3 == F3_Bit_ThreeState)
            {
                F3 = F3_ThreeState;
            }
            else
            {
                F3 = F3_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F2)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF2(uint inputData)
        {
            uint workF2 = inputData & BitMask_F2;

            if (workF2 == F2_Bit_Negative)
            {
                F2 = F2_Negative;
            }
            else if (workF2 == F2_Bit_Positive)
            {
                F2 = F2_Positive;
            }
            else
            {
                F2 = F2_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(M)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsM(uint inputData)
        {
            uint workM = inputData & BitMask_M;

            if (workM == M_Bit_ThreeStateOutput)
            {
                M = M_ThreeStateOutput;
            }
            else if (workM == M_Bit_DigitalLockDetect)
            {
                M = M_DigitalLockDetect;
            }
            else if (workM == M_Bit_NDividerOutput)
            {
                M = M_NDividerOutput;
            }
            else if (workM == M_Bit_DVDD)
            {
                M = M_DVDD;
            }
            else if (workM == M_Bit_RDividerOutput)
            {
                M = M_RDividerOutput;
            }
            else if (workM == M_Bit_AnalogLockDetect)
            {
                M = M_AnalogLockDetect;
            }
            else if (workM == M_Bit_SerialDataOutput)
            {
                M = M_SerialDataOutput;
            }
            else if (workM == M_Bit_DGND)
            {
                M = M_DGND;
            }
            else
            {
                M = M_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(PD1)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsPD1(uint inputData)
        {
            uint workPD1 = inputData & BitMask_PD1;

            if (workPD1 == PD1_Bit_Normal)
            {
                PD1 = PD1_Normal;
            }
            else if (workPD1 == PD1_Bit_PowerDown)
            {
                PD1 = PD1_PowerDown;
            }
            else
            {
                PD1 = PD1_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(F1)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsF1(uint inputData)
        {
            uint workF1 = inputData & BitMask_F1;

            if (workF1 == F1_Bit_Normal)
            {
                F1 = F1_Normal;
            }
            else if (workF1 == F1_Bit_Reset)
            {
                F1 = F1_Reset;
            }
            else
            {
                F1 = F1_Null;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(C)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsC(uint inputData)
        {
            uint workC = inputData & BitMask_C;

            if (workC == C_Bit_Initialize)
            {
                C = C_Initialize;
            }
            else
            {
                C = C_Null;
            }
        }

        /// <summary>
        /// 設定データ→設定数値変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertSettingsToString()
        {
            uint workdata = 0;  // 24bitレジスタのため、32bit用意する。

            // 各設定値を設定して、数値変換する
            if (P == P_Prescaler_8_9)
            {
                workdata |= P_Bit_Prescaler_8_9;
            }
            else if (P == P_Prescaler_16_17)
            {
                workdata |= P_Bit_Prescaler_16_17;
            }
            else if (P == P_Prescaler_32_33)
            {
                workdata |= P_Bit_Prescaler_32_33;
            }
            else if (P == P_Prescaler_64_65)
            {
                workdata |= P_Bit_Prescaler_64_65;
            }
            else
            {
                return (false);
            }

            if (PD2 == PD2_Async)
            {
                workdata |= PD2_Bit_Async;
            }
            else if (PD2 == PD2_Sync)
            {
                workdata |= PD2_Bit_Sync;
            }
            else
            {
                return (false);
            }

            if (CPI2 == CPI2_000)
            {
                workdata |= CPI2_Bit_000;
            }
            else if (CPI2 == CPI2_001)
            {
                workdata |= CPI2_Bit_001;
            }
            else if (CPI2 == CPI2_010)
            {
                workdata |= CPI2_Bit_010;
            }
            else if (CPI2 == CPI2_011)
            {
                workdata |= CPI2_Bit_011;
            }
            else if (CPI2 == CPI2_100)
            {
                workdata |= CPI2_Bit_100;
            }
            else if (CPI2 == CPI2_101)
            {
                workdata |= CPI2_Bit_101;
            }
            else if (CPI2 == CPI2_110)
            {
                workdata |= CPI2_Bit_110;
            }
            else if (CPI2 == CPI2_111)
            {
                workdata |= CPI2_Bit_111;
            }
            else
            {
                return (false);
            }

            if (CPI1 == CPI1_000)
            {
                workdata |= CPI1_Bit_000;
            }
            else if (CPI1 == CPI1_001)
            {
                workdata |= CPI1_Bit_001;
            }
            else if (CPI1 == CPI1_010)
            {
                workdata |= CPI1_Bit_010;
            }
            else if (CPI1 == CPI1_011)
            {
                workdata |= CPI1_Bit_011;
            }
            else if (CPI1 == CPI1_100)
            {
                workdata |= CPI1_Bit_100;
            }
            else if (CPI1 == CPI1_101)
            {
                workdata |= CPI1_Bit_101;
            }
            else if (CPI1 == CPI1_110)
            {
                workdata |= CPI1_Bit_110;
            }
            else if (CPI1 == CPI1_111)
            {
                workdata |= CPI1_Bit_111;
            }
            else
            {
                return (false);
            }

            if (TC == TC_0000)
            {
                workdata |= TC_Bit_0000;
            }
            else if (TC == TC_0001)
            {
                workdata |= TC_Bit_0001;
            }
            else if (TC == TC_0010)
            {
                workdata |= TC_Bit_0010;
            }
            else if (TC == TC_0011)
            {
                workdata |= TC_Bit_0011;
            }
            else if (TC == TC_0100)
            {
                workdata |= TC_Bit_0100;
            }
            else if (TC == TC_0101)
            {
                workdata |= TC_Bit_0101;
            }
            else if (TC == TC_0110)
            {
                workdata |= TC_Bit_0110;
            }
            else if (TC == TC_0111)
            {
                workdata |= TC_Bit_0111;
            }
            else if (TC == TC_1000)
            {
                workdata |= TC_Bit_1000;
            }
            else if (TC == TC_1001)
            {
                workdata |= TC_Bit_1001;
            }
            else if (TC == TC_1010)
            {
                workdata |= TC_Bit_1010;
            }
            else if (TC == TC_1011)
            {
                workdata |= TC_Bit_1011;
            }
            else if (TC == TC_1100)
            {
                workdata |= TC_Bit_1100;
            }
            else if (TC == TC_1101)
            {
                workdata |= TC_Bit_1101;
            }
            else if (TC == TC_1110)
            {
                workdata |= TC_Bit_1110;
            }
            else if (TC == TC_1111)
            {
                workdata |= TC_Bit_1111;
            }
            else
            {
                return (false);
            }

            if (F5F4 == F5F4_Disable1)
            {
                workdata |= F5F4_Bit_Disable1;
            }
            else if (F5F4 == F5F4_Mode1)
            {
                workdata |= F5F4_Bit_Mode1;
            }
            else if (F5F4 == F5F4_Disable2)
            {
                workdata |= F5F4_Bit_Disable2;
            }
            else if (F5F4 == F5F4_Mode2)
            {
                workdata |= F5F4_Bit_Mode2;
            }
            else
            {
                return (false);
            }

            if (F3 == F3_Normal)
            {
                workdata |= F3_Bit_Normal;
            }
            else if (F3 == F3_ThreeState)
            {
                workdata |= F3_Bit_ThreeState;
            }
            else
            {
                return (false);
            }

            if (F2 == F2_Negative)
            {
                workdata |= F2_Bit_Negative;
            }
            else if (F2 == F2_Positive)
            {
                workdata |= F2_Bit_Positive;
            }
            else
            {
                return (false);
            }

            if (M == M_ThreeStateOutput)
            {
                workdata |= M_Bit_ThreeStateOutput;
            }
            else if (M == M_DigitalLockDetect)
            {
                workdata |= M_Bit_DigitalLockDetect;
            }
            else if (M == M_NDividerOutput)
            {
                workdata |= M_Bit_NDividerOutput;
            }
            else if (M == M_DVDD)
            {
                workdata |= M_Bit_DVDD;
            }
            else if (M == M_RDividerOutput)
            {
                workdata |= M_Bit_RDividerOutput;
            }
            else if (M == M_AnalogLockDetect)
            {
                workdata |= M_Bit_AnalogLockDetect;
            }
            else if (M == M_SerialDataOutput)
            {
                workdata |= M_Bit_SerialDataOutput;
            }
            else if (M == M_DGND)
            {
                workdata |= M_Bit_DGND;
            }
            else
            {
                return (false);
            }

            if (PD1 == PD1_Normal)
            {
                workdata |= PD1_Bit_Normal;
            }
            else if (PD1 == PD1_PowerDown)
            {
                workdata |= PD1_Bit_PowerDown;
            }
            else
            {
                return (false);
            }

            if (F1 == F1_Normal)
            {
                workdata |= F1_Bit_Normal;
            }
            else if (F1 == F1_Reset)
            {
                workdata |= F1_Bit_Reset;
            }
            else
            {
                return (false);
            }

            if (C == C_Initialize)
            {
                workdata |= C_Bit_Initialize;
            }
            else
            {
                return (false);
            }

            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            DecString = Convert.ToString(workdata, 10);
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');

            return (true);
        }
    }
}
