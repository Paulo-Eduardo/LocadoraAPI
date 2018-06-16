import Vue from 'vue'
import Router from 'vue-router'
import Login from './views/Login.vue'
import Filme from './views/Filme.vue'
import Genero from './views/Genero.vue'
import Locacao from './views/Locacao.vue'



Vue.use(Router)

const router =  new Router({
  routes: [
    {
      path: '/login',
      name: 'login',
      component: Login
    },    
    {
      path: '/',
      name: 'login',
      component: Login
    },
    {
      path: '/filme',
      name: 'filme',
      component: Filme
    },
    {
      path: '/genero',
      name: 'genero',
      component: Genero
    },
    {
      path: '/locacao',
      name: 'locacao',
      component: Locacao
    }
  ]
});

router.beforeEach((to, from, next) => {
  if(to.path != '/login') {
      if(localStorage.token) {
        fetch(localStorage.link + "/api/Account", {
          headers: {
            "Content-Type": "application/json",
            "Authorization" : "Bearer " + localStorage.token
          }
        })
        .then(function(response) {
          if(response.status == 401){
            next('login');
          }
          else {
            next();
          }
        });
      } else {
        next('login');
      }
  } else {
      next();
  }
})

export default router