'use strict';

app.controller("NotificationController", ['$scope', "WebApi", function ($scope, WebApi) {

    $scope.Notification = {};
    $scope.ChannelList = {};
    $scope.LanguagesList = {};
    $scope.CommunicationTypeList = {};

    $scope.ResetFields = function () {
        $scope.$apply(function () {
            $scope.Notification.ChannelId = 0;
            $scope.Notification.LanguageId = 0;
            $scope.Notification.CommunicationTypeId = 0;
            $scope.Notification.Name = "";
            $scope.Notification.BodyTemplate = "";
            $scope.Notification.UserCanTurnOff = 0;
            $scope.Notification.SentAttempts = 0;
        });
    }
    $scope.ResetFields();
    $scope.WebApiCommunication = WebApi.Communication();
    $scope.WebApiCore = WebApi.Core();



    $scope.CreateCommunication = function (communicationType) {
        $.post($scope.WebApiCommunication + '/api/CommunicationType/AddCommunicationTypeChannel', communicationType, $scope.CreateCommunicationSuccessCallback)
            .error($scope.CreateCommunicationErrorCallback);
    };

    $scope.CreateCommunicationSuccessCallback = function () {
        alert('sucesso');
        $scope.ResetFields();
    }

    $scope.CreateCommunicationErrorCallback = function () {
        alert('Erro ao criar Communication');
        $scope.ResetFields();
    }

    $scope.ListChannelCallback = function (data) {
        $scope.$apply(function () {
            $scope.ChannelList = data;
        });
    };

    $.post($scope.WebApiCommunication + '/api/Channel/GetChannelList', null, $scope.ListChannelCallback);

    $scope.ListLanguagesCallback = function (data) {
        $scope.$apply(function () {
            $scope.LanguagesList = data;
        });
    };
    $.get($scope.WebApiCore + '/api/SystemDomainValues/ListLanguages', null, $scope.ListLanguagesCallback);


    $scope.ListCommunicationTypeCallback = function (data) {
        $scope.$apply(function () {
            $scope.CommunicationTypeList = data;
        });
    };
    $.post($scope.WebApiCommunication + '/api/CommunicationType/GetCommunicationTypeList', null, $scope.ListCommunicationTypeCallback);


     // Eventos de User Experience
    $('#ddlCommunicationType').on('change', function () {
        $(".progress-bar").css('width', '20%');
    });

    $('#ddlChannel').on('change', function () {
        $(".progress-bar").css('width', '30%');
    });

    $('#ddlLanguage').on('change', function () {
        $(".progress-bar").css('width', '50%');
    });

    $('#txtCommunicationBody').on('keyup', function () {
        $(".progress-bar").css('width', '90%');
    });
    // Fim dos Eventos de User Experience

}]);