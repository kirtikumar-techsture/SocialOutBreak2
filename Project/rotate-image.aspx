<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rotate-image.aspx.vb" Inherits="tsma.rotate_image" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <script src="Content/js/jquery-1.7.2.min.js" type="text/javascript"></script>
  <script src="http://jqueryrotate.googlecode.com/svn/trunk/jQueryRotate.js" type="text/javascript"></script>
    <script src="Content/js/jquery.min.js" type="text/javascript"></script>
  <script src="Content/js/jQueryRotate.2.1.js" type="text/javascript"></script>
     <script type="text/javascript" src="Content/js/jrac/jquery.jrac.js"></script>
    <!-- This page business -->
    <link rel="stylesheet" type="text/css" href="Content/css/style_jrac.css" />
     <link rel="stylesheet" type="text/css" href="Content/js/jrac/style.jrac.css" />
       <link rel="stylesheet" type="text/css" href="Content/js/jrac/jquery-ui.css" />
      <script type="text/javascript">
          function CheckPhoto() {
              if ($('#flImage').val() == '') {
                  alert("Please Upload Image");
                  return false;
              }
              /*else
              {
              $('#DivCropArea').show();
              return true;
              }*/
          }
          function ShowCropDiv() {
              $('#divCropImage').show();
          }
	
	</script>
    
</head>
<body>

<%--
    <form id="form1" runat="server">
   <div>
    <table>
        <tr>
            <td style="width:300px:height:300px;">
     <asp:Image ID="Image1" runat="server"  ImageUrl="Desert.jpg" 
          />
            </td>
         </tr>
         
     </table> 

      
   </div> 

    <asp:Button ID="Button1" runat="server" Text="Rotate" 
         Font-Bold="True" Font-Names="Verdana" />
 </form>--%>


    
    <form id="form2" runat="server">
               <asp:ScriptManager ID="objScriptManager1" runat="server"></asp:ScriptManager>
          <div id="divLib" runat="server" style="margin-right:0px;width:74px;height:40px;overflow:hidden;position:absolute">
            <asp:UpdateProgress ID="UpdateProgressLib" AssociatedUpdatePanelID="uplMain" runat="Server" DisplayAfter="0">
              <ProgressTemplate> <img src="Content/images/ajax-loading.gif" style="border:0px;font-family:Tahoma;font-size:12px;" alt="Loading" /> </ProgressTemplate>
            </asp:UpdateProgress>
          </div>
         <table border="0" align="left" cellpadding="0" cellspacing="0" style="padding-left:20px;">
                  	<tr>
                  	  <td align="left" valign="top">&nbsp;</td>
                  	  <td align="left" height="30"><asp:Literal ID="lblMessage" runat="server" Visible="false"></asp:Literal></td>
       	   </tr>
                  	<tr>
                  	  <td width="32" align="left" valign="top"><img src="Content/images/step_1crop.png" width="24" height="24" /></td>
                    	<td align="left" height="30" style="padding-bottom:20px;">Please select image from your computer to edit image : <input type="file" id="flImage" runat="server"/>  &nbsp; &nbsp; &nbsp;<br/>                        </td>
                    </tr>
                    <tr>
                      <td align="left" valign="top" style="padding-bottom:20px;"><img src="Content/images/step_2crop.png" width="24" height="24" /></td>
                      <td align="left" style="padding-bottom:20px;">Click on Upload button to load this image <input type="button" id="btnUpload" runat="server" value="upload" onclick="CheckPhoto();" class="bluetablink"/></td>
                    </tr>

                    <tr>
                      <td align="left" valign="top" style="padding-bottom:20px;">&nbsp;</td>
                    	<td align="left" style="padding-bottom:20px;">
                        
                            <div class="pane clearfix" id="divCropArea" style="text-align:center; display:" align="center">
                            <br />
                               <asp:UpdatePanel ID="uplMain" runat="server">
                                <ContentTemplate>
                               <img ID="imgRotateImg" runat="server" src="../Content/images/resize_preview.jpg"  />
                                 </ContentTemplate>
                                </asp:UpdatePanel>      
                            </div>     
                            
                                  
                            </td>
                    </tr>
                     <tr style="padding-bottom:100px;">
                      <td align="left" valign="top"><img src="Content/images/step_3crop.png" width="24" height="28" /></td>
                    	<td> Rotate Image <input type="submit" runat="server" id="btnRotateImg" value="Rotate Image" class="bluetablink"/> </td>
                    
                    </tr>
                     <tr>
                     <td height="28"></td>
                     </tr>
                         <tr >
                      <td align="left" valign="top" ><img src="Content/images/step_4crop.png" width="24" height="24" /></td>
                    	<td align="left"  valign="top">Download and save this image on your local computer.
                        </br>
                        </br>
                         <a id="lnkDownload" runat="server" class="bluetablink">Download Image</a>
                         
                         Please use this image to upload to sidebar/custom tab designs whereever you want to.                      </td>
                        </tr>

                    </table>
       
                   </form>
</body>
</html>
