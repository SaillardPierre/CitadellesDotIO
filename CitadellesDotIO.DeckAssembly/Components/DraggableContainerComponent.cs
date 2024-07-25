﻿using CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs;
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
        protected DraggableContainerComponent(string draggablesClassName, string dropzonesClassName)
        {
            DraggablesClassName = draggablesClassName;
            DropzonesClassName = dropzonesClassName;
        }

        [Inject]
        private IJSRuntime JS { get; set; }

        protected async Task InitJS()
        {
            await InitDraggables();
            await JS.InvokeVoidAsync("setupDropzones", "." + DropzonesClassName, BlazorComponent);
        }
        protected async Task InitDraggables()
        {
            await JS.InvokeVoidAsync("setupDraggables", "." + DraggablesClassName, "." + DropzonesClassName, BlazorComponent);
        }

        protected DotNetObjectReference<DraggableContainerComponent>? BlazorComponent { get; set; }
        protected Card? DraggedCard { get; set; }
        protected CardItemList? DraggedCardSouce { get; set; }
        protected Card? HoveredCard { get; set; }
        protected CardItemList? HoveredCardSource { get; set; }
        protected CardItemList? DropzoneHoverSource { get; set; }
        protected int? FutureDropIndex { get; set; }

        #region Evenements sur le draggable
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
            //StateException.ThrowIfNotNull(DraggedCard);

            ArgumentNullException.ThrowIfNull(HoveredCard);
            ArgumentNullException.ThrowIfNull(HoveredCardSource);

            HoveredCard.IsHovered = false;
            HoveredCard = null;
            HoveredCardSource = null;
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDragStart))]
        public virtual async Task OnDraggableDragStart(DraggableDragStartEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(DraggedCardSouce);

            DraggedCard = DraggedCardSouce.Cards[args.DraggableIndex];
            DraggedCard.IsDragged = true;
            FutureDropIndex = args.DraggableIndex;
        }
        [JSInvokable(nameof(OnDraggableDragEnd))]
        public async Task OnDraggableDragEnd(DraggableDragEndEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(DraggedCard);
            ArgumentNullException.ThrowIfNull(DraggedCardSouce);

            DraggedCard.Reset();
            DraggedCard = null;
            DraggedCardSouce.Reset();
            DraggedCardSouce = null;
            if (DropzoneHoverSource is not null)
            {
                DropzoneHoverSource.Reset();
                DropzoneHoverSource = null;
            }
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableMove))]
        public async Task OnDraggableMove(DraggableMoveEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(DraggedCard);

            DraggedCard.UpdatePosition(args.DragMoveDirection);
            FutureDropIndex = DragManager.GetFutureIndex(args.DragHoverTarget, DraggedCard.Position, DropzoneHoverSource?.Cards.Select(x => x.Position));
            if (DropzoneHoverSource is not null && FutureDropIndex.HasValue)
            {
                DropzoneHoverSource.SetOverlapFromIndex(FutureDropIndex.Value, args.DragHoverTarget, DraggedCard.Index);
            }
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDrop))]
        public virtual async Task OnDraggableDrop(DraggableDropEventArgs args)
        {
            InvalidEnumException.ThrowIfEqual(args.DropEventSource, DropEventSource.Outside);
            ArgumentNullException.ThrowIfNull(DraggedCard);
            ArgumentNullException.ThrowIfNull(DraggedCardSouce);
            ArgumentNullException.ThrowIfNull(DropzoneHoverSource);

            if (args.DropEventSource == DropEventSource.Self)
            {
                DraggedCardSouce.Cards.PutBackAtIndex(DraggedCard, FutureDropIndex);
            }
            else if (args.DropEventSource == DropEventSource.Target)
            {
                DraggedCardSouce.Cards.Remove(DraggedCard);
                DropzoneHoverSource.Cards.InsertOrAppend(DraggedCard, FutureDropIndex);

            }
            FutureDropIndex = null;
            StateHasChanged();
            //InitDraggables();
        }
        [JSInvokable(nameof(OnDraggableDropzoneEnter))]
        public virtual async Task OnDraggableDropzoneEnter(DraggableDropzoneEnterEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(DropzoneHoverSource);
            InvalidEnumException.ThrowIfEqual(args.DragHoverTarget, DragHoverTarget.None);

            DropzoneHoverSource.SetHoveredState();
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDropzoneLeave))]
        public async Task OnDraggableDropzoneLeave(DraggableDropzoneLeaveEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(DropzoneHoverSource);
            ArgumentNullException.ThrowIfNull(DraggedCard);

            DropzoneHoverSource.ResetExceptCard(DraggedCard);
            DropzoneHoverSource = null;
            StateHasChanged();
        }
        #endregion
    }
}
