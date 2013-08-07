define(['underscore', 'jquery', 'jsui/controls/Templated',
    'text!templates/application/browser.html', 'collections/Applications'],
    function (_, $, TemplateControl, template, Applications) {
        return TemplateControl.extend({
            ctlApplicationEditor: {},
            ctlApplicationsList: {},
            // collection: {},
            events: {
                //'click button#btnSave': 'btnSave_OnClick'
            },

            OnInitialise: function () {
                //_.bindAll(this, 'btnClose_OnClick'); // fixes loss of context for 'this' within methods
                this.template = template;
                this.collection = new Applications;
                //   this.model.
            },
            OnCreateControl: function (name, control) {

                if (name == "ctlApplicationEditor") {


                    this.ctlApplicationEditor = control;
                }

                if (name == "ctlApplicationsList") {
                    control.Configure({
                        collection: this.collection,
                        Key: "Id",
                        Text: "Name",
                        Value: "Id",
                        Events: { 
                            OnSelect: function (model) {
                                this.ctlApplicationEditor.SetDataSource(model);
                            }
                        }
                    });
                    control.collection.fetch({
                        data: {

                        }, success: function () {

                        }

                    });
                    this.ctlApplicationsList = control;
                }
            },
            OnAfterRender: function () {
                $(this.el).dialog({
                    width: 750,
                    height: 550,
                    title: "Item Browser",
                    //close: this.btnClose_OnClick,

                });
            },
            OnDispose: function () {
                //   this.modelBinder.unbind();
            }


        });
    });

