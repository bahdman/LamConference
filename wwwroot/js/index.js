var ham = document.getElementById('ham');
var ul = document.getElementById('ul');
var bg = document.getElementById('bg');
var fcross = document.getElementById('fcross');
var scross = document.getElementById('scross');
var header = document.getElementById('header');
var i = 1;

function TransformHam()
{
    fcross.classList.add("rfcross");
    scross.classList.add("rscross");
    ul.classList.add('show')
    bg.classList.add('show')
    document.body.style.overflow = "hidden";
}

function ReverseHam()
{
    fcross.classList.remove("rfcross");
    scross.classList.remove("rscross");
    ul.classList.remove('show')
    bg.classList.remove('show')
    document.body.style.overflow = "visible";
    i=1;
}

ham.addEventListener('click', ()=>{
    i++;
    if(i%2 == 0)
    {
        TransformHam();
    }
    else{
        ReverseHam();
    }
})

bg.addEventListener("click", ()=>{
    i++
    ReverseHam();
})

document.addEventListener('scroll', () =>{
    if (window.scrollY > 90) {

        header.classList.add("headerActive");
        
    }
    else{
        header.classList.remove("headerActive");
    }
})
    

