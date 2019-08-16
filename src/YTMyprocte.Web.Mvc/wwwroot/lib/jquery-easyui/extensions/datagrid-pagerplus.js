(function ($) {
    $.extend($.fn.datagrid.defaults, {
        abpMethod: null,
        // methodParams: {},
        loader: function (param, success, error) {
            var opts = $(this).datagrid('options');
            //alert(opts.pageSize);
            if (opts.abpMethod) {
                param['maxResultCount'] = opts.pageSize;
                param['skipCount'] = (opts.pageNumber - 1) * parseInt(opts.pageSize);
                if (opts.sortName) {
                    param['sorting'] = opts.sortName + " " + opts.sortOrder;
                    
                }
                
                opts.abpMethod(param).done(function (result) {
                    var data = { total: result.totalCount, rows: result.items };
                    success(data);

                });
            }
            if (!opts.url) return false;
            $.ajax({
                type: opts.method,
                url: opts.url,
                data: param,
                dataType: 'json',
                success: function (data) {
                    success(data);
                },
                error: function () {
                    error.apply(this, arguments);
                }
            });
        },


    });


})(jQuery);