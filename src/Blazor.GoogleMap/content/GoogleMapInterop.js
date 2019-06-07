// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.blazorGoogleMap = {
    initialState: {
        lat: 0,
        lng: 0
    },

    eventHandlersInvoker: {},
    map: {},

    initMapCallback: function () {        
        var center = { lat: this.initialState.lat, lng: this.initialState.lng };        
        this.map = new google.maps.Map(
            document.getElementById('map'), { zoom: 4, center: center });

        if (this.eventHandlersInvoker === undefined) {
            return false;
        }

        this.eventHandlersInvoker.invokeMethodAsync('GetMouseEvents')
            .then((events) => {
                events.map(eventName => {
                    this.map.addListener(eventName,
                        (function (e)
                        {
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

    registerEventInvokers: function (eventHandlersInvoker) {
        this.blazorGoogleMap.eventHandlersInvoker = eventHandlersInvoker;
    }
};
