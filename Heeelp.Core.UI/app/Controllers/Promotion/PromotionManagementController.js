'use strict';

app.controller("PromotionManagementController", ['$scope', 'sendPromotion', 'WebApi', 'send', 'toastr', function ($scope, sendPromotion, WebApi, send, toastr) {




    var profile = $.parseJSON(getCookie("profile"));
    var userSessionId = getCookie(user_session);
    $scope.WebApiCore = WebApi.Core();


    //lista
    $scope.DateRegex = GetDateRegex();
    $scope.PercentRegex = GetPercentRegex();
    $scope.MoneyRegex = GetMoneyRegex();
    $scope.NumberRegex = GetNumberRegex();
    $scope.Lock = true;
    $scope.BlockEdit = true;

    $scope.PromotionList = [];
    $scope.PersonIntegrationList = [];
    $scope.UserList = [];
    $scope.SelecteItemToAdd = [];


    $scope.optionPerson = function (CompanyIntegrationCode) {
        lockControls();
        $scope.SelectedCompanyIntegrationCode = CompanyIntegrationCode;
        $.get($scope.WebApiCore + '/api/Promotion/ListPromotions/' + $scope.Promotion.CompanyIntegrationCode, null, ListPromotionsCallback);
        $.get($scope.WebApiCore + '/api/Person/ListPersonExpertiseByPerson/' + $scope.Promotion.CompanyIntegrationCode, null, SubListExpertiseCallback);
    }


    //callbacks
    var ListPromotionsCallback = function (data) {
        if (data && data.length > 0) {
            $scope.$apply(function () {
                $scope.PromotionList = data;
            });
        } else {
            $scope.$apply(function () {
                $scope.PromotionList = [];
            });
        }
        unlockControls();
        $scope.$apply();
    }
    var ListExpertiseCallback = function (data) {
        $scope.$apply(function () {

            $scope.ExpertiseList = data;
        });
    };
    var ListPromotionBillingModelCallback = function (data) {
        $scope.$apply(function () {

            $scope.PromotionBillingModelList = data;
        });
    };
    var ListPromotionRecurrenceCallback = function (data) {
        $scope.$apply(function () {

            $scope.PromotionRecurrenceList = data;
        });
    };
    var ListPromotionPaymentTypeCallback = function (data) {
        $scope.$apply(function () {

            $scope.PromotionPaymentTypeList = data;
        });
    };
    var ListPromotionMethodPaymentCallback = function (data) {
        $scope.$apply(function () {
            $scope.PromotionMethodPaymentList = data;
        });
    }
    var ListPersonIntegrationCallback = function (data) {
        $scope.$apply(function () {
            $scope.PersonIntegrationList = data;
            unlockControls();
        });
    };
    var PromotionCallBack = function (data) {
        $scope.SelecteItemToAdd = {};

        $scope.BlockEdit = true;
        $scope.SelecteItemToAdd.Title = data.Result.Title;
        $scope.SelecteItemToAdd.ShortDescription = data.Result.ShortDescription;
        $scope.SelecteItemToAdd.StartDateUTC = new Date(data.Result.StartDateUTC.split('-')[0] + '/' + data.Result.StartDateUTC.split('-')[1] + '/' + data.Result.StartDateUTC.split('-')[2].substring(0, 2));
        $scope.SelecteItemToAdd.ValidUntilUTC = new Date(data.Result.ValidUntilUTC.split('-')[0] + '/' + data.Result.ValidUntilUTC.split('-')[1] + '/' + data.Result.ValidUntilUTC.split('-')[2].substring(0, 2));
        $scope.SelecteItemToAdd.RequiredTimeForActivation = data.Result.RequiredTimeForActivation == "0" ? 0 : data.Result.RequiredTimeForActivation;
        $scope.SelecteItemToAdd.PromotionBillingModelId = data.Result.PromotionBillingModelId;
        $scope.SelecteItemToAdd.PromotionRecurrenceId = data.Result.PromotionRecurrenceId;
        $scope.SelecteItemToAdd.PromotionPaymentTypeId = data.Result.PromotionPaymentTypeId;
        $scope.SelecteItemToAdd.NumberOfAvailableCoupons = data.Result.NumberOfAvailableCoupons;
        $scope.SelecteItemToAdd.PersonIntegrationCode = $scope.SelectedCompanyIntegrationCode;
        $scope.SelecteItemToAdd.SelectedExpertiseId = data.Result.ExpertiseId;
        $scope.SelecteItemToAdd.NormalPrice = data.Result.NormalPrice;
        $scope.SelecteItemToAdd.PromotionalPrice = data.Result.PromotionalPrice;
        $scope.SelecteItemToAdd.DiscountePercentege = data.Result.DiscountePercentege;
        $scope.SelecteItemToAdd.UserSessionId = userSessionId;
        $scope.SelecteItemToAdd.PromotionIntegrationCode = data.Result.IntegrationCode;
        $scope.SelecteItemToAdd.PromotionMethodPaymentId = data.Result.PromotionMethodPaymentId;
        $scope.ExpertiseId = data.Result.ExpertiseId;
        $scope.Edit = true;
        $scope.TituloModal = "Editar Promoção";

        unlockControls();
        $scope.$apply();


        $("#selectRequiredTime").val($scope.SelecteItemToAdd.RequiredTimeForActivation);
        $("#selectBillingModel").val($scope.SelecteItemToAdd.PromotionBillingModelId);
        $("#selectRecurrence").val($scope.SelecteItemToAdd.PromotionRecurrenceId);
        $("#selectPaymentType").val($scope.SelecteItemToAdd.PromotionPaymentTypeId);
        $("#selectMethodPayment").val($scope.SelecteItemToAdd.PromotionMethodPaymentId);

        $("#AddPromotion").fadeIn();
    }
    var SubListExpertiseCallback = function (data) {
        if (data != "") {
            $scope.$apply(function () {
                var lnk = "";
                lnk = $('<select  class="form-control" id="mySelect"></select>').html('<option></option>');
                $.each(data, function (index, item) {
                    lnk.append('<option value="' + item.ExpertiseId + '">' + item.Name + '</option>')
                });

                lnk.on("change", function (e) {
                    $(this).nextAll().remove();
                    $scope.SelectedExpertiseId = $(this).val();
                    callSubExpertise();
                    $scope.optionChangedExpertise($(this).val());

                });

                $('#listExpertise').append(lnk);
            });
        }
    }
    var callSubExpertise = function () {
        $.get($scope.WebApiCore + '/api/Expertise/ListSubExpertises/' + $scope.Promotion.ExpertiseId, null, SubListExpertiseCallback);
    }
    var uploadSuccess = function (data) {
        toastr.success('Solicitação enviada com sucesso!', 'Sucesso!');
        $scope.CloseModalUploadImage();
    };
    var uploadError = function () {
        toastr.error('Erro ao enviar solicitação!', 'Erro!');
        $scope.CloseModalUploadImage();
    };


    //modais
    $scope.CloseModal = function () {
        $("#AddPromotion").fadeOut(300);
    }
    $scope.CloseModalUploadImage = function () {
        $("#AddPromotionImage").fadeOut();
    }
    $scope.CloseModalEditPromotion = function () {
        $("#EditPromotionDetailsModal").fadeOut();
    }
    $scope.CloseEditPromotionModal = function () {
        $("#EditPromotionModal").fadeOut();
    }
    $scope.OpenDetails = function (promotion) {
        $scope.BlockEdit = false;
        $('#listExpertise').empty();
        expertiseSelect($scope.SelectedCompanyIntegrationCode);
        $scope.TituloModal = "Incluir Promoção";
        $("#AddPromotion").fadeIn(300);
        $scope.SelecteItemToAdd = {};

        $scope.today();
    };
    $scope.OpenFullDetailsModal = function (promotion) {
        $scope.selectedPromotion = promotion;
        $("#EditPromotionDetailsModal").fadeIn();

    }
    $scope.OpenEditPromotionModal = function (promotion) {
        $scope.selectedPromotion = promotion;
        $("#EditPromotionModal").fadeIn();

    }
    $scope.UploadPromotionImage = function (promotion) {
        $scope.selectedPromotion = promotion;
        $("#AddPromotionImage").fadeIn();
    };


    //gets
    send.get($scope.WebApiCore + '/api/promotion/GetPromotionBillingModelList', null, ListPromotionBillingModelCallback, null);
    send.get($scope.WebApiCore + '/api/promotion/GetPromotionRecurrenceList', null, ListPromotionRecurrenceCallback, null);
    send.get($scope.WebApiCore + '/api/promotion/GetPromotionPaymentTypeList', null, ListPromotionPaymentTypeCallback, null);
    send.get($scope.WebApiCore + '/api/promotion/GetPromotionMethodPaymentList', null, ListPromotionMethodPaymentCallback, null);
    send.get($scope.WebApiCore + '/api/enrollment/ListCompanies', null, ListPersonIntegrationCallback, null);
    send.post($scope.WebApiCore + '/api/Expertise/ListMainExpertises', null, ListExpertiseCallback, null);


    //send
    $scope.sendPromotion = function (Promotion) {
        lockControls();
        var promotionData = {};

        promotionData.Title = Promotion.Title;
        promotionData.ShortDescription = Promotion.ShortDescription;
        promotionData.StartDateUTC = Promotion.StartDateUTC.yyyymmdd();
        promotionData.ValidUntilUTC = Promotion.ValidUntilUTC.yyyymmdd();
        promotionData.RequiredTimeForActivation = Promotion.RequiredTimeForActivation == "0" ? 0 : Promotion.RequiredTimeForActivation;
        promotionData.PromotionBillingModelId = Promotion.PromotionBillingModelId;
        promotionData.PromotionRecurrenceId = Promotion.PromotionRecurrenceId;
        promotionData.PromotionPaymentTypeId = Promotion.PromotionPaymentTypeId;
        promotionData.NumberOfAvailableCoupons = Promotion.NumberOfAvailableCoupons;
        promotionData.PersonIntegrationCode = $scope.SelectedCompanyIntegrationCode;
        if ($("#mySelect").val() == null || $("#mySelect").val() == "")
            promotionData.ExpertiseId = $scope.ExpertiseId;
        else
            promotionData.ExpertiseId = $("#mySelect").val();

        promotionData.UserSessionId = userSessionId;
        promotionData.PromotionIntegrationCode = Promotion.PromotionIntegrationCode;
        promotionData.PromotionMethodPaymentId = Promotion.PromotionMethodPaymentId;
        if (Promotion.PromotionMethodPaymentId == 1) {
            promotionData.NormalPrice = Promotion.NormalPrice.toString().indexOf(',') > -1 ? Promotion.NormalPrice.toString().replace(',', '.') : Promotion.NormalPrice;
            promotionData.PromotionalPrice = Promotion.PromotionalPrice.toString().indexOf(',') > -1 ? Promotion.PromotionalPrice.toString().replace(',', '.') : Promotion.PromotionalPrice;
            promotionData.DiscountePercentege = '0.00';
        }
        if (Promotion.PromotionMethodPaymentId == 2) {
            promotionData.NormalPrice = Promotion.NormalPrice.toString().indexOf(',') > -1 ? Promotion.NormalPrice.toString().replace(',', '.') : Promotion.NormalPrice;
            promotionData.PromotionalPrice = '0.00';
            promotionData.DiscountePercentege = Promotion.DiscountePercentege;
        }
        if (Promotion.PromotionMethodPaymentId == 3) {
            promotionData.DiscountePercentege = Promotion.DiscountePercentege;
            promotionData.NormalPrice = '0.00';
            promotionData.PromotionalPrice = '0.00';
        }





        var url = $scope.WebApiCore + "/api/Promotion/AddDiscountPromotion";

        $.ajax({
            type: 'POST',
            url: url,
            crossDomain: true,
            dataType: "json",
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/x-www-form-urlencoded',
            data: promotionData,
            success: function (response) {
                $scope.$apply(function () {
                    if (!$scope.Edit) {
                        promotionData.IntegrationCode = response;
                        $scope.PromotionList.push(promotionData)
                    }
                });
                $scope.CloseModal();
                toastr.success('Solicitação enviada com sucesso!', 'Sucesso!');
                unlockControls();
                $scope.Edit = false;
            },
            error: function (xhr, status, error) {

                //$scope.CloseModal();
                unlockControls();
                toastr.error('Erro ao enviar solicitacao!', 'Erro!');
            }
        });

    };
    $scope.sendPromotionImages = function () {
        var file = $scope.files;
        var uploadUrl = $scope.WebApiCore + "/api/Promotion/UploadPromotionPhoto";
        sendPromotion.send(file, uploadUrl, uploadSuccess, uploadError, $scope.selectedPromotion);
    };
    $scope.EditPromotionDetails = function (Promotion) {

        var promotionData = {};
        promotionData.PromotionIntegrationCode = Promotion.IntegrationCode;
        promotionData.Alert = Promotion.Alert;
        promotionData.FullDescription = Promotion.FullDescription;
        promotionData.UserSessionId = userSessionId;
        var url = $scope.WebApiCore + "/api/Promotion/EditPromotionDetails";
        $.ajax({
            type: 'POST',
            url: url,
            crossDomain: true,
            dataType: "json",
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/x-www-form-urlencoded',
            data: promotionData,
            success: function (response) {
                toastr.success('Solicitação enviada com sucesso!', 'Sucesso!');
                $scope.$apply(function () {
                    $scope.CloseModalEditPromotion();
                    $scope.UpdatePromotionList(Promotion);
                });
            },
            error: function (xhr, status, error) {
                toastr.error('Erro ao enviar Solicitação!', 'Erro!');

                $scope.CloseModal();
            }
        });
    }
    $scope.Deleted = function (PromotionItem) {
        var index = $scope.PromotionList.indexOf(PromotionItem);
        var promotionData = {};
        promotionData.PromotionIntegrationCode = PromotionItem.IntegrationCode;
        promotionData.UserSessionId = userSessionId;
        var url = $scope.WebApiCore + "/api/Promotion/DeletedPromotion";

        $.ajax({
            type: 'POST',
            url: url,
            crossDomain: true,
            dataType: "json",
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/x-www-form-urlencoded',
            data: promotionData,
            success: function (response) {
                $scope.$apply(function () {
                    if (index != -1) {
                        $scope.PromotionList.splice(index, 1);
                    }
                });
                toastr.success('Promoção excluida com sucesso!', 'Sucesso!');
            },
            error: function (xhr, status, error) {
                toastr.error('Erro ao enviar solicitacao!', 'Erro!');
            }
        });
    };
    $scope.EditPromotion = function (promotionIntergrationCode) {
        lockControls();
        send.get($scope.WebApiCore + '/api/Promotion/GetPromotion/' + promotionIntergrationCode, null, PromotionCallBack, null);

    }


    //utils
    Date.prototype.yyyymmdd = function () {
        var d = this,
         month = '' + (d.getMonth() + 1),
         day = '' + d.getDate(),
         year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;
        return [year, month, day].join('-');
    };
    $scope.calculatePromotion = function () {

        if ($scope.Promotion.PriceReference.PriceReferenceModel) {
            alert("Preço por Cupom = " + $scope.Promotion.PriceReference.EditorialPriceinHeeelps);
            $scope.HeeelpTaxValue = $scope.Promotion.PriceReference.EditorialPriceinHeeelps;
        } else {
            var valorDesconto = ($scope.Promotion.NormalPrice * ($scope.Promotion.DiscountePercentege / 100));
            var valorAposDesconto = $scope.Promotion.NormalPrice - valorDesconto;
            var valorHeeelp = valorAposDesconto * $scope.Promotion.PriceReference.ReferencePercentual / 100;
            $scope.HeeelpTaxValue = valorHeeelp;
            alert("Preço por Cupom = " + valorHeeelp);
        }
    }
    $scope.UpdatePromotionList = function (PromotionItem) {
        for (var i = 0; i < $scope.PromotionList.length ; i++) {
            if ($scope.PromotionList[i].IntegrationCode == PromotionItem.IntegrationCode) {
                $scope.PromotionList[i] = PromotionItem;
                break;
            }
        }
    };
    $scope.popup1 = {
        opened: false
    };
    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };
    $scope.popup2 = {
        opened: false
    };
    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };
    $scope.today = function () {
        $scope.SelecteItemToAdd.StartDateUTC = new Date();
    };
    $scope.dateOptions = {
        dateDisabled: disabled,
        formatYear: 'yy',
        maxDate: maxDate,
        minDate: new Date(),
        startingDay: 1
    };
    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
    $scope.altInputFormats = ['M!/d!/yyyy'];
    $scope.selectedPromotion = {};
    var lockControls = function () {
        $scope.Lock = true;
    }
    var unlockControls = function () {
        $scope.Lock = false;
    }
    var today = new Date();
    var maxDate = new Date(today.getUTCFullYear() + 1, today.getUTCMonth(), today.getUTCDay());
    function disabled(data) {
        return false;
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }

    //  Eventos de User Experience
    $('#fileToUpload').on('change', function () {

        $("#BtnSelecionar").empty();
        $("#BtnSelecionar").append("Imagem selecionada");
        $("#BtnSelecionar").addClass('btn-success');
        $(".progress-bar").css('width', '80%');
    });

    var expertiseSelect = function (companyIntegrationCode) {
        $.get($scope.WebApiCore + '/api/Person/ListPersonExpertiseByPerson/' + companyIntegrationCode, null, SubListExpertiseCallback);
    }

}]);