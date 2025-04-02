document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".delete-button").forEach(button => {
        button.addEventListener("click", function (event) {
            event.preventDefault();  // Prevent the form from submitting immediately
            const form = this.closest("form");

            Swal.fire({
                title: 'Are you sure?',
                text: 'You won\'t be able to undo this action!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Yes, delete it!',
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit();  // Submit the form if confirmed
                }
            });
        });
    });
});