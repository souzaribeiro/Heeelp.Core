'use strict';

app.controller("CommunicationTypeController", ['$scope', "WebApi", function ($scope, WebApi) {

    $scope.Notification = {};
    $scope.ChannelList = {};
    $scope.LanguagesList = {};
    $scope.CommunicationGroupList = {};

    $scope.ResetFields = function () {
        $scope.$apply(function () {
            $scope.Notification.ChannelId = 0;
            $scope.Notification.LanguageId = 0;
            $scope.Notification.CommunicationGroupId = 0;
            $scope.Notification.Name = "";
            $scope.Notification.MessageCodeType = "";
            $scope.Notification.BodyTemplate = "";
            $scope.Notification.UserCanTurnOff = 0;
            $scope.Notification.SentAttempts = 0;
        });
    }
    $scope.ResetFields();
    $scope.WebApiCommunication = WebApi.Communication();
    $scope.WebApiCore = WebApi.Core();



    $scope.CreateCommunicationType = function (communicationType) {
        $.post($scope.WebApiCommunication + '/api/CommunicationType/AddCommunicationType', communicationType, $scope.CreateCommunicationSuccessCallback)
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

    $.post($scope.WebApiCommunication + '/api/Channel/GetChannelList',   $scope.ListChannelCallback);

    $scope.ListLanguagesCallback = function (data) {
        $scope.$apply(function () {
            $scope.LanguagesList = data;
        });
    };
    $.get($scope.WebApiCore + '/api/SystemDomainValues/ListLanguages', null, $scope.ListLanguagesCallback);


    $scope.ListCommunicationGroupCallback = function (data) {
        $scope.$apply(function () {
            $scope.CommunicationGroupList = data;
        });
    };
    $.post($scope.WebApiCommunication + '/api/CommunicationGroup/GetCommunicationGroupList', null, $scope.ListCommunicationGroupCallback);


    // Eventos de User Experience
    $('#ddlChannel').on('change', function () {
        $(".progress-bar").css('width', '20%');
    });

    $('#txtMessageCodeType').on('keyup', function () {
        $(".progress-bar").css('width', '30%');
    });

    $('#txtCommunicationTypeName').on('keyup', function () {
        $(".progress-bar").css('width', '50%');
    });

    $('#txtSentAttempts').on('keyup', function () {
        $(".progress-bar").css('width', '90%');
    });
    // Fim dos Eventos de User Experience
}]);