var form = document.getElementById('form');
var formContainer = document.getElementById('formContainer');
var items = document.querySelectorAll(".del");
var cancelBtn = document.getElementById('cancelBtn');
var bgCover = document.getElementById('bgCover');


function displayForm(e) {
    bgCover.classList.remove('show')
    formContainer.classList.remove('show');
    document.body.style.overflow ="hidden";
    const value = this.getAttribute("idItem");
    form.setAttribute("action", `/IT/DeleteReferenceID/${value}`);
    console.log(form);
}

items.forEach(element => element.addEventListener('click', displayForm));

cancelBtn.addEventListener('click', () =>{
    formContainer.classList.add('show');
    bgCover.classList.add('show');
    document.body.style.overflow = "visible";
});
