<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormUtilisateur
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.listDestinataire = New System.Windows.Forms.ComboBox()
        Me.labelDestinataire = New System.Windows.Forms.Label()
        Me.textboxEnvoie = New System.Windows.Forms.TextBox()
        Me.textboxRecu = New System.Windows.Forms.RichTextBox()
        Me.boutonEnvoie = New System.Windows.Forms.Button()
        Me.boutonQuitter = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'listDestinataire
        '
        Me.listDestinataire.FormattingEnabled = True
        Me.listDestinataire.Location = New System.Drawing.Point(64, 92)
        Me.listDestinataire.Name = "listDestinataire"
        Me.listDestinataire.Size = New System.Drawing.Size(121, 21)
        Me.listDestinataire.TabIndex = 0
        '
        'labelDestinataire
        '
        Me.labelDestinataire.AutoSize = True
        Me.labelDestinataire.Location = New System.Drawing.Point(64, 73)
        Me.labelDestinataire.Name = "labelDestinataire"
        Me.labelDestinataire.Size = New System.Drawing.Size(63, 13)
        Me.labelDestinataire.TabIndex = 1
        Me.labelDestinataire.Text = "Destinataire"
        '
        'textboxEnvoie
        '
        Me.textboxEnvoie.Location = New System.Drawing.Point(67, 169)
        Me.textboxEnvoie.Name = "textboxEnvoie"
        Me.textboxEnvoie.Size = New System.Drawing.Size(385, 20)
        Me.textboxEnvoie.TabIndex = 2
        '
        'textboxRecu
        '
        Me.textboxRecu.Location = New System.Drawing.Point(67, 238)
        Me.textboxRecu.Name = "textboxRecu"
        Me.textboxRecu.Size = New System.Drawing.Size(466, 217)
        Me.textboxRecu.TabIndex = 3
        Me.textboxRecu.Text = ""
        '
        'boutonEnvoie
        '
        Me.boutonEnvoie.Location = New System.Drawing.Point(458, 169)
        Me.boutonEnvoie.Name = "boutonEnvoie"
        Me.boutonEnvoie.Size = New System.Drawing.Size(75, 23)
        Me.boutonEnvoie.TabIndex = 4
        Me.boutonEnvoie.Text = "Envoyer"
        Me.boutonEnvoie.UseVisualStyleBackColor = True
        '
        'boutonQuitter
        '
        Me.boutonQuitter.Location = New System.Drawing.Point(519, 471)
        Me.boutonQuitter.Name = "boutonQuitter"
        Me.boutonQuitter.Size = New System.Drawing.Size(75, 23)
        Me.boutonQuitter.TabIndex = 5
        Me.boutonQuitter.Text = "Quitter"
        Me.boutonQuitter.UseVisualStyleBackColor = True
        '
        'FormUtilisateur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 506)
        Me.Controls.Add(Me.boutonQuitter)
        Me.Controls.Add(Me.boutonEnvoie)
        Me.Controls.Add(Me.textboxRecu)
        Me.Controls.Add(Me.textboxEnvoie)
        Me.Controls.Add(Me.labelDestinataire)
        Me.Controls.Add(Me.listDestinataire)
        Me.Name = "FormUtilisateur"
        Me.Text = "FormUtilisateur"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents listDestinataire As System.Windows.Forms.ComboBox
    Friend WithEvents labelDestinataire As System.Windows.Forms.Label
    Friend WithEvents textboxEnvoie As System.Windows.Forms.TextBox
    Friend WithEvents textboxRecu As System.Windows.Forms.RichTextBox
    Friend WithEvents boutonEnvoie As System.Windows.Forms.Button
    Friend WithEvents boutonQuitter As System.Windows.Forms.Button
End Class
