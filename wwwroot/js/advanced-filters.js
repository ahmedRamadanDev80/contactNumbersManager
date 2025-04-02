document.getElementById("toggleFiltersBtn").addEventListener("click", function () {
    var filterSection = document.getElementById("advancedFilters");
    if (filterSection.style.display === "none") {
        filterSection.style.display = "block";
        this.textContent = "Hide Advanced Filters";
    } else {
        filterSection.style.display = "none";
        this.textContent = "Show Advanced Filters";
    }
});