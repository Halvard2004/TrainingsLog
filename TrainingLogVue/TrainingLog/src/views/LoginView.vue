<script setup>
import { http } from '@/api/http';
import { store } from '@/components/Store';
import { useRouter } from 'vue-router';


const router = useRouter();

async function Login() {
        let url = '/LoginUser';
        const res = await http.post(url, formInputs);
        store.LoginUser(res.data);


        router.replace('/')

}


const  formInputs = {
                username: '',
                password: ''
};

</script>

<template>
    <main>
        <h2>
            Hvis du har enn bruker kan du logge inn her
        </h2>
        <form @submit.prevent="Login" class="form"> 
            <label>Username: <br>
                <input required v-model="formInputs.username">
            </label>
            <br>
            <br>
                <label>Password: <br>
                    <input type="password" required v-model="formInputs.password"></input>
                </label>
            <br>
            <br>
            <div class="submit">
                <button>Log inn</button>
            </div>
        </form>
    </main>
</template>

<style scoped>
main {
    display: flex;
    justify-content: center; 
    align-items: center;
    width: 60%;
}
.submit > button {
background-color: green;
  border-radius: 10px;
  max-height: 40px;
  height: 4vh;
  width: 50%;
}
label {
    font-size: x-large;
}
</style>