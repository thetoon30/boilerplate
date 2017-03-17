(function() {
    'use strict';

    angular
        .module('inspinia.dashboard')
        .controller('dashboardMap', dashboardMap);

    /**
     * dashboardMap - data for Map plugin
     * used in Dashboard 2 view
     */
    function dashboardMap() {
        var data = {
            "US": 298,
            "SA": 200,
            "DE": 220,
            "FR": 540,
            "CN": 120,
            "AU": 760,
            "BR": 550,
            "IN": 200,
            "GB": 120
        };
        this.data = data;
    }
})();
