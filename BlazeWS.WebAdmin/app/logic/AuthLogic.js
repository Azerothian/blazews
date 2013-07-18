define(['backbone', 'jquery', 'app/settings'], function (Backbone, $, settings) {
    function AuthLogic() {
      
    };
    AuthLogic.prototype = {
        IsLoggedIn: function (success, error) {
            $.ajax({
                url: settings.WebservicePath + "/authcheck",
                data: {
                    "format": "json"
                },
                success: function (data, textStatus, jqXHR) {
                    if (data.IsValid) {
                        success();
                    } else {
                        error("You are not logged in");
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                    error(errorThrown);

                }

            });

        },
        Login: function (obj) { // username, password, rememberme, completed, error) {
            $.ajax({
                url: settings.WebservicePath + "/auth",
                data: {
                    "UserName": obj.UserName
                    , "Password": obj.Password
                    , "RememberMe": obj.RememberMe
                    , "format": "json"
                },
                success: function (data, textStatus, jqXHR) {
                    obj.success();
                    
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    obj.error(textStatus, errorThrown);
                    

                }
            });
        },
        Logout: function (obj) {
            $.ajax({
                url: settings.WebservicePath + "/authlogout",
                data: {
                    "format": "json"
                },
                success: function (data, textStatus, jqXHR) {
                    if (obj.success) {
                        obj.success();
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (obj.error) {
                        obj.error(textStatus, errorThrown);
                    }

                    


                }
            });
        }

            


    };

    return AuthLogic;
});