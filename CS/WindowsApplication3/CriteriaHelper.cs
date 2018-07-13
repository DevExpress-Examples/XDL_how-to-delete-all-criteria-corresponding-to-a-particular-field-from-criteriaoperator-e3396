using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Data.Filtering;

namespace DXSample {
    public static class CriteriaHelper {
        static RemoveCriteriaVisitor visitor;
        static RemoveCriteriaVisitor Visitor {
            get {
                if(visitor == null)
                    visitor = new RemoveCriteriaVisitor();
                return visitor;
            }
        }

        public static CriteriaOperator RemoveCriteriaByFieldName(string fieldToRemove, CriteriaOperator op) {
            return Visitor.RemoveCriteriaByFieldName(fieldToRemove, op);
        }
    }

    public class RemoveCriteriaVisitor : IClientCriteriaVisitor<CriteriaOperator> {
        string fieldToRemove = string.Empty;
        public RemoveCriteriaVisitor() { }

        public CriteriaOperator RemoveCriteriaByFieldName(string fieldToRemove, CriteriaOperator op) {
            if(string.IsNullOrEmpty(fieldToRemove)) return null;
            this.fieldToRemove = fieldToRemove;
            return op.Accept(this) as CriteriaOperator;
        }

        #region IClientCriteriaVisitor Members

        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(JoinOperand theOperand) {
            CriteriaOperator condition = theOperand.Condition.Accept(this) as CriteriaOperator;
            CriteriaOperator expression = theOperand.AggregatedExpression.Accept(this) as CriteriaOperator;
            if(object.ReferenceEquals(condition, null) || object.ReferenceEquals(expression, null)) return null;
            return new JoinOperand(theOperand.JoinTypeName, condition, theOperand.AggregateType, expression);
        }

        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(OperandProperty theOperand) {
            if(theOperand.PropertyName == fieldToRemove) return null;
            return theOperand;
        }

         CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(AggregateOperand theOperand) {
            OperandProperty operand = theOperand.CollectionProperty.Accept(this) as OperandProperty;
            CriteriaOperator condition = theOperand.Condition.Accept(this) as CriteriaOperator;
            CriteriaOperator expression = theOperand.AggregatedExpression.Accept(this) as CriteriaOperator;
            if(object.ReferenceEquals(condition, null) || object.ReferenceEquals(expression, null) || object.ReferenceEquals(operand, null)) return null;
            return new AggregateOperand(operand, expression, theOperand.AggregateType, condition);
        }
        #endregion

        #region ICriteriaVisitor Members

        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(FunctionOperator theOperator) {
            List<CriteriaOperator> operators = new List<CriteriaOperator>();
            foreach(CriteriaOperator op in theOperator.Operands) {
                CriteriaOperator temp = op.Accept(this) as CriteriaOperator;
                if(object.ReferenceEquals(temp, null)) return null;
                operators.Add(temp);
            }
            return new FunctionOperator(theOperator.OperatorType, operators);
        }

        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(OperandValue theOperand) {
            return theOperand;
        }

        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(GroupOperator theOperator) {
            List<CriteriaOperator> operators = new List<CriteriaOperator>();
            foreach(CriteriaOperator op in theOperator.Operands) {
                CriteriaOperator temp = op.Accept(this) as CriteriaOperator;
                if(object.ReferenceEquals(temp, null)) continue;
                operators.Add(temp);
            }
            return new GroupOperator(theOperator.OperatorType, operators);
        }

        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(InOperator theOperator) {
            CriteriaOperator leftOperand = theOperator.LeftOperand.Accept(this) as CriteriaOperator;
            List<CriteriaOperator> operators = new List<CriteriaOperator>();
            foreach(CriteriaOperator op in theOperator.Operands) {
                CriteriaOperator temp = op.Accept(this) as CriteriaOperator;
                if(object.ReferenceEquals(temp, null)) continue;
                operators.Add(temp);
            }
            if(object.ReferenceEquals(leftOperand, null)) return null;
            return new InOperator(leftOperand, operators);
        }

        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(UnaryOperator theOperator) {
            CriteriaOperator operand = theOperator.Operand.Accept(this) as CriteriaOperator;
            if(object.ReferenceEquals(operand, null)) return null;
            return new UnaryOperator(theOperator.OperatorType, operand);
        }

        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(BinaryOperator theOperator) {
            CriteriaOperator leftOperand = theOperator.LeftOperand.Accept(this) as CriteriaOperator;
            CriteriaOperator rightOperand = theOperator.RightOperand.Accept(this) as CriteriaOperator;
            if(object.ReferenceEquals(leftOperand, null) || object.ReferenceEquals(rightOperand, null)) return null;
            return new BinaryOperator(leftOperand, rightOperand, theOperator.OperatorType);
        }

        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(BetweenOperator theOperator) {
            CriteriaOperator test = theOperator.TestExpression.Accept(this) as CriteriaOperator;
            CriteriaOperator begin = theOperator.BeginExpression.Accept(this) as CriteriaOperator;
            CriteriaOperator end = theOperator.EndExpression.Accept(this) as CriteriaOperator;
            if(object.ReferenceEquals(test, null) || object.ReferenceEquals(begin, null) || object.ReferenceEquals(end, null)) return null;
            return new BetweenOperator(test, begin, end);
        }

       

        #endregion
    }
}