<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="contact-us-tab-new.aspx.vb" Inherits="tsma.contact_us_tab_new" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <link href="<%=ResolveUrl("~/Content/css/sidebar_style_2.css") %>" rel="stylesheet" type="text/css" />
    <style>
body {
width:810px;
overflow:hidden;
margin:0; padding:0; border:0;
}
</style>
<!--Update End css-->
<script type="text/javascript">
	function SetBodyStyle()
	{
		$("body").css("overflow", "auto");
	}

    function GetWidthHeight() {
        var strw = document.getElementById("divWidthHeight").style.width;
        document.getElementById("hdnWidth").value = strw;
        var strh = document.getElementById("divWidthHeight").style.height;
        document.getElementById("hdnHeight").value = strh;
        //return false;
    }
    function HideProgress() {
        parent.document.getElementById("imgLoading").style.display = 'none';
    }

    function ShowProgress() {
        parent.document.getElementById("imgLoading").style.display = 'block';
    }
</script>
<link type="text/css" href="<%=ResolveUrl("~/Content/css/sidebar_style.css") %>" rel="stylesheet" media="all" />
<link href="<%=ResolveUrl("~/Content/facebox/facebox.css") %>" media="screen" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/Content/facebox/faceplant.css") %>" media="screen" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%=ResolveUrl("~/Content/js/jquery-1.6.2.js") %>"></script>

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/jquery-ui.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        //	$('.pointer share').attr('rel','hi');
        FB.Canvas.setSize();
		
		$('a.lnkSubmit').click(function() {
			submitcontact();
		});
		
		$('a.lnkSubmitVisalus').click(function() {
			submitcontactvisalus();
		});
		
        $("img[class^=pointer share]").each(
				function () {
				    $(this).attr('onClick', "share();");
				}
			);
        $("img[class^=pointer share]").each(
				function () {
				    $(this).attr('onClick', "share();");
				}
			);


        var urlmain = $("div[rel^=videoMain] a:first").attr('rel');
        $("div[rel^=videoMain] a:first").attr('href', urlmain);
        var urlmain1 = $("div[rel^=videoMain2] a:first").attr('rel');
        $("div[rel^=videoMain2] a:first").attr('href', urlmain1);

        var url1 = $("div[rel^=video1] a:first").attr('rel');
        if (url1 == null | url1 == '') {
            $("div[rel^=video1] a:first").attr('href', 'javascript:;');
        }
        else {
            $("div[rel^=video1] a:first").attr('href', url1);
        }
        var url2 = $("div[rel^=video2] a:first").attr('rel');
        if (url2 == null | url2 == '') {
            $("div[rel^=video2] a:first").attr('href', 'javascript:;');
        }
        else {
            $("div[rel^=video2] a:first").attr('href', url2);
        }

        var url3 = $("div[rel^=video3] a:first").attr('rel');
        if (url3 == null | url3 == '') {
            $("div[rel^=video3] a:first").attr('href', 'javascript:;');
        }
        else {
            $("div[rel^=video3] a:first").attr('href', url3);
        }


        // alert(url3);

        //$('input, textarea').focus(function() {
        //			value=$(this).val();
        //			$(this).attr("value","");
        //		});
        //		$('input, textarea').blur(function() {
        //			if($(this).val()=="") {
        //				$(this).val(value);
        //			}
        //		});
        $("#name").focus(function () {
            $("#name").attr('readonly', '');
            $("#name").val($("#name").val().replace('Full Name', ''));
        });

        $("#name").blur(function () {
            if ($("#name").val() == '') {
                $("#name").val('Full Name');
            }
        });

        $("#email").focus(function () {
            $("#email").attr('readonly', '');
            $("#email").val($("#email").val().replace('Email', ''));
        });

        $("#email").blur(function () {
            if ($("#email").val() == '') {
                $("#email").val('Email');
            }
        });
        $("#phno").focus(function () {
            $("#phno").attr('readonly', '');
            $("#phno").val($("#phno").val().replace('Phone Number', ''));
        });

        $("#phno").blur(function () {
            if ($("#phno").val() == '') {
                $("#phno").val('Phone Number');
            }
        });
        $("#msg").focus(function () {
            $("#msg").attr('readonly', '');
            $("#msg").val($("#msg").val().replace('Enter Message Here...', ''));
        });

        $("#msg").blur(function () {
            if ($("#msg").val() == '') {
                $("#msg").val('Enter Message Here...');
            }
        });
		
		$("#fname").focus(function () {
            $("#fname").attr('readonly', '');
            $("#fname").val($("#fname").val().replace('*First Name', ''));
        });

        $("#fname").blur(function () {
            if ($("#fname").val() == '') {
                $("#fname").val('*First Name');
            }
        });
		
		
		$("#lname").focus(function () {
            $("#lname").attr('readonly', '');
            $("#lname").val($("#lname").val().replace('*Last Name', ''));
        });

        $("#lname").blur(function () {
            if ($("#lname").val() == '') {
                $("#lname").val('*Last Name');
            }
        });
		
		$("#phn").focus(function () {
            $("#phn").attr('readonly', '');
            $("#phn").val($("#phn").val().replace('*Phone Number', ''));
        });

        $("#phn").blur(function () {
            if ($("#phn").val() == '') {
                $("#phn").val('*Phone Number');
            }
        });
		
		$("#msg1").focus(function () {
            $("#msg1").attr('readonly', '');
            $("#msg1").val($("#msg1").val().replace('*Message', ''));
        });

        $("#msg1").blur(function () {
            if ($("#msg1").val() == '') {
                $("#msg1").val('*Message');
            }
        });

        $("#call").focus(function () {
            $("#call").attr('readonly', '');
            $("#call").val($("#call").val().replace('Best Time to Call', ''));
        });

        $("#call").blur(function () {
            if ($("#call").val() == '') {
                $("#call").val('Best Time to Call');
            }
        });

		 $("#email1").focus(function () {
            $("#email1").attr('readonly', '');
            $("#email1").val($("#email1").val().replace('*Email Address', ''));
        });

        $("#email1").blur(function () {
            if ($("#email1").val() == '') {
                $("#email1").val('*Email Address');
            }
        });
    });
    function DoTrim(strComp) {
        ltrim = /^\s+/
        rtrim = /\s+$/
        strComp = strComp.replace(ltrim, '');
        strComp = strComp.replace(rtrim, '');
        return strComp;
    }

    function submitcontact() {

		/*var regx = /\n|\r/g;
		var msgval = $("#msg").val().replace(regx,"<br/>") ;*/


        var fields;
        fields = "";
        if (DoTrim(document.getElementById('name').value).length == 0 || DoTrim(document.getElementById('name').value) == 'Full Name') {
            fields = "\n-- Full Name --";
        }

        var re = new RegExp();
        re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        var sinput;
        sinput = "";
        if (DoTrim(document.getElementById('email').value).length == 0 || DoTrim(document.getElementById('email').value) == 'Email') {
            fields = fields + "\n-- Email Address --";
        }
        else {
            sinput = DoTrim(document.getElementById('email').value);
            if (!re.test(sinput)) {
                fields = fields + "\n-- Invalid Email Address --";
                document.getElementById('email').value == "";
            }
        }
        //if (DoTrim(document.getElementById('phno').value).length == 0 || DoTrim(document.getElementById('phno').value)=='Phone Number' ) {
        //	fields = fields + "\n-- Phone Number --";
        //}
        //if (DoTrim(document.getElementById('msg').value).length == 0 || DoTrim(document.getElementById('msg').value)=='Enter Message Here...' ) {
        //	fields = fields + "\n-- Message --";
        //}
        if (fields != "") {
            fields = "Please fill in the following details:\n--------------------------------" + fields;
            alert(fields);
            return false;
        }
        else {
            //alert('test');
            //alert('ajax/ajax-custom-tab-info.aspx?nm='+ $("#name").val() +'&em='+ $("#email").val() +'&fbuid='+ $("#hdnFBUserId").val() +'&tsmauid='+ $("#hdnTSMAUserId").val() +'&fbpid='+ $("#hdnFBPageId").val() +'');
			var url='/ajax/ajax-custom-tab-info.aspx?nm=' + $("#name").val() + '&appid=' +  $("#hdnAppId").val()  + '&appname=' + $("#hdnAppName").val() + '&msg=' + $("#msg").val()  + '&em=' + $("#email").val() + '&ph=' + $("#phno").val() + '&fbuid=' + $("#hdnFBUserId").val() + '&tsmauid=' + $("#hdnTSMAUserId").val() + '&fbpid=' + $("#hdnFBPageId").val() + '&fbpagename=' + $("#hdnFBPageName").val();
            $.ajax({ url: url, success: function (result) {
                alert('Your information submitted successfully.');
                $("#name").val('Full Name');
                $("#email").val('Email');
                $("#phno").val('Phone Number');
                $("#msg").val('Enter Message Here...');
                //hideDivPopup('divForm');
                //showDivPopup('divMessage');			
            }
            });

            return true;
        }
    }

	
	function submitcontactvisalus() {
        var fields;
        fields = "";
        if (DoTrim(document.getElementById('fname').value).length == 0 || DoTrim(document.getElementById('fname').value) == '*First Name') {
            fields = "\n-- First Name --";
        }
		
		if (DoTrim(document.getElementById('lname').value).length == 0 || DoTrim(document.getElementById('lname').value) == '*Last Name') {
            fields = fields + "\n-- Last Name --";
        }

		if (DoTrim(document.getElementById('phn').value).length == 0 || DoTrim(document.getElementById('phn').value) == '*Phone Number') {
            fields =  fields + "\n-- Phone Number --";
        }
		
        var re = new RegExp();
        re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        var sinput;
        sinput = "";
        if (DoTrim(document.getElementById('email1').value).length == 0 || DoTrim(document.getElementById('email1').value) == '*Email Address') {
            fields = fields + "\n-- Email Address --";
        }
        else {
            sinput = DoTrim(document.getElementById('email1').value);
            if (!re.test(sinput)) {
                fields = fields + "\n-- Invalid Email Address --";
                document.getElementById('email1').value == "";
            }
        }
        //if (DoTrim(document.getElementById('phno').value).length == 0 || DoTrim(document.getElementById('phno').value)=='Phone Number' ) {
        //	fields = fields + "\n-- Phone Number --";
        //}
        if (DoTrim(document.getElementById('msg1').value).length == 0 || DoTrim(document.getElementById('msg1').value)=='*Message' ) {
        	fields = fields + "\n-- Message --";
        }
        if (fields != "") {
            fields = "Please fill in the following details:\n--------------------------------" + fields;
            alert(fields);
            return false;
        }
        else {
            //alert('test');
            //alert('ajax/ajax-custom-tab-info.aspx?nm='+ $("#name").val() +'&em='+ $("#email").val() +'&fbuid='+ $("#hdnFBUserId").val() +'&tsmauid='+ $("#hdnTSMAUserId").val() +'&fbpid='+ $("#hdnFBPageId").val() +'');
			var url='/ajax/ajax-custom-tab-info.aspx?nm=' + $("#fname").val() + '&lnm=' + $("#lname").val() +  '&em=' + $("#email1").val() + '&ph=' + $("#phn").val() + '&msg=' + $("#msg1").val() + '&fbuid=' + $("#hdnFBUserId").val() + '&tsmauid=' + $("#hdnTSMAUserId").val() + '&fbpid=' + $("#hdnFBPageId").val() + '&fbpagename=' + $("#hdnFBPageName").val();
            $.ajax({ url: url, success: function (result) {
                alert('Your information submitted successfully.');
                $("#name").val('Full Name');
                $("#email").val('Email');
                $("#phno").val('Phone Number');
                $("#msg").val('Enter Message Here...');
                //hideDivPopup('divForm');
                //showDivPopup('divMessage');			
            }
            });

            return true;
        }
    }


		
