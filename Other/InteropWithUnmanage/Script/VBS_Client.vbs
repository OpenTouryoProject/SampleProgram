Option Explicit 

Dim ret
Dim str1
Dim i1
Dim str2
Dim i2
        
str1 = "test1,"
i1 = 1
str2 = "test2,"
i2 = 2

'----------------------------------------------------------

' <VB_COM.ClassTest>

Dim obj
Set obj = CreateObject("VB_COM.ClassTest")

'正常終了
ret = obj.MethodTest2(str1, i1, str2, i2)
'MethodTest2は、引数・戻り値の型を全てVariant型に変更した。

'MsgBox
Call MsgBox( _
  "ret:" & ret & vbCrLf & _
  "str1:" & str1 & vbCrLf & "i1:" & CStr(i1) & vbCrLf & _
  "str2:" & str2 & vbCrLf & "i2:" & CStr(i2), vbOKOnly)

'異常終了（エラーハンドラへ）
'ret = obj.MethodTest2("", i1, str2, i2)

'----------------------------------------------------------

' <VC_COM.ClassTest>

Set obj = CreateObject("VC_COM.ClassTest")

'正常終了
Call MsgBox(obj.MethodTest("だいすけ", "にしの"))
'引数・戻り値の型がすべてBSTRであれば呼べる模様。