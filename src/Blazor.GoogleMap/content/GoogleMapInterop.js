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
        this.blazorGoogleMap.initialMapOptions = initialMapOptions;
    },

    openInfoWindow: function (id, positionableObject, htmlContent) {
        var content = {};
        if (htmlContent !== undefined) {
            content = htmlContent;
        } else {
            var nodeContent = this.blazorGoogleMap.infoWindowContentNodes.find(function (n) {
                return n.id === id;
            });

            if (nodeContent === undefined) {
                var node = document.getElementById(id);
                node.removeAttribute("style");
                this.blazorGoogleMap.infoWindowContentNodes.push({ id: id, node: node });
                content = node;
            } else {
                content = nodeContent.node;
            }
        }
        
        this.blazorGoogleMap.infoWindow.setContent(content);

        var marker = this.blazorGoogleMap.markersModule.findMarker(positionableObject.id);
        if (marker !== undefined) {
            this.blazorGoogleMap.infoWindow.open(this.blazorGoogleMap.map, marker);
        } else {
            this.blazorGoogleMap.infoWindow.setPosition(positionableObject.position);
            this.blazorGoogleMap.infoWindow.open(this.blazorGoogleMap.map);
        }
    },

    registerEventInvokers: function (eventHandlersInvoker, initDotnetCallback) {
        this.blazorGoogleMap.eventHandlersInvoker = eventHandlersInvoker;
        this.blazorGoogleMap.initDotnetCallback = initDotnetCallback;
    }
};
