var originCountry;
var destCountry;
var search;
var originZip;
var destZip;
var originCity;
var destCity;
var startDate;
var endDate;
var originRad;
var destRad;
var radiusSearch;

function searchFreights_Click(s, e) {
    search = "Freights";
    cbpExample.PerformCallback();
}
function searchLoads_Click(s, e) {
    search = "Loads";
    cbpExample.PerformCallback();
}
function OnBeginCallback(s, e) {
    e.customArgs["search"] = search;
    e.customArgs["originCountry"] = originCountry;
    e.customArgs["destCountry"] = destCountry;
    e.customArgs["originZip"] = originZip;
    e.customArgs["destZip"] = destZip;
    e.customArgs["originCity"] = originCity;
    e.customArgs["destCity"] = destCity;
    e.customArgs["startDate"] = startDate;
    e.customArgs["endDate"] = endDate;
    e.customArgs["originRad"] = originRad;
    e.customArgs["destRad"] = destRad;
    e.customArgs["radiusSearch"] = radiusSearch;
    search = null;
}
function OnEndCallback(s, e) {
    if (search != null)
        cbpExample.PerformCallback();
}

function setOriginCity_Changed(s, e) {
    originCity = s.GetValue();
}
function setDestCity_Changed(s, e) {
    destCity = s.GetValue();
    
}
function setOriginCountry_Changed(s, e) {
    originCountry = s.GetValue();
    
}
function setOriginZip_Changed(s, e) {
    originZip = s.GetValue();
    
}
function setStartDate_Changed(s, e) {
    startDate = s.GetValue();
    
}
function setOriginRad_Changed(s, e) {
    originRad = s.GetValue();
    
}
function setDestCountry_Changed(s, e) {
    destCountry = s.GetValue();
    console.log("Dest Country set to " + destCountry);
}
function setDestZip_Changed(s, e) {
    destZip = s.GetValue();
    console.log("Dest zip set to " + destZip);
}
function setEndDate_Changed(s, e) {
    endDate = s.GetValue();
    console.log("Dest date set to " + endDate);
}
function setDestRad_Changed(s, e) {
    destRad = s.GetValue();
    console.log("Dest radius set to " + destRad);
}
function setSearchWithRadius_Changed(s, e) {
    radiusSearch = s.GetValue();
    console.log("Search with Radius  set to " + radiusSearch);
}