using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree
{
   static class Program
    {
        static void Main(string[] args)
        {

            Add();
            MethodExpression();
            ParamerterExpression();
            LoopExpression();

            List<Member> members = new List<Member>(){
            new Member{Name="jack",Age=12},
             new Member{Name="jack",Age=15}
            };

            Expression<Func<Member, bool>> exp1 = p => p.Name =="jack";
            Expression<Func<Member, bool>> exp2 = p => p.Age == 12;
            exp1 = exp1.AndAlso<Member>(exp2);
            members= members.Where(exp1.Compile()).ToList();
            Console.ReadLine();
        }

        private static void Add() 
        {
            Expression<Func<int, int, int>> exp1 = (x, y) => x + y;
            int result= exp1.Compile().Invoke(2,3);

            Console.WriteLine(result); 
        }

        private static void MethodExpression() 
        {

            MethodCallExpression exp2=Expression.Call(
                null,
                typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
                Expression.Constant("Hello"));
            BlockExpression block = Expression.Block(exp2);
            Expression<Action> lambda = Expression.Lambda<Action>(block);
            lambda.Compile()();
        }

        private static void ParamerterExpression() 
        {
            ParameterExpression number = Expression.Parameter(typeof(int), "number");

            BlockExpression block = Expression.Block(
                new[]{number},
                Expression.Assign(number,Expression.Constant(6)),
                Expression.Divide(number,Expression.Constant(2)));
            Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(block);

            Console.WriteLine(lambda.Compile().Invoke());
            
        }

        private static void LoopExpression() 
        {
            LabelTarget labelBreak = Expression.Label();
            ParameterExpression loopIndex = Expression.Parameter(typeof(int), "index");

            BlockExpression block = Expression.Block(
                new[] { loopIndex },
                Expression.Assign(loopIndex, Expression.Constant(1)),
                Expression.Loop(
                Expression.IfThenElse(
                  Expression.LessThanOrEqual(loopIndex, Expression.Constant(10)),
                  Expression.Block(
                   Expression.Call(
                   null,
                   typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
                    Expression.Constant("Hello" +loopIndex)),
                   Expression.PostIncrementAssign(loopIndex)),
                  Expression.Break(labelBreak)
                ), labelBreak));

            Expression<Action> lambda = Expression.Lambda<Action>(block);
            Console.WriteLine(lambda.Body);
            lambda.Compile()();
        }

        #region 网摘 lambda拼接
        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // zip parameters (map from parameters of second to parameters of first)
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with the parameters in the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // create a merged lambda expression with parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// ParameterRebinder
        /// </summary>
        class ParameterRebinder : ExpressionVisitor
        {
            /// <summary>
            /// The ParameterExpression map
            /// </summary>
            readonly Dictionary<ParameterExpression, ParameterExpression> map;

            /// <summary>
            /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
            /// </summary>
            /// <param name="map">The map.</param>
            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            /// <summary>
            /// Replaces the parameters.
            /// </summary>
            /// <param name="map">The map.</param>
            /// <param name="exp">The exp.</param>
            /// <returns>Expression</returns>
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            /// <summary>
            /// Visits the parameter.
            /// </summary>
            /// <param name="p">The p.</param>
            /// <returns>Expression</returns>
            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        } 
        #endregion
   
        public class Member 
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
