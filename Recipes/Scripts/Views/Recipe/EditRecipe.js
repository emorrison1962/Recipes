var myApp = angular.module("myApp", ['ngTagsInput']);

var editRecipeController = myApp.controller("editRecipeController", ['$scope', '$window', '$log', '$http', function ($scope, $window, $log, $http) {

    $scope.isBusy = true;
    $scope.recipe;
    $scope.tagCatalog = [];
    $scope.selectedTags = [];

    $scope.init = function (model) {
        $scope.recipe = model.Recipe;

        for (var i = 0; i < model.TagCatalog.length; ++i) {
            var tag = model.TagCatalog[i];
            tag.isChecked = false;
            $scope.tagCatalog[i] = tag;
            if (tag.isChecked)
                $scope.selectedTags.push(tag);
        }
    }//init


    $scope.onTagChecked = function (tag) {
        if (tag.isChecked) {
            $scope.selectedTags.push(tag);
        }
        else {
            var index = $scope.selectedTags.indexOf(tag);
            if (index > -1) {
                $scope.selectedTags.splice(index, 1);
            }
        }
    }

    $scope.onTagRemoved = function (tag) {
        var originalTag = $scope.tagCatalog.findTag(tag);
        originalTag.isChecked = false;
    }

    $scope.onTagAdded = function (tag) {
        var originalTag = $scope.tagCatalog.findTag(tag);
        originalTag.isChecked = true;
    }

    $scope.getTagCatalog = function (query) {
        return $scope.tagCatalog;
    }

    $scope.tagCatalog.findTag = function (tag) {
        for (i = 0; i < this.length; i++) {
            if (this[i].TagId == tag.TagId)
                return this[i];
        }
    }

    $scope.saveRecipe = function () {
        $scope.recipe.Tags = $scope.selectedTags;

        $http.post('/Recipe/UpdateRecipe', { recipe: $scope.recipe })
            .success(function (response) {
                $log.info(response)
            })
            .error(function (data, response) {
                $log.error(response);
            });
    }

}]);

