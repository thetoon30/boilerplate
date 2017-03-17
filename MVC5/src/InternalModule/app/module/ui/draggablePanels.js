(function() {
    'use strict';

    angular
        .module('inspinia.ui')
        .controller('draggablePanels', draggablePanels);

/**
 * draggablePanels - Controller for draggable panels example
 */
function draggablePanels($scope) {

    $scope.sortableOptions = {
        connectWith: ".connectPanels",
        handler: ".ibox-title"
    };

}
})();
