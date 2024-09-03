namespace CitadellesDotIO.DeckAssembly.EventArgs.DraggableEventArgs
{
    public abstract class DraggableBaseEventArgs
    {        
        /// <summary>
        /// L'index du draggable dans sa source
        /// </summary>
        public int DraggableIndex { get; set; }
        /// <summary>
        /// L'id de la source du draggable 
        /// (utilisé avec nameof(monListItemContainer) pour identifier sa référence d'objet)
        /// </summary>
        public string DraggableSource { get; set; }
    }
}
