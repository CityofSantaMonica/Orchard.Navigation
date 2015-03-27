using CSM.Navigation.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Security;

namespace CSM.Navigation.Drivers
{
    public class DividerMenuItemPartDriver : ContentPartDriver<DividerMenuItemPart>
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IWorkContextAccessor workContextAccessor;

        public DividerMenuItemPartDriver(IAuthorizationService authorizationService,
                                         IWorkContextAccessor workContextAccessor)
        {
            this.authorizationService = authorizationService;
            this.workContextAccessor = workContextAccessor;
        }

        protected override string Prefix
        {
            get { return "DividerMenuItem"; }
        }

        protected override DriverResult Editor(DividerMenuItemPart part, dynamic shapeHelper)
        {
            var currentUser = workContextAccessor.GetContext().CurrentUser;
            if (!authorizationService.TryCheckAccess(Orchard.Core.Navigation.Permissions.ManageMenus, currentUser, part))
                return null;

            return ContentShape(
                "Parts_DividerMenuItem_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.DividerMenuItem",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(DividerMenuItemPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var currentUser = workContextAccessor.GetContext().CurrentUser;
            if (!authorizationService.TryCheckAccess(Orchard.Core.Navigation.Permissions.ManageMenus, currentUser, part))
                return null;

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(DividerMenuItemPart part, Orchard.ContentManagement.Handlers.ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("ShowDivider", part.ShowDivider);
            context.Element(part.PartDefinition.Name).SetAttributeValue("ShowHeader", part.ShowHeader);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Header", part.Header);
        }

        protected override void Importing(DividerMenuItemPart part, Orchard.ContentManagement.Handlers.ImportContentContext context)
        {
            context.ImportAttribute(part.PartDefinition.Name, "ShowDivider", x => part.ShowDivider = bool.Parse(x));
            context.ImportAttribute(part.PartDefinition.Name, "ShowHeader", x => part.ShowHeader = bool.Parse(x));
            context.ImportAttribute(part.PartDefinition.Name, "Header", x => part.Header = x);
        }
    }
}