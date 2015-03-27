using System;
using Orchard.Environment.Extensions;

namespace CSM.Navigation.ViewModels
{
    [OrchardFeature("CSM.Navigation.TableOfContents")]
    public class TableOfContentsPartViewModel
    {
        public bool Generate { get; set; }

        public string RootSelector { get; set; }

        public int StartLevel { get; set; }

        public int EndLevel { get; set;}

        public bool Affix { get; set; }

        public bool MakeTopLink { get; set; }

        public string TopLinkText { get; set; }

        public string Name { get; set; }

        public string ContainerId
        {
            get { return String.Format("toc-{0}", Name); }
        }
    }
}
