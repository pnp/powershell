using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model;
using PnPCore = PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ContentTypePipeBind
    {
        private readonly string _idOrName;
        public ContentType ContentTypeInstance { get; private set; }

        private readonly PnPCore.IContentType _coreContentType;

        public ContentTypePipeBind(string idOrName)
        {
            if (string.IsNullOrEmpty(idOrName))
                throw new ArgumentException("Content type name or ID null or empty.", nameof(idOrName));

            _idOrName = idOrName;
        }

        public ContentTypePipeBind(ContentTypeId contentTypeId)
        {
            _idOrName = contentTypeId?.StringValue
                ?? throw new ArgumentNullException(nameof(contentTypeId));
        }

        public ContentTypePipeBind(ContentType contentType)
        {
            ContentTypeInstance = contentType
                ?? throw new ArgumentNullException(nameof(contentType));
        }

        public ContentTypePipeBind(PnPCore.IContentType contentType)
        {
            _coreContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }

        private string GetId()
        {
            if (_coreContentType != null)
            {
                _coreContentType.EnsurePropertiesAsync(ct => ct.StringId);
                return _coreContentType.StringId;
            }
            if (ContentTypeInstance != null)
            {
                return ContentTypeInstance.EnsureProperty(ct => ct.StringId);
            }
            if (_idOrName.ToLower().StartsWith("0x0"))
            {
                return _idOrName;
            }
            return null;
        }

        public string GetId(Web web, bool searchInSiteHierarchy = true)
            => GetId()
            ?? web.GetContentTypeByName(_idOrName, searchInSiteHierarchy)?.StringId;

        public string GetId(PnP.Core.Services.PnPContext context, bool searchInSiteHierarchy = true)
            => GetId()
            ?? (searchInSiteHierarchy ? context.Web.AvailableContentTypes : context.Web.ContentTypes).Where(ct => ct.Name == _idOrName).FirstOrDefault()?.StringId;
        public string GetId(List list)
            => GetId()
            ?? list.GetContentTypeByName(_idOrName)?.StringId;

        public string GetId(PnPCore.IList list)
            => GetId()
            ?? list.ContentTypes.Where(ct => ct.Name == _idOrName).FirstOrDefault()?.StringId;

        internal string GetIdOrThrow(string paramName, Web web, bool searchInSiteHierarchy = true)
            => GetId(web, searchInSiteHierarchy)
            ?? throw new PSArgumentException(NotFoundMessage(searchInSiteHierarchy), paramName);

        internal string GetIdOrThrow(string paramName, List list)
            => GetId(list)
            ?? throw new PSArgumentException(NotFoundMessage(list), paramName);

        internal string GetIdOrThrow(string paramName, PnPCore.IList list)
            => GetId(list)
            ?? throw new PSArgumentException(NotFoundMessage(list), paramName);

        public string GetIdOrWarn(BasePSCmdlet cmdlet, Web web, bool searchInSiteHierarchy = true)
        {
            var id = GetId(web, searchInSiteHierarchy);
            if (id is null)
                cmdlet.LogWarning(NotFoundMessage(searchInSiteHierarchy));

            return id;
        }

        public string GetIdOrWarn(BasePSCmdlet cmdlet, Core.Services.PnPContext context, bool searchInSiteHierarchy = true)
        {
            var id = GetId(context, searchInSiteHierarchy);
            if (id is null)
                cmdlet.LogWarning(NotFoundMessage(searchInSiteHierarchy));

            return id;
        }

        public string GetIdOrWarn(BasePSCmdlet cmdlet, List list)
        {
            var id = GetId(list);
            if (id is null)
            {
                cmdlet.LogWarning(NotFoundMessage(list));
            }

            return id;
        }

        internal ContentType GetContentType(Web web, bool searchInSiteHierarchy = true)
        {
            if (ContentTypeInstance is object)
            {
                return ContentTypeInstance;
            }

            var id = GetId();
            if (!string.IsNullOrEmpty(id))
            {
                return web.GetContentTypeById(id, searchInSiteHierarchy);
            }

            return web.GetContentTypeByName(_idOrName, searchInSiteHierarchy);
        }

        internal PnPCore.IContentType GetContentType(Core.Services.PnPContext context, bool searchInSiteHierarchy = true)
        {
            if (_coreContentType is object)
            {
                return _coreContentType;
            }
            if (!string.IsNullOrEmpty(_idOrName))
            {
                var collection = searchInSiteHierarchy ? context.Web.AvailableContentTypes : context.Web.ContentTypes;

                if (_idOrName.ToLower().StartsWith("0x0"))
                {
                    return collection.Where(ct => ct.StringId == _idOrName).FirstOrDefault();
                }
                else
                {
                    return collection.Where(ct => ct.Name == _idOrName).FirstOrDefault();
                }
            }
            return null;
        }
        internal ContentType GetContentType(List list)
        {
            if (ContentTypeInstance is object)
            {
                return ContentTypeInstance;
            }

            var id = GetId();
            if (!string.IsNullOrEmpty(id))
            {
                var ct = list.ContentTypes.GetById(id);
                list.Context.Load(ct, c => c.Id, c => c.Name, c => c.Group, c => c.Description);
                list.Context.ExecuteQueryRetry();
                return ct;
            }

            return list.GetContentTypeByName(_idOrName);
        }

        internal PnP.Core.Model.SharePoint.IContentType GetContentType(PnP.Core.Model.SharePoint.IList list)
        {
            if (ContentTypeInstance is object)
            {
                var stringId = ContentTypeInstance.EnsureProperty(c => c.StringId);
                return list.ContentTypes.FirstOrDefault(c => c.StringId == stringId);
            }
            var id = _idOrName.ToLower().StartsWith("0x0") ? _idOrName : null;
            if (!string.IsNullOrEmpty(id))
            {
                return list.ContentTypes.FirstOrDefault(c => c.Id == id);
            }

            return list.ContentTypes.FirstOrDefault(c => c.Name == _idOrName);
        }

        internal PnP.Core.Model.SharePoint.IContentType GetContentType(PnPBatch batch, PnP.Core.Model.SharePoint.IList list)
        {
            PnPCore.IContentType returnCt = null;
            if (ContentTypeInstance is object)
            {
                var stringId = ContentTypeInstance.EnsureProperty(c => c.StringId);
                var batchedCt = batch.GetCachedContentType(stringId);
                if (batchedCt != null)
                {
                    return batchedCt;
                }
                list.EnsureProperties(l => l.ContentTypes);
                returnCt = list.ContentTypes.FirstOrDefault(c => c.StringId == stringId);
            }
            else
            {
                var id = _idOrName.ToLower().StartsWith("0x0") ? _idOrName : null;
                if (!string.IsNullOrEmpty(id))
                {
                    var batchedCt = batch.GetCachedContentType(id);
                    if (batchedCt != null)
                    {
                        return batchedCt;
                    }
                    list.EnsureProperties(l => l.ContentTypes);
                    returnCt = list.ContentTypes.FirstOrDefault(c => c.Id == id);
                }
                else
                {
                    var batchedCt = batch.GetCachedContentType(_idOrName);
                    if (batchedCt != null)
                    {
                        return batchedCt;
                    }
                    list.EnsureProperties(l => l.ContentTypes);
                    returnCt = list.ContentTypes.FirstOrDefault(c => c.Name == _idOrName);
                }
            }
            if (returnCt != null)
            {
                returnCt.EnsureProperties(ct => ct.StringId);
                batch.CacheContentType(returnCt);
            }
            return returnCt;
        }


        internal ContentType GetContentTypeOrThrow(string paramName, Web web, bool searchInSiteHierarchy = true)
                => GetContentType(web, searchInSiteHierarchy)
                ?? throw new PSArgumentException(NotFoundMessage(searchInSiteHierarchy), paramName);

        internal PnPCore.IContentType GetContentTypeOrThrow(string paramName, PnP.Core.Services.PnPContext context, bool searchInSiteHierarchy = true)
            => GetContentType(context, searchInSiteHierarchy)
            ?? throw new PSArgumentException(NotFoundMessage(searchInSiteHierarchy), paramName);

        internal ContentType GetContentTypeOrError(BasePSCmdlet cmdlet, string paramName, Web web, bool searchInSiteHierarchy = true)
        {
            var ct = GetContentType(web, searchInSiteHierarchy);
            if (ct is null)
                cmdlet.LogError(new PSArgumentException(NotFoundMessage(searchInSiteHierarchy), paramName));
            return ct;
        }

        internal PnPCore.IContentType GetContentTypeOrError(BasePSCmdlet cmdlet, string paramName, PnP.Core.Services.PnPContext context, bool searchInSiteHierarchy = true)
        {
            var ct = GetContentType(context, searchInSiteHierarchy);
            if (ct is null)
            {
                cmdlet.LogError(new PSArgumentException(NotFoundMessage(searchInSiteHierarchy), paramName));
            }
            return ct;
        }

        internal ContentType GetContentTypeOrThrow(string paramName, List list)
            => GetContentType(list)
            ?? throw new PSArgumentException(NotFoundMessage(list), paramName);

        internal ContentType GetContentTypeOrError(BasePSCmdlet cmdlet, string paramName, List list)
        {
            var ct = GetContentType(list);
            if (ct is null)
                cmdlet.LogError(new PSArgumentException(NotFoundMessage(list), paramName));
            return ct;
        }

        internal PnPCore.IContentType GetContentTypeOrError(BasePSCmdlet cmdlet, string paramName, PnPCore.IList list)
        {
            var ct = GetContentType(list);
            if (ct is null)
                cmdlet.LogError(new PSArgumentException(NotFoundMessage(list), paramName));
            return ct;
        }

        internal ContentType GetContentTypeOrWarn(BasePSCmdlet cmdlet, Web web, bool searchInSiteHierarchy = true)
        {
            var ct = GetContentType(web, searchInSiteHierarchy);
            if (ct is null)
                cmdlet.LogWarning(NotFoundMessage(searchInSiteHierarchy));

            return ct;
        }

        internal PnPCore.IContentType GetContentTypeOrWarn(BasePSCmdlet cmdlet, Core.Services.PnPContext context, bool searchInSiteHierarchy = true)
        {
            var ct = GetContentType(context, searchInSiteHierarchy);
            if (ct is null)
                cmdlet.LogWarning(NotFoundMessage(searchInSiteHierarchy));

            return ct;
        }

        internal ContentType GetContentTypeOrWarn(BasePSCmdlet cmdlet, List list)
        {
            var ct = GetContentType(list);
            if (ct is null)
                cmdlet.LogWarning(NotFoundMessage(list));

            return ct;
        }

        internal PnPCore.IContentType GetContentTypeOrWarn(BasePSCmdlet cmdlet, PnPCore.IList list)
        {
            var ct = GetContentType(list);
            if (ct is null)
                cmdlet.LogWarning(NotFoundMessage(list));

            return ct;
        }

        internal PnP.Core.Model.SharePoint.IContentType GetContentTypeOrWarn(BasePSCmdlet cmdlet, PnPBatch batch, PnP.Core.Model.SharePoint.IList list)
        {
            var ct = GetContentType(batch, list);
            if (ct is null)
                cmdlet.LogWarning(NotFoundMessage(list));

            return ct;
        }

        private string NotFoundMessage(bool searchInSiteHierarchy)
            => $"Content type '{this}' not found in site{(searchInSiteHierarchy ? " hierarchy" : "")}.";

        private string NotFoundMessage(List list)
            => $"Content type '{this}' not found in list '{new ListPipeBind(list)}'.";

        private string NotFoundMessage(PnPCore.IList list)
                   => $"Content type '{this}' not found in list '{new ListPipeBind(list)}'.";

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_idOrName))
            {
                return _idOrName;
            }
            else
            {
                if (ContentTypeInstance != null)
                {
                    return ContentTypeInstance.Name;
                }
                if (_coreContentType != null)
                {
                    return _coreContentType.Name;
                }
            }
            return "Unknown identity";
        }
    }
}
