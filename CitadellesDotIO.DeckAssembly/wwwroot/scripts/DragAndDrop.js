﻿function getUpperTopCoordinatesById(id) {
    const rect = document.getElementById(id).getBoundingClientRect();
    return {
        x: rect.left + window.pageXOffset,
        y: rect.top + window.pageYOffset
    };
}

async function setupDraggables(className, dropzoneClassName, blazorComponent) {
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
                var dragHoverTarget = 0; // DragHoverTarget.None
                const dropzone = document.querySelector('.drop-available');
                if (dropzone) {
                    if (sourceDropzone != dropzone) {
                        dragHoverTarget = 2; // DragHoverTarget.Target
                    }
                    else {
                        dragHoverTarget = 1; // DragHoverTarget.Self        
                    }
                }
                const args = {
                    dragMoveDirection: { x: event.dx, y: event.dy },
                    dragHoverTarget: dragHoverTarget
                }
                blazorComponent.invokeMethod('OnDraggableMove', args);
            },
            start(event) {
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
                const args = {}
                blazorComponent.invokeMethod('OnDraggableDragEnd', args);
            }
        }
    })

    const elements = document.querySelectorAll(className);
    elements.forEach(function (element) {

        element.addEventListener('mouseover', function (event) {
            // Si un item est en cours de drag
            if (document.querySelector(`${className}[data-dragged="true"]`) !== null) {
                return;
            }
            // Si l'évènement est levé par un enfant de la div cible
            if (element.contains(event.relatedTarget)) {
                return;
            }

            const draggableSource = element.closest(dropzoneClassName);
            const args = {
                draggableIndex: parseInt(element.dataset.index),
                draggableSource: draggableSource.id
            };
            blazorComponent.invokeMethod('OnDraggableHoverStart', args);
        });

        element.addEventListener('mouseout', function (event) {
            // Si un item est en cours de drag
            if (document.querySelector(`${className}[data-dragged="true"]`) !== null) {
                return;
            }
            // Si l'évènement est levé par un enfant de la div cible
            if (element.contains(event.relatedTarget)) {
                return;
            }
            const args = {};
            blazorComponent.invokeMethod('OnDraggableHoverEnd', args);
        });
    });
};
function setupDropzones(className, blazorComponent) {
    interact(className)
        .dropzone({
            ondragleave: function (event) {
                const dropzone = event.target;
                var draggable = event.relatedTarget;
                var draggableSource = event.relatedTarget.closest(className);
                if (!draggableSource) {
                    //console.log('no source dropzone, OnDragLeave will return early because source dropzone is not of same component as the targets');
                    return;
                }
                const args = {}
                blazorComponent.invokeMethod('OnDraggableDropzoneLeave', args);
            },
            ondragenter: function (event) {
                const dropzone = event.target;
                var draggable = event.relatedTarget;
                var draggableSource = event.relatedTarget.closest(className);
                if (!draggableSource) {
                    //console.log('no source dropzone, OnDragEnter will return early because source dropzone is not of same component as the targets');
                    return;
                }
                console.log(draggable.id + ' was moved into ' + dropzone.id + ' from ' + draggableSource.id);
                // Ajout d'une classe récupérée plus tard pour savoir si on hover
                var dragHoverTarget = 1; // DragHoverTarget.Self    
                if (draggableSource != dropzone) {
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
                    //console.log('no source dropzone, OnDrop will return early');
                    return;
                }
                const dropzone = event.target;
                // TODO : Théoriquement à virer
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

