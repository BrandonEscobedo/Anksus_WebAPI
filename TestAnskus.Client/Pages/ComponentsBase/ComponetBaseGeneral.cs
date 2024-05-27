using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace TestAnskus.Client.Pages.ComponentsBase
{
    public class ComponetBaseGeneral:ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sqr = 0;
            builder.OpenElement(++sqr, "div");
            base.BuildRenderTree(builder);
        }
    }
}
