<script setup>
import { RouterLink, RouterView, useRoute } from 'vue-router'
import { store } from '@/components/Store';
import { ref, onMounted } from 'vue';
import { http } from '@/api/http';

var model = {
    route: useRoute(),
    inputs: {
        
    },
    log: ref({

    }),
    tag: ref({

    })
}

onMounted(async () => {
    await GetLog()
    GetTag()
})

async function GetLog() {
    let url = '/GetLog/' + model.route.params.loggId;
    const res = await http.get(url);
    model.log.value = res.data;
    model.log.value.date = new Date(model.log.value.date).toLocaleDateString();
    console.log(model.log.value)
}
async function GetTag() {
        let url = '/GetTagFromLogId/' + model.route.params.loggId;
    const res = await http.get(url);
    model.tag.value = res.data;
}

</script>

<template>
    <main>
        <h2>Date: {{ model.log.value.date }}</h2>
        <h2>{{ model.log.value.logText }}</h2>
        {{ model.log.value.startTime }}
        {{ model.log.value.endTime }}
         <button>{{ model.tag.value.title }}</button>

    </main>
</template>