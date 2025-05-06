document.addEventListener('DOMContentLoaded', function() {
    // Initialize cart from localStorage
    initializeCart();
    
    function initializeCart() {
        // Check if there's a cart in localStorage
        if (!localStorage.getItem('cart')) {
            localStorage.setItem('cart', JSON.stringify([]));
        }
        
        updateCartCount();
    }
    
    function updateCartCount() {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        const cartCount = document.getElementById('cart-count');
        if (cartCount) {
            cartCount.textContent = cart.length;
        }
    }
    
    // Add functionality to update price as weight changes
    const weightInputs = document.querySelectorAll('.weight-input');
    if (weightInputs) {
        weightInputs.forEach(input => {
            input.addEventListener('input', function() {
                const fishId = this.getAttribute('data-fish-id');
                const price = parseFloat(this.getAttribute('data-price'));
                const weight = parseFloat(this.value);
                const totalPrice = (price * weight).toFixed(2);
                document.getElementById(`total-price-${fishId}`).textContent = `$${totalPrice}`;
            });
        });
    }
    
    // Enable Bootstrap dropdowns
    var dropdownElementList = [].slice.call(document.querySelectorAll('.dropdown-toggle'));
    var dropdownList = dropdownElementList.map(function (dropdownToggleEl) {
        return new bootstrap.Dropdown(dropdownToggleEl);
    });
    
    // Slideshow functionality - MOVED INSIDE the DOMContentLoaded event
    const slides = document.querySelectorAll('.slide');
    if (slides.length > 0) {  // Add check to verify slides exist
        let currentSlide = 0;
        
        function nextSlide() {
            slides[currentSlide].classList.remove('active');
            currentSlide = (currentSlide + 1) % slides.length;
            slides[currentSlide].classList.add('active');
        }
        
        // Change slide every 5 seconds
        setInterval(nextSlide, 5000);
        
        // For debugging
        console.log("Slideshow initialized with " + slides.length + " slides");
    } else {
        console.log("No slides found for slideshow");
    }
});