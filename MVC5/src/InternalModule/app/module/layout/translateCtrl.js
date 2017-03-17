(function() {
    'use strict';

    angular
        .module('inspinia.layout')
        .controller('translateCtrl', translateCtrl);

/**
 * translateCtrl - Controller for translate
 */
function translateCtrl($translate, $scope) {
    $scope.changeLanguage = function (langKey) {
        $translate.use(langKey);
    };
}
})();
