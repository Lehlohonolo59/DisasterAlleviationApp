// ==========================================
// Disaster Alleviation Foundation - Create Donation Script
// Handles dynamic form field toggling based on user selection
// ==========================================

document.addEventListener("DOMContentLoaded", function () {
    // Grab references to key form elements
    const donationTypeSelect = document.getElementById("donationTypeSelect");
    const monetarySection = document.getElementById("monetaryPaymentSection");
    const logisticsSection = document.getElementById("logisticsSection");
    const foodSpecificSection = document.getElementById("foodSpecificSection");

    // Function to update visibility of conditional sections
    function updateFormSections() {
        const selectedValue = donationTypeSelect.value;

        // Reset visibility
        monetarySection.style.display = "none";
        logisticsSection.style.display = "none";
        foodSpecificSection.style.display = "none";

        // Show sections depending on what the user chose
        if (selectedValue === "Money") {
            monetarySection.style.display = "block";
        } else if (selectedValue === "Food") {
            foodSpecificSection.style.display = "block";
            logisticsSection.style.display = "block";
        } else if (selectedValue === "Clothes") {
            logisticsSection.style.display = "block";
        }
    }

    // Listen for changes in the dropdown
    if (donationTypeSelect) {
        donationTypeSelect.addEventListener("change", updateFormSections);
        // Run on page load in case of validation re-rendering
        updateFormSections();
    }
});