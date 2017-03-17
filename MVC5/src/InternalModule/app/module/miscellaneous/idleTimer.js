(function() {
    'use strict';

    angular
        .module('inspinia.miscellaneous')
        .controller('idleTimer', idleTimer);

    /**
     * idleTimer - Controller for Idle Timer
     */
    function idleTimer($scope, Idle, notify) {

    // Custm alter
    $scope.customAlert = false;

    // Start watching idle
    Idle.watch();

    // Config notify behavior
    notify.config({
       duration: '5000'
    });

    // function you want to fire when the user goes idle
    $scope.$on('IdleStart', function () {
        notify({
            message: 'Idle time - You can call any function after idle timeout.',
            classes: 'alert-warning',
            templateUrl: 'app/component/views/common/notify.html'
        });
        $scope.customAlert = true;

    });

    // function you want to fire when the user becomes active again
    $scope.$on('IdleEnd', function () {
        notify({
            message: 'You are back, Great that you decided to move a mouse.',
            classes: 'alert-success',
            templateUrl: 'app/component/views/common/notify.html'
        });
        $scope.customAlert = false;
    });

}
})();
