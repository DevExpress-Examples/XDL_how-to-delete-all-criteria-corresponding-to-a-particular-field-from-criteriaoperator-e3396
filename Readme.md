<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128583014/15.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3396)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# How to delete all criteria corresponding to a particular field from CriteriaOperator


<p>This example demonstrates how to implement a helper that removes all expressions referencing a certain property from the <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressDataFilteringCriteriaOperatortopic">CriteriaOperator</a>. For example, you may want to programmatically remove fragments like <em>"[SavedPassword] = '***'"</em> from filter expressions that persist on client computers, because you decided to remove the SavedPassword property from the model for security reasons.<br><br>Although this task can be accomplished using <a href="https://msdn.microsoft.com/en-us/library/ewy2t5e0%28v=vs.110%29.aspx">regular expressions</a>, this approach is too complicated and is applicable only to CriteriaOperator expressions that follow a certain pattern. This example uses a different technique, which allows you to support expressions of any complexity with minimum effort.<br><br>The solution demonstrated in this example uses the concept described in this Knowledge Base article: <a href="https://www.devexpress.com/Support/Center/p/T320172">Implementation of the base class for a CriteriaOperator expression patcher</a>. The <strong>DevExpress.Data.Filtering.Helpers.ClientCriteriaLazyPatcherBase.AggragatesCommonProcessingBase</strong> class is used in this example as a base class for the <strong>CriteriaPatcherSkipProperties</strong> class (in version 17.2 and earlier use the <strong>CriteriaPatcherBase</strong> class implemented in the example).<br><br>To remove any reference to a specific property from the CriteriaOperator expression, override the CriteriaPatcherBase.VisitProperty method, and return <strong>null</strong> (<strong>Nothing</strong> in Visual Basic) if the property name matches the name of a property that should be removed.</p>


```cs
protected override CriteriaOperator VisitProperty(OperandProperty theOperand) {
	if(PropertiesToremove.Contains(theOperand.PropertyName)) return null;
	return theOperand;
}
```




```vb
Protected Overrides Function VisitProperty(ByVal theOperand As OperandProperty) As CriteriaOperator
	If PropertiesToremove.Contains(theOperand.PropertyName) Then
		Return Nothing
	End If
	Return theOperand
End Function
```


<p>However, this is incomplete. The expression will contain invalid statements if you simply remove properties from it:<em> "() = #2015-12-30# And StartsWith([City], 'q')"</em>. To delete invalid statements from the expression, override the <strong>VisitAggregate, </strong><strong>VisitFunction</strong>, <strong>VisitGroup</strong>, <strong>VisitIn</strong>, <strong>VisitUnary</strong>, <strong>VisitBinary</strong>, and <strong>VisitBetween</strong> methods. The implementation of overridden methods is demonstrated in the <em>CriteriaPatcherSkipProperties.cs</em> and <em>CriteriaPatcherSkipProperties.vb</em> files.<br><br><strong>See also</strong>:<br><a href="https://www.devexpress.com/Support/Center/p/T320172">Implementation of the base class for a CriteriaOperator expression patcher</a></p>
<p><a href="https://www.devexpress.com/Support/Center/p/E3347">How to create a custom converter to convert the CriteriaOperator to the System.String type</a></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=XDL_how-to-delete-all-criteria-corresponding-to-a-particular-field-from-criteriaoperator-e3396&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=XDL_how-to-delete-all-criteria-corresponding-to-a-particular-field-from-criteriaoperator-e3396&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
