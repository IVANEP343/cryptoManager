<script setup>
import { ref, onMounted } from 'vue';
import { fetchClients }      from '../services/clientService';
import { createTransaction } from '../services/transactionService';

const clients = ref([]);
const form    = ref({
  cryptoCode: '',
  action: 'purchase',
  cryptoAmount: 0,
  dateTime: '',
  clientId: null
});
const message = ref('');

onMounted(async () => {
  // Carga la lista de clientes para el select
  const { data } = await fetchClients();
  clients.value  = data;
});

const submitForm = async () => {
  try {
    // Envía solo los campos del DTO: sin 'money'
    await createTransaction({
      cryptoCode:   form.value.cryptoCode,
      action:       form.value.action,
      cryptoAmount: form.value.cryptoAmount,
      dateTime:     form.value.dateTime,
      clientId:     form.value.clientId
    });
    message.value = 'Purchase saved!';
    // Resetear formulario
    form.value = {
      cryptoCode:   '',
      action:       'purchase',
      cryptoAmount: 0,
      dateTime:     '',
      clientId:     null
    };
  } catch (err) {
    // Muestra mensaje de error
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
      <option value="BTC">Bitcoin</option>
      <option value="ETH">Ethereum</option>
      <option value="USDC">USDC</option>
    </select>
  </div>

  <div class="mb-2">
    <label class="block">Amount</label>
    <input
      v-model.number="form.cryptoAmount"
      type="number"
      step="0.00000001"
      class="border p-1 w-full"
    />
  </div>

  <div class="mb-4">
    <label class="block">Date & Time</label>
    <input
      v-model="form.dateTime"
      type="datetime-local"
      class="border p-1 w-full"
    />
  </div>

  <button
    @click="submitForm"
    class="bg-blue-600 text-white px-4 py-2 rounded"
  >
    Save
  </button>

  <p v-if="message" class="mt-2 text-green-600">{{ message }}</p>
</template>

<style scoped>
button {
  background-color: #4CAF50; /* Green */
  border: none;
  color: white;
  padding: 15px 32px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  cursor: pointer;
}

button:hover {
  background-color: #45a049; /* Darker green */
}

nav {
  display: flex;
  gap: 1rem;
}
</style>
