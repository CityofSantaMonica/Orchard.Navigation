using CSM.Navigation.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace CSM.Navigation
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            // Define the DividerMenuItemPart
            ContentDefinitionManager.AlterPartDefinition(
                typeof(DividerMenuItemPart).Name,
                part => part
                    //this part shouldn't be attachable to other types from the UI
                    //we're going to create a type below and attach it programmatically
                    .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition(
                "DividerMenuItem",
                config => config
                    //our custom part
                    .WithPart(typeof(DividerMenuItemPart).Name)
                    //required by the Navigation module
                    .WithPart("MenuPart")
                    //good idea for all types to have a CommonPart
                    .WithPart("CommonPart")
                    //required for Import/Export
                    .WithPart("IdentityPart")
                    //the name given to this menu item type in the Navigation editor
                    .DisplayedAs("Divider")
                    //the description given to this menu item type in the Navigation editor
                    .WithSetting("Description", "Provides a Bootstrapified divider item.")
                    //required by the Navigation module
                    .WithSetting("Stereotype", "MenuItem")
                    //users shouldn't be able to draft these or create them outside the context of a menu
                    .Draftable(false)
                    .Creatable(false)
            );

            return 1;
        }
    }
}