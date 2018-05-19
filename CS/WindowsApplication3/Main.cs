using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;


namespace DXSample {
    public partial class Main: XtraForm {
        public Main() {
            InitializeComponent();
        }

        string fieldToRemove = string.Empty;
        private void OnFormLoad(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'nwindDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.nwindDataSet.Employees);

            PopilateRadioGroup();
        }

        private void PopilateRadioGroup()
        {
            foreach (GridColumn col in gridView1.Columns)
                radioGroup1.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(col.FieldName, col.GetCaption()));
        }

        private void OnRemoveFieldChanged(object sender, EventArgs e)
        {
            RadioGroup radioGroup = sender as RadioGroup;
            object val = radioGroup.EditValue;
            fieldToRemove = val.ToString();
        }

        private void OnApplyFilter(object sender, EventArgs e)
        {
            filterControl1.ApplyFilter();
        }

        private void OnRemoveCriteriaByField(object sender, EventArgs e)
        {
            filterControl1.FilterCriteria = CriteriaHelper.RemoveCriteriaByFieldName(fieldToRemove, filterControl1.FilterCriteria);
        }
    }
}
