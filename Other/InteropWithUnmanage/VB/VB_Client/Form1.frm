VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   2550
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3855
   LinkTopic       =   "Form1"
   ScaleHeight     =   2550
   ScaleWidth      =   3855
   StartUpPosition =   3  'Windows の既定値
   Begin VB.CommandButton Command3 
      Caption         =   "→　VC_DLL"
      Height          =   495
      Left            =   120
      TabIndex        =   3
      Top             =   1920
      Width           =   3615
   End
   Begin VB.CommandButton Command0 
      Caption         =   "→　VC_COM"
      Height          =   495
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   3615
   End
   Begin VB.CommandButton Command2 
      Caption         =   "→　DNET_COM"
      Height          =   495
      Left            =   120
      TabIndex        =   1
      Top             =   1320
      Width           =   3615
   End
   Begin VB.CommandButton Command1 
      Caption         =   "→　VB_COM"
      Height          =   495
      Left            =   120
      TabIndex        =   0
      Top             =   720
      Width           =   3615
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Type MyType
    m1 As Integer
    m2_in As Long
    m3_out As Long
End Type

'VC_DLL.Test_MYLIBAPI
Private Declare Sub Test_MYLIBAPI Lib "VC_DLL.dll" (ByVal text As Long, ByVal caption As Long)
        
' VB6→VC_COM
Private Sub Command0_Click()

On Error GoTo ErrorHandler ' エラー処理を行います。
    
    'Dim o
    'Set o = CreateObject("VC_COM.ClassTest") ' レイトバインド
    
    Dim o As New VC_COMLib.ClassTest ' アーリーバインドするにはtlbが必要
    
    '正常終了
    Call MsgBox(o.MethodTest("だいすけ", "にしの"), vbOKOnly, "MsgBox")
            
    '異常終了（エラーハンドラへ）
    Call o.MethodTest("だいすけ", "")
        
    Exit Sub
    
ErrorHandler:

    'MsgBox
    Call MsgBox( _
        "Err.Number:" & Err.Number & vbCrLf & _
        "Err.Description:" & Err.Description, vbOKOnly)
        
End Sub

' VB6→VB_COM
Private Sub Command1_Click()

On Error GoTo ErrorHandler ' エラー処理を行います。

    Dim ret As Integer
    
    Dim str1 As String
    Dim i1 As Integer
    Dim str2 As String
    Dim i2 As Integer
        
    str1 = "test1,"
    i1 = 1
    str2 = "test2,"
    i2 = 2
        
    'Dim o
    'Set o = CreateObject("VB_COM.ClassTest") ' レイトバインド
    
    Dim o As New VB_COM.ClassTest ' アーリーバインドするにはtlbが必要
    
    '正常終了
    ret = o.MethodTest(str1, i1, str2, i2)
        
    'MsgBox
    Call MsgBox( _
        "ret:" & ret & vbCrLf & _
        "str1:" & str1 & vbCrLf & "i1:" & CStr(i1) & vbCrLf & _
        "str2:" & str2 & vbCrLf & "i2:" & CStr(i2), vbOKOnly)
            
    '異常終了（エラーハンドラへ）
    ret = o.MethodTest("", i1, str2, i2)
        
    'MsgBox
    Call MsgBox( _
        "ret:" & ret & vbCrLf & _
        "str1:" & str1 & vbCrLf & "i1:" & CStr(i1) & vbCrLf & _
        "str2:" & str2 & vbCrLf & "i2:" & CStr(i2), vbOKOnly)
    
    Exit Sub
    
ErrorHandler:

    'MsgBox
    Call MsgBox( _
        "Err.Number:" & Err.Number & vbCrLf & _
        "Err.Description:" & Err.Description, vbOKOnly)

End Sub

' VB6→DNET_COM
Private Sub Command2_Click()

On Error GoTo ErrorHandler ' エラー処理を行います。

    Dim ret As Integer
    
    Dim str1 As String
    Dim i1 As Integer
    Dim str2 As String
    Dim i2 As Integer
        
    str1 = "test1,"
    i1 = 1
    str2 = "test2,"
    i2 = 2
        
    'Dim o
    'Set o = CreateObject("DNET_COM.ClassTest") ' レイトバインド
    
    Dim o As New DNET_COM.ClassTest ' アーリーバインドするにはtlbが必要
        
    '正常終了
    ret = o.MethodTest(str1, i1, str2, i2)
        
    'MsgBox
    Call MsgBox( _
        "ret:" & ret & vbCrLf & _
        "str1:" & str1 & vbCrLf & "i1:" & CStr(i1) & vbCrLf & _
        "str2:" & str2 & vbCrLf & "i2:" & CStr(i2), vbOKOnly)
            
    '異常終了（エラーハンドラへ）
    ret = o.MethodTest("", i1, str2, i2)
        
    'MsgBox
    Call MsgBox( _
        "ret:" & ret & vbCrLf & _
        "str1:" & str1 & vbCrLf & "i1:" & CStr(i1) & vbCrLf & _
        "str2:" & str2 & vbCrLf & "i2:" & CStr(i2), vbOKOnly)
    
    Exit Sub
    
ErrorHandler:

    'MsgBox
    Call MsgBox( _
        "Err.Number:" & Err.Number & vbCrLf & _
        "Err.Description:" & Err.Description, vbOKOnly) ' HResultは-2146233088（80131500）

End Sub

Private Sub Command3_Click()

On Error GoTo ErrorHandler ' エラー処理を行います。

    Call Test_MYLIBAPI(StrPtr("だいすけ"), StrPtr("にしの"))
    
    Exit Sub
    
ErrorHandler:

    'MsgBox
    Call MsgBox( _
        "Err.Number:" & Err.Number & vbCrLf & _
        "Err.Description:" & Err.Description, vbOKOnly)
        
End Sub
