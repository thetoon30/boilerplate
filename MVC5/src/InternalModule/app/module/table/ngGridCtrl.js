(function() {
    'use strict';

    angular
        .module('inspinia.table')
        .controller('ngGridCtrl', ngGridCtrl);

    /**
     * ngGridCtrl - Controller for code ngGrid
     */
    function ngGridCtrl($scope) {
        $scope.ngData = [
            {Name: "Moroni", Age: 50, Position: 'PR Menager', Status: 'active', Date: '12.12.2014'},
            {Name: "Teancum", Age: 43, Position: 'CEO/CFO', Status: 'deactive', Date: '10.10.2014'},
            {Name: "Jacob", Age: 27, Position: 'UI Designer', Status: 'active', Date: '09.11.2013'},
            {Name: "Nephi", Age: 29, Position: 'Java programmer', Status: 'deactive', Date: '22.10.2014'},
            {Name: "Joseph", Age: 22, Position: 'Marketing manager', Status: 'active', Date: '24.08.2013'},
            {Name: "Monica", Age: 43, Position: 'President', Status: 'active', Date: '11.12.2014'},
            {Name: "Arnold", Age: 12, Position: 'CEO', Status: 'active', Date: '07.10.2013'},
            {Name: "Mark", Age: 54, Position: 'Analyst', Status: 'deactive', Date: '03.03.2014'},
            {Name: "Amelia", Age: 33, Position: 'Sales manager', Status: 'deactive', Date: '26.09.2013'},
            {Name: "Jesica", Age: 41, Position: 'Ruby programmer', Status: 'active', Date: '22.12.2013'},
            {Name: "John", Age: 48, Position: 'Marketing manager', Status: 'deactive', Date: '09.10.2014'},
            {Name: "Berg", Age: 19, Position: 'UI/UX Designer', Status: 'active', Date: '12.11.2013'}
        ];

        $scope.ngOptions = { data: 'ngData' };
        $scope.ngOptions2 = {
            data: 'ngData',
            showGroupPanel: true,
            jqueryUIDraggable: true
        };
    }
})();
