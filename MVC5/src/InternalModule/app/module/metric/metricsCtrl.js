(function() {
    'use strict';

    angular
        .module('inspinia.metric')
        .controller('metricsCtrl', metricsCtrl);

/**
 * metricsCtrl - Controller for data for all Sparkline chart
 * used in Metrics view
 */
function metricsCtrl() {
    this.demo1Data = [34, 43, 43, 35, 44, 32, 44, 52];
    this.demo1Options = {
        type: 'line',
        width: '100%',
        height: '60',
        lineColor: '#1ab394',
        fillColor: "#ffffff"
    };

    this.demo2Data = [24, 43, 43, 55, 44, 62, 44, 72];
    this.demo2Options = {
        type: 'line',
        width: '100%',
        height: '60',
        lineColor: '#1ab394',
        fillColor: "#ffffff"
    };

    this.demo3Data = [74, 43, 23, 55, 54, 32, 24, 12];
    this.demo3Options = {
        type: 'line',
        width: '100%',
        height: '60',
        lineColor: '#ed5565',
        fillColor: "#ffffff"
    };

    this.demo4Data = [24, 43, 33, 55, 64, 72, 44, 22];
    this.demo4Options = {
        type: 'line',
        width: '100%',
        height: '60',
        lineColor: '#ed5565',
        fillColor: "#ffffff"
    };

    this.demo5Data = [1, 4];
    this.demo5Options = {
        type: 'pie',
        height: '140',
        sliceColors: ['#1ab394', '#F5F5F5']
    };

    this.demo6Data = [5, 3];
    this.demo6Options = {
        type: 'pie',
        height: '140',
        sliceColors: ['#1ab394', '#F5F5F5']
    };

    this.demo7Data = [2, 2];
    this.demo7Options = {
        type: 'pie',
        height: '140',
        sliceColors: ['#ed5565', '#F5F5F5']
    };

    this.demo8Data = [2, 3];
    this.demo8Options = {
        type: 'pie',
        height: '140',
        sliceColors: ['#ed5565', '#F5F5F5']
    };
}
})();
