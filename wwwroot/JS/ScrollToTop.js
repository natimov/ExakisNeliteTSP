window.scrollHelper = {
    scrollToTop: function () {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    },
    toggleButtonVisibility: function () {
        const btn = document.querySelector('.scroll-to-top');
        window.addEventListener('scroll', () => {
            if (window.scrollY > 300) {
                btn.style.display = 'flex';
            } else {
                btn.style.display = 'none';
            }
        });
    }
};
