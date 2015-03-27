using CSM.Navigation.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace CSM.Navigation.Data
{
    public class DividerMenuItemMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(
                typeof(DividerMenuItemPart).Name,
                part => part
                    .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition(
                "DividerMenuItem", type => type
                    .WithPart(typeof(DividerMenuItemPart).Name)
                    .WithPart("MenuPart")
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
                    .DisplayedAs("Divider Menu Item")
                    .WithSetting("Description", "Provides a Bootstrapified divider item.")
                    .WithSetting("Stereotype", "MenuItem")
                    .Draftable(false)
                    .Creatable(false)
            );

            return 1;
        }
    }
}