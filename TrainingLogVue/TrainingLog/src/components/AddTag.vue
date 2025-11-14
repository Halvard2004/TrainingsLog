<script setup>
import { store } from '@/components/Store';
import { onMounted, ref } from 'vue';
import { http } from '@/api/http';

var model = {
    inputs: ref({
        id: null,
        title: "",
        user_Id: null
    }),
    tags: ref([]),
}

onMounted(() => {
     if(store.user.id !== null){
        model.inputs.value.user_Id = store.user.id
        GetUserTags();
     }
        });

async function CreateNewTag() {
    model.inputs.value.id = generateGUID();
        let url = '/CreateTag';
        const res = await http.post(url, model.inputs.value);
    model.inputs.value = {};
}



async function GetUserTags() {
    let url = '/GetTags/' + store.user.id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        model.tags.value.push({id: element.id, title: element.title, user_Id: element.user_Id})
    });

}

function generateGUID() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    const r = Math.random() * 16 | 0; 
    const v = c === 'x' ? r : (r & 0x3 | 0x8); 
    return v.toString(16);
  });
}


function selectTag(id) {
    let independed = (model.tags).value.find(element => element.id === id);
    model.inputs.value = JSON.parse(JSON.stringify(independed));
}

function deleteTag(id) {
    console.log(id, "Deleted")
}

async function EditTag() {
    let url = '/EditTag/' + model.inputs.value.id;
    const res = await http.put(url, model.inputs.value);
    
    
}
</script>

<template>
    <h1>Legg til Tag</h1>
        <form @submit.prevent="model.inputs.value.id !== null ? EditTag() : CreateNewTag()" class="form">
        <h2>Title</h2>
        <div class="TagTitle">
            <input id="Title" required v-model="model.inputs.value.title"></input>
        </div>
         <div class="submit"> 
            <button>Submit</button>
        </div>
        </form>    
    <h1>Endre/Delete Tag</h1>
        <button v-for="tag in model.tags.value" v-on:click="selectTag(tag.id)">{{ tag.title }} <button v-on:click="deleteTag(tag.id)">X</button></button>
</template>

