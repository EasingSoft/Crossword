<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.MinLen = New System.Windows.Forms.NumericUpDown()
        Me.TotalWords = New System.Windows.Forms.NumericUpDown()
        Me.CapitalPer = New System.Windows.Forms.NumericUpDown()
        Me.MaxLen = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.WordList = New System.Windows.Forms.TextBox()
        Me.GenerateBtn = New System.Windows.Forms.Button()
        Me.Grid = New System.Windows.Forms.DataGridView()
        Me.Randomize = New System.Windows.Forms.Button()
        Me.GapLbl = New System.Windows.Forms.Label()
        CType(Me.MinLen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TotalWords, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CapitalPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaxLen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MinLen
        '
        Me.MinLen.Location = New System.Drawing.Point(145, 22)
        Me.MinLen.Maximum = New Decimal(New Integer() {7, 0, 0, 0})
        Me.MinLen.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.MinLen.Name = "MinLen"
        Me.MinLen.Size = New System.Drawing.Size(73, 26)
        Me.MinLen.TabIndex = 0
        Me.MinLen.Value = New Decimal(New Integer() {2, 0, 0, 0})
        Me.MinLen.Visible = False
        '
        'TotalWords
        '
        Me.TotalWords.Location = New System.Drawing.Point(145, 54)
        Me.TotalWords.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.TotalWords.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TotalWords.Name = "TotalWords"
        Me.TotalWords.Size = New System.Drawing.Size(73, 26)
        Me.TotalWords.TabIndex = 1
        Me.TotalWords.Value = New Decimal(New Integer() {3, 0, 0, 0})
        Me.TotalWords.Visible = False
        '
        'CapitalPer
        '
        Me.CapitalPer.Location = New System.Drawing.Point(145, 118)
        Me.CapitalPer.Name = "CapitalPer"
        Me.CapitalPer.Size = New System.Drawing.Size(73, 26)
        Me.CapitalPer.TabIndex = 2
        Me.CapitalPer.Value = New Decimal(New Integer() {34, 0, 0, 0})
        '
        'MaxLen
        '
        Me.MaxLen.Location = New System.Drawing.Point(145, 86)
        Me.MaxLen.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.MaxLen.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.MaxLen.Name = "MaxLen"
        Me.MaxLen.Size = New System.Drawing.Size(73, 26)
        Me.MaxLen.TabIndex = 3
        Me.MaxLen.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Min Length"
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Max Length"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Total Words"
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Capital %"
        '
        'WordList
        '
        Me.WordList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.WordList.Location = New System.Drawing.Point(26, 205)
        Me.WordList.Multiline = True
        Me.WordList.Name = "WordList"
        Me.WordList.Size = New System.Drawing.Size(192, 150)
        Me.WordList.TabIndex = 8
        '
        'GenerateBtn
        '
        Me.GenerateBtn.Location = New System.Drawing.Point(26, 154)
        Me.GenerateBtn.Name = "GenerateBtn"
        Me.GenerateBtn.Size = New System.Drawing.Size(192, 35)
        Me.GenerateBtn.TabIndex = 9
        Me.GenerateBtn.Text = "Generate"
        Me.GenerateBtn.UseVisualStyleBackColor = True
        '
        'Grid
        '
        Me.Grid.AllowUserToAddRows = False
        Me.Grid.AllowUserToResizeColumns = False
        Me.Grid.AllowUserToResizeRows = False
        Me.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid.ColumnHeadersVisible = False
        Me.Grid.Location = New System.Drawing.Point(290, 154)
        Me.Grid.Name = "Grid"
        Me.Grid.ReadOnly = True
        Me.Grid.RowHeadersVisible = False
        Me.Grid.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.Grid.Size = New System.Drawing.Size(280, 200)
        Me.Grid.TabIndex = 10
        '
        'Randomize
        '
        Me.Randomize.Location = New System.Drawing.Point(290, 319)
        Me.Randomize.Name = "Randomize"
        Me.Randomize.Size = New System.Drawing.Size(280, 35)
        Me.Randomize.TabIndex = 11
        Me.Randomize.Text = "Visualize"
        Me.Randomize.UseVisualStyleBackColor = True
        '
        'GapLbl
        '
        Me.GapLbl.AutoSize = True
        Me.GapLbl.Location = New System.Drawing.Point(286, 124)
        Me.GapLbl.Name = "GapLbl"
        Me.GapLbl.Size = New System.Drawing.Size(0, 20)
        Me.GapLbl.TabIndex = 12
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(595, 375)
        Me.Controls.Add(Me.GapLbl)
        Me.Controls.Add(Me.Randomize)
        Me.Controls.Add(Me.Grid)
        Me.Controls.Add(Me.GenerateBtn)
        Me.Controls.Add(Me.WordList)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MaxLen)
        Me.Controls.Add(Me.CapitalPer)
        Me.Controls.Add(Me.TotalWords)
        Me.Controls.Add(Me.MinLen)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CrossWord"
        CType(Me.MinLen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TotalWords, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CapitalPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaxLen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MinLen As NumericUpDown
    Friend WithEvents TotalWords As NumericUpDown
    Friend WithEvents CapitalPer As NumericUpDown
    Friend WithEvents MaxLen As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents WordList As TextBox
    Friend WithEvents GenerateBtn As Button
    Friend WithEvents Grid As DataGridView
    Friend WithEvents Randomize As Button
    Friend WithEvents GapLbl As Label
End Class