</script>
<script language="JavaScript" type="text/javascript">
<!--
    function printWindow() {
        bV = parseInt(navigator.appVersion);
        if (bV >= 4) window.print();
    }
    function ClickHereToPrint() {
        try {

            var oIframe = document.getElementById('ifrmPrint');
            var oContent = document.getElementById('divToPrint').innerHTML;
            var oDoc = (oIframe.contentWindow || oIframe.contentDocument);
            if (oDoc.document) oDoc = oDoc.document;
            oDoc.write("<html>");
            oDoc.write("</head><body onload='this.focus(); this.print();'>");
            oDoc.write(oContent + "</body></html>");
            oDoc.close();
        }
        catch (e) {
            self.print();
        }
    }
//  End -->
</script>
<script type="text/javascript">

    $(init);
    function init() {
        $('.makeMeDraggable').draggable({
            containment: '#content_bg',
            cursor: 'move',
            snap: '#content'
        });

    }
	
</script>
<script type="text/javascript" src="<%=ResolveUrl("~/Content/js/custom-new.js") %>"></script>
<script src="<%=ResolveUrl("~/Content/facebox/facebox.js") %>" type="text/javascript"></script>
<script type="text/javascript">
    function PopupCenter(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    } 
</script>
<script type="text/javascript">
    function chkTemplatePage3() {

        window.parent.show('warning_popup3');
        document.getElementById('warning_popup3').style.display = '';


        return confirm('Warning!  Please save your work before leaving this page.');
    }
    function SaveSidebar() {
        $("span[rel^=rm-css-text]").css("cursor", "default").css("border", "none");
        $("#commtimg").css("display", "none");
        $("#fbcommt").css("display", "inline");
        $('#spinner').fadeIn('slow');
        $(".urlandtext").css("cursor", "pointer");
        $(".url").css("cursor", "pointer");
        $(".pointer").css("cursor", "pointer");
        $(".hide").css("display", "none");
        $('.rm-css-pan-window').remove();
        $('.rm-css-selected-window').remove();
        $('.rm-css-highlight-window').remove();
        //presentation_html = $('#presentation_html').html();
        $(".hide").css("display", "");
        $("#commtimg").css("display", "inline");
        $("#fbcommt").css("display", "none");
        var test = document.getElementById("divSidebarHtml").innerHTML;
        document.getElementById("hdnSidebarContent").value = test;
        return true; //alert(test);
        //alert(document.getElementById("spnSidebar").innerHTML);

    }
