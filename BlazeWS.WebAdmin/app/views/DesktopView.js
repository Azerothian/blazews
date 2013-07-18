define(['jsui/Control', 'text!templates/desktop.html', 'collections/Applications', 'jquery', 'jquery-ui'], function (Control, template, ApplicationCollection, $) {
    return Control.extend({
        events: {
            'click .btnLogout': 'btnLogout_OnClick'
        },
        OnInitialise: function() {
            //this.model.apps = new ApplicationCollection;
            //this.model.apps.fetch();
            //console.log("APPS TEST", this.model.apps);
        },
        OnRender: function () {
         //   console.log("LoginView OnRender", { Template: loginTemplate, el: this.$el });
            this.$el.html(template);
            
        },
        OnAfterRender: function () {
            this.$el.find('.desktopMenu').menu();
        },
        btnLogout_OnClick: function() {
            app.Logic.Auth.Logout({
                success: function() {
                    app.Logic.Ui.showLogin();
                },
                error: function (textStatus, errorThrown) {
                    console.log("btnLogout_OnClick", textStatus, errorThrown);

                }

            });

        }
    });
});

