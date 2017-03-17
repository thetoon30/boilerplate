(function() {
    'use strict';

    angular
        .module('inspinia.form')
        .controller('wizardCtrl', wizardCtrl);

/**
 * wizardCtrl - Controller for wizard functions
 * used in Wizard view
 */
function wizardCtrl($scope, $rootScope) {
    // All data will be store in this object
    $scope.formData = {};

    // After process wizard
    $scope.processForm = function() {
        alert('Wizard completed');
    };
}
})();
