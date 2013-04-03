function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
$(document).ready(
	function()
	{
		MM_preloadImages('../../../Content/images/dentist_header.jpg');
		MM_preloadImages('../../../Content/images/dentist_main_bg.jpg');
		MM_preloadImages('../../../Content/images/dentist_banner.jpg');
	}
);

$(document).ready(
function(){
	    var pos = $('#spnChooseInd').position();
		pos.left = parseInt(pos.left);
		pos.top = parseInt(pos.top);
		var x = pos.left;
		var y = pos.top;
		$('#dvIndustry').css('top',y+44+'px')
		
		var pos1 = $('#spnChoose').position();
		pos1.left = parseInt(pos1.left);
		pos1.top = parseInt(pos1.top);
		var x1 = pos1.left;
		var y1 = pos1.top;
		$('#dvCompany').css('top',y1+44+'px')
		
});
  
function AddClass(_this)
{
    $(_this).addClass('menuhover');
}

function RemoveClass(_this)
{
	 var bit = $(_this).attr("show")
	 if(bit==0)
	 { 
	 	$(_this).removeClass('menuhover');
	 }
}
 
function GetPositionInd(_this) {
	$('td[group^=industry]').each(
		function()
		{
			$(this).removeClass("menuhover");	
			$(this).attr("show","0");	
		}
	);
	$(_this).addClass('menuhover');
	$(_this).attr("show","1");
    var pos = $(_this).position();
	var id = $(_this).attr("industryid")
	var name=$(_this).attr("industryname")
	var image=$(_this).attr("image")
	var cssstyle=$(_this).attr("cssstyle")
	$('#hdnindid').val(id);
	$("#divSelectedIndustry").removeClass('selectbginactive');
	$("#divSelectedIndustry").removeClass('selectbgnone');
	$("#divSelectedIndustry").addClass('selectbgactive');
	$("#divSelectedIndustry").html('<img src="../../Content/images/'+ image + '" align="absbottom"/>&nbsp;&nbsp;' + name);
	$('#SelectedItem').html(name);
	$("#dvIndustry").slideUp("slow");	
	ShowFirstStepPopUp();
	$("#hdnMapI").val('0');
	$('#hdncid').val(0);
	$("#divSelectedCompany").removeClass('selectbgactive');
	$("#divSelectedCompany").removeClass('selectbginactive');
	$("#divSelectedCompany").addClass('selectbgnone');
	$("#divSelectedCompany").html('');
	$('td[group^=company]').each(
		function()
		{
			$(this).removeClass("menuhover");	
			$(this).attr("show","0");			
		}
	);
	$('#lnkTheme').attr("href", '../../Content/css/'+ cssstyle +'');
	createCookie('style',cssstyle,1);
	
}
function GetPosition(_this) {
  	$('td[group^=company]').each(
		function()
		{
			$(this).removeClass("menuhover");	
			$(this).attr("show","0");			
		}
	);
    $('#CompanyId').val(0);
    $('#txtPwdCompIndu').val('');
	$(_this).attr("show","1");
    $(_this).addClass('menuhover');
    var pos = $(_this).position();
    var bit = $(_this).attr("bit")
    var id = $(_this).attr("companyid")
    var name=$(_this).attr("companyname")
    var image=$(_this).attr("image")
    var cssstyle=$(_this).attr("cssstyle")
	$('#hdnid').val(id);
	
	
	$('#spnName').html(name);
    if (bit == 0) {
        $('#divPassword').css("display", 'none');
        $('#SelectedItem').html(name);
        $("#divSelectedCompany").removeClass('selectbginactive');
        $("#divSelectedCompany").removeClass('selectbgnone');
        $("#divSelectedCompany").addClass('selectbgactive');
        $("#divSelectedCompany").html('<img src="../../Content/images/'+ image + '" align="absbottom"/>&nbsp;&nbsp;' + name);
        $("#dvCompany").slideUp("slow");	
        $("#hdnMap").val('0');
        $('#hdnindid').val(0);
		$('#hdncid').val(id);
        $("#divSelectedIndustry").removeClass('selectbgactive');
        $("#divSelectedIndustry").removeClass('selectbginactive');
        $("#divSelectedIndustry").addClass('selectbgnone');
        $("#divSelectedIndustry").html('');
		$('td[group^=industry]').each(
			function()
			{
				$(this).removeClass("menuhover");	
				$(this).attr("show","0");	
					
			}
		);
        $('#lnkTheme').attr("href", '../../Content/css/'+ cssstyle +'');
	   createCookie('style',cssstyle,1);
       ShowFirstStepPopUp();
			
    }
    else {
        $('#txtPwdCompIndu').removeClass('input-validation-error');
        $('#spnPwdError').html('');
        $('#hdncname').val(name);
        $('#CompanyId').val(id);
        pos.left = parseInt(pos.left);
        pos.top = parseInt(pos.top);
        var x = pos.left;
        var y = pos.top;
        $('#divPassword').css("left", x + 170 + 'px');
        $('#divPassword').css("top", y - 55 + 'px');
        $("#hdnimage").val(image);
		$("#divPassword").slideDown("slow");
        $("#hdnStyle").val(cssstyle);
    }
		 
}
function ShowFirstStepPopUp(_this)
{
	  
    var pos = $('#spnChoose').position();
    pos.left = parseInt(pos.left);
    pos.top = parseInt(pos.top);
    var x = pos.left;
    var y = pos.top;
	
    $('#divFirstStep').css('top', y+110 + 'px')
    $('#divFirstStep').css('left', x+260 + 'px')
	$("#divFirstStep").slideDown("slow");

}
	  
