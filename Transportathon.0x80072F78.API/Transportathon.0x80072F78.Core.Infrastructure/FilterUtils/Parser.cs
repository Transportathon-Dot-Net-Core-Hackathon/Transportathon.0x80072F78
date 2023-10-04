using Microsoft.EntityFrameworkCore.Metadata;
using System.Globalization;

namespace Transportathon._0x80072F78.Core.Infrastructure.FilterUtils;

public class FilterNode
{
    public FilterNode Parent { get; set; }
    public List<FilterNode> Children { get; set; }
    public string Criteria { get; set; }
    public object Value { get; set; }
    public Type ValueType { get; set; }
    public string Operand { get; set; }
    public IProperty Column { get; set; }

    public FilterNode()
    {
        Parent = null;
        Children = new List<FilterNode>();
    }

    public FilterNode(FilterNode parent)
    {
        Parent = parent;
        Children = new List<FilterNode>();
    }
}

public class Parser
{
    public const string SEPERATOR_L2_EXP = "##";
    public const string SEPERATOR_L1_EXP = "@@";
    public const string SEPERATOR_L2_WORDS = "::";
    public const string OPERATOR_OR = "||";
    public const string OPERATOR_AND = "&&";
    public const string OPERATOR_EQUAL = "==";
    public const string OPERATOR_NOT_EQUAL = "!=";
    public const string OPERATOR_GREATER_THAN = ">";
    public const string OPERATOR_GREATER_THAN_OR_EQUAL = ">=";
    public const string OPERATOR_LESS_THAN = "<";
    public const string OPERATOR_LESS_THAN_OR_EQUAL = "<=";
    public const string OPERATOR_CONTAINS = "=@";
    public const string OPERATOR_NOT_CONTAINS = "!@";


    public FilterNode ParseFilter(IEntityType entityType, string filter)
    {
        FilterNode parsedFilter = new();
        if (filter.Contains(SEPERATOR_L1_EXP) == false)
            filter += SEPERATOR_L1_EXP;

        var filterL1Words = filter.Split(new[] { SEPERATOR_L1_EXP }, StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (var filterL1Word in filterL1Words)
        {
            FilterNode childL1 = new FilterNode(parsedFilter);
            parsedFilter.Children.Add(childL1);

            string operand = "";
            if (filterL1Word.StartsWith(OPERATOR_AND) || filterL1Word.StartsWith(OPERATOR_OR))
            {
                operand = filterL1Word.Substring(0, OPERATOR_AND.Length);
                childL1.Operand = operand;
            }

            string filterL1WordEdited = filterL1Word;

            if (filterL1WordEdited.Contains(SEPERATOR_L2_EXP) == false)
                filterL1WordEdited += SEPERATOR_L2_EXP;

            var filterL2Words = filterL1WordEdited.Remove(0, operand.Length > 0 ? operand.Length : 0).Split(new[] { SEPERATOR_L2_EXP }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var filterL2Word in filterL2Words)
            {
                FilterNode childL2 = new FilterNode(childL1);
                childL1.Children.Add(childL2);

                operand = "";
                if (filterL2Word.StartsWith(OPERATOR_AND) || filterL2Word.StartsWith(OPERATOR_OR))
                {
                    operand = filterL2Word.Substring(0, OPERATOR_AND.Length);
                    childL2.Operand = operand;
                }

                string preparedFilterWord = filterL2Word.Remove(0, operand.Length > 0 ? operand.Length : 0);
                var columnName = preparedFilterWord.Split(SEPERATOR_L2_WORDS)[0];
                childL2.Column = entityType.GetProperty(columnName);
                childL2.Criteria = preparedFilterWord.Split(SEPERATOR_L2_WORDS)[1];
                (childL2.ValueType, childL2.Value) = CalculateValAndType(childL2.Column, preparedFilterWord.Split(SEPERATOR_L2_WORDS)[2]);
            }
        }

        return parsedFilter;
    }

    private (Type type, object val) CalculateValAndType(IProperty col, string val)
    {
        (Type type, object val) res = (typeof(string), val);

        if (col.ClrType == typeof(string))
        {
            return res;
        }

        if (col.ClrType.BaseType == typeof(Enum) || Nullable.GetUnderlyingType(col.ClrType)?.IsEnum == true)
        {
            if (col.IsNullable)
            {
                res.type = Nullable.GetUnderlyingType(col.ClrType);
                res.type = Enum.GetUnderlyingType(res.type);
            }
            else
            {
                res.type = Enum.GetUnderlyingType(col.ClrType);
            }

            res.val = ConvertFilterValue(val, res.type);
            return res;
        }

        if (col.ClrType == typeof(DateOnly) || col.ClrType == typeof(DateOnly?))
        {
            res.type = typeof(DateOnly);
            res.val = DateOnly.Parse(val);
            return res;
        }

        if (col.ClrType == typeof(bool) || col.ClrType == typeof(bool?))
        {
            res.type = typeof(bool);
            res.val = ConvertFilterValue(val, res.type);
            return res;
        }

        if (col.ClrType == typeof(short) || col.ClrType == typeof(short?))
        {
            res.type = typeof(short);
            res.val = ConvertFilterValue(val, res.type);
            return res;
        }

        if (col.ClrType == typeof(int) || col.ClrType == typeof(int?))
        {
            res.type = typeof(int);
            res.val = ConvertFilterValue(val, res.type);
            return res;
        }

        if (col.ClrType == typeof(float) || col.ClrType == typeof(float?))
        {
            res.type = typeof(float);
            res.val = float.Parse(val, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            return res;
        }

        if (col.ClrType == typeof(decimal) || col.ClrType == typeof(decimal?))
        {
            res.type = typeof(decimal);
            res.val = decimal.Parse(val, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            return res;
        }

        if (col.ClrType == typeof(Guid) || col.ClrType == typeof(Guid?))
        {
            res.type = typeof(Guid);
            res.val = Guid.Parse(val);
            return res;
        }

        res.type = typeof(decimal);
        res.val = decimal.Parse(val, new NumberFormatInfo() { NumberDecimalSeparator = "." });

        return res;
    }

    private object ConvertFilterValue(object value, Type valType)
    {
        return Convert.ChangeType(value, valType);
    }
}
