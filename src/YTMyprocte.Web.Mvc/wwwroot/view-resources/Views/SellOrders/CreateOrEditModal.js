(function () {
    var _materielService = abp.services.app.materiel;  
    var _orderService = abp.services.app.sell;
    var _$materieldatagrid = $('#dgc');
    var _$orderdatagrid = $('#orderTable');
    var _$materieldialog = $("#dlggc");
    var _$minetbr = $("#toolbar1");
    var tempId = $("#id").val();


    function fLoadMaterielDataGrid() {
        abp.ui.setBusy(_$materieldatagrid);
        _$materieldatagrid.datagrid({
            //得到物料信息
            abpMethod: _materielService.getPagedMateriel,
            queryParams: _$minetbr.find('form').serializeFormToObject(),
            method: 'post',
            fitColumns: true,
            sortName: 'CreationTime',
            sortOrder: 'desc',
            idField: 'id',
            pageSize: 15,
            pageList: [15, 20, 30, 40, 50],
            pagination: true,
            striped: true, //奇偶行是否区分
            singleSelect: false,//单选模式
            rownumbers: true,//行号
            columns: [[
                { field: 'ck', checkbox: true },
                { field: 'id', title: '主键', width: 80, hidden: true },
                { field: 'code', title: '编码', width: 120, sortable: true },
                { field: 'waresName', title: '名称', width: 120, sortable: true },
                { field: 'waresFomat', title: '规格', width: 120, sortable: true },
                { field: 'waresUnit', title: '单位', width: 120 },
                { field: 'sellMoney', title: '销售价(元)', width: 120, sortable: true }
            ]]
        });
        abp.ui.clearBusy(_$materieldatagrid);
    }

    var lastIndex = undefined;
    function endEditing() {
        if (lastIndex == undefined) { return true }
        if (_$orderdatagrid.datagrid('validateRow', lastIndex)) {
            _$orderdatagrid.datagrid('endEdit', lastIndex);
            lastIndex = undefined;
            return true;
        } else {
            return false;
        }
    }
    function fLoadOrderDataGrid() {
        //将物料信息显示（从物料中添加）
        abp.ui.setBusy(_$orderdatagrid);
        _$orderdatagrid.datagrid({  //
            abpMethod: _orderService.getSellOrder,
            queryParams:
            {
                id: tempId
            },
            idField: 'materielId',
            fitColumns: true,
            sortName: 'CreationTime',
            sortOrder: 'desc',
            striped: true, //奇偶行是否区分
            //singleSelect: true,//单选模式
            singleSelect: true,
            rownumbers: true,//行号
            columns: [[
                { field: 'ck', checkbox: true },
                { field: 'orderCode', title: '采购单号', width: 80, hidden: true },
                { field: 'materielId', title: '主键', width: 80, hidden: true },
                { field: 'materielCode', title: '物料编码', width: 120 },
                { field: 'materielName', title: '物料名称', width: 120 },
                { field: 'goodsFomat', title: '规格型号', width: 120 },
                { field: 'goodsUnit', title: '单位', width: 120 },
                {
                    field: 'count', title: '销售数量', width: 120,
                    editor: {
                        type: 'numberbox',
                        options: {
                            required: true,
                            precision: 0,
                            onChange: function (newValue, oldValue) {

                                var row = _$orderdatagrid.datagrid('getSelected');
                                console.info(row);
                                var rindex = _$orderdatagrid.datagrid('getRowIndex', row);
                                var ed = _$orderdatagrid.datagrid('getEditor', {
                                    index: rindex,
                                    field: 'outPrice'
                                });
                                $(ed.target).numberbox('setValue', newValue * row.sellPrice);
                                _$orderdatagrid.datagrid('endEdit', lastIndex);
                                lastIndex = undefined;

                            }
                        }

                    }
                },
                { field: 'sellPrice', title: '销售价(元)', width: 120 },
                {
                    field: 'outPrice', title: '金额(元)', width: 120,
                    editor: {
                        type: 'numberbox',
                        precision: 2

                    }
                }
            ]],
            onBeforeLoad: function () {
                $(this).datagrid('rejectChanges');
            },
            onClickRow: function (rowIndex) {
                if (lastIndex != rowIndex) {
                    if (endEditing()) {
                        _$orderdatagrid.datagrid('selectRow', rowIndex)
                            .datagrid('beginEdit', rowIndex);
                        lastIndex = rowIndex;
                    } else {
                        _$orderdatagrid.datagrid('selectRow', lastIndex);
                    }
                }

            }

        });
        abp.ui.clearBusy(_$orderdatagrid);
    }
    formatterDate = function (date) {

        var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();

        var month = (date.getMonth() + 1) > 9 ? (date.getMonth() + 1) : "0"

            + (date.getMonth() + 1);

        return date.getFullYear() + '-' + month + '-' + day;

    };
    // 初始化搜索相关按钮
    _$minetbr.find(".btn-search").linkbutton({
        width: '80',
        iconCls: 'icon-search'
    }).bind('click', function () {
        //过滤条件
        if (!_$minetbr.find('form').form('validate')) {
            return;
        }
        var qC = _$minetbr.find('form').serializeFormToObject();
        _$materieldatagrid.datagrid("load", qC);
    });
    //清空条件
    _$minetbr.find(".btn-clean").linkbutton({
        width: '80',
        iconCls: 'icon-delete'
    }).bind('click', function () {
        var $form = $(this).closest("form");
        if ($form.length > 0) {
            $form.form('reset');
        }
        var qC = _$minetbr.find('form').serializeFormToObject();
        _$materieldatagrid.datagrid("load", qC);

    });
    $(function () {

        // var lastIndex;
        $('#buyTime').datebox('setValue', formatterDate(new Date()));
        fLoadOrderDataGrid();


        //添加物料
        $("#baddc").click(function () {

            _$materieldialog.dialog('open').dialog('refresh', '').dialog('setTitle', "添加物料");
            fLoadMaterielDataGrid();


            //展示物料信息在订单中
            $('.saveMinec').unbind("click").click(function () {

                var rows = _$materieldatagrid.datagrid('getSelections');
                console.info("选中物料", rows);
                if (rows.length == 0) {
                    _$materieldialog.dialog('close');
                }
                else {
                    _$materieldialog.dialog('close');
                    var selectRows = _$orderdatagrid.datagrid('getRows');
                    for (i = 0; i < rows.length; i++) {
                        var temp = rows[i];
                        var t = false;
                        for (j = 0; j < selectRows.length; j++) {
                            if (temp.id == selectRows[j].materielId) {
                                t = true;
                            }
                        }
                        if (!t) {
                            _$orderdatagrid.datagrid("appendRow", {
                                materielId: temp.id,
                                materielCode: temp.code,
                                materielName: temp.waresName,
                                goodsFomat: temp.waresFomat,
                                goodsUnit: temp.waresUnit,
                                sellPrice: temp.sellMoney
                            });

                            editIndex = _$orderdatagrid.datagrid('getRows').length + 1;
                            _$orderdatagrid.datagrid('selectRow', lastIndex).datagrid('beginEdit', lastIndex);

                            //editIndex = _$orderdatagrid.datagrid('getRows').length-1;
                            //_$orderdatagrid.datagrid('selectRow', editIndex).datagrid('beginEdit', editIndex);  
                        }
                    }
                    // _$orderdatagrid.datagrid("loadData",{"rows":rows});   


                }
                _$materieldatagrid.datagrid('clearSelections');
            });
            $('.consolec').unbind("click").click(function () {
                _$materieldialog.dialog('close');
            });
        });

        //删除选中的物料
        $("#beditc").click(function () {
            var node = _$orderdatagrid.datagrid('getSelected');
            if (node) {

                var rowIndex1 = _$orderdatagrid.datagrid('getRowIndex', node);
                _$orderdatagrid.datagrid('deleteRow', rowIndex1);
                var rowIndex = _$materieldatagrid.datagrid('getRowIndex', node);
                //rowIndex = undefined; 
                _$orderdatagrid.datagrid('clearSelections');
                //_$materieldatagrid.datagrid('uncheckRow', rowIndex); 
            }
            //_$orderdatagrid.datagrid('clearSelections');   
        });

    });

})();