function MM_openBrWindow(obj) {
    var userid = obj;
   
	//alert(userid);
	var iid=$('#hdnindid').val();
	var cid=$('#hdncid').val();
	if(iid==-1 && cid == -1 & obj == -1)
	{
			
	}
	else
	{
		if(iid==0)
		{
				iid = -1;
		}
		if(cid==0)
		{
				cid = -1;
		}
	}

  //window.open("http://www.tsmaapplication.com/Login.aspx?i="+ iid +"&c="+ cid +"&u="+ userid +"", "facebooklgin", "left=370,top=200,menubar=0,resizable=0,width=420,height=300");
window.open("Login.aspx?i="+ iid +"&c="+ cid +"&u="+ userid +"", "facebooklgin", "left=370,top=200,menubar=0,resizable=0,width=420,height=300");
//window.open("http://www.techsturedevelopment.com/Login.aspx?i="+ iid +"&c="+ cid +"&u="+ userid +"", "facebooklgin", "left=370,top=200,menubar=0,resizable=0,width=420,height=300");

}
  
var tmp='1';
var obj;
var Company='';
$(document).ready(function(){
  $("#spnChoose").click(function(){
	$('#divPassword').css("display", 'none');
  	tmp=$("#hdnMap").val();
    if(tmp=='1')
	{
		$("#dvCompany").slideUp("slow");	
		$("#hdnMap").val('0');
		
	}
	else
	{
		$("#dvCompany").slideDown("slow");	
		$("#hdnMap").val('1');
		
	}
	$("#dvIndustry").slideUp("slow");	
	$("#hdnMapI").val('0');	
    $("#divFirstStep").slideUp("slow");
  });
})

  
$(document).ready(function(){
var tmp='1';
var obj;
var Industry='';
	
   $("#spnChooseInd").click(function(){
  	tmp=$("#hdnMapI").val();
    if(tmp=='1')
	{
		$("#dvIndustry").slideUp("slow");	
		$("#hdnMapI").val('0');
	}
	else
	{
		$("#dvIndustry").slideDown("slow");	
		$("#hdnMapI").val('1');
	}	
	$("#dvCompany").slideUp("slow");	
	$("#hdnMap").val('0');
	$("#divFirstStep").slideUp("slow");
  });
})
$(document).ready(function () {
    $('#btnSubmit').bind('click', function (event) {
        MyClickEventHandler();
    });
});

function MyClickEventHandler()
{
    SendRequest('WebService/CompanyPassword.aspx/CompanyPasswordValidation', '', GetSendResult, Error);	
}
function SendRequest(url,args,onSuccess,onFail)
{
	if($('#txtPwdCompIndu').val()=='')
	{
		$('#txtPwdCompIndu').addClass('input-validation-error');
		$('#spnPwdError').html('The Password field is required.');
	}
	else
	{
	    var args = '"pwd":"' + $('#txtPwdCompIndu').val() + '","id":"' + $('#CompanyId').val() + '"';
	    $.ajax({
			type: "POST",
			url: url,
			data: '{' + args +'}',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: onSuccess,
			error: onFail
		});
	}
}
	
function Error(errorResponse)
{
	alert("error")	
    $('#spnPwdError').html('Your request not submited. Please try again! ')
   
		
}
	
function GetSendResult(result)
{
		var name=$('#hdncname').val();
		var image=$('#hdnimage').val();
		var id=$('#hdnid').val();
		var cssstyle=$('#hdnStyle').val();
	if(result)
	{
	   //alert("TEST    " +  result.d.Message);
	   if (result.d.Message == '')
		{
			$('#SelectedItem').html(name);
			$("#divSelectedCompany").removeClass('selectbginactive');
			$("#divSelectedCompany").removeClass('selectbgnone');
		  	$("#divSelectedCompany").addClass('selectbgactive');
		    $("#divSelectedCompany").html('<img src="../../Content/images/'+ image + '" align="absbottom"/>&nbsp;&nbsp;' + name);
			$("#dvCompany").slideUp("slow");	
			$("#hdnMap").val('0');
			$('#hdnindid').val(0);
			$('#hdncid').val(id);
			$("#divSelectedIndustry").removeClass('selectbgactive');
			$("#divSelectedIndustry").removeClass('selectbginactive');
     		$("#divSelectedIndustry").addClass('selectbgnone');
	    	$("#divSelectedIndustry").html('');
			$('td[group^=industry]').each(
			function()
			{
				$(this).removeClass("menuhover");	
				$(this).attr("show","0");	
					
			}
		);
			// $('#lnkTheme').attr("href", '../../Content/css/'+ cssstyle +'');
			ShowFirstStepPopUp();
			$('#divPassword').css("display", 'none');
			$('#lnkTheme').attr("href", '../../Content/css/'+ cssstyle +'');
	         createCookie('style',cssstyle,1);
			//$('#divPassword').slideUp("fast");
			
		}
		else 
        {
            $('#spnPwdError').html(result.d.Message)
			$('#txtPwdCompIndu').addClass('input-validation-error');
		}
		
    }
	
}

function ChangeLoginFocus(eve)
{
	var code= eve.keyCode||eve.which;
	if (code == 13)
	{
		$('#btnSubmit').click();
	}
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

$(document).ready(function(){
	var cssstyle=''
	if(readCookie('style'))
	{	
		cssstyle=readCookie('style');
	}
	else
	{
		cssstyle='apartment-style.css';
	}
	$('#lnkTheme').attr("href", '../../Content/css/' + cssstyle + '');
});
