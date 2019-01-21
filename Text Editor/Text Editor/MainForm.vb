Imports System.ComponentModel
Imports System.IO
Imports System.Security

Public Class MainForm

#Region "Class variables"
    Public applicationTitle As String = My.Application.Info.Title
    Private workingPathFile As String = String.Empty
    Private Const fileFilter As String = "Plain text (*.txt)|*.txt|Log files (*.log)|*.log|All files (*.*)|*.*"
    Private workingPath As String = My.Computer.FileSystem.SpecialDirectories.Desktop

#End Region

#Region "File menu"
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles NewToolStripMenuItem.Click, NewToolStripButton.Click

        If SaveChanges() Then
            SaveToolStripMenuItem_Click(sender, e)
        End If

        workingPathFile = String.Empty
        Me.Text = applicationTitle

        With MyTextBox
            .Clear()
            .Modified = False
            .Focus()
        End With
    End Sub

    Function SaveChanges() As Boolean
        If MyTextBox.Modified Then
            If MsgBoxResult.Yes = MessageBox.Show("Do you want to save changes?", "Save changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                Return True
            End If
        End If
    End Function

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        If workingPathFile <> String.Empty AndAlso (workingPathFile.EndsWith(".txt") OrElse workingPathFile.EndsWith(".log")) Then
            SaveFile()
        Else
            SaveAsToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        If SaveChanges() Then
            SaveToolStripMenuItem_Click(sender, e)
        End If

        With OpenFileDialog1
            .Multiselect = False
            .Filter = fileFilter
            .DefaultExt = "txt"
            .FileName = String.Empty
            .InitialDirectory = workingPath
            If .ShowDialog = DialogResult.OK Then
                workingPathFile = .FileName
                GetFile()
            End If
        End With

    End Sub
    'Read file to Textbox
    Sub GetFile()
        Try
            With MyTextBox
                .Text = My.Computer.FileSystem.ReadAllText(workingPathFile)
                .Modified = False
                .Focus()
            End With

            workingPath = Path.GetDirectoryName(workingPathFile)
            Me.Text = applicationTitle & " - " & Path.GetFileName(workingPathFile)

        Catch ex As IOException
            MessageBox.Show(ex.Message, "File IO error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As SecurityException
            MessageBox.Show(ex.Message, "File permission error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error with opening selected file." & ControlChars.NewLine & ex.Message, "File permission error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        With SaveFileDialog1
            .Filter = fileFilter
            .DefaultExt = "txt"
            If workingPathFile = String.Empty Then
                .FileName = "Document.txt"
                .InitialDirectory = workingPath
            Else
                .FileName = Path.GetFileName(workingPathFile)
                .InitialDirectory = Path.GetDirectoryName(workingPathFile)
            End If
            If .ShowDialog = DialogResult.OK Then
                workingPathFile = .FileName
                SaveFile()
            End If
        End With
    End Sub

    Private Sub SaveFile()
        Try
            My.Computer.FileSystem.WriteAllText(workingPathFile, MyTextBox.Text, False)
            MyTextBox.Modified = False
            MyTextBox.Focus()

            workingPath = Path.GetDirectoryName(workingPathFile)
            Me.Text = applicationTitle & " - " & Path.GetFileName(workingPathFile)
        Catch ex As IOException
            MessageBox.Show(ex.Message, "File IO error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As SecurityException
            MessageBox.Show(ex.Message, "File permission error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error with opening selected file." & ControlChars.NewLine & ex.Message, "File permission error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'Save settings
        If SaveChanges() Then
            SaveToolStripMenuItem_Click(sender, e)
        End If
    End Sub
#End Region

End Class
