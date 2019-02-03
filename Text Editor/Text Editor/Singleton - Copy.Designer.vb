<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Singleton
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.MatchCaseCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DownRadioButton = New System.Windows.Forms.RadioButton()
        Me.UpRadioButton = New System.Windows.Forms.RadioButton()
        Me.FindButton = New System.Windows.Forms.Button()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.ReplaceButton = New System.Windows.Forms.Button()
        Me.ReplaceAllButton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Find what"
        '
        'SearchTextBox
        '
        Me.SearchTextBox.Location = New System.Drawing.Point(96, 14)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(186, 27)
        Me.SearchTextBox.TabIndex = 1
        '
        'MatchCaseCheckBox
        '
        Me.MatchCaseCheckBox.AutoSize = True
        Me.MatchCaseCheckBox.Location = New System.Drawing.Point(20, 61)
        Me.MatchCaseCheckBox.Name = "MatchCaseCheckBox"
        Me.MatchCaseCheckBox.Size = New System.Drawing.Size(102, 24)
        Me.MatchCaseCheckBox.TabIndex = 2
        Me.MatchCaseCheckBox.Text = "Match case"
        Me.MatchCaseCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DownRadioButton)
        Me.GroupBox1.Controls.Add(Me.UpRadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(128, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(154, 76)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Direction"
        '
        'DownRadioButton
        '
        Me.DownRadioButton.AutoSize = True
        Me.DownRadioButton.Checked = True
        Me.DownRadioButton.Location = New System.Drawing.Point(58, 40)
        Me.DownRadioButton.Name = "DownRadioButton"
        Me.DownRadioButton.Size = New System.Drawing.Size(66, 24)
        Me.DownRadioButton.TabIndex = 1
        Me.DownRadioButton.TabStop = True
        Me.DownRadioButton.Text = "Down"
        Me.DownRadioButton.UseVisualStyleBackColor = True
        '
        'UpRadioButton
        '
        Me.UpRadioButton.AutoSize = True
        Me.UpRadioButton.Location = New System.Drawing.Point(6, 40)
        Me.UpRadioButton.Name = "UpRadioButton"
        Me.UpRadioButton.Size = New System.Drawing.Size(46, 24)
        Me.UpRadioButton.TabIndex = 0
        Me.UpRadioButton.Text = "Up"
        Me.UpRadioButton.UseVisualStyleBackColor = True
        '
        'FindButton
        '
        Me.FindButton.Location = New System.Drawing.Point(288, 14)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(125, 27)
        Me.FindButton.TabIndex = 4
        Me.FindButton.Text = "Find Next"
        Me.FindButton.UseVisualStyleBackColor = True
        '
        'CancelButton
        '
        Me.CancelButton.Location = New System.Drawing.Point(288, 113)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(125, 27)
        Me.CancelButton.TabIndex = 5
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'ReplaceButton
        '
        Me.ReplaceButton.Location = New System.Drawing.Point(288, 47)
        Me.ReplaceButton.Name = "ReplaceButton"
        Me.ReplaceButton.Size = New System.Drawing.Size(125, 27)
        Me.ReplaceButton.TabIndex = 6
        Me.ReplaceButton.Text = "Replace"
        Me.ReplaceButton.UseVisualStyleBackColor = True
        '
        'ReplaceAllButton
        '
        Me.ReplaceAllButton.Location = New System.Drawing.Point(288, 80)
        Me.ReplaceAllButton.Name = "ReplaceAllButton"
        Me.ReplaceAllButton.Size = New System.Drawing.Size(125, 27)
        Me.ReplaceAllButton.TabIndex = 7
        Me.ReplaceAllButton.Text = "Replace A&ll"
        Me.ReplaceAllButton.UseVisualStyleBackColor = True
        '
        'Singleton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 216)
        Me.Controls.Add(Me.ReplaceAllButton)
        Me.Controls.Add(Me.ReplaceButton)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.FindButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MatchCaseCheckBox)
        Me.Controls.Add(Me.SearchTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Singleton"
        Me.Text = "Singleton"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents SearchTextBox As TextBox
    Friend WithEvents MatchCaseCheckBox As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DownRadioButton As RadioButton
    Friend WithEvents UpRadioButton As RadioButton
    Friend WithEvents FindButton As Button
    Friend WithEvents CancelButton As Button
    Friend WithEvents ReplaceButton As Button
    Friend WithEvents ReplaceAllButton As Button
End Class
