(function() {
    'use strict';

    angular
        .module('inspinia.chart')
        .controller('rickshawChartCtrl', rickshawChartCtrl);

/**
 * rickshawChartCtrl - Controller for data for all Rickshaw chart
 * used in Rickshaw chart view
 */
function rickshawChartCtrl() {

    /**
     * Data for simple chart
     */
    var simpleChartSeries = [
        {
            color: '#1ab394',
            data: [
                { x: 0, y: 40 },
                { x: 1, y: 49 },
                { x: 2, y: 38 },
                { x: 3, y: 30 },
                { x: 4, y: 32 }
            ]
        }
    ];
    /**
     * Options for simple chart
     */
    var simpleChartOptions = {
        renderer: 'area'
    };

    /**
     * Data for Multi Area chart
     */
    var multiAreaChartSeries = [
        {
            data: [
                { x: 0, y: 40 },
                { x: 1, y: 49 },
                { x: 2, y: 38 },
                { x: 3, y: 20 },
                { x: 4, y: 16 }
            ],
            color: '#1ab394',
            stroke: '#17997f'
        },
        {
            data: [
                { x: 0, y: 22 },
                { x: 1, y: 25 },
                { x: 2, y: 38 },
                { x: 3, y: 44 },
                { x: 4, y: 46 }
            ],
            color: '#eeeeee',
            stroke: '#d7d7d7'
        }
    ];

    /**
     * Options for Multi chart
     */
    var multiAreaChartOptions = {
        renderer: 'area',
        stroke: true
    };

    /**
     * Options for one line chart
     */
    var oneLineChartOptions = {
        renderer: 'line'
    };

    /**
     * Data for one line chart
     */
    var oneLineChartSeries = [
        {
            data: [
                { x: 0, y: 40 },
                { x: 1, y: 49 },
                { x: 2, y: 38 },
                { x: 3, y: 30 },
                { x: 4, y: 32 }
            ],
            color: '#1ab394'
        }
    ];

    /**
     * Options for Multi line chart
     */
    var multiLineChartOptions = {
        renderer: 'line'
    };

    /**
     * Data for Multi line chart
     */
    var multiLineChartSeries = [
        {
            data: [
                { x: 0, y: 40 },
                { x: 1, y: 49 },
                { x: 2, y: 38 },
                { x: 3, y: 30 },
                { x: 4, y: 32 }
            ],
            color: '#1ab394'
        },
        {
            data: [
                { x: 0, y: 20 },
                { x: 1, y: 24 },
                { x: 2, y: 19 },
                { x: 3, y: 15 },
                { x: 4, y: 16 }
            ],
            color: '#d7d7d7'
        }
    ];

    /**
     * Options for Bars chart
     */
    var barsChartOptions = {
        renderer: 'bar'
    };

    /**
     * Data for Bars chart
     */
    var barsChartSeries = [
        {
            data: [
                { x: 0, y: 40 },
                { x: 1, y: 49 },
                { x: 2, y: 38 },
                { x: 3, y: 30 },
                { x: 4, y: 32 }
            ],
            color: '#1ab394'
        }
    ];

    /**
     * Options for Stacked chart
     */
    var stackedChartOptions = {
        renderer: 'bar'
    };

    /**
     * Data for Stacked chart
     */
    var stackedChartSeries = [
        {
            data: [
                { x: 0, y: 40 },
                { x: 1, y: 49 },
                { x: 2, y: 38 },
                { x: 3, y: 30 },
                { x: 4, y: 32 }
            ],
            color: '#1ab394'
        },
        {
            data: [
                { x: 0, y: 20 },
                { x: 1, y: 24 },
                { x: 2, y: 19 },
                { x: 3, y: 15 },
                { x: 4, y: 16 }
            ],
            color: '#d7d7d7'
        }
    ];

    /**
     * Options for Scatterplot chart
     */
    var scatterplotChartOptions = {
        renderer: 'scatterplot',
        stroke: true,
        padding: { top: 0.05, left: 0.05, right: 0.05 }
    }

    /**
     * Data for Scatterplot chart
     */
    var scatterplotChartSeries = [
        {
            data: [
                { x: 0, y: 15 },
                { x: 1, y: 18 },
                { x: 2, y: 10 },
                { x: 3, y: 12 },
                { x: 4, y: 15 },
                { x: 5, y: 24 },
                { x: 6, y: 28 },
                { x: 7, y: 31 },
                { x: 8, y: 22 },
                { x: 9, y: 18 },
                { x: 10, y: 16 }
            ],
            color: '#1ab394'
        }
    ]

    /**
     * Definition all variables
     * Rickshaw chart
     */
    this.simpleSeries = simpleChartSeries;
    this.simpleOptions = simpleChartOptions;
    this.multiAreaOptions = multiAreaChartOptions;
    this.multiAreaSeries = multiAreaChartSeries;
    this.oneLineOptions = oneLineChartOptions;
    this.oneLineSeries = oneLineChartSeries;
    this.multiLineOptions = multiLineChartOptions;
    this.multiLineSeries = multiLineChartSeries;
    this.barsOptions = barsChartOptions;
    this.barsSeries = barsChartSeries;
    this.stackedOptions = stackedChartOptions;
    this.stackedSeries = stackedChartSeries;
    this.scatterplotOptions = scatterplotChartOptions;
    this.scatterplotSeries = scatterplotChartSeries;

}
})();
