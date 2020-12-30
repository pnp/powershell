using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ContentTypePipeBind
    {
        private readonly string _idOrName;
        private readonly ContentType _contentType;

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
            _contentType = contentType
                ?? throw new ArgumentNullException(nameof(contentType));
        }

        private string GetId()
            => _contentType?.EnsureProperty(ct => ct.StringId)
            ?? (_idOrName.ToLower().StartsWith("0x0") ? _idOrName : null);

        public string GetId(Web web, bool searchInSiteHierarchy = true)
            => GetId()
            ?? web.GetContentTypeByName(_idOrName, searchInSiteHierarchy)?.StringId;

        public string GetId(List list)
            => GetId()
            ?? list.GetContentTypeByName(_idOrName)?.StringId;

        internal string GetIdOrThrow(string paramName, Web web, bool searchInSiteHierarchy = true)
            => GetId(web, searchInSiteHierarchy)
            ?? throw new PSArgumentException(NotFoundMessage(web, searchInSiteHierarchy), paramName);

        internal string GetIdOrThrow(string paramName, List list)
            => GetId(list)
            ?? throw new PSArgumentException(NotFoundMessage(list), paramName);


        public string GetIdOrWarn(Cmdlet cmdlet, Web web, bool searchInSiteHierarchy = true)
        {
            var id = GetId(web, searchInSiteHierarchy);
            if (id is null)
                cmdlet.WriteWarning(NotFoundMessage(web, searchInSiteHierarchy));

            return id;
        }

        public string GetIdOrWarn(Cmdlet cmdlet, List list)
        {
            var id = GetId(list);
            if (id is null)
            {
                cmdlet.WriteWarning(NotFoundMessage(list));
            }

            return id;
        }

        internal ContentType GetContentType(Web web, bool searchInSiteHierarchy = true)
        {
            if (_contentType is object)
            {
                return _contentType;
            }

            var id = GetId();
            if (!string.IsNullOrEmpty(id))
            {
                return web.GetContentTypeById(id, searchInSiteHierarchy);
            }

            return web.GetContentTypeByName(_idOrName, searchInSiteHierarchy);
        }

        internal ContentType GetContentType(List list)
        {
            if (_contentType is object)
            {
                return _contentType;
            }

            var id = GetId();
            if (!string.IsNullOrEmpty(id))
            {
                return list.ContentTypes.GetById(id);
            }

            return list.GetContentTypeByName(_idOrName);
        }

        internal PnP.Core.Model.SharePoint.IContentType GetContentType(PnP.Core.Model.SharePoint.IList list)
        {
            if(_contentType is object)
            {
                var stringId = _contentType.EnsureProperty(c => c.StringId);
                return list.ContentTypes.FirstOrDefault(c => c.StringId == stringId);
            }
            var id = _idOrName.ToLower().StartsWith("0x0") ? _idOrName : null;
            if(!string.IsNullOrEmpty(id))
            {
                return list.ContentTypes.FirstOrDefault(c => c.Id == id);
            }

            return list.ContentTypes.FirstOrDefault(c => c.Name == _idOrName);
        }


        internal ContentType GetContentTypeOrThrow(string paramName, Web web, bool searchInSiteHierarchy = true)
            => GetContentType(web)
            ?? throw new PSArgumentException(NotFoundMessage(web, searchInSiteHierarchy), paramName);

        internal ContentType GetContentTypeOrError(Cmdlet cmdlet, string paramName, Web web, bool searchInSiteHierarchy = true)
        {
            var ct = GetContentType(web, searchInSiteHierarchy);
            if (ct is null)
                cmdlet.WriteError(new ErrorRecord(new PSArgumentException(NotFoundMessage(web, searchInSiteHierarchy), paramName), "CONTENTTYPEDOESNOTEXIST", ErrorCategory.InvalidArgument, this));
            return ct;
        }

        internal ContentType GetContentTypeOrThrow(string paramName, List list)
            => GetContentType(list)
            ?? throw new PSArgumentException(NotFoundMessage(list), paramName);

        internal ContentType GetContentTypeOrError(Cmdlet cmdlet, string paramName, List list)
        {
            var ct = GetContentType(list);
            if (ct is null)
                cmdlet.WriteError(new ErrorRecord(new PSArgumentException(NotFoundMessage(list), paramName), "CONTENTTYPEDOESNOTEXIST", ErrorCategory.InvalidArgument, this));
            return ct;
        }

        internal ContentType GetContentTypeOrWarn(Cmdlet cmdlet, Web web, bool searchInSiteHierarchy = true)
        {
            var ct = GetContentType(web, searchInSiteHierarchy);
            if (ct is null)
                cmdlet.WriteWarning(NotFoundMessage(web, searchInSiteHierarchy));

            return ct;
        }

        internal ContentType GetContentTypeOrWarn(Cmdlet cmdlet, List list)
        {
            var ct = GetContentType(list);
            if (ct is null)
                cmdlet.WriteWarning(NotFoundMessage(list));

            return ct;
        }

        private string NotFoundMessage(Web web, bool searchInSiteHierarchy)
            => $"Content type '{this}' not found in site {(searchInSiteHierarchy ? "hierachy" : "")}.";

        private string NotFoundMessage(List list)
            => $"Content type '{this}' not found in list '{new ListPipeBind(list)}'.";

        public override string ToString()
            => _idOrName
            ?? (_contentType.IsPropertyAvailable(ct => ct.Name) ? _contentType.Name : null)
            ?? (_contentType.IsObjectPropertyInstantiated(ct => ct.StringId) ? _contentType.StringId : null)
            ?? $"[ContentType object with no name or Id]";
    }
}
