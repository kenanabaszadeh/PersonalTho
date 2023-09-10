<script setup>
    import { ref, reactive } from 'vue'

    // Create reactive properties for email and password
    const formData = reactive({
        email: '',
        password: ''
    })

    async function loginFunc() {
        try {
            const response = await fetch("https://localhost:7154/api/auth/login", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Username: formData.email, 
                    Password: formData.password
                })
            })

            if (response.ok) {
                const token = await authenticateUser();  

                if (token) {
                    localStorage.setItem('token', token);

                    this.$router.push('/');
                } else {
                    console.error('Login failed');
                }
            } else {
                console.error('Login failed');
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    }
</script>


<template>
    <form>
        <label for="email">Emailiniz: </label>
        <input type="email" name="email" v-model="formData.email" />
        <label for="password">Parolunuz: </label>
        <input type="password" name="password" v-model="formData.password" />
        <button type="button" @click="loginFunc">Login</button>
    </form>
</template>
