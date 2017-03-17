(function() {
    'use strict';

    angular
        .module('inspinia.miscellaneous')
        .controller('modalDemoCtrl', modalDemoCtrl);

    /**
     * modalDemoCtrl - Controller used to run modal view
     * used in Basic form view
     */
    function modalDemoCtrl($scope, $modal) {
        $scope.open = function () {

            var modalInstance = $modal.open({
                templateUrl: 'app/component/views/modal_example.html',
                controller: ModalInstanceCtrl
            });
        };

        $scope.open1 = function () {
            var modalInstance = $modal.open({
                templateUrl: 'app/component/views/modal_example1.html',
                controller: ModalInstanceCtrl
            });
        };

        $scope.open2 = function () {
            var modalInstance = $modal.open({
                templateUrl: 'app/component/views/modal_example2.html',
                controller: ModalInstanceCtrl,
                windowClass: "animated fadeIn"
            });
        };

        $scope.open3 = function (size) {
            var modalInstance = $modal.open({
                templateUrl: 'app/component/views/modal_example3.html',
                size: size,
                controller: ModalInstanceCtrl
            });
        };

        $scope.open4 = function () {
            var modalInstance = $modal.open({
                templateUrl: 'app/component/views/modal_example2.html',
                controller: ModalInstanceCtrl,
                windowClass: "animated flipInY"
            });
        };
    };
})();
