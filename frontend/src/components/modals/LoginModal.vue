<script setup>
import { ref } from 'vue';
import Modal from '../Modal.vue';

const email = ref('');
const password = ref('');

defineProps({ visible: Boolean });
// defineProps(['close', 'submit']);
</script>

<template>
    <Modal :header="$t('ui.login')" :visible="visible" @close="$emit('close')">
        <form @submit.prevent="submit">
            <label>{{ $t('ui.login.email') }}</label>
            <input class="layer2 layer2-input" v-model="email" type="email" required />

            <label>{{ $t('ui.login.password') }}</label>
            <input class="layer2 layer2-input" v-model="password" type="password" required />

            <button class="layer2" type="submit">{{ $t('ui.login.submit') }}</button>
        </form>
    </Modal>
</template>

<style scoped>
label,
input,
button {
    display: block;
}

label {
    margin-bottom: 0.20em;
    text-align: left;
}

input {
    margin-bottom: 0.40em;
    width: 100%;
}

button {
    margin-top: 1.00em;
}
</style>