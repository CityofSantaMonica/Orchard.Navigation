using CSM.Navigation.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace CSM.Navigation.Data
{
    [OrchardFeature("CSM.Navigation.TableOfContents")]
    public class TableOfContentsMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(
                typeof(TableOfContentsPart).Name,
                part => part
                    .Attachable()
                    .WithDescription("Enables generation of a table of contents.")
            );

            ContentDefinitionManager.AlterTypeDefinition(
                "TableOfContentsWidget", type => type
                    .WithPart(typeof(TableOfContentsPart).Name, p => p
                        .WithSetting("TableOfContentsSettings.OptIn", "false")
                        .WithSetting("TableOfContentsSettings.Generate", "true")
                        .WithSetting("TableOfContentsSettings.AllowTitle", "false")
                    )
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

            return 1;
        }
    }
}
