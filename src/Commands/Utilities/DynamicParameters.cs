using Microsoft.SharePoint.Client;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Utilities
{
    internal sealed class PropertyDynamicParameter : RuntimeDefinedParameter
    {
        public PropertyDynamicParameter(string name, Type parameterType, Collection<Attribute> attributes)
            : base(name, parameterType, attributes)
        { }

        public override int GetHashCode()
            => Name.GetHashCode();

        public override bool Equals(object obj)
            => obj is PropertyDynamicParameter pdp
            && pdp.Name == Name;

    }

    internal static class PropertyDynamicParameters
    {
        public static RuntimeDefinedParameterDictionary GetDynamicParametersForSettablePropertiesOfType<TType>()
            => GetDynamicParametersForSettablePropertiesOfType(typeof(TType));

        public static RuntimeDefinedParameterDictionary GetDynamicParametersForSettablePropertiesOfType(Type type)
        {
            var assignableFromType =
                type.Assembly.GetExportedTypes().Where(t => type.IsAssignableFrom(t));

            var parameterDictionary = new RuntimeDefinedParameterDictionary();
            var singleProps = new HashSet<string>();
            var setablePublicProps = assignableFromType.SelectMany(t => t.GetProperties(
                      System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.SetProperty
                    ))
                    .Where(p => !p.IsSpecialName
                            && p.SetMethod is object
                            && p.SetMethod.IsPublic
                            && !p.SetMethod.ContainsGenericParameters
                            && !p.GetIndexParameters().Any()
                            && !string.Equals(p.Name, "Tag", StringComparison.Ordinal)
                            && p.GetCustomAttributes(typeof(EditorBrowsableAttribute), true)
                                  .OfType<EditorBrowsableAttribute>()
                                  .All(a => a.State != EditorBrowsableState.Never))
                    .GroupBy(p => p.Name, resultSelector: (name, pg) =>
                    {
                        var propTypes = pg.Select(p => p.PropertyType).Distinct();
                        var pType = propTypes.Count() == 1
                            ? propTypes.Single()
                            : typeof(object);

                        if (pType == typeof(bool))
                            pType = typeof(SwitchParameter);

                        var count = pg.Count();

                        if (count == 1)
                        {
                            singleProps.Add(name);
                        }
                        return new
                        {
                            PropName = name,
                            PropType = pType,
                            SourceTypes = pg.Select(x => x.ReflectedType),
                            SourceTypesKey = count == 1 || count == assignableFromType.Count() ? null : string.Join("_", pg.Select(x => x.ReflectedType.Name).OrderBy(x => x)),
                        };
                    });

            foreach (var p in setablePublicProps)
            {
                var attributeCollection = new Collection<Attribute>();
                if (p.SourceTypesKey != null)
                    attributeCollection.Add(new ParameterAttribute() { Mandatory = false, ParameterSetName = p.SourceTypesKey });

                foreach (var sourceType in p.SourceTypes)
                    if (sourceType.Name != p.SourceTypesKey)
                        attributeCollection.Add(new ParameterAttribute() { Mandatory = singleProps.Contains(sourceType.Name), ParameterSetName = sourceType.Name });

                var runtimeParameter = new PropertyDynamicParameter(p.PropName, p.PropType, attributeCollection);
                parameterDictionary.Add(runtimeParameter.Name, runtimeParameter);
            }

            var unAmbiguous = parameterDictionary.Values.OfType<PropertyDynamicParameter>()
                .SelectMany(p => p.Attributes.OfType<ParameterAttribute>().Select(a => new { a.ParameterSetName, ParameterName = p.Name }))
                .GroupBy(x => x.ParameterSetName, elementSelector: p => p.ParameterName) 
                .GroupBy(e => string.Join(";", e.OrderBy(x => x)))
                .Where(x => x.Count() == 1)
                .Select(x=> x.Single().Key);

            var unAmbiguousSet = new HashSet<string>(unAmbiguous);

            foreach (var t in assignableFromType)
            {
                parameterDictionary.Add(t.Name, new RuntimeDefinedParameter(t.Name, typeof(SwitchParameter),
                    new Collection<Attribute>() { new ParameterAttribute() { Mandatory = type.Name != t.Name && !unAmbiguousSet.Contains(t.Name), ParameterSetName = t.Name } }));
            } 

            return parameterDictionary;
        }

        public static void SetValuesFromParams(this RuntimeDefinedParameterDictionary dict, ClientObject obj, PSCmdlet cmdlet)
            => SetValuesFromParams(dict, (object)obj.TypedObject, cmdlet);

        public static void SetValuesFromParams(this RuntimeDefinedParameterDictionary dict, object obj, PSCmdlet cmdlet)
        {
            var objType = obj.GetType();

            foreach (var dynParam in dict.Values.OfType<PropertyDynamicParameter>())
            {
                if (!cmdlet.MyInvocation.BoundParameters.TryGetValue(dynParam.Name, out var dynParamValueObj))
                    continue;

                if (dynParamValueObj is SwitchParameter switchParameter)
                    dynParamValueObj = (bool)switchParameter;

                objType.GetProperty(dynParam.Name, dynParam.ParameterType).SetValue(obj, dynParamValueObj);
            }
        }

        public static IEnumerable<Type> BaseTypes(this Type t, Type stopBefore = null)
        {
            if (t == stopBefore) yield break;
            while ((t = t.BaseType) != stopBefore)
                yield return t;
        }
    }
}
