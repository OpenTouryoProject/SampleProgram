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
//* クラス名        ：FxCmnFunction
//* クラス日本語名  ：Framework層の共通クラス
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2009/03/13  西野 大介         新規作成
//*  2009/06/02  西野 大介         sln - IR版からの修正
//*                                ・・・
//*  2018/01/31  西野 大介         ネストしたユーザ コントロールに対応（senderで親UCを確認する）
//*  2018/03/29  西野 大介         .NET Standard対応で、削除機能に関連するメソッドを削除
//**********************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;

using Touryo.Infrastructure.Framework.Migration;
using Touryo.Infrastructure.Framework.Exceptions;
using Touryo.Infrastructure.Public.Util;

namespace Touryo.Infrastructure.Framework.Util
{
    /// <summary>Framework層の共通クラス</summary>
    public class FxCmnFunction
    {
        #region 数定義（コンフィグ）の取得処理

        /// <summary>数定義（コンフィグ）の取得処理</summary>
        /// <param name="configKey">コンフィグのキー</param>
        /// <param name="defaultNum">デフォルト値</param>
        /// <returns>数</returns>
        public static int GetNumFromConfig(string configKey, int defaultNum)
        {
            int temp;

            // null チェック
            if (GetConfigParameter.GetConfigValue(configKey) == null)
            {
                // デフォルト値
                return defaultNum;
            }
            else
            {
                // int チェック
                if (int.TryParse(GetConfigParameter.GetConfigValue(configKey), out temp))
                {
                    // 変換完了
                    return temp;
                }
                else
                {
                    // 変換ミス
                    throw new FrameworkException(
                        FrameworkExceptionMessage.ERROR_IN_WRITING_OF_FX_NUMVAL[0],
                        String.Format(FrameworkExceptionMessage.ERROR_IN_WRITING_OF_FX_NUMVAL[1], configKey));
                }
            }
        }

        #endregion

        #region LRU的にキューを再構築

        /// <summary>GUIDをキーにして、LRU的にキューを再構築する。</summary>
        /// <param name="currentQueue">再構築するキュー</param>
        /// <param name="guid">キーにするGUIDの文字列</param>
        /// <param name="maxLength">キューの要素の最大値</param>
        /// <returns>再構築したキュー</returns>
        /// <remarks>要素が、そのままGUIDの場合</remarks>
        internal static Queue<string> RestructuringLRUQueue1(
            Queue<string> currentQueue, string guid, int maxLength)
        {
            // オーバーロード
            return FxCmnFunction.RestructuringLRUQueue1(currentQueue, guid, guid, maxLength);
        }

        /// <summary>GUIDをキーにして、LRU的にキューを再構築する（更新機能付き）。</summary>
        /// <param name="currentQueue">再構築するキュー</param>
        /// <param name="oldGuid">キーにするGUIDの文字列（検索用）</param>
        /// <param name="newGuid">キーにするGUIDの文字列（更新用）</param>
        /// <param name="maxLength">キューの要素の最大値</param>
        /// <returns>再構築したキュー</returns>
        /// <remarks>要素が、そのままGUIDの場合</remarks>
        internal static Queue<string> RestructuringLRUQueue1(
            Queue<string> currentQueue, string oldGuid, string newGuid, int maxLength)
        {
            //新しいキューの作成 
            Queue<string> tempQueue = new Queue<string>(maxLength);

            // 詰替ワーク
            string tempGuid = null;

            // 詰替発生フラグ
            bool flg = false;

            // 新しいキューへ詰替
            while (0 < currentQueue.Count)
            {
                // 元からデキュー
                tempGuid = (string)currentQueue.Dequeue();

                // GUIDを確認
                if (tempGuid == oldGuid)
                {
                    // oldGuid（更新時：newGuid）は、
                    // 先頭に移動させるため、後にエンキューする。
                    flg = true;
                }
                else
                {
                    // 詰め替え先にエンキュー
                    tempQueue.Enqueue(tempGuid);
                }
            }

            if (flg)
            {
                // 検索し、一致するGUIDを発見できた。

                // 検索したoldGuidを、先頭に移動。
                // （更新時は、newGuidを先頭に移動）
                tempQueue.Enqueue(newGuid);
            }
            else
            {
                // 検索し、一致するGUIDを発見できなかった。

                // 指定のguidは既に消失したものである場合。
                // ・ 自動削除機能の場合 → 無視する。
                // ・ 不正操作防止機能の場合 → 有り得ない。
            }

            // 再構築したキューを返す。
            return tempQueue;
        }

        /// <summary>GUIDをキーにして、LRU的にキューを再構築する。</summary>
        /// <param name="currentQueue">再構築するキュー</param>
        /// <param name="guid">キーにするGUIDの文字列</param>
        /// <param name="maxLength">キューの要素の最大値</param>
        /// <returns>再構築したキュー</returns>
        /// <remarks>要素がArrayListでArrayList[0]がGUIDの場合</remarks>
        internal static Queue<ArrayList> RestructuringLRUQueue2(
            Queue<ArrayList> currentQueue, string guid, int maxLength)
        {
            //新しいキューの作成 
            Queue<ArrayList> tempQueue = new Queue<ArrayList>(maxLength);

            // そのまま詰替える要素
            ArrayList tempArrayList = null;
            // 先頭に移動する要素
            ArrayList tempArrayListThis = null;

            // 新しいキューへ詰替
            while (0 < currentQueue.Count)
            {
                // 元からデキュー
                tempArrayList = (ArrayList)currentQueue.Dequeue();

                // GUIDを確認
                if (tempArrayList[0].ToString() == guid)
                {
                    // これ（tempArrayListThis）は、
                    // 先頭に移動させるため、後にエンキューする。
                    tempArrayListThis = tempArrayList;
                }
                else
                {
                    // 詰め替え先にエンキュー
                    tempQueue.Enqueue(tempArrayList);
                }
            }

            // これ（tempArrayListThis）を、先頭に移動
            tempQueue.Enqueue(tempArrayListThis);

            // 再構築したキューを返す。
            return tempQueue;
        }

        #endregion
    }
}
