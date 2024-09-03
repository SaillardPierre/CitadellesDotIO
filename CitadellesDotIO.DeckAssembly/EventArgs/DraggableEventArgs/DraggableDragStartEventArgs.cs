namespace CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs
{
    public class DraggableDragStartEventArgs : DraggableBaseEventArgs
    {
        public DraggableDragStartEventArgs(int draggableIndex, string draggableSource)
        {
            DraggableIndex = draggableIndex;
            DraggableSource = draggableSource;
        }
    }
}