</script>
<script type="text/javascript">
    function share(message) {

        //alert(message);
        //techsturedevelopment.com
        //		  FB.ui(
        //            {
        //            
        //                method: 'feed',
        //                name: 'Total Social Media System',
        //                link: 'http://techsturedevelopment.com',
        //                picture: 'http://techsturedevelopment.com/Content/images/we_build_img.jpg',
        //                caption: 'TSMA-BETA',
        //                description: 'You can share your reviews on Facebook',
        //                message: ''
        //            });

        //tsmaapplication.com
        //            FB.ui(
        //            {
        //                method: 'feed',
        //                name: 'ForRent.com',      
        //                link: 'http://www.tsmaapplication.com/',
        //                picture: 'http://www.tsmaapplication.com/Content/images/for_rent_logo.jpg',
        //                caption: 'ForRent.com',
        //                description: 'You can share your reviews on Facebook',
        //                message: ''
        //            });
        //so.techsturedevelopment.com	  
       /* FB.ui(
        {

            method: 'feed',
            name: 'Social Outbreak',
            link: 'http://so.techsturedevelopment.com',
            picture: 'http://so.techsturedevelopment.com/Content/images/we_build_img.jpg',
            caption: 'TSMA-Social Outbreak',
            description: 'You can share your reviews on Facebook',
            message: ''
        });*/
        //so.tsmaapplication.com  
          FB.ui(
        {
                     
        method: 'feed',
        name: 'Social Outbreak',
        link: 'https://www.summasocialapp.com',
        picture: 'https://www.summasocialapp.com/Content/images/we_build_img.jpg',
        caption: 'TSMA-Social Outbreak',
        description: 'You can share your reviews on Facebook',
        message: ''
        });

        //https://www.summasocialapp.com	  
      /*   FB.ui(
        {
                     
        method: 'feed',
        name: 'Summa Social',
        link: 'https://www.summasocialapp.com/',
        picture: 'https://www.summasocialapp.com/Content/images/we_build_img.jpg',
        caption: 'Summa Social',
        description: 'You can share your reviews on Facebook',
        message: ''
        });*/
        
        //https://www.uswebsocial.com	  
        /* FB.ui(
        {

        method: 'feed',
        name: 'US Web Social',
        link: 'https://www.uswebsocial.com/',
        picture: 'http://www.uswebsocial.com/Content/images/we_build_img.jpg',
        caption: 'US Web Social',
        description: 'You can share your reviews on Facebook',
        message: ''
        });*/
        //https://www.meadowcreeksocial.com	  
        /*   FB.ui(
        {
                     
        method: 'feed',
        name: 'Meadow Creek Social',
        link: 'https://www.meadowcreeksocial.com/',
        picture: 'http://www.meadowcreeksocial.com/Content/images/we_build_img.jpg',
        caption: 'Meadow Creek Social',
        description: 'You can share your reviews on Facebook',
        message: ''
        });*/

    }
