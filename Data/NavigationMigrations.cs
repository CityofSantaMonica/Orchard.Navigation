using CSM.Navigation.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentPicker.Fields;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace CSM.Navigation.Data
{
    public class NavigationMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(
                typeof(LandingPagePart).Name,
                part => part
                    .Attachable(true)
                    .WithDescription("Confgures the type as a landing page, with content coming from one or more other items.")
                    .WithField("LandingContent", field => field
                        .OfType(typeof(ContentPickerField).Name)
                        .WithDisplayName("Landing Content")
                        .WithSetting("ContentPickerFieldSettings.Required", "False")
                        .WithSetting("ContentPickerFieldSettings.Multiple", "True")
                        .WithSetting("ContentPickerFieldSettings.ShowContentTab", "True")
                    )
            );

            return 1;
        }
    }
}