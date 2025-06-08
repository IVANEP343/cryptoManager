<script setup>
import { ref, onMounted } from 'vue';
import { fetchClients }        from '../services/clientService';
import { createTransaction }   from '../services/transactionService';

const clients      = ref([]);
const form         = ref({
  cryptoCode: '',
  action: 'purchase',
  cryptoAmount: 0,
  money: 0,          // ← por ahora lo ingresa el usuario
  dateTime: '',
  clientId: null
});
const message      = ref('');

onMounted(async () => {
  const { data } = await fetchClients();
  clients.value = data;
});

const submitForm = async () => {
  try {
    await createTransaction(form.value);
    message.value = 'Purchase saved!';
    // reset
    form.value = { cryptoCode: '', action: 'purchase', cryptoAmount: 0, money: 0, dateTime: '', clientId: null };
  } catch (err) {
    message.value = err.response?.data?.title || 'Error saving';
  }
};
</script>

<template>
  <h2 class="text-xl font-bold mb-4">New Purchase</h2>

  <div class="mb-2">
    <label class="block">Client</label>
    <select v-model="form.clientId" class="border p-1 w-full">
      <option :value="null" disabled>Select client</option>
      <option v-for="c in clients" :key="c.id" :value="c.id">
        {{ c.name }}
      </option>
    </select>
  </div>

  <div class="mb-2">
    <label class="block">Crypto</label>
    <select v-model="form.cryptoCode" class="border p-1 w-full">
      <option disabled value="">Choose crypto</option>
      <option value="bitcoin">Bitcoin</option>
      <option value="eth">Ethereum</option>
      <option value="usdc">USDC</option>
    </select>
  </div>

  <div class="mb-2">
    <label class="block">Amount</label>
    <input v-model.number="form.cryptoAmount" type="number" step="0.00000001" class="border p-1 w-full" />
  </div>

  <div class="mb-2">
    <label class="block">Money (ARS)</label>
    <input v-model.number="form.money" type="number" step="0.01" class="border p-1 w-full" />
    <small class="text-gray-500">*Por ahora lo ingresamos a mano; luego vendrá de CriptoYa*</small>
  </div>

  <div class="mb-4">
    <label class="block">Date & Time</label>
    <input v-model="form.dateTime" type="datetime-local" class="border p-1 w-full" />
  </div>

  <button @click="submitForm" class="bg-blue-600 text-white px-4 py-2 rounded">Save</button>

  <p v-if="message" class="mt-2 text-green-600">{{ message }}</p>
</template>
