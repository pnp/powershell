using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PlannerBucketPipeBind
    {
        private readonly string _id;
        private readonly PlannerBucket _bucket;

        public PlannerBucketPipeBind(string input)
        {
            _id = input;
        }

        public PlannerBucketPipeBind(PlannerBucket bucket)
        {
            _bucket = bucket;
        }

        public string GetId()
        {
            if (_bucket != null)
            {
                return _bucket.Id;
            }
            else
            {
                return _id;
            }
        }

        public PlannerBucket GetBucket(Cmdlet cmdlet, PnPConnection connection, string accessToken, string planId)
        {
            // first try to get the bucket by id
            if (_bucket != null)
            {
                return _bucket;
            }
            else
            {
                try
                {
                    var buckets = PlannerUtility.GetBucketsAsync(cmdlet, connection, accessToken, planId).GetAwaiter().GetResult();
                    if (buckets != null)
                    {
                        PlannerBucket bucket = null;
                        var bucketId = _id ?? _bucket?.Id;
                        if (bucketId != null)
                        {
                            bucket = buckets.FirstOrDefault(b => b.Id == bucketId);
                        }
                        if (bucket == null)
                        {
                            // by name?
                            bucket = buckets.FirstOrDefault(b => b.Name.Equals(_id, StringComparison.OrdinalIgnoreCase));
                        }
                        return bucket;
                    }
                }
                catch (GraphException ex)
                {
                    throw new PSArgumentException(ex.Error.Message);
                }
                return null;
            }
        }
    }
}
