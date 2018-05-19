Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports DevExpress.Data.Filtering.Helpers

Namespace PatchCriteriaExample.Filtering
    Public Class CriteriaPatcherSkipProperties
        Inherits ClientCriteriaLazyPatcherBase.AggragatesCommonProcessingBase

        Private ReadOnly PropertiesToremove As IList(Of String)

        Private Sub New(ByVal propertiesToRemove As String)
            Me.PropertiesToremove = New List(Of String)(propertiesToRemove.Split(","c))
        End Sub

        Public Shared Function Patch(ByVal source As CriteriaOperator, ByVal propertiesToRemove As String) As CriteriaOperator
            Return (New CriteriaPatcherSkipProperties(propertiesToRemove)).Process(source)
        End Function

        Private Shared Function IsNothing(theOperator As CriteriaOperator) As Boolean
            Return Object.ReferenceEquals(theOperator, Nothing)
        End Function

        Private Shared Function RemoveEmptyOperands(ByVal source As CriteriaOperatorCollection) As CriteriaOperatorCollection
            Dim result As New CriteriaOperatorCollection()
            For Each operand As CriteriaOperator In source
                If Not IsNothing(operand) Then
                    result.Add(operand)
                End If
            Next operand
            Return result
        End Function

        Public Overrides Function Visit(ByVal theOperand As OperandProperty) As CriteriaOperator
            If PropertiesToremove.Contains(theOperand.PropertyName) Then
                Return Nothing
            End If
            Return theOperand
        End Function

        Public Overrides Function Visit(ByVal theOperand As AggregateOperand) As CriteriaOperator
            Dim collectionProperty As CriteriaOperator = Visit(theOperand.CollectionProperty)
            If IsNothing(collectionProperty) Then
                Return Nothing
            End If
            Dim patched As CriteriaOperator = MyBase.Visit(theOperand)
            If Object.ReferenceEquals(theOperand, patched) Then
                Return theOperand
            End If
            Return Nothing
        End Function
        Public Overrides Function Visit(ByVal theOperator As BetweenOperator) As CriteriaOperator
            theOperator = CType(MyBase.Visit(theOperator), BetweenOperator)
            If IsNothing(theOperator.BeginExpression) OrElse IsNothing(theOperator.EndExpression) OrElse IsNothing(theOperator.TestExpression) Then
                Return Nothing
            End If
            Return theOperator
        End Function

        Public Overrides Function Visit(ByVal theOperator As BinaryOperator) As CriteriaOperator
            theOperator = CType(MyBase.Visit(theOperator), BinaryOperator)
            If IsNothing(theOperator.LeftOperand) OrElse IsNothing(theOperator.RightOperand) Then
                Return Nothing
            End If
            Return theOperator
        End Function

        Public Overrides Function Visit(ByVal theOperator As FunctionOperator) As CriteriaOperator
            Dim result = CType(MyBase.Visit(theOperator), FunctionOperator)
            If Not Object.ReferenceEquals(theOperator, result) Then
                Return Nothing
            End If
            Return result
        End Function

        Public Overrides Function Visit(ByVal theOperator As InOperator) As CriteriaOperator
            Dim result = CType(MyBase.Visit(theOperator), InOperator)
            If IsNothing(result.LeftOperand) Then
                Return Nothing
            End If
            If Object.ReferenceEquals(theOperator.Operands, result.Operands) Then
                Return theOperator
            End If
            Dim filteredOperands As CriteriaOperatorCollection = RemoveEmptyOperands(result.Operands)
            If filteredOperands.Count = 0 Then
                Return Nothing
            End If
            If filteredOperands.Count = 1 Then
                Return New BinaryOperator(theOperator.LeftOperand, filteredOperands(0), BinaryOperatorType.Equal)
            End If
            Return New InOperator(theOperator.LeftOperand, filteredOperands)
        End Function

        Public Overrides Function Visit(ByVal theOperator As UnaryOperator) As CriteriaOperator
            theOperator = CType(MyBase.Visit(theOperator), UnaryOperator)
            If IsNothing(theOperator.Operand) Then
                Return Nothing
            End If
            Return theOperator
        End Function
    End Class
End Namespace
