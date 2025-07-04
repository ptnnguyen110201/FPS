using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class DIContainer
{
    private readonly Dictionary<Type, Func<object>> bindings = new();
    private readonly Dictionary<Type, object> cachedInstances = new();
    private readonly Dictionary<Type, ConstructorInfo> cachedCtors = new();

    public void Bind<TInterface, TImplementation>() where TImplementation : TInterface
    {
        Debug.Log($"[DI] Bind<{typeof(TInterface).Name}> to {typeof(TImplementation).Name}");
        this.bindings[typeof(TInterface)] = () => CreateInstance(typeof(TImplementation));
    }

    public void Bind<TInterface>(Func<object> factory) => this.bindings[typeof(TInterface)] = factory;
   

    public void BindInstance<TInterface>(TInterface instance) => this.cachedInstances[typeof(TInterface)] = instance;
    public TInterface Resolve<TInterface>() => (TInterface)Resolve(typeof(TInterface));

    public bool TryResolve<TInterface>(out TInterface result)
    {
        try
        {
            result = Resolve<TInterface>();
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public bool IsBound<T>() => this.bindings.ContainsKey(typeof(T)) || this.cachedInstances.ContainsKey(typeof(T));

    public object Resolve(Type type)
    {
        if (this.cachedInstances.TryGetValue(type, out object cached))
            return cached;

        if (this.bindings.TryGetValue(type, out Func<object> creator))
        {
            object instance = creator();
            this.cachedInstances[type] = instance;
            return instance;
        }

        if (!type.IsAbstract && !type.IsInterface)
        {
            object instance = this.CreateInstance(type);
            this.cachedInstances[type] = instance;
            return instance;
        }

        throw new Exception($"[DI] No binding found for type {type.Name}");
    }

    private object CreateInstance(Type type)
    {
        ConstructorInfo ctor = GetConstructor(type);
        if (ctor == null)
            throw new Exception($"No public constructor found for {type.Name}");

        object[] args = ctor.GetParameters().Select(p => Resolve(p.ParameterType)).ToArray();
        object instance = Activator.CreateInstance(type, args);
        InjectInto(instance);
        return instance;
    }

    private ConstructorInfo GetConstructor(Type type)
    {
        if (!this.cachedCtors.TryGetValue(type, out ConstructorInfo ctor))
        {
            ctor = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();
            this.cachedCtors[type] = ctor;
        }
        return ctor;
    }

    public void InjectInto(object target)
    {
        var fields = target.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .Where(f => f.GetCustomAttribute<InjectAttribute>() != null);

        foreach (var field in fields)
        {
            object dependency = Resolve(field.FieldType);
            field.SetValue(target, dependency);
        }

        var properties = target.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<InjectAttribute>() != null && p.CanWrite);

        foreach (var prop in properties)
        {
            object dependency = Resolve(prop.PropertyType);
            prop.SetValue(target, dependency);
        }
    }
}

