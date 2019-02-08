Imports System.ComponentModel
Imports System.IO
Imports System.Security

Public Class MainForm

#Region "Class variables"
    Public applicationTitle As String = My.Application.Info.Title
    Private workingPathFile As String = String.Empty
    Private Const fileFilter As String = "Plain text (*.txt)|*.txt|Log files (*.log)|*.log|All files (*.*)|*.*"
    Private workingPath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
    Public findString As String = String.Empty

    'Drag and drop
    Private mouseIsDown As Boolean = False
    Private textToMove As String = String.Empty
    Private textToMoveStart As Integer

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
        SaveSettings()

        If SaveChanges() Then
            SaveToolStripMenuItem_Click(sender, e)
        End If
    End Sub
#End Region

#Region "Undo Cut copy paste"
    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        With MyTextBox
            If .CanUndo Then
                .Undo()
                .ClearUndo()
            End If
        End With
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click, DeleteToolStripMenuItem.Click, CutToolStripButton.Click
        If MyTextBox.SelectedText <> String.Empty Then
            MyTextBox.Cut()
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click, CopyToolStripButton.Click
        If MyTextBox.SelectionLength > 0 Then
            MyTextBox.Copy()
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If Clipboard.GetDataObject.GetDataPresent(DataFormats.Text) Then
            If MyTextBox.SelectionLength > 0 Then
                'Paste over selected text
                If MessageBox.Show(Me, "Do you want to paste over current selection?", "Paste over confirm",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                    'Move selection to the point after current selection and paste
                    MyTextBox.SelectionStart = MyTextBox.SelectionStart + MyTextBox.SelectionLength
                End If
            End If
            MyTextBox.Paste()
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        MyTextBox.SelectAll()
    End Sub

    Private Sub TimeDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimeDateToolStripMenuItem.Click
        MyTextBox.SelectedText = Now.ToShortTimeString & " " & Today.ToShortDateString
        MyTextBox.Modified = True
    End Sub

    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem.Click
        With FontDialog1
            .ShowColor = True
            .ShowApply = True
            .Font = MyTextBox.Font
            .Color = MyTextBox.ForeColor
            If .ShowDialog = DialogResult.OK Then
                With MyTextBox
                    .Font = FontDialog1.Font
                    .ForeColor = FontDialog1.Color
                End With
            End If
        End With
    End Sub

    Private Sub FontDialog1_Apply(sender As Object, e As EventArgs) Handles FontDialog1.Apply
        With MyTextBox
            .Font = FontDialog1.Font
            .ForeColor = FontDialog1.Color
        End With
    End Sub

    Private Sub WordWrapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WordWrapToolStripMenuItem.Click
        MyTextBox.WordWrap = Not MyTextBox.WordWrap
        WordWrapToolStripMenuItem.Checked = MyTextBox.WordWrap
        If MyTextBox.WordWrap Then
            MyTextBox.ScrollBars = ScrollBars.Vertical
        Else
            MyTextBox.ScrollBars = ScrollBars.Both
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub

#End Region

#Region "Find/Find replace/Find next"


    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        'Singelton design pattern
        Dim aSingeltonForm As Singleton = Singleton.GetInstance
        aSingeltonForm.Show()
    End Sub

    Private Sub FindNextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindNextToolStripMenuItem.Click
        Dim aSingeltonForm As Singleton = Singleton.GetInstance
        aSingeltonForm.Show()
    End Sub

    Private Sub ReplaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReplaceToolStripMenuItem.Click
        Dim aSingeltonForm As Singleton = Singleton.GetInstance
        aSingeltonForm.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If MyTextBox.CanUndo Then
            UndoToolStripMenuItem.Enabled = True
        Else
            UndoToolStripMenuItem.Enabled = True
        End If

        If MyTextBox.Text.Length > 0 Then
            CutToolStripButton.Enabled = True
            CutToolStripMenuItem.Enabled = True
            CopyToolStripButton.Enabled = True
            CopyToolStripMenuItem.Enabled = True
            DeleteToolStripMenuItem.Enabled = True
        Else
            CutToolStripButton.Enabled = False
            CutToolStripMenuItem.Enabled = False
            CopyToolStripButton.Enabled = False
            CopyToolStripMenuItem.Enabled = False
            DeleteToolStripMenuItem.Enabled = False
        End If

        If String.IsNullOrWhiteSpace(MyTextBox.Text) Then
            SaveToolStripButton.Enabled = False
            SaveToolStripMenuItem.Enabled = False
            SaveAsToolStripMenuItem.Enabled = False
            SaveToolStripButton.Enabled = False
        Else
            SaveToolStripButton.Enabled = True
            SaveToolStripMenuItem.Enabled = True
            SaveAsToolStripMenuItem.Enabled = True
            SaveToolStripButton.Enabled = True
        End If
    End Sub
#End Region

#Region "Apply Settings"
    Private Sub ApplySettings()
        If My.Settings.FormSize <> Drawing.Size.Empty Then
            Me.Size = My.Settings.FormSize
        End If

        If My.Settings.FormLocation <> Drawing.Point.Empty Then
            Me.Location = My.Settings.FormLocation
        End If

        If My.Settings.FormFontColor <> System.Drawing.Color.Empty Then
            MyTextBox.ForeColor = My.Settings.FormFontColor
        End If

        If (My.Settings.Font.Name <> String.Empty) AndAlso (My.Settings.Font.Size > 0) Then
            MyTextBox.Font = My.Settings.Font
        End If

    End Sub
#End Region

    Private Sub SaveSettings()
        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.FormSize = Me.Size
        Else
            My.Settings.FormSize = Me.RestoreBounds.Size
        End If

        With My.Settings
            .FormLocation = Me.Location
            .FormFontColor = MyTextBox.ForeColor
            .Font = MyTextBox.Font
        End With
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplySettings()
    End Sub

    Private Sub MyTextBox_MouseDown(sender As Object, e As MouseEventArgs) Handles MyTextBox.MouseDown
        'Internal drag and drop
        With MyTextBox
            If .SelectionLength > 0 Then
                textToMove = .SelectedText
                textToMoveStart = .SelectionStart
                mouseIsDown = True
            End If
        End With
    End Sub

    Private Sub MyTextBox_MouseMove(sender As Object, e As MouseEventArgs) Handles MyTextBox.MouseMove
        'if dragging set dragdrop effect
        With MyTextBox
            If mouseIsDown AndAlso (.SelectionLength > 0) Then
                .DoDragDrop(.SelectedText, DragDropEffects.Copy Or DragDropEffects.Move)
            End If
        End With
        mouseIsDown = False
    End Sub

    Private Sub MyTextBox_DragOver(sender As Object, e As DragEventArgs) Handles MyTextBox.DragOver
        'determine cursor location
        With MyTextBox
            .Select(.GetCharIndexFromPosition(.PointToClient(Cursor.Position)), 0)
            .ScrollToCaret()
            .Focus()
        End With
    End Sub

    Private Sub MyTextBox_DragEnter(sender As Object, e As DragEventArgs) Handles MyTextBox.DragEnter
        'Check the data format being droped
        If e.Data.GetDataPresent(DataFormats.Text) Then
            If (e.KeyState And 8) = 8 AndAlso (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy Then
                'ctrl for copy
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub MyTextBox_DragDrop(sender As Object, e As DragEventArgs) Handles MyTextBox.DragDrop
        With MyTextBox
            'determine cursor location and insert data
            Dim index As Integer = .GetCharIndexFromPosition(.PointToClient(Cursor.Position))
            .Text = .Text.Insert(index, e.Data.GetData(DataFormats.Text).ToString)
            'If internal move then delete data from previous location
            If (e.Effect = DragDropEffects.Move) AndAlso textToMove <> String.Empty Then
                Dim foundPosition As Integer = .Text.IndexOf(textToMove, textToMoveStart, StringComparison.OrdinalIgnoreCase)
                If foundPosition > -1 Then
                    .Select(foundPosition, textToMove.Length)
                    .SelectedText = String.Empty
                    'reposition index
                    If foundPosition < index Then
                        index -= textToMove.Length
                    End If
                    If index < 0 Then
                        index = 0
                    End If
                End If
            End If
            'higlhlight new or move data
            .Select(index, e.Data.GetData(DataFormats.Text).ToString.Length)
            .ScrollToCaret()
            .Focus()
            .Modified = True
            textToMove = String.Empty
            textToMoveStart = 0

        End With
    End Sub




End Class
