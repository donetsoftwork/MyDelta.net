using MyDeltas.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyDeltas;

/// <summary>
/// 数据变化
/// </summary>
/// <param name="data"></param>
[JsonConverter(typeof(MyDeltaConverterFactory))]
public class MyDelta(IDictionary<string, object?> data)
{
    /// <summary>
    /// 数据变化
    /// </summary>
    public MyDelta()
        : this(new Dictionary<string, object?>())
    {
    }
    #region 配置
    /// <summary>
    /// 变化的数据
    /// </summary>
    protected readonly IDictionary<string, object?> _data = data;
    /// <summary>
    /// 变化的数据
    /// </summary>
    public IDictionary<string, object?> Data
        => _data;
    /// <summary>
    /// 被修改的属性名称
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetChangedNames()
        => _data.Keys;
    #endregion
    #region 方法
    /// <summary>
    /// 属性是否被修改
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool HasChanged(string name)
        => _data.ContainsKey(name);
    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetValue(string name, object? value)
        => _data[name] = value;
    /// <summary>
    /// 获取被修改的属性值
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual object? GetValue(string name)
        => _data.TryGetValue(name, out var value) ? value : throw new ArgumentOutOfRangeException(name);
    #endregion
    /// <summary>
    /// 判断值是否变更
    /// </summary>
    /// <param name="value0"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool CheckChange(object? value0, object? value)
    {
        if (value0 == null)
        {
            if (value == null)
                return false;
            return true;
        }
        return !value0.Equals(value);
    }
    /// <summary>
    /// 检查值类型(序列化为JsonElement处理)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="expectedType">期望类型</param>
    /// <returns></returns>
    public static object? CheckValueType(object? value, Type expectedType)
    {
        if (value is null)
            return null;
        var valueType = value.GetType();
        if (expectedType.IsAssignableFrom(valueType))
            return value;
        if (value is JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Null:
                    return null;
                case JsonValueKind.String:
                    if (expectedType == typeof(string))
                        return jsonElement.GetString();
                    else if (expectedType == typeof(DateTime))
                        return jsonElement.GetDateTime();
                    else if (expectedType == typeof(DateTimeOffset))
                        return jsonElement.GetDateTimeOffset();
                    else if (expectedType == typeof(Guid))
                        return jsonElement.GetGuid();
                    return jsonElement.GetString();
                case JsonValueKind.Number:
                    if (expectedType == typeof(decimal))
                        return jsonElement.GetDecimal();
                    return jsonElement.GetDouble();
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return jsonElement.GetBoolean();
                default:
                    break;
            }
        }
        return value;
    }
}
