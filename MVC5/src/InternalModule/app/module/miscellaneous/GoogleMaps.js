(function() {
    'use strict';

    angular
        .module('inspinia.miscellaneous')
        .controller('GoogleMaps', GoogleMaps);

    /**
     * GoogleMaps - Controller for data google maps
     */
    function GoogleMaps($scope) {
    $scope.mapOptions = {
        zoom: 11,
        center: new google.maps.LatLng(40.6700, -73.9400),
        // Style for Google Maps
        styles: [{"featureType":"water","stylers":[{"saturation":43},{"lightness":-11},{"hue":"#0088ff"}]},{"featureType":"road","elementType":"geometry.fill","stylers":[{"hue":"#ff0000"},{"saturation":-100},{"lightness":99}]},{"featureType":"road","elementType":"geometry.stroke","stylers":[{"color":"#808080"},{"lightness":54}]},{"featureType":"landscape.man_made","elementType":"geometry.fill","stylers":[{"color":"#ece2d9"}]},{"featureType":"poi.park","elementType":"geometry.fill","stylers":[{"color":"#ccdca1"}]},{"featureType":"road","elementType":"labels.text.fill","stylers":[{"color":"#767676"}]},{"featureType":"road","elementType":"labels.text.stroke","stylers":[{"color":"#ffffff"}]},{"featureType":"poi","stylers":[{"visibility":"off"}]},{"featureType":"landscape.natural","elementType":"geometry.fill","stylers":[{"visibility":"on"},{"color":"#b8cb93"}]},{"featureType":"poi.park","stylers":[{"visibility":"on"}]},{"featureType":"poi.sports_complex","stylers":[{"visibility":"on"}]},{"featureType":"poi.medical","stylers":[{"visibility":"on"}]},{"featureType":"poi.business","stylers":[{"visibility":"simplified"}]}],
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    $scope.mapOptions2 = {
        zoom: 11,
        center: new google.maps.LatLng(40.6700, -73.9400),
        // Style for Google Maps
        styles: [{"featureType":"all","elementType":"all","stylers":[{"invert_lightness":true},{"saturation":10},{"lightness":30},{"gamma":0.5},{"hue":"#435158"}]}],
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    $scope.mapOptions3 = {
        center: new google.maps.LatLng(36.964645, -122.01523),
        zoom: 18,
        // Style for Google Maps
        MapTypeId: google.maps.MapTypeId.SATELLITE,
        styles: [{"featureType":"road","elementType":"geometry","stylers":[{"visibility":"off"}]},{"featureType":"poi","elementType":"geometry","stylers":[{"visibility":"off"}]},{"featureType":"landscape","elementType":"geometry","stylers":[{"color":"#fffffa"}]},{"featureType":"water","stylers":[{"lightness":50}]},{"featureType":"road","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"transit","stylers":[{"visibility":"off"}]},{"featureType":"administrative","elementType":"geometry","stylers":[{"lightness":40}]}],
        mapTypeId: google.maps.MapTypeId.SATELLITE
    };
    $scope.mapOptions4 = {
        zoom: 11,
        center: new google.maps.LatLng(40.6700, -73.9400),
        // Style for Google Maps
        styles: [{"stylers":[{"hue":"#18a689"},{"visibility":"on"},{"invert_lightness":true},{"saturation":40},{"lightness":10}]}],
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
}
})();
