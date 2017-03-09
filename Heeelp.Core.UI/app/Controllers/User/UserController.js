'use strict';

app.controller("UserController", ['$scope', 'sendUser', 'WebApi', function ($scope, sendUser, WebApi) {


    // $scope.courses = bootstrappedData.courses; 
    $scope.User = {};
    $scope.User.Name = "Name";
    $scope.User.Email = "Email";
    $scope.User.SecundaryEmail = "SecundaryEmail";
    $scope.User.SmartPhoneNumber = "SmartPhoneNumber";
    $scope.User.PersonId = "1";
    $scope.User.UserProfileId = "1";
    $scope.User.UserStatusId = "1";
    $scope.User.PersonFatherId = "1";
    $scope.User.UserSystem = getCookie("User");

    //$scope.User.AuthenticationModeId = "1";
    //$scope.User.ValidationDateUTC = "2016-03-03";
    //$scope.User.ValidationToken = "ValidationToken";
    //$scope.User.EnrollmentIP = "EnrollmentIP";
    //$scope.User.ValidationIP = "ValidationIP";
    //$scope.User.IsDefaultUser = "true";
    //$scope.User.ServerInstanceId = "1";
    //$scope.User.CreatedBy = "1";
    //$scope.User.LanguageId = "94";
    //$scope.User.FormFillTime = "1";
    //$scope.User.SecurityCheckNecessary = "true";
    //$scope.User.IsPerpetual = "true";



    //$scope.LanguageList = [];
    //$scope.ServerInstanceList = [];
    $scope.PersonList = [];
    $scope.UserProfileList = [];
    $scope.UserStatusList = [];
    //$scope.UserList = [];
    $scope.ListPersonTypes = [];



    $scope.ListLanguageCallback = function (data) {
        $scope.$apply(function () {

            $scope.LanguageList = data;
        });
    };
    $scope.ListServerInstanceCallback = function (data) {
        $scope.$apply(function () {

            $scope.ServerInstanceList = data;
        });
    };
    $scope.ListPersonCallback = function (data) {
        $scope.$apply(function () {

            $scope.PersonList = data;
        });
    };
    $scope.ListUserProfileCallback = function (data) {
        $scope.$apply(function () {

            $scope.UserProfileList = data;
        });
    };
    $scope.ListUserStatusCallback = function (data) {
        $scope.$apply(function () {

            $scope.UserStatusList = data;
        });
    };
    //$scope.ListUserCallback = function (data) {
    //    $scope.$apply(function () {

    //        $scope.UserList = data;
    //    });
    //};
    $scope.ListPersonTypesCallback = function (data) {
        $scope.$apply(function () {

            $scope.ListPersonTypes = data;
        });
    };
    $scope.GetIpCallback = function (data) {
        $scope.$apply(function () {

            $scope.User.EnrollmentIP = data.ip;
        });
    }
    $scope.WebApiCore = WebApi.Core();



    //$.post($scope.WebApiCore + '/api/Language/ListLanguages', null, $scope.ListLanguageCallback);
    //$.post($scope.WebApiCore + '/api/Server/ListServerInstances', null, $scope.ListServerInstanceCallback);
    //$.post($scope.WebApiCore + '/api/User/ListUsers', null, $scope.ListUserCallback);
    $.post($scope.WebApiCore + '/api/Person/Listpersons', null, $scope.ListPersonCallback);
    $.post($scope.WebApiCore + '/api/User/ListUserProfiles', null, $scope.ListUserProfileCallback);
    $.post($scope.WebApiCore + '/api/User/ListUserStatus', null, $scope.ListUserStatusCallback);
    $.get($scope.WebApiCore + '/api/Person/ListPersonTypes', null, $scope.ListPersonTypesCallback);
    $.get("http://ipv4.myexternalip.com/json", null, $scope.GetIpCallback);

   


    var uploadUrl = $scope.WebApiCore + "/api/User/UserNaturalPersonAdd";

    $scope.optionPersonType = function (personTypeId) {
        if (personTypeId == 2) {
            uploadUrl = $scope.WebApiCore + "/api/User/UserLegalPersonAdd";
        } else {
            uploadUrl = $scope.WebApiCore + "/api/User/UserNaturalPersonAdd";
        }
    }

    $scope.sendUser = function (User) {
        var file = $scope.files;
        sendUser.send(file, uploadUrl, uploadSuccess, uploadError,User);
    };

    var uploadSuccess = function () {
        alert('sucesso');
    };
    var uploadError = function () {
        alert('Erro');
    };


    
    // Eventos de User Experience
     $('#fileToUpload').on('change', function () {

        $("#BtnSelecionar").empty();
        $("#BtnSelecionar").append("Imagem selecionada");
        $("#BtnSelecionar").addClass('btn-success');
        $(".progress-bar").css('width', '80%');
    });
    // Fim dos Eventos de User Experience




}]);