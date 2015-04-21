'use strict';

angular.module('debtManagerApp.user', [])
    .factory('userFactory', ['$http', function ($http) {

        var urlBase = '/api/Users';
        var userFactory = {};

        userFactory.get = function () {
            return $http.get(urlBase);
        };

        return userFactory;
    }]);