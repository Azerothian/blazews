(/** @lends <global> */function (window, document, undefined) {

    (function (factory) {
        "use strict";

        // Define as an AMD module if possible
        if (typeof define === 'function' && define.amd) {
            define(['jquery','underscore', 'jquery-ui'], factory);
        }
            /* Define using browser globals otherwise
             * Prevent multiple instantiations if the script is loaded twice
             */
        else if (jQuery && !jQuery.fn.tableJs) {
            factory(jQuery, _);
        }
    }
    (/** @lends <global> */function ($, _) {
        "use strict";
        

        var tableJsDataSource = function () {
            _.bindAll(this, "getAll", "initialize");
        };
        tableJsDataSource.prototype = {
            columns: {},
            initialize: function() {

            },
            table: {}, 
            getAll: function () {


            }

        };

        var tableJs = function (config) {
            console.log("[lib/tablejs/init]", { Config: config, This: this });


            $(this).html("<thead><tr></tr></thead><tbody></tbody>");
            /// Variable Init

            this.Rows = [];
            this.PrimaryKey = 'id';

            if (config.PrimaryKey) {
                this.PrimaryKey = config.PrimaryKey;
            }
            if (config.Datasource) {
                this.Datasource = config.Datasource;
            } else {
                this.Datasource = new tableJsDataSource;
            }

            this.Datasource.table = this;
            this.Datasource.initialize();
            // Create Columns
            _.bindAll(this, 'add', 'remove', 'change', 'filter');
            
            return this;
        };

        tableJs.prototype = {
            Rows: {},
            PrimaryKey: '',
            Datasource: {},
            add: function (data) {




            },
            remove: function (id) {
                console.log("REMOVE", id);

            },
            removeAll: function () {
                _.each(this.Rows, function (val) {
                    this.remove(val[this.PrimaryKey]);
                });


            },
            createHeader: function()
            {
                var columns = this.Datasource.columns;
            },
            change: function (data) { },
            filter: function (row) { }
            
        };




        // jQuery aliases
        $.fn.tableJs = tableJs;
        $.fn.tableJs_Datasource = tableJsDataSource;


    }));

}(window, document));

