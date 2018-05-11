// Win A Car
// Script for the game logic. 
var timer = null; // keeps track of the game framerate
var second = 0; // track time in seconds


// load all images
const SHEEP = 0;
const CAR = 1;
const DOOR = 2;
const RDOOR = 3;
var imgArray = new Array(4);
imgArray[0] = new Image();
imgArray[0].src = "./images/sheep.png";
imgArray[1] = new Image();
imgArray[1].src = "./images/car.png";
imgArray[2] = new Image();
imgArray[2].src = "./images/door.png";
imgArray[3] = new Image();
imgArray[3].src = "./images/rdoor.png";

// scores
var scoreswitch = 0;
var scorestay = 0;
var scoretotal = 0;

// hints
const HINTS = [
    "Please choose a door",                 //0
    "I will open one unchosen door now",    //1
    "Haha, here is a sheep",                //2
    "Please make a second choice",          //3
    "You stay your choice",                 //4
    "You switched your choice",             //5
    "Behold, all doors will be opened now!",//6
    "You won the car, congratulations!",    //7
    "You lost the game.",                   //8
    "Play again"                            //9
];

var hint=HINTS[0];

// game stages
const gBEGIN = 0; 
const gFIRSTCHOICE = 1;
const gOPENONE = 2;
const gSHOWFIRSTSHEEP =3;
const gSECONDCHOICE = 4;
const gSTAY = 5;
const gSWITCH = 6;
const gRESULT = 7;
const gREPLAY = 8;
const gTOBEGIN = 9;

var gState = gBEGIN;

// get GUI elements
var vhint;
var vdoor = [];
var vstay;
var vswitch;
var vtotal;

// flags
var fselected = [false, false, false];
var switched = false;
var opened = [false, false, false];
var firstopen = 0;
var carpos = 0;
var selpos = -1;
var clickdoor = -1;
var sheeppos = [];


// called repeatedly to run the game logic
function run() {
  
    // game logic
    switch(gState)
    {
        case gBEGIN:
            // place the car randomly
            carpos = Math.floor((Math.random()*3));
            var j=0;
            for(i=0; i<3; ++i)
            {
                vdoor[i].style.backgroundImage = "url(" +  imgArray[DOOR].src + ")";
                if(i !== carpos)
                    sheeppos[j++] = i;
                
                // clear flags
                fselected[i]=false;
                opened[i] = false;
                
            }
                // initial scenario
            vstay.innerHTML = scorestay;   
            vswitch.innerHTML = scoreswitch;
            vtotal.innerHTML = scoretotal;

            vhint.innerHTML = HINTS[0]; 
            
            selpos=-1;
            
            gState = gFIRSTCHOICE;
            second = 0;
            break;
        case gFIRSTCHOICE:
            second = 0;
            break;
        case gOPENONE:
            second++;
            if(second === 5)
            {
                // open one door with a sheep behind
                if(sheeppos.includes(selpos))
                {
                    if(sheeppos[0] === selpos)
                        firstopen = sheeppos[1];
                    else
                        firstopen = sheeppos[0];
                }
                else
                {
                    firstopen = sheeppos[Math.floor(Math.random()*2)];
                    
                }
                opened[firstopen] = true;
                vdoor[firstopen].style.backgroundImage = "url(" +  imgArray[SHEEP].src + ")";
                vhint.innerHTML = HINTS[2];
                gState = gSHOWFIRSTSHEEP;
                second = 0;
            }
            break;
        case gSHOWFIRSTSHEEP:
            second++;
            if(second === 5)
            {
                vhint.innerHTML = HINTS[3];
                gState = gSECONDCHOICE;
                second = 0;
            }
            break;
        case gSECONDCHOICE:
            break;
        case gSTAY:
        case gSWITCH:
            second++;
            if(second === 5)
            {
                vhint.innerHTML = HINTS[6];
                gState = gRESULT;
                second = 0;
            }
            break;
        case gRESULT:
            second++;
            if(second === 5)
            {
                if(carpos === selpos)
                {
                    vhint.innerHTML = HINTS[7];
                    if(switched)
                        scoreswitch++;
                    else
                        scorestay++;
                }
                else
                {
                    vhint.innerHTML = HINTS[8];
                }
                vdoor[carpos].style.backgroundImage = "url(" +  imgArray[CAR].src + ")";
                for(i=0; i<sheeppos.length; ++i)
                {
                    vdoor[sheeppos[i]].style.backgroundImage = "url(" +  imgArray[SHEEP].src + ")";
                }
                scoretotal++;
                
                vtotal.innerHTML = scoretotal;
                vswitch.innerHTML = scoreswitch;
                vstay.innerHTML = scorestay;
                
                gState = gREPLAY;
                second = 0;
            }
            break;
        case gREPLAY:
            second++;
            if(second === 5)
            {
                vhint.innerHTML = HINTS[9];
                gState = gTOBEGIN;
                second = 0;
            }
            break;
        case gTOBEGIN:
            second++;
            if(second === 5)
            {
                vhint.innerHTML = HINTS[9];
                gState = gBEGIN;
                second = 0;
            }
            break;            
        default:
            gState = gBEGIN;
            break;
    }
    
    // stop the game when it is a day
    if (second >= 86400) {
        window.clearInterval(timer);
        timer = null;
    }
}

function click(){
    switch(this.id)
    {
        case "door1":
            clickdoor = 0;
            break;
        case "door2":
            clickdoor = 1;
            break;
        case "door3":
            clickdoor = 2;
            break;
        default:
            alert("This can not happen!");
            break;
    }
    
    if(gState === gFIRSTCHOICE)
    {
        //alert("You chose " + this.id);
        selpos = clickdoor;
        fselected[selpos] = true;
        vdoor[selpos].style.backgroundImage = "url(" +  imgArray[RDOOR].src + ")";
        vhint.innerHTML = HINTS[1];
        gState = gOPENONE;
    }
    
    if(gState === gSECONDCHOICE && !opened[clickdoor] )
    {
        if(clickdoor !== selpos) // switch
        {
            fselected[selpos] = false;
            switched = true;
            vdoor[selpos].style.backgroundImage = "url(" +  imgArray[DOOR].src + ")";
            
            selpos = clickdoor;
            fselected[selpos] = true;
            vdoor[selpos].style.backgroundImage = "url(" +  imgArray[RDOOR].src + ")";
            
            vhint.innerHTML = HINTS[5];
            gState = gSWITCH;
        }
        else //stay
        {
            vhint.innerHTML = HINTS[4];
            gState = gSTAY;
        }
    }
}

// inserts the proper image into the image area and
// begins the game
function display() {
    if (timer)
        return;
    
    for(i=0; i<3; ++i)
    {
        vdoor[i] = document.getElementById("door"+(i+1));
        // register event handlers
        vdoor[i].addEventListener("click", click, false);
    }    
    // get GUI elements
    vhint = document.getElementById("hint");
   
    vstay = document.getElementById("txtstay");
    vswitch = document.getElementById("txtswitch");
    vtotal = document.getElementById("txttotal");    
   
    gState = gBEGIN;
   
    second = 0;
    timer = window.setInterval("run()", 1000); // step is per second
}



window.addEventListener("load", display, false);


