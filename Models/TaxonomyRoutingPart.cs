using Orchard.ContentManagement;
using Orchard.Taxonomies.Fields;

namespace CSM.Navigation.Models
{
    public class TaxonomyRoutingPart : ContentPart
    {
        public static string RoutingTaxonomyFieldName
        {
            get { return "RoutingTaxonomy"; }
        }

        public TaxonomyField RoutingTaxonomyField
        {
            get { return this.Get(typeof(TaxonomyField), RoutingTaxonomyFieldName) as TaxonomyField; }
        }
    }
}