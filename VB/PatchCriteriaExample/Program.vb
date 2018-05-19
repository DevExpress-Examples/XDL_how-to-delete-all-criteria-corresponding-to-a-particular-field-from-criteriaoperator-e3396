Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Data.Filtering
Imports PatchCriteriaExample.Filtering
Imports System.Diagnostics

Namespace PatchCriteriaExample
	Friend NotInheritable Class Program

		Private Sub New()
		End Sub

		Shared Sub Main()
			Dim expectedResult As String = "[A] = 'B'"
			Dim aggregateSkipCollection As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("ToRemove[C = 'D'] and A = 'B'"), "ToRemove").ToString()
			Dim aggregateSkipCondition As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("C[ToRemove = 'D'] and A = 'B'"), "ToRemove").ToString()
			Dim aggregateSkipExpression As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("C[D ='A'].Sum(ToRemove) = 10 and A = 'B'"), "ToRemove").ToString()
			Dim betweenSkipLeftOperand As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("ToRemove between (1, 3) and A ='B'"), "ToRemove").ToString()
			Dim betweenSkipBeginExpression As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("C between (ToRemove, D) and A = 'B'"), "ToRemove").ToString()
			Dim betweenSkipEndExpression As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("C between (D, ToRemove) and A = 'B'"), "ToRemove").ToString()
			Dim binarySkipLeftOperand As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("ToRemove > 1 and A = 'B'"), "ToRemove").ToString()
			Dim binarySkipRightOperand As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("C < ToRemove and A = 'B'"), "ToRemove").ToString()
			Dim functionSkipArgument As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("AddDays(ToRemove, 1) >= C and A = 'B'"), "ToRemove").ToString()
			Dim inSkipLeftOperand As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("ToRemove in (1, 2, 3) and A = 'B'"), "ToRemove").ToString()
			Dim inSkipOperand As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("C in (ToRemove, D, E) and A = 'B'"), "ToRemove").ToString()
			Dim unarySkipOperand As String = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("ToRemove is null and A = 'B'"), "ToRemove").ToString()
			Dim skipSeveral As CriteriaOperator = CriteriaPatcherSkipProperties.Patch(CriteriaOperator.Parse("C is null and A = 'B'"), "A,C")
			Debug.Assert(expectedResult = aggregateSkipCollection, "aggregateSkipCollection", "{0}", aggregateSkipCollection)
			Debug.Assert(expectedResult = aggregateSkipCondition, "aggregateSkipCondition", "{0}", aggregateSkipCondition)
			Debug.Assert(expectedResult = aggregateSkipExpression, "aggregateSkipExpression", "{0}", aggregateSkipExpression)
			Debug.Assert(expectedResult = betweenSkipLeftOperand, "betweenSkipLeftOperand", "{0}", betweenSkipLeftOperand)
			Debug.Assert(expectedResult = betweenSkipBeginExpression, "betweenSkipBeginExpression", "{0}", betweenSkipBeginExpression)
			Debug.Assert(expectedResult = betweenSkipEndExpression, "betweenSkipEndExpression", "{0}", betweenSkipEndExpression)
			Debug.Assert(expectedResult = binarySkipLeftOperand, "binarySkipLeftOperand", "{0}", binarySkipLeftOperand)
			Debug.Assert(expectedResult = binarySkipRightOperand, "binarySkipRightOperand", "{0}", binarySkipRightOperand)
			Debug.Assert(expectedResult = functionSkipArgument, "functionSkipArgument", "{0}", functionSkipArgument)
			Debug.Assert(expectedResult = inSkipLeftOperand, "inSkipLeftOperand", "{0}", inSkipLeftOperand)
			Debug.Assert("[C] In ([D], [E]) And [A] = 'B'" = inSkipOperand, "inSkipOperand", "{0}", inSkipOperand)
			Debug.Assert(expectedResult = unarySkipOperand, "unarySkipOperand", "{0}", unarySkipOperand)
			Debug.Assert(Object.ReferenceEquals(Nothing, skipSeveral), "skipSeveral", "{0}", skipSeveral)
		End Sub
	End Class
End Namespace
