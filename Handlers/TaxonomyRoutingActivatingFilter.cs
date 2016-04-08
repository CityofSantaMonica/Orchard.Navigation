using CSM.Navigation.Models;
using Orchard.Autoroute.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using System.Linq;

namespace CSM.Navigation.Handlers
{
    [OrchardFeature("CSM.Navigation.TaxonomyRouting")]
    public class TaxonomyRoutingActivatingFilter : IContentActivatingFilter
    {
        public void Activating(ActivatingContentContext context)
        {
            if (!context.Definition.Parts.Any(p => p.PartDefinition.Name == typeof(TaxonomyRoutingPart).Name))
            {
                return;
            }

            //ensure we have an AutoroutePart on the content item
            context.Builder.Weld<AutoroutePart>();
        }
    }
}