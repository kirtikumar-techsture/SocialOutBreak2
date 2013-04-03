<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="training.aspx.vb" Inherits="tsma.training" %>
<%@ Register src="inner-footer.ascx" tagname="inner" tagprefix="uc2" %>
<%@ Register src="left.ascx" tagname="left" tagprefix="uc3" %>
<%@ Register src="inner-header.ascx" tagname="inner" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Total Social Media Application</title>
<link href="Content/css/inner.css" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("~/Scripts/jquery-1.6.2.min.js") %>" type="text/javascript"></script>
<script type="text/javascript" src="Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
<link rel="stylesheet" type="text/css" href="Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
    <script type="text/javascript" src="Content/js/fancybox/fancybox/video.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	var loc = window.location;
    var currentURL = loc.protocol + '//' + loc.host + loc.pathname;
	if(loc.protocol == 'https:')
	{
		var currentURL1 = loc.protocol.replace('https:','http:') + '//' + loc.host + loc.pathname;
		//alert(loc.protocol);
	    //alert(currentURL1);		
		$(location).attr('href',currentURL1);
		return true;		
	}
	else
	{
		//var currentURL1 = loc.protocol.replace('http:','https:') + '//' + loc.host + loc.pathname;
		//alert(currentURL1);
    	//$(location).attr('href',currentURL1);
		return true;
	}
});
function ShowVideoDiv(id,title)
	{
	    //document.getElementById('traning_vid2').style.display='none';	
		document.getElementById('divVideo2').style.display='';
		document.getElementById('divVideo1').style.display='none';
		document.getElementById('divVideo3').style.display='none';	
		document.getElementById('divVideo').style.display='none';
        document.getElementById('videotitle').innerHTML=title;
		document.getElementById('youtubevideo2').src='';
		document.getElementById('youtubevideo2').src=id;
		//document.getElementById('myebook').style.display='none';	
			
	}
	
	function ShowVideoDiv1()
	{
	    //document.getElementById('traning_vid2').style.display='none';	
		document.getElementById('divVideo1').style.display='none';
		document.getElementById('divVideo2').style.display='none';
		document.getElementById('divVideo3').style.display='none';	
		//document.getElementById('myebook').style.display='none';	
		document.getElementById('divVideo').style.display='';	
	}
	function ShowVideoDiv2()
	{
	    //document.getElementById('traning_vid2').style.display='none';	
		document.getElementById('divVideo').style.display='none';
		document.getElementById('divVideo3').style.display='none';	
		document.getElementById('divVideo2').style.display='none';
		//document.getElementById('myebook').style.display='none';	
		document.getElementById('divVideo1').style.display='';	
	}
	function ShowVideoDiv3()
	{
	    //document.getElementById('traning_vid2').style.display='none';	
		document.getElementById('divVideo').style.display='none';
		document.getElementById('divVideo2').style.display='none';
		document.getElementById('divVideo1').style.display='none';
		//document.getElementById('myebook').style.display='none';	
		document.getElementById('divVideo3').style.display='';	
	}
</script>
</head>
<body>
<form id="frm" runat="server">
  <asp:ScriptManager ID="objScriptManager" runat="server"></asp:ScriptManager>
  <div id="innerpagepagemain"  >
    <uc1:inner ID="inner1" runat="server" />
    <div id="centermain">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="170" align="left" valign="top" class="leftbg"><uc3:left ID="left1" runat="server" />
          </td>
          <td align="left" valign="top" class="contentbody"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="30" align="left" valign="top"> <span style="float:left;font-family: Tahoma, Geneva, sans-serifthaoma;font-size: 16px;color: #181818;margin:0px;font-weight:bold;line-height:18px;">Your Online Training Center</span>
                
                    <div style="float:right;" > 
           	<a id="btnVideo" runat="server" class="video">
            <img id="imgVideo" src="../Content/images/watch_video_tutorial_red.jpg" runat="server"  style="width:213px;height:36px"/></a>
               </div>
                

                </td>
              </tr>
              <tr>
                <td>Helping professionals launch smart Social Media Marketing <br />
                  so your business goals are accomplished</td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td>
                <div id="div4" runat="server" style="background-color:;padding-left:250px;margin-right:0px;width:74px;height:40px;overflow:hidden;position:absolute;display:">
                                      <asp:UpdateProgress ID="UpdateProgressDiv4" runat="Server" DisplayAfter="0">
                                        <ProgressTemplate> <img src="../Content/images/bigspinner.gif" style="border:0px;font-family:Tahoma;font-size:12px;" alt="Loading" /> </ProgressTemplate>
                                      </asp:UpdateProgress>
                                    </div>
                <asp:UpdatePanel ID="UpdatePanelDiv1" runat="server">
                <ContentTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="581" align="left" valign="top"><div id="divVideo">
                          <div id="divVideoTitle" style=" font-family:Arial, Helvetica, sans-serif; font-weight:bold; padding-bottom:10px; font-size:12px; "><span id="spanTitle" runat="server"></span></div>
                          <div style="width:581px; height:370px; overflow:hidden;"><span id="spnVideoEmbed" runat="server" /></div>
                        </div>
                        </td>
                      <td width="20" align="left" valign="top">&nbsp;</td>
                      <td align="left" valign="top" class="blueboxbg" style="padding:0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td  width=100% align="left" valign="middle" class="blueboxtitle" >Your Virtual Training</td>
                          </tr>
                          <tr>
                            <td align="left" valign="top" style="padding-left:15px; padding-right:5px;">
                            <div style="height:330px; overflow:auto; overflow-x:hidden">
                                <asp:Repeater ID="rptCategory" runat="server" >
                                          <itemtemplate>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0"  >
                                              <tr>
                                                <td height="30">
                                                <div class="subtitle" style="height:25px; padding-top:10px; border-bottom:2px solid #FFF">
                                                <span class="arial14"; style="text-transform: capitalize; color:#000"><strong><%#Eval("vcat_Name")%></strong></span>
                                                </div>
                                                </td>
                                              </tr>
                                              <tr>
                                                <td  style="padding-bottom:5px;padding-top:10px;" width="100%">
                                                <asp:DataList ID="dlstVideos" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" ItemStyle-Width="100%" DataSource='<%# BindAllVideos(Container.dataItem("vcat_id")) %>' >
                                                    <itemtemplate>
                                                      <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                          <td width="100%" valign="top" style="height:20px;">
                                                          <asp:LinkButton  OnCommand="imgVideoThumb_Click" CommandArgument='<%# Container.DataItem("vd_Id") %>' id="lnkLibCatSel" runat="server"><%# Container.DataItem("vd_title") %></asp:LinkButton>
                                                          </td>
                                                        </tr>
                                                        
                                                      </table>
                                                    </itemtemplate>
                                                </asp:DataList>
                                                </td>
                                              </tr>
                                            </table>
                                          </itemtemplate>
                                      </asp:Repeater>
                            </div></td>
                          </tr>
                        </table></td>
                    </tr>
                  </table>
                  </ContentTemplate>
                  </asp:UpdatePanel>
                  </td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
  </div>
  <uc2:inner ID="inner2" runat="server" />
</form>
</body>
</html>
