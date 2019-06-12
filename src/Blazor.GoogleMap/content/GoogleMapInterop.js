// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.blazorGoogleMap = {
    markers: [],

    initMapCallback: function () {
        var currentThis = this === window
            ? this.blazorGoogleMap
            : this;

        currentThis.map = new google.maps.Map(
            document.getElementById('map'), currentThis.initialMapOptions);

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

    addMarker: function (markerRef, marker) {
        var mapMarker = new google.maps.Marker({
            map: this.blazorGoogleMap.map,
            position: marker.position,
            title: marker.title
        });

        if (marker.onClick !== null) {
            mapMarker.addListener('click', function () {
                return markerRef.invokeMethodAsync('OnClickHandle');
            });
        }

        mapMarker.id = marker.id;
        this.blazorGoogleMap.markers.push({ mapMarker: mapMarker, markerRef: markerRef });

        return true;
    },

    openInfoWindow: function (id, positionableObject, htmlContent) {
        var content = htmlContent === undefined 
            ? document.getElementById(id).innerHTML
            : htmlContent;
        
        this.blazorGoogleMap.infoWindow = new google.maps.InfoWindow();
        this.blazorGoogleMap.infoWindow.setContent(content);

        var marker = this.blazorGoogleMap.markers.find(function (m) {
            return m.mapMarker.id === positionableObject.id;
        });

        if (marker !== undefined) {
            this.blazorGoogleMap.infoWindow.open(this.blazorGoogleMap.map, marker.mapMarker);
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
