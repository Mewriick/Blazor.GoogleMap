# Blazor.GoogleMap
Blazor(Server) component for Google Map which allows to mapping features just with C# language and ASP.NET Core 3.0

Blazor Web Assembly is still in preview for ASP.NET Core 3.0 and its' API(s) are still changing a lot. So Blazor.GoogleMap.Client project is removed for now.

## IMPORTANT!
This is just a preview version of the component but ready to use in PROD at your own risk. 
In future new features will be added. Feel free to send PR(s).

# Instalation
Soon

# Setup
Just add ``AddGoogleMaps`` service extension to default service collection of the application. Also don't forget to check other options.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddGoogleMaps(options =>
    {
	options.ApiKey = "Your Google Maps Api Key";
    });
}
```

Also don't forget to add following GoogleMapInterop.js and GoogleMapMarkerInterop.js files into _Host.cshtml file as below

```html
    <script src="_framework/blazor.server.js"></script>

    <script src="~/GoogleMapInterop.js"></script>
    <script src="~/GoogleMapMarkerInterop.js"></script>
```

# Features
* Add marker to the map
* Add OnClick, OnDoubleClick event listeners on the map and marker
* InfoWindow
* Google Map initialization options added to **InitialMapOptions**

```
    FullscreenControl = true,
    MapTypeControl = false,
    MapTypeId = MapTypes.Roadmap,
    RotateControl = false,
    ScaleControl = false,
    Scrollwheel = true,
    StreetViewControl = false,
    ZoomControl = false
```
# Markers
For adding markers to the map you need **IMarkerCollection** service which is provided by method **Create** on **MarkerCollectionFactory** object.

# InfoWindow
For definition of InfoWindow you can use **GoogleMapInfoWindow** component. For handling open library provide **InfoWindow** service with **Open** method.
Method can accept **id** of **GoogleMapInfoWindow** component or whole html which you want to render.
If you add marker into map whit filled property **AssociatedInfoWindowId**, after you click on marker the **InfoWindow** is opened.

# Example

```cs
@page "/map"
@inject MarkerCollectionFactory MarkerCollectionFactory;
@inject InfoWindow  InfoWindow;
@inject IJSRuntime JsRuntime;

<h1>Google Map</h1>

<GoogleMap OnClick="(args)=>MapOnClick(args)" 
	   OnDoubleClick="(args)=>MapOnDoubleClick(args)" 
	   InitialMapOptions="@initialMapOptions">
</GoogleMap>

<GoogleMapInfoWindow Id="infoWindow">
    <div>
        <h4>Infowindow 1</h4>
        @if (selectedMarker != null)
        {
            <p>@selectedMarker.Id</p>
        }


        <button @onclick="()=>RemoveMarker()">Remove marker</button>
    </div>
</GoogleMapInfoWindow>

<GoogleMapInfoWindow Id="infoWindowSecond">
    <div>
        <h4>Infowindow 2</h4>
        @if (selectedMarker != null)
        {
            <p>@selectedMarker.Id</p>
        }


         <button @onclick="()=>RemoveMarker()">Remove marker</button>
    </div>
</GoogleMapInfoWindow>

@functions {
    int currentCount = 0;
    IMarkerCollection markers;
    Marker selectedMarker;
    InitialMapOptions initialMapOptions;
    
    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        markers = MarkerCollectionFactory.Create();
		
	initialMapOptions = new InitialMapOptions
        {
            Center = new Blazor.GoogleMap.Map.Coordinates.LatLng
            {
                Lat = 41.058002,
                Lng = 29.0416793
            },
            Zoom = 5,
            Height = "400px",
            Width = "100%",
            FullscreenControl = true,
            MapTypeControl = false,
            MapTypeId = MapTypes.Roadmap,
            RotateControl = false,
            ScaleControl = false,
            Scrollwheel = true,
            StreetViewControl = false,
            ZoomControl = false
        };

    }

    void MapOnClick(MouseEventArgs mouseEvent)
    {
        Console.WriteLine($"Clicked! {mouseEvent.LatLng.Lat}, {mouseEvent.LatLng.Lng}");
        markers.Add(new MarkerOptions(mouseEvent.LatLng)
        {
            Title = $"Test {DateTime.Now}",
            AssociatedInfoWindowId = markers.Count % 2 == 0 ? "infoWindow" : "infoWindowSecond",
            OnMarkerClick = EventCallback.Factory.Create<Marker>(this, MarkerClick)
        });
    }

    void MapOnDoubleClick(MouseEventArgs mouseEvent)
    {
        Console.WriteLine($"DoubleClicked! {mouseEvent.LatLng.Lat}, {mouseEvent.LatLng.Lng}");
    }

    void MarkerClick(Marker marker)
    {
        selectedMarker = marker;
        Console.WriteLine(marker.Options.Title);
    }

    async Task RemoveMarker()
    {
        if (selectedMarker != null)
        {
            var removedResult = await markers.Remove(selectedMarker);
            Console.WriteLine($"Marker removed: {removedResult}");
        }
    }
}
```
