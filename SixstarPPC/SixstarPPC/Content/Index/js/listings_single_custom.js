/* JS Document */

/******************************

[Table of Contents]

1. Vars and Inits
2. Set Header
3. Init Menu
4. Init Listing Slider
5. Init Popup
6. Init Google Map
7. Init Search Features


******************************/

$(document).ready(function()
{
	"use strict";

	/* 

	1. Vars and Inits

	*/

	var menuActive = false;
	var header = $('.header');
	var map;

	setHeader();

	$(window).on('resize', function()
	{
		setHeader();
	});

	$(document).on('scroll', function()
	{
		setHeader();
	});

	initMenu();
	initListingSlider();
	initPopup();
	initGoogleMap();
	initSearchFeatures();

	/* 

	2. Set Header

	*/

	function setHeader()
	{
		if(window.innerWidth < 992)
		{
			if($(window).scrollTop() > 100)
			{
				header.addClass('scrolled');
			}
			else
			{
				header.removeClass('scrolled');
			}
		}
		else
		{
			if($(window).scrollTop() > 100)
			{
				header.addClass('scrolled');
			}
			else
			{
				header.removeClass('scrolled');
			}
		}
		if(window.innerWidth > 991 && menuActive)
		{
			closeMenu();
		}
	}

	/* 

	3. Init Menu

	*/

	function initMenu()
	{
		if($('.menu').length)
		{
			var menu = $('.menu');
			var hamb = $('.hamburger');

			hamb.on('click', function()
			{
				if(menuActive)
				{
					closeMenu();
				}
				else
				{
					openMenu();

					$(document).one('click', function cls(e)
					{
						if($(e.target).hasClass('menu_mm'))
						{
							$(document).one('click', cls);
						}
						else
						{
							closeMenu();
						}
					});
				}
			});
		}
	}

	function closeMenu()
	{
		var menu = $('.menu');
		menu.removeClass('active');
		menuActive = false;
		menu.css('max-height', "0px");
	}

	function openMenu()
	{
		var menu = $('.menu');
		menu.addClass('active');
		menuActive = true;
		menu.css('max-height', menu.prop('scrollHeight') + "px");
	}

	/* 

	4. Init Listing Slider

	*/

	function initListingSlider()
	{
		if($('.listing_slider').length)
		{
			var listingSlider = $('.listing_slider');

			listingSlider.owlCarousel(
			{
				items:1,
				loop:true,
				autoplay:true,
				nav:false,
				dots:false,
				smartSpeed:1200
			});

			//Handle slider navigation
			if($('.listing_slider_prev').length)
			{
				var listingPrev = $('.listing_slider_prev');
				listingPrev.on('click', function()
				{
					listingSlider.trigger('prev.owl.carousel');
				});
			}

			if($('.listing_slider_next').length)
			{
				var listingNext = $('.listing_slider_next');
				listingNext.on('click', function()
				{
					listingSlider.trigger('next.owl.carousel');
				});
			}
		}
	}

	/* 

	5. Init Popup

	*/

	function initPopup()
	{
		$('.video').magnificPopup({
          disableOn: 700,
          type: 'iframe',
          mainClass: 'mfp-fade',
          removalDelay: 160,
          preloader: false,

          fixedContentPos: false
        });
	}

	/* 

	6. Init Google Map

	*/

	function initGoogleMap()
	{
		var myLatlng = new google.maps.LatLng(36.287327,-5.278641);
    	var mapOptions = 
    	{
    		center: myLatlng,
	       	zoom: 16,
			mapTypeId: google.maps.MapTypeId.ROADMAP,
			draggable: true,
			scrollwheel: false,
			zoomControl: true,
			zoomControlOptions:
			{
				position: google.maps.ControlPosition.RIGHT_CENTER
			},
			mapTypeControl: false,
			scaleControl: false,
			streetViewControl: false,
			rotateControl: false,
			fullscreenControl: true,
			styles:[]
    	}

    	// Initialize a map with options
    	map = new google.maps.Map(document.getElementById('map'), mapOptions);

		// Re-center map after window resize
		google.maps.event.addDomListener(window, 'resize', function()
		{
			setTimeout(function()
			{
				google.maps.event.trigger(map, "resize");
				map.setCenter(myLatlng);
			}, 1400);
		});
	}

	/* 

	7. Init Search Features

	*/

	function initSearchFeatures()
	{
		if($('.search_features').length)
		{
			var triggerEle = $('.search_features_trigger');
			var ele = $('.search_features');

			triggerEle.on('click', function(e)
			{
				e.preventDefault();
				triggerEle.toggleClass('active');
				ele.toggleClass('active');

				var panel = ele;
				var panelH = ele.prop('scrollHeight') + "px";
				
				if(panel.css('max-height') == "0px")
				{
					panel.css('max-height', panel.prop('scrollHeight') + "px");
				}
				else
				{
					panel.css('max-height', "0px");
				} 
			});
		}
	}
});