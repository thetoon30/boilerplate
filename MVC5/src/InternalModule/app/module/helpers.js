/*\
|*|
|*|  :: cookies.js ::
|*|
|*|  A complete cookies reader/writer framework with full unicode support.
|*|
|*|  https://developer.mozilla.org/en-US/docs/DOM/document.cookie
|*|
|*|  This framework is released under the GNU Public License, version 3 or later.
|*|  http://www.gnu.org/licenses/gpl-3.0-standalone.html
|*|
|*|  Syntaxes:
|*|
|*|  * docCookies.setItem(name, value[, end[, path[, domain[, secure]]]])
|*|  * docCookies.getItem(name)
|*|  * docCookies.removeItem(name[, path], domain)
|*|  * docCookies.hasItem(name)
|*|  * docCookies.keys()
|*|
\*/

var docCookies = {
    getItem: function (sKey) {
        return decodeURIComponent(document.cookie.replace(new RegExp("(?:(?:^|.*;)\\s*" + encodeURIComponent(sKey).replace(/[\-\.\+\*]/g, "\\$&") + "\\s*\\=\\s*([^;]*).*$)|^.*$"), "$1")) || null;
    },
    setItem: function (sKey, sValue, vEnd, sPath, sDomain, bSecure) {
        if (!sKey || /^(?:expires|max\-age|path|domain|secure)$/i.test(sKey)) { return false; }
        var sExpires = "";
        if (vEnd) {
            switch (vEnd.constructor) {
                case Number:
                    sExpires = vEnd === Infinity ? "; expires=Fri, 31 Dec 9999 23:59:59 GMT" : "; max-age=" + vEnd;
                    break;
                case String:
                    sExpires = "; expires=" + vEnd;
                    break;
                case Date:
                    sExpires = "; expires=" + vEnd.toUTCString();
                    break;
            }
        }
        document.cookie = encodeURIComponent(sKey) + "=" + encodeURIComponent(sValue) + sExpires + (sDomain ? "; domain=" + sDomain : "") + (sPath ? "; path=" + sPath : "") + (bSecure ? "; secure" : "");
        return true;
    },
    removeItem: function (sKey, sPath, sDomain) {
        if (!sKey || !this.hasItem(sKey)) { return false; }
        document.cookie = encodeURIComponent(sKey) + "=; expires=Thu, 01 Jan 1970 00:00:00 GMT" + (sDomain ? "; domain=" + sDomain : "") + (sPath ? "; path=" + sPath : "");
        return true;
    },
    hasItem: function (sKey) {
        return (new RegExp("(?:^|;\\s*)" + encodeURIComponent(sKey).replace(/[\-\.\+\*]/g, "\\$&") + "\\s*\\=")).test(document.cookie);
    },
    keys: /* optional method: you can safely remove it! */ function () {
        var aKeys = document.cookie.replace(/((?:^|\s*;)[^\=]+)(?=;|$)|^\s*|\s*(?:\=[^;]*)?(?:\1|$)/g, "").split(/\s*(?:\=[^;]*)?;\s*/);
        for (var nIdx = 0; nIdx < aKeys.length; nIdx++) { aKeys[nIdx] = decodeURIComponent(aKeys[nIdx]); }
        return aKeys;
    }
};

function getFragment() {
    if (window.location.hash.indexOf("#") === 0) {
        return parseQueryString(window.location.hash.substr(1));
    } else {
        return {};
    }
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function parseQueryString(queryString) {
    var data = {},
        pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

    if (queryString === null) {
        return data;
    }

    pairs = queryString.split("&");

    for (var i = 0; i < pairs.length; i++) {
        pair = pairs[i];
        separatorIndex = pair.indexOf("=");

        if (separatorIndex === -1) {
            escapedKey = pair;
            escapedValue = null;
        } else {
            escapedKey = pair.substr(0, separatorIndex);
            escapedValue = pair.substr(separatorIndex + 1);
        }

        key = decodeURIComponent(escapedKey);
        value = decodeURIComponent(escapedValue);

        data[key] = value;
    }

    return data;
}

function verifyStateMatch(fragment) {
    var state;

    if (typeof (fragment.access_token) !== "undefined") {
        state = sessionStorage["state"];
        sessionStorage.removeItem("state");

        if (state === null || fragment.state !== state) {
            fragment.error = "invalid_state";
        }
    }
}

function getUserName() {
    var userName = docCookies.getItem('wc_user');

    if (userName) {
        return userName;
    }

    return '';
}

function isAuthenticated() {
    var accessToken = docCookies.getItem('wc_ssid');

    if (accessToken) {
        return true;
    }

    return false;
}

function getSecurityHeaders() {
    var accessToken = docCookies.getItem('wc_ssid');

    if (accessToken) {
        return { "Authorization": "Bearer " + accessToken };
    }

    return {};
}

function setAccessToken(accessToken, userName, persistent) {
    if (persistent) {
        var now = new Date();
        var expires = new Date(now.setDate(now.getDate() + 30));

        docCookies.setItem('wc_ssid', accessToken, expires.toUTCString(), '/');
        docCookies.setItem('wc_user', userName, expires.toUTCString(), '/');
    } else {
        docCookies.setItem('wc_ssid', accessToken, null, '/');
        docCookies.setItem('wc_user', userName, null, '/');
    }
}

function setRegisterTemp(accessToken, externalId, userName, firstName, lastName, email, extraData) {
    sessionStorage["tempAccessToken"] = accessToken;
    sessionStorage["tempExternalId"] = externalId;
    sessionStorage["tempUserName"] = userName;
    sessionStorage["tempFirstName"] = firstName;
    sessionStorage["tempLastName"] = lastName;
    sessionStorage["tempEmail"] = email;
    sessionStorage["tempExtraData"] = extraData;
}

function getRegisterTemp() {
    var regInfo = {
        accessToken: sessionStorage["tempAccessToken"],
        externalId: sessionStorage["tempExternalId"],
        userName: sessionStorage["tempUserName"],
        firstName: sessionStorage["tempFirstName"],
        lastName: sessionStorage["tempLastName"],
        email: sessionStorage["tempEmail"],
        extraData: sessionStorage["tempExtraData"]
    };

    return regInfo;
}

function clearRegisterTemp() {
    sessionStorage.removeItem("tempAccessToken");
    sessionStorage.removeItem("tempExternalId");
    sessionStorage.removeItem("tempUserName");
    sessionStorage.removeItem("tempFirstName");
    sessionStorage.removeItem("tempLastName");
    sessionStorage.removeItem("tempEmail");
    sessionStorage.removeItem("tempExtraData");
}

function clearAccessToken() {
    docCookies.removeItem('wc_ssid', '/');
    docCookies.removeItem('wc_user', '/');
}

function convertPredict(type) {
    if (type == 'home') {
        return '1';
    } else if (type == 'away') {
        return '2';
    } else {
        return 'x';
    }
}

Date.prototype.addHours = function (h) {
    this.setHours(this.getHours() + h);
    return this;
}

function capitaliseFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

function howmany(classname) {
    return document.getElementsByClassName(classname).length;
}