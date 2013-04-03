// JavaScript Document
$(document).ready(function () {
    if (window.opener) {
        var id = $("#hdnIdOfUser").val();
        /*if (id == "0")
        {
        //window.opener.location.href = 'http://www.tsmaapplication.com/';
        window.opener.location.href = 'http://192.168.19.28:5069/';
        //window.opener.location.href = 'http://www.techsturedevelopment.com/';
        window.close();
        }
        else
        {*/
        //window.opener.location.href = 'http://www.tsmaapplication.com/scheduler';
        //window.opener.location.href = 'http://www.techsturedevelopment.com/scheduler';	
        if (id == 1) {
            window.opener.location.href = 'siteuserloginlocal';
		//	window.opener.location.href = 'http://so.techsturedevelopment.com/siteuserloginlocal';
        }
        else {
            window.opener.location.href = 'siteuserlogin';
			//window.opener.location.href = 'http://so.techsturedevelopment.com/siteuserlogin';
        }
        window.close();
        //}
    }
    else {
        //window.location.href = 'http://www.tsmaapplication.com/';
       // window.opener.location.href = 'http://so.techsturedevelopment.com/';					
        window.opener.location.href = 'http://192.168.19.28:5069/';
    }
});