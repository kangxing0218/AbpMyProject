(function () {
    var _purchaseOrderService = abp.services.app.purchaseOrder;
    var _materielService = abp.services.app.materiel;
    var _$materieldatagrid = $('#dgc');
    var _$orderdatagrid = $('#orderTable');
    var _$materieldialog = $("#dlggc");
    var _$minetbr = $("#toolbar1");
   
    var tempId = $("#id").val();
    //物料信息
    function fLoadMaterielDataGrid() {
        //  console.log("wwww", _$materieldatagrid)
        abp.ui.setBusy(_$materieldatagrid);
        _$materieldatagrid.datagrid({
            abpMethod: _materielService.getPagedMateriel,
            queryParams: _$minetbr.find('form').serializeFormToObject(),
            method: 'post',
            fitColumns: true,
            sortName: 'CreationTime',
            sortOrder: 'desc',
            idField: 'id',
            pageSize: 10,
            pageList: [10, 20, 30, 40, 50],
            pagination: true,
            striped: true, //奇偶行是否区分
            singleSelect: false,//单选模式
            rownumbers: true,//行号
            toolbar: "#toolbar1",
            columns: [[
                { field: 'ck', checkbox: true },
                { field: 'id', title: '主键', width: 80, hidden: true },
                { field: 'code', title: '编码', width: 120, sortable: true },
                { field: 'waresName', title: '名称', width: 120, sortable: true },
                { field: 'waresFomat', title: '规格', width: 120, sortable: true },
                { field: 'waresUnit', title: '单位', width: 120 },
                { field: 'buyMoney', title: '采购价(元)', width: 120, sortable: true }
            ]]
        });
        abp.ui.clearBusy(_$materieldatagrid);
    }

    var lastIndex = undefined;
    function endEditing() {
        if (lastIndex == undefined) { return true }//validateRow验证制定行
        if (_$orderdatagrid.datagrid('validateRow', lastIndex)) {
            _$orderdatagrid.datagrid('endEdit', lastIndex);
            lastIndex = undefined;
            return true;
        } else {
            return false;
        }
    }
    function fLoadOrderDataGrid() {
        //
        abp.ui.setBusy(_$orderdatagrid);
        _$orderdatagrid.datagrid({
            abpMethod: _purchaseOrderService.getPagedPurchaseDetail,
            queryParams:
            {
                id: tempId
            },
            idField: 'goodsId',
            fitColumns: true,
            sortName: 'CreationTime',
            sortOrder: 'desc',
            striped: true, //奇偶行是否区分
            //singleSelect: true,//单选模式
            singleSelect: true,
            rownumbers: true,//行号
             //onClickCell: function (index, field) { _$orderdatagrid.datagrid("editCell", { index: index, field: field }); },
            columns: [[
                { field: 'ck', checkbox: true },
                { field: 'orderCode', title: '采购单号', width: 80, hidden: true },
                { field: 'goodsId', title: '主键', width: 80, hidden: true },
                { field: 'materielCode', title: '物料编码', width: 120 },
                { field: 'goodsName', title: '物料名称', width: 120 },
                { field: 'waresFomat', title: '规格型号', width: 120 },
                { field: 'goodsUnit', title: '单位', width: 120 },
                {
                    field: 'count', title: '采购数量', width: 120,
                    editor: {
                        type: 'numberbox',
                        options: {
                            required: true,
                            onChange: function (newValue, oldValue) {
                                var row = _$orderdatagrid.datagrid('getSelected');
                                console.log("ss", row.unitPrice)
                                console.log("qq", row.count)
                                var rindex = _$orderdatagrid.datagrid('getRowIndex', row);
                                var ed = _$orderdatagrid.datagrid('getEditor', {
                                    index: rindex,
                                    field: 'totalPrice'  //获取金额的编辑器
                                });
                                console.log("aqaq", ed)
                                $(ed.target).numberbox('setValue', newValue * row.unitPrice);  //修改值
                                _$orderdatagrid.datagrid('endEdit', lastIndex);
                                lastIndex = undefined;


                            }

                        }

                    }
                },
                {
                    field: 'unitPrice', title: '采购价(元)', width: 120,
                    editor: { type: 'numberbox', options: { precision: 2, required: true } },
                    formatter: function (value, row, index) {
                        if (row.unitPrice == undefined) {
                            row.unitPrice = 0;
                        }
                        return row.unitPrice;
                    }
                },
                {
                    field: 'totalPrice', title: '金额(元)', width: 120,
                    editor: {
                        type: 'numberbox'
                    }
                }
            ]],
            //onLoadSuccess: function () {
            //    _$fmprice.find(".Price").numberbox('setValue', 0);
            //    var rows = $fcgrid.datagrid('getRows');
            //    var total = 0;
            //    for (var i = 0; i < rows.length; i++) {
            //        total += parseFloat(rows[i]['totalPrice']); //获取指定列  
            //    }
            //    _$fmprice.find(".Price").numberbox('setValue', total);
            //},
            onBeforeLoad: function () {
                $(this).datagrid('rejectChanges');
            },

            //在用户点击一行的时候触发，参数包括：
            onClickRow: function (rowIndex) {
                if (lastIndex != rowIndex) {
                    if (endEditing()) {
                        _$orderdatagrid.datagrid('selectRow', rowIndex)
                            .datagrid('beginEdit', rowIndex);
                        lastIndex = rowIndex;
                        // _$orderdatagrid.datagrid('endEdit', lastIndex);
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
        //设置时间
        $('#buyTime').datebox('setValue', formatterDate(new Date()));
        fLoadOrderDataGrid();


        //添加物料
        $("#baddc").click(function () {
            console.log("adada", _$materieldialog)
            //$('#dgelc').dialog('open').dialog('setTitle', '选择物料信息');
            //url = 'PurchaseOrders/SelectMaterials/?id=';   
            _$materieldialog.dialog('open').dialog('setTitle', "添加物料");
            fLoadMaterielDataGrid();


            //展示物料信息在订单中
            $('.saveMinec').unbind("click").click(function () {
                //被选中的多行
                var rows = _$materieldatagrid.datagrid('getSelections');//返回所有被选中的行，
                console.info("选中物料", rows);
                if (rows.length == 0) {
                    _$materieldialog.dialog('close');
                }
                else {
                    _$materieldialog.dialog('close');
                    //返回当前页的所有行。
                    var selectRows = _$orderdatagrid.datagrid('getRows');
                    for (i = 0; i < rows.length; i++) {
                        var temp = rows[i];
                        var t = false;
                        for (j = 0; j < selectRows.length; j++) {
                            if (temp.id == selectRows[j].goodsId) {
                                t = true;
                            }
                        }
                        if (!t) {


                            _$orderdatagrid.datagrid("appendRow", {
                                goodsId: temp.id,
                                materielCode: temp.code,
                                goodsName: temp.waresName,
                                waresFomat: temp.waresFomat,
                                goodsUnit: temp.waresUnit,
                                unitPrice: temp.buyMoney

                            });

                            editIndex = _$orderdatagrid.datagrid('getRows').length + 1;//返回当前页的所有行。
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

                var rowIndex1 = _$orderdatagrid.datagrid('getRowIndex', node);//返回指定行的索引号，该行的参数可以是一行记录或一个ID字段值。
                _$orderdatagrid.datagrid('deleteRow', rowIndex1);

                //var total = options.element.find(".Price").numberbox('getValue');
                //var current = parseFloat(total) - node.amount;
                //options.element.find(".Price").numberbox('setValue', current);
                //var rowIndex = _$materieldatagrid.datagrid('getRowIndex', node);
                //rowIndex = undefined; 
                //  _$orderdatagrid.datagrid('clearSelections');
                //_$materieldatagrid.datagrid('uncheckRow', rowIndex); 
            }
            //_$orderdatagrid.datagrid('clearSelections');   
        });

    });

})();