using System;

namespace CSM.Navigation.ViewModels
{
    public class TableOfContentsPartViewModel
    {
        public string RootSelector { get; set; }

        public int StartLevel { get; set; }
        public string StartLevelSelector
        {
            get { return String.Format("h{0}", StartLevel); }
        }

        public int EndLevel { get; set;}
        public string EndLevelSelector
        {
            get { return String.Format("h{0}", EndLevel); }
        }

        public string Name { get; set; }

        public string ContainerId
        {
            get { return String.Format("toc", Name); }
        }
    }
}
