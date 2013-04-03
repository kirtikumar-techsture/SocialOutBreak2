<?php session_start();?>
<?php include_once('header1.php');?>
<style>.blkimg{ width:100%; position:absolute; background:url(images/blkimg.png); height:100%; padding-top:115px; z-index:1000;}</style>
<?php // print_array($_REQUEST);?>
<?php $logout = isset($_REQUEST['logout'])? $_REQUEST['logout'] :""; ?>
<?php 


if($logout){
session_destroy();
$_SESSION['user_id'] = '';
echo '<script>window.top.location="index.php"</script>';
}

?>
<?php $user_id = $_SESSION['user_id'];?>

<script>
function OpenPopup (c) {
               window.open(c,
               'window',
               'width=480,height=240,scrollbars=0,status=0');
             
			   }
</script>
<script type="text/javascript">
(function() {
var s = document.createElement('SCRIPT'), s1 = document.getElementsByTagName('SCRIPT')[0];
s.type = 'text/javascript';
s.async = true;
s.src = 'http://widgets.digg.com/buttons.js';
s1.parentNode.insertBefore(s, s1);
})();
</script>


<?php $user_data = user_data($user_id); ?>



<?php

if(isset($_POST['most']) && isset($_POST['least']))
{
	for($i=0;$i<24;$i++)
	{
		$answers[]=array('most'=>$_POST['most'][$i],'least'=>$_POST['least'][$i]);
	}

$answer = answers($_SESSION['user_id'],$answers);

//use = $_SESSION['user_id'];
 //  print_array($answer);
		  
$assessment = assessment($answer['assessment_id']);
$pscr = powerscore($answer['assessment_id']);

	$sql= "insert into report set user_id='$user_id' , assessment_id='{$answer['assessment_id']}' , report='{$assessment['preview']}', style='{$assessment['style']}',Influencing='{$pscr['powerdisc_scores'][0]['score']}',Directing='{$pscr['powerdisc_scores'][1]['score']}',Processing='{$pscr['powerdisc_scores'][2]['score']}',Detailing='{$pscr['powerdisc_scores'][3]['score']}',Creating='{$pscr['powerdisc_scores'][4]['score']}',Persisting='{$pscr['powerdisc_scores'][5]['score']}',Relating='{$pscr['powerdisc_scores'][6]['score']}'";
	mysql_query($sql) or die($sql.mysql_error());

  $sql= "insert into assessments set user_id=$user_id , assessment_id = {$answer['assessment_id']} ,pdf_url = '{$answer['assessment_link']}', style='{$assessment['style']}' ";
	mysql_query($sql) or die($sql.mysql_error());
	//sql("insert into assessments set user_id='$user_id' , assessment_id = '{$answer['assessment_id']}' ,pdf_url = '{$answer['assessment_link']}' ");
	//print_array($assessment);
  

} 
//$scr = score(39554);
//print_array($scr);	

//$occ = occupation();
//print_array($occ);

$pscr = powerscore(39554);
//echo $pscr['powerdisc_scores'][0]['area'];		
//print_r($pscr);	
	$sql= "select * from report where user_id='$user_id' order by id desc limit 1";
	$res = mysql_query($sql) or die($sql.mysql_error());
	
	
	if(mysql_num_rows($res)>0)
	{
		$row = mysql_fetch_assoc($res);
			
		$err = false ;
		$user_report  = str_replace('firstname','',$row['report']);
  		$split_report = split('\|',$user_report);
		$assessment_id =$row['assessment_id'];
		$style = $row['style'];
	}
	else
	{
		$err = true ;
		$description='';
		$title='';
	}
	
 ?>
<meta property="og:description" content="<?php echo $split_report[2]; ?>" />
<body>

<div class="mainDiv">
	<div class="inner" style="position:relative"><div style="display:none" id="email_inviter" class="blkimg" >
