'use strict';

app.controller("ExpertiseController", ['$scope', 'fileUpload', 'WebApi', '$location', function ($scope, fileUpload, WebApi,$location) {
    // $scope.courses = bootstrappedData.courses;



    var userId = getCookie("User");

    $scope.Expertise = {};
    $scope.Expertise.Name = "";
    $scope.Expertise.Description = "";
    $scope.Expertise.ExpertiseFather = "";
    $scope.Expertise.ApprovalStatusId = "1";
    $scope.Expertise.CreatedBy = userId
    $scope.Expertise.ApprovedBy = userId;
    $scope.files = [];
    $scope.expertiseList = [];
    $scope.PersonList = [];
    $scope.expertiseSaveList = [];



    $scope.SubListExpertiseCallback = function (data) {
        if (data != "") {
            $scope.$apply(function () {
                var lnk = "";
                lnk = $('<select id="mySelect" class="form-control"></select>').html('<option></option>');
                $.each(data, function (index, item) {
                    lnk.append('<option value="' + item.ExpertiseId + '">' + item.Name + '</option>')
                });

                lnk.on("change", function (e) {
                    $(this).nextAll().remove();
                    $scope.Expertise.ExpertiseFather = $(this).val();
                    callSubExpertise();

                });

                $('#listExpertise').append(lnk);
            });
        }
    }

    $scope.ListExpertisesCallback = function (data) {
        $scope.$apply(function () {

            $scope.expertiseList = data;
            $scope.expertiseSaveList = data;
        });
    };



    $scope.Edit = function (expertise) {
        $location.path('/Expertise/ExpertiseEdit/' + expertise.ExpertiseId);
    };



    $scope.WebApiCore = WebApi.Core();

    $.post($scope.WebApiCore + '/api/Expertise/ListExpertises', null, $scope.SubListExpertiseCallback);


    var hubProx = hub.proxies.notificationhub;
    $scope.UploadClick = function () {
        hubProx.server.send('nome da mensagem', 'mensagem legal');
    };




    var callSubExpertise = function () {
        $.get($scope.WebApiCore + '/api/Expertise/ListSubExpertises/' + $scope.Expertise.ExpertiseFather, null, $scope.SubListExpertiseCallback);
    }

    $scope.uploadFile = function (Expertise) {
        var file = $scope.files;
        var uploadUrl = $scope.WebApiCore + "/api/Expertise/PostFile?jsonData=" + JSON.stringify(Expertise);


        fileUpload.uploadFileToUrl(file, uploadUrl, uploadSuccess, uploadError);
    };

    var uploadSuccess = function () {
        alert('sucesso');
    };
    var uploadError = function () {
    };


    // Eventos de User Experience
    $('#txtExpertiseName').on('keyup', function () {
        $(".progress-bar").css('width', '25%');
    });

    $('#mySelect').on('keyup', function () {
        $(".progress-bar").css('width', '60%');
    });

    $('#fileToUpload').on('change', function () {

        $("#BtnSelecionar").empty();
        $("#BtnSelecionar").append("Imagem selecionada");
        $("#BtnSelecionar").addClass('btn-success');
        $(".progress-bar").css('width', '80%');
    });

    $('#txtExpertiseDescription').on('keyup', function () {
        $(".progress-bar").css('width', '100%');
    });
    // Fim dos Eventos de User Experience

    hubProx.client.addNewMessageToPage = function (name, message) {
        alert(name + ' - ' + message);
    }

    
    hub.start(); // connect to signalr hub



}]);

