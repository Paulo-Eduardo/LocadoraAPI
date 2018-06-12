import Vue from 'vue'
import App from './App.vue'
import router from './router'

Vue.config.productionTip = false
localStorage.link = "http://localhost:81"

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