<table  border="0" cellspacing="0" cellpadding="0" style="position: absolute; margin-left: 190px;padding-right: 130px;"   >
  <tr>
    <td width="16" class="popuplfttop"></td>
    <td class="popuprpttop"></td>
    <td class="popuprgttop"></td>
  </tr>
  <tr>
    <td class="popuprptlft">&nbsp;</td>
    <td class="popupcenter">
	<div style="float:right"><a href="#_" onClick="hide('email_inviter')" style="color:#000000"><b>x</b></a></div>
	<div id="email_div">
	<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="20" class="f14black"><strong>Check your email address books & share your profile results with your friends</strong></td>
      </tr>
      <tr>
        <td height="20" class="f12black" style="padding-left:25px;">If your friends are already in our network, just send them a nudge. if not, invite them to join.   </td>
      </tr>
      <tr>
        <td height="20">
		<form method="post" id="email_form">
		<table width="85%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="20" class="f14black"><strong>1. Select Email Address Book to check </strong></td>
          </tr>
          <tr>
            <td> <div style="float:left; height:1px; border-bottom:1px dotted #d6d6d6; width:80%;">&nbsp;</div></td>
          </tr>
          <tr>
            <td height="50"><a href="#_" onClick="document.getElementById('mail_servies').selectedIndex=0"><img src="images/gmail.png" width="114" height="36" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#_" onClick="document.getElementById('mail_servies').selectedIndex=1"><img src="images/yahoomail.png" width="114" height="36" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#_" onClick="document.getElementById('mail_servies').selectedIndex=2"><img src="images/aol.png" width="114" height="36" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#_" onClick="document.getElementById('mail_servies').selectedIndex=3"><img src="images/windlivehotmail.png" width="152" height="36" /></a></td>
          </tr>
          <tr>
            <td height="50"><a href="#_" onClick="document.getElementById('mail_servies').selectedIndex=3"><img src="images/windlive.png" width="152" height="36" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          </tr>
          <tr>
            <td height="25" class="f14black"><strong>2. Login to your Email Adress </strong></td>
          </tr>
          <tr>
            <td height="25"><div style=" margin:0 auto; width:80%; text-align:center">
			
			<table width="100%" border="0" cellspacing="2" cellpadding="0">
  <tr>
    <td width="85" align="left" class="f14black"><strong>Email:</strong></td>
    <td width="144" align="left"><input type="text" name="user_name" id="user_name" /></td>
    <td>&nbsp;</td>
    <td width="227" align="left"><select name="mail_servies" id="mail_servies">
      <option value='gmail'>GMail</option>
      <option value='yahoo'>Yahoo!</option>
      <option value='aol'>AOL</option>
      <option value='hotmail' selected="selected">Live/Hotmail</option>
    </select>
	
    <input type="hidden" name="email" value="<?php echo $user_data['email'] ;?>" />
    <input type="hidden" name="name" value="<?php echo  $user_data['name']  ;?>" />
    <input type="hidden" name="fname" value="<?php echo $user_data['fname'] ;?>" />
    <input type="hidden" name="lname" value="<?php echo $user_data['lname'] ;?>" />
	
	 </td>
  </tr>
  <tr>
    <td width="85" align="left" class="f14black"><strong>Password:</strong></td>
    <td width="144"  align="left"><input type="password" name="pass" id="pass" /></td>
    <td width="15" align="left">&nbsp;</td>
    <td width="227" align="left">&nbsp;</td>
  </tr>
  <tr>
    <td class="f14black">&nbsp;</td>
    <td height="35"><a href="#_" onClick="if(getContactsChk())ajax('contacts/sendinv.php','email_form','email_div','spin')" ><img src="images/findfriends.png" width="116" height="24" /></a> </td>
    <td><div id="spin" style="display:none"><img src="minispinner.gif"/></div></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td class="f14black">&nbsp;</td>
    <td colspan="3" align="left" class="f11">Click 'find friends' to confirm that you'd like to give PSnapshots.com access to read your email account's  contacts. You will need to enter your password, if you are not logged into your account already. Once, your contacts are read, you can select who you want to invite before proceedings.   </td>
    </tr>
  <tr>
    <td class="f14black">&nbsp;</td>
    <td colspan="3">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="4" align="center" class="f14black"><strong>Your email address book contacts will be brought in to your PersonalityStyle.com Account Contacts  </strong></td>
    </tr>
  <tr>
    <td colspan="4" align="center" class="f14black"><strong>PersonalityStyle.com dones not store the login information for these external accounts.</strong></td>
  </tr>
            </table>
