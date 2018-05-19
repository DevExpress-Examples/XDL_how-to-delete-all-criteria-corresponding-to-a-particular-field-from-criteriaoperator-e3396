using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using PatchCriteriaExample.Filtering;
using System.Diagnostics;

namespace PatchCriteriaExample {
    static class Program {
        static void Main() {
            string expectedResult = "[A] = 'B'";
            string aggregateSkipCollection = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("ToRemove[C = 'D'] and A = 'B'"), "ToRemove")
                .ToString();
            string aggregateSkipCondition = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("C[ToRemove = 'D'] and A = 'B'"), "ToRemove")
                .ToString();
            string aggregateSkipExpression = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("C[D ='A'].Sum(ToRemove) = 10 and A = 'B'"), "ToRemove")
                .ToString();
            string betweenSkipLeftOperand = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("ToRemove between (1, 3) and A ='B'"), "ToRemove")
                .ToString();
            string betweenSkipBeginExpression = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("C between (ToRemove, D) and A = 'B'"), "ToRemove")
                .ToString();
            string betweenSkipEndExpression = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("C between (D, ToRemove) and A = 'B'"), "ToRemove")
                .ToString();
            string binarySkipLeftOperand = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("ToRemove > 1 and A = 'B'"), "ToRemove")
                .ToString();
            string binarySkipRightOperand = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("C < ToRemove and A = 'B'"), "ToRemove")
                .ToString();
            string functionSkipArgument = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("AddDays(ToRemove, 1) >= C and A = 'B'"), "ToRemove")
                .ToString();
            string inSkipLeftOperand = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("ToRemove in (1, 2, 3) and A = 'B'"), "ToRemove")
                .ToString();
            string inSkipOperand = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("C in (ToRemove, D, E) and A = 'B'"), "ToRemove")
                .ToString();
            string unarySkipOperand = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("ToRemove is null and A = 'B'"), "ToRemove")
                .ToString();
            CriteriaOperator skipSeveral = CriteriaPatcherSkipProperties.Patch(
                CriteriaOperator.Parse("C is null and A = 'B'"), "A,C");
            Debug.Assert(expectedResult == aggregateSkipCollection, "aggregateSkipCollection", "{0}", aggregateSkipCollection);
            Debug.Assert(expectedResult == aggregateSkipCondition, "aggregateSkipCondition", "{0}", aggregateSkipCondition);
            Debug.Assert(expectedResult == aggregateSkipExpression, "aggregateSkipExpression", "{0}", aggregateSkipExpression);
            Debug.Assert(expectedResult == betweenSkipLeftOperand, "betweenSkipLeftOperand", "{0}", betweenSkipLeftOperand);
            Debug.Assert(expectedResult == betweenSkipBeginExpression, "betweenSkipBeginExpression", "{0}", betweenSkipBeginExpression);
            Debug.Assert(expectedResult == betweenSkipEndExpression, "betweenSkipEndExpression", "{0}", betweenSkipEndExpression);
            Debug.Assert(expectedResult == binarySkipLeftOperand, "binarySkipLeftOperand", "{0}", binarySkipLeftOperand);
            Debug.Assert(expectedResult == binarySkipRightOperand, "binarySkipRightOperand", "{0}", binarySkipRightOperand);
            Debug.Assert(expectedResult == functionSkipArgument, "functionSkipArgument", "{0}", functionSkipArgument);
            Debug.Assert(expectedResult == inSkipLeftOperand, "inSkipLeftOperand", "{0}", inSkipLeftOperand);
            Debug.Assert("[C] In ([D], [E]) And [A] = 'B'" == inSkipOperand, "inSkipOperand", "{0}", inSkipOperand);
            Debug.Assert(expectedResult == unarySkipOperand, "unarySkipOperand", "{0}", unarySkipOperand);
            Debug.Assert(object.ReferenceEquals(null, skipSeveral), "skipSeveral", "{0}", skipSeveral);
        }
    }
}
