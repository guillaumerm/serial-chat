REM                                                 Historique de la classe
REM
REM No Modification         Date                           Description                                                      Auteur
REM        1              09/15/2015                  Création de la classe FormUtilisateur                     Guillaume R. - Mathieu + Terry Turcotte
REM        2              09/24/2015                  Adaption de la procédure pour afficher les message à      Guillaume R.-Mathieu
REM                                                   l'écran. Nous avons rajouté un champ qui indique l'
REM                                                   utilisateur qui nous a contacté.
REM        3              09/28/2015                  Implémentation d'événement pour rendre cette classe plus       Guillaume R.-Mathieu
REM                                                   MVC. Donc cette classe avertis le controleur lorsque un 
REM                                                   message nécissite sont envoie ou lors de la fermeture du
REM                                                   formulaire l'utilisateur est enlever de la liste ainsi que
REM                                                   sont instance est détruite.
REM        4              09/28/2015                  Ajout de delgate pour la modification de la rich text box
REM                                                   ou que nous ajoutons les messages que nous recevons des
REM                                                   divers client du réseau
REM

Public Class FormUtilisateur
    Private nomUtilisateur As String

    ''Modification 2
    Public Event envoyerMessage(ByVal destination As String, ByVal source As String, ByVal donnees As String)
    Public Event enleveUtilisateur(ByVal utilisateur As String)

    Delegate Sub AppendTextBoxCallback(ByVal [text] As String)

    ''Modification 1
    Public ReadOnly Property Utilisateur
        Get
            Return nomUtilisateur
        End Get
    End Property
    Public Sub New(ByVal pNomUtilisateur As String)

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        nomUtilisateur = pNomUtilisateur
        Me.Text = "Chatteur de " + nomUtilisateur
    End Sub

    ''Modification 2
    Public Sub afficherMessageRecu(ByVal source As String, ByVal msg As String)
        AppendTextBoxRecu(Now.ToLongTimeString + " - " + source.ToUpper() + " - " + msg)
    End Sub

    ''Modification 4
    Public Sub AppendTextBoxRecu(ByVal [text] As String)
        If textboxRecu.InvokeRequired Then
            Dim d As New AppendTextBoxCallback(AddressOf AppendTextBoxRecu)
            textboxRecu.Invoke(d, New Object() {[text]})
        Else
            Dim listTemp As List(Of String) = textboxRecu.Lines.ToList

            If (listTemp.Count = 10) Then
                listTemp.RemoveAt(9)
            End If

            listTemp.Insert(0, [text])
            textboxRecu.Lines = listTemp.ToArray
        End If
    End Sub

    ''Mofication 3
    Private Sub boutonEnvoie_Click(sender As Object, e As EventArgs) Handles boutonEnvoie.Click
        RaiseEvent envoyerMessage(listDestinataire.Text, Me.nomUtilisateur, textboxEnvoie.Text)
    End Sub

    ''Mofication 3
    Private Sub AfficherMessage(ByVal data As String)
        textboxRecu.Text = data
    End Sub

    ''Mofication 3
    Private Sub boutonQuitter_Click(sender As Object, e As EventArgs) Handles boutonQuitter.Click
        RaiseEvent enleveUtilisateur(Me.nomUtilisateur)
    End Sub
End Class