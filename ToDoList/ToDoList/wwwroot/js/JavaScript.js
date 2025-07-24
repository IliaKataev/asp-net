
window.enableDragPrevention = (element) => {
    element.addEventListener('dragover', function (e) {
        e.preventDefault();
    });
};
