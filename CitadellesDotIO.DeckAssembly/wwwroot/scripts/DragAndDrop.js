﻿function getUpperTopCoordinatesForList(parentElement, className) {
    var elements = parentElement.querySelectorAll(className);
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

function applyOnCardMove(event, className, dropzoneClassName, blazorComponent) {
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
            targetNeighboursPositions = getUpperTopCoordinatesForList(dropzone, className);
        }
        else {
            dragHoverTarget = 1; // DragHoverTarget.Self        
            targetNeighboursPositions = getUpperTopCoordinatesForList(sourceDropzone, className);
        }
    }
    const args = {
        pickIndex: parseInt(draggable.dataset.index),
        pickSource: sourceDropzone.id,
        dragHoverTarget: dragHoverTarget,
        draggablePosition: getUpperTopCoordinates(draggable),
        dragMoveDirection: { x: event.dx, y: event.dy },
        targetNeighboursPositions: targetNeighboursPositions
    }
    blazorComponent.invokeMethod('OnDragMove', args);
}
async function setupDraggables(className, dropzoneClassName, blazorComponent) {
    interact(className).draggable({
        intertia: true,
        listeners: {
            move(event) {
                //console.log("dragmove");
                applyOnCardMove(event, className, dropzoneClassName, blazorComponent);
            },
            start(event) {
                //console.log("dragstart");
                const draggable = event.target;
                const pickIndex = parseInt(draggable.dataset.index);
                const sourceDropzone = event.target.closest(dropzoneClassName);
                if (!sourceDropzone) {
                    console.log('no source dropzone, OnDragStart will return early');
                    return;
                }
                const onDragStartEventArgs = {
                    pickIndex: pickIndex,
                    pickSource: sourceDropzone.id
                }
                blazorComponent.invokeMethod('OnDragStartAsync', onDragStartEventArgs);
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

// TODO : Très certainement ajouter des checks sur les noms de clasees /dropzone pour éviter les chevauchement à la con avec un autre composant
async function setupDraggablesV2(className, dropzoneClassName, blazorComponent) {
    interact(className).draggable({
        intertia: true,
        listeners: {
            move(event) {                
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
                        targetNeighboursPositions = getUpperTopCoordinatesForList(dropzone, className);
                    }
                    else {
                        dragHoverTarget = 1; // DragHoverTarget.Self        
                        targetNeighboursPositions = getUpperTopCoordinatesForList(sourceDropzone, className);
                    }
                }
                const args = {
                    dragHoverTarget: dragHoverTarget,
                    draggablePosition: getUpperTopCoordinates(draggable),
                    dragMoveDirection: { x: event.dx, y: event.dy },
                    targetNeighboursPositions: targetNeighboursPositions
                }
                blazorComponent.invokeMethod('OnDraggableMove', args);
            },
            start(event) {
                //console.log("dragstart");
                const draggable = event.target;
                const draggableIndex = parseInt(draggable.dataset.index);
                const draggableSource = event.target.closest(dropzoneClassName);
                if (!draggableSource) {
                    console.log('no source dropzone, OnDragStart will return early');
                    return;
                }
                const args = {
                    draggableIndex: draggableIndex,
                    draggableSource: draggableSource.id
                }
                blazorComponent.invokeMethod('OnDraggableDragStart', args);
            },
            end(event) {
                //console.log("dragend");
                const draggable = event.target;
                const draggableIndex = parseInt(draggable.dataset.index);
                const draggableSource = event.target.closest(dropzoneClassName);
                if (!draggableSource) {
                    console.log('no source dropzone, OnDragEnd will return early');
                    return;
                }
                const args = {
                    draggableIndex: draggableIndex,
                    draggableSource: draggableSource.id
                }
                blazorComponent.invokeMethod('OnDraggableDragEnd', args);
            }
        }
    })
};


function setupDropzonesV2(className, blazorComponent) {
    interact(className)
        .dropzone({
            ondragleave: function (event) {
                const args = {}
                blazorComponent.invokeMethod('OnDraggableDropzoneLeave', args);
            },
            ondragenter: function (event) {
                const dropzone = event.target;
                var draggable = event.relatedTarget;
                var sourceDropzone = event.relatedTarget.closest(className);
                console.log(draggable.id + ' was moved into ' + dropzone.id + ' from ' + sourceDropzone.id);
                // Ajout d'une classe récupérée plus tard pour savoir si on hover
                var dragHoverTarget = 1; // DragHoverTarget.Self    
                if (sourceDropzone != dropzone) {
                    dragHoverTarget = 2; // DragHoverTarget.Target
                }
                const args = {
                    dropzoneHoverSource: dropzone.id,
                    dragHoverTarget: dragHoverTarget
                }
                blazorComponent.invokeMethod('OnDraggableDropzoneEnter', args);
            },
            ondrop: function (event) {
                //console.log('dropped');
                const draggable = event.relatedTarget;
                const draggableSource = draggable.closest(className);

                if (!draggableSource) {
                    console.log('no source dropzone, OnDrop will return early');
                    return;
                }
                const dropzone = event.target;
                dropzone.classList.remove('drop-available');
                //console.log(draggable.id + ' was dropped into ' + dropzone.id + ' from ' + sourceDropzone.id);
                var dropEventSource = 1 // DropEventSource.Target
                if (draggableSource == dropzone) {
                    dropEventSource = 0;// DropEventSource.Self
                }
                const args =
                {
                    draggableIndex: parseInt(draggable.dataset.index),
                    draggableSource: draggableSource.id,
                    dropEventSource: dropEventSource,
                    destinationDropzone: dropzone.id
                };
                blazorComponent.invokeMethod('OnDraggableDrop', args);
            }
        })
}

function setupDropzones(className, blazorComponent) {
    interact(className)
        .dropzone({
            ondragleave: function (event) {
                const dropzone = event.target;
                const draggable = event.relatedTarget;
                const sourceDropzone = event.relatedTarget.closest(className);
                console.log(draggable.id + ' was moved out of ' + dropzone.id);
                dropzone.classList.remove('drop-available');
                const args = {}
                blazorComponent.invokeMethod('OnDraggableDropzoneLeave', args);
            },
            ondragenter: function (event) {
                const dropzone = event.target;
                var draggable = event.relatedTarget;
                const pickIndex = parseInt(draggable.dataset.index);
                var sourceDropzone = event.relatedTarget.closest(className);
                // TODO : Crash ici a la lecture d'un ID, voir lequel, arrive quand survol d'un autre composant
                console.log(draggable.id + ' was moved into ' + dropzone.id + ' from ' + sourceDropzone.id);
                // Ajout d'une classe récupérée plus tard pour savoir si on hover
                var dragHoverTarget = 1; // DragHoverTarget.Self    
                if (sourceDropzone != dropzone) {
                    dragHoverTarget = 2; // DragHoverTarget.Target
                }
                const dragEnterEventArgs = {
                    pickSource: sourceDropzone.id,
                    hoverSource: dropzone.id,
                    dragHoverTarget: dragHoverTarget
                }
                blazorComponent.invokeMethod('OnDragEnter', dragEnterEventArgs);
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