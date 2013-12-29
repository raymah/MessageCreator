using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace MessageCreator.WebApi.Controllers.Hal.Resources
{
    public class DocumentListRepresentation : PagedRepresentationList<DocumentRepresentation>
    {
        public DocumentListRepresentation(IList<DocumentRepresentation> beers, int totalResults, int totalPages, int page, Link uriTemplate) :
            base(beers, totalResults, totalPages, page, uriTemplate, null)
        { }
        public DocumentListRepresentation(IList<DocumentRepresentation> beers, int totalResults, int totalPages, int page, Link uriTemplate, object uriTemplateSubstitutionParams) :
            base(beers, totalResults, totalPages, page, uriTemplate, uriTemplateSubstitutionParams)
        { }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
        }
    }
}