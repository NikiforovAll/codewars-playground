using System.Collections.Generic;
namespace MISC
{
    using System;
    using System.Linq;
    using System.Collections;
    using CodeWars.MISC.ListPartition;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [TestFixture]
    public class ListPartitionTests
    {
        [Test]
        public void ListPartition_PivotAtTheEnd_Success()
        {
            var input = new int[] { 2, 1, 5, 4, 3 };
            ListPartition.Partition(input, 3);
            Assert.That(input, new ListPartitionTestUtils.PartitionedListConstraint(3));
        }
    }

    namespace ListPartitionTestUtils
    {
        class PartitionedListConstraint : Constraint
        {
            public PartitionedListConstraint(int pivot) : base(3)
            {
                Pivot = pivot;
                Expected = $"a,b,{pivot},c,d";
            }

            public int Pivot { get; }

            /// <summary>
            ///     The expected object to match the properties on.
            /// </summary>
            public object Expected { get; }

            /// <inheritdoc />
            /// <remarks>
            ///     Overriden so supply custom message.
            /// </remarks>
            public override string Description { get; protected set; }

            public override ConstraintResult ApplyTo<TActual>(TActual actual)
            {
                Description = Expected.ToString();
                // TODO: fix this to work with hierarchies
                if (actual is IEnumerable<Int32> transformed)
                {
                    // var orderConstraint = new CollectionOrderedConstraint();
                    // var constraintResult = orderConstraint.ApplyTo(transformed);
                    // return constraintResult;
                    var residual = transformed
                        .SkipWhile(e => e.CompareTo(this.Pivot) == -1)
                        .SkipWhile(e => e.CompareTo(this.Pivot) == 0)
                        .SkipWhile(e => e.CompareTo(this.Pivot) == 1);
                    return new EmptyConstraint().ApplyTo(residual);
                    // return new ConstraintResult(this, actual, true);
                }
                return new ConstraintResult(this, actual, false);
            }
        }
    }
}
