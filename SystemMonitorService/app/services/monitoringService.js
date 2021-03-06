﻿myApp.factory("monitoringService", function ($timeout) {
    var url = "http://windows8vm.local:7774";
    var service = {};

    var connection = $.hubConnection(url);
    connection.logging = true;

    var hubProxy = connection.createHubProxy("performanceDataHub");

    service.init = function (scope) {
        hubProxy.on("newCpuValue", function (data) {
            scope.$broadcast("newCpuValue", data);
        });
        hubProxy.on("newDiskValue", function (data) {
            scope.$broadcast("newDiskValue", data);
        });

        connection.disconnected(function () {
            $timeout(function () {
                connection.start().done(function () {
                });
            }, 5000);
        });

        connection.start().done(function () {
        });
    }

    return service;
});