var app = app || {};
define(['backbone','model/Application','collections/Applications', 'collections/Users' ],
    function (Backbone, Application, Applications, Users) {
        function DataLogic() {
            this.Applications = new Applications;
            this.Users = new Users;
            this.System = _.extend(new Backbone.Model, {

                GetCurrentApplicationId: function () {
                    return app.Data.System.get('currentApplication').get('Id');

                }
            });
            
            //this.System.on("change", function (data) {
            //    console.log("[logic/DataLogic/System/on/change]", data);
            //});
            //this.System.on("change:currentApplication", function (data) {
            //    var currentApplication = data.get("currentApplication");
            //    console.log("[logic/DataLogic/System/on/change/currentApplication]", { currentApplication: currentApplication, appId: currentApplication.get('Id'), data: data });
            //    app.Data.Users.fetch({ data: { ApplicationId: currentApplication.get('Id') } });
            //});
        };

        DataLogic.prototype = {
            System: { },
            Applications: {},
            Users: {},
            Refresh: function (obj) {
                app.Data.Applications.fetch({
                    success: obj.success
                });
               
            }

        };
        return DataLogic;

    });