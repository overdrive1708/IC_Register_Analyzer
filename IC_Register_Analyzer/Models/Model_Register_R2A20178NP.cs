﻿using System;
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
        /// 有効ビットマスク(12bit)
        /// </summary>
        private static readonly ushort ValidBitMask = 0b111111111111;

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
            set { SetProperty(ref _binString, value); }
        }
        private static readonly string BinString_Init = "000000000000";
        private static readonly int BinString_MaxDigit = 12;

        /// <summary>
        /// ビットマスク
        /// </summary>
        private static readonly ushort BitMask_DACSelectData = 0b111100000000;
        private static readonly ushort BitMask_DACData = 0b000011111111;

        /// <summary>
        /// DACセレクトデータ
        /// </summary>
        private string _dataDACSelectData;
        public string DACSelectData
        {
            get { return _dataDACSelectData; }
            set { SetProperty(ref _dataDACSelectData, value); }
        }
        private static readonly string DACSelectData_Null = string.Empty;
        private static readonly string DACSelectData_VOUT1 = "VOUT1選択";
        private static readonly string DACSelectData_VOUT2 = "VOUT2選択";
        private static readonly string DACSelectData_VOUT3 = "VOUT3選択";
        private static readonly string DACSelectData_VOUT4 = "VOUT4選択";
        private static readonly string DACSelectData_VOUT5 = "VOUT5選択";
        private static readonly string DACSelectData_VOUT6 = "VOUT6選択";
        private static readonly string DACSelectData_VOUT7 = "VOUT7選択";
        private static readonly string DACSelectData_VOUT8 = "VOUT8選択";
        private static readonly string DACSelectData_DoNotCare = "Don't care";
        private static readonly ushort DACSelectData_Bit_VOUT1 = 0b100000000000;
        private static readonly ushort DACSelectData_Bit_VOUT2 = 0b010000000000;
        private static readonly ushort DACSelectData_Bit_VOUT3 = 0b110000000000;
        private static readonly ushort DACSelectData_Bit_VOUT4 = 0b001000000000;
        private static readonly ushort DACSelectData_Bit_VOUT5 = 0b101000000000;
        private static readonly ushort DACSelectData_Bit_VOUT6 = 0b011000000000;
        private static readonly ushort DACSelectData_Bit_VOUT7 = 0b111000000000;
        private static readonly ushort DACSelectData_Bit_VOUT8 = 0b000100000000;

        /// <summary>
        /// DACデータ
        /// </summary>
        private string _dataDACData;
        public string DACData
        {
            get { return _dataDACData; }
            set { SetProperty(ref _dataDACData, value); }
        }
        private static readonly string DACData_Null = string.Empty;
        private static readonly uint DACData_Max_Threshold = 255;

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
            ushort workdata;  // 12bitレジスタのため、16bit用意する。

            try
            {
                // 入力値が12bitを超えた場合は12bitMAXでクランプ
                if (Convert.ToUInt16(HexString, 16) <= ValidBitMask)
                {
                    workdata = Convert.ToUInt16(HexString, 16);
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
            ushort workdata;  // 12bitレジスタのため、16bit用意する。

            try
            {
                // 入力値が12bitを超えた場合は12bitMAXでクランプ
                if (Convert.ToUInt16(DecString, 10) <= ValidBitMask)
                {
                    workdata = Convert.ToUInt16(DecString, 10);
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
            ushort workdata;  // 12bitレジスタのため、16bit用意する。

            try
            {
                // 入力値が12bitを超えた場合は12bitMAXでクランプ
                if (Convert.ToUInt16(BinString, 2) <= ValidBitMask)
                {
                    workdata = Convert.ToUInt16(BinString, 2);
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

        /// <summary>
        /// 設定データクリア処理
        /// </summary>
        public void ClearSettings()
        {
            DACSelectData = DACSelectData_Null;
            DACData = DACData_Null;
        }

        /// <summary>
        /// 設定数値→設定データ変換処理
        /// </summary>
        /// <returns変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertStringToSettings()
        {
            bool ret;
            ushort workdata;  // 12bitレジスタのため、16bit用意する。

            try
            {
                // 2進数データをもとに各設定データの変換をする
                workdata = Convert.ToUInt16(BinString, 2);

                ConvertStringToSettingsDACSelectData(workdata);
                ConvertStringToSettingsDACData(workdata);

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return (ret);
        }

        /// <summary>
        /// 設定数値→設定データ変換処理(DACセレクトデータ)
        /// </summary>
        /// <param name="inputData">設定数値</param>
        private void ConvertStringToSettingsDACSelectData(ushort inputData)
        {
            ushort workDACSelectData = (ushort)(inputData & BitMask_DACSelectData);

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
        private void ConvertStringToSettingsDACData(ushort inputData)
        {
            ushort workDacData = (ushort)(inputData & BitMask_DACData);
            
            DACData = Convert.ToString(workDacData, 10);
        }

        /// <summary>
        /// 設定データ→設定数値変換処理
        /// </summary>
        /// <returns>変換結果：成功(true)/失敗(false)</returns>
        public bool ConvertSettingsToString()
        {
            ushort workdata = 0;  // 12bitレジスタのため、16bit用意する。

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
                return (false);
            }

            try
            {
                if (Convert.ToUInt16(DACData, 10) <= DACData_Max_Threshold)
                {
                    workdata |= Convert.ToUInt16(DACData, 10);
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

            HexString = Convert.ToString(workdata, 16);
            HexString = HexString.PadLeft(HexString_MaxDigit, '0');
            DecString = Convert.ToString(workdata, 10);
            BinString = Convert.ToString(workdata, 2);
            BinString = BinString.PadLeft(BinString_MaxDigit, '0');

            return (true);
        }
    }
}
