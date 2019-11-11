/* JS Document */

/******************************

[Table of Contents]

1. Vars and Inits
2. Set Header
3. Init Menu
4. Init Google Map


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
	initGoogleMap();

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

	4. Init Google Map

	*/

	function initGoogleMap()
	{
		var myLatlng = new google.maps.LatLng(36.131433, -5.353998);
    	var mapOptions = 
    	{
    		center: myLatlng,
	       	zoom: 17,
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
    	

    	// Use an image for a marker
		// var image = 'images/map_marker.png';
		var image = 
		{
			url:'images/map_marker.png',
			size: new google.maps.Size(201, 195),
			anchor: new google.maps.Point(0, 166) //setting the anchor for larger icons
		};

		var imageSmall =
		{
			url:'images/map_marker_small.png',
			size: new google.maps.Size(80, 78),
			anchor: new google.maps.Point(0, 66) //setting the anchor for larger icons
		};

		var marker = new google.maps.Marker(
		{
			position: new google.maps.LatLng(36.131433, -5.353998),
			map: map
		});

		if($(window).width() < 720)
    	{
    		map.panBy(100, 0);
    		marker.setIcon(imageSmall);
    	}
    	else
    	{
    		map.panBy(250, 0);
    		marker.setIcon(image);
    	}

		// Re-center map after window resize
		google.maps.event.addDomListener(window, 'resize', function()
		{
			setTimeout(function()
			{
				google.maps.event.trigger(map, "resize");
				map.setCenter(myLatlng);
				if($(window).width() < 720)
		    	{
		    		map.panBy(100, 0);
		    		marker.setIcon(imageSmall);
		    	}
		    	else
		    	{
		    		map.panBy(250, 0);
		    		marker.setIcon(image);
		    	}
			}, 1400);
		});
	}
});