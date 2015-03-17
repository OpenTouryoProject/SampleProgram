//**********************************************************************************
//* クラス名        ：FilterConfig
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
using System.Web;
using System.Web.Mvc;

namespace SPA_WebAPI_EF
{
    /// <summary>
    ///  FilterConfig class
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        ///  Regster Global Filters
        /// </summary>
        /// <param name="filters">filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}