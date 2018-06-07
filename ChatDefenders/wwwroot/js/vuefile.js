Vue.component('navbar', {
    props:['showClient'],
    template: `
        <div>
            <nav class="navbar navbar-dark sticky-top bg-dark navbar-expand-lg">
                <a class="navbar-brand ml-auto fill-absolute behind" href="#">
                    <div class="center-inline">Chat Defender</div>
                </a>
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item active">
                        <button type="button" class="btn btn-outline-primary">
                            <i class="fab fa-discord" style="position: relative; top: 1px;"></i> LOGIN
                        </button>
                    </li>
                </ul>
            </nav>
            <div class="hr-absolute"></div>
            <nav class="navbar navbar-dark sticky-top bg-dark navbar-expand-lg">
                <button class="navbar-toggler ml-auto" type="button" data-toggle="collapse" data-target="#navbarSupportedContent">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item danger ml-2">
                            <a class="nav-link" href="#">Contact</a>
                        </li>
                        <li class="nav-item ml-2">
                            <a class="nav-link active" href="#">Home</a>
                        </li>
                        <li class="nav-item ml-2">
                            <a class="nav-link" href="#">About</a>
                        </li>
                        <li class="nav-item dropdown ml-2">
                            <a class="nav-link dropdown-toggle" data-toggle="dropdown" href="#">Important Links</a>

                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="#">wow! thats literally insane</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
    `
})

const app = new Vue({
    el: '#app'
})