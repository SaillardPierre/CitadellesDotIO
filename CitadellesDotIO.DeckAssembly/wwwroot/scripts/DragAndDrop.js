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
    const sourceDropzone = draggable.closest(dropzoneClassName);
    if (!sourceDropzone) {
        console.log('no source dropzone, OnCardMove move will return early');
        return;
    }
    var targetNeighboursPositions = null;
    var dragHoverTarget = 0; // DragHoverTarget.None
    const dropzone = document.querySelector('.drop-available');
    if (dropzone) {
        if (sourceDropzone != dropzone) {
            dragHoverTarget = 2; // DragHoverTarget.Target
            targetNeighboursPositions = getUpperTopCoordinatesForList(dropzone, 'pickPoolDraggable')
        }
        else dragHoverTarget = 1; // DragHoverTarget.Self        
    }
    const args = {
        pickIndex: parseInt(draggable.dataset.index),
        pickSource: sourceDropzone.id,
        dragHoverTarget: dragHoverTarget,
        draggablePosition: getUpperTopCoordinates(draggable),
        draggableMovementPosition: { x: event.dx, y: event.dy },
        sourceNeighboursPositions: getUpperTopCoordinatesForList(sourceDropzone, 'pickPoolDraggable'),
        targetNeighboursPositions: targetNeighboursPositions
    }
    blazorComponent.invokeMethod('OnDragMove', args);
}
async function setupPickPoolDraggables(className, dropzoneClassName, blazorComponent, dragManager) {
    interact(className).draggable({
        intertia: true,
        listeners: {
            move(event) {
                //console.log("dragmove");
                applyOnCardMove(event, dropzoneClassName, dragManager, blazorComponent);
            },
            start(event) {
                console.log("dragstart");
            },
            end(event) {
                //console.log("dragend");
                const draggable = event.target;
                const pickIndex = parseInt(draggable.dataset.index);
                const sourceDropzone = event.target.closest(dropzoneClassName);
                if (!sourceDropzone) {
                    console.log('no source dropzone, OnDragEnd will return early');
                    return;
                }
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

function setupPickPoolDropzones(className, blazorComponent) {
    interact(className)
        .dropzone({
            ondragleave: function (event) {
                const dropzone = event.target;
                //const draggable = event.relatedTarget;
                //const sourceDropzone = event.relatedTarget.closest(className);
                //console.log(draggable.id + ' was moved out of ' + dropzone.id);
                dropzone.classList.remove('drop-available');
            },
            ondragenter: function (event) {
                const dropzone = event.target;
                //var draggable = event.relatedTarget;
                //var sourceDropzone = event.relatedTarget.closest(className);                
                //console.log(draggable.id + ' was moved into ' + dropzone.id + ' from ' + sourceDropzone.id);
                // Ajout d'une classe récupérée plus tard pour savoir si on hover
                dropzone.classList.add('drop-available');
            },
            ondrop: function (event) {
                //console.log('dropped');
                const draggable = event.relatedTarget;
                const sourceDropzone = draggable.closest(className);
                if (!sourceDropzone) {
                    console.log('no source dropzone, OnDrop will return early');
                    return;
                }
                const dropzone = event.target;
                dropzone.classList.remove('drop-available');               
                //console.log(draggable.id + ' was dropped into ' + dropzone.id + ' from ' + sourceDropzone.id);
                var dropEventSource = 1 // DropEventSource.Target
                if (sourceDropzone == dropzone) {
                    dropEventSource = 0;// DropEventSource.Self
                }
                const args =
                {
                    pickIndex: parseInt(draggable.dataset.index),
                    pickSource: sourceDropzone.id,
                    dropEventSource: dropEventSource,
                    destination: dropzone.id
                };
                blazorComponent.invokeMethod('OnDrop', args);
            }
        })
};