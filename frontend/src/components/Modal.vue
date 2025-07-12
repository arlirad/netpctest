<script setup>
defineProps({ header: String, visible: Boolean });
defineEmits(['close'])
</script>

<template>
    <div v-if="visible" class="modal-backdrop">
        <div class="modal layer3">
            <h2>{{ header }}</h2>
            <button class="modal-close" @click="$emit('close')">&#10005;</button>
            <slot></slot>
        </div>
    </div>
</template>

<style scoped>
.modal-backdrop {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background: rgba(0.00, 0.00, 0.00, 0.50);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1000;
}

.modal {
    padding: 2rem;
    min-width: 300px;
    min-height: 200px;
    position: relative;
}

.modal-close {
    position: absolute;
    top: 0.5rem;
    right: 0.5rem;
    font-size: 1.5rem;
    border: none;
    background: none;
    cursor: pointer
}
</style>