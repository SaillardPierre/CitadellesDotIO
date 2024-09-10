using Microsoft.AspNetCore.Components;

namespace CitadellesDotIO.DeckAssembly.Components
{
    public abstract class CustomComponent : ComponentBase, ISelfRefreshable
    {
        public abstract void SelfRefresh();
        private bool _shouldRender = true;
        protected override bool ShouldRender()
        {
            return _shouldRender;
        }
        public void PreventRender()
        {
            _shouldRender = false;
        }
        public void AllowRender()
        {
            _shouldRender = true;
        }
    }
}
