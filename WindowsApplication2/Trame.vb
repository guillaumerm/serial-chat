REM                                                 Historique de la classe
REM
REM     Date                           Description                                     Auteur
REM 09/15/2015      Création de la classe trame                         Guillaume R. - Mathieu + Terry Turcotte
REM 09/22/2015      Amélioration de la classe de la trame               Guillaume R.-Mathieu + Terry Turcotte
REM 09/23/2015      Changement de la fonction ConvertStringToTrame      Guillaume R.-Mathieu + Terry Turcotte
REM                 pour prendre charge du delimiteur de trame é-
REM                 tablie le 22 septembre.
REM
REM 09/23/2015      Ajout d'un type de trame "OBTAIN" ce type est       Guillaume R.-Mathieu + Terry Turcotte
REM                 utilisé par l'application lors de la saisie des
REM                 utilisateurs présent sur le réseau
REM

Public Class Trame
    Private sourceProperty As String
    Private destinationProperty As String
    Private donneesProperty As String
    Private typeProperty As String

    Public Shared ReadOnly END_FRAME As Char = System.Convert.ToChar(23)
    Public Shared ReadOnly DLE As Char = System.Convert.ToChar(16)
    Public Shared ReadOnly SEPARATOR As Char = System.Convert.ToChar(29)

    Enum TypeEnum
        REMOVE
        OBTAIN
        ADD
        DATA
    End Enum

    Public Sub New()
        sourceProperty = ""
        destinationProperty = ""
        donneesProperty = ""
        typeProperty = ""
    End Sub

    Public Sub New(ByVal destination As String, ByVal source As String, ByVal type As TypeEnum, ByVal donnees As String)
        destinationProperty = destination
        sourceProperty = source
        donneesProperty = transperence(donnees)
        typeProperty = type
    End Sub

    Public Sub New(ByVal pTrameString As String)
        ConvertStringToTrame(pTrameString)
    End Sub

    Public Sub ConvertStringToTrame(ByVal pTrameString As String)
        Dim trameStringTemp As List(Of Char) = pTrameString.ToList()

        trameStringTemp.RemoveAt(trameStringTemp.Count - 1)

        Dim trameString As String() = New String(trameStringTemp.ToArray).Split(SEPARATOR)

        destinationProperty = trameString(0)
        sourceProperty = trameString(1)
        typeProperty = trameString(2)
        donneesProperty = detransparence(trameString(3))
    End Sub

    Public Property Source As String
        Get
            Return sourceProperty
        End Get
        Set(value As String)
            sourceProperty = value
        End Set
    End Property

    Public Property Destination As String
        Get
            Return destinationProperty
        End Get
        Set(value As String)
            destinationProperty = value
        End Set
    End Property

    Public Property Donnees As String
        Get
            Return donneesProperty
        End Get
        Set(value As String)
            donneesProperty = value
        End Set
    End Property

    Public Property Type As TypeEnum
        Get
            Return typeProperty
        End Get
        Set(value As TypeEnum)
            typeProperty = value
        End Set
    End Property

    Private Function transperence(ByVal data As String)
        Dim dataTemp As List(Of Char) = data.ToList()
        For i As Integer = 0 To dataTemp.Count - 1
            If (dataTemp(i) = END_FRAME Or dataTemp(i) = SEPARATOR) Then
                If (i = 0) Then
                    dataTemp.Insert(0, DLE)
                Else
                    dataTemp.Insert(i - 1, DLE)
                End If
            End If
        Next

        Return New String(dataTemp.ToArray)
    End Function

    Private Function detransparence(ByVal data As String)
        Dim dataTemp As List(Of Char) = data.ToList()
        If (dataTemp.Count > 0) Then
            For i As Integer = 0 To dataTemp.Count - 1
                If (dataTemp(i) = END_FRAME Or dataTemp(i) = SEPARATOR) Then
                    dataTemp.RemoveAt(i - 1)
                End If
            Next
        End If

        Return New String(dataTemp.ToArray)
    End Function

    Public Overrides Function ToString() As String
        Dim returnString As String = destinationProperty + SEPARATOR + sourceProperty + SEPARATOR + typeProperty + SEPARATOR + transperence(donneesProperty) + END_FRAME
        Return returnString

    End Function
End Class
