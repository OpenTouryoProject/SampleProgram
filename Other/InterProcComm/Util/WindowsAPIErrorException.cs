//**********************************************************************************
//* All Rights Reserved, Copyright (C) 2007,2010 Hitachi Solutions,Ltd.
//**********************************************************************************

//**********************************************************************************
//* クラス名        ：WindowsAPIErrorException
//* クラス日本語名  ：Win32共通例外
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2010/10/26  西野  大介        新規作成
//**********************************************************************************

using System;

namespace Touryo.Infrastructure.Public.Win32
{
    /// <summary>Win32共通例外</summary>
    public class WindowsAPIErrorException : ApplicationException
    {
        /// <summary>エラーメッセージのテンプレート</summary>
        public static readonly string MessageTemplate
            = "Windows APIの{0}関数呼び出しに失敗しました。";

        /// <summary>ErrorCodes</summary>
        private CmnWin32.ErrorCodes _errorCode = 0;

        /// <summary>ErrorCodes</summary>
        public CmnWin32.ErrorCodes ErrorCode
        {
            get
            {
                return this._errorCode;
            }
        }

        /// <summary>コンストラクタ１</summary>
        /// <param name="error">ErrorCodes</param>
        public WindowsAPIErrorException(CmnWin32.ErrorCodes error)
            : base()
        {
            this._errorCode = error;
        }

        /// <summary>コンストラクタ２</summary>
        /// <param name="error">ErrorCodes</param>
        /// <param name="message">メッセージ</param>
        public WindowsAPIErrorException(
            CmnWin32.ErrorCodes error, string message)
            : base(message)
        {
            this._errorCode = error;
        }

        /// <summary>コンストラクタ３</summary>
        /// <param name="error">ErrorCodes</param>
        /// <param name="message">メッセージ</param>
        /// <param name="inner">内部例外</param>
        public WindowsAPIErrorException(
            CmnWin32.ErrorCodes error, string message, System.Exception inner)
            : base(message, inner)
        {
            this._errorCode = error;
        }

        /// <summary>コンストラクタ４</summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        /// <remarks>
        /// リモーティング サーバからの伝播に必要なコンストラクタ。
        /// </remarks>
        protected WindowsAPIErrorException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
