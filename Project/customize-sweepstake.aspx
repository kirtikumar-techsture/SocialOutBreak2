﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customize-sweepstake.aspx.vb" ValidateRequest="false" Inherits="tsma.customize_sweepstake"  EnableEventValidation="false"%>
<%@ Register src="inner-footer.ascx" tagname="inner" tagprefix="uc2" %>
<%@ Register src="left.ascx" tagname="left" tagprefix="uc3" %>
<%@ Register src="inner-header.ascx" tagname="inner" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml" >
<!--Update css-->
<head>
<style type="text/css" media="screen">
    #divBackGround 
    {
        position:absolute;
        background-color:#ffffff;
    }
</style>
<link href="<%=ResolveUrl("Content/css/sidebar_style_2.css")%>" rel="stylesheet" type="text/css" />
<link href="Content/facebookalert/facebookalert_files/facebook.publish.css" rel="stylesheet" type="text/css">
<link type="text/css" href="<%=ResolveUrl("Content/css/sidebar_style.css")%>" rel="stylesheet" media="all" />
<link href="<%=ResolveUrl("content/facebox/facebox.css")%>" media="screen" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("content/facebox/faceplant.css")%>" media="screen" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("content/js/jquery-1.7.1.js")%>"></script>
<script src="<%=ResolveUrl("content/js/jquery.ui.core.js")%>"></script>
<script src="<%=ResolveUrl("content/js/jquery.ui.widget.js")%>"></script>
<script src="<%=ResolveUrl("content/js/jquery.ui.mouse.js")%>"></script>
<script src="<%=ResolveUrl("content/js/jquery.ui.draggable.js")%>"></script>
<script src="Content/js/redirect-to-home.js" type="text/javascript"></script>
<!--Update End css-->
<script type="text/javascript">
	$(function () {
			$("#divLiked").css("display", "none");
			$("#divUnLiked").css("display", "");
	});	
	
    function GetWidthHeight() {
        var strw = document.getElementById("divWidthHeight").style.width;
        document.getElementById("hdnWidth").value = strw;
        var strh = document.getElementById("divWidthHeight").style.height;
        document.getElementById("hdnHeight").value = strh;
        return true;
        //return false;
    }
    /* function HideProgress() {
    parent.document.getElementById("imgLoading").style.display = 'none';
    }

    function ShowProgress() {
    parent.document.getElementById("imgLoading").style.display = 'block';
    }
    */
    function showPages() {
        var pos;
        pos = findPos(document.getElementById('img1'));
        var lft = pos[0] - 325;
        document.getElementById('Fanpagesdiv1').style.left = lft + 'px';
        lft = pos[1] +15;
        document.getElementById('Fanpagesdiv1').style.top = lft + 'px';
        $('#Fanpagesdiv1').slideDown('slow');
    }
    function ClosePage() {
        $('#Fanpagesdiv1').slideUp('slow');
    }
    function findPos(obj) {
        var curleft = curtop = 0;
        if (obj.offsetParent) {
            while (obj.offsetParent) {
                curleft += obj.offsetLeft;
                curtop += obj.offsetTop;
                obj = obj.offsetParent;
            }
        }
        return [curleft, curtop];

    }
    function PostCustomTab(obj) {
        var tmpId;
        tmpId = obj.id.replace('hrefPost', 'hdnPageName');
        tmpId = confirm('Are You Sure You Want to Add Custom Tab on ' + $('#' + tmpId).val() + '?');
        if (tmpId == true) {
            ClosePage();

            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();

            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }

            xmlhttp.onreadystatechange = function () {

                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                    hideDivPopup('divLoading');
                    if (xmlhttp.responseText == '') {
                        //  $('#spinner').fadeIn('slow');
                        $('#divLoading').slideUp('slow');
                        showDivPopup('dvMessage');
                    }
                    else {
                        document.getElementById('spnMsg').innerHTML = xmlhttp.responseText;
                    }
                }
            }
            //  $('#spinner').fadeIn('slow');
            showDivPopup('divLoading');
            xmlhttp.open("POST", "ajax/ajax-post-customtab.aspx?pid=" + document.getElementById(obj.id.replace('hrefPost', 'hdnPageId')).value + "&pat=" + document.getElementById(obj.id.replace('hrefPost', 'hdnAccessToken')).value + "&pnm=" + document.getElementById(obj.id.replace('hrefPost', 'hdnPageName')).value + "&id=" + document.getElementById('hdnCTID').value, true);
            xmlhttp.send();
            //  $('#spinner').fadeOut('slow');
            ClosePage();
            //  alert('Your Business Page uploaded to Facebook Successfully');
            //  alert("ajax/ajax-post-customtab.aspx?pid=" + document.getElementById(obj.id.replace('hrefPost', 'hdnPageId')).value + "&pat=" + document.getElementById(obj.id.replace('hrefPost', 'hdnAccessToken')).value + "&pnm=" + document.getElementById(obj.id.replace('hrefPost', 'hdnPageName')).value + "&id=" + document.getElementById('hdnCTID').value);
        }
    }

    function SaveAlert() {
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        $('#DivSaveCustomTab').css({ 'width': maskWidth, 'height': maskHeight });
        $("#DivSaveCustomTab").show("slow");
    }
    function HideSaveAlert() {
        $("#DivSaveCustomTab").hide("slow");
    }
    function PublishSaveAlert() {
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        $('#DivPublishSaveCustomTab').css({ 'width': maskWidth, 'height': maskHeight });
        $("#DivPublishSaveCustomTab").show("slow");
    }
    function HidePublishSaveAlert() {
        $("#DivPublishSaveCustomTab").hide("slow");
    }
    function PublishAlert() {
        //$("#spnMessage").html(mess);
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        $('#DivPublishCustomTab').css({ 'width': maskWidth, 'height': maskHeight });
        $("#DivPublishCustomTab").show("slow");
    }
    function HidePublishAlert() {
        $("#DivPublishCustomTab").hide("slow");
    }
    function ShareAlert() {
        //$("#spnMessage").html(mess);
        $("#DivShareCoverPhoto").show("slow");
    }
    function PublishCustomTab() {
        __doPostBack("btnUpload", "");
    }
    function HideCustomTabNameAlert() {
        $("#DivCustomTabName").hide("slow");
        $('#spinner').fadeIn('slow');
        __doPostBack("lnkSave", "");
    }

   
</script>

<link href="Content/facebookalert/facebookalert_files/facebook.alert.css" rel="stylesheet" type="text/css">
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/jquery-ui.min.js"></script>
<script type="text/javascript" src="Content/facebookalert/facebookalert_files/jquery_facebook.alert.js"></script>
<script type="text/javascript">
    function FacebookAlert() {
        jConfirm('Warning: Reset will discard your changes.', 'Reset Custom Tab',
				    function (r) {
				        if (r == true) {
				            __doPostBack("lnkReset", "");
				        }
				    });

    }
		</script>
<script type="text/javascript">
    $(init);
    function init() {
        /*var disabled = $( ".makeMeDraggable" ).draggable( "option", "disabled" );
        $( ".makeMeDraggable" ).draggable('option', 'disabled', true );
        $('.makeMeDraggable').draggable({
        containment: '#content_bg',
        cursor: 'move',
        snap: '#content'
        });*/
		
		$("#chkRules").click(function() {
			var checked_status = this.checked;
			if (checked_status == true) {
			   $("#btnPublish").removeAttr("disabled");
			   $("#btnPublish").removeClass("disinputbutton");
			   $("#btnPublish").addClass("inputbutton");			   
			} else {
			   $("#btnPublish").attr("disabled", "disabled");
			   $("#btnPublish").removeClass("inputbutton");						   
			   $("#btnPublish").addClass("disinputbutton");
			}
		});
    }
    function MoveContent(obj) {
        if (obj == '1') {
            $('#lnkDisMove').css('display', '');
            $('#lnkEnaMove').css('display', 'none');
            var disabled1 = $(".makeMeDraggable").draggable("option", "disabled");
            $(".makeMeDraggable").draggable('option', 'disabled', false);
            $(".makeMeDraggable").draggable("option", "revert", false);
            $('.makeMeDraggable').draggable({
                containment: '#content_bg',
                cursor: 'move',
                snap: '#content'
            });

        }
        else if (obj == '0') {
            $('#lnkDisMove').css('display', 'none');
            $('#lnkEnaMove').css('display', '');
            $(".makeMeDraggable").draggable("option", "revert", true);
        }
    }
</script>
<script type="text/javascript" src="Content/js/custom-new.js"></script>
<script src="Content/facebox/facebox.js" type="text/javascript"></script>
<script type="text/javascript">
    function PopupCenter(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        targetWin.focus();
    } 
