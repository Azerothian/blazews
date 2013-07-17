define([],
    function () {
        function ViewLogic () {

        };

        ViewLogic.prototype = {
            showView: function (selector, view) {
                if (this.currentView) {
                    this.currentView.close();
                }
                console.log("Rendering View", selector, view);
                $(selector).html(view.render().el);
                this.currentView = view;
                return view;
            }
            , showLogin: function () {
                requirejs(["views/LoginView"], function (LoginView) {
                    app.Logic.View.showView("body", new LoginView);
                });
            },
            showDesktop: function () {


            }

        };
        return ViewLogic;

    });