/* This file is primarily to set vue components and
 * objects. */

/* Uses the bootstrap navbar, with added functionality
 * to pass the user's information for display. */
Vue.component('navbar', {
    props: [
        "showclient",
        "avatarurl",
        "clientusername"
    ],
    template: `
        <div style="height: 100%; width: 100%; position: absolute; top: 0;">
            <nav class="navbar navbar-dark sticky-top bg-dark navbar-expand-lg">
                <a class="navbar-brand ml-auto fill-absolute behind" href="#">
                    <div class="center-inline">Chat Defender</div>
                </a>
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item active">
                        <template v-if="showclient">
                            <div class="account-name">{{ clientusername }}</div>
                            <img v-bind:src="avatarurl" alt="" height="39px" width="39px"/>
                        </template>
                        <template v-else>
                            <a class="btn btn-outline-primary" href="/account/login">
                                <i class="fab fa-discord" style="position: relative; top: 1px;"></i> LOGIN
                            </a>
                        </template>
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
});

// Component for post box that displays a user's posts.
Vue.component('post', {
    template: `
      <div class="post">
        <img src="https://pbs.twimg.com/profile_images/846659478120366082/K-kZVvT8_400x400.jpg" 
            height="48px" 
            width="48px" 
            class="user-avatar">
        <div class="card">
          <div class="card-header">
            Why we are eating squirrels
          </div>
          <div class="card-body">
            hello! <br /> dont worry about me
          </div>
        </div>
      </div>
    `
});

const app = new Vue({
    el: '#app',
    data: {
        user: undefined
    }
});