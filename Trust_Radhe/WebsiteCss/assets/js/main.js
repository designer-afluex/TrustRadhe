(function ($) {
"use strict";

// meanmenu
$('#mobile-menu-2').meanmenu({
	meanMenuContainer: '.mobile-menu-2',
	meanScreenWidth: "1199"
});
$('#mobile-menu').meanmenu({
	meanMenuContainer: '.mobile-menu',
	meanScreenWidth: "991"
});

$(window).on('scroll', function () {
	var scroll = $(window).scrollTop();
	if (scroll < 200) {
		$(".header-sticky").removeClass("sticky");
	} else {
		$(".header-sticky").addClass("sticky");
	}
});

//mobile side menu
$('.side-toggle').on('click', function () {
	$('.side-info').addClass('info-open');
	$('.offcanvas-overlay').addClass('overlay-open');
})

$('.side-info-close,.offcanvas-overlay').on('click', function () {
	$('.side-info').removeClass('info-open');
	$('.offcanvas-overlay').removeClass('overlay-open');
});

// Search Header
$('.ns-header-action-search').on('click', function() {
	$('body').addClass('search-active');
})
$(".ba-search-popup .ba-color-layer").on("click", function () {
	$("body").removeClass("search-active");
});

function sliderActive() {
	/*------------------------------------
	Slider
	--------------------------------------*/
	if (jQuery(".slider-active").length > 0) {
		let sliderActive1 = '.slider-active';
		let sliderInit1 = new Swiper(sliderActive1, {
			// Optional parameters
			slidesPerView: 1,
			rtl: false,
			slidesPerColumn: 1,
			paginationClickable: true,
			loop: true,

			autoplay: {
				delay: 5000,
			},

			// If we need pagination
	        pagination: {
				el: ".cinkes-swiper-pagination",
				clickable: true,
			},

			// Navigation arrows
			navigation: {
				nextEl: '.slider-swiper-next',
				prevEl: '.slider-swiper-prev',
			},

			a11y: false
		});

		function animated_swiper(selector, init) {
			let animated = function animated() {
				$(selector + ' [data-animation]').each(function () {
					let anim = $(this).data('animation');
					let delay = $(this).data('delay');
					let duration = $(this).data('duration');

					$(this).removeClass('anim' + anim)
						.addClass(anim + ' animated')
						.css({
							webkitAnimationDelay: delay,
							animationDelay: delay,
							webkitAnimationDuration: duration,
							animationDuration: duration
						})
						.one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
							$(this).removeClass(anim + ' animated');
						});
				});
			};
			animated();
			// Make animated when slide change
			init.on('slideChange', function () {
				$(sliderActive1 + ' [data-animation]').removeClass('animated');
			});
			init.on('slideChange', animated);
		}

		animated_swiper(sliderActive1, sliderInit1);
	}}

/* magnificPopup img view */
$('.popup-image').magnificPopup({
	type: 'image',
	gallery: {
	  enabled: true
	}
});
/* magnificPopup video view */
$('.popup-video').magnificPopup({
	type: 'iframe'
});

$('.has-nice-select').niceSelect();
// data background
$("[data-background]").each(function(){
	$(this).css("background-image","url("+$(this).attr("data-background") + ") ")
})
// data width
$("[data-width]").each(function(){
	$(this).css("width",$(this).attr("data-width"))
})
// data background color
$("[data-bg-color]").each(function(){
	$(this).css("background-color",$(this).attr("data-bg-color"))
})
//for menu active class
$('.portfolio_nav button').on('click', function(event) {
	$(this).siblings('.active').removeClass('active');
	$(this).addClass('active');
	event.preventDefault();
});

// scrollToTop
$.scrollUp({
	scrollName: 'scrollUp', // Element ID
	topDistance: '300', // Distance from top before showing element (px)
	topSpeed: 300, // Speed back to top (ms)
	animation: 'fade', // Fade, slide, none
	animationInSpeed: 200, // Animation in speed (ms)
	animationOutSpeed: 200, // Animation out speed (ms)
	scrollText: '<i class="icofont icofont-long-arrow-up"></i>', // Text for element
	activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
});

// service active 
const serviceActive = new Swiper(".service-active", {
	slidesPerView: 3,
	spaceBetween: 30,
	loop: true,
	navigation: {
		nextEl: ".ns-service-prev",
		prevEl: ".ns-service-next",
		},
		pagination: {
			el: ".ns-service-pagination",
			clickable: true,
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 3,
		},
		1200: {
			slidesPerView: 3
		}
	}
});

// service active 2
const serviceActiveTwo = new Swiper(".service-active-2", {
	slidesPerView: 3,
	spaceBetween: 30,
	loop: true,
		pagination: {
			el: ".ns-service-pagination-2",
			clickable: true,
		},
		navigation: {
			nextEl: '.ns-service-swiper-next-6',
			prevEl: '.ns-service-swiper-prev-6',
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 3,
		},
		1200: {
			slidesPerView: 3
		}
	}
});

// team active 
const teamActive = new Swiper(".team-active", {
	slidesPerView: 3,
	spaceBetween: 30,
	loop: true,
		pagination: {
			el: ".ns-team-pagination",
			clickable: true,
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 3,
		},
		1200: {
			slidesPerView: 3
		}
	}
});
// team active 4
const teamActiveFour = new Swiper(".team-active-4", {
	slidesPerView: 4,
	spaceBetween: 30,
	loop: true,
		pagination: {
			el: ".ns-team-pagination",
			clickable: true,
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 2,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 3,
		},
		1200: {
			slidesPerView: 4
		}
	}
});

