/// <reference path="/Scripts/jquery-1.7.1.js" />
/// <reference path="/Scripts/jquery.validate.js" />

$(function () {
	$('#btnGET').bind('click', function (evt) {
		var btn = evt.currentTarget;
		var txtCode = $('#txtCode_GET'), txtLang = $('#txtLanguage_GET');
		var translationUrl = jQuery.validator.format($(this).data('src'), txtCode.val(), txtLang.val());
		var error = function (xhr) {
			$(btn).removeClass().addClass('btn');
			if (xhr.status === 404) {
				$(btn).addClass('btn-warning');
			}
			else {
				$(btn).addClass('btn-danger');
				console.log(xhr);
			}
		};
		var ok = function () {
			$(btn).removeClass().addClass('btn')
				.addClass('btn-success');
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