using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using System.Collections.Generic;

namespace PatchCriteriaExample.Filtering {
    public class CriteriaPatcherSkipProperties : ClientCriteriaLazyPatcherBase.AggregatesCommonProcessingBase {
        private readonly IList<string> PropertiesToremove;

        private CriteriaPatcherSkipProperties(string propertiesToRemove) {
            this.PropertiesToremove = new List<string>(propertiesToRemove.Split(','));
        }

        public static CriteriaOperator Patch(CriteriaOperator source, string propertiesToRemove) {
            return new CriteriaPatcherSkipProperties(propertiesToRemove).Process(source);
        }

        private static bool IsNull(CriteriaOperator theOperator) {
            return object.ReferenceEquals(theOperator, null);
        }

        public override CriteriaOperator Visit(OperandProperty theOperand) {
            if(PropertiesToremove.Contains(theOperand.PropertyName)) return null;
            return theOperand;
        }

        public override CriteriaOperator Visit(AggregateOperand theOperand) {
            CriteriaOperator collectionProperty = Visit(theOperand.CollectionProperty);
            if(IsNull(collectionProperty)) return null;
            CriteriaOperator patched =  base.Visit(theOperand);
            if(object.ReferenceEquals(theOperand, patched)) return theOperand;
            return null;
        }

        public override CriteriaOperator Visit(BetweenOperator theOperator) {
             theOperator = (BetweenOperator)base.Visit(theOperator);
             if(IsNull(theOperator.BeginExpression) || IsNull(theOperator.EndExpression)
                 || IsNull(theOperator.TestExpression))
                 return null;
             return theOperator;
        }

        public override CriteriaOperator Visit(BinaryOperator theOperator) {
            theOperator = (BinaryOperator)base.Visit(theOperator);
            if(IsNull(theOperator.LeftOperand) || IsNull(theOperator.RightOperand))
                return null;
            return theOperator;
        }

        public override CriteriaOperator Visit(FunctionOperator theOperator) {
            var result = (FunctionOperator)base.Visit(theOperator);
            if(!object.ReferenceEquals(theOperator, result)) return null;
            return result;
        }

        public override CriteriaOperator Visit(InOperator theOperator) {
            var result = (InOperator)base.Visit(theOperator);
            if(IsNull(result.LeftOperand)) return null;
            if(object.ReferenceEquals(theOperator.Operands, result.Operands)) return theOperator;
            CriteriaOperatorCollection filteredOperands = RemoveEmptyOperands(result.Operands);
            if(filteredOperands.Count == 0) return null;
            if(filteredOperands.Count == 1)
                return new BinaryOperator(theOperator.LeftOperand, filteredOperands[0], BinaryOperatorType.Equal);
            return new InOperator(theOperator.LeftOperand, filteredOperands);
        }

        public override CriteriaOperator Visit(UnaryOperator theOperator) {
            theOperator = (UnaryOperator)base.Visit(theOperator);
            if(IsNull(theOperator.Operand)) return null;
            return theOperator;
        }

        private static CriteriaOperatorCollection RemoveEmptyOperands(CriteriaOperatorCollection source) {
            CriteriaOperatorCollection result = new CriteriaOperatorCollection();
            foreach(CriteriaOperator operand in source)
                if(!IsNull(operand)) result.Add(operand);
            return result;
        }
    }
}
