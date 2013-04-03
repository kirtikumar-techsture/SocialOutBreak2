//Created by Sean Kane (http://celtickane.com/programming/code/ajax.php)
//Feather Ajax v1.0.1
function AjaxObject101() {
	this.createRequestObject = function() {
		try {
			var ro = new XMLHttpRequest();
		}
		catch (e) {
			var ro = new ActiveXObject("Microsoft.XMLHTTP");
		}
		return ro;
	}
	this.sndReq = function(action, url, data,content) {
			this.div=content;	
			
		if (action.toUpperCase() == "POST") {
			this.http.open("POST",url,true);
			this.http.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
			this.http.onreadystatechange =this.handleResponse;
			this.http.send(data);
		}
		else {
			this.http.open(action,url + '?' + data,true);
			this.http.onreadystatechange = this.handleResponse;
			this.http.send(null);
		}
	}
	this.handleResponse = function() {
		if ( me.http.readyState == 4) {
			if (typeof me.funcDone == 'function') { me.funcDone();}
			
			 document.getElementById(me.div).innerHTML=me.http.responseText;
			 
			
			 /*
			var rawdata = me.http.responseText.split("|");
			for ( var i = 0; i < rawdata.length; i++ ) {
				var item = (rawdata[i]).split("=>");
				if (item[0] != "") {
					
					if (item[1].substr(0,3) == "%V%" ) {
						document.getElementById(item[0]).value = item[1].substring(3);
					}
					else {
						document.getElementById(item[0]).innerHTML = item[1];
					}
				}
			}
			*/
		reveal('type_of_player');}
		if ((me.http.readyState == 1) && (typeof me.funcWait == 'function')) { me.funcWait(); }
	}
	var me = this;
	this.http = this.createRequestObject();
	
	var funcWait = null;
	var funcDone = null;
}
function AjaxObject102() {
	this.createRequestObject = function() {
		try {
			var ro = new XMLHttpRequest();
		}
		catch (e) {
			var ro = new ActiveXObject("Microsoft.XMLHTTP");
		}
		return ro;
	}
	this.sndReq = function(action, url, data,content) {
			this.div=content;	
			
		if (action.toUpperCase() == "POST") {
			this.http.open("POST",url,true);
			this.http.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
			this.http.onreadystatechange =this.handleResponse;
			this.http.send(data);
		}
		else {
			this.http.open(action,url + '?' + data,true);
			this.http.onreadystatechange = this.handleResponse;
			this.http.send(null);
		}
	}
	this.handleResponse = function() {
		if ( me.http.readyState == 4) {
			if (typeof me.funcDone == 'function') { me.funcDone();}
			
			 document.getElementById(me.div).innerHTML=me.http.responseText;
			 
			
			 /*
			var rawdata = me.http.responseText.split("|");
			for ( var i = 0; i < rawdata.length; i++ ) {
				var item = (rawdata[i]).split("=>");
				if (item[0] != "") {
					
					if (item[1].substr(0,3) == "%V%" ) {
						document.getElementById(item[0]).value = item[1].substring(3);
					}
					else {
						document.getElementById(item[0]).innerHTML = item[1];
					}
				}
			}
			*/
		//reveal('type_of_player');
		}
		if ((me.http.readyState == 1) && (typeof me.funcWait == 'function')) { me.funcWait(); }
	}
	var me = this;
	this.http = this.createRequestObject();
	
	var funcWait = null;
	var funcDone = null;
}


/*
function ajax(url,formid,div,spinner)
{
	data='';
//	spinner = 'spinner';
// Collecting form post data 
	if(formid != null && formid != '')
	  {
		var form=document.getElementById(formid);
		
		for ( var e = 0; e < form.elements.length; e ++ ) 
		    {
			var el = form.elements [ e ];
			
			if(el.type=='checkbox' || el.type=='radio')
			{
					if(el.checked)
					{
						data+=el.name+'='+el.value+'&';
					}
			}
			else
			{
				data+=el.name+'='+el.value+'&';
			}
			
			}	
	  }
// making ajax object
	var ao = new AjaxObject101();
	
// Spinner show and hide		
	
	ao.funcWait = function(){if(spinner != null && spinner != ''){document.getElementById(spinner).style.display = '';}}
	ao.funcDone = function(){if(spinner != null && spinner != ''){document.getElementById(spinner).style.display = 'none';}//runScript();
	}	
	  
// Sending Ajax request 	
	ao.sndReq('post',url,data,div);
}
*/

