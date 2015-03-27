using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace CSM.Navigation.Models
{
    [OrchardFeature("CSM.Navigation.TableOfContents")]
    public class TableOfContentsPart : ContentPart
    {
        [Required]
        public string RootSelector
        {
            get { return this.Retrieve(x => x.RootSelector, ".zone-content > :first-child"); }
            set { this.Store(x => x.RootSelector, value); }
        }

        [Required]
        public int StartLevel
        {
            get { return this.Retrieve(x => x.StartLevel, 2); }
            set { this.Store(x => x.StartLevel, value); }
        }

        [Required]
        public int EndLevel
        {
            get { return this.Retrieve(x => x.EndLevel, 3); }
            set { this.Store(x => x.EndLevel, value); }
        }

        [Required]
        public bool Affix
        {
            get { return this.Retrieve(x => x.Affix, false); }
            set { this.Store(x => x.Affix, value); }
        }

        [Required]
        public bool MakeTopLink
        {
            get { return this.Retrieve(x => x.MakeTopLink, false); }
            set { this.Store(x => x.MakeTopLink, value); }
        }

        [Required]
        public string TopLinkText
        {
            get { return this.Retrieve(x => x.TopLinkText, "Back to top"); }
            set { this.Store(x => x.TopLinkText, value); }
        }
    }
}