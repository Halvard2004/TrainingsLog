<script setup>
import { store } from '@/components/Store';
import { onMounted } from 'vue';
import { http } from '@/api/http';

var model = {
    inputs: {
        id: null,
        title: "",
        user_Id: null
    }
}

onMounted(() => {
     if(store.user.id !== null){
        model.inputs.user_Id = store.user.id
     }
        });

async function CreateNewTag() {
    model.inputs.id = generateGUID();
        let url = '/CreateTag';
        const res = await http.post(url, model.inputs);
        console.log(res)

}


function generateGUID() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    const r = Math.random() * 16 | 0; 
    const v = c === 'x' ? r : (r & 0x3 | 0x8); 
    return v.toString(16);
  });
}

</script>

<template>
 <h1>Legg til Tag</h1>
        <form @submit.prevent="CreateNewTag" class="form">
        <h2>Title</h2>
        <div class="TagTitle">
            <input id="Title" required v-model="model.inputs.title"></input>
        </div>
         <div class="submit"> 
            <button>Submit</button>
        </div>
       
        </form>
</template>

