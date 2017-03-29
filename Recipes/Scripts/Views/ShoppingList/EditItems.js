var myApp = angular.module("myApp", []);

var shoppingListEditItemsController = myApp.controller("shoppingListEditItemsController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    var vm = this;
    vm.isBusy = true;

    vm.styles = [];
    vm.styles["<Unknown>"] = "{ 'background-color': 'rgba(0,0,0,0.05)' }";
    vm.styles["Produce"] = "{ 'background-color': 'rgba(0,255,0,0.2)' }";
    vm.styles["Meat"] = "{ 'background-color': 'rgba(255,0,0,0.2)' }";
    vm.styles["Dairy"] = "{ 'background-color': 'rgba(255,255,0,0.50)' }";
    vm.styles["Deli"] = "{ 'background-color': 'rgba(252,158,0,0.40)' }";
    vm.styles["Soap"] = "{ 'background-color': 'rgba(25,182,255,0.30)' }";
    vm.styles["Paper"] = "{ 'background-color': 'rgba(255,0,0,0.1)' }";
    vm.styles["Sam's"] = "{ 'background': 'linear-gradient(to right, #2989d8 0%,#ffffff 15%,#ffffff 85%,#a2b320 100%,#01549e 100%);' }";
    vm.styles["Other"] = "{ 'background-color': 'rgba(255,0,0,0.1)' }";


    $scope.init = function (model) {
        vm.groups = model;
        vm.selectedItems = [];
        $log.debug(vm.groups);
        $scope.setGroupStyles();

        vm.groups.forEach(function (group) {
            group.Items.forEach(function(item) {
                item.Group = group;
            });
        });


    };

    $scope.getAllItems = function () {
        var result = [];
        vm.groups.forEach(function (group) {
            Array.prototype.push.apply(result, group.Items);
        });
        return result;
    };

    $scope.setGroupStyles = function ()
    {
        vm.groups.forEach(function (group) {
            var style = vm.styles[group.Text];
            group.Style = style;
            group.Items.forEach(function (item) {
                item.Style = style;
            });
        });
    };

    $scope.groupChecked = function (group) {
        this.setGroup();
    };

    $scope.setGroup = function () {
        if (vm.selectedGroup !== undefined && vm.selectedItems.length > 0) {
            vm.selectedItems.forEach(function (item) {
                item.Group.Items.RemoveGroupItem(item);
                vm.selectedGroup.Items.push(item);
                item.Style = vm.selectedGroup.Style;
                item.Group = vm.selectedGroup;
                item.IsSelected = false;
            });
            vm.selectedItems = [];
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
            vm.selectedItems.push(item);
        else
            vm.selectedItems.RemoveSelectedItem(item);
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

