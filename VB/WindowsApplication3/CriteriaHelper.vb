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
        Implements IClientCriteriaVisitor

        Private fieldToRemove As String = String.Empty

        Public Sub New()
        End Sub

        Public Function RemoveCriteriaByFieldName(ByVal fieldToRemove As String, ByVal op As CriteriaOperator) As CriteriaOperator
            If String.IsNullOrEmpty(fieldToRemove) Then Return Nothing
            Me.fieldToRemove = fieldToRemove
            Return TryCast(op.Accept(Me), CriteriaOperator)
        End Function

'#Region "IClientCriteriaVisitor Members"
        Public Function Visit(ByVal theOperand As JoinOperand) As Object Implements IClientCriteriaVisitor.Visit
            Dim condition As CriteriaOperator = TryCast(theOperand.Condition.Accept(Me), CriteriaOperator)
            Dim expression As CriteriaOperator = TryCast(theOperand.AggregatedExpression.Accept(Me), CriteriaOperator)
            If ReferenceEquals(condition, Nothing) OrElse ReferenceEquals(expression, Nothing) Then Return Nothing
            Return New JoinOperand(theOperand.JoinTypeName, condition, theOperand.AggregateType, expression)
        End Function

        Public Function Visit(ByVal theOperand As OperandProperty) As Object Implements IClientCriteriaVisitor.Visit
            If Equals(theOperand.PropertyName, fieldToRemove) Then Return Nothing
            Return theOperand
        End Function

        Public Function Visit(ByVal theOperand As AggregateOperand) As Object Implements IClientCriteriaVisitor.Visit
            Dim operand As OperandProperty = TryCast(theOperand.CollectionProperty.Accept(Me), OperandProperty)
            Dim condition As CriteriaOperator = TryCast(theOperand.Condition.Accept(Me), CriteriaOperator)
            Dim expression As CriteriaOperator = TryCast(theOperand.AggregatedExpression.Accept(Me), CriteriaOperator)
            If ReferenceEquals(condition, Nothing) OrElse ReferenceEquals(expression, Nothing) OrElse ReferenceEquals(operand, Nothing) Then Return Nothing
            Return New AggregateOperand(operand, expression, theOperand.AggregateType, condition)
        End Function

'#End Region
'#Region "ICriteriaVisitor Members"
        Public Function Visit(ByVal theOperator As FunctionOperator) As Object Implements ICriteriaVisitor.Visit
            Dim operators As List(Of CriteriaOperator) = New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If ReferenceEquals(temp, Nothing) Then Return Nothing
                operators.Add(temp)
            Next

            Return New FunctionOperator(theOperator.OperatorType, operators)
        End Function

        Public Function Visit(ByVal theOperand As OperandValue) As Object Implements ICriteriaVisitor.Visit
            Return theOperand
        End Function

        Public Function Visit(ByVal theOperator As GroupOperator) As Object Implements ICriteriaVisitor.Visit
            Dim operators As List(Of CriteriaOperator) = New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If ReferenceEquals(temp, Nothing) Then Continue For
                operators.Add(temp)
            Next

            Return New GroupOperator(theOperator.OperatorType, operators)
        End Function

        Public Function Visit(ByVal theOperator As InOperator) As Object Implements ICriteriaVisitor.Visit
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

        Public Function Visit(ByVal theOperator As UnaryOperator) As Object Implements ICriteriaVisitor.Visit
            Dim operand As CriteriaOperator = TryCast(theOperator.Operand.Accept(Me), CriteriaOperator)
            If ReferenceEquals(operand, Nothing) Then Return Nothing
            Return New UnaryOperator(theOperator.OperatorType, operand)
        End Function

        Public Function Visit(ByVal theOperator As BinaryOperator) As Object Implements ICriteriaVisitor.Visit
            Dim leftOperand As CriteriaOperator = TryCast(theOperator.LeftOperand.Accept(Me), CriteriaOperator)
            Dim rightOperand As CriteriaOperator = TryCast(theOperator.RightOperand.Accept(Me), CriteriaOperator)
            If ReferenceEquals(leftOperand, Nothing) OrElse ReferenceEquals(rightOperand, Nothing) Then Return Nothing
            Return New BinaryOperator(leftOperand, rightOperand, theOperator.OperatorType)
        End Function

        Public Function Visit(ByVal theOperator As BetweenOperator) As Object Implements ICriteriaVisitor.Visit
            Dim test As CriteriaOperator = TryCast(theOperator.TestExpression.Accept(Me), CriteriaOperator)
            Dim begin As CriteriaOperator = TryCast(theOperator.BeginExpression.Accept(Me), CriteriaOperator)
            Dim [end] As CriteriaOperator = TryCast(theOperator.EndExpression.Accept(Me), CriteriaOperator)
            If ReferenceEquals(test, Nothing) OrElse ReferenceEquals(begin, Nothing) OrElse ReferenceEquals([end], Nothing) Then Return Nothing
            Return New BetweenOperator(test, begin, [end])
        End Function
'#End Region
    End Class
End Namespace
