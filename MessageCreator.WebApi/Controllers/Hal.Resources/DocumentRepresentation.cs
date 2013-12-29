using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace MessageCreator.WebApi.Controllers.Hal.Resources
{
    public class DocumentRepresentation : Representation
    {
        public DocumentRepresentation()
        {
            Rel = new Link("Document", "/api/Document/{id}").Rel;
        }

        public string Id { get; set; }

        protected override void CreateHypermedia()
        {
            Href = new Link("Document", "/api/Document/{id}").CreateLink(this.Id).Href;
            Links.Add(new Link { Href = Href, Rel = "self" });

            Links.Add(new Link("Document", "/api/DownloadDocument/{id}").CreateLink(new { id=Id }));
        }
    }
}