Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns

Namespace DXSample

    Public Partial Class Main
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private fieldToRemove As String = String.Empty

        Private Sub OnFormLoad(ByVal sender As Object, ByVal e As EventArgs)
            ' TODO: This line of code loads data into the 'nwindDataSet.Employees' table. You can move, or remove it, as needed.
            employeesTableAdapter.Fill(nwindDataSet.Employees)
            PopilateRadioGroup()
        End Sub

        Private Sub PopilateRadioGroup()
            For Each col As GridColumn In gridView1.Columns
                radioGroup1.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(col.FieldName, col.GetCaption()))
            Next
        End Sub

        Private Sub OnRemoveFieldChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim radioGroup As RadioGroup = TryCast(sender, RadioGroup)
            Dim val As Object = radioGroup.EditValue
            fieldToRemove = val.ToString()
        End Sub

        Private Sub OnApplyFilter(ByVal sender As Object, ByVal e As EventArgs)
            filterControl1.ApplyFilter()
        End Sub

        Private Sub OnRemoveCriteriaByField(ByVal sender As Object, ByVal e As EventArgs)
            filterControl1.FilterCriteria = RemoveCriteriaByFieldName(fieldToRemove, filterControl1.FilterCriteria)
        End Sub
    End Class
End Namespace
