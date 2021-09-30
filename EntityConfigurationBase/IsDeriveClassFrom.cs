using System;
using System.Collections.Generic;

namespace EntityConfigurationBase
{
   public static class TypeEctensions
    {
        public static bool IsDeriveClassFrom<TBaseType>(this Type type, bool canAbstract = false)
        {
            return IsDeriveClassFrom(type, typeof(TBaseType), canAbstract);
        }

        /// <summary>
        /// 判断当前类型是否可由指定类型派生
        /// </summary>
        public static bool IsDeriveClassFrom(this Type type, Type baseType, bool canAbstract = false)
        {
            bool result = false;
            if(type!=null&&baseType!=null)
            {
                result= type.IsClass && (canAbstract || !type.IsAbstract) && type.IsBaseOn(baseType);
            }

            return result;
        }
        public static bool IsBaseOn(this Type type, Type baseType)
        {
            if (baseType.IsGenericTypeDefinition)
            {
                return baseType.IsGenericAssignableFrom(type);
            }
            return baseType.IsAssignableFrom(type);
        }
        public static bool IsGenericAssignableFrom(this Type genericType, Type type)
        {
            bool result = false;
            if(genericType!=null && type!=null)
            {
                if (!genericType.IsGenericType)
                {
                    throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");
                }

                List<Type> allOthers = new List<Type> { type };
                if (genericType.IsInterface)
                {
                    allOthers.AddRange(type.GetInterfaces());
                }

                foreach (var other in allOthers)
                {
                    Type cur = other;
                    while (cur != null)
                    {
                        if (cur.IsGenericType)
                        {
                            cur = cur.GetGenericTypeDefinition();
                        }
                        if (cur.IsSubclassOf(genericType) || cur == genericType)
                        {
                            result= true;
                        }
                        cur = cur.BaseType;
                    }
                }
                
            }

            return result;
        }
    }
}
