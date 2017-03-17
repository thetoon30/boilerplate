(function() {
    'use strict';

    angular
        .module('inspinia.chart')
        .controller('chartistCtrl', chartistCtrl);

/**
 * chartistCtrl - Controller for Chartist library
 */
function chartistCtrl() {

    this.lineData = {
        labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
        series: [
            [12, 9, 7, 8, 5],
            [2, 1, 3.5, 7, 3],
            [1, 3, 4, 5, 6]
        ]
    }

    this.lineOptions = {
        fullWidth: true,
        chartPadding: {
            right: 40
        }
    }

    var times = function (n) {
        return Array.apply(null, new Array(n));
    };

    var prepareData = times(26).map(Math.random).reduce(function (data, rnd, index) {
        data.labels.push(index + 1);
        data.series.forEach(function (series) {
            series.push(Math.random() * 100)
        });

        return data;
    }, {
        labels: [],
        series: times(4).map(function () {
            return new Array()
        })
    });

    this.scatterData = prepareData;

    this.scatterOptions = {
        showLine: false,
        axisX: {
            labelInterpolationFnc: function (value, index) {
                return index % 13 === 0 ? 'W' + value : null;
            }
        }
    }

    this.stackedData = {
        labels: ['Q1', 'Q2', 'Q3', 'Q4'],
        series: [
            [800000, 1200000, 1400000, 1300000],
            [200000, 400000, 500000, 300000],
            [100000, 200000, 400000, 600000]
        ]
    }
    this.stackedOptions = {
        stackBars: true,
        axisY: {
            labelInterpolationFnc: function (value) {
                return (value / 1000) + 'k';
            }
        }
    }

    this.horizontalData = {
        labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
        series: [
            [5, 4, 3, 7, 5, 10, 3],
            [3, 2, 9, 5, 4, 6, 4]
        ]
    }

    this.horizontalOptions = {
        seriesBarDistance: 10,
        reverseData: true,
        horizontalBars: true,
        axisY: {
            offset: 70
        }
    }

    var prepareData = {
        series: [5, 3, 4]
    }

    this.pieData = prepareData

    var sum = function (a, b) {
        return a + b
    };

    this.pieOptions = {
        labelInterpolationFnc: function (value) {
            return Math.round(value / prepareData.series.reduce(sum) * 100) + '%';
        }
    }

    this.gaugeData = {
        series: [20, 10, 30, 40]
    }

    this.gaugeOptions = {
        donut: true,
        donutWidth: 60,
        startAngle: 270,
        total: 200,
        showLabel: false
    }

}
})();
