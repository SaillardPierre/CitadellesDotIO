using CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs;
using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Exceptions;
using CitadellesDotIO.DeckAssembly.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace CitadellesDotIO.DeckAssembly.Components
{
    public abstract class DraggableContainerComponent : ComponentBase
    {
        protected string DraggablesClassName { get; set; }
        protected string DropzonesClassName { get; set; }
        public DraggableContainerComponent(string draggablesClassName, string dropzonesClassName)
        {
            DraggablesClassName = draggablesClassName;
            DropzonesClassName = dropzonesClassName;
        }

        [Inject]
        private IJSRuntime JS { get; set; }

        protected async void InitJS()
        {
            await JS.InvokeVoidAsync("setupDraggables", "." + DraggablesClassName, "." + DropzonesClassName, BlazorComponent);
            await JS.InvokeVoidAsync("setupDropzones", "." + DropzonesClassName, BlazorComponent);
        }
        protected DotNetObjectReference<DraggableContainerComponent>? BlazorComponent { get; set; }
        protected Card? DraggedCard { get; set; }
        protected Card? HoveredCard { get; set; }
        protected CardItemList? HoveredCardSource { get; set; }
        protected CardItemList? CurrentSource { get; set; }
        protected CardItemList? CurrentHoverTarget { get; set; }
        protected int? FutureDropIndex { get; set; }

        #region Evenements sur le draggable
        // TODO : Trouver un moyen d'avoir l'event hover sur le draggable
        [JSInvokable(nameof(OnDraggableHoverStart))]
        public virtual async Task OnDraggableHoverStart(DraggableHoverStartEventArgs args)
        {
            StateException.ThrowIfNotNull(DraggedCard);

            ArgumentNullException.ThrowIfNull(HoveredCardSource);

            HoveredCard = HoveredCardSource.Cards[args.DraggableIndex];
            HoveredCard.Reset();
            HoveredCard.IsHovered = true;
            HoveredCard.ZIndex = CardParameters.DraggedCardZIndex;

            HoveredCardSource.SetOverlapFromIndex(args.DraggableIndex);
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableHoverEnd))]
        public async Task OnDraggableHoverEnd(DraggableHoverEndEventArgs args)
        {
            StateException.ThrowIfNotNull(DraggedCard);

            ArgumentNullException.ThrowIfNull(HoveredCard);
            ArgumentNullException.ThrowIfNull(HoveredCardSource);

            HoveredCard.IsHovered = false;
            HoveredCard = null;
            HoveredCardSource.SetOverlapFromIndex(0);
            HoveredCardSource = null;
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDragStart))]
        public virtual async Task OnDraggableDragStart(DraggableDragStartEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(CurrentSource);

            DraggedCard = CurrentSource.Cards[args.DraggableIndex];
            DraggedCard.IsDragged = true;
            FutureDropIndex = args.DraggableIndex;
        }
        [JSInvokable(nameof(OnDraggableDragEnd))]
        public async Task OnDraggableDragEnd(DraggableDragEndEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(DraggedCard);
            ArgumentNullException.ThrowIfNull(CurrentSource);

            DraggedCard.Reset();
            DraggedCard = null;
            CurrentSource.Reset();
            CurrentSource = null;
            if (CurrentHoverTarget is not null)
            {
                CurrentHoverTarget.Reset();
                CurrentHoverTarget = null;
            }
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableMove))]
        public async Task OnDraggableMove(DraggableMoveEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(DraggedCard);

            DraggedCard.UpdatePosition(args.DragMoveDirection);
            FutureDropIndex = DragManager.GetFutureIndex(args);
            if (CurrentHoverTarget is not null)
            {
                if (FutureDropIndex.HasValue)
                {
                    CurrentHoverTarget.HandleHovering(FutureDropIndex.Value, args.DragHoverTarget);
                }
                CurrentHoverTarget.HandleOverlap();
            }
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDrop))]
        public virtual async Task OnDraggableDrop(DraggableDropEventArgs args)
        {
            InvalidEnumException.ThrowIfEqual(args.DropEventSource, DropEventSource.Outside);
            ArgumentNullException.ThrowIfNull(DraggedCard);
            ArgumentNullException.ThrowIfNull(CurrentSource);
            ArgumentNullException.ThrowIfNull(CurrentHoverTarget);

            if (args.DropEventSource == DropEventSource.Self)
            {
                CurrentSource.Cards.PutBackAtIndex(DraggedCard, FutureDropIndex);
            }
            else if (args.DropEventSource == DropEventSource.Target)
            {
                CurrentSource.Cards.Remove(DraggedCard);
                CurrentHoverTarget.Cards.InsertOrAppend(DraggedCard, FutureDropIndex);
                CurrentHoverTarget.Reset();
            }
            FutureDropIndex = null;
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDropzoneEnter))]
        public virtual async Task OnDraggableDropzoneEnter(DraggableDropzoneEnterEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(CurrentHoverTarget);
            InvalidEnumException.ThrowIfEqual(args.DragHoverTarget, DragHoverTarget.None);

            CurrentHoverTarget.IsHovered = true;
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDropzoneLeave))]
        public async Task OnDraggableDropzoneLeave(DraggableDropzoneLeaveEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(CurrentHoverTarget);
            ArgumentNullException.ThrowIfNull(DraggedCard);

            CurrentHoverTarget.ResetExceptCard(DraggedCard);
            CurrentHoverTarget = null;
            StateHasChanged();
        }
        #endregion
    }
}
