//General Variables
var bg = document.getElementById('bg');
var hamburger = document.getElementById('hamburger');
var ul = document.getElementById('ul');
var fcross = document.getElementById('fcross');
var scross = document.getElementById('scross');
var inst = document.getElementById('inst');
var btnNone = document.getElementById('btnNone');
var btnShow = document.getElementById('btnShow');
var i = 0;
//Ends here

//Register Page Variable
var zIndex = document.getElementById('zIndex');

//Ends here

// Registration Page Variable
var special = document.getElementById('special');
// Ends here


var table = document.getElementById('table');


function RemoveAccess() {
    zIndex.style.zIndex = "-5";
}
function AllowAccess() {
    zIndex.style.zIndex = "1";
}

// Registratiopn Page Function To Add Blur Effect to mainContainer//

function AddBlur()
{
    special.classList.add("special")
}
function RemoveBlur()
{
    special.classList.remove("special")
}

// Ends Here


//Restrict Table
function RestrictTable()
{
    table.style.zIndex = "-2";
}

function ShowTable()
{
    table.style.zIndex = "1";
}


hamburger.addEventListener('click', ()=>{
    i++
    if (i % 2 == 1) {
        // General Functionality
        ul.style.visibility = "visible";
        fcross.classList.add("fshow", "red")
        scross.classList.add("sshow", "red")
        bg.classList.add("bgshow")
        document.body.style.overflow = "hidden";
        // Ends here
        
        if(special != null && zIndex != null && table != null)
        {
            AddBlur()
            RemoveAccess()
            RestrictTable()
        }    
        if (special != null) {
            AddBlur()
        } 
        if(zIndex != null)
        {
            RemoveAccess()
        }
        if(table != null)
        {
            RestrictTable()
        }
               
    }
    else{
        ul.style.visibility = "hidden";
        fcross.classList.remove("fshow", "red")
        scross.classList.remove("sshow", "red") 
        bg.classList.remove("bgshow")
        document.body.style.overflow = "visible"
        if(special != null && zIndex != null && table != null)
        {
            RemoveBlur()
            AllowAccess()
            ShowTable()
        }    
        if (special != null) {
            RemoveBlur()
        } 
        if(zIndex != null)
        {
            AllowAccess()
        }
        if(table != null)
        {
            ShowTable()
        }
        i=0;
    }  
})

if(btnShow != null && btnNone != null)
{
    btnShow.addEventListener('click', ()=>{
        inst.classList.remove('show')
        zIndex.classList.add('show')
    
    })
    btnNone.addEventListener('click', ()=>{
        inst.classList.add('show')
        zIndex.classList.remove('show')
    
    })

}



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
