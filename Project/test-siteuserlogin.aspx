<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="test-siteuserlogin.aspx.vb" Inherits="tsma.test_siteuserlogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Total Social Media Application</title>
    <script language="JavaScript" type="text/javascript">
        function newCookie(name, value, days) {
            var days = 1;   // the number at the left reflects the number of days for the cookie to last
            // modify it according to your needs
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                var expires = "; expires=" + date.toGMTString();
            }
            else var expires = "";
            document.cookie = name + "=" + value + expires + "; path=/";
        }

        /*function readCookie(name) {
        alert("first");
        alert(name);
        var nameSG = name + "=";
        var nuller = '';
        if (document.cookie.indexOf(nameSG) == -1)
        return nuller;
    
        var ca = document.cookie.split(';');
        for(var i=0; i<ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameSG) == 0) return c.substring(nameSG.length,c.length); }
        return null; }*/

        function eraseCookie(name) {
            newCookie(name, "", 1);
        }

        function toMem(a) {
            newCookie('UserName', document.frm2.txtUid.value);     // add a new cookie as shown at left for every
            newCookie('Password', document.frm2.txtPwd.value);   // field you wish to have the script remember
        }

        function delMem(a) {
            eraseCookie('UserName');   // make sure to add the eraseCookie function for every field
            eraseCookie('Password');

            document.frm2.txtUid.value = '';   // add a line for every field
            document.frm2.txtPwd.value = '';
        }
        //-->
    </script>

   <script type="text/javascript" language="javascript">
       function ForgotPwdValidation() {
           var fields = "";

           var re = new RegExp();
           re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
           var sinput;
           sinput = "";

           if (DoTrim(document.getElementById('txtForget').value).length == 0) {

               fields = fields + "\n-- Email Address --";

           }
           else {
               sinput = DoTrim(document.getElementById('txtForget').value);
               if (!re.test(sinput)) {
                   fields = fields + "\n-- Invalid Email Address --";
                   document.getElementById('txtForget').value == "";
               }
           }
           if (fields != "") {
               fields = "Please fill in the following details:\n--------------------------------\n" + fields;
               alert(fields);
               return false;
           }
           else {
               return true;
           }
       }
       function OpenDiv() {
           //alert("test");
           $('#divForgotPwd').dialog({ autoOpen: false, bgiframe: true, modal: true });
           $('#divForgotPwd').dialog('open');
           $('#divForgotPwd').parent().appendTo($("form:first"));
       }
       /*function readCookie(name) {
       alert("SEcond");
       alert(name);
       var nameEQ = name + "=";
       var ca = document.cookie.split(';');
       for (var i = 0; i < ca.length; i++) {
       var c = ca[i];
       while (c.charAt(0) == ' ') c = c.substring(1, c.length);
       if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
       }
       return null;
       }*/

       function readCookie(name) {
           var strCookie = '';
           strCookie = readActualCookie(name);
           if (strCookie == 'undefined')
               strCookie = '';
           return strCookie;
       }
       function readActualCookie(name) {
           var nameSG = name + "=";
           var nuller = '';
           if (document.cookie.indexOf(nameSG) == -1)
               return nuller;

           var ca = document.cookie.split(';');
           for (var i = 0; i < ca.length; i++) {
               var c = ca[i];
               while (c.charAt(0) == ' ') c = c.substring(1, c.length);
               if (c.indexOf(nameSG) == 0)
                   return c.substring(nameSG.length, c.length);
           }
           return null;
       }

       function ValidatePass() {
           if ($('#txtForget').val() == '') {
               $('#txtForget').removeClass('input');
               $('#txtForget').addClass('input-validation-error');
               $('#spnForget').html('*');
           }
           else {
               $('#txtUid').removeClass('input-validation-error');
               $('#txtUid').addClass('input');
               $('#spnUid').html('');
           }
       }
       function DoTrim(strComp) {
           ltrim = /^\s+/
           rtrim = /\s+$/
           strComp = strComp.replace(ltrim, '');
           strComp = strComp.replace(rtrim, '');
           return strComp;
       }

       function validatelogin() {
           var fields = "";
           if (DoTrim(document.getElementById('txtUid').value).length == 0) {
               fields = fields + "\n-- UserName --";
           }
           if (DoTrim(document.getElementById('txtPwd').value).length == 0) {
               fields = fields + "\n-- Password --";
           }

           if (fields != "") {
               fields = "Please fill in the following details\n--------------------------------------\n" + fields;
               alert(fields);
               return false;
           }
           else {
               return true;
           }
       }
       function SiteUserLogin12() {
           var count = 0;
           $('#spnEror').html('');
           if ($('#txtUid').val() == '') {
               $('#txtUid').removeClass('input');
               $('#txtUid').addClass('input-validation-error');
               $('#spnUid').html('*');
               count = count + 1;
           }
           else {
               $('#txtUid').removeClass('input-validation-error');
               $('#txtUid').addClass('input');
               $('#spnUid').html('');
           }
           if ($('#txtPwd').val() == '') {
               $('#txtPwd').removeClass('input');
               $('#txtPwd').addClass('input-validation-error');
               $('#spnPwd').html('*');
               count = count + 1;
           }
           else {
               $('#txtPwd').removeClass('input-validation-error');
               $('#txtPwd').addClass('input');
               $('#spnPwd').html('');
           }
           if (count == 0) {
               return true;
           }
           else {
               return false;
           }
       }
       function ChangeLoginFocus(eve) {
           var code = eve.keyCode || eve.which;
           if (code == 13) {
               $('#imgLogin').click();
           }
       }
</script>
</head>
<body>
    <form id="frm2" runat="server"  onSubmit="if(this.checker.checked) toMem(this)">
    <div>
    <input  type="image" name="imglogin1" runat="server" onClick="return validatelogin();" id="imgLogin1" src="Content/images/login.png" alt="Login" >
    </div>
    </form>
</body>
</html>