</div></td>
          </tr>
        </table>
		</form>
		</td>
      </tr>
    </table>
	</div>
	
	</td>
    <td class="popuprptrgt">&nbsp;</td>
  </tr>
  <tr>
    <td class="popuplftbtm"></td>
    <td class="popuprptbtm"></td>
    <td class="popuprgtbtm"></td>
  </tr>
</table></div>
    <div class="blkimg"  id="email_reporter" style="display:none;opacity:0">
<table width="750" border="0" cellspacing="0" cellpadding="0" style="position:absolute;left:100px;top:200px;"  >
  <tr>
    <td width="16" class="popuplfttop"></td>
    <td class="popuprpttop"></td>
    <td class="popuprgttop"></td>
  </tr>
  <tr>
    <td class="popuprptlft">&nbsp;</td>
    <td class="popupcenter">
	<div style="float:right"><a href="#_" onClick="hide('email_reporter')" style="color:#000000"><b>x</b></a></div>
	<div id="email_div2">
	<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="20" class="f14black"></td>
      </tr>
      <tr>
        <td height="20" class="f12black" style="padding-left:25px;"></td>
      </tr>
      <tr>
        <td height="20">
		<form method="post" id="email_form2">
		<table width="85%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="20" class="f14black"></td>
          </tr>
          <tr>
            <td> <div style="float:left; height:1px; border-bottom:1px dotted #d6d6d6; width:80%;">&nbsp;</div></td>
          </tr>
          <tr>
            <td height="50"></td>
          </tr>
          <tr>
            <td height="50">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          </tr>
          <tr>
            <td height="25" class="f14black"></td>
          </tr>
          <tr>
            <td height="25"><div style=" margin:0 auto; width:80%; text-align:center">
			
			<table width="100%" border="0" cellspacing="2" cellpadding="0">
  <tr>
    <td width="85" align="left" class="f14black"></td>
    <td width="144" align="left"></td>
    <td width="15" align="left"><p>&nbsp;</p></td>
    <td width="227" align="left">
	<input type="hidden" name="report" value="<?php echo $split_report[2];    ?>" />
    <input type="hidden" name="email"  value="<?php echo $user_data['email'] ;?>" />
    <input type="hidden" name="name"   value="<?php echo  $user_data['name']  ;?>" />
    <input type="hidden" name="fname"  value="<?php echo $user_data['fname'] ;?>" />
    <input type="hidden" name="lname"  value="<?php echo $user_data['lname'] ;?>" />
	
	 </td>
  </tr>
  <tr>
    <td align="left" class="f14black"></td>
    <td height="35"></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td class="f14black">&nbsp;</td>
    <td height="35"><script>ajax('sendrpt.php','email_form2','email_div2','spinner')</script><!--<a href="#_" onClick="ajax('sendrpt.php','email_form2','email_div2','spinner')" ><img src="images/findfriends.png" width="116" height="24" /></a>--> </td>
    <td><img src="images/minispinner.gif" id="spinner" style="display:none" /></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td class="f14black">&nbsp;</td>
    <td colspan="3" align="left" class="f11"></td>
    </tr>
  <tr>
    <td class="f14black">&nbsp;</td>
    <td colspan="3">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="4" align="center" class="f14black"></td>
    </tr>
  <tr>
    <td colspan="4" align="center" class="f14black"></td>
  </tr>
            </table>
</div></td>
          </tr>
        </table>
		</form>
		</td>
      </tr>
    </table>
	</div>
	
	</td>
    <td class="popuprptrgt">&nbsp;</td>
  </tr>
  <tr>
    <td class="popuplftbtm"></td>
    <td class="popuprptbtm"></td>
    <td class="popuprgtbtm"></td>
  </tr>
</table></div>

   		<div class="navigation">
			<div class="smllogo"><a href="index.php"><img src="images/personality_style_com.png" width="303" height="34" /></a>
            </div>
			<ul class="nav">    			<li><a href="whatisthis.php">What&nbsp;is&nbsp;this?</a></li>
    			<li><a href="howitworks.php">How&nbsp;it&nbsp;works</a></li>
    			<li><a href="samplereports.php">Sample&nbsp;reports</a></li>
    			<li><a href="result.php?logout=log" >Logout</a></li>
			</ul>
		</div>
		<?php
