var myApp = angular.module("myApp", []);

var shoppingListEditItemsController = myApp.controller("shoppingListEditItemsController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    $scope.isBusy = true;

    $scope.init = function (model) {
        $scope.model = model;
        $log.debug($scope.model);
        $scope.setGroupStyles();


    };

    $scope.getAllItems = function () {
        var result = [];
        $scope.model.forEach(function (group) {
            Array.prototype.push.apply(result, group.Items);
        });
        return result;
    };

    $scope.setGroupStyles = function ()
    {
        $scope.model.forEach(function (group) {
            //if (group.Text == )
            group.Style = "{ 'background-color': 'blue' }";
        });
    };

    //////////////////////////////////////////////////////////////////////////////////////
    Array.prototype.where = function (filter) {
        var collection = this;
        switch (typeof filter) {

            case 'function':
                return $.grep(collection, filter);

            case 'object':
                for (var property in filter) {
                    if (!filter.hasOwnProperty(property))
                        continue; // ignore inherited properties

                    collection = $.grep(collection, function (item) {
                        return item[property] === filter[property];
                    });
                }
                return collection.slice(0); // copy the array 
                // (in case of empty object filter)

            default:
                throw new TypeError('func must be either a' +
                    'function or an object of properties and values to filter by');
        }
    };

    Array.prototype.firstOrDefault = function (func) {
        return this.where(func)[0] || null;
    };

}]);

