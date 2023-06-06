<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css" integrity="sha384-hWVjflwFxL6sNzntih27bfxkr27PmbbK/iSvJ+a4+0owXq79v+lsFkW54bOGbiDQ" crossorigin="anonymous">
    <link rel="stylesheet" href="css/main.css">
    <!--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>-->
    <title>Aditya Raj</title>
</head>

<body>
    <header>
      <div class="menu-btn">
            <div class="btn-line"></div>
            <div class="btn-line"></div>
            <div class="btn-line"></div>
        </div>
        <div class="icons">
            <a href="/index.html" class="nav-link"><b>ENG</b></a>&nbsp &nbsp
            <a href="/japanese/index.html" class="nav-link"><b>日本語 </b></a></div>
        </div>
        <nav class="menu">
            <div class="menu-branding">
                <div class="circular--portrait"><im src="img/portrait.jpg" /></div>
            </div>
            <ul class="menu-nav">
                <li class="nav-item current"><a href="index.html" class="nav-link">About Me </a></li>
                <li class="nav-item"><a href="work_experiences.html" class="nav-link">Work Experience </a></li>
                <li class="nav-item"><a href="education.html" class="nav-link">Educations </a></li>
                <li class="nav-item"><a href="work.html" class="nav-link">Projects </a></li>
                <li class="nav-item"><a href="skill.html" class="nav-link">Skills </a></li>
                <li class="nav-item"><a href="achievements.html" class="nav-link">Certifications </a></li>
                <li class="nav-item"><a href="winner.html" class="nav-link">Achievements </a></li>
                <li class="nav-item current"><a href="contact.html" class="nav-link">Contact Me </a></li>
            </ul>
        </nav>
        
    </header>
    <main id="about">
        <h1 class="lg-heading" style="margin-top:2px;">Aditya <span class="text-secondary">Raj</span></h1>

        <div class="about-info"><im src="img/portrait.jpg" alt="Aditya" class="bio-image">
            <div class="bio">
                <h4 class="text-secondary">About Raj</h4>
                <h2 class="sm-heading">                   
                    Raj is a software developer based in Shizuoka Prefecture, Japan, with nearly two years of experience in the field. He is currently working in C#.NET, Xamarin, and PHP. In addition to his technical expertise, Raj has a passion for spirituality and enjoys exploring this interest through meditation and other spiritual practices. This strength not only enhances his personal life but also benefits his professional work by bringing a unique perspective and approach to problem-solving. In his personal life, Raj is a committed vegetarian, reflecting his values of health and sustainability.
                </h2>
             <h4 class="text-secondary">Background</h4>
                <h2 class="sm-heading">
                    Raj was born and raised in Bihar, India, where he developed a passion for technology from a young age. He completed his high school and intermediate studies through the UP Board before pursuing a bachelor's degree in computer applications from a leading university in Patna, Bihar. After earning his degree, Raj decided to further his education and completed a Master in Computer Application from Bangalore, Karnataka.<br>
                    During his time in college, Raj's dedication and hard work led him to secure a placement with a prominent Japanese company. This opportunity allowed him to pursue his career as a software developer, and he has been thriving in this field ever since. Currently based in Japan, Raj continues to build on his skills and knowledge, staying up-to-date with the latest industry trends and developments.
                </h2>
            </div>
        </div>
        <div class="icons-placeholder"></div>
    </main>
    <div class="footer-placeholder"></div>
    <script src="js/main.js"></script>
    <script>
        // Fetch and insert the header content
        /*  fetch('header.html')
           .then(response => response.text())
           .then(headerContent => {
           document.querySelector(".menu-placeholder").innerHTML = headerContent;
           }); */

           // Fetch and insert the footer content
           fetch('footer.html')
           .then(response => response.text())
           .then(footerContent => {
           document.querySelector(".footer-placeholder").innerHTML = footerContent;
           });

           // Fetch and insert the icons content
           fetch('icons.html')
           .then(response => response.text())
           .then(iconsContent => {
           document.querySelector(".icons-placeholder").innerHTML = iconsContent;
           });
   </script>
    
</body>

</html>
//And create icon.html file and footer.html file
