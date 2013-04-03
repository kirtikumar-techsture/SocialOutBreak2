<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="referrer.aspx.vb" Inherits="tsma.referrer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Referrer</title>
<script type="text/javascript" src="Content/js/detectmobile.js"></script>
<%-- <script>
            function RedirectUrl()
			{
						alert('aaaaa');
						var url = detectmobile.replaceDomainName("http://www.foobar.com:9999", "m.barbar.com");
						if(url != "http://m.barbar.com:9999") {
							throw "Failed replace domain name:" + url;
						}
                        var url = detectmobile.replaceDomainName("http://www.foobar.com", "m.barbar.com");
                        if(url != "http://m.barbar.com") {
                                throw "Failed replace domain name:" + url;
                        }
			
                        var url = detectmobile.replaceDomainName("http://localhost:9666/site", "m", true);
                        if(url != "http://m.localhost:9666/site") {
                                throw "Failed replace domain name:" + url;
                        }		
			
                        var url = detectmobile.replaceDomainName("http://www.site.com/page", "m", true, true);
                        if(url != "http://m.site.com/page") {
                                throw "Failed replace domain name:" + url;
                        }     
						
						if(window.location.href.indexOf("m.localhost") >= 0) {
							if(!detectmobile.isOnMobileSite()) {
								alert("Failed in domain name mobile site detection");
								throw "die";
							}
						}
						
						detectmobile.redirectCallback = function(mode, url) {
							alert(mode);
							alert(url);
							var mobileURL = "http://www.facebook.com/pages/Test-Form/103867749667879?id=103867749667879&sk=app_149478528549893&ref=ts"; //detectmobile.replace(url, "http://www.facebook.com/pages/Test-Form/103867749667879?id=103867749667879&sk=app_149478528549893&ref=ts");
							//alert("Going to mobile URL:" + url);
							return mobileURL;
						}
						detectmobile.process();
						
						if(window.location.hostname == "m.localhost") 
						{
							alert("This is mobile");
						} 
						else {
							alert("This is Web");
											//window.location.href = $('#hdnFBReferrerUrl').val();//"http://www.facebook.com/pages/Test-Form/103867749667879?id=103867749667879&sk=app_149478528549893&ref=ts"; //$('#hdnFBReferrerUrl').val();//detectmobile.replace(url, "http://www.facebook.com/pages/Test-Form/103867749667879?id=103867749667879&sk=app_149478528549893&ref=ts");								
						}
			}
			  				

                </script>--%>

</head>
<body>
<input type="hidden" id="hdnReferrerUrl" runat="server" />
<input type="hidden" id="hdnFBReferrerUrl" runat="server" />
</body>
</html>
