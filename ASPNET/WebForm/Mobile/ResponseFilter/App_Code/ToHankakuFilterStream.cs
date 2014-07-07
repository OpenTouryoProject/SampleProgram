//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
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

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;

using System.IO;
using System.Text;
using Microsoft.VisualBasic;

/// <summary>
/// ToHankakuFilterStream
/// （半角化フィルタ ストリーム）
/// </summary>
/// <remarks>
/// 次のWebページを参考にして作成
/// 
/// HttpResponse.Filter プロパティ
/// http://msdn.microsoft.com/ja-jp/library/system.web.httpresponse.filter.aspx
/// </remarks>
public class ToHankakuFilterStream : Stream
{
    /// <summary>修飾対象オブジェクト</summary>
    private Stream TransferStream;

    /// <summary>エンコーディング</summary>
    private Encoding MyEncoding;

    /// <summary>コンストラクタ</summary>
    public ToHankakuFilterStream(Stream transferStream, Encoding myEncoding)
    {
        this.TransferStream = transferStream;
        this.MyEncoding = myEncoding;
    }

    #region abstract（must override）

    /// <summary>現在のストリームが読み取りをサポートするかどうかを示す</summary>
    public override bool CanRead
    {
        // 「Read」を実装しているので「true」
        get { return true; }
    }

    /// <summary>現在のストリームがシークをサポートするかどうかを示す</summary>
    public override bool CanSeek
    {
        // 「Seek」を実装しているので「true」
        get { return true; }
    }

    /// <summary>現在のストリームが書き込みをサポートするかどうかを示す</summary>
    public override bool CanWrite
    {
        // 「Write」を実装しているので「true」
        get { return true; }
    }

    /// <summary>ストリームの長さをバイト単位で取得</summary>
    public override long Length
    {
        get { return this.TransferStream.Length; }
    }

    /// <summary>現在のストリーム内の位置を取得または設定</summary>
    public override long Position
    {
        get { return this.TransferStream.Position; }
        set { this.TransferStream.Position = value; }
    }

    /// <summary>現在のストリーム内の位置を設定</summary>
    /// <param name="offset">originからのバイト オフセット。</param>
    /// <param name="origin">参照ポイント指定</param>
    /// <returns>現在のストリーム内の新しい位置</returns>
    public override long Seek(long offset, System.IO.SeekOrigin origin)
    {
        return this.TransferStream.Seek(offset, origin);
    }

    /// <summary>現在のストリームの長さを設定</summary>
    /// <param name="length">ストリームの長さ</param>
    public override void SetLength(long length)
    {
        this.TransferStream.SetLength(length);
    }

    /// <summary>
    /// 現在のストリームを閉じ、現在のストリームに関連付けられている
    /// すべてのリソース (ソケット、ファイル ハンドルなど) を解放
    /// </summary>
    public override void Close()
    {
        this.TransferStream.Close();
    }

    /// <summary>
    /// ストリームに対応するすべてのバッファをクリアし、
    /// バッファ内のデータを基になるデバイスに書き込む。
    /// </summary>
    public override void Flush()
    {
        this.TransferStream.Flush();
    }

    /// <summary>
    /// 現在のストリームからバイト シーケンスを読み取り、
    /// 読み取ったバイト数の分だけストリームの位置を進める。 
    /// </summary>
    /// <param name="buffer">バイト配列</param>
    /// <param name="offset">データ読み取り開始位置</param>
    /// <param name="count">現在のストリームから読み取る最大バイト数。 </param>
    /// <returns></returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
        return this.TransferStream.Read(buffer, offset, count);
    }

    #endregion

    /// <summary>
    /// フィルタ処理を実装したWriteメソッド
    /// </summary>
    /// <param name="buffer">バイト配列</param>
    /// <param name="offset">バイト オフセット</param>
    /// <param name="count">現在のストリームに書き込むバイト数。 </param>
    public override void Write(byte[] buffer, int offset, int count)
    {
        // 文字列化→半角化→バイト配列化
        byte[] data = MyEncoding.GetBytes(this.ToHankaku(MyEncoding.GetString(buffer)));

        // 書き込み
        this.TransferStream.Write(data, 0, data.Length);
    }

    /// <summary>文字列半角化</summary>
    /// <param name="input">入力文字列</param>
    /// <returns>出力文字列</returns>
    public string ToHankaku(string input)
    {
        // VB関数を使用する。
        return Strings.StrConv(input, VbStrConv.Narrow, 0);
    }
}
