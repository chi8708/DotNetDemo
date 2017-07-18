$(document).ready(function(){	
	$(".gw_nav ul li").hover(function(){
		if($(this).offset().left==$(".gw_nav").offset().left){ return false;}
		$(".gw_nav_po").css("display","block")
					   .css("left",$(this).offset().left-$(".gw_nav").offset().left+50+"px")
					   .css("top",33+"px")
		},function(){
			$(".gw_nav_po").css("display","none")
			})
	$(".gw_nav ul li").eq(1).hover(function(){
		$(".gw_navpro_li").css("left",0+"px")
		$(".gw_navpro_li").stop(true,true).show(150)
		$(".gw_navpro_li").hover(function(){ return;},function(){
			$(".gw_navpro_li").stop(true,true).hide(150)
			})
		},function(){
			$("body").click(function(e){
					var o = $(".gw_navpro_li");
					if(e.pageX > o.offset().left && e.pageX < (o.offset().left + o.width()) && e.pageY >　o.offset().top && e.pageY < (o.offset().top + o.height()) ){
						
						}
						else{
							$(".gw_navpro_li").stop(true,true).hide(150)
							}
				})
			})
	
	//cust_flash_s();	
	$(".gw_cust_flash_s ul li").eq(0).css("background","#ff9000");
	GW_FIX_TIMER = setInterval("cust_flash()",5000)//设置轮播时间
	head_faq();
	
	$(".gw_jj_list").find("li").hover(function(){
		$(".gw_jj_show").animate({"margin-left":0-741*$(this).index()+"px"},400)
		})
	
})

window.onresize=function(){head_faq();}
function head_faq(){
	
	var id = $(".gw_head_link_ico").eq(0);
	$(".gw_head_link_ico").eq(0).hover(function(){
			$(".head_faq").css("top",id.offset().top+id.height()+"px")
			$(".head_faq").css("left",960 - $(".head_faq").width()+"px")
			$(".head_faq").show();
		},function(){
			$(".head_faq").hover(function(){
				$(".head_faq").show();
				},function(){
				$(".head_faq").hide();
				return;
				})
			$(".head_faq").hide();
			})
}

var po_now=0;
var cust_step=1000;
var GW_FIX_TIMER;
function cust_flash(){
	if(po_now == $(".gw_zzsc_sele").length-1){po_now=0;}
	else{po_now++;}
	cust_flash_scroll(po_now);
	
}
function cust_flash_scroll(n){
	$(".fix_flash").stop(true,false).animate({left:0-n*$(".fix_flash li").width()+"px"},500)
	$(".gw_zzsc_sele").removeClass("gw_zzsc_over")
	$(".gw_zzsc_sele").eq(n).addClass("gw_zzsc_over")
	//var theleft = $(".gw_zzsc_sele").eq(n).offset().left - 0.5*$("body").width() + 550 - 48 + 5;
	var theleft =  122 + 26*n ;
	var str = "<img src='"+$(".fix_flash").find("li").eq(n).attr("imglink")+"' style='margin-left:-27px;' />"
	$(".gw_zzsc_flash_v div").html(str)
	$(".gw_zzsc_flash_v").stop(true,true).animate({"left":theleft+"px"},500)
	
	}
function cust_flash_s(){
	$(".gw_zzsc_sele").click(function(){
			po_now=parseInt($(this).attr("number"));
			cust_flash_scroll(po_now);
		})
}
function GW_FIX_scroll(obj){
	clearTimeout(GW_FIX_TIMER);
	cust_flash_scroll($(obj).attr("number"));
	GW_FIX_TIMER = setInterval("cust_flash()",3000)
}