using System;
using System.Xml.Linq;
using CSM.Navigation.Models;
using CSM.Navigation.Settings;
using CSM.Navigation.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Widgets.Models;

namespace CSM.Navigation.Drivers
{
    [OrchardFeature("CSM.Navigation.TableOfContents")]
    public class TableOfContentsPartDriver : ContentPartDriver<TableOfContentsPart>
    {
        protected override string Prefix
        {
            get { return "TableOfContents"; }
        }

        protected override DriverResult Display(TableOfContentsPart part, string displayType, dynamic shapeHelper)
        {
            string name = part.As<WidgetPart>().Name;
            var settings = part.Settings.GetModel<TableOfContentsSettings>();

            var viewModel = new TableOfContentsPartViewModel {
                Generate = settings.Generate,
                RootSelector = part.RootSelector,
                StartLevel = part.StartLevel,
                EndLevel = part.EndLevel,
                Affix = part.Affix,
                MakeTopLink = part.MakeTopLink,
                TopLinkText = part.TopLinkText,
                Name = String.IsNullOrEmpty(name) ? part.ContentItem.Id.ToString() : name
            };

            return ContentShape(
                "Parts_TableOfContents",
                () => shapeHelper.Parts_TableOfContents(ViewModel: viewModel)
            );
        }

        protected override DriverResult Editor(TableOfContentsPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_TableOfContents_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.TableOfContents",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(TableOfContentsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(TableOfContentsPart part, ImportContentContext context)
        {
            var element = context.Data.Element(part.PartDefinition.Name);

            if (element == null) return;

            var defaultPart = new TableOfContentsPart();

            part.StartLevel = element.Attr<int?>("StartLevel") ?? defaultPart.StartLevel;
            part.EndLevel = element.Attr<int?>("EndLevel") ?? defaultPart.EndLevel;
            part.RootSelector = element.Attr<string>("RootSelector") ?? defaultPart.RootSelector;
        }

        protected override void Exporting(TableOfContentsPart part, ExportContentContext context)
        {
            var element = context.Element(part.PartDefinition.Name);

            element.Add(new XAttribute("StartLevel", part.StartLevel));
            element.Add(new XAttribute("EndLevel", part.EndLevel));
            element.Add(new XAttribute("RootSelector", part.RootSelector));
        }
    }
}