</script>
<script type="text/javascript">
    function chkTemplatePage3(mess) {
        var test = $('#hdnIsSaved').val();
        if (test == "1") {
            window.parent.location.href = 'tabs-list.aspx?cId=<%=request("cid")%>';
            return true;
        }
        else {
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            $('#DivSaveBeforeExistTab').css({ 'width': maskWidth, 'height': maskHeight });
            $("#spnMessage").html(mess);
            $("#DivSaveBeforeExistTab").show("slow");
        }
    }

    function HideLinkAlert2() {
        $("#DivSaveBeforeExistTab").hide("slow");
    }
    function ChangedPage2() {
        window.parent.location.href = 'tabs-list.aspx?cId=<%=request("cid")%>';
        return true;
    }

    function SaveCustomTab() {
        parent.document.getElementById('hdnSaveHeader').value = "1";
        document.getElementById('hdnIsSaved').value = "1";
        $("span[rel^=rm-css-text]").css("cursor", "default").css("border", "none");
        $("#commtimg").css("display", "none");
        $("#fbcommt").css("display", "inline");
        //$('#spinner').fadeIn('slow');
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
		//$('#hdnUnLike').val(document.getElementById("divUnLikeHtml").innerHTML);	
		$('#hdnLike').val(document.getElementById("divSweepstakeHtml").innerHTML);	
		//alert($('#hdnLike').val());
//		alert($('#hdnUnLike').val());				
		return true; 
    }
	
	function SaveLikeUnlike(obj) {
		parent.document.getElementById('hdnSaveHeader').value = "0";
        document.getElementById('hdnIsSaved').value = "0";
		if(parseInt(obj) == 1)
		{
			//alert('abc' + $('#spnLiked').attr('rel'));
			$('#hdnSaveMenu').val('2');
			$('#hdnLike').val(document.getElementById("divSweepstakeHtml").innerHTML);	
			$("#lnkSaveBeforeLike").removeClass("likedeactive");			
			$("#lnkSaveBeforeLike").addClass("likeactive");
			$('#divSaveLike').css('display','none');
			$('#divSaveUnLike').css('display','');
			$('#divSaveShare').css('display','none');
			$("#lnkSaveAfterLike").addClass("likedeactive");
			$("#lnkSaveShareText").addClass("likedeactive");
			$("#divLiked").css("display", "none");
			$("#divUnLiked").css("display", "");
			$('#divSweepstakeHtml').css('display','');
		//	$('#divUnLikeHtml').css('display','');
//			$('#divLikeHtml').css('display','none');
			$('#CustomiseUserMessages').css('display','none');
        }
		else if (parseInt(obj) == 2)
		{
			//alert(parseInt(obj));
			
			$('#hdnSaveMenu').val('3');			
			$('#hdnUnLike').val( document.getElementById("divSweepstakeHtml").innerHTML);	
			$('#divLiked').css('display', '');
			$("#divUnLiked").css('display', 'none');	
//			alert("divLiked : " + $('#divLiked').css('display'));
//			alert("divUnLiked : " + $('#divUnLiked').css('display'));
			$('#divSaveLike').css('display','');
			$('#divSaveUnLike').css('display','none');
			$('#divSaveShare').css('display','none');
			$("#lnkSaveBeforeLike").addClass("likedeactive");
			$("#lnkSaveAfterLike").removeClass("likedeactive");
			$("#lnkSaveAfterLike").addClass("likeactive");
			$("#lnkSaveShareText").addClass("likedeactive");
			//$('#divUnLikeHtml').css('display','');
//			$('#divLikeHtml').css('display','none');
			$('#divSweepstakeHtml').css('display','');		
			$('#CustomiseUserMessages').css('display','none');
		}
		else if (parseInt(obj) == 3)
		{
			$('#hdnSaveMenu').val('3');
			$('#hdnLike').val(document.getElementById("divSweepstakeHtml").innerHTML);	
			$("#lnkSaveBeforeLike").addClass("likedeactive");
			$("#lnkSaveAfterLike").addClass("likedeactive");
			$("#lnkSaveShareText").removeClass("likedeactive");				
			$("#lnkSaveShareText").addClass("likeactive");
			$('#divSaveLike').css('display','none');
			$('#divSaveUnLike').css('display','none');
			$('#divSaveShare').css('display','');
			$('#divSweepstakeHtml').css('display','none');
			//$('#divUnLikeHtml').css('display','none');
//			$('#divLikeHtml').css('display','none');
			$('#CustomiseUserMessages').css('display','');
		}
		return true; //alert(test);
    }

    function OpenDivCustomTabName() {
        SaveCustomTab();
        GetWidthHeight();
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        $('#DivCustomTabName').css({ 'width': maskWidth, 'height': maskHeight });
        $('#DivCustomTabName').show('slow');

    }

    function OpenDivShareCustomTabName() {
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        $('#DivCustomTabShareEmail').css({ 'width': maskWidth, 'height': maskHeight });
        $('#DivCustomTabShareEmail').show('slow');
    }
    function PublishCustomTab1() {
        parent.document.getElementById('hdnSaveHeader').value = "1";
        document.getElementById('hdnIsSaved').value = "1";
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
		//$('#hdnUnLike').val(document.getElementById("divUnLikeHtml").innerHTML);	
		$('#hdnLike').val(document.getElementById("divSweepstakeHtml").innerHTML);	
      /*  var test = document.getElementById("divLikeHtml").innerHTML;
        document.getElementById("hdnSidebarContent").value = test;
      */  return true; //alert(test);
        //alert(document.getElementById("spnSidebar").innerHTML);
    }

    function DoTrim(strComp) {
        ltrim = /^\s+/
        rtrim = /\s+$/
        strComp = strComp.replace(ltrim, '');
        strComp = strComp.replace(rtrim, '');
        return strComp;
    }
    function ValidateUserEmail() {
        var fields = "";
        if (DoTrim(document.getElementById("txtUserEmail").value).length == 0) {
            fields = fields + "\n-- Email Address --";
        }
        else {
            var re = new RegExp();
            re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (!re.test(document.getElementById("txtUserEmail").value)) {
                fields = fields + "\n-- Invalid Email Address --";
            }
        }
        if (fields != "") {
            fields = "Please fill in the following details:\n--------------------------------\n" + fields;
            alert(fields);
            return false;
        }
        else {
            PublishCustomTab1();
            GetWidthHeight();
            return true;
        }
    }

    function DownloadPDF() {

        __doPostBack("lnkdownloadrulepdf", "");

    }
</script>
<style>
html, body {
	padding:0px;
	margin:0px;
	background-color: transparent;
}
</style>
<script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
</head>
<body class="rm-widget disabled" id="body" style="background-image:url(Content/images/for_rent_in_header_bg_new.gif); background-repeat:repeat-x; background-position:top;">
<form id="frm1" runat="server">
<asp:ScriptManager ID="objScriptManager1" runat="server"></asp:ScriptManager>
<div id="innerpagepagemain">
  <uc1:inner ID="inner1" runat="server" />
  <div id="centermain">
    <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="170" align="left" valign="top" class="leftbg"><uc3:left ID="left1" runat="server" />
        </td>
        <td align="right" valign="top" class="contentbody">
        <div id="divLoading" style="position:absolute;" >
<asp:UpdateProgress ID="UpdateProgressLib" runat="Server" DisplayAfter="0">
 <ProgressTemplate>
 <div style="width:100%; height:100%; text-align:center; background-image:url(../Content/facebookalert/images/popup_bg.png);  position:absolute; z-index:999999999; text-align:center;">
 <div class="spinner1Class" >
 <img src="content/images/bigspinner.gif" alt="Loading" align="left" />
 
 </div>
 </div>
 </ProgressTemplate>
 </asp:UpdateProgress>
</div>
<%--<asp:UpdatePanel ID="uptMain1" runat="server">
<ContentTemplate>
--%>        <input type="hidden" id="hdnCTID" runat="server" value='' />
          <!-- Content End  here -->
          <!--new_html-->
          <input type="hidden" id="hdnSidebarContent" runat="server" />
          <input type="hidden" id="hdnWidth" runat="server"/>
          <input type="hidden" id="hdnHeight" runat="server" />
          <input type="hidden" id="hdnIsSaved" value="1"/>
          <input type="hidden" id="hdnPublished" runat="server" value="1" />
          <div id="DivSaveCustomTab" style="width:100%; height:100%; text-align:center; background-image:url(Content/facebookalert/images/popup_bg.png); position:absolute; z-index:999999999; text-align:center;  display:none;">
            <div id="popup_container1" style="width:450px; height:80px;"  >
              <div id="popup_content" style="padding-top:10px; padding-left:20px; text-align:left"> Your work has been saved<br/>
                <br/>
                <input type="button" class="inputbutton" style="cursor:pointer" onclick="HideSaveAlert();" value="Close" id="lnkRedirect" runat="server"/>
              </div>
            </div>
          </div>
          <div id="DivPublishSaveCustomTab" style="width:100%; height:100%; text-align:center; background-image:url(Content/facebookalert/images/popup_bg.png); position:absolute; z-index:999999999; text-align:center;  display:none;">
            <div id="popup_container1" style="width:450px; height:80px;"  >
              <div id="popup_content" style="padding-top:10px; padding-left:20px; text-align:left"> Sweepstakes published to your Page successfully<br/>
                <br/>
                <input type="button" class="inputbutton" style="cursor:pointer" onclick="HidePublishSaveAlert();" value="Close" id="lnkRedirectPublish" runat="server"/>
              </div>
            </div>
          </div>
          <div id="DivCustomTabName" style="width:100%; height:100%; text-align:center; background-image:url(Content/facebookalert/images/popup_bg.png); position:absolute; z-index:999999999; text-align:center;  display:none;">
            <div id="popup_container1" style="width:450px; height:80px;" >
              <div id="popup_content" style="padding-top:10px; padding-left:20px; text-align:left"> 
              Sweepstakes Name:
                <input type="text" runat="server" id="txtCustomTabName" width="100" height="20" />
                <br/>

                <br/>
                <a href="javascript:;" id="lnkSave" runat="server" class="bluetablink">Save</a>&nbsp;&nbsp;<a href="#" id="lnkClose" runat="server" style="display:" class="bluetablink" onclick='$("#DivCustomTabName").hide("slow");'>Cancel</a> </div>
            </div>
          </div>
          <div id="DivCustomTabShareEmail" style="width:100%; height:100%; text-align:center; background-image:url(Content/facebookalert/images/popup_bg.png); position:absolute; z-index:999999999; text-align:center;  display:none;">
            <div id="popup_container1" style="width:450px; height:80px;" >
              <div id="popup_content" style="padding-top:10px; padding-left:20px; text-align:left"> Enter User's Primary Email:
                <input type="text" runat="server" id="txtUserEmail" width="100" height="20" />
                <br/>
                <br/>
                <a href="javascript:;" id="lnkShareUserEmail" runat="server" style="display:" class="bluetablink" onclick="return ValidateUserEmail();">Share</a>&nbsp;&nbsp; <a href="javascript:;" class="bluetablink" onclick='$("#DivCustomTabShareEmail").hide("slow");'>Cancel</a> </div>
            </div>
          </div>
          <div id="DivShareCoverPhoto" style="width:100%; height:100%; text-align:center;  background-image:url(Content/facebookalert/images/popup_bg.png);  position:absolute; z-index:999999999; text-align:center;  display:none;">
            <div id="popup_container1" style="width:450px; height:80px;"  >
              <div id="popup_content" style="padding-top:10px; padding-left:20px; text-align:left"> Sweepstakes Shared Successfully<br/>
                <br/>
                <input type="button" class="inputbutton" value="Close" id="lnkRedirectShare" runat="server" />
              </div>
            </div>
          </div>
          <div id="DivPublishCustomTab" style="width:100%; height:100%; text-align:center; background-image:url(Content/facebookalert/images/popup_bg.png); position:absolute; z-index:999999999; text-align:center;  display:none;">
            <div id="popup_container1" style="width:450px; height:80px;"  >
              <div id="popup_content" style="padding-top:10px; padding-left:20px; text-align:left">
                <input type="checkbox" id="chkRules" name="chkRules" onchange="EnablePublish();"/> &nbsp; I have read and agree to the <a href="http://www.facebook.com/page_guidelines.php" target="_blank">Facebook Promotion Rules</a><br/>
                
                <br/>
                <input type="button" id="btnPublish" class="disinputbutton" onclick="PublishCustomTab();"  disabled="disabled" value="Publish" id="popup_publish" />
                &nbsp;&nbsp;
                <input type="button" class="inputbutton" onclick="HidePublishAlert();" style="cursor:pointer" value="Cancel" id="popup_close" />
              </div>
            </div>
          </div>
          <div style="font-family: Tahoma, Geneva, sans-serifthaoma; font-size: 16px; color: #181818; margin:0px;	font-weight:bold;
	line-height:18px; text-align:center">&nbsp;<br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger"></asp:Label>
          </div>
          <div class="content_div" style="vertical-align:top;" >
         
         

        
         <div id="divMenu520" runat="server" style="float:left; width:650px; padding-left:85px;">
			<table align="right" width="655" border="1" cellspacing="0" cellpadding="0">
                <tr>
                  <td align="left" valign="top" style="padding:13px; padding-top:15PX; border:1px solid #46629e; background-color:#6c84b8; border-left:0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td align="center"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td width="69" valign="middle"><img src="Content/images/edit_mode_icon.png" width="57" height="57" /></td>
                            <td width="117" valign="middle" style="font-size:22px; font-weight:bold; color:#FFFFFF ">Edit Mode</td>
                            <td align="center" valign="middle" style="vertical-align:middle; line-height:15px; text-align:left; color:#FFFFFF; border-left:2px solid #bdc7d8; padding-left:10px;">To edit the text, click the Edit Text button.  To customize the background and color or add an image, click the Edit Color & Images button.  Text may look a little different on download.</td>
                          </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td height="40" align="right" valign="top"><a href="#" title="Watch Video Tutorial" onclick="$('#helptext1').fadeIn();"><img src="Content/images/watch_video_tutorial.gif" width="158" height="24" border="0" /></a></td>
                      </tr>
                      <tr>
                        <td height="50" align="center" valign="middle" style="vertical-align:middle; font-weight:bold; line-height:18px;; padding-top:10px; padding-bottom:5px; border-top:1px solid #FFF; display:none">For ideal branding, pair up with the corresponding <br>
                          # for your Sweepstakes</td>
                      </tr>
                      <tr>
                        <td style="border:1px solid #46629e; background-color:#FFF; padding-left:15px; padding-top:8px; padding-bottom:8px ">
                        
                        <div id="helptext1" style="background-image:url(content/images/facebook_frame_coaching2_video_bg.png); background-position:top; background-repeat:no-repeat; width:660px; height:474px; text-align:left;position:absolute;top:265px; left:270px; display:none;z-index:90000">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td width="51" align="left" valign="top">&nbsp;</td>
                                <td align="right" valign="bottom" style="padding-top:55px; text-align:right; padding-right:25px;"><a href="#"><img src="content/images/facebook_transparent_img.png" width="28" height="22" border="0" align="top" onclick="$('#helptext1').fadeOut();" /></a></td>
                              </tr>
                              <tr>
                                <td align="left" valign="top">&nbsp;</td>
                                <td align="left" valign="top" style="text-align:left"><span id="spnVideo" runat="server"></span> </td>
                              </tr>
                            </table>
                          </div>
                          <div id="editor-nav">
                            <div class="editor-nav-panel rc-m">
                              <div class="editor-nav-panel-content">
                              <a id="rm-content-launch"  href="#launch-content" style="width:180px;" class="nav-action-button rc-m rm-css-control-close" title="Edit text content"><img src="Content/images/edit_text_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Edit Text / Upload Video</a>
                              <a id="lnkEnaMove"  href="javascript:;" style="width:180px;" class="nav-action-button rc-m rm-css-control-close" title="click here to activate move content" onclick="return MoveContent('1');"><img src="Content/images/edit_text_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Activate Move Content</a>
                              <a id="lnkDisMove"  href="javascript:;" style="width:180px; display:none;" class="nav-action-button rc-m rm-css-control-close" title="click here to de-activate move content" onclick="return MoveContent('0');"><img src="Content/images/edit_text_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />De-Activate Move Content</a>                              
                              <a id="rm-css-launch" href="#launch-css" class="nav-action-button rc-m " 
                              style="width:180px;" title="Edit Appearance of the Presentation!">
                              <img src="Content/images/edit_color_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Edit Color & Images</a> <a id="ch_temp"  href="javascript:;" target="_parent" class="nav-action-button rc-m " style="width:180px;" title="Change Template for this Page." onclick="chkTemplatePage3('Warning! Please Save your work before leaving this page othrewise your changes will be discarded');"><img src="Content/images/change_template_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Change Template</a><a id="lnkResize"  href="javascript:;" target="_parent" class="nav-action-button rc-m " style="width:180px;" title="Crop Image" onclick="PopupCenter('crop2.aspx', 'Resize','800','700');"><img src="Content/images/crop_img_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Crop Image</a> <a  href="#launch-css" 
                              class="nav-action-button rc-m " style="width:180px;" title="Unhide Hidden Icons" onclick="$('.url').css('display','');"><img src="Content/images/unhide_icons.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Unhide Icons</a> <a href="javascript:;" onclick="FacebookAlert();" style="width:180px;" class="nav-action-button rc-m " title="Reset selected template"><img src="Content/images/reset_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Reset</a><a href="#" id="lnkReset" runat="server" style="display:none" ></a> 
                              <a href="javascript:;"  class="nav-action-button rc-m " style="width:180px;" title="Save CustomTab" id="lnkSaveName" 
                              onclick="return OpenDivCustomTabName();" ><img src="Content/images/save_icon_sidebar.png" width="15" height="15" 
                              align="absmiddle" style="margin-right:7px;"  />Save</a> <a href="javascript:;"  class="nav-action-button rc-m " 
                              style="width:180px;" title="Share Custom Tab" id="lnkShare" onclick="return OpenDivShareCustomTabName();" >
                              <img src="Content/images/share_icon.png" width="15" height="15" align="absmiddle" style="margin-right:7px;"  />Share</a><a 
                              href="javascript:;" class="nav-action-button rc-m " style="width:180px; display:none" title="Publish CustomTab" id="A1" 
                              onclick="showCustomiseUserMessage();"><img id="img2" src="Content/images/publish_custom_tab_icon.png" width="15" height="15"
                               align="absmiddle" style="margin-right:7px;" />Customize User Messages</a> <a href="javascript:;" 
                               class="nav-action-button rc-m " style="width:180px;" title="Publish CustomTab" id="lnkPublish" 
                                onclick="showPages();"><img src="Content/images/publish_custom_tab_icon.png" width="15" height="15"
                                 align="absmiddle" style="margin-right:7px;"  />Publish Sweepstakes</a>
                                <div style="padding-left:10px;"> <img id="imgLoading" src="content/images/bigspinner.gif" style="display:none" /> </div>
                                <!-- <a href="http://www.picnik.com/app" target="_blank" class="nav-action-button rc-m " title="Crop Image" >Crop Image</a> 
                      <a  href="#launch-css" class="nav-action-button rc-m " title="Close full screen" onclick="window.self.close();">Close</a>-->
                              </div>
                            </div>
                          </div></td>
                      </tr>
                    </table></td>
                </tr>
                <tr>
                  <td height="20" align="center" valign="top">
                  <img src="Content/images/sidebar_arrow_down.png" id="img1" width="23" height="12" /></td>
                </tr>
                <tr>
                <td>
                	<table border="0" cellpadding="0" cellspacing="0" width="100%">
                    	<tr>
                        	<td>
                   
