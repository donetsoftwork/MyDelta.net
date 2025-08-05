using MyDeltas.Members;
using System;
using System.Collections.Generic;

namespace MyDeltas;

/// <summary>
/// 扩展方法
/// </summary>
public static class MyDeltaServices
{
    #region IMyDeltaFactory
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory factory, TInstance instance, MyDelta delta)
        => factory.Create(instance, delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory factory, TInstance instance)
        => factory.Create(instance, new Dictionary<string, object?>());
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="changed"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory factory, IDictionary<string, object?> changed)
        => factory.Create(Activator.CreateInstance<TInstance>(), changed);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="delta"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory factory, MyDelta delta)
        => factory.Create(Activator.CreateInstance<TInstance>(), delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance">结构类型</typeparam>
    /// <param name="factory"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory factory)
        => factory.Create(Activator.CreateInstance<TInstance>(), new Dictionary<string, object?>());
    #endregion
    #region IMyDeltaFactory<TInstance>
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory<TInstance> factory, TInstance instance, MyDelta delta)
        => factory.Create(instance, delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory<TInstance> factory, TInstance instance)
        => factory.Create(instance, new Dictionary<string, object?>());
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="changed"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory<TInstance> factory, IDictionary<string, object?> changed)
        => factory.Create(Activator.CreateInstance<TInstance>(), changed);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="delta"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory<TInstance> factory, MyDelta delta)
        => factory.Create(Activator.CreateInstance<TInstance>(), delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TInstance">结构类型</typeparam>
    /// <param name="factory"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TInstance> Create<TInstance>(this IMyDeltaFactory<TInstance> factory)
        => factory.Create(Activator.CreateInstance<TInstance>(), new Dictionary<string, object?>());
    #endregion
    #region MemberAccessorFactory
    /// <summary>
    /// 设置成员访问器
    /// </summary>
    /// <typeparam name="TMemberAccessorFactory"></typeparam>
    /// <typeparam name="TInstance"></typeparam>
    /// <param name="factory"></param>
    /// <param name="members"></param>
    /// <returns></returns>
    public static TMemberAccessorFactory Use<TMemberAccessorFactory, TInstance>(this TMemberAccessorFactory factory, IDictionary<string, IMemberAccessor<TInstance>> members)
        where TMemberAccessorFactory : IMemberAccessorFactory
    {
        factory.SetMembers(members);
        return factory;
    }
    #endregion
}
