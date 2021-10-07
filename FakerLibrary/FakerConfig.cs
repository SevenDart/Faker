using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FakerLibrary.Generators;

namespace FakerLibrary
{
    public class FakerConfig
    {
        public class MemberConfig
        {
            public MemberInfo MemberInfo { get; }
            public IValueGenerator Generator { get; }

            public MemberConfig(MemberInfo memberInfo, IValueGenerator generator)
            {
                MemberInfo = memberInfo;
                Generator = generator;
            }
        }

        public readonly Dictionary<Type, List<MemberConfig>> Configs = new();
        
        public void Add<TContainer, TItem, TGenerator>(Expression<Func<TContainer, TItem>> expression) 
            where TGenerator: IValueGenerator
        {
            var containerType = typeof(TContainer);
            var generator = (IValueGenerator) Activator.CreateInstance(typeof(TGenerator));

            var memberExpression = expression.Body as MemberExpression;
            var member = memberExpression?.Member;

            if (member?.MemberType != MemberTypes.Field && member?.MemberType != MemberTypes.Property)
            {
                throw new ArgumentException("expression must return the configuring field or property.");
            }

            var memberConfig = new MemberConfig(member, generator);
            
            Configs.Add(containerType, new List<MemberConfig>());
            Configs[containerType].Add(memberConfig);
        }
    }
}