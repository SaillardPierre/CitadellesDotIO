function getUpperTopCoordinatesForList(parentElement, className) {
    var elements = parentElement.getElementsByClassName(className);
    var coordinatesList = [];

    for (var i = 0; i < elements.length; i++) {
        coordinatesList.push(getUpperTopCoordinates(elements[i]));
    }

    return coordinatesList;
}

function getUpperTopCoordinates(element) {
    var rect = element.getBoundingClientRect();
    var x = rect.left + window.pageXOffset;
    var y = rect.top + window.pageYOffset;
    return { x: x, y: y };
}

function applyOnCardMove(event, dropzoneClassName, dragManager, blazorComponent) {
    const draggable = event.target;
    const pickIndex = parseInt(draggable.dataset.index);
    const draggablePosition = getUpperTopCoordinates(draggable);
    const draggableMovementPosition = { x: event.dx, y: event.dy };
    const sourceDropzone = event.target.closest(dropzoneClassName);
    const pickSource = sourceDropzone.id;
    const sourceNeighboursPositions = getUpperTopCoordinatesForList(sourceDropzone, 'pickPoolDraggable');
    var targetNeighboursPositions = null;
    var dragHoverTarget = 0; // DragHoverTarget.None
    var dropzone = document.querySelector('.drop-available');
    if (dropzone) {
        if (sourceDropzone != dropzone) {
            dragHoverTarget = 2; // DragHoverTarget.Target
            targetNeighboursPositions = getUpperTopCoordinatesForList(dropzone, 'pickPoolDraggable')
        }
        else dragHoverTarget = 1; // DragHoverTarget.Self        
    }
    const onDragMoveEventArgs = {
        pickIndex: pickIndex,
        pickSource: pickSource,
        dragHoverTarget: dragHoverTarget,
        draggablePosition: draggablePosition,
        draggableMovementPosition: draggableMovementPosition,
        sourceNeighboursPositions: sourceNeighboursPositions,
        targetNeighboursPositions: targetNeighboursPositions
    }
    blazorComponent.invokeMethod('OnDragMove', onDragMoveEventArgs);
}
async function pickPoolDraggables(className, dropzoneClassName, blazorComponent, dragManager) {
    interact(className).draggable({
        intertia: true,
        listeners: {
            move(event) {
                console.log("dragmove");
                applyOnCardMove(event, dropzoneClassName, dragManager, blazorComponent);
            },
            start(event) {

            },
            end(event) {
                console.log("dragend");
                const draggable = event.target;
                const pickIndex = parseInt(draggable.dataset.index);
                const sourceDropzone = event.target.closest(dropzoneClassName);
                const pickSource = sourceDropzone.id;
                const onDragEndEventArgs = {
                    pickIndex: pickIndex,
                    pickSource: pickSource
                }
                blazorComponent.invokeMethod('OnDragEndAsync', onDragEndEventArgs);
            }
        }
    })
};

function pickPoolDropzones(className, blazorComponent) {
    interact(className)
        .dropzone({
            ondragleave: function (event) {
                var dropzone = event.target;
                var draggable = event.relatedTarget;
                var sourceDropzone = event.relatedTarget.closest(className);
                //console.log(draggable.id + ' was moved out of ' + dropzone.id);
                event.target.classList.remove('drop-available');
            },
            ondragenter: function (event) {
                var dropzone = event.target;
                var draggable = event.relatedTarget;
                var sourceDropzone = event.relatedTarget.closest(className);
                //console.log(draggable.id + ' was moved into ' + dropzone.id + ' from ' + sourceDropzone.id);

                sourceDropzone.classList.remove('drop-available');
                // Ajout d'une classe récupérée plus tard pour savoir si on hover
                event.target.classList.add('drop-available');
            },
            ondrop: function (event) {
                const dropzone = event.target;
                const draggable = event.relatedTarget;
                const pickIndex = parseInt(draggable.dataset.index);
                const sourceDropzone = draggable.closest(className);
                const pickSource = sourceDropzone.id;
                //console.log(draggable.id + ' was dropped into ' + dropzone.id + ' from ' + sourceDropzone.id);

                var dropEventSource = 1 // DropEventSource.Target
                if (sourceDropzone == dropzone) {
                    dropEventSource = 0;// DropEventSource.Self
                }
                const args = { pickIndex: pickIndex, pickSource: pickSource, dropEventSource: dropEventSource, destination: dropzone.id };
                blazorComponent.invokeMethod('OnDrop', args);
            }
        })
};