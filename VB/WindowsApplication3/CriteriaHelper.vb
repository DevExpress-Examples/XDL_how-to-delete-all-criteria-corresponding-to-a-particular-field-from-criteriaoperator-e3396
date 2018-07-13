Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.Data.Filtering

Namespace DXSample
	Public NotInheritable Class CriteriaHelper
		Private Shared visitor_Renamed As RemoveCriteriaVisitor
		Private Sub New()
		End Sub
		Private Shared ReadOnly Property Visitor() As RemoveCriteriaVisitor
			Get
				If visitor_Renamed Is Nothing Then
					visitor_Renamed = New RemoveCriteriaVisitor()
				End If
				Return visitor_Renamed
			End Get
		End Property

		Public Shared Function RemoveCriteriaByFieldName(ByVal fieldToRemove As String, ByVal op As CriteriaOperator) As CriteriaOperator
			Return Visitor.RemoveCriteriaByFieldName(fieldToRemove, op)
		End Function
	End Class

	Public Class RemoveCriteriaVisitor
        Implements IClientCriteriaVisitor(Of CriteriaOperator)
        Private fieldToRemove As String = String.Empty
        Public Sub New()
        End Sub

        Public Function RemoveCriteriaByFieldName(ByVal fieldToRemove As String, ByVal op As CriteriaOperator) As CriteriaOperator
            If String.IsNullOrEmpty(fieldToRemove) Then
                Return Nothing
            End If
            Me.fieldToRemove = fieldToRemove
            Return TryCast(op.Accept(Me), CriteriaOperator)
        End Function

#Region "IClientCriteriaVisitor Members"

        Public Function Visit(ByVal theOperand As JoinOperand) As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
            Dim condition As CriteriaOperator = TryCast(theOperand.Condition.Accept(Me), CriteriaOperator)
            Dim expression As CriteriaOperator = TryCast(theOperand.AggregatedExpression.Accept(Me), CriteriaOperator)
            If Object.ReferenceEquals(condition, Nothing) OrElse Object.ReferenceEquals(expression, Nothing) Then
                Return Nothing
            End If
            Return New JoinOperand(theOperand.JoinTypeName, condition, theOperand.AggregateType, expression)
        End Function

        Public Function Visit(ByVal theOperand As OperandProperty)  As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
            If theOperand.PropertyName = fieldToRemove Then
                Return Nothing
            End If
            Return theOperand
        End Function

        Public Function Visit(ByVal theOperand As AggregateOperand)  As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
            Dim operand As OperandProperty = TryCast(theOperand.CollectionProperty.Accept(Me), OperandProperty)
            Dim condition As CriteriaOperator = TryCast(theOperand.Condition.Accept(Me), CriteriaOperator)
            Dim expression As CriteriaOperator = TryCast(theOperand.AggregatedExpression.Accept(Me), CriteriaOperator)
            If Object.ReferenceEquals(condition, Nothing) OrElse Object.ReferenceEquals(expression, Nothing) OrElse Object.ReferenceEquals(operand, Nothing) Then
                Return Nothing
            End If
            Return New AggregateOperand(operand, expression, theOperand.AggregateType, condition)
        End Function
#End Region

#Region "ICriteriaVisitor Members"

        Public Function Visit(ByVal theOperator As FunctionOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim operators As New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If Object.ReferenceEquals(temp, Nothing) Then
                    Return Nothing
                End If
                operators.Add(temp)
            Next op
            Return New FunctionOperator(theOperator.OperatorType, operators)
        End Function

        Public Function Visit(ByVal theOperand As OperandValue) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Return theOperand
        End Function

        Public Function Visit(ByVal theOperator As GroupOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim operators As New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If Object.ReferenceEquals(temp, Nothing) Then
                    Continue For
                End If
                operators.Add(temp)
            Next op
            Return New GroupOperator(theOperator.OperatorType, operators)
        End Function

        Public Function Visit(ByVal theOperator As InOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim leftOperand As CriteriaOperator = TryCast(theOperator.LeftOperand.Accept(Me), CriteriaOperator)
            Dim operators As New List(Of CriteriaOperator)()
            For Each op As CriteriaOperator In theOperator.Operands
                Dim temp As CriteriaOperator = TryCast(op.Accept(Me), CriteriaOperator)
                If Object.ReferenceEquals(temp, Nothing) Then
                    Continue For
                End If
                operators.Add(temp)
            Next op
            If Object.ReferenceEquals(leftOperand, Nothing) Then
                Return Nothing
            End If
            Return New InOperator(leftOperand, operators)
        End Function

        Public Function Visit(ByVal theOperator As UnaryOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim operand As CriteriaOperator = TryCast(theOperator.Operand.Accept(Me), CriteriaOperator)
            If Object.ReferenceEquals(operand, Nothing) Then
                Return Nothing
            End If
            Return New UnaryOperator(theOperator.OperatorType, operand)
        End Function

        Public Function Visit(ByVal theOperator As BinaryOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim leftOperand As CriteriaOperator = TryCast(theOperator.LeftOperand.Accept(Me), CriteriaOperator)
            Dim rightOperand As CriteriaOperator = TryCast(theOperator.RightOperand.Accept(Me), CriteriaOperator)
            If Object.ReferenceEquals(leftOperand, Nothing) OrElse Object.ReferenceEquals(rightOperand, Nothing) Then
                Return Nothing
            End If
            Return New BinaryOperator(leftOperand, rightOperand, theOperator.OperatorType)
        End Function

        Public Function Visit(ByVal theOperator As BetweenOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
            Dim test As CriteriaOperator = TryCast(theOperator.TestExpression.Accept(Me), CriteriaOperator)
            Dim begin As CriteriaOperator = TryCast(theOperator.BeginExpression.Accept(Me), CriteriaOperator)
            Dim [end] As CriteriaOperator = TryCast(theOperator.EndExpression.Accept(Me), CriteriaOperator)
            If Object.ReferenceEquals(test, Nothing) OrElse Object.ReferenceEquals(begin, Nothing) OrElse Object.ReferenceEquals([end], Nothing) Then
                Return Nothing
            End If
            Return New BetweenOperator(test, begin, [end])
        End Function
#End Region
    End Class
End Namespace