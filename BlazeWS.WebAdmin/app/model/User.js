define(['backbone','underscore', 'app/settings'], function (Backbone, _, settings) {
    return Backbone.Model.extend({
        idAttribute: 'Id',
		url: function () {
			var path = this.get('Id') ? '/users/' + this.get("Id") : '/users';
			return settings.WebservicePath + path;
		},
		destroy: function () {
		   
		    var args = _.extend(
                {
                    attrs: { 'Application': this.get('Application') }
                }, arguments);
		    Backbone.Model.prototype.destroy.apply(this, args);
		    
		}



	});



});