<div style="float:left; background-color:#f3f3f3; border-top:#dedee1 1px solid; border-bottom:#dedee1 1px solid; width:700px;
 height:43px; position:relative;">
    <div id="lnkSaveBeforeLike" onclick="SaveLikeUnlike(1)" class="likeactive">
    	<div style="">
        <font color="#FFFFFF">1</font>
        <font>&nbsp;&nbsp;&nbsp;&nbsp;Before Visitor Likes Your Page</font>
        </div>
     </div>   
    <!--<div align="center" style=" 
     width:223px;background-image:url(Content/images/sweepstakes_step_hover.png);background-repeat:no-repeat; height:24px; 
      margin-top:8px; margin-left:10px; float:left; text-align:center;">
    	<div style="margin-left:-1px; margin-top:2px;font-family:Arial, Helvetica, sans-serif; font-size:12px;">
        <font color="#FFFFFF">1</font>
        <font color="#687faf">&nbsp;&nbsp;Before Visitor Like Your Page</font>
        </div>-->
 <div class="likedeactive" id="lnkSaveAfterLike" onclick="SaveLikeUnlike(2);">
    	<div style="" >
        <font color="#FFFFFF">2</font>
        <font>&nbsp;&nbsp;&nbsp;&nbsp;After Visitor Likes Your Page</font>
        </div>
     </div>   
  <div class="likedeactive" id="lnkSaveShareText" onclick="SaveLikeUnlike(3); showCustomiseUserMessage();">
    	<div style="">
        <font color="#FFFFFF">3</font>
        <font>&nbsp;&nbsp;&nbsp;&nbsp;Share and Email Settings</font>
        </div>
   </div>   
</div>
<input type="hidden" id="hdnSaveMenu" value="1" runat="Server" />
<div style="width:700px; height:30px; float:left; position:relative; ">
<div id="divSaveLike" style=" display:none; margin:8px;">
                     <div style="float:right; position:relative; float:left; position:relative; text-align:left; color:#687faf; font-family:Arial, Helvetica, sans-serif; font-size:16px;">
                     <a href="javascript:;" runat="server" id="lnkSavelikePrevious" style="text-decoration:none; " 
                    onclick="return SaveLikeUnlike(1);"><img src="Content/images/sweepstakes_step_previous_arrow.png" border="0" width="5" height="7" align="absmiddle" > Previous</a></div>
                    <div style="float:right; position:relative; float:right; position:relative; text-align:left; font-family:Arial, Helvetica, sans-serif; font-size:16px;">
                    <a href="javascript:;" runat="server" id="lnkSavelikeNext" 
                    onclick="return SaveLikeUnlike(3);" style="text-decoration:none;">Next <img src="Content/images/sweepstakes_step_next_arrow.png" border="0" width="5" height="7" align="absmiddle"></a>
                    </div>
</div>
                    <div id="divSaveUnLike" style="float:right; margin:8px; position:relative; 
                     text-align:left; color:#687faf; font-family:Arial, Helvetica, sans-serif; font-size:16px;">
                    <a href="javascript:;" runat="server" 
                    id="lnkSaveUnlikeNext"  style="text-decoration:none; "
                    onclick="return SaveLikeUnlike(2);">Next  <img src="Content/images/sweepstakes_step_next_arrow.png" border="0" width="5" height="7" align="absmiddle" ></a></div>
                    <div id="divSaveShare" style="float:left; margin:8px; position:relative; float:left; position:relative; text-align:left; display:none; color:#687faf; font-family:Arial, Helvetica, sans-serif; font-size:16px;"><a href="javascript:;" runat="server" 
                    id="lnkSaveShare" onclick="return SaveLikeUnlike(2);" style="text-decoration:none; "> <img src="Content/images/sweepstakes_step_previous_arrow.png" border="0" width="5" height="7" align="absmiddle" > Previous</a></div>