function ajax3(url,formid,div,spinner)
{
	if(spinner != null && spinner != ''){document.getElementById(spinner).style.display = '';}
	
	if(formid == null || formid == '')
	{
		$.post(url, {a:1},
	   function(data) {
		 if(div != null && div != '')document.getElementById(div).innerHTML =  data;
		 if(spinner != null && spinner != ''){document.getElementById(spinner).style.display = 'none';}
	   });
	}
	else
	{
		$.post(url, $("#"+formid).serialize(),
	   function(data) {
		 if(div != null && div != '')document.getElementById(div).innerHTML =  data;
		 if(spinner != null && spinner != ''){document.getElementById(spinner).style.display = 'none';}
	   });

	}
}

function ajax2(url,formid,div,spinner){ajax(url,formid,div,spinner);}

/*function ajax2(url,formid,div,spinner)
{
	data='';
//	spinner = 'spinner';
// Collecting form post data 
	if(formid != null && formid != '')
	  {
		var form=document.getElementById(formid);
		
		for ( var e = 0; e < form.elements.length; e ++ ) 
		    {
			var el = form.elements [ e ];
			
			if(el.type=='checkbox' || el.type=='radio')
			{
					if(el.checked)
					{
						data+=el.name+'='+el.value+'&';
					}
			}
			else
			{
				data+=el.name+'='+el.value+'&';
			}
			
			}	
	  }
// making ajax object
	var ao = new AjaxObject102();
	
// Spinner show and hide		
	
	ao.funcWait = function(){if(spinner != null && spinner != ''){document.getElementById(spinner).style.display = '';}}
	ao.funcDone = function(){if(spinner != null && spinner != ''){document.getElementById(spinner).style.display = 'none';}//runScript();
	}	
	  
// Sending Ajax request 	
	ao.sndReq('post',url,data,div);
}
*/
function runScript(){if(done!=null){falert(done);}}

function loadXMLDoc(url)
{
	if (window.XMLHttpRequest)
	  {// code for IE7+, Firefox, Chrome, Opera, Safari
	  xmlhttp=new XMLHttpRequest();
	  }
	else
	  {// code for IE6, IE5
	  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
	  }
	xmlhttp.open("GET",url,false);
	xmlhttp.send(null);
	// document.getElementById('whererugoing').innerHTML = xmlhttp.responseText;
	if(xmlhttp.responseText == "error")
	{
		alert('Unknow Error! Please try again later.');
	}
	else
	{
		//alert('hi123');		
		document.getElementById('whererugoing').innerHTML = xmlhttp.responseText;
		FB.Connect.streamPublish(xmlhttp.responseText, null, null, null,null,null,false, user_id);
	}
}
function loadURL(url)
{
	if (window.XMLHttpRequest)
	  {// code for IE7+, Firefox, Chrome, Opera, Safari
	  xmlhttp=new XMLHttpRequest();
	  }
	else
	  {// code for IE6, IE5
	  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
	  }
	xmlhttp.open("GET",url,false);
	xmlhttp.send(null);
	// document.getElementById('whererugoing').innerHTML = xmlhttp.responseText;
	if(xmlhttp.responseText == "error")
	{
		alert('Unknow Error! Please try again later.');
		return "";
	}
	else
	{
		//alert('hi123');		
		return xmlhttp.responseText;
	}
}

function add(id)
{
	if(document.getElementById(id) != null)
	{
		setValue(id,parseInt(getValue(id))+1);
	}
	else
	{
		alert('element not found');
	}
	return getValue(id) ;	
}

function minus(id)
{
	if(document.getElementById(id) != null)
	{
		if(getValue(id)>1){setValue(id,getValue(id)-1);}
	}
	else
	{
		alert('element not found');
	}
	return getValue(id) ;	

}



function getValue(id)
{
	return document.getElementById(id).value ;
}

