


//geoCoordinates related functions
var startCountry;
var startZip;
var endCountry;
var endzip;
var latitude;
var longitude;

function setStartCountryName(s, e) {
    startCountry = s.GetValue();
}

function setEndCountryName(s, e) {
    endCountry = s.GetValue();
}


function getStartCoordinates(s, e) {
    startZip = s.GetValue();
    $.ajax({
        url: "/GeoCoordinates/GetCoordinate",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ country: startCountry, zip: startZip }),
        success: function (response) {
            if (response) {
                var lat = response.Latitude;
                var lon = response.Longitude;
                setStartLocationCoordinates(lat, lon);
            } else {
                getCoordinatesFronBing(startCountry, startZip);
                setStartLocationCoordinates(latitude, longitude);
            }
        }
    });
}

function getEndCoordinates(s, e) {
    endzip = s.GetValue();
    $.ajax({
        url: "/GeoCoordinates/GetCoordinate",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ country: endCountry, zip: endzip }),
        success: function (response) {
            if (response) {
                var lat = response.Latitude;
                var lon = response.Longitude;
                setEndLocationCoordinates(lat, lon);
            } else {
                console.log("not found");
                getCoordinatesFronBing(endCountry, endzip);
                setEndLocationCoordinates(latitude, longitude);
            }
        }
    });
}

function getCoordinatesFronBing(country, zip) {
    console.log("getCoordinatesFronBing");
    var url = "http://dev.virtualearth.net/REST/v1/Locations?countryRegion=" + country + "& postalCode=" + zip + "&key=ApuZ25zP2JplCbiYUnTc6nQ-g4MJwgU7L9XaK0HzD0tsiFLpiZg4d_fH0JaqV_LR";
    $.getJSON(url)
        .done(function (data) {
            console.log(data);
            //var country = data.resourceSets[0].resources[0].address.countryRegion;
            latitude = data.resourceSets[0].resources[0].geocodePoints[0].coordinates[0];
            longitude = data.resourceSets[0].resources[0].geocodePoints[0].coordinates[1];
            var geoData = { country: country, zip: zip, latitude: latitude, longitude: longitude };
            console.log(geoData);
            addNewGeoCoordinateData(geoData)
        })
        .fail(function () { console.log("No coordinates found."); });

}

function addNewGeoCoordinateData(data) {
    var country = data.country;
    var zip = data.zip;
    var latitude = data.latitude;
    var longitude = data.longitude;
    $.ajax({
        url: "/GeoCoordinates/Add",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ country: country, zip: zip, latitude: latitude, longitude: longitude }),
        success: function (response) {
            if (response) {
                console.log("Added");
                setStartLocationCoordinates();
            } else {
                console.log("not Added");
            }
        }
    });
}

function setStartLocationCoordinates(lat, lon) {

    if (lat !== null && lon !== null) {
        console.log("setStartLocationCoordinates");
        gridView.GetEditor('StartLat').SetValue(lat);
        gridView.GetEditor('StartLon').SetValue(lon);
    }

}
function setEndLocationCoordinates(lat, lon) {
    console.log("setEndLocationCoordinates");
    gridView.GetEditor('EndLat').SetValue(lat);
    gridView.GetEditor('EndLon').SetValue(lon);
}