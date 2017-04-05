var myApp = angular.module("myApp", []);

var plannerIndexController = myApp.controller("plannerIndexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    var vm = this;
    vm.isBusy = true;

    $scope.init = function (model) {
        vm.model = model;
        $log.debug(vm.model);
    };

    $scope.recipeChecked = function (recipe) {
        if (recipe.IsChecked) {
            vm.model.Planner.Groups[0].Items.push({ PlannerItemId: 0, RecipeId: recipe.RecipeId, Recipe: recipe, Text: recipe.Name })
        }
        else {
            vm.model.Planner.Groups.forEach(function (group) {
                group.Items.RemoveItem(recipe);
            })
        }
    };

    Array.prototype.RemoveItem = function (recipe) {
        for (i = 0; i < this.length; i++) {
            if (this[i].RecipeId === recipe.RecipeId) {
                this.splice(i, 1);
                break;
            }
        }
    };

    $scope.savePlanner = function () {
        $http({
            method: 'POST',
            url: 'Planner/Update',
            data: { planner: vm.model.Planner },
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

