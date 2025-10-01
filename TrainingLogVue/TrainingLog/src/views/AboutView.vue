<script setup>
import { ref } from 'vue';
import { http } from '@/api/http';

const test = ref(1);


async function Test() {
          let url = '/GetTest';
          const res = await http.get(url);
        test.value = res.data;
        console.log(test.value)

}
</script>

<template>
  <div class="about">
    <h1>This is an about page</h1>
  </div>
  <button v-on:click="Test">Test</button>
  {{ test }}
</template>

<style> 
</style>
