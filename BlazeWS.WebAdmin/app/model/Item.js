define(['backbone','underscore', 'app/settings'], function (Backbone, _, settings) {
    return Backbone.Model.extend({
        initialise: function() 
        {
            _.bindAll(this, "defaultUrl", "url");
            this.url = this.defaultUrl();
        },
        idAttribute: 'Id',
        defaultUrl: function () {
            var path = this.get('Id') ? '/items/' + this.get("Id") : '/items';
            return settings.WebservicePath + path;
        },
		sync: function (method, model) {
		    //console.log("model sync", { Method: method, Model: model });
		    return Backbone.sync.apply(this, arguments);
		},
        url: function () {
            return this.defaultUrl();
        }


	});



});