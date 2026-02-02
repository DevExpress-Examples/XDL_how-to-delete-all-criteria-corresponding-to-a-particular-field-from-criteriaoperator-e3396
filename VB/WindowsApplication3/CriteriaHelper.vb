Imports System.Collections.Generic
Imports DevExpress.Data.Filtering

Namespace DXSample

    Public Module CriteriaHelper

        Private visitorField As RemoveCriteriaVisitor

        Private ReadOnly Property Visitor As RemoveCriteriaVisitor
            Get
                If visitorField Is Nothing Then visitorField = New RemoveCriteriaVisitor()
                Return visitorField
            End Get
        End Property

        Public Function RemoveCriteriaByFieldName(ByVal fieldToRemove As String, ByVal op As CriteriaOperator) As CriteriaOperator
            Return Visitor.RemoveCriteriaByFieldName(fieldToRemove, op)
        End Function
    End Module

    Public Class RemoveCriteriaVisitor
        Implements IClientCriteriaVisitor(Of CriteriaOperator)

        Private fieldToRemove As String = String.Empty

        Public Sub New()
        End Sub

        Public Function RemoveCriteriaByFieldName(ByVal fieldToRemove As String, ByVal op As CriteriaOperator) As CriteriaOperator
            If String.IsNullOrEmpty(fieldToRemove) Then Return Nothing
            Me.fieldToRemove = fieldToRemove
            Return TryCast(op.Accept(Me), CriteriaOperator)
        End Function

'#Region "IClientCriteriaVisitor Members"
        Private Function IClientCriteriaVisitor_Visit(ByVal theOperand As JoinOperand) As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
            Dim condition As CriteriaOperator = TryCast(theOperand.Condition.Accept(Me), CriteriaOperator)
            Dim expression As CriteriaOperator = TryCast(theOperand.AggregatedExpression.Accept(Me), CriteriaOperator)
            If ReferenceEquals(condition, Nothing) OrElse ReferenceEquals(expression, Nothing) Then Return Nothing
            Return New JoinOperand(theOperand.JoinTypeName, condition, theOperand.AggregateType, expression)
        End Function

        Private Function IClientCriteriaVisitor_Visit1(ByVal theOperand As OperandProperty) As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
            If Equals(theOperand.PropertyName, fieldToRemove) Then Return Nothing
            Return theOperand
        End Function

        Private Function IClientCriteriaVisitor_Visit2(ByVal theOperand As AggregateOperand) As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
            Dim operand As OperandProperty = TryCast(theOperand.CollectionProperty.Accept(Me), OperandProperty)
            Dim condition As CriteriaOperator = TryCast(theOperand.Condition.Accept(Me), CriteriaOperator)
            Dim expression As CriteriaOperator = TryCast(theOperand.AggregatedExpression.Accept(Me), CriteriaOperator)
            If ReferenceEquals(condition, Nothing) OrElse ReferenceEquals(expression, Nothing) OrElse ReferenceEquals(operand, Nothing) Then Return Nothing
            Return New AggregateOperand(operand, expression, theOperand.AggregateType, condition)
        End Function

'#End Region
'#Region "ICriteriaVisitor Members"
        Private Function ICriteriaVisitor_Visit(ByVal theOperator As FunctionOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim operators As List(Of CriteriaOperator) = New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If ReferenceEquals(temp, Nothing) Then Return Nothing
                operators.Add(temp)
            Next

            Return New FunctionOperator(theOperator.OperatorType, operators)
        End Function

        Private Function ICriteriaVisitor_Visit1(ByVal theOperand As OperandValue) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Return theOperand
        End Function

        Private Function ICriteriaVisitor_Visit2(ByVal theOperator As GroupOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim operators As List(Of CriteriaOperator) = New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If ReferenceEquals(temp, Nothing) Then Continue For
                operators.Add(temp)
            Next

            Return New GroupOperator(theOperator.OperatorType, operators)
        End Function

        Private Function ICriteriaVisitor_Visit3(ByVal theOperator As InOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim leftOperand As CriteriaOperator = TryCast(theOperator.LeftOperand.Accept(Me), CriteriaOperator)
            Dim operators As List(Of CriteriaOperator) = New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If ReferenceEquals(temp, Nothing) Then Continue For
                operators.Add(temp)
            Next

            If ReferenceEquals(leftOperand, Nothing) Then Return Nothing
            Return New InOperator(leftOperand, operators)
        End Function

        Private Function ICriteriaVisitor_Visit4(ByVal theOperator As UnaryOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim operand As CriteriaOperator = TryCast(theOperator.Operand.Accept(Me), CriteriaOperator)
            If ReferenceEquals(operand, Nothing) Then Return Nothing
            Return New UnaryOperator(theOperator.OperatorType, operand)
        End Function

        Private Function ICriteriaVisitor_Visit5(ByVal theOperator As BinaryOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim leftOperand As CriteriaOperator = TryCast(theOperator.LeftOperand.Accept(Me), CriteriaOperator)
            Dim rightOperand As CriteriaOperator = TryCast(theOperator.RightOperand.Accept(Me), CriteriaOperator)
            If ReferenceEquals(leftOperand, Nothing) OrElse ReferenceEquals(rightOperand, Nothing) Then Return Nothing
            Return New BinaryOperator(leftOperand, rightOperand, theOperator.OperatorType)
        End Function

        Private Function ICriteriaVisitor_Visit6(ByVal theOperator As BetweenOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim test As CriteriaOperator = TryCast(theOperator.TestExpression.Accept(Me), CriteriaOperator)
            Dim begin As CriteriaOperator = TryCast(theOperator.BeginExpression.Accept(Me), CriteriaOperator)
            Dim [end] As CriteriaOperator = TryCast(theOperator.EndExpression.Accept(Me), CriteriaOperator)
            If ReferenceEquals(test, Nothing) OrElse ReferenceEquals(begin, Nothing) OrElse ReferenceEquals([end], Nothing) Then Return Nothing
            Return New BetweenOperator(test, begin, [end])
        End Function
'#End Region
    End Class
End Namespace
