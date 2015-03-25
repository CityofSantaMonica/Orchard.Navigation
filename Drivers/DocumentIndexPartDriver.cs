using System.Xml.Linq;
using CSM.Navigation.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;

namespace CSM.Navigation.Drivers
{
    public class DocumentIndexPartDriver : ContentPartDriver<DocumentIndexPart>
    {
        protected override string Prefix
        {
            get { return "DocumentIndex"; }
        }

        protected override DriverResult Editor(DocumentIndexPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_DocumentIndex_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.DocumentIndex",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(DocumentIndexPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(DocumentIndexPart part, ImportContentContext context)
        {
            var element = context.Data.Element(part.PartDefinition.Name);

            if (element == null) return;

            var defaultPart = new DocumentIndexPart();

            part.StartLevel = element.Attr<int?>("StartLevel") ?? defaultPart.StartLevel;
            part.EndLevel = element.Attr<int?>("EndLevel") ?? defaultPart.EndLevel;
            part.RootSelector = element.Attr<string>("RootSelector") ?? defaultPart.RootSelector;
        }

        protected override void Exporting(DocumentIndexPart part, ExportContentContext context)
        {
            var element = context.Element(part.PartDefinition.Name);

            element.Add(new XAttribute("StartLevel", part.StartLevel));
            element.Add(new XAttribute("EndLevel", part.EndLevel));
            element.Add(new XAttribute("RootSelector", part.RootSelector));
        }
    }
}