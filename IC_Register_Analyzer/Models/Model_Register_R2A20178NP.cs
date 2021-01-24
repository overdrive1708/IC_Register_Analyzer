using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace IC_Register_Analyzer.Models
{
    /// <summary>
    /// レジスタモデル(R2A20178NP)
    /// </summary>
    public class Model_Register_R2A20178NP : BindableBase
    {
        /// <summary>
        /// 16進数文字列
        /// </summary>
        private string _hexString;
        public string HexString
        {
            get { return _hexString; }
            set { SetProperty(ref _hexString, value); }
        }
        private static readonly string HexString_Init = "000";
        private static readonly int HexString_MaxDigit = 3;
        
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
            set
            {
                SetProperty(ref _binString, value);
                ConvertBinStringToBitMap();
            }
        }
        private static readonly string BinString_Init = "000000000000";
        private static readonly int BinString_MaxDigit = 12;

        /// <summary>
        /// ビットマップデータ
        /// </summary>
        private uint[] _bitMapData = new uint[12];
        public uint[] BitMapData
        {
            get { return _bitMapData; }
            set { SetProperty(ref _bitMapData, value); }
        }

        /// <summary>
        /// ビットマスク
        /// </summary>
        private static readonly uint BitMask_DACSelectData = 0b111100000000;
        private static readonly uint BitMask_DACData = 0b000011111111;

        /// <summary>
        /// DACセレクトデータ
        /// </summary>
        private string _dataDACSelectData;
        public string DACSelectData
        {
            get { return _dataDACSelectData; }
            set { SetProperty(ref _dataDACSelectData, value); }
        }
        public static readonly string DACSelectData_Null = string.Empty;
        private static readonly string DACSelectData_VOUT1 = "VOUT1選択";
        private static readonly string DACSelectData_VOUT2 = "VOUT2選択";
        private static readonly string DACSelectData_VOUT3 = "VOUT3選択";
        private static readonly string DACSelectData_VOUT4 = "VOUT4選択";
        private static readonly string DACSelectData_VOUT5 = "VOUT5選択";
        private static readonly string DACSelectData_VOUT6 = "VOUT6選択";
        private static readonly string DACSelectData_VOUT7 = "VOUT7選択";
        private static readonly string DACSelectData_VOUT8 = "VOUT8選択";
        public static readonly string DACSelectData_DoNotCare = "Don't care";
        private static readonly uint DACSelectData_Bit_VOUT1 = 0b100000000000;
        private static readonly uint DACSelectData_Bit_VOUT2 = 0b010000000000;
        private static readonly uint DACSelectData_Bit_VOUT3 = 0b110000000000;
        private static readonly uint DACSelectData_Bit_VOUT4 = 0b001000000000;
        private static readonly uint DACSelectData_Bit_VOUT5 = 0b101000000000;
        private static readonly uint DACSelectData_Bit_VOUT6 = 0b011000000000;
        private static readonly uint DACSelectData_Bit_VOUT7 = 0b111000000000;
        private static readonly uint DACSelectData_Bit_VOUT8 = 0b000100000000;

        /// <summary>
        /// DACデータ
        /// </summary>
        private string _dataDACData;
        public string DACData
        {
            get { return _dataDACData; }
            set { SetProperty(ref _dataDACData, value); }
        }

        /// <summary>
        /// バインディングデータ：解析データ：DACセレクトデータのリスト
        /// </summary>
        private ObservableCollection<string> _listDACSelectData;
        public ObservableCollection<string> ListDACSelectData
        {
            get { return _listDACSelectData; }
            set { SetProperty(ref _listDACSelectData, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model_Register_R2A20178NP()
        {
            HexString = HexString_Init;
            DecString = DecString_Init;
            BinString = BinString_Init;

            ListDACSelectData = new ObservableCollection<string>()
            {
                DACSelectData_Null,
                DACSelectData_VOUT1,
                DACSelectData_VOUT2,
                DACSelectData_VOUT3,
                DACSelectData_VOUT4,
                DACSelectData_VOUT5,
                DACSelectData_VOUT6,
                DACSelectData_VOUT7,
                DACSelectData_VOUT8,
                DACSelectData_DoNotCare
            };

            ConvertStringToSettings();
        }

        /// <summary>
        /// 16進数文字列→その他文字列変換処理
        /// </summary>
        public void ConvertHexStringToOtherString()
        {
            uint workdata;

            // 数値変換
            workdata = Convert.ToUInt32(HexString, 16);

            // 基数変換
            DecString = Convert.ToString(workdata, 10);
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');
        }

        /// <summary>
        /// 10進数文字列→その他文字列変換処理
        /// </summary>
        public void ConvertDecStringToOtherString()
        {
            uint workdata;

            // 数値変換
            workdata = Convert.ToUInt32(DecString, 10);

            // 基数変換
            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');
        }

        /// <summary>
        /// 2進数文字列→その他文字列変換処理
        /// </summary>
        public void ConvertBinStringToOtherString()
        {
            uint workdata;

            // 数値変換
            workdata = Convert.ToUInt32(BinString, 2);

            // 基数変換
            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            DecString = Convert.ToString(workdata, 10);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理
        /// </summary>
        public void ConvertStringToSettings()
        {
            uint workdata;

            // 2進数データをもとに各設定データの変換をする
            workdata = Convert.ToUInt32(BinString, 2);

            ConvertStringToSettingsDACSelectData(workdata);
            ConvertStringToSettingsDACData(workdata);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(DACセレクトデータ)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsDACSelectData(uint inputData)
        {
            uint workDACSelectData = inputData & BitMask_DACSelectData;

            if (workDACSelectData == DACSelectData_Bit_VOUT1)
            {
                DACSelectData = DACSelectData_VOUT1;
            }
            else if (workDACSelectData == DACSelectData_Bit_VOUT2)
            {
                DACSelectData = DACSelectData_VOUT2;
            }
            else if (workDACSelectData == DACSelectData_Bit_VOUT3)
            {
                DACSelectData = DACSelectData_VOUT3;
            }
            else if (workDACSelectData == DACSelectData_Bit_VOUT4)
            {
                DACSelectData = DACSelectData_VOUT4;
            }
            else if (workDACSelectData == DACSelectData_Bit_VOUT5)
            {
                DACSelectData = DACSelectData_VOUT5;
            }
            else if (workDACSelectData == DACSelectData_Bit_VOUT6)
            {
                DACSelectData = DACSelectData_VOUT6;
            }
            else if (workDACSelectData == DACSelectData_Bit_VOUT7)
            {
                DACSelectData = DACSelectData_VOUT7;
            }
            else if (workDACSelectData == DACSelectData_Bit_VOUT8)
            {
                DACSelectData = DACSelectData_VOUT8;
            }
            else
            {
                DACSelectData = DACSelectData_DoNotCare;
            }
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(DACデータ)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsDACData(uint inputData)
        {
            uint workDacData = inputData & BitMask_DACData;
            
            DACData = Convert.ToString(workDacData, 10);
        }

        /// <summary>
        /// 設定データ→設定数値変換処理
        /// </summary>
        public void ConvertSettingsToString()
        {
            uint workdata = 0;

            // 各設定値を設定して、数値変換する
            if (DACSelectData == DACSelectData_VOUT1)
            {
                workdata |= DACSelectData_Bit_VOUT1;
            }
            else if (DACSelectData == DACSelectData_VOUT2)
            {
                workdata |= DACSelectData_Bit_VOUT2;
            }
            else if (DACSelectData == DACSelectData_VOUT3)
            {
                workdata |= DACSelectData_Bit_VOUT3;
            }
            else if (DACSelectData == DACSelectData_VOUT4)
            {
                workdata |= DACSelectData_Bit_VOUT4;
            }
            else if (DACSelectData == DACSelectData_VOUT5)
            {
                workdata |= DACSelectData_Bit_VOUT5;
            }
            else if (DACSelectData == DACSelectData_VOUT6)
            {
                workdata |= DACSelectData_Bit_VOUT6;
            }
            else if (DACSelectData == DACSelectData_VOUT7)
            {
                workdata |= DACSelectData_Bit_VOUT7;
            }
            else if (DACSelectData == DACSelectData_VOUT8)
            {
                workdata |= DACSelectData_Bit_VOUT8;
            }
            else
            {
                ;
            }

            workdata |= Convert.ToUInt32(DACData, 10);

            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            DecString = Convert.ToString(workdata, 10);
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');
        }

        /// <summary>
        /// 2進数文字列→ビットマップ変換処理
        /// </summary>
        private void ConvertBinStringToBitMap()
        {
            uint workdata;
            uint[] workBitMapData = new uint[12];

            // 数値変換
            workdata = Convert.ToUInt32(BinString, 2);

            for (int cnt = 0; cnt < 12; cnt++)
            {
                workBitMapData[cnt] = workdata & 0b1;
                workdata >>= 1;
            }

            BitMapData = workBitMapData;
        }
    }
}
