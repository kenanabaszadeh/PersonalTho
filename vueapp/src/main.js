import { createApp } from 'vue'
import App from './App.vue'
import Dashboard from "./components/Dashboard.vue"
import About from "./components/HelloWorld.vue"
import Login from "./components/Login.vue" // Import Login component
import Register from "./components/Register.vue" // Import Register component
import { createRouter, createWebHashHistory } from 'vue-router'

const routes = [
    { path: '/', component: Dashboard },
    { path: '/about', component: About },
    { path: '/login', component: Login }, // Define a route for the Login component
    { path: '/register', component: Register }, // Define a route for the Register component
]

const router = createRouter({
    history: createWebHashHistory(),
    routes,
})

const app = createApp(App)
app.use(router)
app.mount('#app')
