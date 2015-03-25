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

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition(
                typeof(DocumentIndexPart).Name,
                part => part
                    .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition(
                "DocumentIndexWidget", type => type
                    .WithPart(typeof(DocumentIndexPart).Name)
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart", p => p
                        .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false")
                        .WithSetting("DateEditorSettings.ShowDateEditor", "false")
                    )
                    .WithPart("IdentityPart")
                    .DisplayedAs("Table of Contents Widget")
                    .WithSetting("Stereotype", "Widget")
                    .Draftable(false)
            );

            return 2;
        }
    }
}