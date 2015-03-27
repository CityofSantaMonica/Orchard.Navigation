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
            var viewModel = getViewModel(part);

            return ContentShape(
                "Parts_TableOfContents",
                () => shapeHelper.Parts_TableOfContents(ViewModel: viewModel)
            );
        }

        protected override DriverResult Editor(TableOfContentsPart part, dynamic shapeHelper)
        {
            var viewModel = getViewModel(part);

            return ContentShape(
                "Parts_TableOfContents_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.TableOfContents",
                    Model: viewModel,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(TableOfContentsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new TableOfContentsPartViewModel();
            
            if (updater.TryUpdateModel(viewModel, Prefix, null, null))
            {
                updatePart(part, viewModel);
            };

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

        private static TableOfContentsPartViewModel getViewModel(TableOfContentsPart part)
        {
            var widget = part.As<WidgetPart>();
            var settings = part.Settings.GetModel<TableOfContentsSettings>();

            var viewModel = new TableOfContentsPartViewModel {
                ShowOptIn = settings == null ? true : settings.ShowOptIn,
                Generate = part.Generate,
                RootSelector = part.RootSelector,
                StartLevel = part.StartLevel,
                EndLevel = part.EndLevel,
                Affix = part.Affix,
                MakeTopLink = part.MakeTopLink,
                TopLinkText = part.TopLinkText,
                Name = widget == null || String.IsNullOrEmpty(widget.Name) ? part.ContentItem.Id.ToString() : widget.Name
            };

            return viewModel;
        }

        private static void updatePart(TableOfContentsPart part, TableOfContentsPartViewModel viewModel)
        {
            part.Generate = viewModel.Generate;
            part.RootSelector = viewModel.RootSelector;
            part.StartLevel = viewModel.StartLevel;
            part.EndLevel = viewModel.EndLevel;
            part.Affix = viewModel.Affix;
            part.MakeTopLink = viewModel.MakeTopLink;
            part.TopLinkText = viewModel.TopLinkText;
        }
    }
}