</div>



                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
              </table>
            </div>

			<div style="clear:both ">&nbsp;</div>
          <div style="text-align:center; padding-top:10px;"  align="center">
            <!-- Left Template start -->
			
            <div style="float:center; font-family: Tahoma, Geneva, sans-serifthaoma; font-size: 14px; 
            color: #181818; font-weight:bold; text-align:center; ">
              <asp:Label ID="txtname" runat="server"></asp:Label>
            </div>
             <div id="dvTabSubDiv" runat="server" style="padding-top:20px;">
                <div style="text-align:center;" id="presentation_html" align="center"  >
                  <style type="text/css">
        html,body{padding:0px;margin:0px;background-color: transparent;}.SafariBgFix{background-repeat:no-repeat;}
        </style>
                  <br />
                    <div id="divSweepstakeHtml" runat="server" style="text-align:center; float:left; position:relative;" align="center"></div>
                    <div id="divLikeHtml" runat="server" style="text-align:center; float:left; position:relative; display:none" align="center"></div>
                    <div id="divUnLikeHtml" runat="server" style="text-align:center;float:left; position:relative; display:none" align="center"></div>
                    
                  
                    <input type="hidden" id="hdnLike" runat="Server" />
                    <input type="hidden" id="hdnUnLike" runat="Server"/>                    
                </div>
              </div>
            <!-- Left Template end -->
			</div>
			
             <div id="CustomiseUserMessages" align="center" style="display:none; position:relative; float:left; left:42px; width:650px; height:500px; margin:0 auto;  border:0px solid #ff0000;">
			  
			  <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                <tr>
                  <td align="left" valign="top" class="greypopupborder123" style="padding-right:0px;">
                  <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center" style="text-align:center">
                      <tr bordercolor="#cccccc;" style="height:25px;">
                        <td valign="middle" style="display:none"><b>Save Customise Sweepstakes Details</b> </td>
                        <td align="right" valign="middle" style="display:none"><a href="javascript:;" onClick="CloseCustomiseUser();">[Close]</a>&nbsp;&nbsp;&nbsp; </td>
                      </tr>
                      <tr>
                        <td colspan="2" valign="top">
                        <div style="width:650px; overflow:auto; background-color:#f3f3f3; padding:15px; border:1px solid #cccccc;">
                           <table cellpadding="0" cellspacing="2" border="0" width="100%"  align="left">
                     
                            <tr>
                         <td width="168" height="50" align="right" valign="middle" style="line-height:20px; padding-right:5px;" ><asp:Label ID="lblShareText" runat="server" Text="Share Text"></asp:Label>:&nbsp;</td>
                          <td width="476" height="50" align="left" valign="middle" ><asp:TextBox ID="txtShareText" runat="server" CssClass="sweepstakeinput"></asp:TextBox></td>
                          </tr>
                               <tr>
                                 <td height="36" align="right" valign="top" style="line-height:20px; padding-right:5px;" >Share Message:&nbsp;</td>
                            <td height="25" align="left" valign="middle" ><asp:TextBox ID="txtShareMessage" runat="server" CssClass="sweepstakeinputarea"></asp:TextBox></td>
                              </tr>
                              <tr>
                                 <td height="50" align="right" valign="middle" style="line-height:20px; padding-right:5px;" >Share Image:&nbsp;</td>
                                 <td height="60" align="left" valign="middle">
                                <input type="file" runat="server" id="ShareImg" onChange="readURL(this);"  />
                                <span class="SmallText">(we only accept jpg,jpeg,bmp,gif,png files)</span><br/>
                                 <font color="#000000" style="font-family:Arial, Helvetica, sans-serif; line-height:20px; font-weight:bold;"> 
                                <asp:Label id="lblErShareImage" runat="server"></asp:Label>
                                 <a id="lnkShareImage" runat="Server" target="_blank" style="padding-top:2px;"><asp:Label id="lblShareImage" runat="server"></asp:Label></a></font>                                
                                </td>
                              </tr>
                          <%--   <tr>
                                 <td height="36" align="right" valign="middle" >Share Email:&nbsp;</td>
                               <td height="25" align="left" valign="middle" ><asp:TextBox ID="txtShareEmailadd" runat="server" CssClass="sweepstakeinput"></asp:TextBox></td>
                             </tr>--%>
                           
                               <tr>
                                 <td height="36" align="right" valign="top" style="line-height:20px; padding-right:5px;"  >Email Text:&nbsp;</td>
                                 <td height="25" align="left" valign="middle" >
                                 <textarea id="txtShareEmailText" rows="5" cols="45" class="sweepstakeinputarea" runat="server"></textarea>

                                 </td>
                             </tr>
                           
                           <tr>
                               <td height="50" align="right" valign="middle" style="line-height:20px; padding-right:5px;" >Download Sample Privacy Policy and Rules Word Doc  </td> 
                          <td height="60" align="left" valign="middle">   &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:;" id="lnkdownloadrulepdf" onclick="DownloadPDF();" class="bluetablink" runat="server"> Download Now </a> </td>
                        
                           </tr>
                            <tr>
                                 <td height="50" align="right" valign="middle" style="line-height:20px; padding-right:5px;" >Upload Privacy Policy and <br/>
                              Official Rules PDF: &nbsp;</td>
                            <td height="60" align="left" valign="middle">
                          <input type="file" runat="server" id="PDffile" onChange="readURL(this);" /><span class="SmallText">&nbsp;(we only accept .pdf files)</span>&nbsp;<br/>
                                <font color="#000000" style="font-family:Arial, Helvetica, sans-serif; line-height:20px; font-weight:bold;"> <asp:Label id="lblErSharePDF" runat="server"></asp:Label>
                                 <a id="lnkSharePdf" runat="Server" target="_blank"><asp:Label id="lblSharePDF" runat="server"></asp:Label></a>  </font>                               

                             </td>
                    </tr>
                  

                            <input type="hidden" id="Hidden1" runat="server" name="hdnselectedPages" value="" />
                            <input type="hidden" id="Hidden2" runat="server" name="hdnSelectedPagesName" value="" />
                            <input type="hidden" id="Hidden3" runat="server" name="hdnSelectedPagesImage" value="" />
                            <input type="hidden" id="Hidden4" runat="server" name="hdnselectedPagesAccessToken" value="" />
 
                    </table>
                          </div></td>
                      </tr>
                      <tr>
                        <td height="20" align="left" valign="middle" style="padding-top:10px; display:none;">
                        <a href="javascript:;" id="csswdetail" class="bluetablink"  onclick="ValidateCustomiseUserMessage();">Save Sweepstakes Details</a>&nbsp;
                        <a href="javascript:;" id="csswdetails" class="bluetablink" runat="server" style="display:none;" >Save Sweepstakes Details</a>&nbsp;
                        </td>
                      </tr>
                    </table></td>
                </tr>
              </table>
			  </div>

 <div id="Fanpagesdiv1" class="Fanpagesdiv1" style="display:none; width:670px; height:250px; border:0px solid #ff0000; position:absolute; z-index: 1000;">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
    	 	<tr>
                  <td align="center" valign="top" class="greypopupbordertop">
				  <img src="../Content/images/grey_popup_top.gif" width="31" height="11" />
				  </td>
         	</tr>
          <tr>
                  <td align="left" valign="top" class="greypopupborder" style="padding:13px; padding-bottom:16px; padding-right:0px;">
				  <table cellpadding="0" cellspacing="0" border="0" width="100%">
				  <tr bordercolor="#cccccc;" style="height:25px;">
				  <td valign="middle">
				  <b>To add a Sweepstakes tab, select a Business Page(s)</b>
				  </td>
				  <td align="right" valign="middle">
					 <a href="javascript:;" onclick="ClosePage();">[Close]</a>&nbsp;&nbsp;&nbsp;
				  </td>
				  </tr>
                  <tr style="height:30px; display:none">
                  <td >
                  <b>After Like HTML &nbsp;  
                  <input class="checkboxpadding" type="radio" id="rdLike" name="chkPage" runat="server"  group="rdlikes" checked />    &nbsp;&nbsp;&nbsp;Before Like HTML&nbsp;
                  <input class="checkboxpadding" type="radio" id="rdUnLike" name="chkPage" runat="server"  group="rdlikes" /></td>
                 </b>
                 </tr>
 				<tr>
				  <td colspan="2" valign="top">
   			 <div style="width:650px; height:250px; overflow:auto; background-color:#ffffff;padding:0px; padding-top:0px; border:1px solid #cccccc;">
             <input type="hidden" id="hdnselectedPages" runat="server" name="hdnselectedPages"
                                                                    value="" />
                  <input type="hidden" id="hdnSelectedPagesName" runat="server" name="hdnSelectedPagesName"
                                                                    value="" />
                  <input type="hidden" id="hdnSelectedPagesImage" runat="server" name="hdnSelectedPagesImage"
                                                                    value="" />
                  <input type="hidden" id="hdnselectedPagesAccessToken" runat="server" name="hdnselectedPagesAccessToken"
                                                                    value="" />
       <asp:PlaceHolder ID="plcData" runat="server">
					<asp:DataList ID="dstFanPages" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                  <ItemTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td colspan="2" style="border-top:0px solid #cccccc;height:20px;"></td>
                                      </tr>
                                      <tr>
                                        <td width="150" valign="top" align="center"><img src='<%#Eval("picture")%>' height="102" width="102" class='imgborder' group="pageimg" pageid='<%#Eval("Id")%>' /></td>
                                        <td width="10" >&nbsp;</td>
                                      </tr>
                                      <tr height="30" align="center">
                                        <td><input class="checkboxpadding" type="radio" id="chkPage" name="chkPage" runat="server" pageid='<%#Eval("Id")%>' group="pages" onclick='Pageid(this);'  pageaccess_token='<%#Eval("access_token")%>' pagevalue='<%#Eval("name")%>' pageimage='<%#Eval("picture")%>' />
                                          <%#Eval("name")%>
                                          <input type="hidden" id="hdnPageId" runat="server" value='<%#Eval("Id")%>' />
                                          <input type="hidden" id="hdnPageName" runat="server" value='<%#Eval("name")%>' />
                                          <input type="hidden" id="hdnAccessToken" runat="server" value='<%#Eval("access_token")%>' />
                                          <input type="hidden" id="hdnImage" runat="server" value='<%#Eval("picture")%>' />
                                        </td>
                                        <td >&nbsp;</td>
                                      </tr>
                                    </table>
                                  </ItemTemplate>
                                </asp:DataList>
           </asp:PlaceHolder>
           <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
				   <strong style="color:#990066">  You have no business pages.</strong><br /><br /> <a href="javascript:CreatePage();">Click here</a> to create business page. 
		   </asp:PlaceHolder> 
           <asp:PlaceHolder ID="plcError" runat="server" Visible="false">
                                           <strong style="color:#990066">Facebook is experiencing problems. Please try again later</strong><br /><br />
           </asp:PlaceHolder>
     </div>
    
    </td>
  </tr>
  <tr>
<td height="20" align="left" valign="middle" style="padding-top:10px;"><a id="btnSave" class="bluetablink" runat="server" onclick="return ValidatePublish();" title="Publish Custom Tab">Publish Sweepstakes</a>&nbsp;<a id="lnkDownload" class="bluetablink" runat="server" title="Download Sidebar" style="display:none;">Download Sidebar</a> <a id="btnUpload" runat="server" style="display:none">Publish Custom Tab</a> </td>
</tr>
  </table>
  </td>
  </tr>
</table>

    </div>
    
	           
              


         <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>
            
            <div style="display:none;  padding:15px; padding-top:1100px;  position:absolute; z-index:10000;" id="divLoading">
              <div style="border:2px solid #000000; width:350px; height:80px; background-color:#FFFFFF;">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                  <tr>
                    <td style="font-size:16px;"><img src="content/images/demo_wait.gif" align="absmiddle"  />&nbsp;Adding Sweepstakes to your page... </td>
                  </tr>
                </table>
              </div>
            </div>
            <div style="display:none;  padding:15px; padding-top:1100px;  position:absolute; z-index:10000;" id="dvMessage" >
              <div style="border:2px solid #000000; width:350px; height:80px; background-color:#FFFFFF;" >
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                  <tr>
                    <td><strong>Done</strong> </td>
                    <td align="right" style="height:15px;"><a href="javascript:;" onclick="hideDivPopup('dvMessage');">[Close]</a>&nbsp;&nbsp;&nbsp; </td>
                  </tr>
                  <tr>
                    <td colspan="2" valign="top" style="border-top:1px solid #cccccc;"><br />
                      <span id="spnMsg" style="font-size:16px"> Sweepstakes has been successfully added to your business page.<br />
                      </span> </td>
                  </tr>
                </table>
              </div>
            </div>
          </div>
          </contenttemplate>
           <Triggers>
            <asp:PostBackTrigger ControlID="lnkSave" />  
            <asp:PostBackTrigger ControlID="btnSave" />  
            <asp:PostBackTrigger ControlID="lnkShareUserEmail" />                          
           </Triggers>
</asp:updatepanel>

          </td>
      </tr>
    </table>
  </div>
</div>
<input type="hidden" id="hdnType" runat="server" />
</form>
<!--End new html-->
<!-- Below are widget codes are written  -->
<div id="fbox"></div>
<input type="text" name="fix" id="fix" style="display:none" />
<div style="display:none; opacity: 1; left: 600px; top: 244px; width:420px; height: 258px;clear:both;" id="rm-appearance-panel">
  <div style="background-image:url(content/sidebar-images/widget_box_bg.png); width:392px; height:187px; position:relative; padding:0px; padding-top:9px; "  id="rm-appearance-type" class="rm-css-subpanel" >
    <div style="position:absolute; left:16px; top:-17px;"><img src="content/sidebar-images/widget_top_arrow.png" width="32" height="26" /></div>
    <div style="width:366px; height:20px; background-color:#43bae2; margin-left:9px; font-family:arial; font-size:14px; font-weight:bold; color:#FFF; padding-left:8px; padding-top:5px; text-transform:uppercase; position:relative">TEXT options</div>
    <div style="background-color:#FFF; width:346px; height:125px; position:relative; margin-left:9px; padding:14px;" >
      <div style="height:38px;">
        <div style="display: a;" class="rm-css-controlrow fgtype">
          <style>