</script>
<script type="text/javascript" src="<%=ResolveUrl("~/Content/js/ajax.js") %>"></script>
</head>
<body>
<script>
    window.fbAsyncInit = function () {
        FB.init({
            // appId: '270023716379041', //for techsturedevelopment.com
            //appId: '174376286006666',  //for tsmaapplication.com
            //appId: '343180435727440', //for so.techsturedevelopment.com
            appId: '343180435727440', //for so.tsmaapplication.com   
            //appId: '224415534331837', //for https://www.summasocialapp.com
            //appId: '281207455306383', //for https://www.uswebsocial.com
            //appId: '322568194479566', //for https://www.meadowcreeksocial.com
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true // parse XFBML
        });
    };

    (function () {
        var e = document.createElement('script');
        e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
        e.async = true;
        document.getElementById('fb-root').appendChild(e);
    } ());
</script>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js" type="text/javascript"></script>
  <div id='fb-root'></div>
    <script src='https://connect.facebook.net/en_US/all.js'></script>
<script type="text/javascript" charset="utf-8">
    FB.Canvas.setSize();
</script>


    <form id="form1" runat="server" >
    
   <div id="divContent" style="width:810px;" runat="server"></div>
    </form>
    <input type="hidden" id="hdnFBUserId" runat="server" name="hdnFBUserId"  />
  <input type="hidden" id="hdnTSMAUserId" runat="server" name="hdnTSMAUserId" />
  <input type="hidden" id="hdnFBPageId" runat="server" name="hdnFBPageId"  />
