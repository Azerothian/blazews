define(['backbone', 'jsui/collections/State', 'app/settings', 'model/User'], function (Backbone, StateCollection, settings, UserModel) {
    return StateCollection.extend({
    //    deleted: new Backbone.Collection,
        url: settings.WebservicePath + "/users",
        model: UserModel,
        OnParse: function (resp) {
            return resp.Users;
        }
        //parse: function (resp, xhr) {
        //    this.header = resp.header;
        //    this.stats = resp.stats;
        //    console.log("[collections/Users/parse]", resp, resp.Users);
        //    return resp.Users;
        //},
    //    commit: function (obj) {

    //        this.each(function (model) {
    //            console.log("collections/Users", { model: model });
    //            model.save({}, {
    //                success: _.bind(function () {
    //                    this.success(this.model);
    //                }, { success: obj.success, model: model })
    //            });
    //        });
    //        this.deleted.each(function (model) {
    //            model.destroy();

    //        });

    //    },
    //    initialize: function (models, options) {
    //        _.bindAll(this, 'OnDataRemove', 'OnDataAdd', 'commit'); // fixes loss of context for 'this' within methods
    //        this.listenTo(this, 'remove', this.OnDataRemove);
    //        this.listenTo(this, 'add', this.OnDataAdd);
    //    },
    //    OnDataRemove: function (model) {
    //        console.log("[collections/Users/OnDataRemove]", { model: model });
    //        this.deleted.add(model);
    //    },
    //    OnDataAdd: function (model) {
    //        console.log("[collections/Users/OnDataAdd]", { deleted: this.deleted });
    //        var deleted = this.deleted.get(model.cid);
    //        if (deleted) {
    //            this.deleted.remove(model.cid);
    //        }
    //        console.log("[collections/Users/OnDataAdd]", { deleted: this.deleted });
    //    }

    });


});