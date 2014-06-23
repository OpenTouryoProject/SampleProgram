using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>
    /// ソースクラス（バリデーション付）
    /// </summary>
    public class SourceClassWithValid : IDataErrorInfo
    {
        /// <summary>ソースプロパティ１</summary>
        private int _sourceProperty1;

        /// <summary>ソースプロパティ１</summary>
        public int SourceProperty1
        {
            get { return this._sourceProperty1; }
            set { this._sourceProperty1 = value; }
        }

        /// <summary>ソースプロパティ２</summary>
        private int _sourceProperty2;

        /// <summary>ソースプロパティ２</summary>
        public int SourceProperty2
        {
            get { return this._sourceProperty2; }
            set { this._sourceProperty2 = value; }
        }

        /// <summary>ソースプロパティ３</summary>
        private int _sourceProperty3;

        /// <summary>ソースプロパティ３</summary>
        public int SourceProperty3
        {
            get { return this._sourceProperty3; }
            set { this._sourceProperty3 = value; }
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        public SourceClassWithValid(int i, int j, int k)
        {
            this._sourceProperty1 = i;
            this._sourceProperty2 = j;
            this._sourceProperty3 = k;
        }

        /// <summary>
        /// オブジェクトに関するエラー
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>プロパティに関するエラー</summary>
        /// <param name="name">プロパティ名</param>
        /// <returns>メッセージ</returns>
        public string this[string name]
        {
            get
            {
                if (name =="SourceProperty1")
                {
                    // 負の値のみ正常とする。
                    // 型変換エラーは、ExceptionValidationRuleでチェックする。

                    if (this._sourceProperty1 < 0)
                    {
                        // 負の値
                        return null;
                    }
                    else
                    {
                        // ０ or 正の値
                        return "負でない";
                    }
                }

                if (name == "SourceProperty2")
                {
                    // 負の値のみ正常とする。
                    // 型変換エラーは、ExceptionValidationRuleでチェックする。

                    if (this._sourceProperty2 < 0)
                    {
                        // 負の値
                        return null;
                    }
                    else
                    {
                        // ０ or 正の値
                        return "負でない";
                    }
                }

                if (name == "SourceProperty3")
                {
                    // 負の値のみ正常とする。
                    // 型変換エラーは、ExceptionValidationRuleでチェックする。

                    if (this._sourceProperty3 < 0)
                    {
                        // 負の値
                        return null;
                    }
                    else
                    {
                        // ０ or 正の値
                        return "負でない";
                    }
                }

                return null;
            }
        }
    }
}
