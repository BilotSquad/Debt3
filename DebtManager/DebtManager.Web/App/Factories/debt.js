'use strict';

angular.module('debtManagerApp.debt')
    .factory('debtFactory', ['$http', function ($http) {

        var urlBase = '/api/Debts';
        var debtFactory = {};

        debtFactory.get = function () {
            return $http.get(urlBase);
        };

        debtFactory.create = function (entity) {
            return $http.post(urlBase, entity);
        };

        return debtFactory;
    }]);