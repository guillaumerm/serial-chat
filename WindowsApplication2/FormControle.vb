REM                                                 Historique de la classe
REM
REM No Modification         Date                           Description                                                      Auteur
REM        1              09/15/2015                  Création de la classe FormControle                        Guillaume R. - Mathieu + Terry Turcotte
REM        2              09/15/2015                  Implémentation de l'algorithmie pour l'initialisation     Guillaume R.-Mathieu + Terry Turcotte
REM                                                   de la list de utilisateur distant.
REM        3              09/15/2015                  Implémentation de l'algorithmie pour le retrait des       Guillaume R.-Mathieu + Terry Turcotte
REM                                                   utilisateurs lors de la fermeture du controleur           
REM        4              09/28/2015                  Ajout de la gestion des événement du formulaire           Guillaume R.-Mathieu
REM                                                   utilisateur.
REM        5              09/28/2015                  Changer les fonctions qui n'étaient pas "threadsafe"      Guillaume R.-Mathieu
REM                                                   en autre mots j'ai ajouter l'utilisation d'un delegate
REM                                                   pour faire les changements au niveau de la liste des 
REM                                                   destinataire.
REM

Imports System.IO.Ports

''Modification 1
Public Class FormControle
    Private Const NOM_PORT As String = "COM4"
    Private formsUtilisateur As List(Of FormUtilisateur)
    Private listUtilisateurDistant As List(Of String)
    Private listUtilisateurLocale As List(Of String)
    Private WithEvents serialPort As PortSerie

    ''Modification 5
    Delegate Sub SetListDestinataireCallback(ByVal [list] As List(Of String), ByVal [form] As FormUtilisateur)

    Public Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        serialPort = New PortSerie()


        listUtilisateurLocale = New List(Of String)
        listUtilisateurDistant = New List(Of String)
        formsUtilisateur = New List(Of FormUtilisateur)

        ouvrirPortSerie()

        initializeUtilisateur()
    End Sub

    Protected Overrides Sub Finalize()
        RemoveTousUtilisateurs()
        serialPort.Close()
        serialPort = Nothing
    End Sub

    Private Sub initializeUtilisateur()
        SendDataFrame("BROADCAST", "", Trame.TypeEnum.OBTAIN, "utilisateurs")
    End Sub

    Private Sub ouvrirPortSerie()
        If Not serialPort.IsOpen Then
            serialPort.PortName = NOM_PORT
            serialPort.RtsEnable = True
            serialPort.DtrEnable = True
            serialPort.Open()
        End If
    End Sub

    Private Sub RafraichirListDestinataires()
        For Each form As FormUtilisateur In OwnedForms.OfType(Of FormUtilisateur)
            Dim listDestinataireTemp As List(Of String) = New List(Of String)
            listDestinataireTemp.AddRange(listUtilisateurLocale)
            listDestinataireTemp.AddRange(listUtilisateurDistant)
            listDestinataireTemp.Remove(form.Utilisateur)

            If (listDestinataireTemp.Count > 0) Then
                SetListDestinataire(listDestinataireTemp, form)
            Else
                SetListDestinataire(listDestinataireTemp, form)
            End If

        Next
    End Sub

    ''Modification 5
    Private Sub SetListDestinataire(ByVal [list] As List(Of String), ByVal [form] As FormUtilisateur)
        If [form].listDestinataire.InvokeRequired Then
            Dim d As New SetListDestinataireCallback(AddressOf SetListDestinataire)
            [form].Invoke(d, New Object() {[list], [form]})
        Else
            [form].listDestinataire.Items.Clear()
            [form].listDestinataire.Items.AddRange([list].ToArray)
        End If
    End Sub

    ''Modification 2
    Private Sub AjoutUtilisateurLocale(ByVal nomUtilisateur As String)
        If (Not listUtilisateurLocale.Contains(nomUtilisateur) And Not listUtilisateurDistant.Contains(nomUtilisateur)) Then
            listUtilisateurLocale.Add(nomUtilisateur)

            Dim formTemp As FormUtilisateur = New FormUtilisateur(nomUtilisateur)

            formTemp.Owner = Me

            AddHandler formTemp.envoyerMessage, AddressOf OnEnvoyerMessage
            AddHandler formTemp.enleveUtilisateur, AddressOf OnEnleveUtilisateur
            AddHandler formTemp.FormClosing, AddressOf OnFormUtilisateur_FormClosing

            formsUtilisateur.Add(formTemp)
            formTemp.Show()
            listChatteurs.Items.Add(nomUtilisateur)

            SendDataFrame("BROADCAST", "", Trame.TypeEnum.ADD, nomUtilisateur)

            RafraichirListDestinataires()
        Else
            MessageBox.Show("Le nom d'utilisateur sélectionner est déjà utilisé.", "Avertissement", MessageBoxButtons.OK)
        End If
    End Sub

    ''Modification 2
    Private Sub AjoutUtilisateurDistant(ByVal nomUtilisateur As String)
        If (Not listUtilisateurDistant.Contains(nomUtilisateur)) Then
            Dim utilisateurs As String() = nomUtilisateur.Split(New Char() {"%"})

            For Each utilisateurTemp As String In utilisateurs
                If (utilisateurTemp IsNot "") Then
                    listUtilisateurDistant.Add(utilisateurTemp)
                End If
            Next
            RafraichirListDestinataires()
        End If
    End Sub

    ''Modification 3
    Public Sub RemoveUtilisateur(ByVal utilisateur As String)
        If (listUtilisateurDistant.Contains(utilisateur)) Then
            listUtilisateurDistant.Remove(utilisateur)
        ElseIf (listUtilisateurLocale.Contains(utilisateur)) Then
            listUtilisateurLocale.Remove(utilisateur)
            listChatteurs.Items.Remove(utilisateur)

            For Each form As FormUtilisateur In OwnedForms
                If (form.Utilisateur = utilisateur) Then
                    form.Close()
                End If
            Next

            SendDataFrame("BROADCAST", "", Trame.TypeEnum.REMOVE, utilisateur)
        End If

        RafraichirListDestinataires()
    End Sub

    Private Sub RemoveTousUtilisateurs()
        If (listUtilisateurLocale.Count > 0) Then
            Dim utilisateurs As String = ""

            For Each utilisateur As String In listUtilisateurLocale
                utilisateurs += "%" + utilisateur
            Next

            SendDataFrame("BROADCAST", "", Trame.TypeEnum.REMOVE, utilisateurs)
        End If
    End Sub

    Private Sub EnvoyerTousUtilisateurs()
        Dim stringUtilisateur As String = ""

        If (listUtilisateurLocale.Count > 0) Then
            For i As Integer = 0 To listUtilisateurLocale.Count - 1
                stringUtilisateur += "%" + listUtilisateurLocale(i)
            Next
        End If

        SendDataFrame("BROADCAST", "", Trame.TypeEnum.ADD, stringUtilisateur)
    End Sub

    Private Sub envoyerToClient(ByVal destination As String, ByVal source As String, ByVal data As String)
        If (listUtilisateurLocale.Contains(destination)) Then
            For Each form As FormUtilisateur In OwnedForms
                If (form.Utilisateur = destination) Then

                    form.afficherMessageRecu(source, data)
                End If
            Next
        Else
            SendDataFrame(destination, source, Trame.TypeEnum.DATA, data)
        End If

    End Sub

    Private Sub gereTrameRecu(ByRef trame As Trame)
        If (listUtilisateurLocale.Contains(trame.Destination) Or trame.Destination = "BROADCAST") Then
            Select Case trame.Type
                Case Trame.TypeEnum.ADD
                    AjoutUtilisateurDistant(trame.Donnees)
                Case Trame.TypeEnum.REMOVE
                    RemoveUtilisateur(trame.Donnees)
                Case Trame.TypeEnum.DATA
                    envoyerToClient(trame.Destination, trame.Source, trame.Donnees)
                Case Trame.TypeEnum.OBTAIN
                    If (trame.Donnees = "utilisateurs") Then
                        EnvoyerTousUtilisateurs()
                    End If
                Case Else
                    'exception type erronné
            End Select
        End If
    End Sub

    Protected Overridable Sub OnDataReveived() Handles serialPort.DataReceived
        Dim stringRecu As String = ""
        Dim trame As Trame = New Trame()

        While (Not stringRecu.Contains(Trame.END_FRAME))
            stringRecu += serialPort.ReadExisting()
        End While

        trame.ConvertStringToTrame(stringRecu)

        gereTrameRecu(trame)
    End Sub

    Private Sub SendDataFrame(ByVal destination As String, ByVal source As String, ByVal type As Trame.TypeEnum, ByVal data As String)
        Dim trame As Trame = New Trame(destination, source, type, data)
        serialPort.Write(trame.ToString)
    End Sub

    ''Modification 4
    Private Sub OnEnvoyerMessage(destination As String, source As String, donnees As String)
        envoyerToClient(destination, source, donnees)
    End Sub

    ''Modification 4
    Private Sub OnEnleveUtilisateur(nomUtilisateur As String)
        RemoveUtilisateur(nomUtilisateur)
    End Sub

    Private Sub OnFormUtilisateur_FormClosing(sender As Object, e As FormClosingEventArgs)
        Dim formTemp As FormUtilisateur = CType(sender, FormUtilisateur)
        RemoveUtilisateur(formTemp.Utilisateur)
    End Sub

    Private Sub boutonRetirer_Click(sender As Object, e As EventArgs) Handles boutonRetirer.Click
        Dim utilisateur As String = listChatteurs.Text
        RemoveUtilisateur(utilisateur)
    End Sub

    Private Sub boutonAjouter_Click(sender As Object, e As EventArgs) Handles boutonAjouter.Click
        Dim utilistateur As String = listChatteurs.Text
        listChatteurs.Text = ""
        If (utilistateur <> "") Then
            AjoutUtilisateurLocale(utilistateur)
        Else
            MessageBox.Show("Le nom d'utilisateur est vide.", "Avertissement", MessageBoxButtons.OK)
        End If

    End Sub
End Class