div.selector {
    background: none repeat scroll 0 0 #F9F9F9;
    border: 1px solid #CDCDCD;
    border-radius: 5px 5px 5px 5px;
    float: left;
    height: 25px;
    overflow: hidden;
    padding: 0 70px 0 5px;
    position: relative;
}
</style>
          <div style="float:left;font-size:18px;padding:0 70px 0 5px;;width:140px;height:25px;" id="uniform-rm-css-font-family" >
            <select style="opacity: 0;width:140px;cursor:pointer;font-size:16px;" id="rm-css-font-family" class="rm-css-control" name="family">
              <option value="sans-serif-one" style="font:Arial, Helvetica, sans-serif;"><span style="font:Arial, Helvetica, sans-serif;">Helvetica/Arial</span></option>
              <option value="sans-serif-two" style="font:Verdana, Geneva, sans-serif;"><span style="font:Verdana, Geneva, sans-serif;">Verdana</span></option>
              <option value="block-sans" style="font:'Trebuchet MS', Arial, Helvetica, sans-serif;"><span style="font:'Trebuchet MS', Arial, Helvetica, sans-serif;">Impact</span></option>
              <option value="monospace" style="font:'Courier New', Courier, monospace;"><span style="font:'Courier New', Courier, monospace;">CourierNew</span></option>
              <option value="serif-two" style="font:'Times New Roman', Times, serif;"><span style="font:'Times New Roman', Times, serif;">TimesNewRoman</span></option>
              <option value="serif-three" style="font:'Palatino Linotype', 'Book Antiqua', Palatino, serif;"><span style="font:'Palatino Linotype', 'Book Antiqua', Palatino, serif;">Palatino</span></option>
            </select>
          </div>
          <div style="float:right;margin-left:45px;"> <a href="#"  id="rm-css-type-bold">a</a><a href="#" id="rm-css-type-italic">b</a> </div>
        </div>
      </div>
      <div style="height:38px;">
        <div style="font-family:arial; font-size:25px; color:#666666; width:28px; float:left">tT</div>
        <div style="float:left; width:55px;"> <span id="rm-css-type-size-indicator" style="border:1px solid #adadad; background-color:#ddf1fc; padding-left:10px; width:45px; height:22px; font-family:arial; font-size:16px; color:#666666;font-size:20px;padding-right:10px;">12</span> </div>
        <div style="width:100px; float:left; margin-left:15px;"><a href="#" id="rm-css-type-increasesize"><img src="content/sidebar-images/widget_icon_plus.png" width="30" height="26" border="0" /></a>&nbsp;<a href="#" id="rm-css-type-decreasesize"><img src="content/sidebar-images/widget_icon_minus.png" width="30" height="26" hspace="10" border="0" /></a></div>
        <div style="float:left; position:absolute; top:50px; left:230px;"><img src="content/sidebar-images/widget_color.png" width="42" height="37" border="0" /></div>
        <div style="float:left; position:relative; width:68px; left:75px;">
          <div id="rm-css-color-foreground" class="rm-color-select" style="width:70px;height:30px;"></div>
        </div>
      </div>
      <div>
        <div style="float:left; font-family:arial; font-size:16px; color:#666666; width:98px; padding-top:5px;">POSITION</div>
        <div><a href="#" id="rm-css-type-alignleft"><img src="content/sidebar-images/widget_left_align.png" width="30" height="26" border="0" /></a>&nbsp;<a href="#" id="rm-css-type-aligncenter"><img src="content/sidebar-images/widget_center_align.png" width="30" height="26" hspace="9" border="0" /></a>&nbsp;<a href="#" id="rm-css-type-alignright"><img src="content/sidebar-images/widget_right_align.png" width="30" height="26" border="0" /></a></div>
      </div>
      <div style="border-bottom:1px solid #dddddd; height:7px; width:100%;"></div>
      <div style="float:right; width:66px; margin-top:7px;"><a href="#" title="Close Visual Editor">
       <!-- <img src="content/facebox/closelabel1.gif" border="0" />-->
        </a></div>
    </div>
  </div>
  <div style="background-image:url(content/sidebar-images/widget_box_bg2.png); background-repeat:no-repeat; width:392px; height:422px; position:relative;padding:0px; padding-top:9px;"  id="rm-appearance-background" class="rm-css-subpanel">
    <div style="position:absolute; left:16px; top:-16px;"><img src="content/sidebar-images/widget_top_arrow.png" width="32" height="26" /></div>
    <div style="width:366px; height:20px; background-color:#43bae2; margin-left:9px; font-family:arial; font-size:14px; font-weight:bold; color:#FFF; padding-left:8px; padding-top:5px; text-transform:uppercase; position:relative">Image options</div>
    <div style="background-color:#FFF; width:346px; height:174px; position:relative; margin-left:9px; padding:14px;">
      <div style="height:40px;" >
        <div style="width:100px; font-family:arial; font-size:16px; color:#666666; float:left; padding-top:4px;">IMAGE</div>
        <div style="float:left">
		
		
          <div id="rm-css-bgimage-control" class="rm-css-bgimage-control" style="z-index:1000">
            <form class="rm-widget disabled" id="bgimage-upload" method="post" action="#">
              <input style="display:;" id="rm-css-bgimage-uploader" name="rm-image" width="119" type="file" height="18">
              <div style="position:absolute; top:0px; z-index:1000000" >
                 <object style="visibility: visible;" id="rm-css-bgimage-uploaderUploader" data="/uploadify.swf" type="application/x-shockwave-flash" width="120" height="18">
                          <param value="high" name="quality">
                          <param value="transparent" name="wmode">
                          <param value="always" name="allowScriptAccess">
                          <param value="uploadifyID=rm-css-bgimage-uploader&amp;buttonText=Upload%20new&amp;script=/upload.ashx&amp;folder=<%=strFBUserId%>-tabs&amp;width=120&amp;height=120&amp;wmode=transparent&amp;method=POST&amp;queueSizeLimit=200999&amp;simUploadLimit=1&amp;hideButton=true&amp;fileDesc=jpg, jpeg, png, gif&amp;fileExt=*.jpg;*.jpeg;*.gif;*.png&amp;auto=true&amp;sizeLimit=10000000&amp;fileDataName=Filedata&amp;" name="flashvars">
                        </object>
              </div>
              <div id="rm-css-bgimage-uploaderQueue" class="uploadifyQueue" style=" display:;" ></div>
            <div class="progress-bar" id="progress-bar" align="left"  style="text-align:center; vertical-align:top; position:absolute; z-index:0; top:0px;  width:200px; left:0px; height:21px; padding-top:3px; border:0px solid #FF0000; display:;">Add </div>
            <a style="display: none; z-index:5;" href="#remove-bg-image" id="rm-css-bgimage-remove" class="" title="remove this image?">Remove</a>
            <input type="hidden" runat="server" id="hdnImageAreaW" value="100" />
            <input type="hidden" runat="server" id="hdnImageAreaH" value="100" />
            <input type="hidden" id="hdnResizeCheck" value="1" />
            <input type="hidden" id="hdnMoveContent" value="0" />            
            </form>
          </div>
		  
		  
		  
        </div>
        <div style="float:right; padding-top:2px; "><span style="font:Arial, Helvetica, sans-serif;font-size:10px;float:right;font-weight:bold;">Width &nbsp;: <span id="bgwidth" style="color:#060"></span><br>
        Height : <span id="bgheight" style="color:#060"></span></span></div> <br/>
		<div style="clear:both; height:1px; ">&nbsp;</div>
        <div style="font-size:16px;  font-family:arial; color:#666666; float:left; padding-top:4px; padding-left:0px;">Auto re-size image</div>
        <div style="float:left; padding-top:4px; padding-left:10px;"> <img src="content/images/c.png" style="cursor:pointer;" id="imgResizeCb" onclick="CheckThis(this);" /> </div>
      </div>
      <div style="height:41px;">
        <div style="float:left; position:absolute; top:70px; left:18px;"><a href="#"><img src="content/sidebar-images/widget_color.png" width="42" height="37" border="0" /></a></div>
        <div style="font-family:arial; font-size:16px; color:#666666; float:left; position:relative; margin-left:55px;  padding-top:10px; *padding-top:20px;">BACKGROUND COLOR</div>
        <div style="width:87px; float:right">
          <div style="background-color: transparent;width:70px; height:30px;" id="rm-css-color-background" class="rm-color-select"></div>
        </div>
      </div>
      <div style="height:37px;">
        <div style="font-size:16px; font-family:arial; color:#666666; width:84px; float:left; padding-top:18px;">POSITION</div>
        <div style="float:left; padding-top:10px;"><a href="#" id="rm-css-bgposition-left"><img src="content/sidebar-images/widget_position_left.png" width="31" height="26" border="0" /></a>&nbsp;<a href="#" id="rm-css-bgposition-top"><img src="content/sidebar-images/widget_position_top.png" width="30" height="26" hspace="4" border="0" /></a>&nbsp;<a href="#" id="rm-css-bgposition-bottom"><img src="content/sidebar-images/widget_position_bottom.png" width="30" height="26" border="0" /></a>&nbsp;<a href="#" id="rm-css-bgposition-right"><img src="content/sidebar-images/widget_position_right.png" width="30" height="26" hspace="4" border="0" /></a>&nbsp;
          <!--<a href="#" id="rm-css-bgposition-free" style="top:90px;left:233px;"><img src="content/sidebar-images/widget_position_move.png" width="30" height="26" hspace="10" border="0" /></a>-->
         <a  href="#" style="margin-left:92px; *margin-left:220px;  *margin-right:-270px;" onclick="$('#helptext').fadeIn();"><img hspace="10" height="26" border="0"  src="content/sidebar-images/questionmark-48-Icon.png" ></a>

          <div style="clear:both; position:absolute; z-index:10000000000; display:none;" id="helptext">
	       <div style="background-image:url(content/sidebar-images/widget_box_bg4.png); background-repeat:no-repeat; width:194px; height:239px; padding-top:10px;">
              <div style="background-image:url(content/sidebar-images/widget_box_text_bg.png); width:173px; height:228px; margin-left:10px;">
                <div style="padding:10px;">
                  <div style="font-family:arial; font-size:11px; line-height:14px;">Use the position settings to align your images to left, right, top, or bottom. OR use the move tool to freely position your image by dragging it around.<br />
                    <br />
                    You can repeat your image horizontally, vertically or click none for no repeat.<br />
                    <br />
                    To remove your image click the minus buttom at the top left.</div>
                  <div style="border-bottom:1px solid #dddddd; height:7px;"></div>
                
                </div>
                
              </div>
                <div style="float:right; width:66px; margin-top:5px;"><a href="#" onclick="$('#helptext').fadeOut();"><img src="content/sidebar-images/widget_icon_close.png" width="66" height="17" border="0" /></a></div>
            </div>
			</div>
        </div>
      </div>
      <div style="height:32px;">
        <div style="font-size:16px; font-family:arial; color:#666666; width:84px; float:left; padding-top:10px;">REPEAT</div>
        <div style="float:left; padding-top:5px;"><a href="#" id="rm-css-bgrepeat-x"><img src="content/sidebar-images/widget_repeat_x.png" width="30" height="26" /></a>&nbsp;<a href="#" id="rm-css-bgrepeat-y" ><img src="content/sidebar-images/widget_repeat_y.png" width="30" height="26" hspace="4" /></a>&nbsp;<a href="#" id="rm-css-bg-transparent" style="display:none"><img src="content/sidebar-images/widget_repeat_no.png" width="30" height="26" /></a> </div>
      </div>
      <div style="border-bottom:1px solid #dddddd; height:7px;"></div>
		
