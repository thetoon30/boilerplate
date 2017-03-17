(function() {
    'use strict';

    angular
        .module('inspinia.miscellaneous')
        .controller('liveFavicon', liveFavicon);

/**
 * liveFavicon - Controller for live favicon
 */
function liveFavicon($scope){
    $scope.example1 = function(){
        Tinycon.setBubble(1);
        Tinycon.setOptions({
            background: '#f03d25'
        });
    }

    $scope.example2 = function(){
        Tinycon.setBubble(1000);
        Tinycon.setOptions({
            background: '#f03d25'
        });
    }

    $scope.example3 = function(){
        Tinycon.setBubble('In');
        Tinycon.setOptions({
            background: '#f03d25'
        });
    }

    $scope.example4 = function(){
        Tinycon.setOptions({
            background: '#e0913b'
        });
        Tinycon.setBubble(8);
    }
}
})();
