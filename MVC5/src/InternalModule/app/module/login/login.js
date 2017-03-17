(function () {
    'use strict';

    angular
       .module('inspinia.login')
       .controller('LoginController', LoginController);

    LoginController.$inject = ['$scope', '$http', 'userService', '$location'];

    function LoginController($scope, $http, userService, $location)
    {
        $scope.model = {};
        $scope.form = {};
        var model = getRegisterTemp();
        $scope.model = model;
        $scope.modelexId = model.externalId;
        //var fragment = getFragment();
        //console.log(fragment);
        //if (typeof (fragment.error) !== "undefined") {
        //    console.log(fragment.error);
        //} else if (typeof (fragment.access_token) !== "undefined") {
        //    $http({
        //        method: 'POST',
        //        url: '/api/account/userInfo',
        //        headers: {
        //            "Authorization": "Bearer " + fragment.access_token
        //        }
        //    }).success(function (data) {
        //        if (data.HasRegistered) {
        //            clearRegisterTemp();

        //            setAccessToken(fragment.access_token, data.UserName, false);
        //            //location.href = '/#/user/index';
        //        } else {
        //            clearRegisterTemp();
        //            setRegisterTemp(fragment.access_token, '', data.UserName, data.FirstName, data.LastName, data.Email, '');
        //            $scope.modelexId = data.ExternalId;
        //            console.log($scope.modelexId);
        //            sessionStorage["isFbRegister"] = true;
        //        }
        //    }).error(function (data) {
        //        console.log(data);
        //    });
        //}

        $scope.login = function () {
            $scope.loginModel = {
                userName: $scope.form.userName,
                passWord: $scope.form.passWord,
                grant_type: "password"
            };

            userService.login($scope.loginModel).success(function (result) {
                setAccessToken(result.access_token, result.userName, false);
                $location.path("/user/index");
            }).error(function(result){
                $scope.isLogginIn = false;
                $scope.isInValidLogin = true;
                $scope.errorMessage = result.error_description;
                console.log(result);
            });
        }

        $scope.fbLogin = function () {
            $scope.isExtLogginIn = true;
            ///home/index
            $http({
                method: 'GET',
                url: '/api/account/externalLogins?returnUrl=/home/index&generateState=true',
            }).success(function (data) {
                sessionStorage["isFbRegister"] = true;
                window.location = data[0].Url;
            }).error(function (data) {
                $scope.isExtLogginIn = false;
                console.log(data);
            });
        }

        $scope.twLogin = function () {
            $scope.isExtLogginIn = true;
            $http({
                method: 'GET',
                url: '/api/account/externalLogins?returnUrl=/home/index&generateState=true',
            }).success(function (data) {
                sessionStorage["istwRegister"] = true;
                window.location = data[1].Url;
            }).error(function (data) {
                $scope.isExtLogginIn = false;
                console.log(data);
            });
        }

        $scope.registerEx = function () {
            var tempData = getRegisterTemp();
            var data = {
                UserName: $scope.form.UserName,
                Email: $scope.form.Email,
                Password: $scope.form.Password,
                ConfirmPassword: $scope.form.ConfirmPassword,
                FirstName: $scope.form.FirstName,
                LastName: $scope.form.LastName,
                Role: "User"
            };
            //console.log(data);
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
                    sessionStorage.removeItem("istwRegister");
                    $http({
                        method: 'POST',
                        url: '/api/Token',
                        data: $.param({
                            userName: $scope.form.UserName,
                            password: $scope.form.Password,
                            grant_type: 'password'
                        }),
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                    }).success(function (data1) {
                        setAccessToken(data1.access_token, data1.userName, false);
                        location.href = '/#/user/index';
                    }).error(function (data1) {
                    });
                }).error(function (data) {
                    $scope.regHasError = true;
                    $scope.regErrors = data.modelState;
                });
            }
        }
    }
})();