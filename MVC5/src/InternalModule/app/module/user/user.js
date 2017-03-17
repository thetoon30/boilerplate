(function () {
    'use strict';

    angular
       .module('inspinia.user')
       .controller('UserAccountController', UserAccountController);

    UserAccountController.$inject = ['$scope', 'userService', '$stateParams', '$location'];

    function UserAccountController($scope, userService, $stateParams, $location)
    {
        $scope.currentPage = 1;

        $scope.formDetail = {};

        $scope.isRoleSaving = false;

        $scope.total = 10;

        $scope.totals = [10, 20, 30, 50, 100];

        $scope.dataStart = ($scope.currentPage * $scope.total) - $scope.total;

        $scope.isActive = isActive;

        $scope.selectTotal = selectTotal;

        $scope.selectPage = selectPage;

        $scope.selectPrevious = selectPrevious;

        $scope.selectNext = selectNext;

        $scope.saveAddUser = saveAddUser;

        $scope.searchUser = searchUser;

        $scope.showDetail = showDetail;

        $scope.saveRole = saveRole;

        $scope.saveEditUser = saveEditUser;

        $scope.deleteUser = deleteUser;

        getAllRole();

        if ($location.path().indexOf("index") > -1) {
            getAllUser();
        } else if($location.path().indexOf("edit") > -1){
            getUserById($stateParams.id);
        }

        function deleteUser(id) {
            userService.deleteUser(id).then(function (result) {
                getAllUser();
            });
        }

        function getAllUser() {
            userService.getAllUser($scope.dataStart, $scope.total).then(function (result) {
                $scope.users = result.data.items;
                $scope.totalUser = result.data.total;
                makePaging();
            });
        }

        function getUserById(id) {
            userService.getUserById(id).then(function (result) {
                $scope.form = result.data.item;
            });
        }

        function getAllRole() {
            userService.getAllRole().then(function (result) {
                $scope.roles = result.data.items;
            });
        }

        function isActive(page) {
            if (page == $scope.currentPage) {
                return "active";
            }
        }

        function makePaging() {
            $scope.pagings = [];
            $scope.totalPage = Math.ceil($scope.totalUser / $scope.total);
            for (var i = 1; i <= $scope.totalPage ; i++) {
                $scope.pagings.push(i);
            }

            $scope.dataStart = ($scope.currentPage * $scope.total) - $scope.total;
        }

        function saveAddUser() {
            userService.register($scope.form).then(function (result) {
                $location.path("/user/index");
            });
        }

        function saveEditUser() {
            userService.updateUser($stateParams.id, $scope.form).then(function (result) {
                $location.path("/user/index");
            });
        }

        function saveRole(id) {
            $scope.isRoleSaving = false;
            $scope.roleErrors = null;
            var role = [];
            role.push($scope.formDetail.checkrole);
            userService.postAddToRole(id, role).success(function (data) {
                $scope.isRoleSaving = true;
            }).error(function (error) {
                $scope.roleErrors = error.ModelState;
            });
        }

        function selectTotal(total) {
            $scope.currentPage = 1;
            $scope.total = total;
            $scope.dataStart = ($scope.currentPage * $scope.total) - $scope.total;
            searchOrGetAll();
        }

        function selectPage(page) {
            $scope.currentPage = page;
            $scope.dataStart = ($scope.currentPage * $scope.total) - $scope.total;
            searchOrGetAll();
        }

        function selectPrevious() {
            if ($scope.currentPage - 1 > 0) {
                $scope.currentPage = $scope.currentPage - 1;
                $scope.dataStart = ($scope.currentPage * $scope.total) - $scope.total;
                searchOrGetAll();
            }
        }

        function selectNext() {
            if ($scope.currentPage + 1 <= $scope.totalPage) {
                $scope.currentPage = $scope.currentPage + 1;
                $scope.dataStart = ($scope.currentPage * $scope.total) - $scope.total;
                searchOrGetAll();
            }
        }

        function searchUser() {
            if (!$scope.search.EmployeeCode) {
                $scope.search.EmployeeCode = '';
            }

            if (!$scope.search.UserName) {
                $scope.search.UserName = '';
            }

            if (!$scope.search.FullName) {
                $scope.search.FullName = '';
            }

            if (!$scope.search.Role) {
                $scope.search.Role = '';
            }

            userService.searchUser($scope.dataStart, $scope.total, $scope.search.EmployeeCode,$scope.search.UserName, $scope.search.FullName, $scope.search.Role).then(function (result) {
                $scope.isSearch = true;
                $scope.users = result.data.items;
                $scope.totalUser = result.data.total;
                $scope.currentPage = 1;
                makePaging();
            });
        }

        function searchUserNextTime() {
            if (!$scope.search.EmployeeCode) {
                $scope.search.EmployeeCode = '';
            }

            if (!$scope.search.UserName) {
                $scope.search.UserName = '';
            }

            if (!$scope.search.FullName) {
                $scope.search.FullName = '';
            }

            if (!$scope.search.Role) {
                $scope.search.Role = '';
            }

            userService.searchUser($scope.dataStart, $scope.total, $scope.search.EmployeeCode, $scope.search.UserName, $scope.search.FullName, $scope.search.Role).then(function (result) {
                $scope.users = result.data.items;
                $scope.totalUser = result.data.total;
                makePaging();
            });
        }

        function showDetail(data) {
            $scope.profile = data;
            $scope.formDetail.checkrole = $scope.profile.Role;
        }

        function searchOrGetAll() {
            if ($scope.isSearch) {
                searchUserNextTime();
            } else {
                getAllUser();
            }
        }
    }
})();