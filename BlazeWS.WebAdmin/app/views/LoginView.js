define(['jsui/Control', 'text!templates/auth/login.html', 'jquery', 'jquery-ui'], function (Control, loginTemplate, $) {
    return Control.extend({
        events: {
            'click button#btnLogin': 'btnLogin_OnClick'
        },
        OnInitialise: function() {
            this.model.title = "Login";
         //   this.model.
        },
        OnRender: function () {
            console.log("LoginView OnRender", { Template: loginTemplate, el: this.$el });
            this.$el.html(loginTemplate);
            
        },
        OnAfterRender: function () {
            this.$el.dialog({ title: this.model.title });
        },

        btnLogin_OnClick: function () {
            console.log("btnLogin_OnClick");

            //var username =  
            var loginRequest = {
                UserName: this.txtUsername(),
                Password: this.txtPassword(),
                RememberMe: this.chkRememberMe(),
                success: function () {
                    console.log("Auth Success");
                    app.Logic.Ui.showDesktop();
                },
                error: function (textStatus, errorThrown) {
                    console.log("Auth Failure", textStatus, errorThrown);
                }
            };
            // console.log(loginRequest);
            app.Logic.Auth.Login(loginRequest);

        },

        txtUsername: function () {
            return $(this.el).find('.txtUsername').val();

        },
        txtPassword: function () {
            return $(this.el).find('.txtPassword').val();
        }

        ,
        chkRememberMe: function () {
            return $(this.el).find('.chkRememberMe').is(':checked');

        }
    });
});