</div>
    </div>
<div style="float:right; width:130px; position:absolute; border:0px solid #FF0000; top:230px; left:245px;"  id="divSaveAndClose">
  <a style="display: inline-block;position:absolute; margin-left:-220px; margin-top:5px;font-family:'Times New Roman', Times, serif;font-size:14px;color:grey;" href="#cancel" id="rm-css-control-cancel" title="Undo latest changes for this element?"><strong>Undo Changes</strong></a>
   <a href="#" id="rm-css-control-close"  title="Close Visual Editor"><img src="content/facebox/closelabel1.gif" border="0" /></a>
</div>
</div>
<div id="hide"></div>
<div class="main_box_hover2" style="position:absolute; margin:250px 0 0 50px; z-index:1; display:none" id="warning_popup3" >
  <div class="main_box_hover_top2">
    <div class="main_box_hover_top_left"></div>
    <div class="main_box_hover_top_mid2"></div>
    <div class="main_box_hover_top_right"></div>
  </div>
  <div class="main_box_hover_mid2">
    <div class="main_box_hover_mid_left_shadow2">
      <div class="main_box_hover_mid_right_shadow2">
        <div class="main_box_hover_mid_inn2">
          <div class="main_box_hover_mid_inn_hd3" style="font-size:13px; font-family:Verdana, Geneva, sans-serif; width:416px padding-left:8px;">Warning! Please save your work before leaving this page.</div>
          <div class="apply_tut_but" style="margin-left:20px;"> <a href="#_" onclick="hide('warning_popup3');window.parent.showsidebartemplate();">Already Saved</a> </div>
          <div class="apply_tut_but" style="margin-left:0;"> <a href="#_" onclick="hide('warning_popup3');return false;">Close & Click Save</a> </div>
        </div>
      </div>
    </div>
  </div>
  <div class="main_box_hover_top2">
    <div class="main_box_hover_bot_left"></div>
    <div class="main_box_hover_bot_mid2"></div>
    <div class="main_box_hover_bot_right"></div>
  </div>
