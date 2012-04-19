/// <reference path="/Scripts/jquery-1.7.2.js" />
/// <reference path="/Scripts/jquery.validate.js" />

$(function () {
	$('#btnGET').bind('click', function (evt) {
		var btn = $(evt.currentTarget);
		var txtCode = $('#txtCode_GET'), txtLang = $('#txtLanguage_GET'), txaTranslation = $('#txaTranslation');
		var translationUrl = jQuery.validator.format($(this).data('src'), txtCode.val(), txtLang.val());

		var error = function (xhr) {
			btn.removeClass().addClass('btn');
			if (xhr.status === 404) {
				btn.addClass('btn-warning');
				txaTranslation.val('').parents('.control-group')
					.removeClass('success error').addClass('warning');
			}
			else {
				btn.addClass('btn-danger');
				txaTranslation.val('')
					.parents('.control-group')
					.removeClass('success warning').addClass('error');
				console.log(xhr);
			}
		};
		var ok = function (resp) {
			btn.removeClass().addClass('btn')
				.addClass('btn-success');
			txaTranslation.val(JSON.stringify(resp))
				.parents('.control-group')
				.removeClass('error warning').addClass('success');
		};
		$.ajax({
			url: translationUrl,
			type: 'GET',
			dataType: 'json',
			error: error,
			success: ok
		});
		;
	});
});