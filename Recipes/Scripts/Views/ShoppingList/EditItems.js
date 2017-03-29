var myApp = angular.module("myApp", []);

var shoppingListEditItemsController = myApp.controller("shoppingListEditItemsController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    var vm = this;
    $scope.isBusy = true;

    $scope.styles = [];
    $scope.styles["<Unknown>"] = "{ 'background-color': 'rgba(0,0,0,0.1)' }";
    $scope.styles["Produce"] = "{ 'background-color': 'rgba(0,255,0,0.3)' }";
    $scope.styles["Meat"] = "{ 'background-color': 'rgba(255,0,0,0.3)' }";
    $scope.styles["Dairy"] = "{ 'background-color': 'rgba(255,255,0,0.50)' }";
    $scope.styles["Deli"] = "{ 'background-color': 'rgba(252,158,0,0.50)' }";
    $scope.styles["Soap"] = "{ 'background-color': 'rgba(25,182,255,0.39)' }";
    $scope.styles["Paper"] = "{ 'background-color': 'rgba(255,0,0,0.1)' }";
    $scope.styles["Sam's"] = "{ 'background': 'linear-gradient(to right, #3b6799 0%,#fcfcfc 20%,#fff2f2 80%,#ff5e5e 100%);' }";
    $scope.styles["Other"] = "{ 'background-color': 'rgba(255,0,0,0.1)' }";


    $scope.init = function (model) {
        $scope.model = model;
        $scope.selectedItems = [];
        $log.debug($scope.model);
        $scope.setGroupStyles();

        $scope.model.forEach(function (group) {
            group.Items.forEach(function(item) {
                item.Group = group;
            });
        });


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
            var style = $scope.styles[group.Text];
            group.Style = style;
            group.Items.forEach(function (item) {
                item.Style = style;
            });
        });
    };

    $scope.groupChecked = function (group) {
        vm.selectedGroup = group;
        //$scope.setGroup();
    };

    $scope.setGroup = function () {
        if (undefined !== $scope.selectedGroup) {
            if ($scope.selectedItems.length > 0) {
                $scope.selectedItems.forEach(function (item) {
                    item.Group.Items.RemoveGroupItem(item);
                    $scope.selectedGroup.Items.push(item);
                    item.Style = $scope.selectedGroup.Style;
                    item.Group = $scope.selectedGroup;
                });
            }
        }
    };

    Array.prototype.RemoveGroupItem = function (item) {
        for (i = 0; i < this.length; i++) {
            if (this[i].ShoppingListItemId === item.ShoppingListItemId) {
                this.splice(i, 1);
                break;
            }
        }
    };



    $scope.itemChecked = function (item) {
        if (item.IsSelected)
            $scope.selectedItems.push(item);
        else
            $scope.selectedItems.RemoveSelectedItem(item);
        $scope.setGroup();
    };

    Array.prototype.RemoveSelectedItem = function (item) {
        for (i = 0; i < this.length; i++) {
            if (this[i].ShoppingListItemId === item.ShoppingListItemId) {
                this.splice(i, 1);
                break;
            }
        }
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