</div>
<script type="text/javascript">
    edit = 0;


    function falert(msg) {
        jQuery.facebox('<b>' + msg + '</b>');
    }


    $("div[rel^=rm-css-bg]").click(function (e) {
        var width = $(this).width();
        var height = $(this).height();
        $('#hdnImageAreaW').val($(this).width());
        $('#hdnImageAreaH').val($(this).height());
        $("#bgwidth").html(width + 'px');
        $("#bgheight").html(height + 'px');
    });


    $("div[rel^=rrm-css-bg]").click(function (e) {
        var width = $(this).width();
        var height = $(this).height();
        $('#hdnImageAreaW').val($(this).width() + 20);
        $('#hdnImageAreaH').val($(this).height());
        $("#bgwidth").html(width + 'px');
        $("#bgheight").html(height + 'px');
    });
    /*
    $(".dotted").mouseenter(function(){
    if(edit){
    width = $(this).width();
    height = $(this).height();

    $('.highlight-window').fadeOut('fast',function(){$('.highlight-window').remove('.highlight-window');});
    $(this).append('<div class="highlight-window" style="position: absolute; border: 1px dotted rgb(0, 0, 0); outline: 1px dotted rgb(255, 255, 255); padding: 5px; cursor: pointer; top: -5px; left: -4px; width: '+width+'px; height: '+height+'px; z-index: 1100; display: none;"></div');
    $('.highlight-window').fadeIn('fast');
    return false;
    }
    });

    $(".dotted").mouseleave(function(){
    if(edit){
    $('.highlight-window').fadeOut('fast',function(){$('.highlight-window').remove('.highlight-window');});
    }
    });

    $(".dotted").click(function(){
    if(edit){
    $('.highlight-window').fadeOut('fast',function(){$('.highlight-window').remove('.highlight-window');});
    }
    });
    */

    $.ajaxSetup({
        url: "/xmlhttp/",
        global: false,
        type: "POST"
    });


    $("#rm-css-launch").click(function () {
        edit = 0;
        $("span[rel^=rm-css-text]").css("cursor", "default").css("border", "none");
        $("div[rel^=video]").css("cursor", "default");
        $(".url").css("cursor", "default");
        $(".pointer").css("cursor", "default");
        $(".urlandtext").css("cursor", "default");
    });

    $("#rm-content-launch").click(function () {
        edit = 1;
        $("span[rel^=rm-css-text]").css("cursor", "pointer").css("border", "thin solid").css("border-style", "dotted");
        $("div[rel^=video]").css("cursor", "pointer");
        $(".url").css("cursor", "pointer");
        $(".pointer").css("cursor", "pointer");
        $(".urlandtext").css("cursor", "pointer");
        $('.rm-css-edit fg fgtype').draggable({
            containment: '#content_bg',
            cursor: 'move',
            snap: '#content'
        });

    });

    $("div[rel^=video]").click(function (e) {

        id = $(this).attr('rel');
        size = $(this).attr('class');

        if (size == 'big') {
            func = 'videobig';
        }
        else if (size == 'big2') {
            func = 'videobig2';
        }
        else if (size == 'small') {
            func = 'videosmall';
        }
        else {
            func = 'videobig';
        }

        form = '<div style="font-size:16px"><b>Enter YouTube URL : </b><br><br> <input type="text" id="videourl" value=""  size="55" style="font-size:16px"/> <div style="float:left; padding-top:10px; font-size:10px; font-family:Arial, Helvetica, sans-serif;"><b>Note:</b>  Please note that using http instead of https will result in a security warning. If available, use https instead.</div><br/><br/><br/><div style=" border-top: 1px solid #DDDDDD; padding-top: 10px; margin-top: 10px; text-align: right; padding-bottom:5px;"> <a href="javascript:;" class="bluetablink" onClick="' + func + '(document.getElementById(\'videourl\').value,\'' + id + '\');fboxhide();">Save and Close</a>&nbsp; <a href="javascript:;" class="bluetablink" onClick="' + func + 'reset(\'' + id + '\');">Remove</a></div></div>';

        if (edit) {
            fbook_box_1(e, form);
            $('#facebox').fadeIn('slow');
        }

    });

    //$(".hide").css("display","block");
    $('<style type="text/css">.hide{ display:block }</style>').appendTo('#hide');

    $(".pointer").css("cursor", "default");
    $(".url").css("cursor", "default");


    $(".url").click(function (e) {

        url = $(this).attr('rel');
        if (url == 'http://') {
            url = '';
        }
        if (url != '') {
            seturl(url, index);
            setLocation(url);
        }
        func = 'seturl';
        form = '<div style="font-size:16px"><b>Enter Website URL : </b><br><br> <input type="text" id="url" value="' + url + '"  size="38" style="font-size:16px"/><div style="padding-top:5px;"> Hide: <input type="checkbox" onclick="seturlimagehide(\'' + $(".url").index(this) + '\',this.checked);" /></div><div style="float:left; padding-top:10px; font-size:10px; font-family:Arial, Helvetica, sans-serif;"><b>Note:</b>  Please note that using http instead of https will result in a security warning. If available, use https instead.</div><br/><br/><div style=" border-top: 1px solid #DDDDDD; padding-top: 10px; margin-top: 10px; text-align: right; padding-bottom:5px;"> <a href="javascript:;" class="bluetablink" onClick="' + func + '(document.getElementById(\'url\').value,\'' + $(".url").index(this) + '\');fboxhide();">Save and Close </a></div></div>';

        if (edit) {
            fbook_box_1(e, form);
            $('#facebox').fadeIn('slow');
        }
    });


    $(".urlwithouthide").click(function (e) {

        url = $(this).attr('rel');
        index = $(".urlwithouthide").index(this);
        if (url == 'http://') {
            url = '';
        }
        if (url != '') {
            seturlwithouthide(url, index);
            setLocation(url);
        }
        func = 'seturlwithouthide';
        form = '<div style="font-size:16px"><b>Enter Website URL : </b><br><br> <input type="text" id="urlwithouthide" value="' + url + '"  size="38" style="font-size:16px"/><div style="padding-top:5px;"></div><div style="float:left; padding-top:10px; font-size:10px; font-family:Arial, Helvetica, sans-serif;"><b>Note:</b>  Please note that using http instead of https will result in a security warning. If available, use https instead.</div><br/><br/><div style=" border-top: 1px solid #DDDDDD; padding-top: 10px; margin-top: 10px; text-align: right; padding-bottom:5px;"> <a href="javascript:;" class="bluetablink" onClick="' + func + '(document.getElementById(\'urlwithouthide\').value,\'' + $(".urlwithouthide").index(this) + '\');fboxhide();">Save and Close </a></div></div>';

        if (edit) {
            fbook_box_1(e, form);
            $('#facebox').fadeIn('slow');
        }
    });


    $(".share").click(function (e) {

        msg = $(this).attr('rel');
        func = 'setshare';
        form = '<div style="font-size:16px"><b>Enter Feed message : </b><br><br> <input type="text" id="sharemsg" value="' + msg + '"  size="38" style="font-size:16px"/>  <input type="button" value="Update" onClick="' + func + '(document.getElementById(\'sharemsg\').value,\'' + $(".share").index(this) + '\');fboxhide();" /></div>';

        if (edit) {
            fbook_box(e, form);
            $('#facebox').fadeIn('slow');
        }
    });

    $(".urlandtext").click(function (e) {

        id = $(this).attr('id');
        url = $(this).attr('rel');
        if (url == 'http://') {
            url = '';
        }
        func = 'seturl2';
        form = '<div style="font-size:16px"><b>Edit Text : </b><br><br> <input type="text" onkeyup="$(\'#' + id + '\').html(nl2br(this.value))" value="' + $(this).html() + '"  size="38" style="font-size:16px"/><br><br> </div><div style="font-size:16px"><b>Enter Website URL : </b><br><br> <input type="text" id="url" value="' + url + '"  size="38" style="font-size:16px"/><div style="float:left; padding-top:10px; font-size:10px; font-family:Arial, Helvetica, sans-serif;"><b>Note:</b>  Please note that using http instead of https will result in a security warning. If available, use https instead.</div><br/><br/><br/><div style=" border-top: 1px solid #DDDDDD; padding-top: 5px; margin-top: 10px; text-align: right; padding-bottom:5px;"><div style="padding-top:5px;"><a href="javascript:;" class="bluetablink" onClick="' + func + '(document.getElementById(\'url\').value,\'' + $(".urlandtext").index(this) + '\');fboxhide();" >Save and Close</a></div></div></div>';

        if (edit) {
            fbook_box_1(e, form);
            $('#facebox').fadeIn('slow');
        }

    });


    function fbook_box_1(e, html) {
        //e.pageX
        $('#fbox').html('<div style="top: ' + (e.pageY - 30) + 'px; left: ' + 280 + 'px; display: none;z-index:2000000000" id="facebox"><div class="popup"><table><tr><td class="tl"></td><td class="b"></td><td class="tr"></td></tr><tr><td class="b"></td><td class="body"><div style="float: none; display: block;" class="content">' + html + '</div><div class="footer" style="display: none; width:100%;"><a class="close" href="#_" onclick="fboxhide()"><img class="close_image" title="close" src="content/facebox/closelabel1.gif"></a></div></td><td class="b"></td></tr><tr><td class="bl"></td><td class="b"></td><td class="br"></td></tr></table></div></div><div class="facebox_hide facebox_overlayBG" onclick="fboxhide()" id="facebox_overlay" style="display: block; opacity: 0;filter:alpha(opacity=0);"></div>');
    }

    $("#contact").click(function (e) {

        id = $(this).attr('id');
        //email  = $(this).attr('rel');
        func = 'setcontact';
        form = '<div style="font-size:16px"><b>Edit Text : </b><br><br> <input type="text" id="url" onkeyup="$(\'#' + id + '\').html(this.value)" value="' + $(this).html() + '"  size="38" style="font-size:16px"/><br><br> </div><!--<div style="font-size:16px"><b>Enter Email Adress : </b><br><br> <input type="text" id="email" value="' + email + '"  size="38" style="font-size:16px"/>  <input type="button" value="Update" onClick="' + func + '(document.getElementById(\'email\').value);fboxhide();" /> </div>-->';

        if (edit) {
            fbook_box(e, form);
            $('#facebox').fadeIn('slow');
        }

    });


    $(".contact").click(function (e) {

        func = 'setcontact';
        form = '<div style="font-size:16px"><b>Edit Text : </b><br><br> <input type="text" id="url" onkeyup="$(\'.contact\').html(this.value)" value="' + $(this).html() + '"  size="38" style="font-size:16px"/><br><br> </div>';

        if (edit) {
            fbook_box(e, form);
            $('#facebox').fadeIn('slow');
        }

    });

    function setcontact(email) {
        $("#contact").attr('rel', email);
        $("#contact").attr('onClick', 'setLocation(\'mailto:' + email + '\');');
    }
    function seturlimagehide(index, bol) {
        if (bol) {
            $(".url:eq(" + index + ")").css('display', 'none');
        }
        else {
            $(".url:eq(" + index + ")").css('display', '');
        }
    }
    function setshare(msg, index) {
        $(".share:eq(" + index + ")").attr('rel', msg);
        //$(".share:eq("+index+")").removeAttr('onclick');
        $(".share:eq(" + index + ")").attr('onClick', 'share(\'' + msg + '\');');
        //alert (url+' : '+index);
    }
    function seturl(url, index) {

        //$(".url:eq("+index+")").removeAttr('onclick');
        var str = 'setLocation(\'' + url + '\');'
        $(".url:eq(" + index + ")").attr('rel', url);
        $(".url:eq(" + index + ")").attr('onClick', str);
        // alert('.url:eq(" + index + ")").attr('onclick', 'setLocation(\'' + url + '\');"');
        //alert (url+' : '+index);

    }

    function seturlwithouthide(url, index) {

        //$(".url:eq("+index+")").removeAttr('onclick');
        var str = 'setLocation(\'' + url + '\');'
        $(".urlwithouthide:eq(" + index + ")").attr('rel', url);
        $(".urlwithouthide:eq(" + index + ")").attr('onClick', str);
        // $(".url:eq(" + index + ")").attr('onClick', str);
        //		$(".url:eq(" + index + ")").attr('rel', url);

        // alert('.url:eq(" + index + ")").attr('onclick', 'setLocation(\'' + url + '\');"');
        //alert (url+' : '+index);

    }
    function seturl2(url, index) {
        var str = 'setLocation(\'' + url + '\');'
        $(".urlandtext:eq(" + index + ")").attr('rel', url);
        $(".urlandtext:eq(" + index + ")").attr('onClick', str);
        //$(".urlandtext:eq(" + index + ")").attr('onClick', 'setLocation(\'' + url + '\');');
        //alert (url+' : '+index);
    }

    function videobig(url, id) {
        //alert('big url : '+ url + ' id : '+id);
        var vid;
        var results;

        results = url.match("[\\?&]v=([^&#]*)");
        var res;

        res = (results === null) ? url : results[1];
        vid = res.replace("http://youtu.be/", "");
        var urlnew;
        urlnew = "https://www.youtube.com/watch?v=" + vid;
        //alert(id);
        //alert(vid);
        var width = $('#videoMain').width();
        var height = $('#videoMain').height();
        var heighthalf = (height / 2) - 15;
        var widthhalf = (width / 2) - 15;
        var photo = 'https://img.youtube.com/vi/' + vid + '/0.jpg';
        //$("div[rel=" + id + "] img:first").attr("src", photo); 
        //$("div[rel=" + id + "] a:first").attr("href", url); 
        //$("div[rel=" + id + "] a:first").attr("class", "video");
        // $('#' + id).css('background-image', 'url(' + photo + ')');
        $('#' + id).html('<a href="javascript:;" rel="' + urlnew + '&fs=1&rel=0&enablejsapi=1&playerapiid=ytplayer;" class="video"><img src="' + photo + '" height="' + height + '" width="' + width + '" style="z-index=-100;"><div style="z-index: 10; left: ' + widthhalf + 'px; position: absolute; top: ' + heighthalf + 'px"><img src="https://so.tsmaapplication.com/content/images/play_icon.png" style="z-index:10; "></div> </a>');
        // $.ajax({
        //            url: 'ajax.php',
        //            data: ({ utubevid: vid, ajax: 1, utube: 1, page_id: 0 }),
        //            success: function (data) {
        //                $('#' + id).html(data);
        //                $('#spinner').fadeOut('slow', function () { $('#' + id).css('background-image', "url(http://mysocialmediaagency.com/tsms_beta/playimgbig.php?img=http://mysocialmediaagency.com/tsms_beta/thumb.php?x=370%26y=229%26src=https://img.youtube.com/vi/" + vid + "/0.jpg)"); });
        //            }
        //        });

    }

    function videobig2(url, id) {
        //alert('big url : '+ url + ' id : '+id);
        var vid;
        var results;

        results = url.match("[\\?&]v=([^&#]*)");
        var res;

        res = (results === null) ? url : results[1];
        vid = res.replace("http://youtu.be/", "");
        var urlnew;
        urlnew = "https://www.youtube.com/watch?v=" + vid;
        //alert(id);
        //alert(vid);
        var width = $('#videoMain2').width();
        var height = $('#videoMain2').height();
        var heighthalf = (height / 2) - 15;
        var widthhalf = (width / 2) - 15;
        var photo = 'https://img.youtube.com/vi/' + vid + '/0.jpg';
        //$("div[rel=" + id + "] img:first").attr("src", photo); 
        //$("div[rel=" + id + "] a:first").attr("href", url); 
        //$("div[rel=" + id + "] a:first").attr("class", "video");
        // $('#' + id).css('background-image', 'url(' + photo + ')');
        $('#' + id).html('<a href="javascript:;" rel="' + urlnew + '&fs=1&rel=0&enablejsapi=1&playerapiid=ytplayer;" class="video"><img src="' + photo + '" height="' + height + '" width="' + width + '" style="z-index=-100;"><div style="z-index: 10; left: ' + widthhalf + 'px; position: absolute; top: ' + heighthalf + 'px"><img src="https://so.tsmaapplication.com/content/images/play_icon.png" style="z-index:10; "></div> </a>');
        // $.ajax({
        //            url: 'ajax.php',
        //            data: ({ utubevid: vid, ajax: 1, utube: 1, page_id: 0 }),
        //            success: function (data) {
        //                $('#' + id).html(data);
        //                $('#spinner').fadeOut('slow', function () { $('#' + id).css('background-image', "url(http://mysocialmediaagency.com/tsms_beta/playimgbig.php?img=http://mysocialmediaagency.com/tsms_beta/thumb.php?x=370%26y=229%26src=https://img.youtube.com/vi/" + vid + "/0.jpg)"); });
        //            }
        //        });

    }

    function videosmall(url, id) {
        //alert('small url : '+ url + ' id : '+id);

        var vid;
        var results;

        results = url.match("[\\?&]v=([^&#]*)");
        var res;

        res = (results === null) ? url : results[1];
        vid = res.replace("http://youtu.be/", "");
        var urlnew;
        urlnew = "https://www.youtube.com/watch?v=" + vid;

        var photo = 'https://img.youtube.com/vi/' + vid + '/2.jpg';
        $("div[rel=" + id + "] img:first").attr("src", photo);
        $("div[rel=" + id + "] a:first").attr("href", 'javascript:;');
        $("div[rel=" + id + "] a:first").attr("rel", urlnew);
        $("div[rel=" + id + "] a:first").attr("class", "video");


    }
    function videobigreset(id) {

        $('#' + id).html('');
        //$('#' + id).css('background-image', "none");
    }


    function videosmallreset(id) {

        $("div[rel=" + id + "] img:first").attr("src", "content/images/no_video_youtube.jpg");
        // $("div[rel=" + id + "] a:first").attr("href","");
        $("div[rel=" + id + "] a:first").attr("rel", "");
        $("div[rel=" + id + "] a:first").attr("class", "");

        //        if (document.getElementById("videoimgfix") != null) {
        //            $("div[rel=" + id + "] div:first").css('background-image', "url(<?php echo SITE_URL ; ?>template2/images/testimonialVideo.png)");
        //        }
        //        else {
        //            $("div[rel=" + id + "] img:first").attr("src", "<?php echo SITE_URL ; ?>template2/images/testimonialVideo.png");
        //        }
    }

    function videobig2reset(id) {
        $('#' + id).html('');
        // $('#' + id).css('background-image', "none");
    }


    $("#rm-css-control-cancel").click(function (e) {
        /*alert(revent_index);
        alert(revent_style);*/
        $(".rm-css-edit:eq(" + revent_index + ")").attr('style', revent_style);
        $(".rm-css-edit:eq(" + revent_index + ")").parent().attr('style', revent_parent_style);
    });

    $(".rm-css-edit").click(function (e) {
        revent_index = $(".rm-css-edit").index(this);
        revent_style = $(this).attr('style');
        revent_parent_style = $(this).parent().attr('style');
    });

    function rmtextcontrolcancel() {
        $("#content_edit_box").val(textrevent_html);
        $("span[rel^=rm-css-text]:eq(" + textrevent_index + ")").html(textrevent_html);
    }

    $("span[rel^=rm-css-text]").click(function (e) {
        textrevent_index = $("span[rel^=rm-css-text]").index(this);
        textrevent_html = $(this).html();
    });

    $("span[rel^=rm-css-text][title!=notextedit]").click(function (e) {

        rel = $(this).attr('rel');
        form = '<div style="font-size:16px"><b>Edit Text : </b><br><br> <textarea style="font-size: 12px;width:400px;height:100px;" id="content_edit_box" onkeyup="$(\'span[rel=' + rel + ']\').html(nl2br(this.value));show(\'rm-text-control-cancel\')"  >' + mtrim(br2nl($(this).html()), '&nbsp;') + '</textarea><br><br><a href="#" id="rm-text-control-cancel" style="display:none" onclick="rmtextcontrolcancel()">Cancel Text changes</a></div>';

        if (edit) {
            fbook_box(e, form);
            $('#facebox').fadeIn('slow');
        }
    });

    $("span[rel^=rm-css-text]").mouseenter(function () { if (edit) $(this).css("cursor", "pointer"); });
    $("span[rel^=rm-css-text]").mouseleave(function () { if (edit) $(this).css("cursor", "default"); });
    $("#commtimg").css("display", "inline");
    $("#fbcommt").css("display", "none");

    function fbook_box(e, html) {
        //e.pageX
        $('#fbox').html('<div style="top: ' + (e.pageY - 30) + 'px; left: ' + 280 + 'px; display: none;z-index:2000000000" id="facebox"><div class="popup"><table><tr><td class="tl"></td><td class="b"></td><td class="tr"></td></tr><tr><td class="b"></td><td class="body"><div style="float: none; display: block;" class="content">' + html + '</div><div class="footer" style="display: block; width:100%;"><a class="close" href="#_" onclick="fboxhide()"><img class="close_image" title="close" src="content/facebox/closelabel1.gif"></a></div></td><td class="b"></td></tr><tr><td class="bl"></td><td class="b"></td><td class="br"></td></tr></table></div></div><div class="facebox_hide facebox_overlayBG" onclick="fboxhide()" id="facebox_overlay" style="display: block; opacity: 0;filter:alpha(opacity=0);"></div>');
    }

    function fboxhide() {
        parent.document.getElementById('hdnSaveHeader').value = "0";
        document.getElementById('hdnIsSaved').value = "0";
        $('#facebox_overlay').hide();
        $('#facebox').fadeOut('slow');
    }

    function share() {
        return true;
    }
    function setLocation(url) {
        return url;
    }

    /*if (navigator.appName == 'Microsoft Internet Explorer') {
    jQuery.facebox('<div style="font-size:14px;font-weight:bold">This Tool work best on Following Browsers.</div></br><div><a href="#"><div style="float:left"><img src="content/sidebar-images/firefox_small_logo.png" height=120 /><div align="center" style="font-weight:bold;font-size:14px;font:arial;" >Firefox</div></div></a> <a href="#"><div style="float:left"><img src="content/sidebar-images/chrome_small_logo.png" height=120/><div align="center" style="font-weight:bold;font-size:14px;font:arial;" >Chrome</div></div></a>  <a href="#"><div style="float:left"><img src="content/sidebar-images/safari_small_logo.png" height=120/><div align="center" style="font-weight:bold;font-size:14px;font:arial;" >Safari</div></div></a> <div>');
    }*/

    function show(id) {
        if (edit) {
            document.getElementById(id).style.display = 'block';
        }
    }

    function hide(id) {
        document.getElementById(id).style.display = 'none';
    }

    function nl2br(str, is_xhtml) {
        // http://kevin.vanzonneveld.net
        // +   original by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
        // +   improved by: Philip Peterson
        // +   improved by: Onno Marsman
        // +   improved by: Atli Þór
        // +   bugfixed by: Onno Marsman
        // +      input by: Brett Zamir (http://brett-zamir.me)
        // +   bugfixed by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
        // +   improved by: Brett Zamir (http://brett-zamir.me)
        // +   improved by: Maximusya
        // *     example 1: nl2br('Kevin\nvan\nZonneveld');
        // *     returns 1: 'Kevin<br />\nvan<br />\nZonneveld'
        // *     example 2: nl2br("\nOne\nTwo\n\nThree\n", false);
        // *     returns 2: '<br>\nOne<br>\nTwo<br>\n<br>\nThree<br>\n'
        // *     example 3: nl2br("\nOne\nTwo\n\nThree\n", true);
        // *     returns 3: '<br />\nOne<br />\nTwo<br />\n<br />\nThree<br />\n'
        var breakTag = (is_xhtml || typeof is_xhtml === 'undefined') ? '<br />' : '<br>';

        brstr = (str + '').replace(/([^>\r\n]?)(\r\n|\n\r|\r|\n)/g, '$1' + breakTag + '$2');

        if (brstr == '') {
            return '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
        }
        else {
            return brstr;
        }
    }

    function br2nl(str) {
        return str.replace(/<br\s*\/?>/mg, "");
    };

    /**
    *
    *  Javascript trim, ltrim, rtrim
    *  http://www.webtoolkit.info/
    *
    **/

    function trim(str, chars) {
        return ltrim(rtrim(str, chars), chars);
    }

    function ltrim(str, chars) {
        chars = chars || "\\s";
        return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
    }

    function rtrim(str, chars) {
        chars = chars || "\\s";
        return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
    }

    function mtrim(str, chars) {
        chars = chars || "\\s";
        return str.replace(new RegExp("&nbsp;", "g"), "");
    }