// team active 5
const teamActiveFive = new Swiper(".team-active-5", {
	slidesPerView: 3,
	spaceBetween: 30,
	loop: true,
		navigation: {
			nextEl: '.ns-team-swiper-next',
			prevEl: '.ns-team-swiper-prev',
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 3,
		},
		1200: {
			slidesPerView: 3
		}
	}
});


// blog list
const blogList = new Swiper(".blog-list-active", {
	slidesPerView: 1,
	spaceBetween: 0,
	loop: true,
	speed: 1500,
		navigation: {
			nextEl: '.ns-blog-list-swiper-next',
			prevEl: '.ns-blog-list-swiper-prev',
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 1,
		},
		992: {
			slidesPerView: 1
		},
		1200: {
			slidesPerView: 1
		}
	}
});

// project active 
const projectActive = new Swiper(".project-active", {
	slidesPerView: 4,
	spaceBetween: 30,
	loop: true,
		pagination: {
			el: ".ns-project-pagination",
			clickable: true,
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 2,
		},
		1200: {
			slidesPerView: 3
		},
		1500: {
			slidesPerView: 4
		}
	}
});


// project active 4
const projectActiveFour = new Swiper(".ns-project-active-4", {
	slidesPerView: 4,
	spaceBetween: 30,
	loop: true,
	navigation: {
		nextEl: '.ns-project-swiper-next-4',
		prevEl: '.ns-project-swiper-prev-4',
	},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 3,
		},
		1200: {
			slidesPerView: 3
		},
		1500: {
			slidesPerView: 3
		}
	}
});

// project active 5
const projectActiveFive = new Swiper(".project-active-5", {
	slidesPerView: 4,
	spaceBetween: 0,
	loop: true,
	grabCursor: true,
	observer: true,
	observeParents: true,
	navigation: {
		nextEl: '.ns-project-swiper-next-5',
		prevEl: '.ns-project-swiper-prev-5',
	},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 2,
		},
		1200: {
			slidesPerView: 3
		},
		1400: {
			slidesPerView: 3
		},
		1600: {
			slidesPerView: 4
		}
	}
});


// blog active 
const blogActive = new Swiper(".blog-active", {
	slidesPerView: 3,
	spaceBetween: 30,
	loop: true,
		pagination: {
			el: ".ns-blog-pagination",
			clickable: true,
		},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 2,
		},
		992: {
			slidesPerView: 3,
		},
		1200: {
			slidesPerView: 3
		}
	}
});

// brand active 
const brandActive = new Swiper(".brand-active", {
	slidesPerView: 5,
	spaceBetween: 30,
	loop: true,
	speed: 2500,
	autoplay: {
		delay: 3000,
	},
		breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 2,
		},
		768: {
			slidesPerView: 3,
		},
		992: {
			slidesPerView: 4,
		},
		1200: {
			slidesPerView: 5
		}
	}
});


// testimonial active 
const testimonialActive = new Swiper('.testimonial-active', {
	spaceBetween: 0,
	loop: true,
	loopedSlides: 1,
	slidesPerView: 1,
	allowTouchMove:false,
	speed: 2500,
	pagination: {
		el: ".ns-testimonial-pagination",
		clickable: true,
	},
	breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 1,
		},
		992: {
			slidesPerView: 1,
		},
		1199: {
			slidesPerView: 1
		}
	}
});
const testimonialThumbs = new Swiper('.testimonial-thumb', {
	centeredSlides: true,
	slidesPerView: 1,
	touchRatio: 0.2,
	slideToClickedSlide: true,
	loop: true,
	speed: 800,
	effect: 'fade',
	fadeEffect: {
	  crossFade: true
	},
	breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 1,
		},
		992: {
			slidesPerView: 1,
		},
		1199: {
			slidesPerView: 1
		}
	}
});
testimonialActive.controller.control = testimonialThumbs;
testimonialThumbs.controller.control = testimonialActive;


// testimonial active 5

const slider_thumb = new Swiper('.testimonial-thumb-5', {
	loop: true,
	spaceBetween: 10,
	slidesPerView: 3,
	freeMode: false,
	watchSlidesProgress: true,
	breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
			
		},
		768: {
			slidesPerView: 3,
		},
		993: {
			slidesPerView: 3,
		},
	}
});
const slider3 = new Swiper('.testimonial-active-5', {
	loop: true,
	spaceBetween: 30,
	rtl: false,
	slidesPerView: 1,

	pagination: {
		el: ".ns-testimonial-pagination",
		clickable: true,
	},
	thumbs: {
		swiper: slider_thumb,
	},
});


// testimonial active 4
const testimonialActiveFour = new Swiper('.testimonial-active-4', {
	spaceBetween: 30,
	loop: true,
	loopedSlides: 1,
	slidesPerView: 1,
	// allowTouchMove:false,
	speed: 2500,
	pagination: {
		el: ".ns-testimonial-pagination",
		clickable: true,
	},
	breakpoints: {
		0: {
			slidesPerView: 1,
			},
		576: {
			slidesPerView: 1,
		},
		768: {
			slidesPerView: 1,
		},
		992: {
			slidesPerView: 1,
		},
		1199: {
			slidesPerView: 1
		}
	}
});

//   odometer
$('.about_count').appear(function (e) {
	var odo = $(".about_count");
	odo.each(function () {
		var countNumber = $(this).attr("data-count");
		$(this).html(countNumber);
	});
});

//   odometer 2
$('.counter_count').appear(function (e) {
	var odo = $(".counter_count");
	odo.each(function () {
		var countNumber = $(this).attr("data-count");
		$(this).html(countNumber);
	});
});

// WOW active
new WOW().init();

sliderActive();

})(jQuery);