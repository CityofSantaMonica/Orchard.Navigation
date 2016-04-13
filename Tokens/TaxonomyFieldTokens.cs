using Orchard.Localization;
using Orchard.Taxonomies.Fields;
using Orchard.Taxonomies.Services;
using Orchard.Tokens;
using System;
using System.Linq;

namespace CSM.Navigation.Tokens
{
    public class TaxonomyFieldTokens : ITokenProvider
    {
        public static string TermPath { get { return "TermPath"; } }
        public static string TermPathNoTaxonomy { get { return "TermPath.NoTaxonomy"; } }

        private readonly ITaxonomyService _taxonomyService;

        public TaxonomyFieldTokens(ITaxonomyService taxonomyService)
        {
            _taxonomyService = taxonomyService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context)
        {
            // Usage:
            // Content.Fields.PartWithTaxonomyField.TaxonomyFieldName.TermPath -> 'my-taxonomy/parent-category/child-category/grandchild-category/'
            // Content.Fields.PartWithTaxonomyField.TaxonomyFieldName.TermPath.NoTaxonomy -> 'parent-category/child-category/grandchild-category/'

            context.For("TaxonomyField", T("Taxonomy Field"), T("Tokens for Taxonomy Fields"))
                   .Token(TermPath, T(TermPath), T("The complete term path associated with this field's selected term."))
                   .Token(TermPathNoTaxonomy, T(TermPathNoTaxonomy), T("The term path associated with this field's selected term, minus the term's taxonomy name"));
        }

        public void Evaluate(EvaluateContext context)
        {
            context.For<TaxonomyField>("TaxonomyField")
                   .Token(TermPath, field => Evaluate(field, false))
                   .Token(TermPathNoTaxonomy, field => Evaluate(field, true));
        }

        private string Evaluate(TaxonomyField field, bool removeTaxonomy)
        {
            var term = field.Terms.FirstOrDefault();

            if (term == null)
                return null;

            var taxonomy = _taxonomyService.GetTaxonomy(term.TaxonomyId);

            string value = term.Slug.ToLower();
            return removeTaxonomy ? value.Replace(taxonomy.Slug.ToLower() + "/", String.Empty) : value;
        }
    }
}