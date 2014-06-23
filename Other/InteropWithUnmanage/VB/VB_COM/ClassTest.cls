VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "ClassTest"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'********************************************************************
'* Projectのプロパティ
'* ・全般：ActiveX DLL、対話型IFの抑止、メモリに保持
'* ・コンポーネント：バイナリ互換
'*
'* Classプロパティ
'* ・Instancing = Multiuse
'*
'* IDL定義と、ByVal, ByRefをテストする。
'*
'* VBCOMではHRESULTはIErrorInfoを通じて、Err.Numberで返る仕様。
'* 利用可能なユーザ定義のHRESULTは[vbObjectError + n]とする。
'*
'* 各HRESULTに対応する.NET Frameworkの例外クラスの対応付け表
'* http://msdn.microsoft.com/ja-jp/library/aa720134(VS.71).aspx
'********************************************************************

'* <summary>COM化テスト</summary>
'* <param name="str1">文字列（in）</param>
'* <param name="i1">数値（in）</param>
'* <param name="str2">文字列（in、out）</param>
'* <param name="i2">数値（in、out）</param>
'* <returns>数値（retval）</returns>
'* <see cref="http://msdn.microsoft.com/ja-jp/library/8tesw2eh.aspx"/>
Public Function MethodTest( _
    ByVal str1 As String, ByVal i1 As Integer, _
    ByRef str2 As String, ByRef i2 As Integer) As Integer
    
    ' ByVal str1
    If str1 = "" Then
        Call Err.Raise(vbObjectError + 1, "ClassTest.MethodTest", "str1が空です")
    Else
        str1 = str1 & str1 ' ByValなので変更は返らない
    End If
    
    ' ByVal i1
    If i1 = 0 Then
        Call Err.Raise(vbObjectError + 2, "ClassTest.MethodTest", "i1が0です")
    Else
        i1 = i1 + i1 ' ByValなので変更は返らない
    End If
    
    ' ByRef str2
    If str2 = "" Then
        Call Err.Raise(vbObjectError + 3, "ClassTest.MethodTest", "str2が空です")
    Else
        str2 = str2 & str2 ' ByRefなので変更は返る
    End If
    
    ' ByRef i2
    If i2 = 0 Then
        Call Err.Raise(vbObjectError + 4, "ClassTest.MethodTest", "i2が0です")
    Else
        i2 = i2 + i2 ' ByRefなので変更は返る
    End If
    
    ' 0 以上 100 未満の乱数を取得し戻す。
    MethodTest = CInt(Math.Rnd() * 100)
    
End Function

' 上記をVBSから利用できるように全ての型をVariantに変更したメソッド

Public Function MethodTest2( _
    ByVal str1 As Variant, ByVal i1 As Variant, _
    ByRef str2 As Variant, ByRef i2 As Variant) As Variant
    
    ' ByVal str1
    If str1 = "" Then
        Call Err.Raise(vbObjectError + 1, "ClassTest.MethodTest", "str1が空です")
    Else
        str1 = str1 & str1 ' ByValなので変更は返らない
    End If
    
    ' ByVal i1
    If i1 = 0 Then
        Call Err.Raise(vbObjectError + 2, "ClassTest.MethodTest", "i1が0です")
    Else
        i1 = i1 + i1 ' ByValなので変更は返らない
    End If
    
    ' ByRef str2
    If str2 = "" Then
        Call Err.Raise(vbObjectError + 3, "ClassTest.MethodTest", "str2が空です")
    Else
        str2 = str2 & str2 ' ByRefなので変更は返る
    End If
    
    ' ByRef i2
    If i2 = 0 Then
        Call Err.Raise(vbObjectError + 4, "ClassTest.MethodTest", "i2が0です")
    Else
        i2 = i2 + i2 ' ByRefなので変更は返る
    End If
    
    ' 0 以上 100 未満の乱数を取得し戻す。
    MethodTest2 = CVar(Math.Rnd() * 100)
    
End Function