if($err)
{
	echo "<div align=center><p style='margin: 100px;' ><h2>No Report is Found in database.</h2><br>
	<a href=login.php class=link3 >Click here Login</a>
	</p>
	<div>";
}
else
{

	$sql ="select * from style where style='$style'";
	$res = mysql_query($sql) or die($sql.mysql_error());
	$row = mysql_fetch_assoc($res);
	$power_image = $row['power_image']; 
	$title = $row['title'];
	
?>
		<div class="mainContainer" style="margin-top:25px;">
        <div style="float:left; width:7%; margin:0 3%;">
          <table width="55" border="0" cellspacing="7" cellpadding="0">
            <tr>
              <td height="28">&nbsp;</td>
            </tr>
            <tr></tr>
            <tr>
            
            
              <td><a href="http://twitter.com/share" class="twitter-share-button" data-count="vertical">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script></td>
            </tr>
            <tr>
              <td><a class="DiggThisButton DiggMedium" href="http://digg.com/submit?url=<?php echo SITE_URL;?>"></a></td>
            </tr>
            <tr>
              <td><a href="#" onClick="emailform.submit()"><img src="images/email.jpg" width="50" height="20" border="0" /></a></td>
            </tr>
            <tr>
              <td><!--<a href="#" onClick="feed2()"><img src="images/share.jpg" width="50" height="21" border="0" /></a>-->
             
              <div class="addthis_toolbox addthis_default_style">
   <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=xa-4ccad5b20a7f4447" class="addthis_button_compact"><img src="images/share.jpg" width="50" height="21" border="0" /></a>
				<!--<span class="addthis_separator">|</span>-->
                <!--<a class="addthis_button_preferred_1"></a>
				<a class="addthis_button_preferred_2"></a>
				<a class="addthis_button_preferred_3"></a>
				<a class="addthis_button_preferred_4"></a>-->
				</div>
    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4ccad5b20a7f4447"></script>

              
              
  
              </td>
            </tr>
          </table>
        </div>

          <div style="float:left; width:85%;margin-right:2%">
         <div style="float:left; width:100%"> <div style="float:left; width:70%; margin-right:2%;color:#759397;">
            <table width="98%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="35" valign="top" class="f24" style="font-weight:normal; color:#759397">Custom Personality Report: Free Preview Version<?php echo $us;?></td>
              </tr>
              <tr>
                <td height="25" valign="bottom"><span class="f18" style=" text-decoration:underline; color:#759397; font-weight:normal"><?php echo $title;?></span></td>
              </tr>
              <tr>
                <td>
                <?php //echo $user_data['email'] ;?>
                <?php //echo $user_data['fname'] ;?>
                <?php $sql2 = "select fname from users where user_id='$user_id'";
				      $res2 = mysql_query($sql2) or die($sql2.mysql_error());
                      $row2 = mysql_fetch_assoc($res2);
                      $fname2 = $row2['fname'];?>
                <?php echo str_replace('|',$fname2,$user_report); ?>
                <br>
                  Does this profile seem pretty accurate? We really hope so, since we’re using a system that’s been 
                  refinined for over 80 years! Find out what other information we’ve determined about you in the 
                  complete report below <!--or <a href="#" class="link3">click here if you are interested in getting a custom Hiring Edge Behavioral 
                Report to append to your resume.<br />
                  </a>-->
                <div class="f11" style="margin-top:8px ; height:56px; width:100%" >
                <script src="http://connect.facebook.net/en_US/all.js#xfbml=1"></script>
                <fb:like href="http://PersonalityStyle.com"></fb:like>
                </div><br />
                <a href="#" class="link3">                  </a></td>
              </tr>
            </table>
          </div>
          <div style="float:left; font-size:12px; width:21%; padding:3%; border:1px solid #759397;"><img src="powerimages/<?php echo $power_image ;?>" width="166" height="168" /><br />
            <strong style="text-decoration:underline;"><?php echo $title;?></strong><br /> 
          <p style="line-height:16px;">
          <?php echo $fname2;?><?php echo $split_report[2]; ?>
          <br /><br />
          <a href="#_" onClick="feed2()"><img src="images/fb_share.jpg" width="59" height="18" /></a>

          </a></p>
</div></div>


<div style="float:left; width:100%">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td height="35" valign="top" class="f24" style="font-weight:normal; color:#759397">Compare &amp; Invite!</td>
            </tr>
            <tr>
              <td height="25" valign="bottom"><span class="f18" style=" text-decoration:underline; color:#759397; font-weight:normal"><?php //echo $title;?></span></td>
            </tr>
            <tr>
              <td><!--<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;<a href="#" class="linkteal" onClick="hide('email_inviter');show('invite');">Compare results with your Facebook Friends!</a><br />-->
                <img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;<a href="#" onClick="alertbox('email_reporter');" class="linkteal">Click here to email your report. </a><br />
              <img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;<a href="#" onClick="document.getElementById('email_inviter').style.display='block'";document.getElementById('email_inviter').innerHTML=email_inviter;hide('invite');" class="linkteal">One-Click Invites: Skim and invite your Webmail &amp; IM </a>contacts (Hotmail, Yahoo, Gmail, GTalk, AIM, MSM &amp; more)<a href="#" class="link3">                  </a></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td><img src="images/liketext.png" width="780" height="29" /></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td><table width="65%" border="0" cellspacing="10" cellpadding="0" style="border:1px solid #cecccc;">
                <tr>
                  <td width="158"><img src="images/img2.jpg" width="158" height="202" /></td>
                  <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td><!--<strong>Included in your report:</strong><br />
<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;Your personality strengths/limitations<br />
<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;Your greatest fears<br />
<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;Your motivational goals<br />
<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;Your leadership traits<br />
<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;Your reaction to pressure<br />
<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;How you communicate with others<br />
<img src="images/arrow.png" width="5" height="10" align="absmiddle" />&nbsp;&nbsp;POWER CHART of your personality-->
						<img src="design/resultpagetext.png"></td>
                    </tr>
                    <tr>
                      <td style="background-color:#f3f3f3; border:1px solid #a8a5a5"><table width="100%" border="0" cellspacing="10" cellpadding="0">
                        <tr>
                          <td align="right" class="f13" style="color:#878989"><!--Product Description:<br />
                            <strong>Complete 7 Page</strong><br />
                            <strong>Personality Awareness </strong><strong>Report</strong>-->
                            <img src="design/resultpagetext2.png"></td>
                          <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td height="24" colspan="2" align="center" bgcolor="#fea900" style="color:#FFF">Our Price: <?php echo formatMoney($mount); ?></td>
                            </tr>
                            <tr>
                              <td></td>
                              <td height="5"></td>
                            </tr>
                            <tr>
                              
                              <td colspan="2" align="center"><a href="cart.php?id=<?php echo $assessment_id;?>"><img src="images/addtocart.jpg" width="81" height="20" /></a></td>
                            </tr>
                          </table></td>
                        </tr>
                      </table></td>
                    </tr>
                  </table></td>
                </tr>
              </table></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td style="padding-left:58px"><img src="images/visa.png" width="40" height="25" />&nbsp;<img src="images/mastercard.png" width="40" height="25" />&nbsp;<img src="images/cardimg.jpg" width="28" height="23" />&nbsp;&nbsp;&nbsp;<img src="images/discovercard.jpg" width="37" height="23" />&nbsp;&nbsp;&nbsp;
             <img src="images/paypal.jpg"  width="213" height="37" /></td>
            </tr>
          </table>
          
</div>

          </div>
<?php
}
?>		  
        
        </div>
		<div class="footer">
        <div class="footerdiv">
		  <ul class="navfooter">                      
		    <li><a href="commotion.php">CommotionPublishing.com</a></li>
    			<li><a href="twitter.php">Twitter</a></li>
    			<li><a href="facebook1.php">Facebook</a></li>
    			<li><a href="privacy.php">Privacy</a></li>
                <li><a href="copyright.php">Copyright</a></li>
                <li><a href="links.php">Links</a></li>
		  </ul>
         </div> 
         
         <div class="tex-cnter flotlft f13" style="width:100%">All content copyright 2010</div>
	  </div>
        
  </div>

	</div>


