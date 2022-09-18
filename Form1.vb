Public Class Form1
    Private Server As TcpController

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Server = New TcpController
        txtChat.Text = ":: SERVIDOR INICIADO ::" & vbCrLf

        AddHandler Server.MessageReceived, AddressOf OnlineReceived
    End Sub

    Private Sub OnlineReceived(sender As TcpController, Data As String)
        UpdateText(txtChat, Data)
    End Sub

    Private Delegate Sub UpdateTextDelegate(TB As TextBox, txt As String)

    Private Sub UpdateText(TB As TextBox, txt As String)
        If TB.InvokeRequired Then
            TB.Invoke(New UpdateTextDelegate(AddressOf UpdateText), New Object() {TB, txt})
        Else
            If txt IsNot Nothing Then
                TB.AppendText(txt & vbCrLf)
            End If
        End If
    End Sub
End Class
