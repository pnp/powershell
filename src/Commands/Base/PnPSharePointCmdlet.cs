using System;
using System.Management.Automation;
using System.Reflection;
using System.Threading;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base;
using Resources = PnP.PowerShell.Commands.Properties.Resources;


namespace PnP.PowerShell.Commands
{
    /// <summary>
    /// Base class for all the PnP SharePoint related cmdlets
    /// </summary>
    public class PnPSharePointCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Reference the the SharePoint context on the current connection. If NULL it means there is no SharePoint context available on the current connection.
        /// </summary>
        public ClientContext ClientContext => Connection?.Context ?? PnPConnection.CurrentConnection.Context;

        [Parameter(Mandatory = false)] // do not remove '#!#99'
        public PnPConnection Connection = null;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (PnPConnection.CurrentConnection != null && PnPConnection.CurrentConnection.ApplicationInsights != null)
            {
                PnPConnection.CurrentConnection.ApplicationInsights.TrackEvent(MyInvocation.MyCommand.Name);
            }

            if (Connection == null && ClientContext == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
        }

        protected override void ProcessRecord()
        {
            try
            {
                var tag = PnPConnection.CurrentConnection.PnPVersionTag + ":" + MyInvocation.MyCommand.Name.Replace("SPO", "");
                if (tag.Length > 32)
                {
                    tag = tag.Substring(0, 32);
                }
                ClientContext.ClientTag = tag;

                ExecuteCmdlet();
            }
            catch (PipelineStoppedException)
            {
                //don't swallow pipeline stopped exception
                //it makes select-object work weird
                throw;
            }
            catch (Exception ex)
            {
                PnPConnection.CurrentConnection.RestoreCachedContext(PnPConnection.CurrentConnection.Url);
                ex.Data.Add("CorrelationId", PnPConnection.CurrentConnection.Context.TraceCorrelationId);
                ex.Data.Add("TimeStampUtc", DateTime.UtcNow);
                var errorDetails = new ErrorDetails(ex.Message);

                errorDetails.RecommendedAction = "Use Get-PnPException for more details.";
                var errorRecord = new ErrorRecord(ex, "EXCEPTION", ErrorCategory.WriteError, null);
                errorRecord.ErrorDetails = errorDetails;

                WriteError(errorRecord);
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        protected string AccessToken
        {
            get
            {
                if (PnPConnection.CurrentConnection != null)
                {
                    if (PnPConnection.CurrentConnection.Context != null)
                    {
                        var settings = GetContextSettings(PnPConnection.CurrentConnection.Context);
                        if (settings != null)
                        {
                            var authManager = settings.AuthenticationManager;
                            if (authManager != null)
                            {
                                return authManager.GetAccessTokenAsync(PnPConnection.CurrentConnection.Context.Url).GetAwaiter().GetResult();
                            }
                        }
                    }
                }
                return null;
            }
        }

        internal ClientContextSettings GetContextSettings(ClientRuntimeContext clientContext)
        {
            if (!clientContext.StaticObjects.TryGetValue(ClientContextSettings.PnPSettingsKey, out object settingsObject))
            {
                return null;
            }
            var settings = new ClientContextSettings();

            settings.AuthenticationManager = (AuthenticationManager)GetPropertyValue(settingsObject,"AuthenticationManager");

            return settings;
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            Type objType = obj.GetType();
            PropertyInfo propInfo = GetPropertyInfo(objType, propertyName);
            if (propInfo == null)
                throw new ArgumentOutOfRangeException("propertyName",
                  string.Format("Couldn't find property {0} in type {1}", propertyName, objType.FullName));
            return propInfo.GetValue(obj, null);
        }

        private PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propInfo = null;
            do
            {
                propInfo = type.GetProperty(propertyName,
                       BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (propInfo == null && type != null);
            return propInfo;
        }
    }
}
