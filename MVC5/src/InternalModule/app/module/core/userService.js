(function () {
    'use strict';

    angular
        .module('inspinia.core')
        .factory('userService', userService);

    userService.$inject = ['$q', '$http'];

    function userService($q, $http) {

        var service = {
            getAllUser: getAllUser,
            getAllRole: getAllRole,
            register: register,
            postChangePassword: postChangePassword,
            postAddToRole : postAddToRole,
            searchUser: searchUser,
            updateUser: updateUser,
            deleteUser: deleteUser,
            getUserById: getUserById,
            login: login,
            logout: logout,
            userInfo: userInfo
        };

        return service;

        function getAllUser(start, max) {
            return $http.get('api/Account/User/?start=' + start + '&max=' + max, { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function getAllRole() {
            return $http.get('api/Account/Role', { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function register(data) {
            return $http.post('api/Account/Register', data, { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function postChangePassword(pw) {
            return $http({
                method: 'POST',
                url: '/api/account/changePassword',
                data: {
                    OldPassword: pw.OldPassword,
                    newPassword: pw.newPassword,
                    confirmPassword: pw.confirmPassword
                },
                headers: getSecurityHeaders()
            }).success(getDataComplete);

            function getDataComplete(data) {
                return data;
            }
        }

        function postAddToRole(id, role) {
            return $http({
                method: 'POST',
                url: '/api/account/AddToRole?id=' + id,
                data: role,
                headers: getSecurityHeaders()
            }).success(getDataComplete);

            function getDataComplete(data) {
                return data;
            }
        }

        function searchUser(start, max, employeeCode, userName, fullName, roleId) {
            return $http.get('api/Account/Search/?start=' + start + '&max=' + max + '&ec=' + employeeCode + '&un=' + userName + '&fn=' + fullName + '&ro=' + roleId, { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function updateUser(id, data) {
            return $http.put('api/Account/User/' + id, data, { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function deleteUser(id) {
            return $http.post('api/Account/User/' + id, '', { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function getUserById(id) {
            return $http.get('api/Account/User/' + id, { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function login(data) {
            return $http.post('/api/Token', $.param(data), { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }

        }

        function logout() {
            return $http.post('api/Account/Logout', '', { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }

        function userInfo() {
            return $http.get('/api/Account/UserInfo', { headers: getSecurityHeaders() })
                .success(getDataComplete)
                .error(function (data) {
                    console.log(data);
                });

            function getDataComplete(data) {
                return data;
            }
        }
    }
})();
