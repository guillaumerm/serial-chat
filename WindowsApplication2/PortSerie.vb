REM                                                 Historique de la classe
REM
REM  No Modification        Date                           Description                                     Auteur
REM        1            09/01/2015      Création de la classe wrappeur du port série           Guillaume R. - Mathieu + Terry Turcotte
REM        2            09/07/2015      Ajout des fonctions d'envoie et de réception           Guillaume R. - Mathieu + Terry Turcotte
REM

Imports System.IO.Ports

''Modification 1
Public Class PortSerie
    Private WithEvents serialPort As SerialPort
    Public Event PinChanged As SerialPinChangedEventHandler
    Public Event DataReceived As SerialDataReceivedEventHandler

    Public Sub New()
        serialPort = New SerialPort()
    End Sub

    Public Sub New(pNomPort As String)
        serialPort = New SerialPort(pNomPort)
    End Sub

    Public ReadOnly Property DsrHolding As Boolean
        Get
            Return serialPort.DsrHolding
        End Get
    End Property

    Public ReadOnly Property CDHolding As Boolean
        Get
            Return serialPort.CDHolding
        End Get
    End Property

    Public ReadOnly Property CtsHolding As Boolean
        Get
            Return serialPort.CtsHolding
        End Get
    End Property

    Public Property DtrEnable As Boolean
        Get
            Return serialPort.DtrEnable
        End Get
        Set(value As Boolean)
            serialPort.DtrEnable() = value
        End Set
    End Property

    Public Property RtsEnable As Boolean
        Get
            Return serialPort.RtsEnable()
        End Get
        Set(value As Boolean)
            serialPort.RtsEnable() = value
        End Set
    End Property

    Public Property PortName As String
        Get
            Return serialPort.PortName
        End Get
        Set(value As String)
            serialPort.PortName = value
        End Set
    End Property

    Public Property BaudRate As Integer

        Get
            Return serialPort.BaudRate
        End Get
        Set(value As Integer)
            serialPort.BaudRate = value
        End Set
    End Property

    Public Property Parity As Parity

        Get
            Return serialPort.Parity
        End Get
        Set(value As Parity)
            serialPort.Parity = value
        End Set
    End Property

    Public Property StopBits As StopBits

        Get
            Return serialPort.StopBits
        End Get
        Set(value As StopBits)
            serialPort.StopBits = value
        End Set
    End Property

    Public Property DataBits As Integer
        Get
            Return serialPort.DataBits
        End Get
        Set(value As Integer)
            serialPort.DataBits = value
        End Set
    End Property

    Public Function IsOpen() As Boolean
        Return serialPort.IsOpen()
    End Function

    Public Sub Open()
        serialPort.Open()
    End Sub

    Public Sub Close()
        If serialPort IsNot Nothing And serialPort.IsOpen() Then
            serialPort.Close()
        End If
    End Sub

    ''Modification 2
    Public Function ReadExisting() As String
        Return serialPort.ReadExisting
    End Function

    ''Modification 2
    Public Sub Write(ByVal chaine As String)
        serialPort.Write(chaine)
    End Sub

    Shared Function GetPortNames() As Array
        Return SerialPort.GetPortNames
    End Function

    Protected Overridable Sub OnPinChanged(o As Object, e As SerialPinChangedEventArgs) Handles serialPort.PinChanged
        RaiseEvent PinChanged(serialPort, e)
    End Sub
    ''Modification 2
    Protected Overridable Sub OnDataReceived(o As Object, e As SerialDataReceivedEventArgs) Handles serialPort.DataReceived
        RaiseEvent DataReceived(Me, e)
    End Sub
End Class