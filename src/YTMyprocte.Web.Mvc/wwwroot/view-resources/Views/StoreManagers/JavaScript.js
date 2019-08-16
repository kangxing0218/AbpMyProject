
$(function () {
   
    $('#add1').textbox('textbox').bind('keyup', function () {
        //var tempValue = $(this).val();
        //var add2 = $('#add2').textbox('textbox').val();
        //$("#sum").textbox('setValue', tempValue*add2); 
        cucalCountMoney();
    });
    $('#add2').textbox('textbox').bind('keyup', function () {
        cucalCountMoney();
    });







});
