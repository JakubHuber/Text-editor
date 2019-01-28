Public Class Singleton
    Private Shared myInstance As Singleton = Nothing
    Private Shared ReadOnly aLock As New Object

    'Changed public to private to do not allow to instance form from outside
    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Singleton_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Cancel closing event
        e.Cancel = True
        Me.Hide()

    End Sub

    Public Shared Function GetInstance() As Singleton
        'Tread safe
        SyncLock aLock
            'If constructor has not been evoked, call constuctor and return instance
            If myInstance Is Nothing Then
                myInstance = New Singleton

            End If
            Return myInstance
        End SyncLock
    End Function

    Public Sub FindText()
        Dim startPosition As Integer
        Dim foundPosition As Integer

        With MainForm.MyTextBox
            If startPosition <> .SelectionStart Then
                If DownRadioButton.Checked Then
                    startPosition = .SelectionStart + 1
                    If startPosition > .TextLength Then
                        startPosition = .TextLength
                    End If
                Else
                    startPosition = .SelectionStart - 1
                    If startPosition < 0 Then
                        startPosition = 0
                    End If
                End If
            End If

            If DownRadioButton.Checked Then
                If MatchCaseCheckBox.Checked Then
                    foundPosition = .Text.IndexOf(MainForm.findString, startPosition, StringComparison.Ordinal)
                Else
                    foundPosition = .Text.IndexOf(MainForm.findString, startPosition, StringComparison.OrdinalIgnoreCase)
                End If
            Else
                If MatchCaseCheckBox.Checked Then
                    foundPosition = .Text.LastIndexOf(MainForm.findString.ToString, startPosition, StringComparison.Ordinal)
                Else
                    foundPosition = .Text.LastIndexOf(MainForm.findString.ToString, startPosition, StringComparison.OrdinalIgnoreCase)
                End If
            End If

            If foundPosition = -1 Then
                If DialogResult.Yes = MessageBox.Show("Cannot find""" & MainForm.findString & """. Reverse search direction?",
                                                      MainForm.applicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                    DownRadioButton.Checked = Not DownRadioButton.Checked
                    UpRadioButton.Checked = Not UpRadioButton.Checked
                End If
                .ScrollToCaret()
                .Focus()
                Exit Sub
            End If

            .Select(foundPosition, MainForm.findString.Length)
            .ScrollToCaret()
            .Focus()
        End With
    End Sub

    Private Sub SearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles SearchTextBox.TextChanged
        'When find string changes update Mainforms variable
        If SearchTextBox.Text.Length > 0 Then
            MainForm.findString = SearchTextBox.Text
            'Unselect last found but keep cursor in location
            MainForm.MyTextBox.Select(MainForm.MyTextBox.SelectionStart, 0)
            FindButton.Enabled = True
        Else
            MainForm.findString = String.Empty
            FindButton.Enabled = False
        End If
    End Sub

    Private Sub FindButton_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        FindText()
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        Me.Hide()
    End Sub

    Private Sub Singleton_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If MainForm.findString <> String.Empty Then
            SearchTextBox.Text = MainForm.findString
        End If
    End Sub
End Class