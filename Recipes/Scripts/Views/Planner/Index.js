var myApp = angular.module("myApp", ['as.sortable']);

var plannerIndexController = myApp.controller("plannerIndexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    var vm = this;
    vm.isBusy = true;

    $scope.init = function (model) {
        vm.model = model;
        $log.debug(vm.model);
    };

    //$scope.dragControlListeners = {
    //    accept: function (sourceItemHandleScope, destSortableScope) {return boolean}//override to determine drag is allowed or not. default is true.
    //    itemMoved: function (event) {//Do what you want},
    //    orderChanged: function(event) {//Do what you want},
    //            containment: '#board'//optional param.
    //            clone: true //optional param for clone feature.
    //            allowDuplicates: false //optional param allows duplicates to be dropped.
    //    };

    $scope.dragControlListeners = {
        //containment: '#board'//optional param.
        //allowDuplicates: true //optional param allows duplicates to be dropped.
    };

    $scope.mouseover = function () {
        var x = 0;
        //var ctx = canvas.getContext("2d");
        //ctx.fillStyle = "#FF0000";
        //ctx.fillRect(0, 0, 80, 80);
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

