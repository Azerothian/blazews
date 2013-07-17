var app = app || {};
define(['jsui/jsui'],
    function () {
        function UiLogic() {

        };

        UiLogic.prototype = {
            showLogin: function () {
                jsui.root.removeAll();
                requirejs(["views/LoginView"], function (LoginView) {
                    var login = new LoginView();
                    jsui.root.addChild("ctlLogin", login);
                });

            },
            showDesktop: function () {
                jsui.root.removeAll();

                app.Data.Refresh({
                    success: function () {
                        if (app.Data.System.get("currentApplication")) {
                            requirejs(["views/DesktopView"], function (DesktopView) {
                                var desk = new DesktopView();
                                jsui.root.addChild("ctlDesktop", desk);
                            });
                        } else {
                            requirejs(["views/application/selector"], function (ApplicationSelector) {
                                var app = new ApplicationSelector();
                                jsui.root.addChild("ctlApplicationSelector", app);
                            });
                        }

                    }
                });



            }

        };
        return UiLogic;

    });