Imports Microsoft.VisualBasic
Imports System
Namespace DXSample
	Partial Public Class Main
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.defaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
			Me.filterControl1 = New DevExpress.XtraEditors.FilterControl()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.employeesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.nwindDataSet = New DXSample.nwindDataSet()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.colEmployeeID = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colFirstName = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colTitle = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colBirthDate = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colCity = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colRegion = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colCountry = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.employeesTableAdapter = New DXSample.nwindDataSetTableAdapters.EmployeesTableAdapter()
			Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
			Me.radioGroup1 = New DevExpress.XtraEditors.RadioGroup()
			Me.simpleButton1 = New DevExpress.XtraEditors.SimpleButton()
			Me.simpleButton2 = New DevExpress.XtraEditors.SimpleButton()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.employeesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nwindDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.panelControl1.SuspendLayout()
			CType(Me.radioGroup1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' defaultLookAndFeel1
			' 
			Me.defaultLookAndFeel1.LookAndFeel.SkinName = "Sharp"
			' 
			' filterControl1
			' 
			Me.filterControl1.Cursor = System.Windows.Forms.Cursors.Arrow
			Me.filterControl1.Dock = System.Windows.Forms.DockStyle.Right
			Me.filterControl1.Location = New System.Drawing.Point(722, 0)
			Me.filterControl1.Name = "filterControl1"
			Me.filterControl1.Size = New System.Drawing.Size(297, 339)
			Me.filterControl1.SourceControl = Me.gridControl1
			Me.filterControl1.TabIndex = 0
			Me.filterControl1.Text = "filterControl1"
			' 
			' gridControl1
			' 
			Me.gridControl1.DataSource = Me.employeesBindingSource
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.Location = New System.Drawing.Point(0, 0)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(1019, 339)
			Me.gridControl1.TabIndex = 1
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' employeesBindingSource
			' 
			Me.employeesBindingSource.DataMember = "Employees"
			Me.employeesBindingSource.DataSource = Me.nwindDataSet
			' 
			' nwindDataSet
			' 
			Me.nwindDataSet.DataSetName = "nwindDataSet"
			Me.nwindDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
			' 
			' gridView1
			' 
			Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colEmployeeID, Me.colFirstName, Me.colTitle, Me.colBirthDate, Me.colCity, Me.colRegion, Me.colCountry})
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			' 
			' colEmployeeID
			' 
			Me.colEmployeeID.FieldName = "EmployeeID"
			Me.colEmployeeID.Name = "colEmployeeID"
			Me.colEmployeeID.Visible = True
			Me.colEmployeeID.VisibleIndex = 0
			' 
			' colFirstName
			' 
			Me.colFirstName.FieldName = "FirstName"
			Me.colFirstName.Name = "colFirstName"
			Me.colFirstName.Visible = True
			Me.colFirstName.VisibleIndex = 1
			' 
			' colTitle
			' 
			Me.colTitle.FieldName = "Title"
			Me.colTitle.Name = "colTitle"
			Me.colTitle.Visible = True
			Me.colTitle.VisibleIndex = 2
			' 
			' colBirthDate
			' 
			Me.colBirthDate.FieldName = "BirthDate"
			Me.colBirthDate.Name = "colBirthDate"
			Me.colBirthDate.Visible = True
			Me.colBirthDate.VisibleIndex = 3
			' 
			' colCity
			' 
			Me.colCity.FieldName = "City"
			Me.colCity.Name = "colCity"
			Me.colCity.Visible = True
			Me.colCity.VisibleIndex = 4
			' 
			' colRegion
			' 
			Me.colRegion.FieldName = "Region"
			Me.colRegion.Name = "colRegion"
			Me.colRegion.Visible = True
			Me.colRegion.VisibleIndex = 5
			' 
			' colCountry
			' 
			Me.colCountry.FieldName = "Country"
			Me.colCountry.Name = "colCountry"
			Me.colCountry.Visible = True
			Me.colCountry.VisibleIndex = 6
			' 
			' employeesTableAdapter
			' 
			Me.employeesTableAdapter.ClearBeforeFill = True
			' 
			' panelControl1
			' 
			Me.panelControl1.Controls.Add(Me.simpleButton2)
			Me.panelControl1.Controls.Add(Me.radioGroup1)
			Me.panelControl1.Controls.Add(Me.simpleButton1)
			Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.panelControl1.Location = New System.Drawing.Point(0, 339)
			Me.panelControl1.Name = "panelControl1"
			Me.panelControl1.Size = New System.Drawing.Size(1019, 57)
			Me.panelControl1.TabIndex = 4
			' 
			' radioGroup1
			' 
			Me.radioGroup1.Location = New System.Drawing.Point(204, 9)
			Me.radioGroup1.Name = "radioGroup1"
			Me.radioGroup1.Size = New System.Drawing.Size(605, 38)
			Me.radioGroup1.TabIndex = 1
'			Me.radioGroup1.EditValueChanged += New System.EventHandler(Me.OnRemoveFieldChanged);
			' 
			' simpleButton1
			' 
			Me.simpleButton1.Location = New System.Drawing.Point(22, 7)
			Me.simpleButton1.Name = "simpleButton1"
			Me.simpleButton1.Size = New System.Drawing.Size(146, 40)
			Me.simpleButton1.TabIndex = 0
			Me.simpleButton1.Text = "ApplyFilter"
'			Me.simpleButton1.Click += New System.EventHandler(Me.OnApplyFilter);
			' 
			' simpleButton2
			' 
			Me.simpleButton2.Location = New System.Drawing.Point(838, 10)
			Me.simpleButton2.Name = "simpleButton2"
			Me.simpleButton2.Size = New System.Drawing.Size(150, 37)
			Me.simpleButton2.TabIndex = 2
			Me.simpleButton2.Text = "Remove Criteria By Field"
'			Me.simpleButton2.Click += New System.EventHandler(Me.OnRemoveCriteriaByField);
			' 
			' Main
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(1019, 396)
			Me.Controls.Add(Me.filterControl1)
			Me.Controls.Add(Me.gridControl1)
			Me.Controls.Add(Me.panelControl1)
			Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
			Me.Name = "Main"
			Me.Text = "IClientCriteriaVisitor"
'			Me.Load += New System.EventHandler(Me.OnFormLoad);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.employeesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nwindDataSet, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.panelControl1.ResumeLayout(False)
			CType(Me.radioGroup1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private defaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
		Private filterControl1 As DevExpress.XtraEditors.FilterControl
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private nwindDataSet As nwindDataSet
		Private employeesBindingSource As System.Windows.Forms.BindingSource
		Private employeesTableAdapter As DXSample.nwindDataSetTableAdapters.EmployeesTableAdapter
		Private colEmployeeID As DevExpress.XtraGrid.Columns.GridColumn
		Private colFirstName As DevExpress.XtraGrid.Columns.GridColumn
		Private colTitle As DevExpress.XtraGrid.Columns.GridColumn
		Private colBirthDate As DevExpress.XtraGrid.Columns.GridColumn
		Private colCity As DevExpress.XtraGrid.Columns.GridColumn
		Private colRegion As DevExpress.XtraGrid.Columns.GridColumn
		Private colCountry As DevExpress.XtraGrid.Columns.GridColumn
		Private panelControl1 As DevExpress.XtraEditors.PanelControl
		Private WithEvents simpleButton1 As DevExpress.XtraEditors.SimpleButton
		Private WithEvents radioGroup1 As DevExpress.XtraEditors.RadioGroup
		Private WithEvents simpleButton2 As DevExpress.XtraEditors.SimpleButton
	End Class
End Namespace