<div>
<table style="position:absolute;left:100px;top:200px;display:none" id="invite"  cellspacing="0" cellpadding="0" >
  <tr>
    <td width="16" class="popuplfttop"></td>
    <td class="popuprpttop"></td>
    <td class="popuprgttop"></td>
  </tr>
  <tr>
    <td class="popuprptlft">&nbsp;</td>
    <td class="popupcenter" >
	<div style="float:right"><a href="#_" onClick="hide('invite')"><b style="color:#000000"><b>x</b></a></div>

<fb:serverfbml width="580" >
<script type="text/fbml" >
    <fb:fbml>
        <fb:request-form
                action="<?php echo SITE_URL.'result.php?'.$_SERVER['QUERY_STRING']; ?>"
                method="POST"
                invite="true"
                type="Personlity Style"
                content="Personlity Style  <fb:req-choice url='<?php echo SITE_URL;?>'
                        label='<?php echo htmlspecialchars("Accept ",ENT_QUOTES); ?>' />" >
                <fb:multi-friend-selector
                        showborder="true"
                        actiontext="Invite your friends"
                        exclude_ids=""
                        cols="3"
                        rows="3"
                />
        </fb:request-form>  
    </fb:fbml>
</script>
</fb:serverfbml>

    </td>
    <td class="popuprptrgt">&nbsp;</td>
  </tr>
  <tr>
    <td class="popuplftbtm"></td>
    <td class="popuprptbtm"></td>
    <td class="popuprgtbtm"></td>
  </tr>
