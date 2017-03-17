/**
 * INSPINIA - Responsive Admin Theme
 *
 */
(function () {
    angular.module('inspinia', [
        'ui.router',                    // Routing
        'oc.lazyLoad',                  // ocLazyLoad
        'ui.bootstrap',                 // Ui Bootstrap
        'pascalprecht.translate',       // Angular Translate
        'ngIdle',                       // Idle timer
        'ngSanitize',                   // ngSanitize

        'inspinia.layout',
        'inspinia.core',
        'inspinia.login',
        'inspinia.user',
        'inspinia.view',
        'inspinia.chart',
        'inspinia.dashboard',
        'inspinia.form',
        'inspinia.metric',
        'inspinia.miscellaneous',
        'inspinia.table',
        'inspinia.ui'
    ])
})();

// Other libraries are loaded dynamically in the config.js file using the library ocLazyLoad