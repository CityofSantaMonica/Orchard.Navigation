using Orchard.UI.Resources;

namespace CSM.Navigation
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            //the library plugin
            manifest
                .DefineScript("toc")
                    .SetUrl("lib/toc.min.js", "lib/toc.js")
                    .SetVersion("0.3.2")
                    .SetDependencies("jQuery");

            //this module's defined plugin, depends on the library plugin
            manifest
                .DefineScript("tableOfContents")
                    .SetUrl("tableOfContents.js")
                    .SetDependencies("toc", "jQuery");

            //some default styling
            manifest
                .DefineStyle("tableOfContents")
                    .SetUrl("tableOfContents.css");
        }
    }
}