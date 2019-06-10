// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.blazorGoogleMap = {
    initialState: {
        lat: 0,
        lng: 0
    },

    initDotnetCallback: {},
    eventHandlersInvoker: {},
    infoWindows: {},
    map: {},
    markers: [],

    initMapCallback: function () {
        var center = { lat: this.initialState.lat, lng: this.initialState.lng };
        this.map = new google.maps.Map(
            document.getElementById('map'), { zoom: 4, center: center });

        if (this.eventHandlersInvoker === undefined) {
            return false;
        }

        this.initDotnetCallback.invokeMethodAsync('OnInitFinished');
        this.eventHandlersInvoker.invokeMethodAsync('GetMouseEvents')
            .then((events) => {
                events.map(eventName => {
                    this.map.addListener(eventName,
                        (function (e) {
                            return this.eventHandlersInvoker
                                .invokeMethodAsync('InvokeMouseEvent', eventName, e);
                        }).bind(this)
                    );
                });
            });

        return true;
    },

    initMap: function (lat, lng) {
        this.blazorGoogleMap.initialState = {
            lat: lat,
            lng: lng
        };
    },

    addMarker: function (markerRef, marker) {
        var mapMarker = new google.maps.Marker({
            map: this.blazorGoogleMap.map,
            position: marker.position,
            title: marker.title,           
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

    openInfoWindow: function (id, positionableObject) {
        if (this.blazorGoogleMap.infoWindows[id] === undefined) {
            this.blazorGoogleMap.infoWindows[id] = new google.maps.InfoWindow();
        }       

        var content = document.getElementById(id).innerHTML;
        var infoWindow = this.blazorGoogleMap.infoWindows[id];
        infoWindow.setContent(content);
        
        var marker = this.blazorGoogleMap.markers.find(function (m) {
            return m.mapMarker.id === positionableObject.id;
        });

        if (marker !== undefined) {
            infoWindow.open(this.blazorGoogleMap.map, marker.mapMarker);
        } else {
            infoWindow.setPosition(positionableObject.position);
            infoWindow.open(this.blazorGoogleMap.map);
        }
    },

    registerEventInvokers: function (eventHandlersInvoker, initDotnetCallback) {
        this.blazorGoogleMap.eventHandlersInvoker = eventHandlersInvoker;
        this.blazorGoogleMap.initDotnetCallback = initDotnetCallback;
    }
};
