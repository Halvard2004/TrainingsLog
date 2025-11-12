<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { store } from '@/components/Store';
import { onMounted } from 'vue';
import { http } from '@/api/http';

var model = {
    inputs: {
        id: null,
        userConnection: null,
        logText: "",
        date: null,
        starttime: null,
        endtime: null,
        
    }
}

</script>

<template>
    <main>
        <RouterLink to="/add/log"><button>Add Log</button></RouterLink>
        <RouterLink to="/add/tag"><button>Add Tag</button></RouterLink>
        <RouterView />
    </main>
</template>

<style scoped>
.TimeInDay > input {
    margin: 20px;
}

.TimeInDay > h2 {
    display: flex;
}

#Text {
    width: 20vw;
    height: 30vh;
}
</style>