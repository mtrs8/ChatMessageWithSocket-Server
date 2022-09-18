Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Public Class TcpController
    Public Event MessageReceived(sender As TcpController, Data As String)

    'Configuração do Server
    Public ServerIP As IPAddress = IPAddress.Parse("10.0.0.104")
    Public ServerPort As Integer = 8888
    Public Server As TcpListener

    Private TConnection As Thread
    Public IsListening As Boolean

    'Configuração do Client
    Private Client As TcpClient
    Private ClientData As StreamReader

    'Percorre até o servidor estar ouvindo
    Private Sub Listenning()
        Do Until IsListening = False
            'Aceita conexão do Cliente
            If Server.Pending = True Then
                Client = Server.AcceptTcpClient
                ClientData = New StreamReader(Client.GetStream)
                'Recebendo Mensagens
                Try
                    RaiseEvent MessageReceived(Me, ClientData.ReadLine)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
            Thread.Sleep(100)
        Loop
    End Sub

    Public Sub New()
        Me.Server = New TcpListener(ServerIP, ServerPort)
        Server.Start()
        Me.TConnection = New Thread(New ThreadStart(AddressOf Listenning))
    End Sub
End Class
