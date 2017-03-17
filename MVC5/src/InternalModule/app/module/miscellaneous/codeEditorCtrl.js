(function() {
    'use strict';

    angular
        .module('inspinia.miscellaneous')
        .controller('codeEditorCtrl', codeEditorCtrl);

/**
 * codeEditorCtrl - Controller for code editor - Code Mirror
 */
function codeEditorCtrl($scope) {
    $scope.editorOptions = {
        lineNumbers: true,
        matchBrackets: true,
        styleActiveLine: true,
        theme:"ambiance"
    };

    $scope.editorOptions2 = {
        lineNumbers: true,
        matchBrackets: true,
        styleActiveLine: true
    };

}
})();
