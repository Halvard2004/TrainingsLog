<script setup>
import { RouterLink, RouterView, useRoute } from 'vue-router'
import { store } from '@/components/Store';
import { onMounted } from 'vue';
import { http } from '@/api/http';

var model = {
    route: useRoute(),
    inputs: {
        
    },
    log: {

    },
    tag: {

    }
}

onMounted(async () => {
    await GetLog()
    GetTag()
})

async function GetLog() {
    let url = '/GetLog/' + model.route.params.loggId;
    const res = await http.get(url);
    model.log = res.data;
    model.log.date = new Date(model.log.date).toLocaleDateString();
}
async function GetTag() {
        let url = '/GetTagFromLogId/' + model.route.params.loggId;
    const res = await http.get(url);
    model.tag = res.data;
}

</script>

<template>
    <main>
        <h2>Date: {{ model.log.date }}</h2>
        <h2>{{ model.log.logText }}</h2>
        {{ model.log.startTime }}
        {{ model.log.endTime }}
         <button>{{ model.tag.title }}</button>

    </main>
</template>