<?php
if($_POST['ajax']==1)
{
include '../config.php';
include '../includes/memcache.php';
$cache = new cache;
$cache->temp_path='../tmp/';
  
$page_id = isset($_REQUEST['page_id'])?$_REQUEST['page_id']:0;

	if($_POST['save']==1)
	{
		$presentation =  addslashes(stripslashes(str_replace('rm-css-edit bg','rm-css-edit SafariBgFix bg',$_REQUEST['presentation'])));	
		//echo 'here';
		
		$sql = "select id from user_fbpages  where page_id='$page_id'";
		$res =  mysql_query($sql) or die($sql.mysql_error());
		if(mysql_num_rows($res)>0)
		{
			$sql = "update user_fbpages set presentation ='".$presentation."' , template_id='{$_REQUEST['template_id']}'  where page_id='$page_id'";
		}
		else
		{
			$sql = "insert into user_fbpages set height='2500' , presentation='".$presentation."' , page_id='$page_id' , user_id='{$_REQUEST['user_id']}' , template_type='customize' , template_id='{$_REQUEST['template_id']}' ";
		}
		$res =  mysql_query($sql) or die($sql.mysql_error());
		
		$cache->remove(md5('pres'.$page_id));
		if($page_id=='164715573572272' or true)
		{
			//echo 'here';
			$imagesName=getImagesFromString2(stripslashes($presentation));
			//print_r($imagesName);
			remove_directory('/ebs/var/www/vhosts/totalsocialmediasyst.com/httpdocs/templateapp/template2/upload/'.$page_id,$imagesName);
		}
	}
	
	
	if($_POST['utube']==1)
	{
		$utubevid = isset($_REQUEST['utubevid'])?$_REQUEST['utubevid']:0;
		$x = isset($_REQUEST['x'])?$_REQUEST['x']:367;
		$y = isset($_REQUEST['y'])?$_REQUEST['y']:223;	
	?>
	<fb:swf waitforclick='false' imgsrc='<?php echo SITE_URL;?>playimgbig2.php?vid=<?php echo $utubevid;?>' swfsrc='http://www.youtube.com/v/<?php echo $utubevid;?>&autoplay=0&hl=en_US&fs=1&rel=0' width="<?php echo $x;?>" height="<?php echo $y;?>" border="0" align="top" />
	<?php
	}

}


function remove_directory($directory,$array,$empty=true)
{
   /* if(substr($directory,-1) == '/')
    {
        $directory = substr($directory,0,-1);
    }
    if(!file_exists($directory) || !is_dir($directory))
    {
        return FALSE;
    }elseif(is_readable($directory))
    {
        $handle = opendir($directory);
        while (FALSE !== ($item = readdir($handle)))
        {
            if($item != '.' && $item != '..')
            {
                $path = $directory.'/'.$item;
				foreach ($array as $key => $val) {
				$save_arr[$key] = $directory.'/'.$val;
				}
				 
                if(is_dir($path)) 
                {
                   // recursive_remove_directory($path);
                }else{
					//echo "Deleting $path <br>\n";
                    if(!in_array($path,$save_arr))unlink($path);
                }
            }
        }
        closedir($handle);
        if($empty == FALSE)
        {
            if(!rmdir($directory))
            {
                return FALSE;
            }
			//else
			//echo "Deleting $directory <br>\n";
        }
    }*/
    return TRUE;
}

function getImagesFromString2($string='')
{
//	preg_match_all('~background:\s*url\([\'"]?([^)]+)[\'"]?\)\s+[.]*[^;]*;~i', $string, $reg);
//	preg_match_all('~background:\s*url\([\'"]?([^)]+\.(?:jpe?g|gif|png))~i', $string, $reg);
	preg_match_all('~[background:\s*url|background-image]\s*\([\'"]?([^)]+\.(?:jpe?g|gif|png))~i', $string, $reg);

	foreach ($reg[1] as $val)
	{
		$arr[] = basename($val);
	}
	return $arr ;
}

function getImagesFromString($string='')
{
	
$document = new DOMDocument();
if($string)
{
    libxml_use_internal_errors(true);
    $document->loadHTML($string);
    libxml_clear_errors();
}

$images = array();

foreach($document->getElementsByTagName('img') as $img)
{
    
	$images[]=basename($img->getAttribute('src'));
	// Extract what we want
    /*
	$image = array
    (
        'src' => basename($img->getAttribute('src')),
    );
   
    // Skip images without src
    if( ! $image['src'])
        continue;

    // Add to collection. Use src as key to prevent duplicates.
    $images[$image['src']] = $image;
	*/
}
return  array_values($images);
}


