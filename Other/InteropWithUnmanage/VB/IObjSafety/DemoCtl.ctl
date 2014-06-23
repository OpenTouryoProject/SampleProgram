VERSION 5.00
Begin VB.UserControl DemoCtl 
   ClientHeight    =   3600
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   4800
   ScaleHeight     =   3600
   ScaleWidth      =   4800
   Begin VB.CommandButton cmdTest 
      Caption         =   "Command1"
      Height          =   1215
      Left            =   360
      TabIndex        =   0
      Top             =   720
      Width           =   4095
   End
End
Attribute VB_Name = "DemoCtl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Implements IObjectSafety

Private Sub IObjectSafety_GetInterfaceSafetyOptions(ByVal riid As _
    Long, pdwSupportedOptions As Long, pdwEnabledOptions As Long)

    Dim Rc      As Long
    Dim rClsId  As udtGUID
    Dim IID     As String
    Dim bIID()  As Byte

    pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER Or _
                          INTERFACESAFE_FOR_UNTRUSTED_DATA

    If (riid <> 0) Then
        CopyMemory rClsId, ByVal riid, Len(rClsId)

        bIID = String$(MAX_GUIDLEN, 0)
        Rc = StringFromGUID2(rClsId, VarPtr(bIID(0)), MAX_GUIDLEN)
        Rc = InStr(1, bIID, vbNullChar) - 1
        IID = Left$(UCase(bIID), Rc)

        Select Case IID
            Case IID_IDispatch
                pdwEnabledOptions = IIf(m_fSafeForScripting, _
                INTERFACESAFE_FOR_UNTRUSTED_CALLER, 0)
                Exit Sub
            Case IID_IPersistStorage, IID_IPersistStream, _
                IID_IPersistPropertyBag
                pdwEnabledOptions = IIf(m_fSafeForInitializing, _
                    INTERFACESAFE_FOR_UNTRUSTED_DATA, 0)
                Exit Sub
            Case Else
                Err.Raise E_NOINTERFACE
                Exit Sub
        End Select
    End If
End Sub

Private Sub IObjectSafety_SetInterfaceSafetyOptions(ByVal riid As _
    Long, ByVal dwOptionsSetMask As Long, ByVal dwEnabledOptions As Long)
        
    Dim Rc          As Long
    Dim rClsId      As udtGUID
    Dim IID         As String
    Dim bIID()      As Byte

    If (riid <> 0) Then
            
        CopyMemory rClsId, ByVal riid, Len(rClsId)

        bIID = String$(MAX_GUIDLEN, 0)
        Rc = StringFromGUID2(rClsId, VarPtr(bIID(0)), MAX_GUIDLEN)
        Rc = InStr(1, bIID, vbNullChar) - 1
        IID = Left$(UCase(bIID), Rc)

        Select Case IID
            Case IID_IDispatch
                If ((dwEnabledOptions And dwOptionsSetMask) <> _
                    INTERFACESAFE_FOR_UNTRUSTED_CALLER) Then
                    Err.Raise E_FAIL
                    Exit Sub
                Else
                    If Not m_fSafeForScripting Then
                        Err.Raise E_FAIL
                    End If
                    Exit Sub
                End If

            Case IID_IPersistStorage, IID_IPersistStream, _
                IID_IPersistPropertyBag
                If ((dwEnabledOptions And dwOptionsSetMask) <> _
                    INTERFACESAFE_FOR_UNTRUSTED_DATA) Then
                    Err.Raise E_FAIL
                    Exit Sub
                Else
                    If Not m_fSafeForInitializing Then
                        Err.Raise E_FAIL
                    End If
                Exit Sub
                End If

            Case Else
                Err.Raise E_NOINTERFACE
                Exit Sub
        End Select
    End If
End Sub
      
Private Sub cmdTest_Click()
   
   Call MsgBox("テスト", vbOKOnly, "テスト")
    
End Sub

