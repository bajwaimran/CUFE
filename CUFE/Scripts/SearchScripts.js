var slat;
var slon;
var elat;
var elon;
function btnOK_Click(s, e) {
    var x = startcountry.GetText();
    //alert('Wait... You are searching freights for ' + x);
    //str = MVCxClientUtils.GetSerializedEditorValuesInContainer("searchForm");
    //str = $("form").serialize();
    var sd = startdate.GetText();
    var ed = enddate.GetText();
    if (startcountry.GetText() != null && startcountry.GetText() != '') {
        str = "startcountry=" + startcountry.GetText();
        if (endcountry.GetText() != null && endcountry.GetText() != '') {
            str = str + "&endcountry=" + endcountry.GetText();
            if (sd != '' && ed != '') {
                str = str + "&startdate=" + sd + "&enddate=" + ed;
            }
            window.open("@Url.Action("Search", "Freights")" + "?" + str);
        } else {
            alert("Please select destination Country!");
        }
    }
    //check if searched by city name
    else if (startlocation.GetText() != null && startlocation.GetText() != '') {
        str = "startlocation=" + startlocation.GetText();
        if (endlocation.GetText() != null && endlocation.GetText() != '') {
            str = str + "&endlocation=" + endlocation.GetText();
            if (sd != '' && ed != '') {
                str = str + "&startdate=" + sd + "&enddate=" + ed;
            }
            window.open("@Url.Action("Search", "Freights")" + "?" + str);
        } else {
            alert("Please enter destination city!");
        }
    }
    //check by zip code
    else if (statzip.GetText() != null && statzip.GetText() != '') {
        str = "statzip=" + statzip.GetText();
        if (endzip.GetText() != null && endzip.GetText() != '') {
            str = str + "&endzip=" + endzip.GetText();
            if (sd != '' && ed != '') {
                str = str + "&startdate=" + sd + "&enddate=" + ed;
            }
            window.open("@Url.Action("Search", "Freights")" + "?" + str);
        } else {
            alert("Please enter destination Zip code!");
        }
    } else {
        alert("Please enter a criteria to search!");
    }


}

function btnLoads_Click(s, e) {
    var sd = startdate.GetText();
    var ed = enddate.GetText();

    if (startcountry.GetText() != null && startcountry.GetText() != '') {
        str = "startcountry=" + startcountry.GetText();
        if (endcountry.GetText() != null && endcountry.GetText() != '') {
            str = str + "&endcountry=" + endcountry.GetText();
            if (sd != '' && ed != '') {
                str = str + "&startdate=" + sd + "&enddate=" + ed;
            }
            window.open("@Url.Action("Search", "Loads")" + "?" + str);
        } else {
            alert("Please select destination Country!");
        }
    }
    //check if searched by city name
    else if (startlocation.GetText() != null && startlocation.GetText() != '') {
        str = "startlocation=" + startlocation.GetText();
        if (endlocation.GetText() != null && endlocation.GetText() != '') {
            str = str + "&endlocation=" + endlocation.GetText();
            if (sd != '' && ed != '') {
                str = str + "&startdate=" + sd + "&enddate=" + ed;
            }
            window.open("@Url.Action("Search", "Loads")" + "?" + str);
        } else {
            alert("Please enter destination city!");
        }
    }
    //check by zip code
    else if (statzip.GetText() != null && statzip.GetText() != '') {
        str = "statzip=" + statzip.GetText();
        if (endzip.GetText() != null && endzip.GetText() != '') {
            str = str + "&endzip=" + endzip.GetText();

            if (sd != '' && ed != '') {
                str = str + "&startdate=" + sd + "&enddate=" + ed;
            }
            //check if search using radius
            console.log(withRadius.GetValue());
            if (withRadius.GetValue() == true) {
                getGeoCodeByZip(statzip.GetText(), endzip.GetText());
                setTimeout(function () {
                    str = "startLat=" + slat + "&startLon=" + slon + "&endLat=" + elat + "&endLon=" + elon + "&startradius=" + startradius.GetValue() + "&endradius=" + endradius.GetValue();
                    window.open("@Url.Action("Search", "Loads")" + "?" + str);
                }, 1000);

            } else {

                window.open("@Url.Action("Search", "Loads")" + "?" + str);
            }

        } else {
            alert("Please enter destination Zip code!");
        }
    } else {
        alert("Please enter a criteria to search!");
    }

}




function rxVal_btn(s, e) {
    console.log("you hit me again!!");
    endlocation.SetValue("");
    startlocation.SetValue("");
    startcountry
    endzip

    startcountry.SetValue("");
    statzip.SetValue("");
    startlocation.SetValue("");
    startdate.SetValue();
    endcountry.SetValue();
    endzip.SetValue("");
    endlocation.SetValue("");
    enddate.SetValue();
}

function OnBeginCallback(s, e) {
    e.customArgs["FreightID"] = freightID;
    employeeId = null;
}
function OnEndCallback(s, e) {
    if (employeeId != null)
        cbpExample.PerformCallback();
}

