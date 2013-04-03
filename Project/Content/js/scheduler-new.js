// JavaScript Documentfunction CheckMessage() {
function DoTrim(strComp) {
	ltrim = /^\s+/
	rtrim = /\s+$/
	strComp = strComp.replace(ltrim, '');
	strComp = strComp.replace(rtrim, '');
	return strComp;
}	
//Start
function QuickPost(t) 
{
	$("#dvLoading").show();
	setTimeout(function(){doQuickPost(t);}, 1000);	
}
function doQuickPost(t)
{
	CloseLib();
	$("#dvLoading").hide();
	$('#AjaxFileUpload1_Html5DropZone').hide();
	$('.removeButton').hide();
	$('.ajax__fileupload_queueContainer').hide();
	$('#AjaxFileUpload1_ctl00').css('border','0px');
	if(t=='1'){$("#tblPhoto").hide();$("#tblVideo").hide();$("#tbBusinessPage").hide();$(".popupbordertop").css( "padding-left", "70px" );}
	if(t=='2'){$("#tblVideo").hide();$("#tbBusinessPage").hide();$("#tblPhoto").slideDown("slow");$(".popupbordertop").css( "padding-left", "210px" );}
	if(t=='3'){$("#tblPhoto").hide();$("#tbBusinessPage").hide();$("#tblVideo").slideDown("slow");$(".popupbordertop").css( "padding-left", "310px" );}
	if(t=='4'){$("#tbBusinessPage").slideDown("slow");$(".popupbordertop").css( "padding-left", "470px" );}	
}

function ValidateDrafts() {			
	var fields = "";
	if ($.trim($('#txtMessage').val()) == '') {fields = fields + "\n-- Compose Message --";}   
	//Checkbox check
	var isChecked='0';
	$.each($($(".checkboxpadding"), x), function() {				
	if($(this).attr('checked'))
	{
		isChecked='1';		
	}
	});
	if (isChecked == '0') {fields = fields + "\n-- Select Business Page --";}   
	
	if (fields.length > 0) {alert("Fill in Following Information\n" + fields);return false;}
	else {
		$('#spnSaveToDraft').html('&nbsp;&nbsp;<img align="absmiddle" src="content/images/loading-fb.gif" />&nbsp;Saving...&nbsp;&nbsp;');
		 var msg=$("#txtMessage").val();
		 var lnkY=$("#txtvideo").val();
		 var lnkYId=$("#hdnVideoId").val();
		 var lnkYImage=$("#hdnVideoImage").val();		 
		 var lnkP=$('#hdnImageUrl').val();
		 var fpId='';
		 var fpAccessToken='';
		 var fpName='';
		 var fpImage='';
		 //Get FanPages Details
		 var x = $("#tbBusinessPage");
		 $.each($($(".checkboxpadding"), x), function() {				
			if($(this).attr('checked'))
			{
				var objchk=$(this).attr('id');
				var tmpId=objchk.replace('chkPage_','hdnPageId_');
				fpId=fpId + $('#' + tmpId).val() + '%@$';	
				tmpId=objchk.replace('chkPage_','hdnPageName_');
				fpName=fpName + $('#' + tmpId).val() + '%@$';
				tmpId=objchk.replace('chkPage_','hdnAccessToken_');
				fpAccessToken=fpAccessToken + $('#' + tmpId).val() + '%@$';
				tmpId=objchk.replace('chkPage_','hdnImage_');
				fpImage=fpImage + $('#' + tmpId).val() + '%@$';
			}
			});		 
		 //Final Post
		$.post("post-to-facebook.aspx",{pt:'1',msg:msg,lnkY:lnkY,lnkYId:lnkYId,lnkYImage:lnkYImage,lnkP:lnkP,fpId:fpId,fpName:fpName,fpAccessToken:fpAccessToken,fpImage:fpImage},function(result){
		$('#spnSaveToDraft').html('&nbsp;&nbsp;<img align="absmiddle" src="content/images/saved-fb.png" />&nbsp;Saved&nbsp;&nbsp;');
		deselectAll();
		setTimeout(function(){$('#spnSaveToDraft').html('Save as Draft');}, 1000);	
		});;
		 return true;
		}
}
//File Upload
function uploadError(sender, args) {
	//addToClientTable(args.get_fileName(), "<span style='color:red;'>" + args.get_errorMessage() + "</span>");
	alert(args.get_errorMessage());
}
function uploadComplete(sender, args) {
	var src=$('#hdnFBUserId').val() + '-' + args.get_fileName();
	$('#hdnImageUrl').val(src);
	src='content/uploads/images/' + src;
	$('#imgPhoto').attr("src", src);	
}
function CheckVideo() {
	document.getElementById('imgThumbnail').src = 'content/images/loading-fb.gif'
	var obj = document.getElementById('txtvideo');
	if (obj.value != '') {
		var vid;
		var results;
		var url = DoTrim(obj.value).toLowerCase();
		//Youtube
		url = DoTrim(obj.value);
		results = url.match("[\\?&]v=([^&#]*)");
		var res;
		res = (results === null) ? "" : results[1];
		vid = res.replace("http://youtu.be/", "");
		if (vid != '') 
		{
			$('#hdnVideoId').val(vid);
			$('#hdnVideoImage').val('http://img.youtube.com/vi/' + vid + '/2.jpg');			
			setTimeout(function(){$('#imgThumbnail').attr('src','http://img.youtube.com/vi/' + vid + '/2.jpg');}, 1000);
			return true;
		}		
	}
	else {
		obj.value = '';
		document.getElementById('txtvideo').value = '';
		setTimeout(function(){$('#imgThumbnail').attr('src','content/images/no_img.jpg');}, 1000);
		$('#hdnVideoId').val('');
		$('#hdnVideoImage').val('');
		return false;
	}
}
function validateURL() {
	var textval = document.getElementById("txtvideo").value;
	var urlregex = new RegExp(
			"^(http:\/\/www.|https:\/\/www.|ftp:\/\/www.|www.){1}([0-9A-Za-z]+\.)");
	if (urlregex.test(textval)) {
		return true;
	}
	else {
		alert("Please Enter Valid Url");
		return false;
	}
}
function deselectAll()
{
	$("#txtMessage").val('');	
	$("#txtvideo").val('');
  	$("#hdnVideoId").val('');
	$("#hdnVideoImage").val('');
	$('#hdnImageUrl').val('');
	$('#imgThumbnail').attr('src','content/images/no_img.jpg');
	$('#imgPhoto').attr("src", 'content/images/no_img.jpg');	
	var x = $("#tbBusinessPage");
	$.each($($(".checkboxpadding"), x), function() {				
	if($(this).attr('checked'))
	{
		$(this).attr('checked',false);
	}
	});	
	
}

