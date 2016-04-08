using CSM.Navigation.Models;
using Orchard.Autoroute.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using System;

namespace CSM.Navigation.Handlers
{
    [OrchardFeature("CSM.Navigation.TaxonomyRouting")]
    public class TaxonomyRoutingPartHandler : ContentHandler
    {
        public TaxonomyRoutingPartHandler()
        {
            Filters.Add(new TaxonomyRoutingActivatingFilter());

            OnActivated<TaxonomyRoutingPart>(OnActivatedHandler);
        }

        private void OnActivatedHandler(ActivatedContentContext context, TaxonomyRoutingPart part)
        {
            var autoroute = part.As<AutoroutePart>();
            if (autoroute == null)
            {
                return;
            }

            autoroute.CustomPattern = String.Format("{{Content.Fields.{0}.{1}.TermPath}}/{{Content.Slug}}", typeof(TaxonomyRoutingPart).Name, TaxonomyRoutingPart.RoutingTaxonomyFieldName);
            autoroute.UseCustomPattern = true;
        }
    }
}