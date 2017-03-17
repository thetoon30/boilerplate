function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable();

    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();

    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();

    self.registerfbUser = ko.observable();
    self.registerfbEmail = ko.observable();
    self.registerfbPassword = ko.observable();
    self.registerfbPassword2 = ko.observable();
    self.registerfbFirstName = ko.observable();
    self.registerfbLastName = ko.observable();
    self.registerfbRole = ko.observable();
    self.registerfbRole("User");
    self.model = ko.observable();
    function showError(jqXHR) {
        self.result(jqXHR.status + ': ' + jqXHR.statusText);
    }

    var fragment = getFragment();
    if (typeof (fragment.error) !== "undefined") {
        console.log(fragment.error);
    } else if (typeof (fragment.access_token) !== "undefined") {
        $.ajax({
            type: 'POST',
            url: '/api/account/userInfo',
            headers: {
                "Authorization": "Bearer " + fragment.access_token
            }
        }).success(function (data) {
            if (data.HasRegistered) {
                clearRegisterTemp();

                setAccessToken(fragment.access_token, data.UserName, false);
                location.href = '/#/user/index';
            } else {
                clearRegisterTemp();
                setRegisterTemp(fragment.access_token, data.ExternalId, data.UserName, data.FirstName, data.LastName, data.Email, data.LoginProvider);
                location.href = '/#/exlogin';
                //self.model = getRegisterTemp();
                //console.log(self.model.userName);
                //self.registerfbUser(data.UserName);
                //self.registerfbEmail(data.Email);
                //self.registerfbFirstName(data.FirstName);
                //self.registerfbLastName(data.LastName);
                //sessionStorage["isFbRegister"] = true;
            }
        }).error(function (data) {
            console.log(data);
        });
    }


    self.callApi = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/values',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.register = function () {
        self.result('');

        var data = {
            UserName: "user3",
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2(),
            FirstName: "user3",
            LastName: "user3",
            Role: "User"
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
        }).fail(showError);
    }

    self.registerfb = function () {
        var tempData = getRegisterTemp();
        var data = {
            UserName: self.registerfbUser(),
            Email: self.registerfbEmail(),
            Password: self.registerfbPassword(),
            ConfirmPassword: self.registerfbPassword2(),
            FirstName: self.registerfbFirstName(),
            LastName: self.registerfbLastName(),
            Role: "User"
        };

        if (typeof (tempData.accessToken) !== 'undefined') {
            $.ajax({
                type: 'POST',
                url: '/api/account/registerExternal',
                data: JSON.stringify(data),
                headers: {
                    'Authorization': 'Bearer ' + tempData.accessToken,
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                clearRegisterTemp();
                sessionStorage.removeItem("isFbRegister");

                //$http({
                //    method: 'POST',
                //    url: '/token',
                //    data: $.param({
                //        userName: $scope.model.userName,
                //        password: $scope.model.password,
                //        grant_type: 'password'
                //    }),
                //    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                //}).success(function (data1) {
                //    setAccessToken(data1.access_token, data1.userName, false);
                //    location.href = '/home';
                //}).error(function (data1) {
                //});
            }).error(function (data) {
                //$scope.loadbusy = false;
                //$scope.regHasError = true;
                //$scope.regErrors = data.modelState;
            });
        }
    }

    self.login = function () {
        self.result('');

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);
        }).fail(showError);
    }

    self.fbLogin = function () {
        self.isExtLogginIn = true;

        $.ajax({
            type: 'GET',
            url: '/api/account/externalLogins?returnUrl=/home/index&generateState=true',
        }).success(function (data) {
            sessionStorage["isFbRegister"] = true;

            //var extLogin = $(data).filter(function () {
            //    return this.name === 'Facebook';
            //})[0];
            window.location = data[0].Url;
        }).error(function (data) {
            self.isExtLogginIn = false;
            console.log(data);
        });
    }

    self.twLogin = function () {
        self.isExtLogginIn = true;

        $.ajax({
            type: 'GET',
            url: '/api/account/externalLogins?returnUrl=/home/index&generateState=true',
        }).success(function (data) {
            sessionStorage["istwRegister"] = true;
            window.location = data[1].Url;
        }).error(function (data) {
            self.isExtLogginIn = false;
            console.log(data);
        });
    }

    self.logout = function () {
        self.user('');
        sessionStorage.removeItem(tokenKey)
    }
}

var app = new ViewModel();
ko.applyBindings(app);