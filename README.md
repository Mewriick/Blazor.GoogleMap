# Blazor.GoogleMap
Blazor component for GoogleMap which allows to manipulate with GoogleMap just with C# language and no JS is required.

## IMPORTANT!
This is very very first preview version and API it will increase and also documentation.

# Instalation
[![NuGet Pre Release](https://img.shields.io/badge/nuget-0.0.1-orange.svg)](https://www.nuget.org/packages/BlazorMap)

# Setup
```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddGoogleMaps(options =>
    {
	options.ApiKey = "Your Google Maps Api Key";
    });
}
```

# Features
* Add marker to the map
* Add OnClick, OnDoubleClick event listeners on the map and marker
* InfoWindow

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

<h1>Google Map</h1>

<GoogleMap OnClick="@MapOnClick" OnDoubleClick="@MapOnDoubleClick"></GoogleMap>
<GoogleMapInfoWindow Id="infoWindow">
    <div>
        <h4>Infowindow 1</h4>
        @if (selectedMarker != null)
        {
            <p>@selectedMarker.Id</p>
        }


        <button onclick="@RemoveMarker">Remove marker</button>
    </div>
</GoogleMapInfoWindow>

<GoogleMapInfoWindow Id="infoWindowSecond">
    <div>
        <h4>Infowindow 2</h4>
        @if (selectedMarker != null)
        {
            <p>@selectedMarker.Id</p>
        }


        <button onclick="@RemoveMarker">Remove marker</button>
    </div>
</GoogleMapInfoWindow>

@functions {
    int currentCount = 0;
    IMarkerCollection markers;
    Marker selectedMarker;

    protected override void OnInit()
    {
        base.OnInit();
        markers = MarkerCollectionFactory.Create();
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
