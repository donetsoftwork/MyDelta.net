using System;
using System.Text.Json;

namespace MyDeltas.Json;

/// <summary>
/// Json值处理
/// </summary>
public static class JsonValueConverter
{
    /// <summary>
    /// 获取字符串
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static string? GetString(JsonElement json)
        => json.GetString();
    /// <summary>
    /// 获取时间
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(JsonElement json)
        => json.GetDateTime();
    /// <summary>
    /// DateTimeOffset
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static DateTimeOffset GetDateTimeOffset(JsonElement json)
        => json.GetDateTimeOffset();
}
