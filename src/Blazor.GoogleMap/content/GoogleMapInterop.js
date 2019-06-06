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
        console.log("create map");
        // The location of Uluru
        var uluru = { lat: this.initialState.lat, lng: this.initialState.lng };
        // The map, centered at Uluru
        this.map = new google.maps.Map(
            document.getElementById('map'), { zoom: 4, center: uluru });
        // The marker, positioned at Uluru
        var marker = new google.maps.Marker({ position: uluru, map: this.map });

        if (this.eventHandlersInvoker !== undefined) {
            this.map.addListener('click', function (e) {
                console.log("Invoke Dotnet onclick");
                return eventHandlersInvoker.invokeMethodAsync('InvokeOnClick', e);
            });
        }

        return true;
    },

    initMap: function (lat, lng) {
        this.initialState = {
            lat: lat,
            lng: lng
        };

        console.log(this.initialState);
    },

    registerEventInvokers: function (eventHandlersInvoker) {
        this.eventHandlersInvoker = eventHandlersInvoker;
    }
};
