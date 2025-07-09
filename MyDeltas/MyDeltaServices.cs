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
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory factory, TStructuralType instance, MyDelta delta)
        => factory.Create(instance, delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory factory, TStructuralType instance)
        => factory.Create(instance, new Dictionary<string, object?>());
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="changed"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory factory, IDictionary<string, object?> changed)
        => factory.Create(Activator.CreateInstance<TStructuralType>(), changed);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="delta"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory factory, MyDelta delta)
        => factory.Create(Activator.CreateInstance<TStructuralType>(), delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType">结构类型</typeparam>
    /// <param name="factory"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory factory)
        => factory.Create(Activator.CreateInstance<TStructuralType>(), new Dictionary<string, object?>());
    #endregion
    #region IMyDeltaFactory<TStructuralType>
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory<TStructuralType> factory, TStructuralType instance, MyDelta delta)
        => factory.Create(instance, delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="instance"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory<TStructuralType> factory, TStructuralType instance)
        => factory.Create(instance, new Dictionary<string, object?>());
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="changed"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory<TStructuralType> factory, IDictionary<string, object?> changed)
        => factory.Create(Activator.CreateInstance<TStructuralType>(), changed);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="delta"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory<TStructuralType> factory, MyDelta delta)
        => factory.Create(Activator.CreateInstance<TStructuralType>(), delta.Data);
    /// <summary>
    /// 创建一个新的 MyDelta 实例
    /// </summary>
    /// <typeparam name="TStructuralType">结构类型</typeparam>
    /// <param name="factory"></param>
    /// <returns>MyDelta 实例</returns>
    public static MyDelta<TStructuralType> Create<TStructuralType>(this IMyDeltaFactory<TStructuralType> factory)
        => factory.Create(Activator.CreateInstance<TStructuralType>(), new Dictionary<string, object?>());
    #endregion
    #region MemberAccessorFactory
    /// <summary>
    /// 设置成员访问器
    /// </summary>
    /// <typeparam name="TMemberAccessorFactory"></typeparam>
    /// <typeparam name="TStructuralType"></typeparam>
    /// <param name="factory"></param>
    /// <param name="members"></param>
    /// <returns></returns>
    public static TMemberAccessorFactory Use<TMemberAccessorFactory, TStructuralType>(this TMemberAccessorFactory factory, IDictionary<string, IMemberAccessor<TStructuralType>> members)
        where TMemberAccessorFactory : IMemberAccessorFactory
    {
        factory.SetMembers(members);
        return factory;
    }
    #endregion
}
