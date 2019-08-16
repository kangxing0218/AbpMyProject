(function () {
    var _storeManagerService = abp.services.app.storeManager;
    var _materielService = abp.services.app.materiel;
    var _$minedatagrid = $('#dg')
    var _$minedialog = $("#dlg");
    var _$materieldialog = $("#dlggc");
    var _$materieldatagrid = $('#dgc');
    var _$minetbr = $("#toolbar");
    var _$minetbr1 = $("#toolbar1");
    $(function () {

        fLoadQueryData();
        fLoadDataGrid();

        $('#btnadd').click(function () {
            $('#dlg').dialog('open').dialog('setTitle', '创建供应商');
            var url = 'StoreManagers/CreateOrUpdateModal/?id=';

            actionRoles("创建用户信息", "恭喜您，创建成功！", null, url);
        });

        $('#btnedit').click(function () {
          
            var node = _$minedatagrid.datagrid('getSelected');
            if (node) {
                //var args = { "id": node.Id };
                var url = 'StoreManagers/CreateOrUpdateModal/?id=' + node.id;

                actionRoles("编辑库存信息", "恭喜您，修改成功！", node.id, url);

            } else {
                $.messager.show({
                    title: "提示",
                    msg: '请选择要操作的记录'
                });
            }
        });
        $('#btnremove').click(function () {
            //选中要删除行，if语句判断是否选中
            var dataSelected = $("#dg").datagrid("getSelected");
            if (dataSelected !== null) {//取到选中数据
                $.messager.confirm('确认', '您确认想要删除该供应商吗？', function (r) {
                    if (r) {
                        _storeManagerService.deleteStoreManager({
                            id: dataSelected.id
                        }).done(function () {
                            freshPage();
                            $.messager.show({
                                title: "提示",
                                msg: '恭喜您，删除成功！'
                            });
                        });
                    }
                });
            } else//未选中需编辑数据行
            {
                $.messager.show({
                    title: '删除',
                    msg: '请选择要删除的供应商！'
                });
            }

        });
 


    function fLoadDataGrid() {
        abp.ui.setBusy(_$minedatagrid);
        _$minedatagrid.datagrid({

            abpMethod: _storeManagerService.getPagedStoreManager
            //queryParams: _$minetbr.find('form').serializeFormToObject()
        });
        abp.ui.clearBusy(_$minedatagrid);
    }
    function fLoadQueryData() {
        function onComboboxHidePanel() {
            var el = $(this);
            el.combobox('textbox').focus();
            // 检查录入内容是否在数据里
            var opts = el.combobox("options");
            var data = el.combobox("getData");
            var value = el.combobox("getValue");
            // 有高亮选中的项目, 则不进一步处理
            var panel = el.combobox("panel");
            var items = panel.find(".combobox-item-selected");
            if (items.length > 0) {
                var values = el.combobox("getValues");
                el.combobox("setValues", values);
                return;
            }
            var allowInput = opts.allowInput;
            if (allowInput) {
                var idx = data.length;

                data[idx] = [];
                data[idx][opts.textField] = value;
                data[idx][opts.valueField] = value;
                el.combobox("loadData", data);
            } else {
                // 不允许录入任意项, 则清空
                el.combobox("clear");
            }
        }
    }    
    
    function actionRoles(title, message, id, data) {
        // 执行一些动作...
        console.info("id", id);
        console.info("url333", data);
        _$minedialog.dialog('open').dialog('refresh', data).dialog('setTitle', title);
        //console.info("表单", _$form);
        //  abp.ui.setBusy(_$minedialog);
        $('.saveMine').unbind("click").click(function () {
            var _$form = _$minedialog.find('form');
            //  console.info("表单", _$form);
            if (!_$form.form('validate')) {

                return;
            }
            abp.ui.setBusy(_$minedialog);
            // _$form.validate();
            var storeManager = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js   
            console.info("库存信息", storeManager);
            _storeManagerService.createOrUpdateStoreManager({
                storeManager: storeManager

            }).done(function () {
                _$minedialog.dialog('close');
                freshPage(); //reload page to see new user!

                // abp.notify.success(message);
                $.messager.show({
                    title: "提示",
                    msg: message
                });
            }).always(function () {
                abp.ui.clearBusy(_$minedialog);
            });
        });

    }
    function freshPage() {
        _$minedatagrid.datagrid("reload"); //reload page to see new user!
    }

    _$minedatagrid.datagrid({
        method: 'post',
        fit: false,
        sortName: 'CreationTime',
        sortOrder: 'desc',
        idField: 'id',
        pageSize: 10,
        pageList: [10, 20, 30, 40, 50],
        pagination: true,
        singleSelect: true,//单选模式
        fitColumns: true,
        rownumbers: true,//行号
        toolbar: "#toolbar",
        frozenColumns: [[
            { field: 'id', title: '主键', width: 80, hidden: true },
            { field: 'code', title: '编码', width: 80 },
            { field: 'materielName', title: '商品名称', width: 120, sortable: true },
        ]],
        columns: [[
            { field: 'materielUnit', title: '单位', width: 120, sortable: true },
            { field: 'materielMoney', title: '价格', width: 120, sortable: true     },
            { field: 'currentAmount', title: '当前库存', width: 120, sortable: true},
            { field: 'countMoney', title: '总金额', width: 120, sortable: true}
        ]]
        });


        //前端数据 验证  sell

        //自定义金额校验规则
        var postReg = /^[1][3,4,5,7,8][0-9]{9}$/;
        $.extend($.fn.validatebox.defaults.rules, {
            telePhone: {
                validator: function (value, param) {
                    return postReg.test(value);
                },
                message: '联系方式输入有误！'
            }

        }); 
        //输入两个数据求和

        $('#add1').textbox({ 
            onChange: function (value) {    
            var aa0 = this.value; console.info(aa0)
         }  
         });

        //function sum() {
        //var inputs = document.getElementsByTagName("input");
        //    for (i = 0; i < inputs.length; i++) {
        //        inputs[i].onblur = function () {
        //            var add1 = document.getElementById("add1").value;
        //            console.info("id", add1);
        //            var add2 = document.getElementById("add2").value;
        //            document.getElementById("sum").value = add1 * add2 *;
        //        }
        //    }
        //}






        $("#btnacd").click(function () {
            console.log("adada", _$materieldialog)
            //$('#dgelc').dialog('open').dialog('setTitle', '选择物料信息');
            //url = 'PurchaseOrders/SelectMaterials/?id=';   
            _$materieldialog.dialog('open').dialog('setTitle', "添加物料");
            fLoadMaterielDataGrid();


            //展示物料信息在订单中
            $('.saveMinec').unbind("click").click(function () {
                //被选中的多行
                var rows = $("#dgc").datagrid("getSelected");//返回所有被选中的行，
                console.info("选中物料", rows);
                if (rows.length == 0) {
                    _$materieldialog.dialog('close');
                } else if (rows.length > 2) {
                    $.messager.show({
                        title: "提示",
                        msg: '只能选择一行'
                    });
                } 
                else {
                  
                            _$minedatagrid.datagrid("appendRow", {
                                //goodsId: temp.id,
                                code: rows.code,
                                materielName: rows.waresName,
                               // waresFomat: temp.waresFomat,
                                materielUnit: rows.waresUnit,
                                materielMoney: rows.buyMoney

                            });

                            editIndex = _$minedatagrid.datagrid('getRows').length + 1;//返回当前页的所有行。
                          //  _$minedatagrid.datagrid('selectRow', lastIndex).datagrid('beginEdit', lastIndex);

                            //editIndex = _$orderdatagrid.datagrid('getRows').length-1;
                            //_$orderdatagrid.datagrid('selectRow', editIndex).datagrid('beginEdit', editIndex);  
                     
                
                    // _$orderdatagrid.datagrid("loadData",{"rows":rows});   
                }
                _$materieldatagrid.datagrid('clearSelections');
            });
            $('.consolec').unbind("click").click(function () {
                _$materieldialog.dialog('close');
            });
        });


        function fLoadMaterielDataGrid() {
            //  console.log("wwww", _$materieldatagrid)
            abp.ui.setBusy(_$materieldatagrid);
            _$materieldatagrid.datagrid({
                abpMethod: _materielService.getPagedMateriel,
                queryParams: _$minetbr1.find('form').serializeFormToObject(),
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


       
    });
})();


/**
 *  
 */
function cucalCountMoney() {
    var add1 = $("#add1").textbox("textbox").val();
    var add2 = $('#add2').textbox('textbox').val();
   // var add3 = $("#add2").textbox("getValue");
    
    var add = add1 * add2;
    $("#sum").textbox("setValue", add);    
}
