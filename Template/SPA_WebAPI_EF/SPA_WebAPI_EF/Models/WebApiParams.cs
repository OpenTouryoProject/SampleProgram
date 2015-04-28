//**********************************************************************************
//* クラス名        ：WebApiParams
//*
//* 作成日時        ：－
//* 作成者          ：－
//* 更新履歴        ：－
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
//**********************************************************************************

// system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA_WebAPI_EF.Models
{
    /// <summary>
    ///  WebApiParams class
    /// </summary>
    public class WebApiParams
    {
        /// <summary>汎用エリア</summary>
        public object Obj;

        /// <summary>
        /// データアクセス プロバイダ
        /// </summary>
        public string ddlDap { get; set; }

        /// <summary>
        /// クエリモード
        /// </summary>
        public string ddlMode2 { get; set; }

        /// <summary>
        /// 並び替え対象列
        /// </summary>
        public string OrderColumn { get; set; }

        /// <summary>
        /// 降順・昇順
        /// </summary>
        public string OrderSequence { get; set; }

        /// <summary>
        /// 荷主 ID
        /// </summary>
        public int ShipperId { get; set; }

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone { get; set; }
    }
}