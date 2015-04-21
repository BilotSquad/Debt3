'use strict';

angular.module('debtManagerApp.dashboard')
    .factory('dashboardFactory', ['$http', function ($http) {

        var dashboardFactory = {};

        dashboardFactory.getAll = function () {
            return $http.get('/api/AggregatedDebts');
        };

        dashboardFactory.getAllMinimized = function () {
            return $http.get('/api/AggregatedMinimizedDebts');
        };

        return dashboardFactory;
    }]);