using System;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace CSM.Navigation.Models
{
    public class DividerMenuItemPart : ContentPart
    {
        public bool ShowDivider
        {
            get { return Retrieve<bool>("ShowDivider"); }
            set { Store<bool>("ShowDivider", value); }
        }

        public bool ShowHeader
        {
            get { return Retrieve<bool>("ShowHeader"); }
            set { Store<bool>("ShowHeader", value); }
        }

        public string Header
        {
            get { return Retrieve<string>("Header"); }
            set { Store<string>("Header", value); }
        }
    }
}