<input type="hidden" id="hdnFBPageName" runat="server" name="hdnFBPageName"  />
   <input type="hidden" id="hdnAppId" runat="server" name="hdnAppId"  />
   <input type="hidden" id="hdnAppName" runat="server" name="hdnAppName"  />
  <input type="hidden" id="hdnVideoTop"  name="hdnVideoTop"  />
</body>
</html>
<%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
	<script type="text/javascript" src="<%=ResolveUrl("~/Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.pack.js") %>"></script>
	<link rel="stylesheet" type="text/css" href="<%=ResolveUrl("~/Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.css") %>" media="screen" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Content/js/fancybox/fancybox/videofb.js") %>"></script>--%>
<script type="text/javascript">
    //$(".hide").css("display","block");
		$("#videoMain").click(function() {
		var vwidth = $("#videoMain").css('width');
		var vheight = $("#videoMain").css('height');
		var obj = $(".video").attr('rel');
		$("#videoMain").html('');
        if (obj != '') {
            var vid;
            var results;
            var url = DoTrim(obj).toLowerCase();
            //Youtube
            url = DoTrim(obj);
            results = url.match("[\\?&]v=([^&#]*)");
            var res;

            res = (results === null) ? "" : results[1];
            vid = res.replace("http://youtu.be/", "");
			$("#videoMain").html('<iframe width="' + vwidth +'" height="' + vheight +'" src="https://www.youtube.com/embed/' + vid + '?feature=player_detailpage&autoplay=1" frameborder="0" allowfullscreen></iframe>');
        }
		//$("#videoMain").html('<iframe width="' + vwidth +'" height="' + vheight +'" src="http://www.youtube.com/embed/' + vid + '?feature=player_detailpage&autoplay=1" frameborder="0" allowfullscreen></iframe>');
		//$('<iframe width="493" height="297" src="http://www.youtube.com/embed/q4Ob7OSLUyY?feature=player_detailpage&autoplay=1" frameborder="0" allowfullscreen></iframe>').appendTo('#videoMain')
		//$("#videoMain").Wrap('<iframe width="493" height="297" src="http://www.youtube.com/embed/q4Ob7OSLUyY?feature=player_detailpage&autoplay=1" frameborder="0" allowfullscreen></iframe>');
		//$("#videoMain").html("<iframe width='493' height='297' src='http://www.youtube.com/embed/q4Ob7OSLUyY?feature=player_detailpage&autoplay=1' frameborder='0' allowfullscreen></iframe>");
		return false;
	});
	
    $(".url").css("cursor", "pointer");
    $(".url").click(function (e) {
        url = $(this).attr('rel');
        index = $(".url").index(this);
        if (url == 'http://') {
            url = '';
        }
        if (url != '') {
            //seturl(url,index);
            setLocation(url);
        }
    });


    $(".urlandtext").click(function (e) {

        id = $(this).attr('id');
        url = $(this).attr('rel');
        index = $(".url").index(this);
        if (url == 'http://') {
            url = '';
        }
        if (url != '') {
            //seturl2(url,index);
            setLocation(url);
        }
    });



    /*function seturl(url, index) {

    //$(".url:eq("+index+")").removeAttr('onclick');
    alert(url);
    var str = 'setLocation(\'' + url + '\');'
    $(".url:eq(" + index + ")").attr('rel', url);
    $(".url:eq(" + index + ")").attr('onClick', str);
    // alert('.url:eq(" + index + ")").attr('onclick', 'setLocation(\'' + url + '\');"');
    //alert (url+' : '+index);

    }

    function seturl2(url, index) {
    //   alert(url);
    var str = 'setLocation(\'' + url + '\');'
    $(".urlandtext:eq(" + index + ")").attr('rel', url);
    $(".urlandtext:eq(" + index + ")").attr('onClick', str);
    //$(".urlandtext:eq(" + index + ")").attr('onClick', 'setLocation(\'' + url + '\');');
    //alert (url+' : '+index);
    }*/
	$(".urlwithouthide").css("cursor", "pointer");
	$(".urlwithouthide").click(function (e) {
        url = $(this).attr('rel');
        index = $(".urlwithouthide").index(this);
        if (url == 'http://') {
            url = '';
        }
        if (url != '') {
            //seturl(url,index);
            setLocation(url);
        }
    });
	
	$(".urlandtextwithhide").css("cursor", "pointer");
	$(".urlandtextwithhide").click(function (e) {

        id = $(this).attr('id');
        url = $(this).attr('rel');
		if(url=='http://')
		{
			url='';
		}
		if(url !='')
		{
			//seturlandtextwithhide(url,index);
			setLocation(url);
		}
    });
	
    function setLocation(pageURL) {
        //alert(pageURL);
        if (pageURL.indexOf("http://") != -1 || pageURL.indexOf("https://") != -1) {
            // alert("testse");
            window.open(pageURL);
        }
        else {
            //alert("Tessetteattttetrd");
            window.open("http://" + pageURL);
        }
    }
    //function setLocation(url) {
    //	window.open(url);
    //return url;
    // }



    $('#playvideo').click(function () {

        $("#Video1").css('display', 'none');
        $("#Youtube1").css('display', '');
        $("#Youtube1").attr('src', 'https://www.youtube.com/embed/rQJelSFgFs0?autoplay=1&cc_load_policy=1');
    })

    $('#playvideo2').click(function () {

        $("#Video2").css('display', 'none');
        $("#Youtube2").css('display', '');
        $("#Youtube2").attr('src', 'https://www.youtube.com/embed/DVOuO-187e8?autoplay=1&cc_load_policy=1');
    })


    function ShowVideo() {

        $("#Video1").css('display', 'none');
        $("#Youtube1").css('display', '');
        $("#Youtube1").attr('src', 'https://www.youtube.com/embed/rQJelSFgFs0?autoplay=1&cc_load_policy=1');

    }

</script>    