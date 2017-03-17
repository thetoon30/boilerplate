(function () {
    'use strict';

    angular
        .module('inspinia.layout')
        .controller('LayoutController', LayoutController);

    LayoutController.$inject = ['$scope', '$http', 'loggedInUser', 'userService', '$location'];

    function LayoutController($scope, $http, loggedInUser, userService, $location) {
        $scope.loggedInUser = loggedInUser;

        $scope.logout = logout;

        function logout() {
            userService.logout().success(function (data) {
                clearAccessToken();
                $location.path("login");
            }).error(function(data){
                console.log(data);
            });
        }
    };
})();
