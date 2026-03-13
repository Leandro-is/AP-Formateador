using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class ExtendLinq
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool ascending = true)
        {
            return ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer, bool ascending = true)
        {
            return ascending ? source.OrderBy(keySelector, comparer) : source.OrderByDescending(keySelector, comparer);
        }
        public static IEnumerable<MemberExpression> GetPropertyAccesses<T>(this Expression<Func<T, object>> expression)
        {
            var visitor = new MemberAccesses(expression.Parameters[0]);
            visitor.Visit(expression);
            return visitor.Members;
        }
        internal class MemberAccesses : ExpressionVisitor
        {
            private ParameterExpression parameter;
            public HashSet<MemberExpression> Members { get; private set; }
            public MemberAccesses(ParameterExpression parameter)
            {
                this.parameter = parameter;
                Members = new HashSet<MemberExpression>();
            }
            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Expression == parameter)
                {
                    Members.Add(node);
                }
                return base.VisitMember(node);
            }
        }
    }
}
