using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace Shopping.Framework.Common
{
    public class AutoMapperExtensions : Profile
    {
        public AutoMapperExtensions()
        {
            Init();
        }
        public static Assembly[]? Assemblies;

        public void Init()
        {
            Type autoMapperDtoType = typeof(IAutoMapperDto);

            Type toCurrentType = typeof(IToCurrent<>);
            Type toEntityType = typeof(IToEntity<>);
            if (Assemblies != null)
            {
                foreach (var assembly in Assemblies)
                {
                    var types = assembly.GetTypes().Where(a => !a.IsAbstract && !a.IsInterface && a.GetInterfaces().Contains(autoMapperDtoType));
                    foreach (Type _type in types)
                    {
                        var interfaces = _type.GetInterfaces();
                        var toCurrent = interfaces.FirstOrDefault(a => a.IsGenericType && a.GetGenericTypeDefinition() == toCurrentType);
                        if (toCurrent != null)
                        {
                            var entity = toCurrent.GenericTypeArguments.FirstOrDefault();
                            CreateMap(entity, _type);
                        }

                        var toEntity = interfaces.FirstOrDefault(a => a.IsGenericType && a.GetGenericTypeDefinition() == toEntityType);
                        if (toEntity != null)
                        {
                            var entity = toEntity.GenericTypeArguments.FirstOrDefault();
                            CreateMap(_type, entity);
                        }
                    }
                }
            }
        }
    }
    public interface IAutoMapperDto
    { }
    /// <summary>
    /// T 转 当前类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IToCurrent<T> : IAutoMapperDto
    { }
    /// <summary>
    /// 当前类 转 T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IToEntity<T> : IAutoMapperDto
    { }
}