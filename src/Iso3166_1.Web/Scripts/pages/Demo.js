/// <reference path="/Scripts/jquery-1.7.1.js" />
/// <reference path="/Scripts/jquery.validate.js" />

$(function () {
	var ddl = $('#countries_fr');
	var countriesUrl = ddl.data("src");
	$.getJSON(countriesUrl, null, function (result) {
		$.each(result.countries, function () {
			var country = this;
			ddl.append($("<option />").val(country.alpha2_Code).text(country.name));
		});
	});

	var txtCode = $('#txtCode');
	var txaCountry = $('#txaCountry');
	$('#btnQuery').bind('click', function () {
		txaCountry.val('')
			.parents('.control-group')
				.removeClass('success').addClass('error');
		var countryUrl = jQuery.validator.format($(this).data('src'), txtCode.val());
		$.getJSON(countryUrl, null, function (country) {
			txaCountry.val(JSON.stringify(country))
				.parents('.control-group')
					.removeClass('error').addClass('success');
		});
	});
});