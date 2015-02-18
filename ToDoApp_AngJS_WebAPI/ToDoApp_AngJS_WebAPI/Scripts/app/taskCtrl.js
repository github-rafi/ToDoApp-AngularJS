(function () {
    'use strict'; 
    
    //create angularjs controller
    var app = angular.module('app', []);//set and get the angular module

app.controller('taskController', ['$scope', '$http', taskController]);



//angularjs controller method
function taskController($scope, $http) {

    //declare variable for mainain ajax load and entry or edit mode
    $scope.loading = true;
    $scope.addMode = false;


 


    //get all task information
    $http.get('/api/Task/').success(function (data) {
        $scope.tasks = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

    //by pressing toggleEdit button ng-click in html, this method will be hit
    $scope.toggleEdit = function () {
        this.task.editMode = !this.task.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {

    
        $scope.addMode = !$scope.addMode;

     
    //   $scope.Priorities = [
// { label: 'High', value: 'High' },
//{ label: 'Medium', value: 'Medium' },
 //{ label: 'Low', value: 'Low' }
    //    ];
        //  this.newtask.Priority = "High";
       // $scope.newtask.Priority = "High";
     
        //    document.getElementById("priorityadd").options[1].selected = true;

        $scope.newtask = {
            TaskName: '',
            Priority: 'High',
            DueDate: ''
        };

    };

    //Inser Task
    $scope.add = function () {
        $scope.loading = true;
        $http.post('/api/Task/', this.newtask).success(function (data) {
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.tasks.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Task! " + data;
            $scope.loading = false;
        });
    };

    //Edit Task
    $scope.save = function () {
        alert("Edit");
        $scope.loading = true;
        var frien = this.task;
        alert(frien);
        $http.put('/api/Task/' + frien.Id, frien).success(function (data) {
            alert("Saved Successfully!!");
            frien.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Task! " + data;
            $scope.loading = false;
        });
    };

    //Delete Task
    $scope.deletetask = function () {
        $scope.loading = true;
        var Id = this.task.Id;
        $http.delete('/api/Task/' + Id).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.tasks, function (i) {
                if ($scope.tasks[i].Id === Id) {
                    $scope.tasks.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Task! " + data;
            $scope.loading = false;
        });
    };

}

app.directive('datepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(function () {
                element.datepicker({
                    dateFormat: 'yy-mm-dd',
                    onSelect: function (date) {
                        scope.$apply(function () {
                            ngModelCtrl.$setViewValue(date);
                        });
                    }
                    
                });
            });
        }
    }
});





})();