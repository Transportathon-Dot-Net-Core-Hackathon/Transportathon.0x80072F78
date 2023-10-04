using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Core.Infrastructure.FilterUtils;

public class Filter : IFilter
{
    public IQueryable<T> PrepareFilterQuery<T>(IEntityType entityType, IQueryable<T> query,
        string filter = "", string search = "", string sort = "") where T : class
    {
        if (string.IsNullOrWhiteSpace(search) == false)
        {
            var propertyNameList = GetColumnNamesByType(entityType, typeof(string));
            var predicate = SearchPredicateForBasicSearch<T>(propertyNameList, search);
            query = query.Where(predicate);
        }

        if (string.IsNullOrWhiteSpace(filter) == false)
        {
            ExpressionBuilder<T> expressionBuilder = new();
            var predicate = expressionBuilder.ExpressionGenerator(entityType, filter);
            query = query.Where(predicate);
        }

        if (string.IsNullOrWhiteSpace(sort) == false)
        {
            var parsedSortCriteria = ParseSortCriteria(entityType, sort);
            query = SearchPredicateForColumnSort(query, parsedSortCriteria);
        }

        return query;
    }

    private Expression<Func<T, bool>> SearchPredicateForBasicSearch<T>(List<string> colNames, string search)
    {
        var expressions = new List<Expression>();
        var parameterExpression = Expression.Parameter(typeof(T));
        var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

        foreach (var colName in colNames)
        {
            var property = Expression.Property(parameterExpression, colName);
            var constant = Expression.Constant(search);
            var expression = Expression.Call(property, containsMethod, constant);

            expressions.Add(expression);
        }

        var expBody = expressions.Aggregate(Expression.Or);

        return Expression.Lambda<Func<T, bool>>(expBody, parameterExpression);
    }

    private IQueryable<T> SearchPredicateForColumnSort<T>(IQueryable<T> query,
        List<(string sortColumnName, Type colType, string sortCriteria)> parsedSortCriteria)
    {
        var parameterExpression = Expression.Parameter(typeof(T));

        foreach (var sortCriteria in parsedSortCriteria)
        {
            var property = Expression.Property(parameterExpression, sortCriteria.sortColumnName);
            var constant = Expression.Constant(sortCriteria.sortCriteria);
            if (sortCriteria == parsedSortCriteria[0] && sortCriteria.sortCriteria.Equals("-1"))
            {
                var orderByDescendingLambda = Expression.Lambda(property, parameterExpression);
                var queryExpression = query.Expression;

                var OrderByDescExpression = Expression.Call(typeof(Queryable), nameof(Queryable.OrderByDescending),
                    new[]
                    {
                    query.ElementType,
                    sortCriteria.colType
                    }, queryExpression, Expression.Quote(orderByDescendingLambda));

                query = query.Provider.CreateQuery<T>(OrderByDescExpression);
            }
            else if (sortCriteria == parsedSortCriteria[0] && sortCriteria.sortCriteria.Equals("1"))
            {
                var orderByLambda = Expression.Lambda(property, parameterExpression);
                var queryExpression = query.Expression;

                var OrderByExpression = Expression.Call(typeof(Queryable), nameof(Queryable.OrderBy), new[]
                {
                query.ElementType,
                sortCriteria.colType
            }, queryExpression, Expression.Quote(orderByLambda));

                query = query.Provider.CreateQuery<T>(OrderByExpression);
            }
            else if (sortCriteria != parsedSortCriteria[0] && sortCriteria.sortCriteria.Equals("-1"))
            {
                var thenByDescLambda = Expression.Lambda(property, parameterExpression);
                var queryExpression = query.Expression;

                var thenByDescExpression = Expression.Call(typeof(Queryable), nameof(Queryable.ThenByDescending), new[]
                {
                query.ElementType,
                sortCriteria.colType
            }, queryExpression, Expression.Quote(thenByDescLambda));

                query = query.Provider.CreateQuery<T>(thenByDescExpression);
            }
            else if (sortCriteria != parsedSortCriteria[0] && sortCriteria.sortCriteria.Equals("1"))
            {
                var thenByLambda = Expression.Lambda(property, parameterExpression);
                var queryExpression = query.Expression;

                var thenByExpression = Expression.Call(typeof(Queryable), nameof(Queryable.ThenBy), new[]
                {
                query.ElementType,
                sortCriteria.colType
            }, queryExpression, Expression.Quote(thenByLambda));

                query = query.Provider.CreateQuery<T>(thenByExpression);
            }
        }

        return query;
    }

    private List<(string sortColumnName, Type colType, string sortCriteria)> ParseSortCriteria(IEntityType entityType, string sort)
    {
        var parsedSortCriteriaAndColumnNameString =
            sort.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        List<(string sortColumnName, Type colType, string sortCriteria)> columnNamesAndSortCriterias = new();

        foreach (var sortCriteriaAndColumnName in parsedSortCriteriaAndColumnNameString)
        {
            var columnNamesToBeSorted = sortCriteriaAndColumnName.Split("::")[0];
            var criteriaForSort = sortCriteriaAndColumnName.Split("::")[1];
            var columnProperty = entityType.GetProperty(columnNamesToBeSorted);
            columnNamesAndSortCriterias.Add((
                sortColumnName: columnNamesToBeSorted,
                colType: columnProperty.ClrType,
                sortCriteria: criteriaForSort));
        }

        return columnNamesAndSortCriterias;
    }

    private List<string> GetColumnNamesByType(IEntityType entityType, Type type)
    {
        List<string> columnNameList = new();
        var entityProperties = entityType.GetProperties().ToList();

        foreach (var property in entityProperties)
        {
            if (property.ClrType.FullName.Equals(type.ToString()))
                columnNameList.Add(property.Name);
        }

        return columnNameList;
    }
}