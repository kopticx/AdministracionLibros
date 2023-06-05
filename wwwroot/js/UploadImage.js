const imagePreview = document.getElementById('ImagePreview')
const imageUploader = document.getElementById('ImageUploader');

console.log(imagePreview);

imageUploader.addEventListener("change", (e) => {
    var file = e.target.files[0]

    var allowedExtensions = /(jpg|jpeg|png|gif)$/i;
    if (!allowedExtensions.exec(file.name.split('.')[1])) {
        alert('Extensión no permitida. Utiliza: .jpeg/.jpg/.png/.gif.');
        ImagenUrl.value = null;
        imageUploader.value = '';
        imagePreview.src = '';
        return false;
    }

    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (e) => {
        e.preventDefault();

        if (imagePreview !== null) {
            imagePreview.src = e.target.result;
        }
        ImagenUrl.value = e.target.result;
    };
});