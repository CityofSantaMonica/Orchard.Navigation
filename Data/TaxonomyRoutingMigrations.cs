using CSM.Navigation.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using Orchard.Taxonomies.Fields;

namespace CSM.Navigation.Data
{
    [OrchardFeature("CSM.Navigation.TaxonomyRouting")]
    public class TaxonomyRoutingMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(
                typeof(TaxonomyRoutingPart).Name,
                part => part
                    .Attachable()
                    .WithDescription("Enables Autorouting by Taxonomy.")
                    .WithField("RoutingTaxonomy", field => field
                        .OfType(typeof(TaxonomyField).Name)
                        .WithDisplayName("Routing Taxonomy")
                        .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                        .WithSetting("TaxonomyFieldSettings.Required", "True")
                        .WithSetting("TaxonomyFieldSettings.SingleChoice", "True")
                        .WithSetting("TaxonomyFieldSettings.Autocomplete", "False")
                        .WithSetting("TaxonomyFieldSettings.AllowCustomTerms", "False")
                    )
                );

            return 1;
        }
    }
}