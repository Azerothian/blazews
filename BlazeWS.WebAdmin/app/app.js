var app = app || {};
app.Logic = app.Logic || {};

requirejs.config({
    baseUrl: '/lib',
    paths: {
        app: '/app',
        views: '/app/views',
        logic: '/app/logic',
        templates: '/app/templates',
        util: '/app/util',
        collections: '/app/collections',
        model: '/app/model',
        'jquery': 'jquery/jquery',
        'jquery-ui': 'jquery-ui/ui/jquery-ui',
        'underscore': 'underscore-amd/underscore',
        'backbone': 'backbone-amd/backbone',
        'text': 'requirejs-text/text',
        'css': 'require-css/css',
        'less': 'require-css/less',
        'jquery.jeditable': 'jquery_jeditable/jquery.jeditable',
        'normalize': 'require-css/normalize',
        'backbone.marionette': 'backbone.marionette/lib/backbone.marionette',
        'jquery.jstree': 'jstree/jquery.jstree'
    },
    shim: {
        jquery: {
            exports: '$'
        },
        'jquery-ui': {
            deps: ['jquery']
        },
        'util/ajaxhelper':
         {
             deps: ['jquery']
         },
        underscore: {
            exports: '_'
        },
        bootstrap: {
            deps: ['underscore']
        },
        backbone: {
            deps: ['underscore', 'jquery'],
            exports: 'backbone'
        }
    }

});
requirejs([
    'jquery',
    'underscore',
    'backbone',
    'jsui/jsui',
    'logic/AuthLogic',
    'logic/UiLogic',
    'logic/DataLogic'
],
    function (
        $,
        _,
        backbone,
        JSui,
        AuthLogic,
        UiLogic,
        DataLogic
    ) {
        console.log('App Starting up...');

        app.Logic.Auth = new AuthLogic;
        app.Logic.Ui = new UiLogic;
        app.Data = new DataLogic;
        app.jsui = new JSui;

        app.jsui.start('body');

        RouterLogic = Backbone.Router.extend({
            routes: {
                'login': 'showLogin'
                , 'l/:model/:func/:id': 'loadViewWithId'
                , 'l/:model/:func': 'loadView'
                 , '*actions': 'defaultRoute'
            },

            initialize: function () {
                console.log('Routes Initialised...');
            }
        });

        app.Logic.Router = new RouterLogic;

        app.Logic.Router.on('route:defaultRoute', function (actions) {
            //Default
            console.log('default', actions);
        });
        app.Logic.Router.on('route:loadViewWithId', function (model, func, id) {
            //Default

            requirejs(["views/" + model + "/" + func], function (view) {
                var v = new view;

                v.DataSource.set('Id', id);
                console.log("[/app/router/loadViewWithId]", model, func, id, v);
                app.jsui.root.addChild(model + func + id, v);
                app.Logic.Router.navigate("", false);
                return v;
            });
        });
        app.Logic.Router.on('route:loadView', function (model, func) {
            //Default
            app.Logic.Router.loadView(model, func);
            app.Logic.Router.navigate("", false);
        });
        app.Logic.Router.loadView = function (model, func, success) {
            requirejs(["views/" + model + "/" + func], function (view) {
                var v = new view;
                if (success) {
                    success(v);
                }
                console.log("[/app/router/loadView]", model, func, v);
                app.jsui.root.addChild(model + func, v);
                
            });

        };

        app.Logic.Router.on('route:showLogin', function () {
            console.log('login route');
            app.Logic.Ui.showLogin();
        });

        Backbone.history.start();

        app.Logic.Auth.IsLoggedIn(function () {
            console.log('Login Success');
            //app.Data.Refresh();
            app.Logic.Ui.showDesktop();
        }, function (message) {
            app.Logic.Ui.showLogin();
            console.log('Login Failed', message);
        });

    });