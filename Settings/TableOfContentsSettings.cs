using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Orchard.Environment.Extensions;

namespace CSM.Navigation.Settings
{
    /// <summary>
    /// Per-content-type settings for an attached Table of Contents part.
    /// </summary>
    [OrchardFeature("CSM.Navigation.TableOfContents")]
    public class TableOfContentsSettings
    {
        public bool OptIn { get; set; }
        public bool Generate { get; set; }
        public bool AllowTitle { get; set; }

        public TableOfContentsSettings()
        {
            //by default allow table of contents generation to be opt-in
            OptIn = true;
            //by default don't generate a table of contents
            Generate = false;
            //by default allow a title to be given to the table of contents
            AllowTitle = true;
        }
    }

    /// <summary>
    /// Driver for the Table of Contents part and type-part settings.
    /// </summary>
    [OrchardFeature("CSM.Navigation.TableOfContents")]
    public class TableOfContentsSettingsDriver : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "TableOfContentsPart")
                yield break;

            var model = definition.Settings.GetModel<TableOfContentsSettings>();

            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name != "TableOfContentsPart")
                yield break;

            var model = new TableOfContentsSettings();
            updateModel.TryUpdateModel(model, "TableOfContentsSettings", null, null);
            builder.WithSetting("TableOfContentsSettings.OptIn", model.OptIn.ToString());
            builder.WithSetting("TableOfContentsSettings.Generate", model.Generate.ToString());
            builder.WithSetting("TableOfContentsSettings.AllowTitle", model.AllowTitle.ToString());
            yield return DefinitionTemplate(model);
        }
    }
}
