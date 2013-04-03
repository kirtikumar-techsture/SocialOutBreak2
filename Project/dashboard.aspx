<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dashboard.aspx.vb" Inherits="tsma.dashboard" %>
<%@ Register src="left.ascx" tagname="left" tagprefix="uc3" %>
<%@ Register src="inner-header.ascx" tagname="inner1" tagprefix="uc1" %>
<%@ Register src="inner-footer.ascx" tagname="inner" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Total Social Media Application</title>
	<script src="<%=ResolveUrl("~/Scripts/jquery-1.6.2.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
	<script type="text/javascript" src="Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
    <script type="text/javascript" src="Content/js/fancybox/fancybox/video.js"></script>
     
           
</head>
<body>
    <form id="form1" runat="server">
      <div id="innerpagepagemain">
     <uc1:inner1 ID="inner1" runat="server" />
      <div id="centermain">
      <div id="DivSaveSidebar" style="width:100%; height:100%; text-align:center;  background-image:url(Content/facebookalert/images/popup_bg.png);  position:absolute; z-index:999999999; text-align:center;  display:none;">
			  <div id="popup_container1" style="width:450px; height:80px;"  >
			    <div id="popup_content" style="padding-top:10px; padding-left:20px; text-align:left">
                  <span id="spnMessage"></span><br/><br/>
                   <input type="button" class="inputbutton" onclick="HideSaveAlert();" value="Close" id="popup_close" />
				</div>
			 </div>
			 </div>
       <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="left" valign="top" ><table width="974" border="0" align="left" cellpadding="0" cellspacing="0">
      <tr>
        <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
               <td width="172" align="left" valign="top" class="leftbg">
                
                    <uc3:left ID="left1" runat="server" />
                
                </td>
               
                <td align="left" valign="top" class="contentbody">
				
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
			  <tr>
                  <td align="left" valign="top" style="padding-bottom:5px;">
				  <h6></h6>
				  </td>
				 </tr> 
                 <tr>
                 <td align="center" style="">&nbsp;
                     </td>
                 </tr>
                <tr>
                  <td align="center" valign="top">
                  
                  <table width="500" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="49" align="center" valign="middle" bgcolor="#5973a8" style="font-size:22px; font-family:arial;  color:#FFF">Watch the Tutorial Video</td>
                      </tr>
                      <tr>
                        <td align="center" valign="top" style="background-image:url(Content/images/title_down_bg.gif); background-position:top; background-repeat:repeat-x"><img src="Content/images/arrow_down_blue.png" width="58" height="20" /></td>
                      </tr>
                      <tr>
                        <td align="left" valign="top" bgcolor="#edeff4"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td align="center" valign="top">Make Summa Social work for you by  <br />
                              watching this simple video walkthru of our dashboard.</td>
                          </tr>
                        
                          <tr>
                            <td align="center" valign="top" style="padding:10px;"> <div style="border:1px solid #bdc7d8; background-color:#FFF; padding:13px;">
                            
                              
                              <iframe style="width:660px;height:360px" src="https://www.youtube.com/embed/X_fG5Nqv2To?feature=player_detailpage" frameborder="0" allowfullscreen></iframe>


                              </div>
                              </td>
							    <tr>
    <td height="30px;" valign="middle" style="text-align:right; padding-right:10px; padding-bottom:7px;">
      <asp:LinkButton ID="pdfdownload" runat="server">DOWNLOAD PDF</asp:LinkButton>
    </td>
  </tr>
                          </tr>
                        </table></td>
                      </tr>
                    </table>
                  </td>
                  <td align="left" valign="top">&nbsp;
				      </td>
                </tr>
              </table>
              <br />
			  
               </table>
                   
                </td>
                </tr>
              </table></td>
          </tr>
          </table></td>
      </tr>
    </table>
    <uc2:inner ID="inner2" runat="server" />
    </form>
<div id="divBackGround" class="divBackGround" style="display:none;"></div>
</body>
</html>
<script language="javascript">

    function showPages() {
        var pos;
        pos = findPos(document.getElementById('img1'));
        var lft = pos[0] - 540;
        document.getElementById('Fanpagesdiv1').style.left = lft + 'px';
        lft = pos[1] + 10;
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
    function PostSweepStake(obj) {
        var tmpId;
        tmpId = obj.id.replace('hrefPost', 'hdnPageName');
        tmpId = confirm('Are You Sure You Want to Add Sweepstakes on ' + $('#' + tmpId).val() + '?');
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
                        $('#divLoading').slideUp('slow');
                        showDivPopup('dvMessage');
                    }
                    else {
                        document.getElementById('spnMsg').innerHTML = xmlhttp.responseText;
                    }
                }
            }
            showDivPopup('divLoading');
            xmlhttp.open("POST", "ajax/ajax-post-sweepstake.aspx?pid=" + document.getElementById(obj.id.replace('hrefPost', 'hdnPageId')).value + "&pat=" + document.getElementById(obj.id.replace('hrefPost', 'hdnAccessToken')).value + "&pnm=" + document.getElementById(obj.id.replace('hrefPost', 'hdnPageName')).value, true);
            xmlhttp.send();
        }
    }
    function AllClose() {

    }
    function Pageid(_this) {

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
        SaveAlert('Sweepstakes uploaded successfully');
        hideDivPopup('imgLoading');

        showDivPopup('dvMessage');
    }
    function ShowProgress() {
        // parent.document.getElementById("imgLoading").style.display = 'block';
        ClosePage();
        showDivPopup('imgLoading');
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
            ShowProgress();
            return true;
        }
    }
    function SaveAlert(mess) {
        alert("test");
        $("#spnMessage").html(mess);
        $("#DivSaveSidebar").show("slow");
    }
    function HideSaveAlert() {
        $("#DivSaveSidebar").hide("slow");
    }
</script>