//Save To Lib
function SelectLibCat() {
	$('#divSchedule').slideUp('slow');
	$('#trNew').css('display', '');
	$('#trCreateNew').css('display', 'none');
	$('#txtNewLibCat').val('');
	var pos = $('#lnkSaveToMyLib').position();
	pos.left = parseInt(pos.left);
	pos.top = parseInt(pos.top);
	var x = pos.left-52;
	var y = pos.top;
	
	$('#divLibCat').css('left', x + 'px')
	$('#divLibCat').css('top', y + 30 + 'px')
	if ($('#divLibCat').css('display') == 'none') {
		$('#divLibCat').slideDown('slow');
		LoadMyLibrariesFolders();
	}
	else {
		$('#divLibCat').slideUp('slow');
	}
}
//Close Lib
function CloseLib()
{
	$('#divSchedule').slideUp('slow');
	$('#trNew').css('display', '');
	$('#trCreateNew').css('display', 'none');
	$('#txtNewLibCat').val('');
	$('#divLibCat').slideUp('slow');
}
function FolderOver(obj,todo)
{
	if(todo==0)
	{
		obj.style.border='1px solid #83aada';
		obj.style.backgroundColor='#ddecfe';
	}
	else
	{
		obj.style.border='1px solid #ffffff';
		obj.style.backgroundColor='#ffffff';
	}
}
//Create New Folder/Lib
function CreateNewLibCat() {
	$('#trNew').css('display', 'none');
	$('#trCreateNew').css('display', '');
	$('#txtNewLibCat').val('');
	$('#hdnLibCatId').val('-1');
}
function saveMyLibrary()//For New Folder
{
	if (DoTrim($('#txtNewLibCat').val()) == '') 
	{
		alert('-- Enter Folder Title');
		//$('#txtNewLibCat').addClass("error");		
	}
	else {

		if ($('#txtMessage').val() == '') {
			$('#txtMessage').addClass('error');
			alert("Please enter Library message!")			
		}
		else 
		{
			$('#txtNewLibCat').removeClass("error");
			//Save
			$('#ibtnFolderSave').attr('src','content/images/loading-fb.gif');
			var msg=$("#txtMessage").val();
		 	var lnkY=$("#txtvideo").val();
		 	var lnkYId=$("#hdnVideoId").val();
		 	var lnkYImage=$("#hdnVideoImage").val();		 
		 	var lnkP=$('#hdnImageUrl').val();
			var catTitle=$('#txtNewLibCat').val();
		 	$.post("post-to-facebook.aspx",{pt:'3',msg:msg,lnkY:lnkY,lnkYId:lnkYId,lnkYImage:lnkYImage,lnkP:lnkP,lcat:'-1',catTitle:catTitle},function(result){
			setTimeout(function(){$('#ibtnFolderSave').attr('src','content/images/saved-fb.png');setTimeout(function(){$('#ibtnFolderSave').attr('src','content/images/save-ico.png');deselectAll();LoadMyLibrariesFolders();}, 1000);}, 1000);
			});;						
		}
	}	
}
function saveMyLibrary2(id)//For Existing Folder
{
	if ($('#txtMessage').val() == '') {
		$('#txtMessage').addClass('error');
		alert("-- Please enter Library message!")			
	}
	else 
	{
		$('#txtNewLibCat').removeClass("error");
		//Show Loading...
		var content='<div style="float:left;height=15px;">' + $('#tdLibFolder_'+ id).html() + '</div>';
		$('#tdLibFolder_'+ id).html(content + '<div style="float:right; height=15px;"><img align="absmiddle" src="content/images/loading-fb.gif" style="padding-bottom:10px;" /></div>');
		//Save
		var msg=$("#txtMessage").val();
	 	var lnkY=$("#txtvideo").val();
	 	var lnkYId=$("#hdnVideoId").val();
	 	var lnkYImage=$("#hdnVideoImage").val();		 
	 	var lnkP=$('#hdnImageUrl').val();
		var catTitle=$('#txtNewLibCat').val();
	 	$.post("post-to-facebook.aspx",{pt:'3',msg:msg,lnkY:lnkY,lnkYId:lnkYId,lnkYImage:lnkYImage,lnkP:lnkP,lcat:id,catTitle:''},function(result){
		setTimeout(function(){$('#tdLibFolder_'+ id).html(content + '<div style="float:right"><img align="absmiddle" src="content/images/saved-fb.png" style="padding-bottom:10px;" /></div>');setTimeout(function(){deselectAll();$('#tdLibFolder_'+ id).html(content);}, 1000);}, 1000);
		});;						
	}	
}
function LoadMyLibrariesFolders()
{
	$('#spnLibFolders').html('&nbsp;&nbsp;<img align="absmiddle" src="content/images/loading-fb.gif" style="padding-bottom:10px;" />&nbsp;');
	$.post("post-to-facebook.aspx",{pt:'2'},function(result){
	setTimeout(function(){$('#spnLibFolders').html(result);}, 1000);	
	});;
}
//Quick Post
function validateQuickPost(){
var fields = "";
	if ($.trim($('#txtMessage').val()) == '') {fields = fields + "\n-- Compose Message --";}   
	//Checkbox check
	var isChecked='0';
	$.each($($(".checkboxpadding"), x), function() {				
	if($(this).attr('checked'))
	{
		isChecked='1';		
	}
	});
	if (isChecked == '0') {fields = fields + "\n-- Select Business Page --";}   
	
	if (fields.length > 0) {alert("Fill in Following Information\n" + fields);return false;}
	else {
		$('#spnQuickPost').html('&nbsp;&nbsp;<img align="absmiddle" src="content/images/loading-fb.gif" />&nbsp;Posting...&nbsp;&nbsp;');
		 var msg=$("#txtMessage").val();
		 var lnkY=$("#txtvideo").val();
		 var lnkYId=$("#hdnVideoId").val();
		 var lnkYImage=$("#hdnVideoImage").val();		 
		 var lnkP=$('#hdnImageUrl').val();
		 var fpId='';
		 var fpAccessToken='';
		 var fpName='';
		 var fpImage='';
		 var sm_id='';
		 //Get FanPages Details
		 var x = $("#tbBusinessPage");
		 $.each($($(".checkboxpadding"), x), function() {				
			if($(this).attr('checked'))
			{
				var objchk=$(this).attr('id');
				var tmpId=objchk.replace('chkPage_','hdnPageId_');
				fpId=fpId + $('#' + tmpId).val() + '%@$';	
				tmpId=objchk.replace('chkPage_','hdnPageName_');
				fpName=fpName + $('#' + tmpId).val() + '%@$';
				tmpId=objchk.replace('chkPage_','hdnAccessToken_');
				fpAccessToken=fpAccessToken + $('#' + tmpId).val() + '%@$';
				tmpId=objchk.replace('chkPage_','hdnImage_');
				fpImage=fpImage + $('#' + tmpId).val() + '%@$';
				var sm_id='0';
			}
			});		 
		 //Final Post
		$.post("post-to-facebook.aspx",{pt:'4',msg:msg,sm_id:sm_id,lnkY:lnkY,lnkYId:lnkYId,lnkYImage:lnkYImage,lnkP:lnkP,fpId:fpId,fpName:fpName,fpAccessToken:fpAccessToken,fpImage:fpImage},function(result){
		$('#spnQuickPost').html('&nbsp;&nbsp;<img align="absmiddle" src="content/images/saved-fb.png" />&nbsp;Posting Done&nbsp;&nbsp;');
		deselectAll();
		setTimeout(function(){$('#spnQuickPost').html('Quick Post');}, 1000);	
		});;
		 return true;
		}
}