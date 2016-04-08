using CSM.Navigation.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Taxonomies.Fields;
using Orchard.Taxonomies.Services;
using Orchard.Tokens;
using System;
using System.Linq;

namespace CSM.Navigation.Tokens
{
    [OrchardFeature("CSM.Navigation.TaxonomyRouting")]
    public class TaxonomyRoutingTokens : ITokenProvider
    {
        private readonly ITaxonomyService _taxonomyService;

        public TaxonomyRoutingTokens(ITaxonomyService taxonomyService)
        {
            _taxonomyService = taxonomyService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context)
        {
            // Usage:
            // Content.Fields.TaxonomyRoutingPart.RoutingTaxonomy.TermPath -> 'parent-category/child-category/grandchild-category/'

            context.For("TaxonomyField", T("Taxonomy Field"), T("Tokens for Taxonomy Fields"))
                   .Token("TermPath", T("TermPath"), T("The term path associated with the {0} field.", TaxonomyRoutingPart.RoutingTaxonomyFieldName));
        }

        public void Evaluate(EvaluateContext context)
        {
            context.For<TaxonomyField>("TaxonomyField")
                   .Token("TermPath", field =>
                   {
                       var term = field.Terms.First();
                       var taxonomy = _taxonomyService.GetTaxonomy(term.TaxonomyId);

                       return term.Slug.ToLower().Replace(taxonomy.Name.ToLower() + "/", String.Empty);
                   });
        }
    }
}