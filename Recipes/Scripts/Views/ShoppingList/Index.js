var myApp = angular.module("myApp", []);

var shoppingListIndexController = myApp.controller("shoppingListIndexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

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
        vm.model = model;
        $log.debug(vm.model);
        $scope.setGroupStyles();
    };

    $scope.setGroupStyles = function () {
        vm.model.Groups.forEach(function (group) {
            var style = vm.styles[group.Text];
            group.Style = style;
            group.Items.forEach(function (item) {
                item.Style = style;
            });
        });
    };

    $scope.collapseClicked = function (btn) {
        $("#btn_toggle_checked")
            .find('span')
            .toggleClass('glyphicon glyphicon-menu-up')
            .toggleClass('glyphicon glyphicon-menu-down');
    };

    $scope.getAllItems = function () {
        var result = [];
        vm.model.Groups.forEach(function (group) {
            Array.prototype.push.apply(result, group.Items);
        });
        return result;
    };

    $scope.addItem = function () {
        var items = $scope.getAllItems();
        var item = items.firstOrDefault({ Text: vm.newItem });
        if (null !== item) {//prevent dupes
            item.IsChecked = false;
        }
        else {
            var item = { ShoppingListItemId: 0, Text: vm.newItem, IsChecked: false };
            vm.model.Groups[0].Items.push(item);
        }
        vm.newItem = "";
    }

    $scope.newItem_Changed = function titleCase() {
        var splitStr = vm.newItem.toLowerCase().split(' ');
        for (var i = 0; i < splitStr.length; i++) {
            // You do not need to check if i is larger than splitStr length, as your for does that for you
            // Assign it back to the array
            splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
        }
        // Directly return the joined string
        vm.newItem = splitStr.join(' ');
    }

    $scope.saveShoppingList = function () {
        $http({
            method: 'POST',
            url: 'ShoppingList/UpdateShoppingList',
            data: { shoppingList: vm.model },
        }).success(function (data, status, headers, config) {
            vm.message = '';
            if (data.success == false) {
                var str = '';
                for (var error in data.errors) {
                    str += data.errors[error] + '\n';
                }
                vm.message = str;
            }
            else {
                vm.message = 'Saved Successfully';
                //$window.location.href("/Courses/Index");

            }
        }).error(function (data, status, headers, config) {
            vm.message = 'Unexpected Error';
        });
    };


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

