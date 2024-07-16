using CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs;
using CitadellesDotIO.DeckAssembly.EventArgs.Enums;
using CitadellesDotIO.DeckAssembly.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CitadellesDotIO.DeckAssembly.Components
{
    public abstract class DraggableContainerComponent : ComponentBase
    {
        // TODO : S'assurer du nommage, sur le pickpool c'est blazorComponent
        protected DotNetObjectReference<DraggableContainerComponent>? BlazorComponent { get; set; }
        protected Card? CurrentCard { get; set; }
        protected CardItemList? CurrentSource { get; set; }
        protected CardItemList? CurrentHoverTarget { get; set; }

        protected int? FutureDropIndex { get; set; }
        #region Evenements sur le draggable
        // TODO : Trouver un moyen d'avoir l'event hover sur le draggable
        [JSInvokable(nameof(OnDraggableHoverStart))]
        public async Task OnDraggableHoverStart() { }
        [JSInvokable(nameof(OnDraggableHoverEnd))]
        public async Task OnDraggableHoverEnd() { }
        [JSInvokable(nameof(OnDraggableDragStart))]
        public virtual async Task OnDraggableDragStart(DraggableDragStartEventArgs args)
        {
            // On force a définir la source avant d'appeler celle ci
            ArgumentNullException.ThrowIfNull(CurrentSource);
            CurrentCard = CurrentSource.Cards[args.DraggableIndex];
            FutureDropIndex = args.DraggableIndex;
        }
        [JSInvokable(nameof(OnDraggableDragEnd))]
        public async Task OnDraggableDragEnd(DraggableDragEndEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(CurrentCard);
            CurrentCard.Reset();
            CurrentCard = null;
            ArgumentNullException.ThrowIfNull(CurrentSource);
            CurrentSource.Reset();
            CurrentSource = null;
            ArgumentNullException.ThrowIfNull(CurrentHoverTarget);
            CurrentHoverTarget.Reset();
            CurrentHoverTarget = null;
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableMove))]
        public async Task OnDraggableMove(DraggableMoveEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(CurrentCard);
            CurrentCard.UpdatePosition(args.DragMoveDirection);
            FutureDropIndex = DragManager.GetFutureIndex(args);
            /// TODO : du code sur current hovertarget ici quand implémenté
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
            ArgumentNullException.ThrowIfNull(CurrentCard);
            // A revoir
            if (args.DropEventSource == DropEventSource.Outside)
            {
                return;
            }
            ArgumentNullException.ThrowIfNull(CurrentSource);
            ArgumentNullException.ThrowIfNull(CurrentHoverTarget);
            if (args.DropEventSource == DropEventSource.Self)
            {
                CurrentSource.Cards.PutBackAtIndex(CurrentCard, FutureDropIndex);
            }
            else if (args.DropEventSource == DropEventSource.Target)
            {
                CurrentSource.Cards.Remove(CurrentCard);
                CurrentHoverTarget.Cards.InsertOrAppend(CurrentCard, FutureDropIndex);
                CurrentHoverTarget.Reset();
            }
            FutureDropIndex = null;
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDropzoneEnter))]
        public virtual async Task OnDraggableDropzoneEnter(DraggableDropzoneEnterEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(CurrentHoverTarget);
            if (args.DragHoverTarget == EventArgs.Enums.DragHoverTarget.None)
            {
                throw new ArgumentOutOfRangeException(args.DragHoverTarget.ToString());
            }
            CurrentHoverTarget.IsHovered = true;
            StateHasChanged();
        }
        [JSInvokable(nameof(OnDraggableDropzoneLeave))]
        public async Task OnDraggableDropzoneLeave(DraggableDropzoneLeaveEventArgs args)
        {
            // TODO : Bizare ici CurrentHoverTarget est null sur une autre dropzone que la idlehand
            ArgumentNullException.ThrowIfNull(CurrentHoverTarget);
            ArgumentNullException.ThrowIfNull(CurrentCard);
            CurrentHoverTarget.ResetExceptCard(CurrentCard);
            CurrentHoverTarget = null;
            StateHasChanged();
        }
        #endregion
    }
}
