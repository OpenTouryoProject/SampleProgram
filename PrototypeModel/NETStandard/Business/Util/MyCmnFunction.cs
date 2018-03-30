//**********************************************************************************
//* Copyright (C) 2007,2016 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

//**********************************************************************************
//* クラス名        ：MyCmnFunction
//* クラス日本語名  ：Business層の共通クラス（テンプレート）
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成（テンプレート）
//*  2009/06/02  西野 大介         sln - IR版からの修正
//*                                ・#5  ： コントロール数取得処理（デフォルト値不正）
//*  2009/07/21  西野 大介         コントロール取得処理の仕様変更
//*  2009/08/10  西野 大介         同名のコントロール追加に対応（GridView/ItemTemplate）。
//*  2010/09/24  西野 大介         ジェネリック対応（Dictionary、List、Queue、Stack<T>）
//*                                nullチェック方法、Contains → ContainsKeyなどに注意
//*  2010/10/21  西野 大介         幾つかのイベント処理の正式対応（ベースクラス２→１へ）
//*  2012/06/14  西野 大介         コントロール検索の再帰処理性能の集約＆効率化。
//*  2014/05/16  西野 大介         キャスト可否チェックのロジックを見直した。
//*  2017/01/31  西野 大介         System.Webを使用しているCalculateSessionSizeメソッドをPublicから移動
//*  2018/03/29  西野 大介         .NET Standard対応で、削除機能に関連するメソッドを削除
//*  2018/03/29  西野 大介         .NET Standard対応で、HttpSessionのポーティング
//**********************************************************************************

using Touryo.Infrastructure.Public.IO;
using Touryo.Infrastructure.Framework.Migration;

using System;
using Microsoft.AspNetCore.Http;

namespace Touryo.Infrastructure.Business.Util
{
    /// <summary>Business層の共通クラス</summary>
    public class MyCmnFunction
    {
        #region CalculateSessionSize

        /// <summary>Sessionサイズ測定</summary>
        /// <returns>Sessionサイズ（MB）</returns>
        /// <remarks>シリアル化できないオブジェクトを含む場合は落ちる。</remarks>
        public static long CalculateSessionSizeMB()
        {
            //return MyCmnFunction.CalculateSessionSizeKB() / 1000;
            return (long)Math.Round(MyCmnFunction.CalculateSessionSize() / 1000000.0, 0, MidpointRounding.AwayFromZero);
        }

        /// <summary>Sessionサイズ測定</summary>
        /// <returns>Sessionサイズ（KB）</returns>
        /// <remarks>シリアル化できないオブジェクトを含む場合は落ちる。</remarks>
        public static long CalculateSessionSizeKB()
        {
            //return MyCmnFunction.CalculateSessionSize() / 1000;
            return (long)Math.Round(MyCmnFunction.CalculateSessionSize() / 1000.0, 0, MidpointRounding.AwayFromZero);
        }

        /// <summary>Sessionサイズ測定</summary>
        /// <returns>Sessionサイズ（バイト）</returns>
        /// <remarks>シリアル化できないオブジェクトを含む場合は落ちる。</remarks>
        public static long CalculateSessionSize()
        {
            // ワーク変数
            long size = 0;

            // SessionのオブジェクトをBinarySerializeしてサイズを取得。
            foreach (string key in MyHttpContext.Current.Session.Keys)
            {
                // 当該キーのオブジェクト・サイズを足しこむ。
                size += BinarySerialize.ObjectToBytes(MyHttpContext.Current.Session.GetString(key)).Length;
            }

            // Sessionサイズ（バイト）
            return size;
        }

        #endregion
    }
}
