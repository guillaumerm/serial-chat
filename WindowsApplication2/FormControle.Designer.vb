<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormControle
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.listChatteurs = New System.Windows.Forms.ComboBox()
        Me.labelChatteurs = New System.Windows.Forms.Label()
        Me.boutonAjouter = New System.Windows.Forms.Button()
        Me.boutonRetirer = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'listChatteurs
        '
        Me.listChatteurs.FormattingEnabled = True
        Me.listChatteurs.Location = New System.Drawing.Point(15, 25)
        Me.listChatteurs.Name = "listChatteurs"
        Me.listChatteurs.Size = New System.Drawing.Size(263, 21)
        Me.listChatteurs.TabIndex = 0
        '
        'labelChatteurs
        '
        Me.labelChatteurs.AutoSize = True
        Me.labelChatteurs.Location = New System.Drawing.Point(12, 9)
        Me.labelChatteurs.Name = "labelChatteurs"
        Me.labelChatteurs.Size = New System.Drawing.Size(101, 13)
        Me.labelChatteurs.TabIndex = 1
        Me.labelChatteurs.Text = "Noms des chatteurs"
        '
        'boutonAjouter
        '
        Me.boutonAjouter.Location = New System.Drawing.Point(15, 52)
        Me.boutonAjouter.Name = "boutonAjouter"
        Me.boutonAjouter.Size = New System.Drawing.Size(75, 23)
        Me.boutonAjouter.TabIndex = 2
        Me.boutonAjouter.Text = "Ajouter"
        Me.boutonAjouter.UseVisualStyleBackColor = True
        '
        'boutonRetirer
        '
        Me.boutonRetirer.Location = New System.Drawing.Point(203, 52)
        Me.boutonRetirer.Name = "boutonRetirer"
        Me.boutonRetirer.Size = New System.Drawing.Size(75, 23)
        Me.boutonRetirer.TabIndex = 3
        Me.boutonRetirer.Text = "Retirer"
        Me.boutonRetirer.UseVisualStyleBackColor = True
        '
        'FormControle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 86)
        Me.Controls.Add(Me.boutonRetirer)
        Me.Controls.Add(Me.boutonAjouter)
        Me.Controls.Add(Me.labelChatteurs)
        Me.Controls.Add(Me.listChatteurs)
        Me.Name = "FormControle"
        Me.Text = "Fenetre Controle"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents listChatteurs As System.Windows.Forms.ComboBox
    Friend WithEvents labelChatteurs As System.Windows.Forms.Label
    Friend WithEvents boutonAjouter As System.Windows.Forms.Button
    Friend WithEvents boutonRetirer As System.Windows.Forms.Button
End Class
