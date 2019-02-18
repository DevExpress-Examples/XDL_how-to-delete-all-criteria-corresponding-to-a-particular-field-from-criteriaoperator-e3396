Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns


Namespace DXSample
	Partial Public Class Main
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()
		End Sub

		Private fieldToRemove As String = String.Empty
		Private Sub OnFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			' TODO: This line of code loads data into the 'nwindDataSet.Employees' table. You can move, or remove it, as needed.
			Me.employeesTableAdapter.Fill(Me.nwindDataSet.Employees)

			PopilateRadioGroup()
		End Sub

		Private Sub PopilateRadioGroup()
			For Each col As GridColumn In gridView1.Columns
				radioGroup1.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(col.FieldName, col.GetCaption()))
			Next col
		End Sub

		Private Sub OnRemoveFieldChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioGroup1.EditValueChanged
			Dim radioGroup As RadioGroup = TryCast(sender, RadioGroup)
			Dim val As Object = radioGroup.EditValue
			fieldToRemove = val.ToString()
		End Sub

		Private Sub OnApplyFilter(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			filterControl1.ApplyFilter()
		End Sub

		Private Sub OnRemoveCriteriaByField(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
			filterControl1.FilterCriteria = CriteriaHelper.RemoveCriteriaByFieldName(fieldToRemove, filterControl1.FilterCriteria)
		End Sub
	End Class
End Namespace