</table>
</div>
<!--###############################################################################-->


<!--################################################################################
-->
<script>

function getContactsChk(){

user_name = getValue('user_name');
pass = getValue('pass');

if(pass=='' || user_name==''  )
{
	alert('Please Enter Your Email and Password.');
	return false ;
}
else
{
	return true ;
}

}

email_inviter = document.getElementById('email_inviter').innerHTML;

</script>


<form name="emailform" method="post" target="myiframe" action="reportemail.php">
<input type="hidden" name="email" value="<?php echo $user_data['email'] ;?>">
<input type="hidden" name="message" value="<?php echo urlencode(str_replace('|',$user_data['fname'],$user_report)); ?>">
</form>

	<script src="http://connect.facebook.net/en_US/all.js"></script>
        <script>
       FB.init({appId: '<?php echo FACEBOOK_APP_ID; ?>', status: true,
               cookie: true, xfbml: true});
      FB.Event.subscribe('auth.login', function(response) {
        window.location.reload();
      });
	  
	 function fblogout(){
	  FB.Connect.logoutAndRedirect("index.php");


	 }
	 
	  function fblogin()
	  {
	  FB.login(function(response) {
	  if (response.session) {
		if (response.perms) {
		  // user is logged in and granted some permissions.
		  // perms is a comma separated list of granted permissions
		} else {
		  // user is logged in, but did not grant any permissions
		}
	  } else {
		// user is not logged in
	  }
	}, {perms:'read_stream,publish_stream,offline_access,email'});
	  }
      
	
	function feed2()
	{	  
		 FB.ui(
		   {
			 method: 'stream.publish',
			 message: '',
			 attachment: {
			   name: 'PersonalityStyle.com',
			   caption: '<?php echo "<b>$title</b>";?>',
			   description: (
				 '<?php echo $user_data['fname'];?><?php echo $split_report[2];?>'
			   ),
			   'media': [{'type':'image','src':'<?php echo SITE_URL;?>powerimages/<?php echo $power_image ;?>', 'href':'<?php echo SITE_URL;?>'}]
			   ,
			   href: '<?php echo SITE_URL;?>'
			 },
			 action_links: [
			   { text: 'PersonalityStyle.com', href: '<?php echo SITE_URL;?>' }
			 ],
			 user_message_prompt: 'Share your thoughts about Connect'
		   },
		   function(response) {
			 if (response && response.post_id) {
			  // alert('Post was published.');
			 } else {
			 //  alert('Post was not published.');
			 }
		   }
		 );
	} 
      </script>
      <script>
function alertbox(box)
{
 //setInnerHTML('txt'+box,txt);
 show(box);
 fade(box);
 //setTimeout(function(){hide('box'+box);},8000);
}
</script>  
<div id="fb-root"></div>
<iframe src="loginchk.php" name="myiframe" style="display:none" />