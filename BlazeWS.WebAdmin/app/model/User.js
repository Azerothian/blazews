define(['backbone','underscore', 'app/settings'], function (Backbone, _, settings) {
    return Backbone.Model.extend({
        idAttribute: 'Id',
		url: function () {
			var path = this.get('Id') ? '/users/' + this.get("Id") : '/users';
			return settings.WebservicePath + path;
		}


	});



});