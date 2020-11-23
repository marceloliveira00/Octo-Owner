import Vue from 'vue'
import App from './App.vue'
import Vuesax from 'vuesax'
import 'vuesax/dist/vuesax.css'
import 'material-icons/iconfont/material-icons.css';
import axios from 'axios'
import VueTheMask from 'vue-the-mask'
Vue.use(VueTheMask)

Vue.config.productionTip = false

Vue.prototype.$http = axios

Vue.use(Vuesax, {
  // options here
})

new Vue({
  render: h => h(App),
}).$mount('#app')