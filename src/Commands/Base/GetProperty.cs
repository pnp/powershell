using Microsoft.SharePoint.Client;
using System;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPProperty")]
    [OutputType(typeof(object))]
    public class EnsureProperty : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public ClientObject ClientObject;

        [Parameter(Mandatory = true, Position = 1)]
        public string[] Property;

        protected override void ExecuteCmdlet()
        {

            var loadRequired = false;
            foreach (var property in Property)
            {
                var expression = GetClientObjectExpression(ClientObject, property);

                if (!ClientObject.IsPropertyAvailable(expression))
                {
                    ClientObject.Context.Load(ClientObject, expression);
                    loadRequired = true;
                }
            }
            if (loadRequired)
            {
                ClientObject.Context.ExecuteQueryRetry();
            }
            if (Property.Length == 1)
            {
                WriteObject((GetClientObjectExpression(ClientObject, Property[0]).Compile())(ClientObject));
            }

        }

        private static Expression<Func<ClientObject, object>> GetClientObjectExpression(ClientObject clientObject, string property)
        {
            var memberExpression = Expression.PropertyOrField(Expression.Constant(clientObject), property);
            var memberName = memberExpression.Member.Name;

            var parameter = Expression.Parameter(typeof(ClientObject), "i");
            var cast = Expression.Convert(parameter, memberExpression.Member.ReflectedType);
            var body = Expression.Property(cast, memberName);
            var exp = Expression.Lambda<Func<ClientObject, Object>>(Expression.Convert(body, typeof(object)),
                parameter);

            return exp;

        }

    }
}
