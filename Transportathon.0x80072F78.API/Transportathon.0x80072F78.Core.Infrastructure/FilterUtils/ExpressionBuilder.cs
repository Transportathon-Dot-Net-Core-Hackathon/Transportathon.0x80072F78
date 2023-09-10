using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Transportathon._0x80072F78.Core.Infrastructure.FilterUtils;

public class ExpressionBuilder<T>
{
    /// <summary>
    /// Expression Parameter
    /// Expression.Parameter(typeof(T), "x")
    /// Use only this property(don't create new one) while expression building operations. 
    /// </summary>
    private ParameterExpression _exprParameter;

    public ExpressionBuilder()
    {
        //x => x.KartTipi ....
        _exprParameter = Expression.Parameter(typeof(T), "x");
    }

    public Expression<Func<T, bool>> ExpressionGenerator(IEntityType entityType, string filter)
    {
        Parser parser = new();
        ExpressionBuilder<T> expressionBuilder = new();
        FilterNode parsedFilter = parser.ParseFilter(entityType, filter);
        var predicate = expressionBuilder.ExpressionGenerator(parsedFilter);
        return predicate;
    }

    /// <summary>
    /// FilterNode kullanarak expression üretir. 
    /// Filter yapısı ve formatı ile ilgili detaylı bilgi Parser altındaki FilterNode class açıklamalarında bulunmaktadır. 
    /// 
    /// Örnek Filter:
    /// KartTipi::==::1::##||KartTipi::==::2::@@&&IslemTipi::==::1::##||IslemTipi::==::2@@&&KrediLimiti::>::600
    /// ------  L2 ------    ------  L2 ------    ------  L2 -------    -----  L2 ------    ------  L2 --------
    /// ----------------  L1 -----------------    ----------------  L1 -----------------    ------  L1 --------
    /// 
    /// KartTipi::==::1::##||KartTipi::==::2::@@&&IslemTipi::==::1::##||IslemTipi::==::2@@&&KrediLimiti::>::600
    /// WHERE c."KartTipi" IN (1, 2) AND c."IslemTipi" IN (1, 2) AND c."KrediLimiti"::numeric > 600.0
    /// 
    /// Ad::!@::M002::@@&&KartTipi::==::1::##||IslemTipi::==::1
    /// WHERE (strpos(c."Ad", 'M002') > 0) = FALSE AND (c."KartTipi" = 1 OR c."IslemTipi" = 1)
    /// 
    /// </summary>
    /// <param name="rootNode"></param>
    /// <returns></returns>
    private Expression<Func<T, bool>> ExpressionGenerator(FilterNode rootNode)
    {
        Expression expAggregatedLevel1 = null;
        List<FilterNode> childrenLevel1 = rootNode.Children.Count > 0 ? rootNode.Children : new List<FilterNode>();

        //L1 expression'lar, L2 expression'ları birleştirmek için kullanılır
        foreach (FilterNode childNodeL1 in childrenLevel1)
        {
            //L2 expression'lar, asıl sorguların tanımlı olduğu expression'lardır
            Expression expAggregatedLevel2 = null;

            foreach (FilterNode childNodeL2 in childNodeL1.Children)
            {
                Expression childExpression = CreateExpressionFromFilter(childNodeL2);

                if (expAggregatedLevel2 == null)
                    expAggregatedLevel2 = childExpression;

                switch (childNodeL2.Operand)
                {
                    case Parser.OPERATOR_AND:
                        expAggregatedLevel2 = Expression.AndAlso(expAggregatedLevel2, childExpression); break;
                    case Parser.OPERATOR_OR:
                        expAggregatedLevel2 = Expression.OrElse(expAggregatedLevel2, childExpression); break;
                    default:
                        break;
                }
            }

            if (expAggregatedLevel1 == null)
                expAggregatedLevel1 = expAggregatedLevel2;

            switch (childNodeL1.Operand)
            {
                case Parser.OPERATOR_AND:
                    expAggregatedLevel1 = Expression.AndAlso(expAggregatedLevel1, expAggregatedLevel2); break;
                case Parser.OPERATOR_OR:
                    expAggregatedLevel1 = Expression.OrElse(expAggregatedLevel1, expAggregatedLevel2); break;
                default:
                    break;
            }
        }

        var result = Expression.Lambda<Func<T, bool>>(expAggregatedLevel1, _exprParameter);
        return result;
    }

    private Expression CreateExpressionFromFilter(FilterNode filterNode)
    {
        BinaryExpression binaryExpression = null; //Eger binary olmuyorsa => Expression binaryExpression = null;
        MemberExpression prop = Expression.PropertyOrField(_exprParameter, filterNode.Column.Name);
        UnaryExpression propConverted = Expression.Convert(prop, filterNode.ValueType);
        ConstantExpression value = Expression.Constant(filterNode.Value);
        //UnaryExpression valConverted = Expression.Convert(value, typeof(object));

        switch (filterNode.Criteria)
        {
            case Parser.OPERATOR_EQUAL:
                binaryExpression = Expression.Equal(propConverted, value); break;
            case Parser.OPERATOR_NOT_EQUAL:
                binaryExpression = Expression.NotEqual(propConverted, value); break;
            case Parser.OPERATOR_GREATER_THAN:
                binaryExpression = Expression.GreaterThan(propConverted, value); break;
            case Parser.OPERATOR_GREATER_THAN_OR_EQUAL:
                binaryExpression = Expression.GreaterThanOrEqual(propConverted, value); break;
            case Parser.OPERATOR_LESS_THAN:
                binaryExpression = Expression.LessThan(propConverted, value); break;
            case Parser.OPERATOR_LESS_THAN_OR_EQUAL:
                binaryExpression = Expression.LessThanOrEqual(propConverted, value); break;
            case Parser.OPERATOR_CONTAINS:
                //var constainsExp = CreateContainsExpression(prop, value);
                //binaryExpression = constainsExp; 
                binaryExpression = CreateBinaryContainsExpression(prop, value); break;
            case Parser.OPERATOR_NOT_CONTAINS:
                binaryExpression = CreateBinaryNotContainsExpression(prop, value);
                break;
            default:
                binaryExpression = Expression.Equal(propConverted, value); break;
        }

        Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(binaryExpression, _exprParameter);
        return lambda.Body;
    }

    private Expression CreateContainsExpression(MemberExpression prop, ConstantExpression val)
    {
        var methodCallExp = Expression.Call(prop, nameof(string.Contains), Type.EmptyTypes, val);
        return Expression.Lambda<Func<T, bool>>(methodCallExp, _exprParameter).Body;
    }

    private BinaryExpression CreateBinaryContainsExpression(MemberExpression prop, ConstantExpression val)
    {
        BinaryExpression exp = Expression.Equal(CreateContainsExpression(prop, val), Expression.Constant(true));
        return exp;
    }

    private BinaryExpression CreateBinaryNotContainsExpression(MemberExpression prop, ConstantExpression val)
    {
        BinaryExpression exp = Expression.Equal(CreateContainsExpression(prop, val), Expression.Constant(false));
        return exp;
    }
}