</script>
<div id="divBackGround" class="divBackGround" style="display:none;"></div>
</td>
</tr>
</table>
</div>
</div>
<%--<div id="div3" runat="server" style="margin-right:0px;width:74px;height:40px;overflow:hidden;position:absolute;">
                          <asp:UpdateProgress ID="UpdateProgress1" runat="Server" DisplayAfter="0">
                            <ProgressTemplate> <img src="content/images/bigspinner.gif" style="border:0px;font-family:Tahoma;font-size:12px;" alt="Loading" /> </ProgressTemplate>
                          </asp:UpdateProgress>
                        </div>	--%>
<uc2:inner ID="inner2" runat="server" />
</body>
</html>

<script type="text/javascript" >
    function Pageid(_this) {
        if ($(_this).attr("checked") == true) {
            $('input[group^=pages]').each(
							function () {
							    $(this).attr("checked", false)
							}
						);
            $(_this).attr("checked", true);
        }
        else {
            $('input[group^=page]').each(
							function () {
							    $(this).attr("checked", false)
							}
						);
        }
    }
    function selectedpages() {
        var pages = '';
        var hdnPages = '';
        $('input[group^=pages]').each(
						function () {
						    if ($(this).attr("checked") == true) {
						        pages = pages + $(this).attr("pageid") + ','

						    }
						}
					);
        if (pages != '') {

            $('#hdnPageId').val(pages);
            return true;
        }
        else {
            return false;
        }

    }

    function selectedpageimage() {

        var pageImage = '';
        var hdnPages = '';
        $('input[group^=pages]').each(
						function () {
						    if ($(this).attr("checked") == 'checked') {
						        pageImage = pageImage + $(this).attr("pageimage") + ','
						    }
						}
					);
        if (pageImage != '') {
            $('#hdnImage').val(pageImage);
            return true;
        }
        else {
            return false;
        }
    }
    function selectedpagesName() {
        var pages = '';
        var hdnPages = '';
        $('input[group^=pages]').each(
						function () {
						    if ($(this).attr("checked") == 'checked') {
						        var pageid = $(this).attr("pageid")
						        pages = pages + $(this).attr("pagevalue") + "<a href='javascript:RemovePage(" + pageid + ")'>&nbsp;&nbsp;<img src='../content/images/remove.gif' width='8' height='8' /></a><br/>"
						    }
						}
					);
        if (pages != '') {
            $('#divHtml').show("slow");
            $('#divHtml').html("<strong>Selected Pages:</strong><br/>" + pages);
            return true;
        }
        else {
            $('#divHtml').hide("slow");
            $('#divHtml').html('');
            return false;
        }
    }

    function RemovePage(pageid) {
        $('input[group^=pages]').each(
						function () {
						    if ($(this).attr("checked") == 'checked') {
						        var pageid1 = $(this).attr('pageid');
						        if (pageid == pageid1) {
						            var id = $(this).attr('id');
						            $("#" + id).removeAttr("checked");
						            selectedpagesName();
						        }
						    }
						}
					);


    }
    function HideProgress() {
        // parent.document.getElementById("imgLoading").style.display = 'none';
        hideDivPopup('divLoading');
        showDivPopup('dvMessage');
    }
    function ShowProgress() {
        ClosePage();
        $('#spinner').fadeIn('slow');
        //showDivPopup('divLoading');

    }


    function selectedpagesAccessToken() {

        var pagesaccesstoken = '';
        var hdnPages = '';
        $('input[group^=pages]').each(
						function () {
						    if ($(this).attr("checked") == 'checked') {
						        pagesaccesstoken = pagesaccesstoken + $(this).attr("pageaccess_token") + ','
						    }
						}
					);

        if (pagesaccesstoken != '') {
            $('#hdnAccessToken').val(pagesaccesstoken);
            return true;
        }
        else {
            return false;
        }
    }
    function ValidatePublish() {

        var Title = "Fill in Following Information\n";
        var fields = "";
        if (selectedpages() == false) {
            fields = fields + "\n-- Please select a business page before attempting to post --";
        }

        selectedpagesAccessToken();
        selectedpageimage();
        selectedpagesName();

        if (fields.length > 0) {
            alert(Title + fields);

            return false;
        }
        else {
            ClosePage();
            PublishCustomTab1();
            GetWidthHeight();
            //PublishAlert();
            return true;
        }
    }
    function CheckThis(obj) {
        if (obj.src.indexOf('/c.png') == -1) {
            obj.src = obj.src.replace('/uc.png', '/c.png');
            document.getElementById('hdnResizeCheck').value = '1';
        }
        else {
            obj.src = obj.src.replace('/c.png', '/uc.png');
            document.getElementById('hdnResizeCheck').value = '0';
        }
    }

    $('.fgtype').each(
	function () {
	    $(this).click(
			function () {
			    setpos(1);
			}
		);
	}
	)
    $('.bgtrans').each(
	function () {
	    $(this).click(
			function () {
			    setpos(2);
			}
		);
	}
	)
    function setpos(mode) {
        if (mode == 1) {
            $('#divSaveAndClose').css('top', 185 + 'px');
        }
        else {
            $('#divSaveAndClose').css('top', 230 + 'px');
        }
    }
    function CloseCustomiseUser() {
        ClosePage();
        $('#CustomiseUserMessages').slideUp('slow');
        //showDivPopup('divLoading');

    }

    function showCustomiseUserMessage() {
        //SaveCustomTab();
        /*var pos;
        pos = findPos(document.getElementById('img1'));
        var lft = pos[0]+700;
        document.getElementById('CustomiseUserMessages').style.left = '200px';
        lft = pos[1] + 120;
        document.getElementById('CustomiseUserMessages').style.top = lft + 'px';*/
        $('#CustomiseUserMessages').slideDown('slow');
    }


    function ValidateCustomiseUserMessage() {

        var fields = "";

        if ((document.getElementById('txtShareText').value) == '' ) {
            fields = fields + "\n-- Share Text Required --";
            
        }

        if ((document.getElementById('txtShareMessage').value) == '') {
            fields = fields + "\n-- Share Message Required --";
            
        }

//        if ((document.getElementById('txtShareEmailadd').value) == '') {
//            fields = fields + "\n-- Share Email Address Required --";
//           
//        }

        if ((document.getElementById('txtShareEmailText').value) == '') {
            fields = fields + "\n-- Share Email Add Text Required --";

        }

        if ((document.getElementById('ShareImg').value) == '') {
            fields = fields + "\n-- Share Image Required --";

        }

        else {
            var fup = document.getElementById('ShareImg');
            var fileName = fup.value;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "JPEG" || ext == "jpeg" || ext == "jpg" || ext == "JPG" || ext == "doc") {
                
            }
            else {
                
                alert("Upload Gif or Jpg images only");
                fup.focus();
                
            }

        }

        if ((document.getElementById('PDffile').value) == '') {
            fields = fields + "\n-- Share PDF Required --";

        }

        else {
            var fup = document.getElementById('PDffile');
            var fileName = fup.value;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "pdf") {

            }
            else {

                alert("Upload PDF file only");
                fup.focus();

            }

        }
        if (fields != "") {
            fields = "Please fill in the following details\n--------------------------------------\n" + fields;
            alert(fields);
            return false;
        }
        else {

          
            __doPostBack("csswdetails", "");
            return true;
          
        }

    }
</script>
