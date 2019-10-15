// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.blazorGoogleMap = {
    infoWindowContentNodes: [],

    initMapCallback: function () {
        var currentThis = this === window
            ? this.blazorGoogleMap
            : this;

        currentThis.map = new google.maps.Map(document.getElementById('map'), currentThis.initialMapOptions);
        currentThis.infoWindow = new google.maps.InfoWindow();

        if (currentThis.eventHandlersInvoker === undefined) {
            return false;
        }

        currentThis.initDotnetCallback.invokeMethodAsync('OnInitFinished');
        currentThis.eventHandlersInvoker.invokeMethodAsync('GetMouseEvents')
            .then((events) => {
                events.map(eventName => {
                    currentThis.map.addListener(eventName,
                        (function (e) {
                            return this.eventHandlersInvoker
                                .invokeMethodAsync('InvokeMouseEvent', eventName, e);
                        }).bind(currentThis)
                    );
                });
            });

        return true;
    },

    initMap: function (initialMapOptions) {
        blazorGoogleMap.initialMapOptions = initialMapOptions;
    },

    openInfoWindow: function (id, positionableObject, htmlContent) {
        var content = {};
        if (htmlContent !== undefined) {
            content = htmlContent;
        } else {
            var nodeContent = blazorGoogleMap.infoWindowContentNodes.find(function (n) {
                return n.id === id;
            });

            if (nodeContent === undefined) {
                var node = document.getElementById(id);
                node.removeAttribute("style");
                blazorGoogleMap.infoWindowContentNodes.push({ id: id, node: node });
                content = node;
            } else {
                content = nodeContent.node;
            }
        }
        
        blazorGoogleMap.infoWindow.setContent(content);

        var marker = blazorGoogleMap.markersModule.findMarker(positionableObject.id);
        if (marker !== undefined) {
            blazorGoogleMap.infoWindow.open(blazorGoogleMap.map, marker);
        } else {
            blazorGoogleMap.infoWindow.setPosition(positionableObject.position);
            blazorGoogleMap.infoWindow.open(blazorGoogleMap.map);
        }
    },
    registerEventInvokers: function (eventHandlersInvoker, initDotnetCallback) {
        if (blazorGoogleMap) {
          
            blazorGoogleMap.eventHandlersInvoker = eventHandlersInvoker;
            blazorGoogleMap.initDotnetCallback = initDotnetCallback;
        }
    }
};
