//Navigation Hamburger menu
const link = document.getElementById('navigation__link');
const burger = document.getElementById('navigation__text');
const ul = document.querySelector('.navigation__menu');

link.addEventListener('click', (e) => {
	e.preventDefault();
	burger.classList.toggle('nav-toggle-open');
	ul.classList.toggle('nav-toggle-open');

});

//accordion
const $accordionTriggers = $('.accordion__intro');

$accordionTriggers.on('click', function() {
	const $this = $(this);
	const $parent = $this.parents('.accordion');
	const $content = $parent.find('.accordion__content');

	$accordionTriggers.each(function() {
		if (this === $this[0]) {
			$content.stop().slideToggle();
		} else {
			$(this).parents('.accordion').find('.accordion__content').stop().slideUp();
		}
	});
});

//modal popup
const modalBtn = document.querySelector('.btn-modal');
const modalBg = document.querySelector('.modal');
const close = document.querySelector('.modal__close');

if (modalBg !== null) {
	modalBtn.addEventListener('click', () => {
		modalBg.classList.add('modal--show');
	});
	close.addEventListener('click', () => {
		modalBg.classList.remove('modal--show');
	});
	window.addEventListener('keydown', (e) => {
		if (e.key === 'Escape') {
			modalBg.classList.remove('modal--show');
		}
	});
}

