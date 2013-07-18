define(['backbone',
    'jsui/Control',
    'collections/Applications',
    'jquery',
    'text!templates/application/selector.html',
    'jquery-ui'],
    function (Backbone, Control, ApplicationCollection, $, tmplSelector) {
        return Control.extend({
            DataSource: {},
            events: {
            },
            OnInitialise: function () {
                this.listenTo(app.Data.Applications, 'change', this.OnRender);
                this.template = _.template(tmplSelector);
                _.bindAll(this, 'btnChange_OnClick', 'btnCancel_OnClick'); // fixes loss of context for 'this' within methods
                this.setModel("title", "Application Selector");
            },

            OnRender: function () {
                var tmpl = this.template({ Applications: app.Data.Applications });
                console.log("[view/application/selector/OnRender]", { template: tmpl } );
                $(this.el).html(tmpl);
            },
            OnAfterRender: function () {
                this.$el.dialog({
                    title: this.getModel('title'),
                    buttons: {
                        "Select": this.btnChange_OnClick
                    }

                });
            },
            btnChange_OnClick: function () {
                var value = $(this.el).find('.ddlApplicationId').val();
                console.log("[views/application/selector/btnChange_OnClick]", { Value: value });
                app.Data.System.set("currentApplication", app.Data.Applications.get(value));
                this.$el.dialog("close");
                app.Logic.Ui.showDesktop();
                this.dispose();
            },
            btnCancel_OnClick: function () {
                this.$el.dialog("close");
                this.dispose();

            }
        });
    });

