using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace CSM.Navigation.Models
{
    public class TableOfContentsPart : ContentPart
    {
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
        public string RootSelector
        {
            get { return this.Retrieve(x => x.RootSelector, ".zone-content > :first-child"); }
            set { this.Store(x => x.RootSelector, value); }
        }
    }
}