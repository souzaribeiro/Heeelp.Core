function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}
function Logout() {
    window.location = '/Account/Logout';
    
}

function GetDateRegex() {
    return /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/
}

function GetPercentRegex() {
    return /^0*(100\,00|[0-9]?[0-9]\,[0-9]{2})$/
}

function GetMoneyRegex() {
    return /^(\d{1,3}(\.\d{3})*|\d+)(\,\d{2})?$/
}

function GetNumberRegex() {
    return /^(\d*[0-9])$/
}


function GetSessionStorage(key) {

    var obj = window.sessionStorage.getItem(key);
    if (obj) {
        return $.parseJSON(obj);
    }
    return null;
}
function SetSessionStorage(key, obj) {
    window.sessionStorage.setItem(key, JSON.stringify(obj));
}
function RemoveSessionStorage(key) {
    window.sessionStorage.removeItem(key);
}


function GetLocalStorage(key) {
    var obj = window.localStorage.getItem(key);
    if (obj)
        return $.parseJSON(obj);
    return null;
}
function SetLocalStorage(key, obj) {
    window.localStorage.setItem(key, JSON.stringify(obj));
}