function setValue(id,newValue)
{
	document.getElementById(id).value = newValue ;
	return 1;
}

function setInnerHTML(id,newValue)
{
	document.getElementById(id).innerHTML = newValue ;
	return 1;
}


function setInnerHTML(id)
{
	return document.getElementById(id).innerHTML ;
}

function toggle(ids)
{
  if(isArray(ids))
  {
	  for ( x in  ids)
		{
			if(getDisplay(ids[x])=='none')
			{
				show(ids[x]);
			}
			else
			{
				hide(ids[x]);
			}
		}
  }
  else
  {
  	if(getDisplay(ids)=='none')
			{
				show(ids);
			}
			else
			{
				hide(ids);
			}
  }		
	return 1;
}

function show(id)
{
	document.getElementById(id).style.display = '';
	return 1;
}

function hide(id)
{
	document.getElementById(id).style.display = 'none';
	return 1;
}

function getDisplay(id)
{
	return document.getElementById(id).style.display ;
}

function isArray(testObject)
{   
    return testObject && !(testObject.propertyIsEnumerable('length')) && typeof testObject === 'object' && typeof testObject.length === 'number';
}

var TimeToFade = 3000.0;

function fade(eid)
{
  var element = document.getElementById(eid);
  if(element == null)
    return;
   
  if(element.FadeState == null)
  {
    if(element.style.opacity == null
        || element.style.opacity == ''
        || element.style.opacity == '1')
    {
      element.FadeState = 2;
    }
    else
    {
      element.FadeState = -2;
    }
  }
   
  if(element.FadeState == 1 || element.FadeState == -1)
  {
    element.FadeState = element.FadeState == 1 ? -1 : 1;
    element.FadeTimeLeft = TimeToFade - element.FadeTimeLeft;
  }
  else
  {
    element.FadeState = element.FadeState == 2 ? 1 : 1;
    element.FadeTimeLeft = TimeToFade;
    setTimeout("animateFade(" + new Date().getTime() + ",'" + eid + "')", 33);
  }  
}

function animateFade(lastTick, eid)
{  
  var curTick = new Date().getTime();
  var elapsedTicks = curTick - lastTick;
 
  var element = document.getElementById(eid);
 
  if(element.FadeTimeLeft <= elapsedTicks)
  {
    element.style.opacity = element.FadeState == 1 ? '1' : '1';
    element.style.filter = 'alpha(opacity = '
        + (element.FadeState == 1 ? '100' : '1') + ')';
    element.FadeState = element.FadeState == 1 ? 2 : -2;
    return;
  }
 
  element.FadeTimeLeft -= elapsedTicks;
  var newOpVal = element.FadeTimeLeft/TimeToFade;
  if(element.FadeState == 1)
    newOpVal = 1 - newOpVal;

  element.style.opacity = newOpVal;
  element.style.filter = 'alpha(opacity = ' + (newOpVal*100) + ')';
  
  setTimeout("animateFade(" + curTick + ",'" + eid + "')", 1);
}



function ChLenghtLimit2(id,maxnum)
{	
	a=document.getElementById(id).value;
	b=a.substring(0,maxnum);
	document.getElementById(id).value=b;
		alert('here');
		if(a.length<maxnum)
		{
			c=maxnum-a.length
			document.getElementById('count').innerHTML=c;
		}
		else
		{
			document.getElementById('count').innerHTML=0;
		}
}

function uniqid()
    {
    var newDate = new Date;
    return newDate.getTime();
    } 


function chkImageType(file)
{
var str			   =	getValue('file') ;
var before_ext_len =	str.indexOf('.')+1 ;
var file_ext	   =	str.substr(before_ext_len) ;
var	file_ext	   =  	file_ext.toLowerCase();
//var uniq_filename=uniqid()+'.'+file_ext;
var ch=0;

var valid_ext = ['jpg','jpeg','png','gif'];

	for(var i in valid_ext)
	{	
		if(valid_ext[i] == file_ext )
		{		
		 ch=1;
		 return true;
		}
	}

	if (ch==0)
	{
	falert('Invalid file type !......'); 
	return false;
	}
}