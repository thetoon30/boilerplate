(function() {
    'use strict';

    angular
        .module('inspinia.miscellaneous')
        .controller('notifyCtrl', notifyCtrl);

/**
 * notifyCtrl - Controller angular notify
 */
function notifyCtrl($scope, notify) {
    $scope.msg = 'Hello! This is a sample message!';
    $scope.demo = function () {
        notify({
            message: $scope.msg,
            classes: $scope.classes,
            templateUrl: $scope.template
        });
    };
    $scope.closeAll = function () {
        notify.closeAll();
    };

    $scope.inspiniaTemplate = 'app/component/views/common/notify.html';
    $scope.inspiniaDemo1 = function(){
        notify({ message: 'Info - This is a Inspinia info notification', classes: 'alert-info', templateUrl: $scope.inspiniaTemplate});
    }
    $scope.inspiniaDemo2 = function(){
        notify({ message: 'Success - This is a Inspinia success notification', classes: 'alert-success', templateUrl: $scope.inspiniaTemplate});
    }
    $scope.inspiniaDemo3 = function(){
        notify({ message: 'Warning - This is a Inspinia warning notification', classes: 'alert-warning', templateUrl: $scope.inspiniaTemplate});
    }
    $scope.inspiniaDemo4 = function(){
        notify({ message: 'Danger - This is a Inspinia danger notification', classes: 'alert-danger', templateUrl: $scope.inspiniaTemplate});
    }
}
})();
