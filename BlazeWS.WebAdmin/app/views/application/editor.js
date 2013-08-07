define(['underscore', 'jsui/Control',
    'model/DataSource',
    'jsui/controls/Templated',
    'text!templates/application/editor.html',
    'jquery',
    'collections/DataSources',
    'jquery-ui'],
    function (_, Control, ItemModel, TemplateControl, template, $, DataSourceCollection) {
        return TemplateControl.extend({
            ctlDataSources: {},
            collection : {},
            events: {
            },

            OnInitialise: function () {
                console.log("INIT editor");
                _.bindAll(this, 'SetDataSource'); // fixes loss of context for 'this' within methods
                this.template = template;
                this.collection = new DataSourceCollection;

            },
            OnConfigure: function(config)
            {
                if(this.config.Application)
                {
                    this.collection.Application = this.config.Application;
                }
            },
            OnCreateControl: function (name, control) {
                if (name == "ctlDataSources") {
                    control.Configure({
                        editable: {
                            Buttons: {
                                Delete: true,
                                Commit: true,
                                Save: true
                            }
                        },
                        collection: this.collection,
                        columns: [
                            { Header: { Title: "Name" }, Data: "Name", Index: 1, Editable: { type: 'text' } },
                            { Header: { Title: "Path" }, Data: "Path", Index: 2, Editable: { type: 'text' } },
                            { Header: { Title: "Type" }, Data: "Type", Index: 3, Editable: { type: 'text' } },
                            { Header: { Title: "IsPrimarySource" }, Data: "IsPrimarySource", Index: 4, Editable: { type: 'text' } },
                            { Header: { Title: "JsonData" }, Data: "JsonData", Index: 5, Editable: { type: 'text' } }

                        ]
                    });

                    this.ctlDatasources = control;
                    
                }
            },
            SetDataSource: function (application) {
                this.collection.Application = application;
                this.collection.clear();
                this.collection.fetch();
            },
            OnAfterRender: function () {

            },
            OnDispose: function () {

            },

            btnSave_OnClick: function () {
            },
            OnDataChange: function () {
                
            }
        });
    });

