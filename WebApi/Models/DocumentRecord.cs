using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageCreator.WebApi.Models
{
    public class DocumentRecord
    {
        public DocumentRecord()
        {
        }

        public DocumentRecord(string p)
        {
            var record = p.Split('_');
            DocumentHandle = long.Parse(record[0]);
            RepositoryType = record[1];
        }

        public long DocumentHandle { get; set; }
        public string RepositoryType { get; set; }

        public override string ToString()
        {
            return string.Format("{0}_{1}", DocumentHandle, RepositoryType);
        }
    }
}