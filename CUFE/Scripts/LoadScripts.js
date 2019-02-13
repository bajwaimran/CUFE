var freightID = null;
function OnListBoxIndexChanged(s, e) {
    var keys = gridView.GetSelectedKeysOnPage();
    if (keys != undefined && keys.length != 0) {
        freightID = keys[0];
        if (!cbpExample.InCallback())
            cbpExample.PerformCallback();
    }
}
function OnBeginCallback(s, e) {
    e.customArgs["FreightID"] = freightID;
    employeeId = null;
}
function OnEndCallback(s, e) {
    if (employeeId != null)
        cbpExample.PerformCallback();
}